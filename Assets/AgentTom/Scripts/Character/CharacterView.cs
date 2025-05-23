using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterView : MonoBehaviour
{
	private readonly int VelocityKey = Animator.StringToHash("Velocity");
	private readonly int DeadKey = Animator.StringToHash("Dead");
	private readonly int DamagedKey = Animator.StringToHash("Damaged");
	private readonly int InJumpProcessKey = Animator.StringToHash("InJumpProcess");
	
	private const string EdgeKey = "_Edge";
	private const string YOffsetKey = "_YOffset";
	private const string HitScaleKey = "_HitScale";

	private const float TimeToDissolve = 5f;
	private const float TimeToTakeHit = 0.25f;
	private const float MaxYOffset = 2f;
	private const float MaxAddedScale = 0.5f;

	[SerializeField] private Animator _animator;
	[SerializeField] private Character _character;
	[SerializeField] private ParticleSystem _damageEffectPrefab;
	[SerializeField] private Image _filledImage;
	[SerializeField] private TMP_Text _controllerText;

	[SerializeField] private Transform _body;
	[SerializeField] private Transform _hpBar;

	private const float _minWeight = 0f;
	private const float _maxWeight = 1f;
	
	private	SkinnedMeshRenderer _renderer;
	private Coroutine _dissilveProcess;
	private Coroutine _hitProcess;

	private int _maxHealth;

	private float HealthPersent => _character.HealthValue * 100 / _maxHealth;	
	
	private void Awake()
	{
		_maxHealth = _character.HealthValue;
		ShowHealthPoints();

		_renderer = _body.GetComponent<SkinnedMeshRenderer>();
	}

	private void Update()
	{
		if (_character.IsDead)
			DeadProcess();

		_animator.SetBool(InJumpProcessKey, _character.InJumpProcess);

		SetInjuredBehaviour();

		WalkingProcess();
	}

	private void DeadProcess()
	{
		_animator.SetTrigger(DeadKey);
	}

	public void Dissolve()
	{
		_hpBar.gameObject.SetActive(false);

		if (_dissilveProcess != null)
			StopCoroutine(_dissilveProcess);

		_dissilveProcess = StartCoroutine(DissolveProcess());
	}

	private IEnumerator DissolveProcess()
	{
		float time = 0;

		while (time <= TimeToDissolve)
		{
			SetFloatFor(_renderer, EdgeKey, time / TimeToDissolve);

			time += Time.deltaTime;
			yield return null;
		}

		_dissilveProcess = null;
	}

	private IEnumerator HitProcess()
	{		
		float time = 0;		

		while (time <= TimeToTakeHit)
		{
			SetFloatFor(_renderer, YOffsetKey, time* MaxYOffset / TimeToTakeHit);
			SetFloatFor(_renderer, HitScaleKey, 1f + (time / TimeToTakeHit)* MaxAddedScale);

			time += Time.deltaTime;
			yield return null;
		}

		while (time > 0)
		{
			SetFloatFor(_renderer, YOffsetKey, time * MaxYOffset / TimeToTakeHit);
			SetFloatFor(_renderer, HitScaleKey, 1f + (time / TimeToTakeHit)* MaxAddedScale);

			time -= Time.deltaTime;
			yield return null;
		}
		
		_hitProcess = null;
	}

	private void WalkingProcess()
	{
		if (_character.IsDead)
			return;

		if (_character.CurrentVelocity.magnitude > 0.05f)
			_animator.SetFloat(VelocityKey, 1f);
		else
			_animator.SetFloat(VelocityKey, 0.01f);
	}	

	private void SetInjuredBehaviour()
	{
		if(_character.IsDead)
			return;

		if (HealthPersent > 30)
			_animator.SetLayerWeight(1, _minWeight);
		else
			_animator.SetLayerWeight(1, _maxWeight);
	}

	public void ShowDamageEffects()
	{
		_damageEffectPrefab.Play();
		_animator.SetTrigger(DamagedKey);

		if (_hitProcess != null)
			StopCoroutine(_hitProcess);

		_dissilveProcess = StartCoroutine(HitProcess());
	}

	public void ShowHealthPoints()
	{
		_filledImage.fillAmount = (float)_character.HealthValue / _maxHealth;
	}

	private void SetFloatFor(SkinnedMeshRenderer renderer, string key, float param)
	{			
		renderer.material.SetFloat(key, param);		
	}
}

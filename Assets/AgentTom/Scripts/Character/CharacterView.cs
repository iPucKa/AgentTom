using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterView : MonoBehaviour
{
	private readonly int VelocityKey = Animator.StringToHash("Velocity");
	private readonly int DeadKey = Animator.StringToHash("Dead");
	private readonly int DamagedKey = Animator.StringToHash("Damaged");
	
	private const string EdgeKey = "_Edge";
	private const float TimeToDissolve = 5f;

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
	private Coroutine _process;
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

		if (_process != null)
			StopCoroutine(_process);

		_process = StartCoroutine(DissolveProcess());
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

		_process = null;
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

using UnityEngine;

public class CharacterView : MonoBehaviour
{
	private readonly int VelocityKey = Animator.StringToHash("Velocity");
	private readonly int IsRunningKey = Animator.StringToHash("IsRunning");
	private readonly int DeadKey = Animator.StringToHash("Dead");

	[SerializeField] private Animator _animator;
	[SerializeField] private Character _character;

	private const float _minWeight = 0f;
	private const float _maxWeight = 1f;
	
	private bool _isRunning;
	private int _maxHealth;

	private float HealthPersent => _character.HealthValue * 100 / _maxHealth;	
	
	private void Awake()
	{
		_maxHealth = _character.HealthValue;
		Debug.Log(HealthPersent);
	}

	private void Update()
	{
		if (_character.IsDead)
			DeadProcess();

		SetInjuredBehaviour();

		WalkingProcess();

		if (Input.GetKey(KeyCode.LeftShift))
			RunningProcess();	
	}

	private void DeadProcess()
	{
		_animator.SetTrigger(DeadKey);
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
	
	private void RunningProcess()
	{
		if (_character.IsDead)
			return;

		if (_isRunning == true)
			StopRunning();

		_isRunning = true;
		_animator.SetBool(IsRunningKey, true);
	}

	private void StopRunning()
	{
		_isRunning = false;
		_animator.SetBool(IsRunningKey, false);
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
}

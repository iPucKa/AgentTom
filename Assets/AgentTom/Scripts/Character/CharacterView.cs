using System;
using UnityEngine;

public class CharacterView : MonoBehaviour
{
	private readonly int VelocityKey = Animator.StringToHash("Velocity");
	private readonly int IsRunningKey = Animator.StringToHash("IsRunning");
	private readonly int DeadKey = Animator.StringToHash("Dead");

	[SerializeField] private Animator _animator;
	[SerializeField] private Character _character;

	private const float _maxWeight = 1f;
	private bool _isRunning;
	private int _maxHealth;

	private void Awake()
	{
		_maxHealth = _character.HealthValue;
	}

	private void Update()
	{
		if (_character.HealthValue == _maxHealth)
			SetInjuredBehaviour(0);
		else
			SetInjuredBehaviour(_maxWeight);

		if (_character.CurrentVelocity.magnitude > 0.05f)
			StartWalking();
		else
			StopWalking();

		if (Input.GetKey(KeyCode.LeftShift))
			RunningProcess();
		else if (_isRunning)
			StopRunning();		

		if (_character.HealthValue <= 0)
			DeadProcess();
	}

	private void DeadProcess()
	{
		_animator.SetTrigger(DeadKey);
	}

	private void StopWalking()
	{
		_animator.SetFloat(VelocityKey, 0.01f);
	}

	private void StartWalking()
	{
		_animator.SetFloat(VelocityKey, 1f);
	}

	private void StopRunning()
	{
		_isRunning = false;
		_animator.SetBool(IsRunningKey, false);
	}

	private void RunningProcess()
	{
		if(_isRunning == true)
			return;

		_isRunning = true;
		_animator.SetBool(IsRunningKey, true);
	}

	private void SetInjuredBehaviour(float weight)
	{
		_animator.SetLayerWeight(1, weight);
	}
}

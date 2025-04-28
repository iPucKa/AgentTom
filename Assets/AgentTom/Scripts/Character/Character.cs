using UnityEngine;
using UnityEngine.AI;

public class Character : MonoBehaviour, IMovable, IRotatable, IDamageable, IHealable
{
	private AgentDirectionalMover _mover;
	private DirectionalRotator _rotator;
	private Health _health;

	[SerializeField] private float _moveSpeed;
	[SerializeField] private float _rotationSpeed;
	[SerializeField] private int _maxHealth;
	[SerializeField] private CharacterView _characterView;

	public Vector3 CurrentVelocity => _mover.CurrentVelocity;

	public Quaternion CurrentRotation => _rotator.CurrentRotation;

	public Vector3 Position => transform.position;

	public int HealthValue => _health.Value;

	public bool IsDead => _health.Value == 0;

	private void Awake()
	{
		_mover = new AgentDirectionalMover(GetComponent<NavMeshAgent>(), _moveSpeed);
		_rotator = new DirectionalRotator(transform, _rotationSpeed);
		_health = new Health(_maxHealth);
	}

	private void Update()
	{
		_mover.Update(Time.deltaTime);
		_rotator.Update(Time.deltaTime);
	}

	public void TakeDamage(int damage)
	{
		_characterView.ShowDamageEffects();

		_health.Reduce(damage);

		_characterView.ShowHealthPoints();
	}

	public void AddHealth(int health)
	{
		_health.Add(health);

		_characterView.ShowHealthPoints();
	}

	public void SetMovePosition(Vector3 position)
	{ 
		if(IsDead) 
			return;

		_mover.SetTargetPosition(position); 
	}

	public void SetRotationDirection(Vector3 inputDirection)
	{
		if (IsDead)
			return;

		_rotator.SetInputDirection(inputDirection);
	}

	//public void ShowControllerType(string message) => _characterView.ShowControllerType(message);
}

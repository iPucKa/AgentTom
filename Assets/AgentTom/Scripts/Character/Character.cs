using UnityEngine;
using UnityEngine.AI;

public class Character : MonoBehaviour, IMovable, IRotatable, IDamageable, IHealable
{
	private MouseMover _mover;
	private DirectionalRotator _rotator;
	private Health _health;

	[SerializeField] private float _moveSpeed;
	[SerializeField] private float _rotationSpeed;
	[SerializeField] private int _maxHealth;

	public Vector3 CurrentVelocity => _mover.CurrentVelocity;

	public Quaternion CurrentRotation => _rotator.CurrentRotation;

	public Vector3 Position => transform.position;

	public int HealthValue => _health.Value;

	private void Awake()
	{
		//_mover = new DirectionalMover(GetComponent<CharacterController>(), _moveSpeed);
		_mover = new MouseMover(GetComponent<NavMeshAgent>(), _moveSpeed);
		_rotator = new DirectionalRotator(transform, _rotationSpeed);
		_health = new Health(_maxHealth);
	}

	private void Update()
	{
		_mover.Update(Time.deltaTime);
		_rotator.Update(Time.deltaTime);
	}

	public void TakeDamage(int damage) => _health.Reduce(damage);

	public void AddHealth(int health) => _health.Add(health);

	public void SetMovePosition(Vector3 position) => _mover.SetTargetPosition(position);
	
	public void SetRotationDirection(Vector3 inputDirection) => _rotator.SetInputDirection(inputDirection);	
}

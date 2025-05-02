using UnityEngine;
using UnityEngine.AI;
using UnityEngine.TextCore.Text;

public class Character : MonoBehaviour, IMovable, IRotatable, IDamageable, IHealable
{
	[SerializeField] private float _moveSpeed;
	[SerializeField] private float _rotationSpeed;
	[SerializeField] private int _maxHealth;
	[SerializeField] private CharacterView _characterView;
	
	[SerializeField] private float _jumpSpeed;
	[SerializeField] private AnimationCurve _jumpCurve;

	private AgentDirectionalMover _mover;
	private DirectionalRotator _rotator;
	private Health _health;
	private CharacterJumper _jumper;
	private NavMeshAgent _agent;

	public Vector3 CurrentVelocity => _mover.CurrentVelocity;

	public Quaternion CurrentRotation => _rotator.CurrentRotation;

	public Vector3 Position => transform.position;

	public int HealthValue => _health.Value;

	public bool IsDead => _health.Value == 0;

	public bool InJumpProcess => _jumper.InProcess;

	private void Awake()
	{
		_agent = GetComponent<NavMeshAgent>();

		_mover = new AgentDirectionalMover(_agent, _moveSpeed);
		_rotator = new DirectionalRotator(transform, _rotationSpeed);
		_health = new Health(_maxHealth);
		_jumper = new CharacterJumper(_jumpSpeed, _agent, this, _jumpCurve);
	}

	private void Update()
	{
		_mover.Update(Time.deltaTime);
		_rotator.Update(Time.deltaTime);

		if (IsOnNavMeshLink(out OffMeshLinkData offMeshLinkData))
		{
			if (InJumpProcess == false)
			{
				SetRotationDirection(offMeshLinkData.endPos - offMeshLinkData.startPos);

				Jump(offMeshLinkData);
			}

			return;
		}
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

	private bool IsOnNavMeshLink(out OffMeshLinkData offMeshLinkData)
	{
		if (_agent.isOnOffMeshLink)
		{
			offMeshLinkData = _agent.currentOffMeshLinkData;
			return true;
		}

		offMeshLinkData = default(OffMeshLinkData);
		return false;
	}

	private void Jump(OffMeshLinkData offMeshLinkData) => _jumper.Jump(offMeshLinkData);
}

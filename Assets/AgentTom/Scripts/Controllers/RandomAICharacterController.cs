using UnityEngine;
using UnityEngine.AI;

public class RandomAICharacterController : Controller
{
	private IMovable _movable;
	private Pointer _pointer;

	private NavMeshQueryFilter _queryFilter;
	private NavMeshPath _path = new NavMeshPath();

	private float _currentTime;
	private float _timeToChangeDirection;

	private Vector3 _targetPosition;
	private Vector3 _position;
	private LayerMask _layerMask;

	private const float RadiusRange = 4f;

	public override float IdleTime => _currentTime;

	public RandomAICharacterController(IMovable movable, float timeToChangeDirection, Pointer pointer, NavMeshQueryFilter queryFilter, LayerMask layerMask)
	{
		_movable = movable;
		_timeToChangeDirection = timeToChangeDirection;
		_pointer = pointer;
		_queryFilter = queryFilter;
		_layerMask = layerMask;

		_targetPosition = _movable.Position;

		_currentTime = 0;
	}

	protected override void UpdateLogic(float deltaTime)
	{
		Debug.Log("¬ключили другой контроллер");

		_currentTime += Time.deltaTime;		

		if (_currentTime >= _timeToChangeDirection)
		{
			_position = GetRandomPosition(_movable.Position);

			if (Physics.Raycast(_position, Vector3.down, out RaycastHit hit, Mathf.Infinity, _layerMask.value))
			{
				_targetPosition = hit.point;
			}

			if (TryGetPath(_targetPosition, _path))
			{
				_currentTime = 0;

				_pointer.SetPointerToPosition(_targetPosition);

				_movable.SetMovePosition(_targetPosition);
			}
		}	

		_pointer.UpdatePointerVisibility(_movable.Position);
	}

	public bool TryGetPath(Vector3 targetPosition, NavMeshPath pathToTarget) => NavMeshUtils.TryGetPath(_movable.Position, targetPosition, _queryFilter, pathToTarget);

	private Vector3 GetRandomPosition(Vector3 position)
	{
		float xPosition = position.x + Random.Range(-RadiusRange, RadiusRange);
		float zPosition = position.z + Random.Range(-RadiusRange, RadiusRange);

		return new Vector3(xPosition, 0f, zPosition);
	}
}

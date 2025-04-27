using UnityEngine;
using UnityEngine.AI;

public class PlayerMouseMovableController : Controller
{
	private IMovable _movable;
	private Camera _camera;

	private const int LeftMouseButtonKey = 0;

	private NavMeshQueryFilter _queryFilter;
	private NavMeshPath _path = new NavMeshPath();

	private Pointer _pointer;

	private Vector3 _targetPosition;
	private Vector3 _position;
	private LayerMask _layerMask;

	public PlayerMouseMovableController(IMovable movable, NavMeshQueryFilter queryFilter, Camera camera, Pointer pointer, LayerMask layerMask)
	{
		_movable = movable;
		_queryFilter = queryFilter;
		_camera = camera;
		_pointer = pointer;
		_layerMask = layerMask;
		//_targetPosition = movable.Position;
	}

	protected override void UpdateLogic(float deltaTime)
	{
		//Vector3 inputDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
		_position = Input.mousePosition;

		if (Input.GetMouseButtonDown(LeftMouseButtonKey))
		{
			Ray ray = _camera.ScreenPointToRay(_position);

			if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _layerMask.value))
			{
				_targetPosition = hit.point;
			}

			//Vector3 inputDirection = _targetPosition - _movable.Position;

			//	NavMesh.CalculatePath(_enemy.transform.position, _character.transform.position, _queryFilter, _path);

			if (TryGetPath(_targetPosition, _path))
			{
				_pointer.SetPointerToPosition(_targetPosition);

				_movable.SetMovePosition(_targetPosition); 
			}
		}

		_pointer.UpdatePointerVisibility(_movable.Position);
	}

	public bool TryGetPath(Vector3 targetPosition, NavMeshPath pathToTarget) => NavMeshUtils.TryGetPath(_movable.Position, _targetPosition, _queryFilter, pathToTarget);	
}

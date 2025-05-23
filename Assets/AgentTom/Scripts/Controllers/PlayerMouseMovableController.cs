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
	private Timer _timer;

	private Vector3 _targetPosition;
	private Vector3 _position;
	private LayerMask _layerMask;

	public PlayerMouseMovableController(IMovable movable, Camera camera, Pointer pointer, NavMeshQueryFilter queryFilter, LayerMask layerMask, Timer timer)
	{
		_movable = movable;
		_camera = camera;
		_pointer = pointer;
		_queryFilter = queryFilter;
		_layerMask = layerMask;

		_timer = timer;
	}

	protected override void UpdateLogic(float deltaTime)
	{
		if(_timer.InProcess(out float elapsedTime) == false) 
			_timer.StartProcess();

		_timer.ShowTime("����� �����������: ", elapsedTime);

		_position = Input.mousePosition;

		if (Input.GetMouseButtonDown(LeftMouseButtonKey))
		{
			_timer.StopProcess();

			Ray ray = _camera.ScreenPointToRay(_position);

			if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _layerMask.value))
			{
				_targetPosition = hit.point;
			}

			if (TryGetPath(_targetPosition, _path))
			{
				_pointer.SetPointerToPosition(_targetPosition);

				_movable.SetMovePosition(_targetPosition); 
			}
		}

		_pointer.UpdatePointerVisibility(_movable.Position);
	}

	public bool TryGetPath(Vector3 targetPosition, NavMeshPath pathToTarget) => NavMeshUtils.TryGetPath(_movable.Position, targetPosition, _queryFilter, pathToTarget);	
}

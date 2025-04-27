using UnityEngine;
using UnityEngine.AI;

public class TestExample : MonoBehaviour
{
	[SerializeField] private Character _character;
	[SerializeField] private Pointer _pointerPrefab;
	[SerializeField] private LayerMask _layerMask;

	private Controller _characterController;
	private Controller _aIController;

	private Controller _currentController;
	private Pointer _pointer;

	private Camera _camera;
	private Timer _timer;

	private NavMeshQueryFilter _queryFilter = new NavMeshQueryFilter();

	private const float _timeToChangeBehavoiur = 10f;

	private void Awake()
	{
		_camera = Camera.main;
		_timer = new Timer();

		_queryFilter.agentTypeID = 0;
		_queryFilter.areaMask = NavMesh.AllAreas;

		_pointer = Instantiate(_pointerPrefab, _character.Position, Quaternion.identity);

		_characterController = new CompositController(
			new PlayerMouseMovableController(_character, _camera, _pointer, _queryFilter, _layerMask, _timer),
			new PlayerRotatableController(_character, _character));

		_characterController.Enable();
		_currentController = _characterController;
	}

	private void Update()
	{
		_currentController.Update(Time.deltaTime);

		if (_characterController.IsWorking)
			if (_timer.CurrentTime >= _timeToChangeBehavoiur)
				SwitchControllers();
	}

	private void OnDrawGizmos()
	{
		if (Application.isPlaying)
		{
			Gizmos.color = Color.red;

			Vector3 mouseWorldPosition = _camera.ScreenToWorldPoint(Input.mousePosition);

			Gizmos.DrawSphere(mouseWorldPosition, 1);
			Gizmos.DrawRay(mouseWorldPosition, _camera.transform.forward * 100);
		}
	}

	public void SwitchControllers()
	{
		_characterController.Disable();

		_aIController = new CompositController(
				new RandomAICharacterController(_character, 2, _pointer, _queryFilter, _layerMask),
				new PlayerRotatableController(_character, _character));

		_aIController.Enable();
		_currentController = _aIController;
	}
}

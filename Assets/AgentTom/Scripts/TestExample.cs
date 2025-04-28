using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class TestExample : MonoBehaviour
{
	[SerializeField] private Character _character;
	[SerializeField] private Pointer _pointerPrefab;
	[SerializeField] private LayerMask _layerMask;

	[SerializeField] private TMP_Text _timerTtext;

	private Controller _characterController;

	private Pointer _pointer;

	private Camera _camera;
	private Timer _timer;

	private NavMeshQueryFilter _queryFilter = new NavMeshQueryFilter();

	private const float _timeToChangeBehavoiur = 10f;

	private void Awake()
	{
		_camera = Camera.main;
		_timer = new Timer(_timerTtext);

		_queryFilter.agentTypeID = 0;
		_queryFilter.areaMask = NavMesh.AllAreas;

		_pointer = Instantiate(_pointerPrefab, _character.Position, Quaternion.identity);

		_characterController = new CompositController(
			new TwoBehaviourCompositController(_character, _timer, _timeToChangeBehavoiur, 
				new PlayerMouseMovableController(_character, _camera, _pointer, _queryFilter, _layerMask, _timer),
				new RandomAICharacterController(_character, 2, _pointer, _queryFilter, _layerMask)),
			new PlayerRotatableController(_character, _character));

		_characterController.Enable();
	}

	private void Update()
	{
		_characterController.Update(Time.deltaTime);

		if (_character.IsDead)
			_characterController.Disable();
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
}

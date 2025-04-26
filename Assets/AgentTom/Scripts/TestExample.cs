using UnityEngine;
using UnityEngine.AI;

public class TestExample : MonoBehaviour
{
	[SerializeField] private Character _character;
	[SerializeField] private Pointer _pointerPrefab;

	private Controller _characterController;	

	private Camera _camera;

	private void Awake()
	{	
		_camera = Camera.main;

		NavMeshQueryFilter queryFilter = new NavMeshQueryFilter();
		queryFilter.agentTypeID = 0;
		queryFilter.areaMask = NavMesh.AllAreas;

		Pointer pointer = Instantiate(_pointerPrefab, _character.Position, Quaternion.identity);
		//pointer.gameObject.SetActive(false);

		_characterController = new CompositController(			
			new PlayerMouseMovableController(_character, queryFilter, _camera, pointer),
			new PlayerRotatableController(_character, _character));
		
		_characterController.Enable();
	}

	private void Update()
	{
		_characterController.Update(Time.deltaTime);
	}

	private void OnDrawGizmos()
	{
		if (Application.isPlaying)
		{
			Gizmos.color = Color.red;

			Vector3 mouseWorldPosition = _camera.ScreenToWorldPoint(Input.mousePosition);

			Gizmos.DrawSphere(mouseWorldPosition, 1);
			Gizmos.DrawRay(mouseWorldPosition, _camera.transform.forward * 100);

			//Gizmos.color = Color.magenta;

			//Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
			//Gizmos.DrawRay(ray);
			//Gizmos.DrawSphere(ray.origin, 1);
		}
	}
}

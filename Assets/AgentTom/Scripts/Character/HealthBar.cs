using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Character _owner;

	private float _yPosition;

	private void Awake()
	{
		_yPosition = transform.position.y;
	}

	private void Update()
	{
		transform.position = _owner.transform.position + new Vector3(0f, _yPosition, 0f);
	}
}

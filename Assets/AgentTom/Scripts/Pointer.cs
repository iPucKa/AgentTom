using UnityEngine;

public class Pointer : MonoBehaviour
{
	public void SetPointerToPosition(Vector3 position)
	{
		gameObject.SetActive(true);
		transform.position = position;
	}

	public void UpdatePointerVisibility(Vector3 position)
	{
		float distance = (transform.position - position).magnitude;

		if (distance <= 0.1f)
		{
			gameObject.SetActive(false);
		}
	}
}

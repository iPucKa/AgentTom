using UnityEngine;

public class Destroyer : MonoBehaviour
{
	private const float _deathTime = 3f;

	private void Start()
	{
		Destroy(gameObject, _deathTime);
	}
}

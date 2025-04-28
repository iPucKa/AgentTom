using UnityEngine;

public class Powerup : MonoBehaviour
{

	[SerializeField] private float _rotateSpeed;
	[SerializeField] private ParticleSystem _powerupEffectPrefab;

	private const int BigHealth = 1000;

	private Vector3 _defaultPosition;
	private float _time;

	private void Awake()
	{
		_defaultPosition = transform.position;
	}

	private void Update()
	{
		_time += Time.deltaTime;

		DoAnimation();
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.TryGetComponent(out IHealable healable))
			healable.AddHealth(BigHealth);

		EffectPlay();
		Destroy(gameObject);
	}

	private void DoAnimation()
	{
		transform.Rotate(Vector3.up, _rotateSpeed);
		transform.position = _defaultPosition + Vector3.up * Mathf.Sin(_time) / 1;
	}

	private void EffectPlay()
	{
		Instantiate(_powerupEffectPrefab, transform.position, Quaternion.identity);
		_powerupEffectPrefab.Play();
	}
}

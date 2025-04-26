using UnityEngine;

public class Mine : MonoBehaviour
{
	[SerializeField] private GameObject _nonActiveMine;
	[SerializeField] private GameObject _activeMine;

	[SerializeField] private float _explosionRadius;
	[SerializeField] private float _timeUntilExplosion;
	[SerializeField] private int _damage;

	private bool _isDetonated;
	private float _time;

	private void Update()
	{
		if (_isDetonated == false)
			_time = 0;

		if (_isDetonated)
			_time += Time.deltaTime;

		if (_time >= _timeUntilExplosion)
		{
			Explode();
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.TryGetComponent(out IDamageable damageable))
		{
			_isDetonated = true;
			_activeMine.SetActive(true);
			_nonActiveMine.SetActive(false);
		}
	}

	private void Explode()
	{
		Collider[] detectedColliders = Physics.OverlapSphere(transform.position, _explosionRadius);

		foreach (Collider collider in detectedColliders)
			if (collider.TryGetComponent(out IDamageable damageable))
				damageable.TakeDamage(_damage);

		Destroy(gameObject);
	}
}

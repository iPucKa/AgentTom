using UnityEngine;

public class Mine : MonoBehaviour
{
	[SerializeField] private float _explosionRadius;
	[SerializeField] private float _timeUntilExplosion;
	[SerializeField] private int _damage;

	private bool _isDetonated;
	private bool _isActivated;
	private float _time;

	public bool IsActivated => _isActivated;

	public bool IsDetonated => _isDetonated;

	private void Update()
	{
		if (IsActivated == false)
			_time = 0;

		if (IsActivated)
			_time += Time.deltaTime;

		if (_time >= _timeUntilExplosion && _isDetonated == false)
		{
			Explode();

			_isDetonated = true;
		}

		//if(IsActivated && IsDetonated)
			//Destroy(gameObject);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.TryGetComponent(out IDamageable damageable))
			_isActivated = true;
	}

	private void Explode()
	{
		Collider[] detectedColliders = Physics.OverlapSphere(transform.position, _explosionRadius);

		foreach (Collider collider in detectedColliders)
			if (collider.TryGetComponent(out IDamageable damageable))
				damageable.TakeDamage(_damage);
	}

	private void OnDrawGizmos()
	{
		if (Application.isPlaying)
		{
			Gizmos.color = Color.red;

			Gizmos.DrawWireSphere(transform.position, _explosionRadius);
		}
	}
}

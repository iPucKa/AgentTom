using UnityEngine;
using TMPro;

public class Mine : MonoBehaviour
{
	[SerializeField] private float _explosionRadius;
	[SerializeField] private float _timeToExplosion;
	[SerializeField] private int _damage;

	[SerializeField] private TMP_Text _timerText;

	private Timer _timer;

	private bool _isDetonated;
	private bool _isActivated;

	public bool IsActivated => _isActivated;

	public bool IsDetonated => _isDetonated;

	public float TimeToDetonate => _timer.TimeLimit;

	private void Awake()
	{
		_timer = new Timer(this, _timeToExplosion, _timerText);
	}

	private void Update()
	{
		if (IsActivated == false)
			return;

		if (_timer.InProcess(out float elapsedTime) && elapsedTime >= TimeToDetonate)
			if (_isDetonated == false)
			{
				Explode();

				_isDetonated = true;
				_timer.StopProcess();
			}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.TryGetComponent(out IDamageable damageable))
		{
			if(_timer.InProcess(out float elapsedTime) == false)
			{
				_isActivated = true;
				_timer.StartProcess();
			}			
		}
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

	public bool InActivationProcess(out float elapsedTime) => _timer.InProcess(out elapsedTime);

	public void ShowTime(string message, float elapsedTime) => _timer.ShowTime(message, elapsedTime);
}

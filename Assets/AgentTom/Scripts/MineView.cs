using UnityEngine;

public class MineView : MonoBehaviour
{
	[SerializeField] private Mine _mine;
	[SerializeField] private GameObject _nonActiveMine;
	[SerializeField] private GameObject _activeMine;

	[SerializeField] private ParticleSystem _mineExplosionEffectPrefab;

	private ParticleSystem _explosionEffect;
	private bool _isActivated;

	private void Update()
	{
		if (_mine.IsActivated && _isActivated == false)
		{
			_activeMine.SetActive(true);
			_nonActiveMine.SetActive(false);

			_isActivated = true;
		}

		if (_mine.IsDetonated)
			if (_explosionEffect == null)
			{ 
				_explosionEffect = Instantiate(_mineExplosionEffectPrefab, transform.position, Quaternion.identity);

				DestroyMine();
			}		
	}

	private void DestroyMine()
	{
		_activeMine.SetActive(false);

		Destroy(gameObject, 2);
	}
}

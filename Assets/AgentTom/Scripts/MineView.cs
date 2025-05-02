using UnityEngine;

public class MineView : MonoBehaviour
{
	[SerializeField] private Mine _mine;

	[SerializeField] private ParticleSystem _mineExplosionEffectPrefab;

	private MeshRenderer _renderer;

	private ParticleSystem _explosionEffect;

	private const string TimeToDetonationKey = "_TimeToDetonate";


	private void Awake()
	{
		_renderer = GetComponentInChildren<MeshRenderer>();
	}

	private void Update()
	{
		if (_mine.InActivationProcess(out float elapsedTime))					
			SetFloatFor(_renderer, TimeToDetonationKey, elapsedTime / _mine.TimeToDetonate);		

		if (_mine.IsDetonated)
			if (_explosionEffect == null)
			{ 
				_explosionEffect = Instantiate(_mineExplosionEffectPrefab, transform.position, Quaternion.identity);

				DestroyMine();
			}		
	}

	private void DestroyMine()
	{
		gameObject.SetActive(false);

		Destroy(gameObject);
	}

	private void SetFloatFor(MeshRenderer renderer, string key, float param)
	{
		renderer.materials[1].SetFloat(key, param);
	}
}

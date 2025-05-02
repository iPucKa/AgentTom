using UnityEngine;

public class MineView : MonoBehaviour
{
	private const string TimeToDetonationKey = "_TimeToDetonate";
	private const string AgroScaleKey = "_AgroScale";

	[SerializeField] private Mine _mine;

	[SerializeField] private ParticleSystem _mineExplosionEffectPrefab;

	[SerializeField] private AudioClip _explosionSound;
	[SerializeField] private Transform _explosionSoundPrefab;

	private MeshRenderer _renderer;
	
	private AudioSource _audioSource;
	private ParticleSystem _explosionEffect;

	private void Awake()
	{
		_renderer = GetComponentInChildren<MeshRenderer>();
		_explosionSoundPrefab = Instantiate(_explosionSoundPrefab, transform.position, Quaternion.identity);
		_audioSource = _explosionSoundPrefab.GetComponent<AudioSource>();
	}

	private void Update()
	{
		if (_mine.InActivationProcess(out float elapsedTime))
		{
			SetFloatFor(_renderer, TimeToDetonationKey, elapsedTime / _mine.TimeToDetonate);
			SetFloatFor(_renderer, AgroScaleKey, 1f + elapsedTime / _mine.TimeToDetonate);

			_mine.ShowTime("", elapsedTime);
		}

		if (_mine.IsDetonated)
		{
			if (_explosionEffect == null)
			{
				MakeVisualEffects();				
			}
			
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
		float materialsCount = renderer.materials.Length;

		for (int i = 0; i < materialsCount; i++)
		{
			renderer.materials[i].SetFloat(key, param);
		}
	}

	private void MakeVisualEffects()
	{
		_audioSource.PlayOneShot(_explosionSound);
		_explosionEffect = Instantiate(_mineExplosionEffectPrefab, transform.position, Quaternion.identity);
	}
}

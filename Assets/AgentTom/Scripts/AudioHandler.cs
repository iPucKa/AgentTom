using UnityEngine;
using UnityEngine.Audio;

public class AudioHandler
{
	private const float OffVolumeValue = -80;
	private const float OnMusicVolumeValue = -20;
	private const float OnSoundsVolumeValue = 0;

	private const string MusicKey = "MusicVolume";
	private const string SoundsKey = "SoundsVolume";

	private AudioMixer _audioMixer;

	public AudioHandler(AudioMixer audioMixer)
	{
		_audioMixer = audioMixer;
	}

	public bool IsMusicOn() => IsMusicVolumeOn(MusicKey);

	public bool IsSoundsOn() => IsSoundsVolumeOn(SoundsKey);

	public void OffMusic() => OffVolume(MusicKey);

	public void OnMusic() => OnMusicVolume(MusicKey);

	public void OffSounds() => OffVolume(SoundsKey);

	public void OnSounds() => OnMSoundsVolume(SoundsKey);

	private bool IsMusicVolumeOn(string key)
		=> _audioMixer.GetFloat(key, out float volume) && Mathf.Abs(volume - OnMusicVolumeValue) <= 0.01f;

	private bool IsSoundsVolumeOn(string key)
		=> _audioMixer.GetFloat(key, out float volume) && Mathf.Abs(volume - OnSoundsVolumeValue) <= 0.01f;

	private void OnMusicVolume(string key) => _audioMixer.SetFloat(key, OnMusicVolumeValue);
	private void OnMSoundsVolume(string key) => _audioMixer.SetFloat(key, OnSoundsVolumeValue);

	private void OffVolume(string key) => _audioMixer.SetFloat(key, OffVolumeValue);
}

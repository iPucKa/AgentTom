using TMPro;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _musicText;
    [SerializeField] private TMP_Text _soundsText;

	[SerializeField] private AudioMixer _audioMixer;

    private AudioHandler _audioHandler;

	private void Awake()
	{
		_audioHandler = new AudioHandler(_audioMixer);
	}

	public void SwitchMusic()
	{
		if (_audioHandler.IsMusicOn())
		{
			_audioHandler.OffMusic();
			_musicText.text = "MUSIC OFF";
		}
		else
		{
			_audioHandler.OnMusic();
			_musicText.text = "MUSIC ON";
		}
	}

	public void SwitchSounds()
	{
		if (_audioHandler.IsSoundsOn())
		{
			_audioHandler.OffSounds();
			_soundsText.text = "SOUNDS OFF";
		}
		else
		{
			_audioHandler.OnSounds();
			_soundsText.text = "SOUNDS ON";
		}
	}
}

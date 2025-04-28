using TMPro;
using UnityEngine;

public class Timer
{
    private float _time;

	private TMP_Text _timerText;

	public float CurrentTime => _time;

	public Timer(TMP_Text timerText)
	{
		_time = 0;
		_timerText = timerText;
	}

	public void AddTime(float deltaTime) 
	{ 
		_time += Time.deltaTime;
		_timerText.text = "Время бездействия: " + _time.ToString("0.0") + "c";
	}

	public void ResetTime()
	{
		_time = 0;
		_timerText.text = $"Время бездействия: {_time} с";
	}
}

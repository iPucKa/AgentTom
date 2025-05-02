using System.Collections;
using UnityEngine;
using TMPro;

public class Timer
{
	private float _timeLimit;
	private float _elapsedTime;

	private TMP_Text _timerText;

	private MonoBehaviour _coroutineRunner;

	private Coroutine _process;

	public Timer(MonoBehaviour coroutineRunner, float timeLimit, TMP_Text timerText)
	{
		_coroutineRunner = coroutineRunner;
		_timeLimit = timeLimit;
		_timerText = timerText;
	}

	public float TimeLimit => _timeLimit;

	public bool InProcess(out float elapsedTime)
	{
		if (_process == null)
		{
			elapsedTime = 0;
			return false;
		}

		elapsedTime = _elapsedTime;
		return true;
	}

	public void StartProcess()
	{
		if (_process != null)
			_coroutineRunner.StopCoroutine(_process);

		_process = _coroutineRunner.StartCoroutine(Process());
	}

	public void StopProcess()
	{
		if (_process != null)
			_coroutineRunner.StopCoroutine(_process);

		_process = null;
	}

	private IEnumerator Process()
	{
		_elapsedTime = 0;

		while (_elapsedTime < _timeLimit)
		{
			_elapsedTime += Time.deltaTime;

			if (_elapsedTime > _timeLimit)
				_elapsedTime = _timeLimit;

			yield return null;
		}

		//_process = null;
	}

	public void ShowTime(string message) => _timerText.text = message;	

	public void ShowTime(string message,float elapsedTime) => _timerText.text = message + elapsedTime.ToString("0");	
}

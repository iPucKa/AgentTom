using UnityEngine;

public class Timer
{
    private float _time;

	public float CurrentTime => _time;

	public Timer()
	{
		_time = 0;
	}

	public void AddTime(float deltaTime) => _time += Time.deltaTime;	

    public void ResetTime() => _time = 0;
}

using UnityEngine;

public class Health
{
	private int _maxHealth;
	public Health(int value)
	{
		Value = value;
		_maxHealth = value;
	}

	public int Value { get; private set; }

	public void Reduce(int value)
	{
		if (value < 0)
		{
			Debug.LogError("Damage < 0");
			return;
		}

		Value -= value;

		if (Value < 0)
		{
			Value = 0;
		}
	}

	public void Add(int value)
	{
		if (value < 0)
		{
			Debug.LogError("Health < 0");
			return;
		}

		Value += value;

		if (Value > _maxHealth)
		{
			Value = _maxHealth;
		}
	}
}

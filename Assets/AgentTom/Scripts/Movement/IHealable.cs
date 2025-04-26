public interface IHealable
{
	int HealthValue { get; }

	void AddHealth(int health);
}

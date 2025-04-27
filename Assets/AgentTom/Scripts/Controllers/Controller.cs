public abstract class Controller
{
	private bool _isEnabled;
	private float _time;

	public bool IsWorking => _isEnabled;
	public virtual float IdleTime => _time;

	public virtual void Enable() => _isEnabled = true;

	public virtual void Disable() => _isEnabled = false;

	public void Update(float deltaTime)
	{
		if (_isEnabled == false)
			return;

		_time += deltaTime;

		UpdateLogic(deltaTime);
	}

	protected abstract void UpdateLogic(float deltaTime);
}

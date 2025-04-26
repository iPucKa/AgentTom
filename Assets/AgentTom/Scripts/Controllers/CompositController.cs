public class CompositController : Controller
{
	private Controller[] _controllers;

	public CompositController(params Controller[] controllers)
	{
		_controllers = controllers;
	}

	public override void Enable()
	{
		base.Enable();
		foreach (Controller controller in _controllers)
			controller.Enable();
	}

	public override void Disable()
	{
		base.Disable();

		foreach (Controller controller in _controllers)
			controller.Disable();
	}

	protected override void UpdateLogic(float deltaTime)
	{
		foreach (Controller controller in _controllers)
			controller.Update(deltaTime);
	}
}

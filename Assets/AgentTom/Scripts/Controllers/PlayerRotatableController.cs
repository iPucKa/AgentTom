public class PlayerRotatableController : Controller
{
	private IRotatable _rotatable;
	private IMovable _movable;

	public PlayerRotatableController(IRotatable rotatable, IMovable movable)
	{
		_rotatable = rotatable;
		_movable = movable;
	}

	protected override void UpdateLogic(float deltaTime)
	{
		_rotatable.SetRotationDirection(_movable.CurrentVelocity);
	}
}

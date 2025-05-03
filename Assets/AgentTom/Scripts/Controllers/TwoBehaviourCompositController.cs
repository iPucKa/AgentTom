public class TwoBehaviourCompositController : Controller
{
	private PlayerMouseMovableController _playerControlledState;
	private RandomAICharacterController _aIControlledState;

	private Character _character;

	private Controller _currentController;

	private Timer _timer;
	private float _timeToChangeBehavoiur;

	public TwoBehaviourCompositController(Character character, Timer timer, float timeToChangeBehavoiur, PlayerMouseMovableController playerMouseMovableController, RandomAICharacterController randomAICharacterController)
	{
		_character = character;
		_timer = timer;
		_timeToChangeBehavoiur = timeToChangeBehavoiur;

		_playerControlledState = playerMouseMovableController;
		_aIControlledState = randomAICharacterController;

		_currentController = _playerControlledState;
	}

	public override void Enable()
	{
		base.Enable();

		_currentController.Enable();
	}

	public override void Disable()
	{
		base.Disable();

		_currentController.Disable();
	}

	protected override void UpdateLogic(float deltaTime)
	{
		_currentController.Update(deltaTime);

		if (_character.IsDead == false)
			if (_playerControlledState.IsWorking)
			{
				if (_timer.InProcess(out float elapsedTime) && elapsedTime >= _timeToChangeBehavoiur)
				{
					SwitchControllers();

					_timer.ShowTime("Automation is ON");
				}				
			}
	}

	private void SwitchControllers()
	{
		_timer.StopProcess();

		_playerControlledState.Disable();
		_aIControlledState.Enable();

		_currentController = _aIControlledState;
	}
}

using UnityEngine;
using UnityEngine.AI;

public class AgentDirectionalMover
{
	private NavMeshAgent _agent;

	private Vector3 _targetPosition;

	public AgentDirectionalMover(NavMeshAgent agent, float movementSpeed)
	{
		_agent = agent;

		_agent.speed = movementSpeed;
		_agent.acceleration = 999;
		_targetPosition = _agent.transform.position;
	}

	public Vector3 CurrentVelocity => _agent.desiredVelocity;

	public void SetTargetPosition(Vector3 position) => _targetPosition = position;

	public void Update(float deltaTime)
	{		
		_agent.SetDestination(_targetPosition);
	}
}

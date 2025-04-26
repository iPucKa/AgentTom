using UnityEngine;

public interface IMovable : ITransformPosition
{
	Vector3 CurrentVelocity { get; }

	void SetMovePosition(Vector3 inputDirection);
}

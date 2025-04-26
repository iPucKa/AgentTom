using UnityEngine;

public interface IRotatable : ITransformPosition
{
	Quaternion CurrentRotation { get; }

	void SetRotationDirection(Vector3 inputDirection);
}

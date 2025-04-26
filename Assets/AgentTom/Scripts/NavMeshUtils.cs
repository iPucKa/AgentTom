using UnityEngine;
using UnityEngine.AI;

public class NavMeshUtils
{
	public static bool TryGetPath(Vector3 sourcePosition, Vector3 targetPosition, NavMeshQueryFilter queryFilter, NavMeshPath pathToTarget)
	{
		if (NavMesh.CalculatePath(sourcePosition, targetPosition, queryFilter, pathToTarget) && pathToTarget.status != NavMeshPathStatus.PathInvalid)
			return true;

		return false;
	}
}

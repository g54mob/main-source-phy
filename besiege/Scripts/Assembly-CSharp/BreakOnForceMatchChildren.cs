using UnityEngine;

[AddComponentMenu("Destruction/Break On Force Match Children")]
public class BreakOnForceMatchChildren : BreakOnForce, IExplosionEffect
{
	public Transform[] childrenToMatchFrom;

	protected override void SetParent(Transform breakObj)
	{
		breakObj.parent = ((!usePhysicsGoalAsParent) ? BreakParent : ReferenceMaster.physicsGoalInstance);
		breakObj.localScale = base.transform.localScale;
		for (int i = 0; i < childrenToMatchFrom.Length; i++)
		{
			Transform transform = breakObj.FindChild(childrenToMatchFrom[i].name);
			transform.rotation = childrenToMatchFrom[i].rotation;
			transform.position = childrenToMatchFrom[i].position;
		}
	}
}

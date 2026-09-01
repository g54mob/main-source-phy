using UnityEngine;

[CreateAssetMenu(fileName = "StandingData", menuName = "PhysicsAnimation/StandingData", order = 0)]
public class StandingData : ScriptableObject
{
	public int defaultSate;

	public float baseOffset;

	public float standingForce;

	public StandingInstance[] standingInstances;
}

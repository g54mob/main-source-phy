using UnityEngine;

[CreateAssetMenu(fileName = "AnimationForceData", menuName = "PhysicsAnimation/AnimationForceData", order = 0)]
public class AnimationForceData : ScriptableObject
{
	public AnimationForceInstance[] animations;
}

using UnityEngine;

[CreateAssetMenu(fileName = "AnimationJointData", menuName = "PhysicsAnimation/AnimationJointData", order = 0)]
public class AnimationJointData : ScriptableObject
{
	public AnimationJointInstance[] joints;
}

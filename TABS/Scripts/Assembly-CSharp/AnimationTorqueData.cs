using UnityEngine;

[CreateAssetMenu(fileName = "AnimationTorqueData", menuName = "PhysicsAnimation/AnimationTorqueData", order = 0)]
public class AnimationTorqueData : ScriptableObject
{
	public AnimationTorqueInstance[] animations;
}

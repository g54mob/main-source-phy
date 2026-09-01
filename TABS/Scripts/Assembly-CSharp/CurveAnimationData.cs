using UnityEngine;

[CreateAssetMenu(fileName = "CurveAnimatonData", menuName = "PhysicsAnimation/CurveAnimatonData", order = 0)]
public class CurveAnimationData : ScriptableObject
{
	public CurveAnimationInstance[] animations;

	public CurveAnimationInstance[] forceAnimations;
}

using UnityEngine;

[CreateAssetMenu(fileName = "StepData", menuName = "PhysicsAnimation/StepData", order = 0)]
public class StepData : ScriptableObject
{
	public int defaultState;

	public StepInstance[] steps;
}

using UnityEngine;

[CreateAssetMenu(fileName = "movementData", menuName = "PhysicsAnimation/MovementData", order = 0)]
public class MovementHandlerData : ScriptableObject
{
	public MovementInstance[] moves;
}

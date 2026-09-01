using UnityEngine;

public class SnapToBounds : MonoBehaviour
{
	public enum axisToSnap
	{
		Up = 0,
		Right = 1,
		Forward = 2
	}

	public axisToSnap axisOfMovement;

	public AddPiece AddPieceCode;

	private void Update()
	{
		Machine machine = Machine.Active();
		if (axisOfMovement == axisToSnap.Up && (bool)machine)
		{
			Bounds bounds = machine.GetBounds();
			base.transform.position = new Vector3(bounds.center.x, bounds.max.y, bounds.center.z);
		}
	}
}

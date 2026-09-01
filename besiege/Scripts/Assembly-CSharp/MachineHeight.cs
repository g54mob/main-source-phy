using UnityEngine;

[AddComponentMenu("UI/Machine Height")]
public class MachineHeight : ClickBehaviour
{
	public enum moveAxisState
	{
		Up = 0,
		Right = 1,
		Forward = 2,
		FULL = 3
	}

	public float speed = 1f;

	public AddPiece AddPieceCode;

	public bool clicked;

	public Renderer bgRend;

	public Material clickedMaterial;

	public float maxSpeed = 0.2f;

	public moveAxisState axisOfMovement;

	private MachineObjectTracker machineTracker;

	private AudioSource audioSource;
}

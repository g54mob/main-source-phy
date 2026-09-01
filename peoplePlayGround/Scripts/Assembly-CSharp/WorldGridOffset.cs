using UnityEngine;

public class WorldGridOffset : MonoBehaviour
{
	public Vector2 GridOffset = new Vector2(-0.24000001f, 0.025f);

	public static Vector3 Offset;

	private void Start()
	{
		Offset = GridOffset;
	}
}

using System;
using UnityEngine;

[AddComponentMenu("Levels/World Bounds Manager")]
public class WorldBoundsManager : MonoBehaviour
{
	[Serializable]
	public class Border
	{
		public enum Axis
		{
			X = 0,
			Y = 1,
			Z = 2
		}

		public Transform transform;

		public BoxCollider collider;

		public MeshRenderer renderer;

		public Axis axis;

		public bool positive = true;
	}

	public Vector3 levelSize = new Vector3(1000f, 1000f, 1000f);

	public Vector3 sandBoxSize = new Vector3(2000f, 2000f, 2000f);

	public Vector3 multiplayerSize = new Vector3(2000f, 2000f, 2000f);

	public float yOffset = -250f;

	public Border[] borders;

	private void SetBorder(Border border, Vector3 size)
	{
		Vector3 position = border.transform.position;
		position.y = yOffset;
		position.y += size.y / 2f;
		float num = 20f;
		Vector2 vector = border.renderer.transform.localScale;
		switch (border.axis)
		{
		case Border.Axis.X:
			position.x = ((!border.positive) ? (0f - size.x) : size.x) / 2f;
			border.collider.size = new Vector3(num, size.y, size.z);
			border.renderer.transform.localScale = new Vector3(size.z, size.y, 1f);
			break;
		case Border.Axis.Y:
			position.y += size.y / 2f;
			border.collider.size = new Vector3(size.x, num, size.z);
			border.renderer.transform.localScale = new Vector3(size.x, size.z, 1f);
			break;
		case Border.Axis.Z:
			position.z = ((!border.positive) ? (0f - size.z) : size.z) / 2f;
			border.collider.size = new Vector3(size.x, size.y, num);
			border.renderer.transform.localScale = new Vector3(size.x, size.y, 1f);
			break;
		}
		border.transform.position = position;
		float num2 = border.renderer.transform.localScale.x / vector.x;
		float num3 = border.renderer.transform.localScale.y / vector.y;
		Debug.Log(num2 + " : " + num3);
	}

	public void SetAllBorders(bool isLevel, bool isMP)
	{
		for (int i = 0; i < borders.Length; i++)
		{
			SetBorder(borders[i], (!isLevel) ? ((!isMP) ? sandBoxSize : multiplayerSize) : levelSize);
		}
	}
}

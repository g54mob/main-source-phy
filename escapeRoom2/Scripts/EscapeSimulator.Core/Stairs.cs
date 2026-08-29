using UnityEngine;

public class Stairs : MonoBehaviour
{
	public enum Direction
	{
		Right = 0,
		Left = 1,
		Straight = 2
	}

	public Collider slope;

	public Transform stairs;

	public Direction direction;

	private float initialScaleX;

	private bool didInit;

	public void init()
	{
		if (!didInit)
		{
			didInit = true;
			initialScaleX = stairs.localScale.x;
		}
	}

	public void syncVisuals()
	{
		init();
		if (direction != Direction.Straight)
		{
			int num = ((direction != Direction.Left) ? 1 : (-1));
			float x = initialScaleX * (float)num;
			stairs.localScale = new Vector3(x, stairs.localScale.y, stairs.localScale.z);
		}
	}
}

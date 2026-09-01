using UnityEngine;

public class TextureUtil
{
	public static int ToPowersOfTwo(int x)
	{
		int num = 0;
		while (true)
		{
			int num2 = ((num == 0) ? 1 : (num * 2));
			if (num2 <= num)
			{
				return num;
			}
			if (num2 > x)
			{
				break;
			}
			num = num2;
		}
		return num;
	}

	public static Vector2Int ToPowersOfTwo(Vector2Int size)
	{
		return new Vector2Int(ToPowersOfTwo(size.x), ToPowersOfTwo(size.y));
	}
}

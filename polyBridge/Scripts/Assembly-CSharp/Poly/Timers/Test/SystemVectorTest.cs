using System.Numerics;
using UnityEngine;

namespace Poly.Timers.Test
{
	public class SystemVectorTest : MonoBehaviour
	{
		public int count = 10000;

		private void Update()
		{
			UnityEngine.Vector2[] array = new UnityEngine.Vector2[count];
			Vec2[] array2 = new Vec2[count];
			System.Numerics.Vector2[] array3 = new System.Numerics.Vector2[count];
			array[0] = UnityEngine.Vector2.zero;
			array[1] = UnityEngine.Vector2.one;
			for (int i = 2; i < count; i++)
			{
				array[i] = array[i - 1] + array[i - 2];
			}
			array2[0] = Vec2.zero;
			array2[1] = Vec2.one;
			_ = Vec2.zero;
			for (int j = 2; j < count; j++)
			{
				array2[j].setAdd(ref array2[j - 1], ref array2[j - 2]);
			}
			array3[0] = System.Numerics.Vector2.Zero;
			array3[1] = System.Numerics.Vector2.One;
			for (int k = 2; k < count; k++)
			{
				array3[k] = System.Numerics.Vector2.Add(array3[k - 1], array3[k - 2]);
			}
		}
	}
}

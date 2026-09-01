using System.Collections.Generic;
using Poly.Collide;
using Poly.Math;

namespace Poly.Physics.Gameplay
{
	public class Trigger
	{
		public Transform2 t2;

		public List<PolygonShape> shapes = new List<PolygonShape>();

		public void Destroy()
		{
			for (int i = 0; i < shapes.Count; i++)
			{
				shapes[i] = null;
			}
			shapes = null;
		}
	}
}

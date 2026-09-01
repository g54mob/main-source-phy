using System.Diagnostics;

namespace Poly.Physics
{
	[DebuggerDisplay("{index}")]
	public struct ShapeHandleIndex
	{
		public short index;

		public bool isValid => index >= 0;

		public ref ShapeHandle Get()
		{
			return ref World.shapeHandleArray[index];
		}

		public static implicit operator ShapeHandleIndex(short index)
		{
			return new ShapeHandleIndex
			{
				index = index
			};
		}

		public static implicit operator short(ShapeHandleIndex index)
		{
			return index.index;
		}
	}
}

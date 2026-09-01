using System;
using Poly.Collide;

namespace Poly.Physics
{
	[Serializable]
	public abstract class WorldObjectImpl
	{
		[NonSerialized]
		public World world;

		internal short worldIdx = -1;

		public object userData;

		[NonSerialized]
		public ShapeHandleIndex shapeHandleIndex = (short)(-1);

		[NonSerialized]
		internal Shape shape;

		[NonSerialized]
		internal ShapeHandle? shapeHandle;

		[NonSerialized]
		public bool isEnabled = true;

		public bool isAddedToWorld => Exists(world);

		public abstract bool isDynamic { get; }

		public virtual void SetWorldAndIndex(World world, int index)
		{
			this.world = world;
			worldIdx = (short)index;
		}

		public virtual void UpdateShapeHandleIndex(short oldIndex, short newIndex)
		{
			shapeHandleIndex = newIndex;
		}

		public void SetShape(ref ShapeHandle newShapeHandle)
		{
			newShapeHandle.entityHandle = this;
			shape = newShapeHandle.shape;
			shapeHandle = newShapeHandle;
		}

		public void ReleaseShape()
		{
			if (shapeHandle.HasValue)
			{
				shapeHandle.Value.Dispose();
				shape = null;
				shapeHandle = null;
			}
		}

		public static implicit operator bool(WorldObjectImpl obj)
		{
			return obj != null;
		}

		protected static bool Exists(object obj)
		{
			return obj != null;
		}
	}
}

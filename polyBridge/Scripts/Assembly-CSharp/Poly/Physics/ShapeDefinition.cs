using Poly.Collide;
using UnityEngine;

namespace Poly.Physics
{
	public class ShapeDefinition
	{
		public bool enableCollision;

		public Shape.Type type;

		public float radius;

		public Vec2[] vertices;

		public PhysicsMaterial2D physicsMaterial;

		public short collisionGroup;

		public Layer layer;

		public RecollisionType recollisionType;

		public float tmpSurfaceVelocity;

		public float lengthX;

		public static implicit operator ShapeDefinition(NodeShapeDefinition nsd)
		{
			return new ShapeDefinition
			{
				enableCollision = nsd.enableCollision,
				type = Shape.Type.Circle,
				radius = nsd.collisionRadius,
				vertices = null,
				physicsMaterial = nsd.physicsMaterial,
				collisionGroup = (short)nsd.collisionGroup,
				layer = nsd.layer,
				recollisionType = RecollisionType.DistanceOnly,
				tmpSurfaceVelocity = nsd.surfaceVelocity
			};
		}
	}
}

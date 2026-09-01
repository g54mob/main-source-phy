using System;
using Poly.Math;
using Poly.Physics;
using UnityEngine;

namespace Poly.Collide
{
	[Serializable]
	public class Shape
	{
		public enum Type : sbyte
		{
			Invalid = -1,
			Circle = 0,
			Segment = 1,
			Polygon = 2,
			NumCollisionValues = 3,
			AabbShape = 4
		}

		public Type type;

		public float radius = 0.5f;

		public float friction;

		public float bounciness;

		public float tmpSurfaceVelocity;

		public virtual Aabb GetAabb(ref Transform2 t2, float padding)
		{
			return default(Aabb);
		}

		public void SetPhysicsProperties(PhysicsMaterial2D physicsMaterial)
		{
			if ((bool)physicsMaterial)
			{
				friction = physicsMaterial.friction;
				bounciness = physicsMaterial.bounciness;
			}
			else
			{
				friction = 0.4f;
				bounciness = 0f;
			}
		}

		public static implicit operator bool(Shape s)
		{
			return s != null;
		}

		public static ShapeHandle CreateShapeAndHandle(ShapeDefinition define)
		{
			Shape shape = null;
			switch (define.type)
			{
			case Type.Circle:
				shape = new Circle(define.radius);
				break;
			case Type.Segment:
				shape = new Segment(define.lengthX, define.radius);
				break;
			}
			shape.tmpSurfaceVelocity = define.tmpSurfaceVelocity;
			shape.radius = define.radius;
			shape.SetPhysicsProperties(define.physicsMaterial);
			ShapeHandle result = ShapeHandle.Create();
			result.shape = shape;
			result.collisionGroup = define.collisionGroup;
			result.layer = define.layer;
			result.recollisionType = define.recollisionType;
			return result;
		}

		public static void ReuseShapeAndHandle(ref ShapeHandle handle, ShapeDefinition define)
		{
			Shape shape = handle.shape;
			if (define.type == Type.Segment)
			{
				((Segment)shape).halfLengthX = 0.5f * define.lengthX;
			}
			shape.tmpSurfaceVelocity = define.tmpSurfaceVelocity;
			shape.radius = define.radius;
			shape.SetPhysicsProperties(define.physicsMaterial);
			handle.t2 = Transform2.identity;
			handle.nodeIdx = short.MinValue;
			handle.motionIdx = short.MinValue;
			handle.collisionGroup = define.collisionGroup;
			handle.layer = define.layer;
			handle.recollisionType = define.recollisionType;
		}
	}
}

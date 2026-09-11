using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using Unity.Mathematics;
using UnityEngine;

namespace Vertx.Debugging
{
	public static class Shape
	{
		public readonly struct Point2D : IDrawable
		{
			public readonly float3 Position;

			public readonly float Scale;

			public Point2D(float2 point, float z = 0f, float scale = 0.3f)
			{
				Scale = scale;
				Position = new float3(point.x, point.y, z);
			}

			public Point2D(Vector2 point, float z = 0f, float scale = 0.3f)
				: this((float2)point, z, scale)
			{
			}

			public Point2D(float3 position, float scale = 0.3f)
			{
				Scale = scale;
				Position = position;
			}

			public Point2D(Vector3 point, float scale = 0.3f)
				: this((float3)point, scale)
			{
			}
		}

		public readonly struct Ray2D : IDrawable
		{
			public readonly float3 Origin;

			public readonly float2 Direction;

			public Ray2D(float3 origin, float2 direction)
			{
				Origin = origin;
				Direction = direction;
			}

			public Ray2D(Vector3 origin, Vector2 direction)
				: this((float3)origin, (float2)direction)
			{
			}

			public Ray2D(float2 origin, float2 direction, float z = 0f)
				: this(new float3(origin.x, origin.y, z), direction)
			{
			}

			public Ray2D(Vector2 origin, Vector2 direction, float z = 0f)
				: this((float2)origin, (float2)direction, z)
			{
			}

			public Ray2D(float3 origin, float angleDegrees)
				: this(origin, GetDirectionFromAngle(Angle.FromDegrees(angleDegrees)))
			{
			}

			public Ray2D(Vector3 origin, float angleDegrees)
				: this((float3)origin, angleDegrees)
			{
			}

			public Ray2D(float2 origin, float angleDegrees, float z = 0f)
				: this(origin, GetDirectionFromAngle(Angle.FromDegrees(angleDegrees)), z)
			{
			}

			public Ray2D(Vector2 origin, float angleDegrees, float z = 0f)
				: this((float2)origin, angleDegrees, z)
			{
			}
		}

		public readonly struct Arrow2D : IDrawable
		{
			public readonly float3 Origin;

			public readonly float2 Direction;

			public readonly float ArrowheadScale;

			public Arrow2D(float3 origin, float2 direction, float arrowheadScale = 1f)
			{
				Origin = origin;
				Direction = direction;
				ArrowheadScale = arrowheadScale;
			}

			public Arrow2D(Vector3 origin, Vector2 direction, float arrowheadScale = 1f)
				: this((float3)origin, (float2)direction, arrowheadScale)
			{
			}

			public Arrow2D(float2 origin, float2 direction, float z = 0f, float arrowheadScale = 1f)
				: this(new float3(origin.x, origin.y, z), direction)
			{
			}

			public Arrow2D(Vector2 origin, Vector2 direction, float z = 0f, float arrowheadScale = 1f)
				: this((float2)origin, (float2)direction, z, arrowheadScale)
			{
			}

			public Arrow2D(float3 origin, float angleDegrees, float arrowheadScale = 1f)
				: this(origin, GetDirectionFromAngle(Angle.FromDegrees(angleDegrees)), arrowheadScale)
			{
			}

			public Arrow2D(Vector3 origin, float angleDegrees, float arrowheadScale = 1f)
				: this((float3)origin, angleDegrees, arrowheadScale)
			{
			}

			public Arrow2D(float2 origin, float angleDegrees, float z = 0f, float arrowheadScale = 1f)
				: this(origin, GetDirectionFromAngle(Angle.FromDegrees(angleDegrees)), z, arrowheadScale)
			{
			}

			public Arrow2D(Vector2 origin, float angleDegrees, float z = 0f, float arrowheadScale = 1f)
				: this((float2)origin, angleDegrees, z, arrowheadScale)
			{
			}
		}

		public readonly struct ArrowStrip2D : IDrawable
		{
			public readonly IEnumerable<float2> Points;

			public readonly float Z;

			public readonly float ArrowheadScale;

			public ArrowStrip2D(IEnumerable<float2> points, float z = 0f, float arrowheadScale = 1f)
			{
				Points = points;
				Z = z;
				ArrowheadScale = arrowheadScale;
			}

			public ArrowStrip2D(IEnumerable<Vector2> points, float z = 0f, float arrowheadScale = 1f)
				: this(points.Select((Func<Vector2, float2>)((Vector2 v) => v)), z, arrowheadScale)
			{
			}
		}

		public readonly struct Axis2D : IDrawable
		{
			public readonly float3 Position;

			public readonly float AngleDegrees;

			public readonly float Scale;

			public readonly bool ShowArrowHeads;

			public Axis2D(float2 origin, float angleDegrees, float z = 0f, bool showArrowHeads = true)
			{
				Position = new float3(origin.x, origin.y, z);
				AngleDegrees = angleDegrees;
				ShowArrowHeads = showArrowHeads;
				Scale = 1f;
			}

			public Axis2D(Vector2 origin, float angleDegrees, float z = 0f, bool showArrowHeads = true)
				: this((float2)origin, angleDegrees, z, showArrowHeads)
			{
			}

			public Axis2D(float2 origin, float angleDegrees, float z = 0f, float scale = 1f, bool showArrowHeads = true)
			{
				Position = new float3(origin.x, origin.y, z);
				AngleDegrees = angleDegrees;
				ShowArrowHeads = showArrowHeads;
				Scale = scale;
			}

			public Axis2D(Vector2 origin, float angleDegrees, float z = 0f, float scale = 1f, bool showArrowHeads = true)
				: this((float2)origin, angleDegrees, z, scale, showArrowHeads)
			{
			}
		}

		public readonly struct Circle2D : IDrawable
		{
			private readonly Circle _circle;

			public float4x4 Matrix => _circle.Matrix;

			public Circle2D(float4x4 matrix)
			{
				_circle = new Circle(matrix);
			}

			public Circle2D(Matrix4x4 matrix)
				: this((float4x4)matrix)
			{
			}

			public Circle2D(float2 origin, float radius, float z = 0f)
			{
				_circle = new Circle(new float3(origin.x, origin.y, z), quaternion.identity, radius);
			}

			public Circle2D(Vector2 origin, float radius, float z = 0f)
				: this((float2)origin, radius, z)
			{
			}

			internal Circle2D(float3 origin, float radius)
			{
				_circle = new Circle(origin, quaternion.identity, radius);
			}

			public Circle2D(CircleCollider2D circleCollider)
			{
				Transform transform = circleCollider.transform;
				float3 float5 = transform.lossyScale;
				float3 origin = circleCollider.transform.TransformPoint(circleCollider.offset);
				origin.z = transform.position.z;
				_circle = new Circle(origin, quaternion.identity, circleCollider.radius * math.max(float5.x, float5.y));
			}
		}

		public readonly struct Arc2D : IDrawable
		{
			public readonly Arc Arc;

			public Arc2D(float2 origin, float rotationDegrees, float radius, Angle angle, float z = 0f)
			{
				Arc = new Arc(new float3(origin.x, origin.y, z), quaternion.AxisAngle(math.forward(), rotationDegrees * (MathF.PI / 180f)), radius, angle);
			}

			public Arc2D(Vector2 origin, float rotationDegrees, float radius, Angle angle, float z = 0f)
				: this((float2)origin, rotationDegrees, radius, angle, z)
			{
			}

			public Arc2D(float2 origin, float rotationDegrees, float radius, float z = 0f)
				: this(origin, rotationDegrees, radius, Angle.FromTurns(1f), z)
			{
			}

			public Arc2D(Vector2 origin, float rotationDegrees, float radius, float z = 0f)
				: this((float2)origin, rotationDegrees, radius, z)
			{
			}

			internal Arc2D(float4x4 matrix)
			{
				Arc = new Arc(matrix);
			}
		}

		public readonly struct Box2D : IDrawable
		{
			[Flags]
			internal enum Point
			{
				Origin = 0,
				Top = 1,
				Right = 2,
				Bottom = 4,
				Left = 8,
				TopLeft = 9,
				TopRight = 3,
				BottomRight = 6,
				BottomLeft = 0xC
			}

			public readonly float4x4 Matrix;

			internal Box2D(float4x4 matrix)
			{
				Matrix = matrix;
			}

			public Box2D(float2 origin, float2 size, float angleDegrees = 0f, float z = 0f)
			{
				Matrix = float4x4.TRS(new float3(origin.x, origin.y, z), quaternion.AxisAngle(math.forward(), angleDegrees * (MathF.PI / 180f)), size.xy0());
			}

			public Box2D(Vector2 origin, Vector2 size, float angleDegrees = 0f, float z = 0f)
				: this((float2)origin, (float2)size, angleDegrees, z)
			{
			}

			public Box2D(float3 origin, float2 size, float angleDegrees = 0f)
			{
				Matrix = float4x4.TRS(origin, quaternion.AxisAngle(math.forward(), angleDegrees * (MathF.PI / 180f)), size.xy0());
			}

			public Box2D(Vector3 origin, Vector2 size, float angleDegrees = 0f)
				: this((float3)origin, (float2)size, angleDegrees)
			{
			}

			public Box2D(float3 origin, quaternion rotation, float2 size)
			{
				Matrix = float4x4.TRS(origin, rotation, size.xy0());
			}

			public Box2D(Vector3 origin, Quaternion rotation, Vector2 size)
				: this((float3)origin, (quaternion)rotation, (float2)size)
			{
			}

			public Box2D(BoxCollider2D boxCollider)
			{
				if (Mathf.Approximately(boxCollider.transform.lossyScale.sqrMagnitude, 0f))
				{
					Matrix = float4x4.Scale(new float3(0f, 0f, 0f));
					return;
				}
				float4x4 float4x5 = boxCollider.transform.localToWorldMatrix;
				float4x5.SetRow(0, Vector4.Scale(float4x5.GetRow(0), new Vector4(1f, 1f, 0f, 1f)));
				float4x5.SetRow(1, Vector4.Scale(float4x5.GetRow(1), new Vector4(1f, 1f, 0f, 1f)));
				float4x5.SetRow(2, new Vector4(0f, 0f, 1f, boxCollider.transform.position.z));
				Matrix = float4x5 * float4x4.TRS(boxCollider.offset.xy0(), quaternion.identity, ColliderLocalSize(boxCollider).xy0());
			}

			private static float2 ColliderLocalSize(BoxCollider2D boxCollider)
			{
				if (!boxCollider.autoTiling || !boxCollider.TryGetComponent<SpriteRenderer>(out var component) || component.drawMode == SpriteDrawMode.Simple)
				{
					return boxCollider.size;
				}
				return component.size;
			}

			internal static float3 GetPoint(float4x4 matrix, Point point)
			{
				float3 point2 = new float3(((point & Point.Left) != Point.Origin) ? (-0.5f) : ((0 + (point & Point.Right) != Point.Origin) ? 0.5f : 0f), ((point & Point.Bottom) != Point.Origin) ? (-0.5f) : ((0 + (point & Point.Top) != Point.Origin) ? 0.5f : 0f), 0f);
				return matrix.MultiplyPoint3x4(point2);
			}
		}

		public readonly struct Box2DWithEdgeRadius : IDrawable
		{
			public readonly Box2D Box;

			public readonly float EdgeRadius;

			public Box2DWithEdgeRadius(Box2D box, float edgeRadius)
			{
				Box = box;
				EdgeRadius = edgeRadius;
			}

			public Box2DWithEdgeRadius(BoxCollider2D collider)
				: this(new Box2D(collider), collider.edgeRadius)
			{
			}
		}

		public readonly struct Area2D : IDrawable
		{
			public readonly float3 PointA;

			public readonly float3 PointB;

			public Area2D(float2 pointA, float2 pointB, float z = 0f)
			{
				PointA = new float3(pointA.x, pointA.y, z);
				PointB = new float3(pointB.x, pointB.y, z);
			}

			public Area2D(Vector2 pointA, Vector2 pointB, float z = 0f)
				: this((float2)pointA, (float2)pointB, z)
			{
			}

			public Area2D(float3 pointA, float3 pointB)
			{
				PointA = pointA;
				PointB = pointB;
				PointB.z = PointA.z;
			}

			public Area2D(Vector3 pointA, Vector3 pointB)
				: this((float3)pointA, (float3)pointB)
			{
			}

			public Area2D(Bounds bounds2D, float z = 0f)
			{
				PointA = bounds2D.min;
				PointA.z = z;
				PointB = bounds2D.max;
				PointB.z = z;
			}

			public Area2D(Rect rect, float z = 0f)
			{
				float2 float5 = rect.min;
				float2 float6 = rect.max;
				PointA = new float3(float5.x, float5.y, z);
				PointB = new float3(float6.x, float6.y, z);
			}
		}

		public readonly struct Capsule2D : IDrawable
		{
			public enum Direction
			{
				Vertical = 0,
				Horizontal = 1
			}

			internal readonly float3 _pointA;

			internal readonly float3 _pointB;

			internal readonly float _radius;

			internal readonly float3 _verticalDirection;

			internal readonly float3 _scaledLeft;

			public float3 PointA => _pointA;

			public float3 PointB => _pointB;

			public float Radius => _radius;

			public Capsule2D(float2 point, float2 size, CapsuleDirection2D capsuleDirection, float angleDegrees = 0f)
				: this(point, size, capsuleDirection, angleDegrees, 0f)
			{
			}

			public Capsule2D(Vector2 point, Vector2 size, CapsuleDirection2D capsuleDirection, float angleDegrees = 0f)
				: this((float2)point, (float2)size, capsuleDirection, angleDegrees)
			{
			}

			public Capsule2D(float2 point, float2 size, CapsuleDirection2D capsuleDirection, float angleDegrees, float z)
				: this(point, size, (Direction)capsuleDirection, angleDegrees, z)
			{
			}

			public Capsule2D(Vector2 point, Vector2 size, CapsuleDirection2D capsuleDirection, float angleDegrees, float z)
				: this((float2)point, (float2)size, capsuleDirection, angleDegrees, z)
			{
			}

			public Capsule2D(CapsuleCollider2D collider)
			{
				float2 float5 = collider.size * 0.5f;
				Transform transform = collider.transform;
				float3 float6 = transform.TransformPoint(collider.offset);
				float2 x = transform.TransformVector((collider.direction == CapsuleDirection2D.Vertical) ? new float3(float5.x, 0f, 0f) : new float3(0f, float5.y, 0f)).xy();
				_radius = math.length(x);
				float2 vector = transform.TransformVector((collider.direction == CapsuleDirection2D.Vertical) ? new float3(0f, float5.y, 0f) : new float3(float5.x, 0f, 0f)).xy();
				vector.EnsureNormalized(out var length);
				vector *= math.max(length - _radius, 0f);
				_pointA = new float3(float6.x + vector.x, float6.y + vector.y, float6.z);
				_pointB = new float3(float6.x - vector.x, float6.y - vector.y, float6.z);
				_verticalDirection = math.normalize(_pointA - _pointB);
				_scaledLeft = PerpendicularCounterClockwise(_verticalDirection) * _radius;
			}

			public Capsule2D(float2 point, float2 size, Direction capsuleDirection, float angleDegrees = 0f)
				: this(point, size, capsuleDirection, angleDegrees, 0f)
			{
			}

			public Capsule2D(Vector2 point, Vector2 size, Direction capsuleDirection, float angleDegrees = 0f)
				: this((float2)point, (float2)size, capsuleDirection, angleDegrees)
			{
			}

			internal Capsule2D(float3 pointA, float3 pointB, float radius, float3 verticalDirection, float3 scaledLeft)
			{
				_pointA = pointA;
				_pointB = pointB;
				_radius = radius;
				_verticalDirection = verticalDirection;
				_scaledLeft = scaledLeft;
			}

			public Capsule2D(float2 point, float2 size, Direction capsuleDirection, float angleDegrees, float z)
			{
				if (capsuleDirection == Direction.Horizontal)
				{
					float y = size.y;
					size.y = size.x;
					size.x = y;
					angleDegrees += 180f;
				}
				_radius = size.x * 0.5f;
				float y2 = math.max(0f, size.y - size.x) * 0.5f;
				GetRotationCoefficients(Angle.FromDegrees(angleDegrees), out var s, out var c);
				_verticalDirection = RotateUsingCoefficients(math.up(), s, c);
				float2 float5 = RotateUsingCoefficients(new float2(0f, y2), s, c);
				_pointA = GetFloat(point + float5);
				_pointB = GetFloat(point - float5);
				_scaledLeft = (new float2(c, s) * _radius).xy0();
				float3 GetFloat(float2 v2)
				{
					return new float3(v2.x, v2.y, z);
				}
			}

			public Capsule2D(Vector2 point, Vector2 size, Direction capsuleDirection, float angleDegrees, float z)
				: this((float2)point, (float2)size, capsuleDirection, angleDegrees, z)
			{
			}

			internal Capsule2D(float3 pointA, float3 pointB, float radius)
			{
				_verticalDirection = pointA - pointB;
				_verticalDirection.EnsureNormalized();
				_pointA = pointA;
				_pointB = pointB;
				_radius = radius;
				_scaledLeft = PerpendicularCounterClockwise(_verticalDirection) * _radius;
			}
		}

		public readonly struct Spiral2D : IDrawable
		{
			public readonly float3 Origin;

			public readonly float Radius;

			public readonly Angle Angle;

			public readonly float Revolutions;

			public Spiral2D(float2 origin, float radius, Angle angle = default(Angle), float revolutions = 3f, float z = 0f)
			{
				Origin = new float3(origin.x, origin.y, z);
				Radius = radius;
				Angle = angle;
				Revolutions = revolutions;
			}

			public Spiral2D(Vector2 origin, float radius, Angle angle = default(Angle), float revolutions = 3f, float z = 0f)
				: this((float2)origin, radius, angle, revolutions, z)
			{
			}
		}

		public readonly struct Raycast2D : IDrawableCast, IDrawable
		{
			public readonly float2 Origin;

			public readonly float2 Direction;

			public readonly float Distance;

			public readonly float MinDepth;

			public readonly float MaxDepth;

			public readonly RaycastHit2D? Result;

			public Raycast2D(float2 origin, float2 direction, RaycastHit2D? result, float distance = float.PositiveInfinity, float minDepth = 0f, float maxDepth = 0f)
			{
				Origin = origin;
				direction = math.normalizesafe(direction);
				Direction = direction;
				Distance = distance;
				Result = result;
				MinDepth = minDepth;
				MaxDepth = math.max(minDepth, maxDepth);
			}
		}

		public readonly struct Linecast2D : IDrawableCast, IDrawable
		{
			public readonly float2 PointA;

			public readonly float2 PointB;

			public readonly float MinDepth;

			public readonly float MaxDepth;

			public readonly RaycastHit2D? Result;

			public Linecast2D(float2 pointA, float2 pointB, RaycastHit2D? result, float minDepth = 0f, float maxDepth = 0f)
			{
				PointA = pointA;
				PointB = pointB;
				Result = result;
				MinDepth = minDepth;
				MaxDepth = math.max(maxDepth, minDepth);
			}
		}

		public readonly struct RaycastAll2D : IDrawableCast, IDrawable
		{
			public readonly Raycast2D Raycast;

			public readonly IList<RaycastHit2D> Results;

			public readonly int ResultCount;

			public RaycastAll2D(float2 origin, float2 direction, IList<RaycastHit2D> results, int resultCount, float distance = float.PositiveInfinity, float minDepth = 0f, float maxDepth = 0f)
			{
				Raycast = new Raycast2D(origin, direction, null, distance, minDepth, maxDepth);
				Results = results;
				ResultCount = resultCount;
			}

			public RaycastAll2D(float2 origin, float2 direction, IList<RaycastHit2D> results, float distance = float.PositiveInfinity, float minDepth = 0f, float maxDepth = 0f)
				: this(origin, direction, results, results.Count, distance, minDepth, maxDepth)
			{
			}
		}

		public readonly struct LinecastAll2D : IDrawableCast, IDrawable
		{
			public readonly Linecast2D Linecast;

			public readonly IList<RaycastHit2D> Results;

			public readonly int ResultCount;

			public LinecastAll2D(float2 pointA, float2 pointB, IList<RaycastHit2D> results, int resultCount, float minDepth = 0f, float maxDepth = 0f)
			{
				Linecast = new Linecast2D(pointA, pointB, null, minDepth, maxDepth);
				Results = results;
				ResultCount = resultCount;
			}

			public LinecastAll2D(float2 pointA, float2 pointB, IList<RaycastHit2D> results, float minDepth = 0f, float maxDepth = 0f)
				: this(pointA, pointB, results, results.Count, minDepth, maxDepth)
			{
			}
		}

		public readonly struct CircleCast : IDrawableCast, IDrawable
		{
			public readonly float2 Origin;

			public readonly float Radius;

			public readonly float2 Direction;

			public readonly float MaxDistance;

			public readonly float MinDepth;

			public readonly float MaxDepth;

			public readonly RaycastHit2D? Hit;

			public CircleCast(float2 origin, float radius, float2 direction, RaycastHit2D? hit, float maxDistance = float.PositiveInfinity, float minDepth = 0f, float maxDepth = 0f)
			{
				Origin = origin;
				direction.EnsureNormalized();
				Direction = direction;
				Radius = radius;
				Hit = hit;
				MinDepth = minDepth;
				MaxDepth = math.max(minDepth, maxDepth);
				MaxDistance = maxDistance;
			}
		}

		public readonly struct CircleCastAll : IDrawableCast, IDrawable
		{
			public readonly CircleCast CircleCast;

			public readonly IList<RaycastHit2D> Results;

			public readonly int ResultCount;

			public CircleCastAll(float2 origin, float radius, float2 direction, IList<RaycastHit2D> results, int resultCount, float maxDistance = float.PositiveInfinity, float minDepth = 0f, float maxDepth = 0f)
			{
				CircleCast = new CircleCast(origin, radius, direction, null, maxDistance, minDepth, maxDepth);
				Results = results;
				ResultCount = resultCount;
			}

			public CircleCastAll(float2 origin, float radius, float2 direction, IList<RaycastHit2D> results, float maxDistance = float.PositiveInfinity, float minDepth = 0f, float maxDepth = 0f)
				: this(origin, radius, direction, results, results.Count, maxDistance, minDepth, maxDepth)
			{
			}
		}

		public readonly struct BoxCast2D : IDrawableCast, IDrawable
		{
			public readonly Box2D Box;

			public readonly float2 Direction;

			public readonly float Distance;

			public readonly float MinDepth;

			public readonly float MaxDepth;

			public readonly RaycastHit2D? Hit;

			public BoxCast2D(float2 origin, float2 size, float angleDegrees, float2 direction, RaycastHit2D? hit, float distance = float.PositiveInfinity, float minDepth = 0f, float maxDepth = 0f)
			{
				MinDepth = minDepth;
				MaxDepth = math.max(minDepth, maxDepth);
				Box = new Box2D(origin, size, angleDegrees, minDepth);
				Hit = hit;
				direction.EnsureNormalized();
				Direction = direction;
				Distance = distance;
			}
		}

		public readonly struct BoxCast2DAll : IDrawableCast, IDrawable
		{
			public readonly BoxCast2D BoxCast;

			public readonly IList<RaycastHit2D> Results;

			public readonly int ResultCount;

			public BoxCast2DAll(float2 origin, float2 size, float angleDegrees, float2 direction, IList<RaycastHit2D> results, int resultsCount, float distance = float.PositiveInfinity, float minDepth = 0f, float maxDepth = 0f)
			{
				BoxCast = new BoxCast2D(origin, size, angleDegrees, direction, null, distance, minDepth, maxDepth);
				Results = results;
				ResultCount = resultsCount;
			}

			public BoxCast2DAll(float2 origin, float2 size, float angleDegrees, float2 direction, IList<RaycastHit2D> results, float distance = float.PositiveInfinity, float minDepth = 0f, float maxDepth = 0f)
				: this(origin, size, angleDegrees, direction, results, results.Count, distance, minDepth, maxDepth)
			{
			}
		}

		public readonly struct CapsuleCast2D : IDrawableCast, IDrawable
		{
			public readonly Capsule2D Capsule;

			public readonly float2 Direction;

			public readonly float Distance;

			public readonly float MinDepth;

			public readonly float MaxDepth;

			public readonly RaycastHit2D? Hit;

			public CapsuleCast2D(float2 origin, float2 size, CapsuleDirection2D capsuleDirection, float angle, float2 direction, RaycastHit2D? hit, float maxDistance = float.PositiveInfinity, float minDepth = 0f, float maxDepth = 0f)
			{
				Capsule = new Capsule2D(origin, size, capsuleDirection, angle, minDepth);
				direction.EnsureNormalized();
				Direction = direction;
				Hit = hit;
				Distance = maxDistance;
				MinDepth = minDepth;
				MaxDepth = math.max(minDepth, maxDepth);
			}
		}

		public readonly struct CapsuleCast2DAll : IDrawableCast, IDrawable
		{
			public readonly CapsuleCast2D CapsuleCast;

			public readonly IList<RaycastHit2D> Results;

			public readonly int ResultCount;

			public CapsuleCast2DAll(float2 origin, float2 size, CapsuleDirection2D capsuleDirection, float angleDegrees, float2 direction, IList<RaycastHit2D> results, int count, float maxDistance = float.PositiveInfinity, float minDepth = 0f, float maxDepth = 0f)
			{
				CapsuleCast = new CapsuleCast2D(origin, size, capsuleDirection, angleDegrees, direction, null, maxDistance, minDepth, maxDepth);
				Results = results;
				ResultCount = count;
			}

			public CapsuleCast2DAll(float2 origin, float2 size, CapsuleDirection2D capsuleDirection, float angleDegrees, float2 direction, IList<RaycastHit2D> results, float maxDistance = float.PositiveInfinity, float minDepth = 0f, float maxDepth = 0f)
				: this(origin, size, capsuleDirection, angleDegrees, direction, results, results.Count, maxDistance, minDepth, maxDepth)
			{
			}
		}

		public readonly struct Raycast : IDrawableCast, IDrawable
		{
			public readonly Ray Ray;

			public readonly RaycastHit? Hit;

			public Raycast(float3 origin, float3 direction, RaycastHit? hit, float distance = float.PositiveInfinity)
			{
				Ray = new Ray(origin, direction, distance);
				Hit = hit;
			}

			public Raycast(UnityEngine.Ray ray, RaycastHit? hit, float distance = float.PositiveInfinity)
				: this(ray.origin, ray.direction, hit, distance)
			{
			}
		}

		public readonly struct Linecast : IDrawableCast, IDrawable
		{
			public readonly Line Line;

			public readonly RaycastHit? Hit;

			public Linecast(float3 start, float3 end, RaycastHit? hit)
			{
				Line = new Line(start, end);
				Hit = hit;
			}
		}

		public readonly struct RaycastAll : IDrawableCast, IDrawable
		{
			public readonly Ray Ray;

			public readonly RaycastHit[] Results;

			public readonly int ResultCount;

			public RaycastAll(float3 origin, float3 direction, RaycastHit[] results, int resultCount, float distance = float.PositiveInfinity)
			{
				direction = math.normalizesafe(direction);
				Ray = new Ray(origin, direction, distance);
				Results = results;
				ResultCount = resultCount;
			}

			public RaycastAll(float3 origin, float3 direction, RaycastHit[] results, float distance = float.PositiveInfinity)
				: this(origin, direction, results, results.Length, distance)
			{
			}

			public RaycastAll(UnityEngine.Ray ray, RaycastHit[] results, int resultCount, float distance = float.PositiveInfinity)
				: this(ray.origin, ray.direction, results, resultCount, distance)
			{
			}

			public RaycastAll(UnityEngine.Ray ray, RaycastHit[] results, float distance = float.PositiveInfinity)
				: this(ray, results, results.Length, distance)
			{
			}
		}

		public readonly struct SphereCast : IDrawableCast, IDrawable
		{
			public readonly float3 Origin;

			public readonly float Radius;

			public readonly float3 Direction;

			public readonly float MaxDistance;

			public readonly RaycastHit? Hit;

			public SphereCast(float3 origin, float radius, float3 direction, RaycastHit? hit, float maxDistance = float.PositiveInfinity)
			{
				Origin = origin;
				direction.EnsureNormalized();
				Direction = direction;
				Radius = radius;
				Hit = hit;
				MaxDistance = GetClampedMaxDistance(maxDistance);
			}

			public SphereCast(UnityEngine.Ray ray, float radius, RaycastHit? hit, float maxDistance = float.PositiveInfinity)
				: this(ray.origin, radius, ray.direction, hit, maxDistance)
			{
			}
		}

		public readonly struct SphereCastAll : IDrawableCast, IDrawable
		{
			public readonly float3 Origin;

			public readonly float Radius;

			public readonly float3 Direction;

			public readonly float MaxDistance;

			public readonly RaycastHit[] Results;

			public readonly int ResultCount;

			public SphereCastAll(float3 origin, float radius, float3 direction, RaycastHit[] results, int resultCount, float maxDistance = float.PositiveInfinity)
			{
				Origin = origin;
				direction.EnsureNormalized();
				Direction = direction;
				Radius = radius;
				Results = results;
				ResultCount = resultCount;
				MaxDistance = GetClampedMaxDistance(maxDistance);
			}

			public SphereCastAll(float3 origin, float radius, float3 direction, RaycastHit[] results, float maxDistance = float.PositiveInfinity)
				: this(origin, radius, direction, results, results.Length, maxDistance)
			{
			}

			public SphereCastAll(UnityEngine.Ray ray, float radius, RaycastHit[] results, int resultCount, float distance = float.PositiveInfinity)
				: this(ray.origin, radius, ray.direction, results, resultCount, distance)
			{
			}

			public SphereCastAll(UnityEngine.Ray ray, float radius, RaycastHit[] results, float distance = float.PositiveInfinity)
				: this(ray, radius, results, results.Length, distance)
			{
			}
		}

		public readonly struct BoxCast : IDrawableCast, IDrawable
		{
			public readonly Box Box;

			public readonly float3 Direction;

			public readonly float MaxDistance;

			public readonly RaycastHit? Hit;

			public BoxCast(Box box, float3 direction, RaycastHit? hit, float maxDistance = float.PositiveInfinity)
			{
				Box = box;
				Direction = direction;
				Hit = hit;
				MaxDistance = GetClampedMaxDistance(maxDistance);
			}

			public BoxCast(float3 center, float3 halfExtents, float3 direction, RaycastHit? hit, quaternion orientation, float maxDistance = float.PositiveInfinity)
				: this(new Box(center, halfExtents, orientation), direction, hit, maxDistance)
			{
			}

			public BoxCast(float3 center, float3 halfExtents, float3 direction, RaycastHit? hit, float maxDistance = float.PositiveInfinity)
				: this(center, halfExtents, direction, hit, quaternion.identity, maxDistance)
			{
			}
		}

		public readonly struct BoxCastAll : IDrawableCast, IDrawable
		{
			public readonly Box Box;

			public readonly float3 Direction;

			public readonly float MaxDistance;

			public readonly RaycastHit[] Results;

			public readonly int ResultCount;

			public BoxCastAll(Box box, float3 direction, RaycastHit[] results, int count, float maxDistance = float.PositiveInfinity)
			{
				Box = box;
				Direction = direction;
				Results = results;
				ResultCount = count;
				MaxDistance = GetClampedMaxDistance(maxDistance);
			}

			public BoxCastAll(float3 center, float3 halfExtents, float3 direction, RaycastHit[] results, int count, quaternion orientation, float maxDistance = float.PositiveInfinity)
				: this(new Box(center, halfExtents, orientation), direction, results, count, maxDistance)
			{
			}

			public BoxCastAll(float3 center, float3 halfExtents, float3 direction, RaycastHit[] results, int count, float maxDistance = float.PositiveInfinity)
				: this(center, halfExtents, direction, results, count, quaternion.identity, maxDistance)
			{
			}

			public BoxCastAll(float3 center, float3 halfExtents, float3 direction, RaycastHit[] results, quaternion orientation, float maxDistance = float.PositiveInfinity)
				: this(center, halfExtents, direction, results, results.Length, orientation, maxDistance)
			{
			}

			public BoxCastAll(float3 center, float3 halfExtents, float3 direction, RaycastHit[] results, float maxDistance = float.PositiveInfinity)
				: this(center, halfExtents, direction, results, results.Length, quaternion.identity, maxDistance)
			{
			}
		}

		public readonly struct CapsuleCast : IDrawableCast, IDrawable
		{
			public readonly Capsule Capsule;

			public readonly float3 Direction;

			public readonly float MaxDistance;

			public readonly RaycastHit? Hit;

			public CapsuleCast(Capsule capsule, float3 direction, RaycastHit? hit, float maxDistance = float.PositiveInfinity)
			{
				Capsule = capsule;
				Direction = direction;
				Hit = hit;
				MaxDistance = GetClampedMaxDistance(maxDistance);
			}
		}

		public readonly struct CapsuleCastAll : IDrawableCast, IDrawable
		{
			public readonly Capsule Capsule;

			public readonly float3 Direction;

			public readonly float MaxDistance;

			public readonly RaycastHit[] Results;

			public readonly int ResultCount;

			public CapsuleCastAll(Capsule capsule, float3 direction, RaycastHit[] results, int count, float maxDistance = float.PositiveInfinity)
			{
				Capsule = capsule;
				Direction = direction;
				Results = results;
				ResultCount = count;
				MaxDistance = maxDistance;
			}

			public CapsuleCastAll(Capsule capsule, float3 direction, RaycastHit[] results, float maxDistance = float.PositiveInfinity)
				: this(capsule, direction, results, results.Length, maxDistance)
			{
			}
		}

		public readonly struct Line : IDrawable
		{
			public readonly float3 A;

			public readonly float3 B;

			public Line(float3 a, float3 b)
			{
				A = a;
				B = b;
			}

			public Line(Vector3 a, Vector3 b)
				: this((float3)a, (float3)b)
			{
			}

			public Line(Ray ray)
			{
				A = ray.Origin;
				B = ray.Origin + ray.Direction;
			}

			public Line GetShortened(float shortenBy, float minShorteningNormalised = 0f)
			{
				float3 vector = B - A;
				vector.EnsureNormalized(out var length);
				float num = length;
				length = math.max(length - shortenBy, length * minShorteningNormalised);
				length = num - length;
				return new Line(A + vector * length, B - vector * length);
			}
		}

		public readonly struct DashedLine : IDrawable
		{
			public readonly Line Line;

			public DashedLine(Line line)
			{
				Line = line;
			}

			public DashedLine(float3 a, float3 b)
				: this(new Line(a, b))
			{
			}

			public DashedLine(Vector3 a, Vector3 b)
				: this((float3)a, (float3)b)
			{
			}

			public DashedLine(Ray ray)
				: this(new Line(ray))
			{
			}

			public DashedLine GetShortened(float shortenBy, float minShorteningNormalised = 0f)
			{
				return new DashedLine(Line.GetShortened(shortenBy, minShorteningNormalised));
			}
		}

		public readonly struct LineStrip : IDrawable
		{
			public readonly IEnumerable<float3> Points;

			public LineStrip(IEnumerable<float3> points)
			{
				Points = points;
			}

			public LineStrip(IEnumerable<Vector3> points)
			{
				Points = points.Select((Func<Vector3, float3>)((Vector3 v) => v));
			}
		}

		public readonly struct Ray : IDrawable
		{
			public readonly float3 Origin;

			public readonly float3 Direction;

			public Ray(float3 origin, float3 direction)
			{
				Origin = origin;
				Direction = direction;
			}

			public Ray(Vector3 origin, Vector3 direction)
				: this((float3)origin, (float3)direction)
			{
			}

			public Ray(float3 origin, float3 direction, float distance)
			{
				Origin = origin;
				direction.EnsureNormalized();
				Direction = direction * GetClampedMaxDistance(distance);
			}

			public Ray(Vector3 origin, Vector3 direction, float distance)
				: this((float3)origin, (float3)direction, distance)
			{
			}

			public Ray(UnityEngine.Ray ray, float distance = float.PositiveInfinity)
				: this(ray.origin, ray.direction * GetClampedMaxDistance(distance))
			{
			}

			public Ray(UnityEngine.Ray2D ray, float distance = float.PositiveInfinity)
				: this(ray.origin.xy0(), (ray.direction * GetClampedMaxDistance(distance)).xy0())
			{
			}

			public static implicit operator Ray(UnityEngine.Ray ray)
			{
				return new Ray(ray.origin, ray.direction);
			}

			public Ray(RaycastHit hit)
			{
				Origin = hit.point;
				Direction = hit.normal;
			}

			public Ray(RaycastHit2D hit)
			{
				Origin = new float3(hit.point.x, hit.point.y, hit ? hit.transform.position.z : 0f);
				Direction = new float3(hit.normal.x, hit.normal.y, 0f);
			}
		}

		public readonly struct Point : IDrawable
		{
			public readonly float3 Position;

			public readonly float Scale;

			public Point(float3 position, float scale = 0.3f)
			{
				Position = position;
				Scale = scale;
			}

			public Point(Vector3 position, float scale = 0.3f)
				: this((float3)position, scale)
			{
			}

			public Point(float2 position, float scale = 0.3f)
				: this(new float3(position.x, position.y, 0f), scale)
			{
			}

			public Point(Vector2 position, float scale = 0.3f)
				: this(new float3(position.x, position.y, 0f), scale)
			{
			}
		}

		public readonly struct Arrow : IDrawable
		{
			public readonly float3 Origin;

			public readonly float3 Direction;

			public readonly float ArrowheadScale;

			public Arrow(float3 origin, float3 direction, float arrowheadScale = 1f)
			{
				Origin = origin;
				Direction = direction;
				ArrowheadScale = arrowheadScale;
			}

			public Arrow(Vector3 origin, Vector3 direction, float arrowheadScale = 1f)
				: this((float3)origin, (float3)direction, arrowheadScale)
			{
			}

			public Arrow(float3 origin, quaternion rotation, float length = 1f, float arrowheadScale = 1f)
				: this(origin, math.mul(rotation, math.forward()) * length, arrowheadScale)
			{
			}

			public Arrow(Vector3 origin, Quaternion rotation, float length = 1f, float arrowheadScale = 1f)
				: this((float3)origin, (quaternion)rotation, length, arrowheadScale)
			{
			}

			public Arrow(Line line, float arrowheadScale = 1f)
				: this(line.A, line.B - line.A, arrowheadScale)
			{
			}
		}

		public readonly struct ArrowStrip : IDrawable
		{
			public readonly IEnumerable<float3> Points;

			public readonly float ArrowheadScale;

			public ArrowStrip(IEnumerable<float3> points, float arrowheadScale = 1f)
			{
				Points = points;
				ArrowheadScale = arrowheadScale;
			}

			public ArrowStrip(IEnumerable<Vector3> points, float arrowheadScale = 1f)
				: this(points.Select((Func<Vector3, float3>)((Vector3 v) => v)), arrowheadScale)
			{
			}
		}

		public readonly struct HalfArrow : IDrawable
		{
			public readonly Line Line;

			public readonly float3 Perpendicular;

			public readonly float ArrowheadScale;

			public HalfArrow(Line line, float3 perpendicular, float arrowheadScale = 1f)
			{
				Line = line;
				perpendicular.EnsureNormalized();
				Perpendicular = perpendicular;
				ArrowheadScale = arrowheadScale;
			}

			public HalfArrow(Line line, Vector3 perpendicular, float arrowheadScale = 1f)
				: this(line, (float3)perpendicular, arrowheadScale)
			{
			}

			public HalfArrow(float3 origin, float3 direction, float3 perpendicular, float arrowheadScale = 1f)
				: this(new Line(origin, origin + direction), perpendicular, arrowheadScale)
			{
			}

			public HalfArrow(Vector3 origin, Vector3 direction, Vector3 perpendicular, float arrowheadScale = 1f)
				: this((float3)origin, (float3)direction, (float3)perpendicular, arrowheadScale)
			{
			}
		}

		public readonly struct CurvedArrow : IDrawable
		{
			public readonly float3 Origin;

			public readonly float3 Direction;

			public readonly float3 Perpendicular;

			public readonly Angle Angle;

			public CurvedArrow(float3 origin, float3 direction, float3 perpendicular)
				: this(origin, direction, perpendicular, Angle.FromTurns(0.1f))
			{
			}

			public CurvedArrow(Vector3 origin, Vector3 direction, Vector3 perpendicular)
				: this((float3)origin, (float3)direction, (float3)perpendicular)
			{
			}

			public CurvedArrow(float3 origin, float3 direction, float3 perpendicular, Angle angle)
			{
				Origin = origin;
				Direction = direction;
				perpendicular.EnsureNormalized();
				if (!Mathf.Approximately(math.dot(direction, perpendicular), 0f))
				{
					direction.EnsureNormalized();
					perpendicular = math.normalize(math.cross(direction, math.cross(direction, perpendicular)));
				}
				Perpendicular = perpendicular;
				Angle = ((angle.Turns > 0.5f) ? Angle.FromTurns(0.5f) : angle);
			}

			public CurvedArrow(Vector3 origin, Vector3 direction, Vector3 perpendicular, Angle angle)
				: this((float3)origin, (float3)direction, (float3)perpendicular, angle)
			{
			}

			public CurvedArrow(in Line line, float3 perpendicular)
				: this(line.A, line.B - line.A, perpendicular)
			{
			}

			public CurvedArrow(in Line line, Vector3 perpendicular)
				: this(in line, (float3)perpendicular)
			{
			}

			public CurvedArrow(in Line line, float3 perpendicular, Angle angle)
				: this(line.A, line.B - line.A, perpendicular, angle)
			{
			}

			public CurvedArrow(in Line line, Vector3 perpendicular, Angle angle)
				: this(in line, (float3)perpendicular, angle)
			{
			}
		}

		public readonly struct Arrow2DFromNormal : IDrawable
		{
			public readonly float3 Origin;

			public readonly float3 Direction;

			public readonly float3 Normal;

			public Arrow2DFromNormal(float3 origin, float3 direction, float3 normal)
			{
				Origin = origin;
				Direction = direction;
				Normal = normal;
				Normal.EnsureNormalized();
			}

			public Arrow2DFromNormal(Vector3 origin, Vector3 direction, Vector3 normal)
				: this((float3)origin, (float3)direction, (float3)normal)
			{
			}

			public Arrow2DFromNormal(float3 origin, quaternion rotation, float length = 1f)
			{
				Origin = origin;
				Direction = math.mul(rotation, math.forward()) * length;
				Normal = math.mul(rotation, math.right());
				Normal.EnsureNormalized();
			}

			public Arrow2DFromNormal(Vector3 origin, Quaternion rotation, float length = 1f)
				: this((float3)origin, (quaternion)rotation, length)
			{
			}
		}

		public readonly struct Axis : IDrawable
		{
			public readonly float3 Origin;

			public readonly quaternion Rotation;

			public readonly bool ShowArrowHeads;

			public readonly Axes VisibleAxes;

			public readonly float Scale;

			public Axis(float3 origin, quaternion rotation, bool showArrowHeads = true, Axes visibleAxes = Axes.All, float scale = 1f)
			{
				Origin = origin;
				Rotation = rotation;
				ShowArrowHeads = showArrowHeads;
				VisibleAxes = visibleAxes;
				Scale = scale;
			}

			public Axis(Vector3 origin, Quaternion rotation, bool showArrowHeads = true, Axes visibleAxes = Axes.All, float scale = 1f)
				: this((float3)origin, (quaternion)rotation, showArrowHeads, visibleAxes, scale)
			{
			}

			public Axis(Transform transform, bool showArrowHeads = true, Axes visibleAxes = Axes.All, float scale = 1f)
			{
				Origin = transform.position;
				Rotation = transform.rotation;
				ShowArrowHeads = showArrowHeads;
				VisibleAxes = visibleAxes;
				Scale = scale;
			}
		}

		public readonly struct SurfacePoint : IDrawable
		{
			public readonly float3 Origin;

			public readonly float3 Direction;

			public readonly float Radius;

			public SurfacePoint(float3 origin, float3 direction)
			{
				Origin = origin;
				direction.EnsureNormalized(out var length);
				Direction = direction;
				Radius = length * 0.05f;
			}

			public SurfacePoint(Vector3 origin, Vector3 direction)
				: this((float3)origin, (float3)direction)
			{
			}

			public SurfacePoint(float3 origin, float3 direction, float radius)
			{
				Origin = origin;
				direction.EnsureNormalized(out var _);
				Direction = direction;
				Radius = radius;
			}

			public SurfacePoint(Vector3 origin, Vector3 direction, float radius)
				: this((float3)origin, (float3)direction, radius)
			{
			}
		}

		public readonly struct Circle : IDrawable
		{
			private readonly Arc _arc;

			public float4x4 Matrix => _arc.Matrix;

			public Circle(float4x4 matrix)
			{
				_arc = new Arc(matrix);
			}

			public Circle(Matrix4x4 matrix)
				: this((float4x4)matrix)
			{
			}

			public Circle(float3 origin, quaternion rotation, float radius)
			{
				_arc = new Arc(float4x4.TRS(origin, rotation, new float3(radius, radius, radius)));
			}

			public Circle(Vector3 origin, Quaternion rotation, float radius)
				: this((float3)origin, (quaternion)rotation, radius)
			{
			}

			public Circle(float3 origin, float3 normal, float3 direction, float radius)
				: this(origin, math.mul(quaternion.LookRotation((math.abs(math.dot(direction, normal)) > 0.999f) ? GetValidPerpendicular(normal) : direction, normal), Arc.Base3DRotation), radius)
			{
			}

			public Circle(Vector3 origin, Vector3 normal, Vector3 direction, float radius)
				: this((float3)origin, (float3)normal, (float3)direction, radius)
			{
			}

			public Circle(float3 origin, float3 normal, float radius)
				: this(origin, normal, GetValidPerpendicular(normal), radius)
			{
			}

			public Circle(Vector3 origin, Vector3 normal, float radius)
				: this((float3)origin, (float3)normal, radius)
			{
			}
		}

		public readonly struct Arc : IDrawable
		{
			public readonly float4x4 Matrix;

			public readonly Angle Angle;

			internal static readonly quaternion Base3DRotation = quaternion.Euler(MathF.PI / 2f, -MathF.PI / 2f, 0f);

			public Arc(float4x4 matrix, Angle angle)
			{
				Matrix = matrix;
				Angle = angle;
			}

			public Arc(Matrix4x4 matrix, Angle angle)
				: this((float4x4)matrix, angle)
			{
			}

			internal Arc(float4x4 matrix)
				: this(matrix, Angle.FromTurns(1f))
			{
			}

			public Arc(Matrix4x4 matrix)
				: this((float4x4)matrix)
			{
			}

			public Arc(float3 origin, quaternion rotation, float radius, Angle angle)
				: this(float4x4.TRS(origin, rotation, new float3(radius, radius, radius)), angle)
			{
			}

			public Arc(Vector3 origin, Quaternion rotation, float radius, Angle angle)
				: this((float3)origin, (quaternion)rotation, radius, angle)
			{
			}

			public Arc(float3 origin, quaternion rotation, float radius, Angle fromAngle, Angle toAngle)
				: this(origin, GetAdjustedRotation(rotation, fromAngle, toAngle, out var total), radius, total)
			{
			}

			public Arc(Vector3 origin, Quaternion rotation, float radius, Angle fromAngle, Angle toAngle)
				: this((float3)origin, (quaternion)rotation, radius, fromAngle, toAngle)
			{
			}

			public Arc(float3 origin, quaternion rotation, float radius)
				: this(origin, rotation, radius, Angle.FromTurns(1f))
			{
			}

			public Arc(Vector3 origin, Quaternion rotation, float radius)
				: this((float3)origin, (quaternion)rotation, radius)
			{
			}

			public Arc(float2 origin, float rotationDegrees, float radius, Angle angle)
				: this(origin.xy0(), float.IsNaN(rotationDegrees) ? quaternion.identity : quaternion.AxisAngle(math.forward(), rotationDegrees * (MathF.PI / 180f)), radius, angle)
			{
			}

			public Arc(Vector2 origin, float rotationDegrees, float radius, Angle angle)
				: this((float2)origin, rotationDegrees, radius, angle)
			{
			}

			public Arc(float2 origin, float rotationDegrees, float radius, Angle fromAngle, Angle toAngle)
				: this(origin.xy0(), GetAdjustedRotation(float.IsNaN(rotationDegrees) ? quaternion.identity : quaternion.AxisAngle(math.forward(), rotationDegrees * (MathF.PI / 180f)), fromAngle, toAngle, out var total), radius, total)
			{
			}

			public Arc(Vector2 origin, float rotationDegrees, float radius, Angle fromAngle, Angle toAngle)
				: this((float2)origin, rotationDegrees, radius, fromAngle, toAngle)
			{
			}

			public Arc(float2 origin, float rotationDegrees, float radius)
				: this(origin, rotationDegrees, radius, Angle.FromTurns(1f))
			{
			}

			public Arc(Vector2 origin, float rotationDegrees, float radius)
				: this((float2)origin, rotationDegrees, radius)
			{
			}

			public Arc(float3 origin, float3 normal, float3 direction, float radius, Angle angle)
				: this(origin, math.mul(quaternion.LookRotation(direction, normal), Base3DRotation), radius, angle)
			{
			}

			public Arc(Vector3 origin, Vector3 normal, Vector3 direction, float radius, Angle angle)
				: this((float3)origin, (float3)normal, (float3)direction, radius, angle)
			{
			}

			public Arc(float3 origin, float3 normal, float3 direction, float radius, Angle fromAngle, Angle toAngle)
				: this(origin, GetAdjustedRotation(math.mul(quaternion.LookRotation(direction, normal), Base3DRotation), fromAngle, toAngle, out var total), radius, total)
			{
			}

			public Arc(Vector3 origin, Vector3 normal, Vector3 direction, float radius, Angle fromAngle, Angle toAngle)
				: this((float3)origin, (float3)normal, (float3)direction, radius, fromAngle, toAngle)
			{
			}

			public Arc(float3 origin, float3 normal, float3 direction, float radius)
				: this(origin, normal, direction, radius, Angle.FromTurns(1f))
			{
			}

			public Arc(Vector3 origin, Vector3 normal, Vector3 direction, float radius)
				: this((float3)origin, (float3)normal, (float3)direction, radius)
			{
			}

			public Arc(float3 origin, float3 normal, float radius)
				: this(origin, normal, GetValidPerpendicular(normal), radius, Angle.FromTurns(1f))
			{
			}

			public Arc(Vector3 origin, Vector3 normal, float radius)
				: this((float3)origin, (float3)normal, radius)
			{
			}

			public Arc(Line chord, float3 aim, float arcLength)
			{
				float3 float5 = chord.A - chord.B;
				float num = math.length(float5);
				if (num < 1E-05f || arcLength <= num)
				{
					Angle = default(Angle);
					Matrix = float4x4.identity;
					return;
				}
				var (num2, num3, angle) = GetSegmentDetails(num, arcLength);
				float5 /= num;
				aim.EnsureNormalized();
				float3 float6 = math.normalizesafe(math.cross(aim, float5));
				float3 float7 = math.cross(float5, float6);
				Matrix = float4x4.TRS((chord.A + chord.B) / 2f - float7 * num3, math.mul(quaternion.LookRotation(float7, float6), Base3DRotation), new float3(num2, num2, num2));
				Angle = angle;
			}

			public Arc(Line chord, Vector3 aim, float arcLength)
				: this(chord, (float3)aim, arcLength)
			{
			}

			private static (float radius, float height, Angle angle) GetSegmentDetails(float chordLength, float arcLength)
			{
				float num = math.sqrt(48f * ((arcLength - chordLength) / (2f * arcLength)));
				int num2 = 0;
				float num4;
				do
				{
					float num3 = math.sin(num * 0.5f);
					num4 = num3 / num - chordLength / (2f * arcLength);
					float num5 = (num * math.cos(num * 0.5f) - 2f * num3) / (2f * num * num);
					num -= num4 / num5;
				}
				while (num4 > 0.001f && ++num2 < 10);
				float num6 = num;
				float num7 = arcLength / num6;
				float item = num7 * math.cos(0.5f * num6);
				return (radius: num7, height: item, angle: Angle.FromRadians(num6));
			}

			public static float GetRadius(in Angle angle, float chordLength)
			{
				return chordLength / (2f * math.sin(0.5f * angle.Radians));
			}

			public static Angle GetAngle(float radius, float chordLength)
			{
				return Angle.FromRadians(math.asin(chordLength / (2f * radius)) * 2f);
			}

			public static float GetChordLength(in Angle angle, float radius)
			{
				return 2f * radius * math.sin(angle.Radians * 0.5f);
			}

			internal static quaternion GetAdjustedRotation(quaternion rotation, Angle fromAngle, Angle toAngle, out Angle total)
			{
				float num = (fromAngle.Radians + toAngle.Radians) / 2f;
				total = Angle.FromTurns((float)toAngle - (float)fromAngle);
				return math.mul(rotation, quaternion.RotateZ(0f - num));
			}
		}

		public readonly struct Annulus : IDrawable
		{
			public readonly float3 Origin;

			public readonly quaternion Rotation;

			public readonly float InnerRadius;

			public readonly float OuterRadius;

			public readonly Angle SectorWidth;

			public Annulus(float3 origin, quaternion rotation, float innerRadius, float outerRadius)
				: this(origin, rotation, innerRadius, outerRadius, Angle.FromTurns(1f))
			{
			}

			public Annulus(Vector3 origin, Quaternion rotation, float innerRadius, float outerRadius)
				: this((float3)origin, (quaternion)rotation, innerRadius, outerRadius)
			{
			}

			public Annulus(float3 origin, float3 normal, float3 direction, float innerRadius, float outerRadius)
				: this(origin, normal, direction, innerRadius, outerRadius, Angle.FromTurns(1f))
			{
			}

			public Annulus(Vector3 origin, Vector3 normal, Vector3 direction, float innerRadius, float outerRadius)
				: this((float3)origin, (float3)normal, (float3)direction, innerRadius, outerRadius)
			{
			}

			public Annulus(float3 origin, quaternion rotation, float innerRadius, float outerRadius, Angle sectorWidth)
			{
				SectorWidth = sectorWidth;
				Origin = origin;
				Rotation = rotation;
				InnerRadius = innerRadius;
				OuterRadius = outerRadius;
			}

			public Annulus(Vector3 origin, Quaternion rotation, float innerRadius, float outerRadius, Angle sectorWidth)
				: this((float3)origin, (quaternion)rotation, innerRadius, outerRadius, sectorWidth)
			{
			}

			public Annulus(float3 origin, quaternion rotation, float innerRadius, float outerRadius, Angle fromAngle, Angle toAngle)
				: this(origin, Arc.GetAdjustedRotation(rotation, fromAngle, toAngle, out var total), innerRadius, outerRadius, total)
			{
			}

			public Annulus(Vector3 origin, Quaternion rotation, float innerRadius, float outerRadius, Angle fromAngle, Angle toAngle)
				: this((float3)origin, (quaternion)rotation, innerRadius, outerRadius, fromAngle, toAngle)
			{
			}

			public Annulus(float3 origin, float3 normal, float3 direction, float innerRadius, float outerRadius, Angle sectorWidth)
				: this(origin, math.mul(quaternion.LookRotation(direction, normal), Arc.Base3DRotation), innerRadius, outerRadius, sectorWidth)
			{
			}

			public Annulus(Vector3 origin, Vector3 normal, Vector3 direction, float innerRadius, float outerRadius, Angle sectorWidth)
				: this((float3)origin, (float3)normal, (float3)direction, innerRadius, outerRadius, sectorWidth)
			{
			}

			public Annulus(float3 origin, float3 normal, float3 direction, float innerRadius, float outerRadius, Angle fromAngle, Angle toAngle)
				: this(origin, math.mul(quaternion.LookRotation(direction, normal), Arc.Base3DRotation), innerRadius, outerRadius, fromAngle, toAngle)
			{
			}

			public Annulus(Vector3 origin, Vector3 normal, Vector3 direction, float innerRadius, float outerRadius, Angle fromAngle, Angle toAngle)
				: this((float3)origin, (float3)normal, (float3)direction, innerRadius, outerRadius, fromAngle, toAngle)
			{
			}

			public float3 RandomPoint()
			{
				return Origin + math.mul(Rotation, RandomPoint(InnerRadius, OuterRadius, SectorWidth).xy0());
			}

			[PublicAPI]
			public static float2 RandomPoint(float innerRadius, float outerRadius, Angle sectorWidth)
			{
				float num = sectorWidth.Radians * 0.5f;
				float x = UnityEngine.Random.Range(0f - num, num);
				float num2 = outerRadius * outerRadius;
				float num3 = innerRadius * innerRadius;
				float num4 = num2 - num3;
				float num5 = math.sqrt(UnityEngine.Random.value * num4 + num3);
				float x2 = num5 * math.cos(x);
				float y = num5 * math.sin(x);
				return new float2(x2, y);
			}

			[PublicAPI]
			public static float2 RandomPointNonUniform(float innerRadius, float outerRadius, Angle sectorWidth)
			{
				float num = sectorWidth.Radians * 0.5f;
				float x = UnityEngine.Random.Range(0f - num, num);
				float num2 = outerRadius - innerRadius;
				float num3 = UnityEngine.Random.value * num2 + innerRadius;
				float x2 = num3 * math.cos(x);
				float y = num3 * math.sin(x);
				return new float2(x2, y);
			}
		}

		public readonly struct Sphere : IDrawable
		{
			public readonly float4x4 Matrix;

			public Sphere(float4x4 matrix)
			{
				Matrix = matrix;
			}

			public Sphere(Matrix4x4 matrix)
				: this((float4x4)matrix)
			{
			}

			public Sphere(float3 origin)
			{
				Matrix = float4x4.Translate(origin);
			}

			public Sphere(Vector3 origin)
				: this((float3)origin)
			{
			}

			public Sphere(float3 origin, float radius)
			{
				Matrix = math.mul(float4x4.Translate(origin), float4x4.Scale(new float3(radius, radius, radius)));
			}

			public Sphere(Vector3 origin, float radius)
				: this((float3)origin, radius)
			{
			}

			public Sphere(float3 origin, quaternion rotation, float radius)
			{
				Matrix = float4x4.TRS(origin, rotation, new float3(radius, radius, radius));
			}

			public Sphere(Vector3 origin, Quaternion rotation, float radius)
				: this((float3)origin, (quaternion)rotation, radius)
			{
			}

			public Sphere(Transform transform, float radius)
				: this(transform.position, transform.rotation, radius)
			{
			}

			public Sphere(Transform transform)
			{
				Matrix = transform.localToWorldMatrix;
			}

			public Sphere(SphereCollider sphereCollider)
			{
				Transform transform = sphereCollider.transform;
				float3 float5 = transform.lossyScale;
				float num = math.max(math.abs(float5.x), math.max(math.abs(float5.y), math.abs(float5.z))) * sphereCollider.radius;
				Matrix = float4x4.TRS(transform.TransformPoint(sphereCollider.center), transform.rotation, new float3(num, num, num));
			}

			public Sphere GetTranslated(float3 translation)
			{
				return new Sphere(math.mul(float4x4.Translate(translation), Matrix));
			}
		}

		public readonly struct Hemisphere : IDrawable
		{
			public readonly float3 Origin;

			public readonly quaternion Orientation;

			public readonly float Radius;

			public Hemisphere(float3 origin, quaternion orientation, float radius)
			{
				Origin = origin;
				Orientation = orientation;
				Radius = radius;
			}

			public Hemisphere(Vector3 origin, Quaternion orientation, float radius)
				: this((float3)origin, (quaternion)orientation, radius)
			{
			}
		}

		public readonly struct Box : IDrawable
		{
			public readonly float4x4 Matrix;

			public readonly bool Shade3D;

			public Box(float4x4 matrix, bool shade3D = true)
			{
				Matrix = matrix;
				Shade3D = shade3D;
			}

			public Box(Matrix4x4 matrix, bool shade3D = true)
				: this((float4x4)matrix, shade3D)
			{
			}

			public Box(float3 position, float3 halfExtents, quaternion orientation, bool shade3D = true)
				: this(float4x4.TRS(position, orientation, halfExtents), shade3D)
			{
			}

			public Box(Vector3 position, Vector3 halfExtents, Quaternion orientation, bool shade3D = true)
				: this((float3)position, (float3)halfExtents, (quaternion)orientation, shade3D)
			{
			}

			public Box(float3 position, float3 halfExtents, bool shade3D = true)
				: this(float4x4.TRS(position, quaternion.identity, halfExtents), shade3D)
			{
			}

			public Box(Vector3 position, Vector3 halfExtents, bool shade3D = true)
				: this((float3)position, (float3)halfExtents, shade3D)
			{
			}

			public Box(Transform transform, bool shade3D = true)
			{
				Shade3D = shade3D;
				Matrix = transform.localToWorldMatrix;
			}

			public Box(Bounds bounds, bool shade3D = true)
				: this((float3)bounds.center, (float3)bounds.extents, quaternion.identity, shade3D)
			{
			}

			public Box(BoundsInt bounds, bool shade3D = true)
				: this((float3)bounds.center, bounds.size.xyz() * 2f, quaternion.identity, shade3D)
			{
			}

			public Box(BoxCollider boxCollider)
			{
				Shade3D = true;
				Transform transform = boxCollider.transform;
				Matrix = float4x4.TRS(transform.TransformPoint(boxCollider.center), transform.rotation, (float3)(boxCollider.size * 0.5f) * (float3)transform.lossyScale);
			}

			public Box GetTranslated(float3 translation)
			{
				return new Box(math.mul(float4x4.Translate(translation), Matrix));
			}
		}

		public readonly struct Capsule : IDrawable
		{
			public readonly float3 SpherePosition1;

			public readonly float3 SpherePosition2;

			public readonly float Radius;

			public Capsule(float3 center, quaternion rotation, float height, float radius)
			{
				float y = height * 0.5f - radius;
				float3 float5 = math.mul(rotation, new float3(0f, y, 0f));
				SpherePosition1 = center + float5;
				SpherePosition2 = center - float5;
				Radius = radius;
			}

			public Capsule(Vector3 center, Quaternion rotation, float height, float radius)
				: this((float3)center, (quaternion)rotation, height, radius)
			{
			}

			public Capsule(float3 spherePosition1, float3 spherePosition2, float radius)
			{
				SpherePosition1 = spherePosition1;
				SpherePosition2 = spherePosition2;
				Radius = radius;
			}

			public Capsule(Vector3 spherePosition1, Vector3 spherePosition2, float radius)
				: this((float3)spherePosition1, (float3)spherePosition2, radius)
			{
			}

			public Capsule(float3 lowestPosition, float3 direction, float height, float radius)
			{
				SpherePosition1 = lowestPosition + direction * radius;
				SpherePosition2 = SpherePosition1 + direction * (height - radius * 2f);
				Radius = radius;
			}

			public Capsule(Vector3 lowestPosition, Vector3 direction, float height, float radius)
				: this((float3)lowestPosition, (float3)direction, height, radius)
			{
			}

			public Capsule(CharacterController characterController)
			{
				Transform transform = characterController.transform;
				float3 float5 = transform.lossyScale;
				float num = math.max(math.abs(float5.x), math.abs(float5.z));
				Radius = characterController.radius * num;
				float num2 = math.max(characterController.height * 0.5f * math.abs(float5.y), Radius) - Radius;
				float3 float6 = transform.TransformPoint(characterController.center);
				SpherePosition1 = new float3(float6.x, float6.y - num2, float6.z);
				SpherePosition2 = new float3(float6.x, float6.y + num2, float6.z);
			}

			public Capsule(CapsuleCollider collider)
			{
				Transform transform = collider.transform;
				float3 float5 = transform.lossyScale;
				float num = math.max(math.abs(float5.x), math.abs(float5.z));
				Radius = collider.radius * num;
				float3 float6 = transform.TransformPoint(collider.center);
				float num2 = math.max(collider.height * 0.5f * math.abs(float5.y), Radius) - Radius;
				float3 float7 = collider.direction switch
				{
					0 => transform.TransformDirection(new float3(num2, 0f, 0f)), 
					2 => transform.TransformDirection(new float3(0f, 0f, num2)), 
					_ => transform.TransformDirection(new float3(0f, num2, 0f)), 
				};
				SpherePosition1 = float6 - float7;
				SpherePosition2 = float6 + float7;
			}

			public Capsule GetTranslated(float3 translation)
			{
				return new Capsule(SpherePosition1 + translation, SpherePosition2 + translation, Radius);
			}
		}

		public readonly struct Cylinder : IDrawable
		{
			public readonly float3 Center;

			public readonly quaternion Rotation;

			public readonly float HalfHeight;

			public readonly float Radius;

			public Cylinder(float3 center, quaternion rotation, float height, float radius)
			{
				Center = center;
				Rotation = rotation;
				HalfHeight = height * 0.5f;
				Radius = radius;
			}

			public Cylinder(Vector3 center, Quaternion rotation, float height, float radius)
				: this((float3)center, (quaternion)rotation, height, radius)
			{
			}

			public Cylinder(float3 point1, float3 point2, float radius)
			{
				float3 vector = point2 - point1;
				vector.EnsureNormalized(out HalfHeight);
				HalfHeight *= 0.5f;
				Center = point1 + vector * HalfHeight;
				Radius = radius;
				if (HalfHeight == 0f)
				{
					Rotation = quaternion.identity;
					return;
				}
				float3 validPerpendicular = GetValidPerpendicular(vector);
				Rotation = quaternion.LookRotation(validPerpendicular, vector);
			}

			public Cylinder(Vector3 point1, Vector3 point2, float radius)
				: this((float3)point1, (float3)point2, radius)
			{
			}

			public Cylinder(float3 lowestPosition, float3 direction, float height, float radius)
			{
				direction.EnsureNormalized();
				HalfHeight = height * 0.5f;
				Center = lowestPosition + direction * HalfHeight;
				Radius = radius;
				if (HalfHeight == 0f)
				{
					Rotation = quaternion.identity;
					return;
				}
				float3 validPerpendicular = GetValidPerpendicular(direction);
				Rotation = quaternion.LookRotation(validPerpendicular, direction);
			}

			public Cylinder(Vector3 lowestPosition, Vector3 direction, float height, float radius)
				: this((float3)lowestPosition, (float3)direction, height, radius)
			{
			}
		}

		public readonly struct Cone : IDrawable
		{
			public readonly float3 PointBase;

			public readonly quaternion Rotation;

			public readonly float Height;

			public readonly float RadiusBase;

			public readonly float RadiusTip;

			public Cone(float3 pointBase, float3 pointTip, float radiusBase, float radiusTip = 0f)
			{
				PointBase = pointBase;
				RadiusBase = radiusBase;
				RadiusTip = radiusTip;
				float3 vector = pointTip - PointBase;
				vector.EnsureNormalized(out Height);
				if (Height == 0f)
				{
					Rotation = quaternion.identity;
					return;
				}
				float3 validPerpendicular = GetValidPerpendicular(vector);
				Rotation = quaternion.LookRotation(vector, validPerpendicular);
			}

			public Cone(Vector3 pointBase, Vector3 pointTip, float radiusBase, float radiusTip = 0f)
				: this((float3)pointBase, (float3)pointTip, radiusBase, radiusTip)
			{
			}

			public Cone(float3 pointBase, float3 direction, float height, float radiusBase, float radiusTip = 0f)
			{
				PointBase = pointBase;
				RadiusBase = radiusBase;
				RadiusTip = radiusTip;
				Height = height;
				if (Height == 0f)
				{
					Rotation = quaternion.identity;
					return;
				}
				float3 validPerpendicular = GetValidPerpendicular(direction);
				Rotation = quaternion.LookRotation(direction, validPerpendicular);
			}

			public Cone(Vector3 pointBase, Vector3 direction, float height, float radiusBase, float radiusTip = 0f)
				: this((float3)pointBase, (float3)direction, height, radiusBase, radiusTip)
			{
			}

			public Cone(float3 pointBase, quaternion rotation, float height, float radiusBase, float radiusTip = 0f)
			{
				PointBase = pointBase;
				RadiusBase = radiusBase;
				RadiusTip = radiusTip;
				Height = height;
				Rotation = rotation;
			}

			public Cone(Vector3 pointBase, Quaternion rotation, float height, float radiusBase, float radiusTip = 0f)
				: this((float3)pointBase, (quaternion)rotation, height, radiusBase, radiusTip)
			{
			}

			public Cone Flip()
			{
				return new Cone(PointBase + math.mul(Rotation, new float3(0f, 0f, Height)), math.mul(Rotation, quaternion.AxisAngle(math.up(), 180f)), Height, RadiusBase, RadiusTip);
			}
		}

		public readonly struct Frustum : IDrawable
		{
			public readonly float4x4 Matrix;

			public Frustum(float4x4 matrix)
			{
				Matrix = matrix;
			}

			public Frustum(Matrix4x4 matrix)
				: this((float4x4)matrix)
			{
			}

			public Frustum(Camera camera, Camera.MonoOrStereoscopicEye eye = Camera.MonoOrStereoscopicEye.Mono)
			{
				Matrix4x4 matrix4x = eye switch
				{
					Camera.MonoOrStereoscopicEye.Left => camera.GetStereoProjectionMatrix(Camera.StereoscopicEye.Left), 
					Camera.MonoOrStereoscopicEye.Right => camera.GetStereoProjectionMatrix(Camera.StereoscopicEye.Right), 
					Camera.MonoOrStereoscopicEye.Mono => camera.projectionMatrix, 
					_ => throw new ArgumentOutOfRangeException("eye", eye, null), 
				};
				Matrix = camera.cameraToWorldMatrix * matrix4x.inverse;
			}

			public Frustum(float3 position, quaternion rotation, float fieldOfViewDegrees, float aspect, float nearClipPlane, float farClipPlane)
			{
				float4x4 a = float4x4.TRS(position, rotation, new float3(1));
				for (int i = 0; i < 3; i++)
				{
					a.c2[i] = 0f - a.c2[i];
				}
				Matrix = math.mul(a, math.inverse(float4x4.PerspectiveFov(math.radians(fieldOfViewDegrees), aspect, nearClipPlane, farClipPlane)));
			}

			public Frustum(Vector3 position, Quaternion rotation, float fieldOfViewDegrees, float aspect, float nearClipPlane, float farClipPlane)
				: this((float3)position, (quaternion)rotation, fieldOfViewDegrees, aspect, nearClipPlane, farClipPlane)
			{
			}

			public Frustum(float3 position, quaternion rotation, Angle fieldOfView, float aspect, float nearClipPlane, float farClipPlane)
			{
				float4x4 a = float4x4.TRS(position, rotation, new float3(1));
				for (int i = 0; i < 3; i++)
				{
					a.c2[i] = 0f - a.c2[i];
				}
				Matrix = math.mul(a, math.inverse(float4x4.PerspectiveFov(fieldOfView.Radians, aspect, nearClipPlane, farClipPlane)));
			}

			public Frustum(Vector3 position, Quaternion rotation, Angle fieldOfView, float aspect, float nearClipPlane, float farClipPlane)
				: this((float3)position, (quaternion)rotation, fieldOfView, aspect, nearClipPlane, farClipPlane)
			{
			}
		}

		public readonly struct FieldOfView : IDrawable
		{
			public readonly float3 Position;

			public readonly quaternion Rotation;

			public readonly Angle HorizontalAngle;

			public readonly Angle VerticalAngle;

			public readonly float Distance;

			public FieldOfView(float3 position, quaternion rotation, Angle horizontalAngle, Angle verticalAngle, float distance)
			{
				Position = position;
				Rotation = rotation;
				HorizontalAngle = horizontalAngle;
				VerticalAngle = verticalAngle;
				Distance = distance;
			}

			public FieldOfView(Vector3 position, Quaternion rotation, Angle horizontalAngle, Angle verticalAngle, float distance)
				: this((float3)position, (quaternion)rotation, horizontalAngle, verticalAngle, distance)
			{
			}

			public static Angle VerticalFieldOfViewWithAspectToHorizontalFieldOfView(Angle verticalFieldOfView, float aspect)
			{
				return Angle.FromRadians(2f * math.atan(math.tan(verticalFieldOfView.Radians * 0.5f) * aspect));
			}
		}

		public readonly struct Pyramid : IDrawable
		{
			public readonly float3 PointBase;

			public readonly quaternion Rotation;

			public readonly float3 Size;

			public Pyramid(float3 pointBase, quaternion rotation, float3 size)
			{
				PointBase = pointBase;
				Rotation = rotation;
				Size = size;
			}

			public Pyramid(Vector3 pointBase, Quaternion rotation, Vector3 size)
				: this((float3)pointBase, (quaternion)rotation, (float3)size)
			{
			}

			public Pyramid Flip()
			{
				return new Pyramid(PointBase + math.mul(Rotation, new float3(0f, 0f, Size.z)), math.mul(Rotation, quaternion.AxisAngle(new float3(0f, 1f, 0f), 180f)), Size);
			}
		}

		public readonly struct Plane : IDrawable
		{
			public readonly UnityEngine.Plane Value;

			public readonly float3 PointOnPlane;

			public readonly float2 DisplaySize;

			[PublicAPI]
			public Plane(UnityEngine.Plane plane, float3 pointOnPlane, float2 displaySize)
			{
				Value = plane;
				PointOnPlane = plane.ClosestPointOnPlane(pointOnPlane);
				DisplaySize = displaySize;
			}

			[PublicAPI]
			public Plane(UnityEngine.Plane plane, Vector3 pointOnPlane, Vector2 displaySize)
				: this(plane, (float3)pointOnPlane, (float2)displaySize)
			{
			}

			[PublicAPI]
			public Plane(UnityEngine.Plane plane, float3 pointOnPlane)
			{
				Value = plane;
				PointOnPlane = plane.ClosestPointOnPlane(pointOnPlane);
				DisplaySize = new float2(2f, 2f);
			}

			[PublicAPI]
			public Plane(UnityEngine.Plane plane, Vector3 pointOnPlane)
				: this(plane, (float3)pointOnPlane)
			{
			}

			[PublicAPI]
			public static float3 Intersection(UnityEngine.Plane a, UnityEngine.Plane b, UnityEngine.Plane c)
			{
				float num = math.dot(a.normal, math.cross(b.normal, c.normal));
				if (Math.Abs(num) < 0.0001f)
				{
					return float3.zero;
				}
				return (a.distance * math.cross(b.normal, c.normal) + b.distance * math.cross(c.normal, a.normal) + c.distance * math.cross(a.normal, b.normal)) / (0f - num);
			}
		}

		public readonly struct Catenary : IDrawable
		{
			public readonly float3 A;

			public readonly float3 B;

			public readonly float Length;

			private readonly float _a;

			private readonly float _p;

			private readonly float _q;

			private readonly bool _straightLine;

			public Catenary(float3 a, float3 b, float length)
			{
				if (!(b.y < a.y))
				{
					float3 a2 = a;
					float3 b2 = b;
					A = a2;
					B = b2;
				}
				else
				{
					float3 b2 = b;
					float3 a2 = a;
					A = b2;
					B = a2;
				}
				Length = length;
				_straightLine = !TryCalculateCatenaryArgs(A, B, Length, out (float, float, float) args);
				(_a, _p, _q) = args;
			}

			public Catenary(Vector3 a, Vector3 b, float length)
				: this((float3)a, (float3)b, length)
			{
			}

			private static bool TryCalculateCatenaryArgs(float3 p0, float3 p1, float length, out (float a, float p, float q) args)
			{
				float3 x = p1 - p0;
				if (math.length(x) >= length)
				{
					args = default((float, float, float));
					return false;
				}
				float num = math.length(x.xz);
				float y = x.y;
				float num2 = math.sqrt(length * length - y * y);
				if (num == 0f)
				{
					args = default((float, float, float));
					return false;
				}
				float num3 = 0f;
				float num4 = 1f;
				int i;
				for (i = 0; i < 32; i++)
				{
					if (!(num2 < 2f * num4 * math.sinh(num / (2f * num4))))
					{
						break;
					}
					num3 = num4;
					num4 *= 2f;
				}
				i += 16;
				float num5 = 0f;
				while (i > 0)
				{
					i--;
					num5 = (num3 + num4) * 0.5f;
					if (num2 < 2f * num5 * math.sinh(num / (2f * num5)))
					{
						num3 = num5;
					}
					else
					{
						num4 = num5;
					}
				}
				float item = (num - num5 * math.log((length + y) / (length - y))) / 2f;
				float item2 = (y - length * (1f / math.tanh(num / (2f * num5)))) / 2f;
				args = (a: num5, p: item, q: item2);
				return true;
			}

			public float3 Evaluate(float t)
			{
				if (_straightLine)
				{
					return math.lerp(A, B, t);
				}
				float3 float5 = B - A;
				float num = math.length(float5.xz);
				float num2 = (_a * asinh(t * Length / _a - math.sinh(_p / _a)) + _p) / num;
				float y = _a * math.cosh((num2 * num - _p) / _a) + _q;
				return A + new float3(float5.x * num2, y, float5.z * num2);
				static float asinh(float x)
				{
					return math.log(x + math.sqrt(x * x + 1f));
				}
			}
		}

		internal readonly struct Outline : IDrawable
		{
			public readonly float3 A;

			public readonly float3 B;

			public readonly float3 C;

			public Outline(float3 a, float3 b, float radius)
			{
				A = a;
				B = b;
				C = new float3(radius, 0f, 0f);
			}

			public Outline(float3 a, float3 b, float radiusA, float radiusB)
			{
				A = a;
				B = b;
				C = new float3(radiusA, radiusB, 0f);
			}

			public Outline(float3 a, float3 b, float3 c)
			{
				A = a;
				B = b;
				C = c;
			}
		}

		internal readonly struct Cast : IDrawable
		{
			public readonly float4x4 Matrix;

			public readonly float3 Vector;

			public Cast(float4x4 matrix, float3 vector)
			{
				Matrix = matrix;
				Vector = vector;
			}
		}

		public readonly struct MeshNormals : IDrawable
		{
			public readonly Mesh Mesh;

			public readonly Matrix4x4 Matrix;

			public readonly float ArrowLength;

			public MeshNormals(Mesh mesh, Matrix4x4 matrix4X4, float arrowLength)
			{
				Mesh = mesh;
				Matrix = matrix4X4;
				ArrowLength = arrowLength;
			}

			public MeshNormals(Mesh mesh, Transform transform, float arrowLength)
				: this(mesh, transform.localToWorldMatrix, arrowLength)
			{
			}
		}

		public readonly struct Angle : IEquatable<Angle>
		{
			public float Turns { get; }

			public float Radians => Turns * MathF.PI * 2f;

			public float Degrees => Turns * 360f;

			private Angle(float turns)
			{
				Turns = turns;
			}

			public static Angle FromRadians(float value)
			{
				return new Angle(value / (MathF.PI * 2f));
			}

			public static Angle FromDegrees(float value)
			{
				return FromRadians(value * (MathF.PI / 180f));
			}

			public static Angle FromTurns(float value)
			{
				return new Angle(value);
			}

			public static Angle FromArcLength(float length, float radius)
			{
				if (radius == 0f || length == 0f)
				{
					return default(Angle);
				}
				return FromRadians(length / radius);
			}

			internal Angle Abs()
			{
				return FromTurns(math.abs(Turns));
			}

			public static implicit operator float(Angle value)
			{
				return value.Turns;
			}

			public bool Equals(Angle other)
			{
				return Turns.Equals(other.Turns);
			}

			public override bool Equals(object obj)
			{
				if (obj is Angle other)
				{
					return Equals(other);
				}
				return false;
			}

			public override int GetHashCode()
			{
				return Turns.GetHashCode();
			}
		}

		[Flags]
		public enum DrawModifications
		{
			None = 0,
			AlphaFade = 1,
			NormalFade = 2,
			FaceCamera = 4,
			Custom = 8,
			Custom2 = 0x10,
			Custom3 = 0x20
		}

		[Flags]
		public enum Axes : byte
		{
			None = 0,
			X = 1,
			Y = 2,
			Z = 4,
			TwoDimensional = 3,
			All = 7
		}

		public static class BoxUtility
		{
			[Flags]
			public enum Direction : byte
			{
				None = 0,
				Top = 1,
				Right = 2,
				Forward = 4,
				Bottom = 8,
				Left = 0x10,
				Back = 0x20
			}

			public readonly struct Point
			{
				public readonly float3 Coordinate;

				public readonly Direction Direction;

				public Point(float3 coordinate, Direction direction)
				{
					Coordinate = coordinate;
					Direction = direction;
				}
			}

			public readonly struct Edge
			{
				public readonly float3 A;

				public readonly float3 B;

				public readonly Direction Direction;

				public Edge(float3 a, float3 b, Direction direction)
				{
					A = a;
					B = b;
					Direction = direction;
				}
			}

			public static readonly Point[] Points = new Point[8]
			{
				new Point(new float3(-1f, -1f, -1f), Direction.Bottom | Direction.Left | Direction.Back),
				new Point(new float3(1f, -1f, -1f), Direction.Right | Direction.Bottom | Direction.Back),
				new Point(new float3(-1f, 1f, -1f), Direction.Top | Direction.Left | Direction.Back),
				new Point(new float3(1f, 1f, -1f), Direction.Top | Direction.Right | Direction.Back),
				new Point(new float3(-1f, -1f, 1f), Direction.Forward | Direction.Bottom | Direction.Left),
				new Point(new float3(1f, -1f, 1f), Direction.Right | Direction.Forward | Direction.Bottom),
				new Point(new float3(-1f, 1f, 1f), Direction.Top | Direction.Forward | Direction.Left),
				new Point(new float3(1f, 1f, 1f), Direction.Top | Direction.Right | Direction.Forward)
			};

			public static readonly Edge[] Edges = new Edge[12]
			{
				new Edge(new float3(-1f, -1f, -1f), new float3(1f, -1f, -1f), Direction.Bottom | Direction.Back),
				new Edge(new float3(-1f, -1f, -1f), new float3(-1f, 1f, -1f), Direction.Left | Direction.Back),
				new Edge(new float3(-1f, -1f, -1f), new float3(-1f, -1f, 1f), Direction.Bottom | Direction.Left),
				new Edge(new float3(-1f, -1f, 1f), new float3(-1f, 1f, 1f), Direction.Forward | Direction.Left),
				new Edge(new float3(-1f, -1f, 1f), new float3(1f, -1f, 1f), Direction.Forward | Direction.Bottom),
				new Edge(new float3(-1f, 1f, -1f), new float3(1f, 1f, -1f), Direction.Top | Direction.Back),
				new Edge(new float3(-1f, 1f, -1f), new float3(-1f, 1f, 1f), Direction.Top | Direction.Left),
				new Edge(new float3(1f, -1f, -1f), new float3(1f, 1f, -1f), Direction.Right | Direction.Back),
				new Edge(new float3(1f, -1f, -1f), new float3(1f, -1f, 1f), Direction.Right | Direction.Bottom),
				new Edge(new float3(1f, 1f, 1f), new float3(1f, 1f, -1f), Direction.Top | Direction.Right),
				new Edge(new float3(1f, 1f, 1f), new float3(1f, -1f, 1f), Direction.Right | Direction.Forward),
				new Edge(new float3(1f, 1f, 1f), new float3(-1f, 1f, 1f), Direction.Top | Direction.Forward)
			};

			public static Direction ConstructDirection(float dotRight, float dotUp, float dotForward)
			{
				Direction direction = Direction.None;
				if (dotRight < -1E-05f)
				{
					direction |= Direction.Left;
				}
				else if (dotRight > 1E-05f)
				{
					direction |= Direction.Right;
				}
				if (dotUp < -1E-05f)
				{
					direction |= Direction.Bottom;
				}
				else if (dotUp > 1E-05f)
				{
					direction |= Direction.Top;
				}
				if (dotForward < -1E-05f)
				{
					direction |= Direction.Back;
				}
				else if (dotForward > 1E-05f)
				{
					direction |= Direction.Forward;
				}
				return direction;
			}

			public static int CountDirections(Direction direction)
			{
				int num = 0;
				for (int i = 1; i <= 32; i++)
				{
					if (((uint)direction & (uint)(byte)(1 << i)) != 0)
					{
						num++;
					}
				}
				return num;
			}
		}

		public readonly struct Text : IDrawableCastManaged, IDrawableManaged
		{
			public readonly Vector3 Position;

			public readonly object Value;

			public readonly Camera Camera;

			public Text(Vector3 position, object value, Camera camera = null)
			{
				Camera = camera;
				Position = position;
				Value = value;
			}
		}

		[Flags]
		public enum View : byte
		{
			None = 0,
			Game = 1,
			Scene = 2,
			All = 3
		}

		public readonly struct ScreenText : IDrawableCastManaged, IDrawableManaged
		{
			public readonly object Value;

			public readonly UnityEngine.Object Context;

			public readonly View ActiveViews;

			public ScreenText(object value, UnityEngine.Object context = null, View activeViews = View.All)
			{
				ActiveViews = activeViews;
				Value = value;
				Context = context;
			}
		}

		public static readonly Color XColor;

		public static readonly Color YColor;

		public static readonly Color ZColor;

		public static readonly Color HitColor;

		public static readonly Color CastColor;

		public static readonly Color EnterColor;

		public static readonly Color StayColor;

		public static readonly Color ExitColor;

		private const int MaxDrawDistance = 1000000;

		internal static float4 ToFloat4(this Color color)
		{
			return new float4(color.r, color.g, color.b, color.a);
		}

		internal static float2 xy(this Vector3 value)
		{
			return new float2(value.x, value.y);
		}

		internal static float3 xy0(this float2 value)
		{
			return new float3(value.x, value.y, 0f);
		}

		internal static float3 xy0(this Vector2 value)
		{
			return new float3(value.x, value.y, 0f);
		}

		internal static float3 xyz(this Vector3Int value)
		{
			return new float3(value.x, value.y, value.z);
		}

		internal static float2 xy(this Vector2Int value)
		{
			return new float2(value.x, value.y);
		}

		private static float4 GetRow(this float4x4 matrix, int row)
		{
			return new float4(matrix.c0[row], matrix.c1[row], matrix.c2[row], matrix.c3[row]);
		}

		private static void SetRow(this float4x4 matrix, int row, float4 value)
		{
			matrix.c0[row] = value[0];
			matrix.c1[row] = value[1];
			matrix.c2[row] = value[2];
			matrix.c3[row] = value[3];
		}

		internal static float3 MultiplyPoint3x4(this float4x4 matrix, float3 point)
		{
			float4 c = matrix.c0;
			float4 c2 = matrix.c1;
			float4 c3 = matrix.c2;
			float4 c4 = matrix.c3;
			float3 result = default(float3);
			result.x = c[0] * point.x + c2[0] * point.y + c3[0] * point.z + c4[0];
			result.y = c[1] * point.x + c2[1] * point.y + c3[1] * point.z + c4[1];
			result.z = c[2] * point.x + c2[2] * point.y + c3[2] * point.z + c4[2];
			return result;
		}

		internal static float3 MultiplyVector(this float4x4 matrix, float3 vector)
		{
			float4 c = matrix.c0;
			float4 c2 = matrix.c1;
			float4 c3 = matrix.c2;
			float3 result = default(float3);
			result.x = c[0] * vector.x + c2[0] * vector.y + c3[0] * vector.z;
			result.y = c[1] * vector.x + c2[1] * vector.y + c3[1] * vector.z;
			result.z = c[2] * vector.x + c2[2] * vector.y + c3[2] * vector.z;
			return result;
		}

		private static void Translate(this ref float4x4 matrix, float3 translation)
		{
			float4 c = matrix.c3;
			c.x += translation.x;
			c.y += translation.y;
			c.z += translation.z;
			matrix.c3 = c;
		}

		private static void TranslateAndSetZ(this ref float4x4 matrix, float2 translation, float z)
		{
			float4 c = matrix.c3;
			c.x += translation.x;
			c.y += translation.y;
			c.z = z;
			matrix.c3 = c;
		}

		private static void SetZ(this ref float4x4 matrix, float z)
		{
			float4 c = matrix.c3;
			c.z = z;
			matrix.c3 = c;
		}

		private static Line GetWithZ(this in Line line, float z)
		{
			return new Line(new float3(line.A.x, line.A.y, z), new float3(line.B.x, line.B.y, z));
		}

		private static Box2D GetTranslated(this in Box2D box2D, float3 translation)
		{
			float4x4 matrix = box2D.Matrix;
			Translate(ref matrix, translation);
			return new Box2D(matrix);
		}

		private static Box2D GetTranslatedWithZ(this in Box2D box2D, float2 translation, float z)
		{
			float4x4 matrix = box2D.Matrix;
			matrix.TranslateAndSetZ(translation, z);
			return new Box2D(matrix);
		}

		private static Box2D GetWithZ(this in Box2D box2D, float z)
		{
			float4x4 matrix = box2D.Matrix;
			matrix.SetZ(z);
			return new Box2D(matrix);
		}

		private static Arc GetWithZ(this in Arc arc, float z)
		{
			float4x4 matrix = arc.Matrix;
			matrix.SetZ(z);
			return new Arc(matrix);
		}

		private static Capsule2D GetTranslated(this in Capsule2D capsule2D, float2 translation)
		{
			return new Capsule2D(new float3(capsule2D._pointA.x + translation.x, capsule2D._pointA.y + translation.y, capsule2D._pointA.z), new float3(capsule2D._pointB.x + translation.x, capsule2D._pointB.y + translation.y, capsule2D._pointB.z), capsule2D._radius, capsule2D._verticalDirection, capsule2D._scaledLeft);
		}

		private static Capsule2D GetTranslatedWithZ(this in Capsule2D capsule2D, float2 translation, float zOverride)
		{
			return new Capsule2D(new float3(capsule2D._pointA.x + translation.x, capsule2D._pointA.y + translation.y, zOverride), new float3(capsule2D._pointB.x + translation.x, capsule2D._pointB.y + translation.y, zOverride), capsule2D._radius, capsule2D._verticalDirection, capsule2D._scaledLeft);
		}

		private static Capsule2D GetWithZ(this in Capsule2D capsule2D, float zOverride)
		{
			return new Capsule2D(new float3(capsule2D._pointA.x, capsule2D._pointA.y, zOverride), new float3(capsule2D._pointB.x, capsule2D._pointB.y, zOverride), capsule2D._radius, capsule2D._verticalDirection, capsule2D._scaledLeft);
		}

		static Shape()
		{
			XColor = Constants.XColor;
			YColor = Constants.YColor;
			ZColor = Constants.ZColor;
			HitColor = Constants.HitColor;
			CastColor = Constants.CastColor;
			EnterColor = Constants.EnterColor;
			StayColor = Constants.StayColor;
			ExitColor = Constants.ExitColor;
		}

		private static float3 GetValidPerpendicular(float3 input)
		{
			float3 y = math.right();
			if ((double)math.abs(math.dot(input, y)) > 0.95)
			{
				y = math.up();
			}
			return math.normalizesafe(math.cross(input, y));
		}

		private static float3 GetValidAxisAligned(float3 normal)
		{
			float3 float5 = new float3(0f, 0f, 1f);
			if (math.abs(math.dot(normal, float5)) > 0.707f)
			{
				float5 = new float3(0f, 1f, 0f);
			}
			return float5;
		}

		private static void EnsureNormalized(this ref float3 vector3, out float length)
		{
			float num = math.lengthsq(vector3);
			if (Mathf.Approximately(num, 1f))
			{
				length = 1f;
				return;
			}
			length = math.sqrt(num);
			vector3 /= length;
		}

		private static void EnsureNormalized(this ref float2 vector2)
		{
			float num = math.lengthsq(vector2);
			if (!Mathf.Approximately(num, 1f) && !Mathf.Approximately(num, 0f))
			{
				vector2 /= math.sqrt(num);
			}
		}

		private static void EnsureNormalized(this ref float2 vector2, out float length)
		{
			float num = math.lengthsq(vector2);
			if (Mathf.Approximately(num, 1f))
			{
				length = 1f;
				return;
			}
			if (Mathf.Approximately(num, 0f))
			{
				length = 0f;
				return;
			}
			length = math.sqrt(num);
			vector2 /= length;
		}

		private static void EnsureNormalized(this ref float3 vector3)
		{
			float num = math.lengthsq(vector3);
			if (!Mathf.Approximately(num, 1f) && !Mathf.Approximately(num, 0f))
			{
				vector3 /= math.sqrt(num);
			}
		}

		private static float GetClampedMaxDistance(float distance)
		{
			if (float.IsInfinity(distance))
			{
				return 1000000f;
			}
			return math.min(distance, 1000000f);
		}

		private static void GetRotationCoefficients(Angle angle, out float s, out float c)
		{
			float radians = angle.Radians;
			s = math.sin(radians);
			c = math.cos(radians);
		}

		private static float2 RotateUsingCoefficients(float2 vector, float s, float c)
		{
			float x = vector.x * c - vector.y * s;
			float y = vector.x * s + vector.y * c;
			return new float2(x, y);
		}

		private static float2 Rotate(float2 vector, Angle angle)
		{
			GetRotationCoefficients(angle, out var s, out var c);
			return RotateUsingCoefficients(vector, s, c);
		}

		private static float3 RotateUsingCoefficients(float3 vector, float s, float c)
		{
			float x = vector.x * c - vector.y * s;
			float y = vector.x * s + vector.y * c;
			return new float3(x, y, vector.z);
		}

		private static float3 Rotate(float3 vector, Angle angle)
		{
			GetRotationCoefficients(angle, out var s, out var c);
			return RotateUsingCoefficients(vector, s, c);
		}

		private static float2 GetDirectionFromAngle(Angle angle)
		{
			if ((float)angle != 0f)
			{
				return Rotate(new float2(1f, 0f), angle);
			}
			return new float2(1f, 0f);
		}

		private static float ToAngleDegrees(this float2 v)
		{
			return math.atan2(v.y, v.x) * 57.29578f;
		}

		private static float2 PerpendicularClockwise(float2 vector2)
		{
			return new float2(vector2.y, 0f - vector2.x);
		}

		private static float2 PerpendicularCounterClockwise(float2 vector2)
		{
			return new float2(0f - vector2.y, vector2.x);
		}

		private static float3 PerpendicularClockwise(float3 vector3)
		{
			return new float3(vector3.y, 0f - vector3.x, vector3.z);
		}

		private static float3 PerpendicularCounterClockwise(float3 vector3)
		{
			return new float3(0f - vector3.y, vector3.x, vector3.z);
		}

		private static float3 Add(this float3 a, float2 b)
		{
			return new float3(a.x + b.x, a.y + b.y, a.z);
		}

		private static bool HasZeroDistanceHit(RaycastHit[] results, int resultCount)
		{
			for (int i = 0; i < resultCount; i++)
			{
				if (results[i].distance == 0f)
				{
					return true;
				}
			}
			return false;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static bool IsWhite(Color c)
		{
			if (c.r == 1f && c.g == 1f && c.b == 1f)
			{
				return c.a == 1f;
			}
			return false;
		}
	}
}

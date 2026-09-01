using System;

namespace Poly.Collide
{
	[Serializable]
	public class CollisionDispatcherImpl
	{
		public delegate void CollisionHandler(ref HandlerInput input, ref HandlerOutput output);

		public enum BuildMotion : byte
		{
			None = 0,
			ForShapeA = 1,
			ForShapeB = 2
		}

		[Serializable]
		public struct HandlerInfo
		{
			public CollisionHandler handler;

			public EntityTypes entityTypes;

			public bool isReversed;

			public bool isIgnored;

			public HandlerInfo(CollisionHandler handler, EntityTypes entityTypes, bool isReversed = false, bool isIgnored = false)
			{
				this.handler = handler;
				this.entityTypes = entityTypes;
				this.isReversed = isReversed;
				this.isIgnored = isIgnored;
			}
		}

		public HandlerInfo[,] handlers;

		public CollisionHandler[] handlersOne;

		public void Init()
		{
			int num = 3;
			handlers = new HandlerInfo[num, num];
			RegisterHandler(Shape.Type.Polygon, Shape.Type.Polygon, EntityTypes.BodyBody, CollidePolygon.Collide_PolygonPolygon);
			RegisterHandler(Shape.Type.Polygon, Shape.Type.Segment, EntityTypes.BodyEdge, CollidePolygon.Collide_PolygonSegment);
			RegisterHandler(Shape.Type.Polygon, Shape.Type.Circle, EntityTypes.BodyNode, CollidePolygon.Collide_PolygonCircle);
			RegisterHandler(Shape.Type.Segment, Shape.Type.Circle, EntityTypes.EdgeNode, ProcessCollision.Collide_SegmentCircle);
			NotHandling(Shape.Type.Segment, Shape.Type.Segment);
			NotHandling(Shape.Type.Circle, Shape.Type.Circle);
		}

		public void RegisterHandler(Shape.Type a, Shape.Type b, EntityTypes entityTypes, CollisionHandler handler = null)
		{
			HandlerInfo handlerInfo = new HandlerInfo(handler, entityTypes);
			handlers[(int)a, (int)b] = handlerInfo;
			if (a != b)
			{
				handlerInfo.isReversed = true;
				handlers[(int)b, (int)a] = handlerInfo;
			}
		}

		public void NotHandling(Shape.Type a, Shape.Type b)
		{
			HandlerInfo handlerInfo = new HandlerInfo
			{
				isIgnored = true
			};
			handlers[(int)a, (int)b] = handlerInfo;
			handlers[(int)b, (int)a] = handlerInfo;
		}
	}
}

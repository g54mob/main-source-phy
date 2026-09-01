using Poly.Base;
using Poly.Collide;
using Poly.Draw;
using Poly.Extension;
using Poly.Math;
using Poly.Solver;
using UnityEngine;

namespace Poly.Physics.Viewers
{
	public class ContactPointViewer : SingletonBehaviour<ContactPointViewer>
	{
		public bool drawNormalArrows = true;

		public bool drawLineBetweenLocalContacts = true;

		public bool drawContactPointCrosses = true;

		public bool drawImpulse = true;

		public bool drawFrictionImpulses;

		public float impulseScale = 10f;

		public bool hidePositionBasedImpulsePart;

		public bool drawRefSurfaceDistance;

		public bool drawReferencePositions;

		public bool drawFeatures;

		public void Update()
		{
		}

		public static void Draw(FastList<CollisionInfo> infos)
		{
			if (SingletonBehaviour<ContactPointViewer>.instanceExists && SingletonBehaviour<ContactPointViewer>.instance.enabled && (bool)Singleton<GlDrawer, int>.instance)
			{
				for (int i = 0; i < infos.Count; i++)
				{
					SingletonBehaviour<ContactPointViewer>.instance.Draw(ref infos.array[i]);
				}
			}
		}

		private void Draw(ref CollisionInfo info)
		{
			Vec3 vec = Vec3.back * 0.01f;
			if (drawNormalArrows)
			{
				GlDrawer.color = ColorEx.blueCobalt;
				GlDrawer.DrawArrow(vec + info.contactPoint0, info.normal * 0.5f);
			}
			if (drawLineBetweenLocalContacts)
			{
				GlDrawer.DrawLine(vec + info.contactPoint0, vec + info.contactPoint1, Color.green);
			}
			if (drawContactPointCrosses)
			{
				vec += Vec3.back * 0.01f;
				GlDrawer.color = Color.white;
				GlDrawer.DrawCross(vec + info.contactPoint0, 0.2f);
				vec += Vec3.back * 0.01f;
				GlDrawer.color = Color.red;
				GlDrawer.DrawCross(vec + info.contactPoint1, 0.2f);
			}
			if (drawImpulse && (info.sumVelImpulses_InFrame != 0f || info.sumFullImpulses_InFrame != 0f || info.sumFrictionImpulses_InFrame != 0f))
			{
				GlDrawer.color = Color.magenta;
				float num = (hidePositionBasedImpulsePart ? info.sumVelImpulses_InFrame : info.sumFullImpulses_InFrame);
				GlDrawer.DrawArrow(vec + info.contactPoint0, info.normal * num * SingletonBehaviour<ContactPointViewer>.instance.impulseScale);
				if (drawFrictionImpulses)
				{
					GlDrawer.DrawArrow(vec + info.contactPoint0, info.tangent_slow * info.sumFrictionImpulses_InFrame * SingletonBehaviour<ContactPointViewer>.instance.impulseScale);
				}
			}
			if (drawRefSurfaceDistance)
			{
				ContactPointCache obj = ((info.featureIdxInCache == 0) ? info.cacheValue.pointCache0 : info.cacheValue.pointCache1);
				float persistent_refSurfaceDistance = obj.persistent_refSurfaceDistance;
				GlDrawer.color = ColorEx.yellow;
				Vec3 vec2 = vec + info.contactPoint0 + info.normal * 0.01f;
				Vec3 vec3 = vec2 + info.tangent_slow * persistent_refSurfaceDistance;
				GlDrawer.DrawLine(vec2, vec3);
				GlDrawer.DrawCross(vec2, 0.05f, 45f);
				GlDrawer.DrawCross(vec3, 0.05f, 45f);
			}
			if (drawReferencePositions)
			{
				float num2 = 0.1f;
				GlDrawer.color = ColorEx.orange;
				_ = info.featureIdxInCache;
				_ = 1;
				Debug_Slow_GetTransforms(in info, out var tA, out var tB);
				GlDrawer.color = ColorEx.green;
				GlDrawer.DrawCircle(tA.position, num2 * 1.5f);
				GlDrawer.DrawCircle(tB.position, num2 * 1.5f);
				GlDrawer.color = ColorEx.green;
				GlDrawer.DrawCircle(tA.position, num2 * 1.5f);
				GlDrawer.DrawCircle(tB.position, num2 * 1.5f);
				Debug_Slow_GetComTransforms(in info, out tA, out tB);
				GlDrawer.color = ColorEx.white;
				GlDrawer.DrawCircle(tA.position, num2 * 1.1f);
				GlDrawer.DrawCircle(tB.position, num2 * 1.1f);
			}
			if (drawFeatures)
			{
				Debug_Slow_GetTransforms(in info, out var tA2, out var tB2);
				Feature feature = ((info.featureIdxInCache == 0) ? info.cacheValue.feature0 : info.cacheValue.feature1);
				ref ShapeHandle reference = ref World.shapeHandleArray[info.shapeHandleIdx0];
				ref ShapeHandle reference2 = ref World.shapeHandleArray[info.shapeHandleIdx1];
				Shape shape = reference.shape;
				Shape shape2 = reference2.shape;
				Vec2 vec4 = ((PolygonShape)shape).verts[feature.vert0];
				Vec2 vec5 = ((PolygonShape)shape2).verts[feature.vert2];
				vec4 = tA2 * vec4;
				vec5 = tB2 * vec5;
				Vec2 vec6 = Vec2.zero;
				bool flag = true;
				switch (feature.type)
				{
				case Feature.Type.PointEdge:
					vec6 = ((PolygonShape)shape2).verts[feature.vert1];
					vec6 = tB2 * vec6;
					flag = false;
					break;
				case Feature.Type.EdgePoint:
					vec6 = ((PolygonShape)shape).verts[feature.vert1];
					vec6 = tA2 * vec6;
					break;
				}
				GlDrawer.color = ColorEx.yellow;
				GlDrawer.DrawWireSquareXY(vec4, Vec2.one * 0.1f);
				if (flag)
				{
					GlDrawer.color = ColorEx.yellow;
					GlDrawer.DrawWireSquareXY(vec6, Vec2.one * 0.1f);
					GlDrawer.DrawLine(vec4, vec6);
				}
				else
				{
					GlDrawer.color = ColorEx.orangeTangerine;
					GlDrawer.DrawWireSquareXY(vec6, Vec2.one * 0.08f);
					GlDrawer.DrawLine(vec6, vec5);
				}
				GlDrawer.color = ColorEx.orangeTangerine;
				GlDrawer.DrawWireSquareXY(vec5, Vec2.one * 0.08f);
			}
		}

		private static void Debug_Slow_GetTransforms(in CollisionInfo info, out Transform2 tA, out Transform2 tB)
		{
			World world = SingletonBehaviour<World>.instance;
			int count = world.bodies.Count;
			switch (info.entityTypes)
			{
			case EntityTypes.BodyBody:
				tA = world.bodies[info.motionIdx0].t2;
				tB = world.bodies[info.motionIdx1].t2;
				break;
			case EntityTypes.BodyEdge:
			{
				tA = world.bodies[info.motionIdx0].t2;
				int num3 = (short)world.edgesWithMotions[info.motionIdx1 - count].shapeHandleIndex;
				tB = World.shapeHandleArray[num3].t2;
				break;
			}
			case EntityTypes.BodyNode:
				tA = world.bodies[info.motionIdx0].t2;
				tB.position = world.nodeHandles[info.nodeIdx1].pos;
				tB.rotation = Rotation2.identity;
				break;
			case EntityTypes.EdgeEdge:
			{
				int num = (short)world.edgesWithMotions[info.motionIdx0 - count].shapeHandleIndex;
				tA = World.shapeHandleArray[num].t2;
				int num2 = (short)world.edgesWithMotions[info.motionIdx1 - count].shapeHandleIndex;
				tB = World.shapeHandleArray[num2].t2;
				break;
			}
			case EntityTypes.EdgeNode:
			{
				int num = (short)world.edgesWithMotions[info.motionIdx0 - count].shapeHandleIndex;
				tA = World.shapeHandleArray[num].t2;
				tB.position = world.nodeHandles[info.nodeIdx1].pos;
				tB.rotation = Rotation2.identity;
				break;
			}
			case EntityTypes.NodeNode:
				tA.position = world.nodeHandles[info.nodeIdx0].pos;
				tA.rotation = Rotation2.identity;
				tB.position = world.nodeHandles[info.nodeIdx1].pos;
				tB.rotation = Rotation2.identity;
				break;
			default:
				Debug.LogWarning("Reference contact point display not handled for this collision type: " + info.entityTypes);
				tA.position = (tB.position = Vec2.zero);
				tA.rotation = (tB.rotation = Rotation2.identity);
				break;
			}
		}

		private static void Debug_Slow_GetComTransforms(in CollisionInfo info, out Transform2 tA, out Transform2 tB)
		{
			Debug_Slow_GetTransforms(in info, out tA, out tB);
			World world = SingletonBehaviour<World>.instance;
			int count = world.bodies.Count;
			switch (info.entityTypes)
			{
			case EntityTypes.BodyBody:
				tA.position = world.bodies[info.motionIdx0].motion.com;
				tB.position = world.bodies[info.motionIdx1].motion.com;
				break;
			case EntityTypes.BodyEdge:
				tA.position = world.bodies[info.motionIdx0].motion.com;
				tB.position = world.edgesWithMotions[info.motionIdx1 - count].optional_motion.com;
				break;
			case EntityTypes.BodyNode:
				tA.position = world.bodies[info.motionIdx0].motion.com;
				break;
			case EntityTypes.EdgeEdge:
				tA.position = world.edgesWithMotions[info.motionIdx0 - count].optional_motion.com;
				tB.position = world.edgesWithMotions[info.motionIdx1 - count].optional_motion.com;
				break;
			case EntityTypes.EdgeNode:
				tA.position = world.edgesWithMotions[info.motionIdx0 - count].optional_motion.com;
				break;
			case EntityTypes.NodeNode:
				break;
			}
		}
	}
}

using Poly.Base;
using Poly.Draw;
using Poly.Extension;
using Poly.Math;
using UnityEngine;

namespace Poly.Physics.Viewers
{
	public class JointViewer : WorldListener
	{
		public bool showJointIndices;

		public override void AfterWorldFixedUpdate()
		{
			if (!Singleton<GlDrawer, int>.instance)
			{
				return;
			}
			foreach (WheelJoint joint in SingletonBehaviour<World>.instance.joints)
			{
				Draw(joint, showJointIndices);
			}
			foreach (WheelJoint customShapeJoint in SingletonBehaviour<World>.instance.customShapeJoints)
			{
				Draw(customShapeJoint, showJointIndices);
			}
		}

		private static void Draw(WheelJoint joint, bool showJointIndices)
		{
			float num = joint.body.motion.angle * 57.29578f;
			Transform2 t = joint.body.t2;
			Transform2 t2 = joint.connectedBody.t2;
			Vec2 vec = t * joint.anchor;
			Vec2 vec2 = t2 * joint.connectedAnchor;
			Vec2 vec3 = t.rotation * joint.prismaticAxis;
			if (joint.enablePrismaticMovement)
			{
				Vec2 vec4 = joint.prismaticLimits;
				Vec2 vec5 = vec + vec3 * vec4.x;
				Vec2 vec6 = vec + vec3 * vec4.y;
				GlDrawer.color = (joint.isBroken ? Color.red : Color.green);
				GlDrawer.DrawLine(vec5, vec6);
				GlDrawer.DrawCross(vec, 0.08f);
				GlDrawer.DrawCross(vec5, 0.04f, num + 45f);
				GlDrawer.DrawCross(vec6, 0.04f, num + 45f);
				GlDrawer.color = Color.yellow;
				GlDrawer.DrawCircle(vec2, 0.02f);
				if (joint.isBroken)
				{
					GlDrawer.color = ColorEx.lightGray * ColorEx.alphaHalf;
					GlDrawer.DrawLine(vec, vec2);
				}
			}
			else
			{
				GlDrawer.color = Color.green;
				GlDrawer.DrawCircle(vec, 0.1f);
				GlDrawer.color = Color.yellow;
				GlDrawer.DrawCircle(vec2, 0.1f);
			}
			if (showJointIndices)
			{
				GlDrawer.DrawLabel(vec + vec3.rotated90 * 0.05f, joint.worldIdx.ToString(), Color.white);
			}
		}
	}
}

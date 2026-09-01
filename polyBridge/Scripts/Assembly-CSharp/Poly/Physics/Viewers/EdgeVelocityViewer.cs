using Poly.Base;
using UnityEngine;

namespace Poly.Physics.Viewers
{
	public class EdgeVelocityViewer : MonoBehaviour
	{
		public Font font;

		private void OnGUI()
		{
			Color gray = Color.gray;
			gray.a = 0.3f;
			DrawGuiTextUtil.InitGuiStyle(font, gray);
			World instance = SingletonBehaviour<World>.instance;
			foreach (EdgeHandle edgeHandle in instance.edgeHandles)
			{
				Vec2 vec = edgeHandle.node0.solverNode.vel + edgeHandle.node1.solverNode.vel;
				vec *= 0.5f / instance.settings.deltaTimeForVelocityEdge;
				if (vec.magnitude > 0.1f)
				{
					Vec2 vec2 = 0.5f * (edgeHandle.node0.pos + edgeHandle.node1.pos) + 0.3f * Vec2.up;
					DrawGuiTextUtil.DisplayGuiLabel_Slow(text: $"vel: {vec.magnitude:0.}", posInWorld: vec2);
				}
			}
		}
	}
}

using Poly.Collide;
using Poly.Solver;

namespace Poly.Physics
{
	public interface ICollisionListener
	{
		void OnPolyCollisionEnter(in CollisionEvent e);

		void OnPolyCollisionStay(in CollisionEvent e);

		void OnPolyCollisionExit(ShapeHandleIndex a, ShapeHandleIndex b, ReceivingHandle receivingHandle, in CollisionCache cache);

		void VerifyReset();

		void OnPolyCollisionProcess_Internal(in CollisionEvent ePartial, ref CollisionInfo info);
	}
}

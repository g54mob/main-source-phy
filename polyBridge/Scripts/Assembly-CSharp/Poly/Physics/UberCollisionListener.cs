using System.Collections.Generic;
using Poly.Collide;
using Poly.Solver;
using UnityEngine;

namespace Poly.Physics
{
	public class UberCollisionListener : MonoBehaviour, ICollisionListener
	{
		public List<ICollisionListener> listeners = new List<ICollisionListener>();

		private static UberCollisionListener _instance;

		public static bool enableVisualization;

		public static UberCollisionListener instance => _instance ?? (_instance = Object.FindObjectOfType<UberCollisionListener>());

		public void OnPolyCollisionEnter(in CollisionEvent e)
		{
			foreach (ICollisionListener listener in listeners)
			{
				listener.OnPolyCollisionEnter(in e);
			}
		}

		public void OnPolyCollisionStay(in CollisionEvent e)
		{
			foreach (ICollisionListener listener in listeners)
			{
				listener.OnPolyCollisionStay(in e);
			}
		}

		public void OnPolyCollisionExit(ShapeHandleIndex a, ShapeHandleIndex b, ReceivingHandle receivingHandle, in CollisionCache cache)
		{
			foreach (ICollisionListener listener in listeners)
			{
				listener.OnPolyCollisionExit(a, b, receivingHandle, in cache);
			}
		}

		public void VerifyReset()
		{
			foreach (ICollisionListener listener in listeners)
			{
				listener.VerifyReset();
			}
		}

		public void OnPolyCollisionProcess_Internal(in CollisionEvent ePartial, ref CollisionInfo info)
		{
		}

		void ICollisionListener.OnPolyCollisionEnter(in CollisionEvent e)
		{
			OnPolyCollisionEnter(in e);
		}

		void ICollisionListener.OnPolyCollisionStay(in CollisionEvent e)
		{
			OnPolyCollisionStay(in e);
		}

		void ICollisionListener.OnPolyCollisionExit(ShapeHandleIndex a, ShapeHandleIndex b, ReceivingHandle receivingHandle, in CollisionCache cache)
		{
			OnPolyCollisionExit(a, b, receivingHandle, in cache);
		}

		void ICollisionListener.OnPolyCollisionProcess_Internal(in CollisionEvent ePartial, ref CollisionInfo info)
		{
			OnPolyCollisionProcess_Internal(in ePartial, ref info);
		}
	}
}

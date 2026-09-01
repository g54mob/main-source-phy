using Poly.Collide;
using Poly.Solver;
using UnityEngine;

namespace Poly.Physics
{
	public class MinimalCollisionListener : MonoBehaviour, ICollisionListener
	{
		public void OnPolyCollisionEnter(in CollisionEvent e)
		{
			e.GetContactID();
			ContactData contactData = ContactData.CreateFromEvent(in e);
			Debug.Log("Collision enter: " + contactData.receivingObject.name + " and " + contactData.otherObject.name);
		}

		public void OnPolyCollisionStay(in CollisionEvent e)
		{
			e.GetContactID();
			ContactData contactData = ContactData.CreateFromEvent(in e);
			Debug.Log("Collision stay: " + contactData.receivingObject.name + " and " + contactData.otherObject.name);
		}

		public void OnPolyCollisionExit(ShapeHandleIndex a, ShapeHandleIndex b, ReceivingHandle receivingHandle, in CollisionCache cache)
		{
			CollisionEvent.GetContactID(a, b, receivingHandle);
			GameObject[] obj = new GameObject[2]
			{
				a.Get().GetUnityComponent().gameObject,
				b.Get().GetUnityComponent().gameObject
			};
			Transform transform = obj[(uint)receivingHandle].transform;
			Transform transform2 = obj[(uint)(1 - receivingHandle)].transform;
			Debug.Log("Collision exit: " + transform.name + " and " + transform2.name);
		}

		public void VerifyReset()
		{
		}

		public void OnPolyCollisionProcess_Internal(in CollisionEvent ePartial, ref CollisionInfo info)
		{
		}

		private void OnEnable()
		{
			UberCollisionListener.instance.listeners.Add(this);
		}

		private void OnDisable()
		{
			UberCollisionListener.instance.listeners.Remove(this);
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

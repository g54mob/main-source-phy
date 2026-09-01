using Poly.Base;
using Poly.Collide;
using Poly.Physics;
using Poly.Solver;
using UnityEngine;

namespace Poly.Game
{
	[RequireComponent(typeof(Poly.Physics.Rigidbody))]
	public class AudioCollisionListener_Unused : PolyBehaviour, ICollisionListener
	{
		public enum VehicleAudioSize
		{
			small = 0,
			medium = 1,
			large = 2
		}

		public enum CollisionType
		{
			tire = 0,
			body = 1
		}

		public VehicleAudioSize VehicleSize;

		public CollisionType Type;

		public float impactVelocityThreshold = 0.3f;

		public GameObject ImpactMarkerPrefab;

		public bool Debug;

		private Poly.Physics.Rigidbody body;

		private void Awake()
		{
			body = GetComponent<Poly.Physics.Rigidbody>();
		}

		public void OnPolyCollisionEnter(in CollisionEvent e)
		{
			ContactData data = ContactData.CreateFromEvent(in e);
			ProcessContactData(ref data, in e);
		}

		private bool ProcessContactData(ref ContactData data, in CollisionEvent e)
		{
			bool result = false;
			for (int i = 0; i < 2; i++)
			{
				ref readonly ContactPointInfo reference = ref e.point0;
				if (i == 1)
				{
					reference = ref e.point1;
				}
				bool flag = e.numPoints > i && (double)reference.distance < 0.01;
				bool flag2 = data[i] != null;
				if (flag ^ flag2)
				{
					if (!flag)
					{
						Object.Destroy(((Transform)data[i]).gameObject);
						data[i] = null;
					}
					result = true;
					flag2 = data[i] != null;
				}
				float num = Vec2.Dot(in reference.normal, in reference.relativePointVelocityBeforeCollision);
				if (e.numPoints > i && reference.isNewImpact && num < 0f - impactVelocityThreshold && Debug)
				{
					Object.Instantiate(ImpactMarkerPrefab, reference.position, Quaternion.identity, SingletonBehaviour<World>.instance.transform).transform.localPosition = reference.position;
				}
			}
			return result;
		}

		private void OnEnable()
		{
			body.collisionListeners.Add(this);
		}

		private void OnDisable()
		{
			body.collisionListeners.Remove(this);
		}

		public void OnPolyCollisionExit(ShapeHandleIndex a, ShapeHandleIndex b, ReceivingHandle receivingHandle, in CollisionCache cache)
		{
		}

		public void OnPolyCollisionStay(in CollisionEvent e)
		{
			ContactData data = ContactData.CreateFromEvent(in e);
			ProcessContactData(ref data, in e);
		}

		public void VerifyReset()
		{
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

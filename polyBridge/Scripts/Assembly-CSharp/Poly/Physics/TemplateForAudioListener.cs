using System.Collections.Generic;
using Poly.Base;
using Poly.Collide;
using Poly.Extension;
using Poly.Solver;
using UnityEngine;

namespace Poly.Physics
{
	public class TemplateForAudioListener : MonoBehaviour, ICollisionListener
	{
		public float contactDistanceThreshold = 0.01f;

		public float impactVelocityThreshold = 0.3f;

		public float slidingVelocityThreshold = 0.1f;

		public bool createDebugObjects;

		public GameObject contactMarkerPrefab;

		public GameObject impactMarkerPrefab;

		public Material restingContactMaterial;

		public Material slidingContactMaterial;

		public bool trackNormals;

		protected Dictionary<int, ContactData> datas = new Dictionary<int, ContactData>();

		private static TemplateForAudioListener _instance;

		public static TemplateForAudioListener instance => _instance ?? (_instance = Object.FindObjectOfType<TemplateForAudioListener>());

		public virtual bool OnImpact(ref ContactData data, int pointIdx, in ContactPointInfo point)
		{
			if (createDebugObjects)
			{
				Object.Instantiate(impactMarkerPrefab, point.position, Quaternion.identity, SingletonBehaviour<World>.instance.transform).transform.localPosition = point.position;
			}
			return false;
		}

		public virtual void OnTouchingPointEnter(ref ContactData data, int pointIdx, in ContactPointInfo point)
		{
			if (createDebugObjects)
			{
				data[pointIdx] = Object.Instantiate(contactMarkerPrefab, SingletonBehaviour<World>.instance.transform).transform;
			}
			OnTouchingPointStay(ref data, pointIdx, in point);
		}

		public virtual bool OnTouchingPointStay(ref ContactData data, int pointIdx, in ContactPointInfo point)
		{
			if (data[pointIdx] != null)
			{
				Transform obj = (Transform)data[pointIdx];
				obj.localPosition = point.position;
				bool flag = slidingVelocityThreshold < point.tangentVelocity;
				obj.GetComponent<MeshRenderer>().sharedMaterial = (flag ? slidingContactMaterial : restingContactMaterial);
			}
			if (trackNormals)
			{
				data.SetNormal(pointIdx, point.normal, point.distance);
			}
			return trackNormals;
		}

		public virtual void OnTouchingPointExit(ref ContactData data, int pointIdx)
		{
			Transform transform = (Transform)data[pointIdx];
			if ((bool)transform && (bool)transform.gameObject)
			{
				Object.Destroy(transform.gameObject);
			}
			data[pointIdx] = null;
			if (trackNormals)
			{
				data.SetNormal(pointIdx, Vec2.zero, 1.7014117E+38f);
			}
		}

		public void OnPolyCollisionEnter(in CollisionEvent e)
		{
			Vec2Short contactID = e.GetContactID();
			ContactData value = ContactData.CreateFromEvent(in e);
			if (!datas.ContainsKey(contactID.key))
			{
				datas.Add(contactID.key, value);
				OnPolyCollisionStay(in e);
			}
		}

		public void OnPolyCollisionStay(in CollisionEvent e)
		{
			Vec2Short contactID = e.GetContactID();
			if (datas.TryGetValue(contactID.key, out var value) && ProcessContactData(ref value, in e))
			{
				datas[contactID.key] = value;
			}
		}

		public void OnPolyCollisionExit(ShapeHandleIndex a, ShapeHandleIndex b, ReceivingHandle receivingHandle, in CollisionCache cache)
		{
			Vec2Short contactID = CollisionEvent.GetContactID(a, b, receivingHandle);
			if (datas.TryGetValue(contactID.key, out var value))
			{
				datas.Remove(contactID.key);
				ClearContactData_NoWriteBack(value);
			}
		}

		public void VerifyReset()
		{
			Clear();
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
			Clear();
		}

		private bool ProcessContactData(ref ContactData data, in CollisionEvent e)
		{
			bool flag = false;
			for (int i = 0; i < 2; i++)
			{
				ref readonly ContactPointInfo reference = ref e.point0;
				ref bool reference2 = ref data.isInTouch0;
				if (i == 1)
				{
					reference = ref e.point1;
					reference2 = ref data.isInTouch1;
				}
				bool flag2 = reference2;
				reference2 = i < e.numPoints && reference.distance < contactDistanceThreshold;
				if (reference2 ^ flag2)
				{
					if (reference2)
					{
						OnTouchingPointEnter(ref data, i, in reference);
					}
					else
					{
						OnTouchingPointExit(ref data, i);
					}
					flag = true;
				}
				else if (reference2)
				{
					flag |= OnTouchingPointStay(ref data, i, in reference);
				}
				float num = Vec2.Dot(in reference.normal, in reference.relativePointVelocityBeforeCollision);
				if (i < e.numPoints && reference.isNewImpact && num < 0f - impactVelocityThreshold)
				{
					flag |= OnImpact(ref data, i, in reference);
				}
			}
			return flag;
		}

		private void ClearContactData_NoWriteBack(ContactData data)
		{
			if (data.isInTouch0)
			{
				OnTouchingPointExit(ref data, 0);
			}
			if (data.isInTouch1)
			{
				OnTouchingPointExit(ref data, 1);
			}
			if (data.userData0 != null && (bool)(Transform)data.userData0)
			{
				Object.Destroy(((Transform)data.userData0).gameObject);
			}
			if (data.userData1 != null && (bool)(Transform)data.userData1)
			{
				Object.Destroy(((Transform)data.userData1).gameObject);
			}
		}

		protected virtual void Clear()
		{
			foreach (KeyValuePair<int, ContactData> data in datas)
			{
				ClearContactData_NoWriteBack(data.Value);
			}
			datas.Clear();
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

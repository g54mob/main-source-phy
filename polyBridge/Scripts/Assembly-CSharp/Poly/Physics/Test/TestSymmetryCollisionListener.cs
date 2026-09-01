using System.Collections.Generic;
using Poly.Base;
using Poly.Collide;
using Poly.Extension;
using Poly.Solver;
using UnityEngine;

namespace Poly.Physics.Test
{
	public class TestSymmetryCollisionListener : MonoBehaviour, ICollisionListener
	{
		public bool createObjects = true;

		public bool alwaysEnableVisualization;

		public GameObject contactMarkerPrefab;

		public GameObject impactMarkerPrefab;

		public Material restingContactMaterial;

		public Material slidingContactMaterial;

		public float contactDistanceThreshold = 0.01f;

		public float impactVelocityThreshold = 0.1f;

		private Dictionary<Vec2Short, ContactData> contactData = new Dictionary<Vec2Short, ContactData>();

		private static TestSymmetryCollisionListener _instance;

		public bool isEmpty => contactData.Count == 0;

		private bool enableVisualization => UberCollisionListener.enableVisualization;

		public static TestSymmetryCollisionListener instance => _instance ?? (_instance = Object.FindObjectOfType<TestSymmetryCollisionListener>());

		public void OnPolyCollisionEnter(in CollisionEvent e)
		{
			Vec2Short contactID = e.GetContactID();
			if (contactData.ContainsKey(contactID))
			{
				ContactData value = contactData[contactID];
				value.debug_extraRef++;
				contactData[contactID] = value;
			}
			else
			{
				contactData.Add(contactID, ContactData.CreateFromEvent(in e));
			}
			OnPolyCollisionStay(in e);
		}

		public void OnPolyCollisionStay(in CollisionEvent e)
		{
			Vec2Short contactID = e.GetContactID();
			if (!contactData.ContainsKey(contactID))
			{
				return;
			}
			ContactData value = contactData[contactID];
			if ((e.numPoints > 0 && e.point0.distance < contactDistanceThreshold) ^ (value.marker0 != null))
			{
				if (value.marker0 == null)
				{
					if (createObjects && (enableVisualization || alwaysEnableVisualization))
					{
						value.marker0 = Object.Instantiate(contactMarkerPrefab, SingletonBehaviour<World>.instance.transform).transform;
					}
				}
				else
				{
					Object.Destroy(value.marker0.gameObject);
					value.marker0 = null;
				}
				contactData[contactID] = value;
			}
			if ((e.numPoints > 1 && e.point1.distance < contactDistanceThreshold) ^ (value.marker1 != null))
			{
				if (value.marker1 == null)
				{
					if (createObjects && (enableVisualization || alwaysEnableVisualization))
					{
						value.marker1 = Object.Instantiate(contactMarkerPrefab, SingletonBehaviour<World>.instance.transform).transform;
					}
				}
				else
				{
					Object.Destroy(value.marker1.gameObject);
					value.marker1 = null;
				}
				contactData[contactID] = value;
			}
			if ((bool)value.marker0)
			{
				value.marker0.localPosition = e.point0.position;
				bool flag = 0.1f < e.point0.tangentVelocity;
				value.marker0.GetComponent<MeshRenderer>().sharedMaterial = (flag ? slidingContactMaterial : restingContactMaterial);
			}
			if ((bool)value.marker1)
			{
				value.marker1.localPosition = e.point1.position;
				bool flag2 = 0.1f < e.point1.tangentVelocity;
				value.marker1.GetComponent<MeshRenderer>().sharedMaterial = (flag2 ? slidingContactMaterial : restingContactMaterial);
			}
			if (e.point0.isNewImpact && Vec2.Dot(in e.point0.normal, in e.point0.relativePointVelocityBeforeCollision) < 0f - impactVelocityThreshold && createObjects && (enableVisualization || alwaysEnableVisualization))
			{
				Object.Instantiate(impactMarkerPrefab, e.point0.position, Quaternion.identity, SingletonBehaviour<World>.instance.transform).transform.localPosition = e.point0.position;
			}
			if (e.numPoints == 2 && e.point1.isNewImpact && Vec2.Dot(in e.point1.normal, in e.point1.relativePointVelocityBeforeCollision) < 0f - impactVelocityThreshold && createObjects && (enableVisualization || alwaysEnableVisualization))
			{
				Object.Instantiate(impactMarkerPrefab, e.point1.position, Quaternion.identity, SingletonBehaviour<World>.instance.transform).transform.localPosition = e.point0.position;
			}
		}

		public void OnPolyCollisionExit(ShapeHandleIndex a, ShapeHandleIndex b, ReceivingHandle receivingHandle, in CollisionCache cache)
		{
			Vec2Short contactID = CollisionEvent.GetContactID(a, b, receivingHandle);
			if (contactData.ContainsKey(contactID))
			{
				ContactData value = contactData[contactID];
				if ((bool)value.marker0)
				{
					Object.Destroy(value.marker0.gameObject);
					value.marker0 = null;
				}
				if ((bool)value.marker1)
				{
					Object.Destroy(value.marker1.gameObject);
					value.marker1 = null;
				}
				if (value.debug_extraRef > 0)
				{
					value.debug_extraRef--;
					contactData[contactID] = value;
				}
				else
				{
					contactData.Remove(contactID);
				}
			}
		}

		public void VerifyReset()
		{
			Clear();
		}

		public void OnPolyCollisionProcess_Internal(in CollisionEvent ePartial, ref CollisionInfo info)
		{
		}

		public void Clear()
		{
			foreach (ContactData value in contactData.Values)
			{
				if ((bool)value.marker0)
				{
					Object.Destroy(value.marker0.gameObject);
				}
				if ((bool)value.marker1)
				{
					Object.Destroy(value.marker1.gameObject);
				}
			}
			contactData.Clear();
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

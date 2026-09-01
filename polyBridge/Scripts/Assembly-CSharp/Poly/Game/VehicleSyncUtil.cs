using System;
using System.Reflection;
using UnityEngine;

namespace Poly.Game
{
	public static class VehicleSyncUtil
	{
		public static Transform SplitVehicleSyncTargetsIntoVisualAndPhysics(Transform vehicleRoot)
		{
			Transform transform = new GameObject("[Runtime Triggers]").transform;
			transform.AlignAndSetAsChildOf(vehicleRoot);
			VehicleSyncTarget[] componentsInChildren = vehicleRoot.GetComponentsInChildren<VehicleSyncTarget>();
			foreach (VehicleSyncTarget vehicleSyncTarget in componentsInChildren)
			{
				Transform transform2 = new GameObject(vehicleSyncTarget.name + "[Target: auto-generated]").transform;
				transform2.AlignAndSetAsChildOf(vehicleSyncTarget.transform);
				transform2.parent = transform;
				VehicleSyncTarget vehicleSyncTarget2 = transform2.gameObject.CloneComponent(vehicleSyncTarget);
				vehicleSyncTarget.m_type = VehicleSyncTarget.Type.VisualMesh;
				vehicleSyncTarget2.m_type = VehicleSyncTarget.Type.GameplayTrigger;
				vehicleSyncTarget2.gameObject.layer = vehicleSyncTarget.gameObject.layer;
				Collider[] componentsInChildren2 = vehicleSyncTarget.GetComponentsInChildren<Collider>();
				foreach (Collider collider in componentsInChildren2)
				{
					VehicleSyncTarget componentInParent = collider.GetComponentInParent<VehicleSyncTarget>();
					if (vehicleSyncTarget == componentInParent)
					{
						if (collider.transform == vehicleSyncTarget.transform)
						{
							transform2.gameObject.CloneComponent(collider);
						}
						else
						{
							Transform transform3 = new GameObject(collider.name + "[Collider: auto-generated]").transform;
							transform3.AlignAndSetAsChildOf(collider.transform);
							transform3.parent = transform2;
							transform3.gameObject.CloneComponent(collider);
							transform3.gameObject.layer = collider.gameObject.layer;
						}
						UnityEngine.Object.Destroy(collider);
					}
				}
			}
			return transform;
		}

		public static void AlignAndSetAsChildOf(this Transform t, Transform parent)
		{
			t.parent = parent;
			t.localPosition = Vector3.zero;
			t.localRotation = Quaternion.identity;
			t.localScale = Vector3.one;
		}

		public static T CloneComponent<T>(this GameObject destination, T original) where T : Component
		{
			Type type = original.GetType();
			Component component = destination.AddComponent(type);
			BindingFlags bindingAttr = BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
			FieldInfo[] fields = type.GetFields(bindingAttr);
			foreach (FieldInfo fieldInfo in fields)
			{
				fieldInfo.SetValue(component, fieldInfo.GetValue(original));
			}
			PropertyInfo[] properties = type.GetProperties(bindingAttr);
			foreach (PropertyInfo propertyInfo in properties)
			{
				if (propertyInfo.CanWrite && propertyInfo.Name != "name")
				{
					try
					{
						propertyInfo.SetValue(component, propertyInfo.GetValue(original, null), null);
					}
					catch
					{
					}
				}
			}
			return component as T;
		}
	}
}

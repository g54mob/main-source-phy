using System;
using Poly.Math;
using Poly.Physics;
using UnityEngine;

public class VehicleSyncTarget : MonoBehaviour
{
	public enum Type
	{
		Invalid = 0,
		GameplayTrigger = 1,
		VisualMesh = 2
	}

	public VehicleSyncPart m_VehicleSyncPart;

	[NonSerialized]
	public Transform m_Source;

	public Type m_type;

	private Vector3 m_DefaultLocalPosition;

	private Quaternion m_DefaultLocalRotation;

	public void SaveDefaultTransform()
	{
		m_DefaultLocalPosition = base.transform.localPosition;
		m_DefaultLocalRotation = base.transform.localRotation;
	}

	public void RestoreDefaultTransform()
	{
		base.transform.localPosition = m_DefaultLocalPosition;
		base.transform.localRotation = m_DefaultLocalRotation;
	}

	public void Sync(bool interpolate, bool rotate180 = false)
	{
		Quaternion quaternion = (rotate180 ? Quaternion.AngleAxis(180f, Vector3.up) : Quaternion.identity);
		if ((bool)m_Source)
		{
			Poly.Physics.Rigidbody component = m_Source.GetComponent<Poly.Physics.Rigidbody>();
			if ((bool)component)
			{
				Transform3 transform = (interpolate ? component.interpolatedTransform : component.discreteTransform);
				base.transform.position = transform.position;
				base.transform.rotation = transform.rotation * quaternion;
			}
			else
			{
				base.transform.position = m_Source.position;
				base.transform.rotation = m_Source.rotation * quaternion;
			}
		}
	}
}

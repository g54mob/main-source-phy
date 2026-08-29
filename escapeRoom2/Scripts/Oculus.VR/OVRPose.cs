using System;
using UnityEngine;

[Serializable]
public struct OVRPose
{
	public Vector3 position;

	public Quaternion orientation;

	public static OVRPose identity => new OVRPose
	{
		position = Vector3.zero,
		orientation = Quaternion.identity
	};

	public override bool Equals(object obj)
	{
		if (obj is OVRPose)
		{
			return this == (OVRPose)obj;
		}
		return false;
	}

	public override int GetHashCode()
	{
		return position.GetHashCode() ^ orientation.GetHashCode();
	}

	public static bool operator ==(OVRPose x, OVRPose y)
	{
		if (x.position == y.position)
		{
			return x.orientation == y.orientation;
		}
		return false;
	}

	public static bool operator !=(OVRPose x, OVRPose y)
	{
		return !(x == y);
	}

	public static OVRPose operator *(OVRPose lhs, OVRPose rhs)
	{
		return new OVRPose
		{
			position = lhs.position + lhs.orientation * rhs.position,
			orientation = lhs.orientation * rhs.orientation
		};
	}

	public OVRPose Inverse()
	{
		OVRPose result = default(OVRPose);
		result.orientation = Quaternion.Inverse(orientation);
		result.position = result.orientation * -position;
		return result;
	}

	public OVRPose flipZ()
	{
		OVRPose result = this;
		result.position.z = 0f - result.position.z;
		result.orientation.z = 0f - result.orientation.z;
		result.orientation.w = 0f - result.orientation.w;
		return result;
	}

	public OVRPlugin.Posef ToPosef_Legacy()
	{
		return new OVRPlugin.Posef
		{
			Position = position.ToVector3f(),
			Orientation = orientation.ToQuatf()
		};
	}

	public OVRPlugin.Posef ToPosef()
	{
		return new OVRPlugin.Posef
		{
			Position = 
			{
				x = position.x,
				y = position.y,
				z = 0f - position.z
			},
			Orientation = 
			{
				x = 0f - orientation.x,
				y = 0f - orientation.y,
				z = orientation.z,
				w = orientation.w
			}
		};
	}

	public OVRPose Rotate180AlongX()
	{
		OVRPose result = this;
		result.orientation *= Quaternion.Euler(180f, 0f, 0f);
		return result;
	}
}

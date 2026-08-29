using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using OVR.OpenVR;
using Unity.Collections;
using UnityEngine;
using UnityEngine.XR;

public static class OVRExtensions
{
	public static OVRPose ToTrackingSpacePose(this Transform transform, Camera camera)
	{
		OVRPose identity = OVRPose.identity;
		if (OVRNodeStateProperties.GetNodeStatePropertyVector3(XRNode.Head, NodeStatePropertyType.Position, OVRPlugin.Node.Head, OVRPlugin.Step.Render, out var retVec))
		{
			identity.position = retVec;
		}
		if (OVRNodeStateProperties.GetNodeStatePropertyQuaternion(XRNode.Head, NodeStatePropertyType.Orientation, OVRPlugin.Node.Head, OVRPlugin.Step.Render, out var retQuat))
		{
			identity.orientation = retQuat;
		}
		return identity * transform.ToHeadSpacePose(camera);
	}

	[Obsolete("ToWorldSpacePose should be invoked with an explicit mainCamera parameter")]
	public static OVRPose ToWorldSpacePose(this OVRPose trackingSpacePose)
	{
		return trackingSpacePose.ToWorldSpacePose(Camera.main);
	}

	public static OVRPose ToWorldSpacePose(this OVRPose trackingSpacePose, Camera mainCamera)
	{
		OVRPose oVRPose = trackingSpacePose.ToHeadSpacePose();
		Matrix4x4 localToWorldMatrix = mainCamera.transform.localToWorldMatrix;
		Matrix4x4 matrix4x = Matrix4x4.TRS(oVRPose.position, oVRPose.orientation, Vector3.one);
		Matrix4x4 matrix4x2 = localToWorldMatrix * matrix4x;
		return new OVRPose
		{
			position = matrix4x2.GetColumn(3),
			orientation = matrix4x2.rotation
		};
	}

	public static OVRPose ToHeadSpacePose(this OVRPose trackingSpacePose)
	{
		OVRPose identity = OVRPose.identity;
		if (OVRNodeStateProperties.GetNodeStatePropertyVector3(XRNode.Head, NodeStatePropertyType.Position, OVRPlugin.Node.Head, OVRPlugin.Step.Render, out var retVec))
		{
			identity.position = retVec;
		}
		if (OVRNodeStateProperties.GetNodeStatePropertyQuaternion(XRNode.Head, NodeStatePropertyType.Orientation, OVRPlugin.Node.Head, OVRPlugin.Step.Render, out var retQuat))
		{
			identity.orientation = retQuat;
		}
		return identity.Inverse() * trackingSpacePose;
	}

	public static OVRPose ToHeadSpacePose(this Transform transform, Camera camera)
	{
		Quaternion orientation = Quaternion.Inverse(camera.transform.rotation) * transform.rotation;
		Vector4 column = (camera.transform.worldToLocalMatrix * transform.localToWorldMatrix).GetColumn(3);
		return new OVRPose
		{
			orientation = orientation,
			position = column
		};
	}

	public static OVRPose ToOVRPose(this Transform t, bool isLocal = false)
	{
		OVRPose result = default(OVRPose);
		result.orientation = (isLocal ? t.localRotation : t.rotation);
		result.position = (isLocal ? t.localPosition : t.position);
		return result;
	}

	public static void FromOVRPose(this Transform t, OVRPose pose, bool isLocal = false)
	{
		if (isLocal)
		{
			t.localRotation = pose.orientation;
			t.localPosition = pose.position;
		}
		else
		{
			t.rotation = pose.orientation;
			t.position = pose.position;
		}
	}

	public static OVRPose ToOVRPose(this OVRPlugin.Posef p)
	{
		return new OVRPose
		{
			position = new Vector3(p.Position.x, p.Position.y, 0f - p.Position.z),
			orientation = new Quaternion(0f - p.Orientation.x, 0f - p.Orientation.y, p.Orientation.z, p.Orientation.w)
		};
	}

	public static OVRTracker.Frustum ToFrustum(this OVRPlugin.Frustumf f)
	{
		return new OVRTracker.Frustum
		{
			nearZ = f.zNear,
			farZ = f.zFar,
			fov = new Vector2
			{
				x = 57.29578f * f.fovX,
				y = 57.29578f * f.fovY
			}
		};
	}

	public static Color FromColorf(this OVRPlugin.Colorf c)
	{
		return new Color
		{
			r = c.r,
			g = c.g,
			b = c.b,
			a = c.a
		};
	}

	public static OVRPlugin.Colorf ToColorf(this Color c)
	{
		return new OVRPlugin.Colorf
		{
			r = c.r,
			g = c.g,
			b = c.b,
			a = c.a
		};
	}

	public static Vector2 FromSizef(this OVRPlugin.Sizef v)
	{
		return new Vector2
		{
			x = v.w,
			y = v.h
		};
	}

	public static OVRPlugin.Sizef ToSizef(this Vector2 v)
	{
		return new OVRPlugin.Sizef
		{
			w = v.x,
			h = v.y
		};
	}

	public static Vector2 FromVector2f(this OVRPlugin.Vector2f v)
	{
		return new Vector2
		{
			x = v.x,
			y = v.y
		};
	}

	public static Vector2 FromFlippedXVector2f(this OVRPlugin.Vector2f v)
	{
		return new Vector2
		{
			x = 0f - v.x,
			y = v.y
		};
	}

	public static OVRPlugin.Vector2f ToVector2f(this Vector2 v)
	{
		return new OVRPlugin.Vector2f
		{
			x = v.x,
			y = v.y
		};
	}

	public static Vector3 FromSize3f(this OVRPlugin.Size3f v)
	{
		return new Vector3
		{
			x = v.w,
			y = v.h,
			z = v.d
		};
	}

	public static OVRPlugin.Size3f ToSize3f(this Vector3 v)
	{
		return new OVRPlugin.Size3f
		{
			w = v.x,
			h = v.y,
			d = v.z
		};
	}

	public static Vector3 FromVector3f(this OVRPlugin.Vector3f v)
	{
		return new Vector3
		{
			x = v.x,
			y = v.y,
			z = v.z
		};
	}

	public static Vector3 FromFlippedXVector3f(this OVRPlugin.Vector3f v)
	{
		return new Vector3
		{
			x = 0f - v.x,
			y = v.y,
			z = v.z
		};
	}

	public static Vector3 FromFlippedZVector3f(this OVRPlugin.Vector3f v)
	{
		return new Vector3
		{
			x = v.x,
			y = v.y,
			z = 0f - v.z
		};
	}

	public static OVRPlugin.Vector3f ToVector3f(this Vector3 v)
	{
		return new OVRPlugin.Vector3f
		{
			x = v.x,
			y = v.y,
			z = v.z
		};
	}

	public static OVRPlugin.Vector3f ToFlippedXVector3f(this Vector3 v)
	{
		return new OVRPlugin.Vector3f
		{
			x = 0f - v.x,
			y = v.y,
			z = v.z
		};
	}

	public static OVRPlugin.Vector3f ToFlippedZVector3f(this Vector3 v)
	{
		return new OVRPlugin.Vector3f
		{
			x = v.x,
			y = v.y,
			z = 0f - v.z
		};
	}

	public static Vector4 FromVector4f(this OVRPlugin.Vector4f v)
	{
		return new Vector4
		{
			x = v.x,
			y = v.y,
			z = v.z,
			w = v.w
		};
	}

	public static OVRPlugin.Vector4f ToVector4f(this Vector4 v)
	{
		return new OVRPlugin.Vector4f
		{
			x = v.x,
			y = v.y,
			z = v.z,
			w = v.w
		};
	}

	public static Quaternion FromQuatf(this OVRPlugin.Quatf q)
	{
		return new Quaternion
		{
			x = q.x,
			y = q.y,
			z = q.z,
			w = q.w
		};
	}

	public static Quaternion FromFlippedXQuatf(this OVRPlugin.Quatf q)
	{
		return new Quaternion
		{
			x = q.x,
			y = 0f - q.y,
			z = 0f - q.z,
			w = q.w
		};
	}

	public static Quaternion FromFlippedZQuatf(this OVRPlugin.Quatf q)
	{
		return new Quaternion
		{
			x = 0f - q.x,
			y = 0f - q.y,
			z = q.z,
			w = q.w
		};
	}

	public static OVRPlugin.Quatf ToQuatf(this Quaternion q)
	{
		return new OVRPlugin.Quatf
		{
			x = q.x,
			y = q.y,
			z = q.z,
			w = q.w
		};
	}

	public static OVRPlugin.Quatf ToFlippedXQuatf(this Quaternion q)
	{
		return new OVRPlugin.Quatf
		{
			x = q.x,
			y = 0f - q.y,
			z = 0f - q.z,
			w = q.w
		};
	}

	public static OVRPlugin.Quatf ToFlippedZQuatf(this Quaternion q)
	{
		return new OVRPlugin.Quatf
		{
			x = 0f - q.x,
			y = 0f - q.y,
			z = q.z,
			w = q.w
		};
	}

	public static HmdMatrix34_t ConvertToHMDMatrix34(this Matrix4x4 m)
	{
		return new HmdMatrix34_t
		{
			m0 = m[0, 0],
			m1 = m[0, 1],
			m2 = 0f - m[0, 2],
			m3 = m[0, 3],
			m4 = m[1, 0],
			m5 = m[1, 1],
			m6 = 0f - m[1, 2],
			m7 = m[1, 3],
			m8 = 0f - m[2, 0],
			m9 = 0f - m[2, 1],
			m10 = m[2, 2],
			m11 = 0f - m[2, 3]
		};
	}

	public static Transform FindChildRecursive(this Transform parent, string name)
	{
		for (int i = 0; i < parent.childCount; i++)
		{
			Transform child = parent.GetChild(i);
			if (child.name.Contains(name))
			{
				return child;
			}
			Transform transform = child.FindChildRecursive(name);
			if (transform != null)
			{
				return transform;
			}
		}
		return null;
	}

	public static bool Equals(this Gradient gradient, Gradient otherGradient)
	{
		if (gradient.colorKeys.Length != otherGradient.colorKeys.Length || gradient.alphaKeys.Length != otherGradient.alphaKeys.Length)
		{
			return false;
		}
		for (int i = 0; i < gradient.colorKeys.Length; i++)
		{
			GradientColorKey gradientColorKey = gradient.colorKeys[i];
			GradientColorKey gradientColorKey2 = otherGradient.colorKeys[i];
			if (gradientColorKey.color != gradientColorKey2.color || gradientColorKey.time != gradientColorKey2.time)
			{
				return false;
			}
		}
		for (int j = 0; j < gradient.alphaKeys.Length; j++)
		{
			GradientAlphaKey gradientAlphaKey = gradient.alphaKeys[j];
			GradientAlphaKey gradientAlphaKey2 = otherGradient.alphaKeys[j];
			if (gradientAlphaKey.alpha != gradientAlphaKey2.alpha || gradientAlphaKey.time != gradientAlphaKey2.time)
			{
				return false;
			}
		}
		return true;
	}

	public static void CopyFrom(this Gradient gradient, Gradient otherGradient)
	{
		GradientColorKey[] array = new GradientColorKey[otherGradient.colorKeys.Length];
		for (int i = 0; i < array.Length; i++)
		{
			Color color = otherGradient.colorKeys[i].color;
			array[i].color = new Color(color.r, color.g, color.b, color.a);
			array[i].time = otherGradient.colorKeys[i].time;
		}
		GradientAlphaKey[] array2 = new GradientAlphaKey[otherGradient.alphaKeys.Length];
		for (int j = 0; j < array2.Length; j++)
		{
			array2[j].alpha = otherGradient.alphaKeys[j].alpha;
			array2[j].time = otherGradient.alphaKeys[j].time;
		}
		gradient.SetKeys(array, array2);
	}

	[Obsolete("Anchor APIs that specify a storage location are obsolete.")]
	internal static OVRPlugin.SpaceStorageLocation ToSpaceStorageLocation(this OVRSpace.StorageLocation storageLocation)
	{
		return storageLocation switch
		{
			OVRSpace.StorageLocation.Local => OVRPlugin.SpaceStorageLocation.Local, 
			OVRSpace.StorageLocation.Cloud => OVRPlugin.SpaceStorageLocation.Cloud, 
			_ => throw new NotSupportedException(string.Format("{0} is not a supported {1}", storageLocation, "SpaceStorageLocation")), 
		};
	}

	internal static OVREnumerable<T> ToNonAlloc<T>([NoEnumeration] this IEnumerable<T> enumerable)
	{
		return new OVREnumerable<T>(enumerable);
	}

	internal static NativeArray<T> ToNativeArray<T>(this IEnumerable<T> enumerable, Allocator allocator) where T : struct
	{
		if (enumerable == null)
		{
			throw new ArgumentNullException("enumerable");
		}
		if (!(enumerable is T[] array))
		{
			if (!(enumerable is IReadOnlyList<T> readOnlyList))
			{
				if (!(enumerable is HashSet<T> hashSet))
				{
					if (!(enumerable is Queue<T> queue))
					{
						if (!(enumerable is IReadOnlyCollection<T> readOnlyCollection))
						{
							if (enumerable is ICollection<T> collection)
							{
								NativeArray<T> result = new NativeArray<T>(collection.Count, allocator, NativeArrayOptions.UninitializedMemory);
								int num = 0;
								{
									foreach (T item in collection)
									{
										result[num++] = item;
									}
									return result;
								}
							}
							int num2 = 0;
							int num3 = 4;
							NativeArray<T> nativeArray = new NativeArray<T>(num3, Allocator.Temp, NativeArrayOptions.UninitializedMemory);
							foreach (T item2 in enumerable)
							{
								if (num2 == num3)
								{
									num3 *= 2;
									NativeArray<T> nativeArray2;
									using (nativeArray)
									{
										nativeArray2 = new NativeArray<T>(num3, Allocator.Temp, NativeArrayOptions.UninitializedMemory);
										NativeArray<T>.Copy(nativeArray, nativeArray2, nativeArray.Length);
									}
									nativeArray = nativeArray2;
								}
								nativeArray[num2++] = item2;
							}
							using (nativeArray)
							{
								NativeArray<T> nativeArray3 = new NativeArray<T>(num2, allocator, NativeArrayOptions.UninitializedMemory);
								NativeArray<T>.Copy(nativeArray, nativeArray3, num2);
								return nativeArray3;
							}
						}
						NativeArray<T> result2 = new NativeArray<T>(readOnlyCollection.Count, allocator, NativeArrayOptions.UninitializedMemory);
						int num4 = 0;
						{
							foreach (T item3 in readOnlyCollection)
							{
								result2[num4++] = item3;
							}
							return result2;
						}
					}
					NativeArray<T> result3 = new NativeArray<T>(queue.Count, allocator, NativeArrayOptions.UninitializedMemory);
					int num5 = 0;
					{
						foreach (T item4 in queue)
						{
							result3[num5++] = item4;
						}
						return result3;
					}
				}
				NativeArray<T> result4 = new NativeArray<T>(hashSet.Count, allocator, NativeArrayOptions.UninitializedMemory);
				int num6 = 0;
				{
					foreach (T item5 in hashSet)
					{
						result4[num6++] = item5;
					}
					return result4;
				}
			}
			NativeArray<T> result5 = new NativeArray<T>(readOnlyList.Count, allocator, NativeArrayOptions.UninitializedMemory);
			for (int i = 0; i < result5.Length; i++)
			{
				result5[i] = readOnlyList[i];
			}
			return result5;
		}
		return new NativeArray<T>(array, allocator);
	}
}

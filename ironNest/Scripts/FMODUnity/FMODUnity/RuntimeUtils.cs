using System;
using FMOD;
using UnityEngine;

namespace FMODUnity
{
	public static class RuntimeUtils
	{
		public static string GetCommonPlatformPath(string path)
		{
			return null;
		}

		public static VECTOR ToFMODVector(this Vector3 vec)
		{
			return default(VECTOR);
		}

		public static ATTRIBUTES_3D To3DAttributes(this Vector3 pos)
		{
			return default(ATTRIBUTES_3D);
		}

		public static ATTRIBUTES_3D To3DAttributes(this Transform transform)
		{
			return default(ATTRIBUTES_3D);
		}

		public static ATTRIBUTES_3D To3DAttributes(this Transform transform, Vector3 velocity)
		{
			return default(ATTRIBUTES_3D);
		}

		public static ATTRIBUTES_3D To3DAttributes(this GameObject go)
		{
			return default(ATTRIBUTES_3D);
		}

		public static ATTRIBUTES_3D To3DAttributes(Transform transform, Rigidbody rigidbody = null)
		{
			return default(ATTRIBUTES_3D);
		}

		public static ATTRIBUTES_3D To3DAttributes(GameObject go, Rigidbody rigidbody)
		{
			return default(ATTRIBUTES_3D);
		}

		public static ATTRIBUTES_3D To3DAttributes(Transform transform, Rigidbody2D rigidbody)
		{
			return default(ATTRIBUTES_3D);
		}

		public static ATTRIBUTES_3D To3DAttributes(GameObject go, Rigidbody2D rigidbody)
		{
			return default(ATTRIBUTES_3D);
		}

		public static THREAD_TYPE ToFMODThreadType(ThreadType threadType)
		{
			return default(THREAD_TYPE);
		}

		public static string DisplayName(this ThreadType thread)
		{
			return null;
		}

		public static THREAD_AFFINITY ToFMODThreadAffinity(ThreadAffinity affinity)
		{
			return default(THREAD_AFFINITY);
		}

		private static void SetFMODAffinityBit(ThreadAffinity affinity, ThreadAffinity mask, THREAD_AFFINITY fmodMask, ref THREAD_AFFINITY fmodAffinity)
		{
		}

		public static void EnforceLibraryOrder()
		{
		}

		public static void DebugLog(string message)
		{
		}

		public static void DebugLogFormat(string format, params object[] args)
		{
		}

		public static void DebugLogWarning(string message)
		{
		}

		public static void DebugLogWarningFormat(string format, params object[] args)
		{
		}

		public static void DebugLogError(string message)
		{
		}

		public static void DebugLogErrorFormat(string format, params object[] args)
		{
		}

		public static void DebugLogException(Exception e)
		{
		}

		public static string GetPluginArchitectureFolder()
		{
			return null;
		}
	}
}

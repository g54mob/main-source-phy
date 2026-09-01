using System;
using HTraceAO.Scripts.Globals;
using HTraceAO.Scripts.Wrappers;
using UnityEngine;
using UnityEngine.Rendering;

namespace HTraceAO.Scripts.Extensions
{
	public static class HExtensions
	{
		[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
		public class HRangeAttribute : Attribute
		{
			public readonly bool isFloat;

			public readonly float minFloat;

			public readonly float maxFloat;

			public readonly int minInt;

			public readonly int maxInt;

			public HRangeAttribute(float minFloat, float maxFloat)
			{
			}

			public HRangeAttribute(int minInt, int maxInt)
			{
			}
		}

		public struct HRangeAttributeElement
		{
			public bool isFloat;

			public float minFloat;

			public float maxFloat;

			public int minInt;

			public int maxInt;
		}

		public static void DebugPrint(DebugType type, string msg)
		{
		}

		public static ComputeShader LoadComputeShader(string shaderName)
		{
			return null;
		}

		public static RayTracingShader LoadRayTracingShader(string shaderName)
		{
			return null;
		}

		public static bool ContainsOnOfElement(this string str, string[] elements)
		{
			return false;
		}

		public static T NextEnum<T>(this T src) where T : struct
		{
			return default(T);
		}

		public static float Clamp(float value, Type type, string nameOfField)
		{
			return 0f;
		}

		public static int Clamp(int value, Type type, string nameOfField)
		{
			return 0;
		}

		public static void HRelease(this ComputeBuffer computeBuffer)
		{
		}

		public static void HRelease(this CommandBuffer commandBuffer)
		{
		}

		public static void HRelease(this GraphicsBuffer graphicsBuffer)
		{
		}

		public static void HRelease(this HDynamicBuffer hDynamicBuffer)
		{
		}

		public static void HRelease(this RayTracingAccelerationStructure rayTracingAccelerationStructure)
		{
		}
	}
}

using System;
using System.Collections.Generic;
using System.Reflection;
using Meta.XR.ImmersiveDebugger.Utils;
using UnityEngine;

namespace Meta.XR.ImmersiveDebugger.Manager
{
	internal static class TweakUtils
	{
		private static readonly Dictionary<Type, Type> _types;

		private static readonly HashSet<Type> _supportsValueRange;

		private const string Min = "min";

		private const string Max = "max";

		static TweakUtils()
		{
			_types = new Dictionary<Type, Type>();
			_supportsValueRange = new HashSet<Type>();
			_types?.Clear();
			_supportsValueRange?.Clear();
			_supportsValueRange?.Add(typeof(int));
			_supportsValueRange?.Add(typeof(float));
			Register(Mathf.InverseLerp, Mathf.Lerp, (float f) => f);
			Register((int start, int end, int value) => Mathf.InverseLerp(start, end, value), (int start, int end, float tween) => Mathf.RoundToInt(Mathf.Lerp(start, end, tween)), (float f) => (int)f);
			Register((bool _, bool _, bool value) => (!value) ? 0f : 1f, (bool _, bool _, float tween) => tween > 0f, (float f) => f > 0f);
			_supportsValueRange?.Add(typeof(Enum));
			_types?.Add(typeof(Enum), typeof(TweakEnum));
		}

		public static bool IsTypeSupported(Type t)
		{
			if (t != null)
			{
				return _types.ContainsKey(t);
			}
			return false;
		}

		public static bool IsTypeSupportsValueRange(Type t)
		{
			if (t != null)
			{
				return _supportsValueRange.Contains(t);
			}
			return false;
		}

		public static Tweak Create(MemberInfo memberInfo, DebugMember attribute, object instance)
		{
			Type dataType = memberInfo.GetDataType();
			if (!_types.TryGetValue(dataType, out var value))
			{
				return null;
			}
			return Activator.CreateInstance(value, memberInfo, instance, attribute) as Tweak;
		}

		public static TweakEnum Create(MemberInfo memberInfo, DebugMember attribute, object instance, Type enumType)
		{
			Type baseType = memberInfo.GetDataType().BaseType;
			if (baseType == null)
			{
				return null;
			}
			if (!_types.TryGetValue(baseType, out var value))
			{
				return null;
			}
			return Activator.CreateInstance(value, memberInfo, instance, attribute, enumType) as TweakEnum;
		}

		private static void Register<T>(Func<T, T, T, float> inverseLerp, Func<T, T, float, T> lerp, Func<float, T> fromFloat)
		{
			_types.Add(typeof(T), typeof(Tweak<T>));
			Tweak<T>.InverseLerp = inverseLerp;
			Tweak<T>.Lerp = lerp;
			Tweak<T>.FromFloat = fromFloat;
		}

		public static bool IsMemberTypeValidForTweak(MemberInfo member)
		{
			if ((member.MemberType != MemberTypes.Field || !IsTypeSupported((member as FieldInfo)?.FieldType)) && (member.MemberType != MemberTypes.Property || !IsTypeSupported((member as PropertyInfo)?.PropertyType)))
			{
				return member.IsBaseTypeEqual(typeof(Enum));
			}
			return true;
		}

		public static void ProcessMinMaxRange(MemberInfo member, DebugMember attribute, UnityEngine.Object instance)
		{
			Type dataType = member.GetDataType();
			double num = 0.0;
			if (dataType == typeof(float))
			{
				num = (float)member.GetValue(instance);
			}
			else if (dataType == typeof(int))
			{
				num = (int)member.GetValue(instance);
			}
			else if (dataType == typeof(double))
			{
				num = (double)member.GetValue(instance);
			}
			if (!((double)attribute.Min <= num) || !(num <= (double)attribute.Max))
			{
				float num2 = Mathf.Abs((float)(num * 0.5));
				attribute.Min = RoundToNearest((float)(num - (double)num2), "min");
				attribute.Max = RoundToNearest((float)(num + (double)num2), "max");
			}
		}

		internal static float RoundToNearest(float value, string op)
		{
			float num = 0f;
			if (value >= 0f)
			{
				if (!(value < 1f))
				{
					if (value < 10f)
					{
						return Mathf.Round(value);
					}
					num = Mathf.Pow(10f, Mathf.Floor(Mathf.Log10(value)));
					return Mathf.Ceil(value / num) * num;
				}
				return (!(op == "min")) ? 1 : 0;
			}
			if (!(value > -1f))
			{
				if (value > -10f)
				{
					return Mathf.Round(value);
				}
				num = Mathf.Pow(10f, Mathf.Floor(Mathf.Log10(0f - value)));
				return ((op == "min") ? Mathf.Floor(value / num) : Mathf.Ceil(value / num)) * num;
			}
			if (!(op == "min"))
			{
				return 0f;
			}
			return -1f;
		}
	}
}

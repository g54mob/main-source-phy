using System;
using System.Diagnostics;

namespace NewBlood
{
	[Conditional("UNITY_EDITOR")]
	[AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
	public sealed class DefaultInitializeOnEnterPlayModeAttribute : Attribute
	{
		public DefaultInitializeType InitializeType { get; }

		public object? Value { get; }

		public DefaultInitializeOnEnterPlayModeAttribute(DefaultInitializeType type = DefaultInitializeType.Default)
		{
			InitializeType = type;
		}

		public DefaultInitializeOnEnterPlayModeAttribute(bool value)
		{
			Value = value;
		}

		public DefaultInitializeOnEnterPlayModeAttribute(byte value)
		{
			Value = value;
		}

		public DefaultInitializeOnEnterPlayModeAttribute(sbyte value)
		{
			Value = value;
		}

		public DefaultInitializeOnEnterPlayModeAttribute(ushort value)
		{
			Value = value;
		}

		public DefaultInitializeOnEnterPlayModeAttribute(short value)
		{
			Value = value;
		}

		public DefaultInitializeOnEnterPlayModeAttribute(uint value)
		{
			Value = value;
		}

		public DefaultInitializeOnEnterPlayModeAttribute(int value)
		{
			Value = value;
		}

		public DefaultInitializeOnEnterPlayModeAttribute(ulong value)
		{
			Value = value;
		}

		public DefaultInitializeOnEnterPlayModeAttribute(long value)
		{
			Value = value;
		}

		public DefaultInitializeOnEnterPlayModeAttribute(float value)
		{
			Value = value;
		}

		public DefaultInitializeOnEnterPlayModeAttribute(double value)
		{
			Value = value;
		}
	}
}

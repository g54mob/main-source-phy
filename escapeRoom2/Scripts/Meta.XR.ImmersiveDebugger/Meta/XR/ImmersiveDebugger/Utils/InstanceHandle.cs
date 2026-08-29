using System;
using UnityEngine;

namespace Meta.XR.ImmersiveDebugger.Utils
{
	[Serializable]
	internal readonly struct InstanceHandle : IEquatable<InstanceHandle>
	{
		public UnityEngine.Object Instance { get; }

		public Type Type { get; }

		public int InstanceId { get; }

		public bool IsStatic => InstanceId == 0;

		public bool Valid
		{
			get
			{
				if (Type != null)
				{
					if (!IsStatic)
					{
						return Instance != null;
					}
					return true;
				}
				return false;
			}
		}

		public InstanceHandle(Type type, UnityEngine.Object instance)
		{
			Type = type;
			Instance = instance;
			InstanceId = ((instance != null) ? instance.GetInstanceID() : 0);
		}

		public static InstanceHandle Static(Type type)
		{
			return new InstanceHandle(type, null);
		}

		public bool Equals(InstanceHandle other)
		{
			if (InstanceId == other.InstanceId)
			{
				return Type == other.Type;
			}
			return false;
		}

		public override bool Equals(object obj)
		{
			if (obj is InstanceHandle other)
			{
				return Equals(other);
			}
			return false;
		}

		public override int GetHashCode()
		{
			return (InstanceId.GetHashCode() * 486187739 + Type?.GetHashCode()).GetValueOrDefault();
		}
	}
}

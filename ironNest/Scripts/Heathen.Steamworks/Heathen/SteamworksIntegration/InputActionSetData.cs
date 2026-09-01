using System;
using Steamworks;
using UnityEngine;

namespace Heathen.SteamworksIntegration
{
	[Serializable]
	public struct InputActionSetData : IEquatable<InputActionSetHandle_t>, IComparable<InputActionSetHandle_t>, IEquatable<ulong>, IComparable<ulong>
	{
		[SerializeField]
		private InputActionSetHandle_t handle;

		public ulong Handle => 0uL;

		public bool IsActive(InputControllerStateData controller)
		{
			return false;
		}

		public bool IsActive(InputHandle_t controller)
		{
			return false;
		}

		public void Activate(InputControllerStateData controller)
		{
		}

		public void Activate(InputHandle_t controller)
		{
		}

		public static InputActionSetData Get(string setName)
		{
			return default(InputActionSetData);
		}

		public static InputActionSetData Get(InputActionSetHandle_t handle)
		{
			return default(InputActionSetData);
		}

		public static InputActionSetData Get(ulong handleValue)
		{
			return default(InputActionSetData);
		}

		public bool Equals(InputActionSetHandle_t other)
		{
			return false;
		}

		public int CompareTo(InputActionSetHandle_t other)
		{
			return 0;
		}

		public bool Equals(ulong other)
		{
			return false;
		}

		public int CompareTo(ulong other)
		{
			return 0;
		}

		public override bool Equals(object obj)
		{
			return false;
		}

		public override int GetHashCode()
		{
			return 0;
		}

		public static bool operator ==(InputActionSetData l, InputActionSetHandle_t r)
		{
			return false;
		}

		public static bool operator ==(InputActionSetData l, ulong r)
		{
			return false;
		}

		public static bool operator ==(InputActionSetHandle_t l, InputActionSetData r)
		{
			return false;
		}

		public static bool operator ==(ulong l, InputActionSetData r)
		{
			return false;
		}

		public static bool operator !=(InputActionSetData l, InputActionSetHandle_t r)
		{
			return false;
		}

		public static bool operator !=(InputActionSetData l, ulong r)
		{
			return false;
		}

		public static bool operator !=(InputActionSetHandle_t l, InputActionSetData r)
		{
			return false;
		}

		public static bool operator !=(ulong l, InputActionSetData r)
		{
			return false;
		}

		public static implicit operator ulong(InputActionSetData c)
		{
			return 0uL;
		}

		public static implicit operator InputActionSetData(ulong id)
		{
			return default(InputActionSetData);
		}

		public static implicit operator InputActionSetHandle_t(InputActionSetData c)
		{
			return default(InputActionSetHandle_t);
		}

		public static implicit operator InputActionSetData(InputActionSetHandle_t id)
		{
			return default(InputActionSetData);
		}
	}
}

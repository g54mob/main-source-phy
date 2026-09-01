using System;

namespace XGamingRuntime
{
	public abstract class EquatableHandle
	{
		internal abstract IntPtr GetInternalPtr();

		public override bool Equals(object obj)
		{
			if (obj is EquatableHandle)
			{
				EquatableHandle equatableHandle = (EquatableHandle)obj;
				return GetInternalPtr() == equatableHandle.GetInternalPtr();
			}
			return false;
		}

		public override int GetHashCode()
		{
			return GetInternalPtr().GetHashCode();
		}

		public static bool operator ==(EquatableHandle handle1, EquatableHandle handle2)
		{
			return (!object.ReferenceEquals(handle1, null)) ? handle1.Equals(handle2) : object.ReferenceEquals(handle2, null);
		}

		public static bool operator !=(EquatableHandle handle1, EquatableHandle handle2)
		{
			return !(handle1 == handle2);
		}
	}
}

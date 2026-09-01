using System;

namespace FMOD
{
	[Serializable]
	public struct GUID : IEquatable<GUID>
	{
		public int Data1;

		public int Data2;

		public int Data3;

		public int Data4;

		public bool IsNull => false;

		public GUID(Guid guid)
		{
			Data1 = 0;
			Data2 = 0;
			Data3 = 0;
			Data4 = 0;
		}

		public static GUID Parse(string s)
		{
			return default(GUID);
		}

		public override bool Equals(object other)
		{
			return false;
		}

		public bool Equals(GUID other)
		{
			return false;
		}

		public static bool operator ==(GUID a, GUID b)
		{
			return false;
		}

		public static bool operator !=(GUID a, GUID b)
		{
			return false;
		}

		public override int GetHashCode()
		{
			return 0;
		}

		public static implicit operator Guid(GUID guid)
		{
			return default(Guid);
		}

		public override string ToString()
		{
			return null;
		}
	}
}

using System;
using System.Text;

namespace Heathen.SteamworksIntegration
{
	[Serializable]
	public struct RemoteStorageFile : IEquatable<RemoteStorageFile>
	{
		public string name;

		public int size;

		public DateTime Timestamp;

		public byte[] Data => null;

		public bool Equals(RemoteStorageFile other)
		{
			return false;
		}

		public override bool Equals(object obj)
		{
			return false;
		}

		public override int GetHashCode()
		{
			return 0;
		}

		public override string ToString()
		{
			return null;
		}

		public string ToString(Encoding encoding)
		{
			return null;
		}

		public T ToJson<T>()
		{
			return default(T);
		}

		public T ToJson<T>(Encoding encoding)
		{
			return default(T);
		}

		public static bool operator ==(RemoteStorageFile l, RemoteStorageFile r)
		{
			return false;
		}

		public static bool operator !=(RemoteStorageFile l, RemoteStorageFile r)
		{
			return false;
		}
	}
}

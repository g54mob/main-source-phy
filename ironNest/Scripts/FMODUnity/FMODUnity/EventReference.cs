using System;
using FMOD;

namespace FMODUnity
{
	[Serializable]
	public struct EventReference
	{
		public GUID Guid;

		public bool IsNull => false;

		public override string ToString()
		{
			return null;
		}
	}
}

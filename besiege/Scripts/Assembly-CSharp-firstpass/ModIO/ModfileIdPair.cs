using System;

namespace ModIO
{
	[Serializable]
	public struct ModfileIdPair
	{
		public static readonly ModfileIdPair NULL = new ModfileIdPair(0, 0);

		public int modId;

		public int modfileId;

		public ModfileIdPair(int modId, int modfileId)
		{
			this.modId = modId;
			this.modfileId = modfileId;
		}
	}
}

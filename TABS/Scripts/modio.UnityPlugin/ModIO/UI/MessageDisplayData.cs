using System;

namespace ModIO.UI
{
	[Serializable]
	public struct MessageDisplayData
	{
		public enum Type
		{
			Info = 0,
			Success = 1,
			Warning = 2,
			Error = 3
		}

		public Type type;

		public string content;

		public float displayDuration;
	}
}

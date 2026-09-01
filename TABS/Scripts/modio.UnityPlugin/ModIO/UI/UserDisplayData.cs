using System;

namespace ModIO.UI
{
	[Serializable]
	[Obsolete("No longer supported.")]
	public struct UserDisplayData
	{
		public UserProfileDisplayData profile;

		public ImageDisplayData avatar;
	}
}

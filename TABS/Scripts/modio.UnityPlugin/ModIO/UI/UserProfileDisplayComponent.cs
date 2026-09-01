using System;
using UnityEngine;

namespace ModIO.UI
{
	[Obsolete("No longer supported.")]
	public abstract class UserProfileDisplayComponent : MonoBehaviour
	{
		public abstract UserProfileDisplayData data { get; set; }

		public abstract event Action<UserProfileDisplayComponent> onClick;

		public abstract void Initialize();

		public abstract void DisplayProfile(UserProfile profile);

		public abstract void DisplayLoading();
	}
}

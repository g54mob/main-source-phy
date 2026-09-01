using System;
using UnityEngine;

namespace ModIO.UI
{
	[Obsolete("No longer supported.")]
	public abstract class ModProfileDisplayComponent : MonoBehaviour
	{
		public abstract ModProfileDisplayData data { get; set; }

		public abstract event Action<ModProfileDisplayComponent> onClick;

		public abstract void Initialize();

		public abstract void DisplayProfile(ModProfile profile);

		public abstract void DisplayLoading();
	}
}

using System;
using UnityEngine;

namespace ModIO.UI
{
	[Obsolete("No longer supported.")]
	public abstract class ModTagDisplayComponent : MonoBehaviour
	{
		public abstract ModTagDisplayData data { get; set; }

		public abstract event Action<ModTagDisplayComponent> onClick;

		public abstract void Initialize();

		public abstract void DisplayModTag(string tagName, string categoryName);

		public abstract void DisplayModTag(ModTag tag, string categoryName);

		public abstract void DisplayLoading();
	}
}

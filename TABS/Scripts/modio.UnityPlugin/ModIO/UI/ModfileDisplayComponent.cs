using System;
using UnityEngine;

namespace ModIO.UI
{
	[Obsolete("No longer supported.")]
	public abstract class ModfileDisplayComponent : MonoBehaviour
	{
		public abstract ModfileDisplayData data { get; set; }

		public abstract event Action<ModfileDisplayComponent> onClick;

		public abstract void Initialize();

		public abstract void DisplayModfile(Modfile modfile);

		public abstract void DisplayLoading();
	}
}

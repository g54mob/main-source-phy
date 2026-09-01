using System;
using UnityEngine;

namespace ModIO.UI
{
	[Obsolete("No longer supported.")]
	public abstract class ModStatisticsDisplayComponent : MonoBehaviour
	{
		public abstract ModStatisticsDisplayData data { get; set; }

		public abstract event Action<ModStatisticsDisplayComponent> onClick;

		public abstract void Initialize();

		public abstract void DisplayStatistics(ModStatistics statistics);

		public abstract void DisplayLoading();
	}
}

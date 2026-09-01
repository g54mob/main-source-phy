using System.Collections.Generic;
using UnityEngine;

namespace MoreMountains.Tools
{
	[AddComponentMenu("More Mountains/Tools/Activation/MMApplicationPlatformActivation")]
	public class MMApplicationPlatformActivation : MonoBehaviour
	{
		public enum ExecutionTimes
		{
			Awake = 0,
			Start = 1,
			OnEnable = 2
		}

		[Header("Settings")]
		public ExecutionTimes ExecutionTime;

		public bool DebugToTheConsole;

		[Header("Platforms")]
		public List<PlatformBindings> Platforms;

		protected virtual void OnEnable()
		{
		}

		protected virtual void Awake()
		{
		}

		protected virtual void Start()
		{
		}

		protected virtual void Process()
		{
		}

		protected virtual void DisableIfNeeded(PlatformBindings.PlatformActions platform, string platformName)
		{
		}
	}
}

using System;
using System.Collections;
using SRF;
using UnityEngine;
using UnityEngine.UI;

namespace SRDebugger.UI.Controls
{
	public class ProfilerMemoryBlock : SRMonoBehaviourEx
	{
		private float _lastRefresh;

		[RequiredField]
		public Text CurrentUsedText;

		[RequiredField]
		public UnityEngine.UI.Slider Slider;

		[RequiredField]
		public Text TotalAllocatedText;

		protected override void OnEnable()
		{
			base.OnEnable();
			TriggerRefresh();
		}

		protected override void Update()
		{
			base.Update();
			if (SRDebug.Instance.IsDebugPanelVisible && Time.realtimeSinceStartup - _lastRefresh > 1f)
			{
				TriggerRefresh();
				_lastRefresh = Time.realtimeSinceStartup;
			}
		}

		public void TriggerRefresh()
		{
			uint totalReservedMemory = UnityEngine.Profiler.GetTotalReservedMemory();
			uint totalAllocatedMemory = UnityEngine.Profiler.GetTotalAllocatedMemory();
			Slider.maxValue = totalReservedMemory;
			Slider.value = totalAllocatedMemory;
			uint num = totalReservedMemory >> 10;
			num /= 1024;
			uint num2 = totalAllocatedMemory >> 10;
			num2 /= 1024;
			TotalAllocatedText.text = "Reserved: <color=#FFFFFF>{0}</color>MB".Fmt(num);
			CurrentUsedText.text = "<color=#FFFFFF>{0}</color>MB".Fmt(num2);
		}

		public void TriggerCleanup()
		{
			StartCoroutine(CleanUp());
		}

		private IEnumerator CleanUp()
		{
			GC.Collect();
			yield return Resources.UnloadUnusedAssets();
			GC.Collect();
			TriggerRefresh();
		}
	}
}

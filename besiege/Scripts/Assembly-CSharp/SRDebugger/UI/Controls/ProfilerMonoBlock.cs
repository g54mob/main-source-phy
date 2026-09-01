using System;
using SRF;
using UnityEngine;
using UnityEngine.UI;

namespace SRDebugger.UI.Controls
{
	public class ProfilerMonoBlock : SRMonoBehaviourEx
	{
		private float _lastRefresh;

		[RequiredField]
		public Text CurrentUsedText;

		[RequiredField]
		public GameObject NotSupportedMessage;

		[RequiredField]
		public UnityEngine.UI.Slider Slider;

		[RequiredField]
		public Text TotalAllocatedText;

		private bool _isSupported;

		protected override void OnEnable()
		{
			base.OnEnable();
			_isSupported = UnityEngine.Profiler.GetMonoUsedSize() != 0;
			NotSupportedMessage.SetActive(!_isSupported);
			CurrentUsedText.gameObject.SetActive(_isSupported);
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
			long num = ((!_isSupported) ? GC.GetTotalMemory(false) : UnityEngine.Profiler.GetMonoHeapSize());
			uint monoUsedSize = UnityEngine.Profiler.GetMonoUsedSize();
			Slider.maxValue = num;
			Slider.value = monoUsedSize;
			long num2 = num >> 10;
			num2 /= 1024;
			uint num3 = monoUsedSize >> 10;
			num3 /= 1024;
			TotalAllocatedText.text = "Total: <color=#FFFFFF>{0}</color>MB".Fmt(num2);
			if (num3 != 0)
			{
				CurrentUsedText.text = "<color=#FFFFFF>{0}</color>MB".Fmt(num3);
			}
		}

		public void TriggerCollection()
		{
			GC.Collect();
			TriggerRefresh();
		}
	}
}

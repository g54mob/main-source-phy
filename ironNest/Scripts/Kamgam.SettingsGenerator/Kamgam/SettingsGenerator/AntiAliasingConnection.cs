using System.Collections.Generic;
using UnityEngine;

namespace Kamgam.SettingsGenerator
{
	public class AntiAliasingConnection : ConnectionWithOptions<string>
	{
		public bool LimitToMainCamera;

		public bool IncludeMSAA;

		protected MSAAConnection _msaaConnection;

		protected List<string> _labels;

		public MSAAConnection MsaaConnection => null;

		protected void onNewCameraFound(Camera cam)
		{
		}

		public override List<string> GetOptionLabels()
		{
			return null;
		}

		public override void SetOptionLabels(List<string> optionLabels)
		{
		}

		public override void RefreshOptionLabels()
		{
		}

		public override int Get()
		{
			return 0;
		}

		public override void Set(int index)
		{
		}

		private void setOnCamera(Camera cam, int index)
		{
		}
	}
}

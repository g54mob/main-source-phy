using System.Collections.Generic;
using UnityEngine.Rendering;

namespace Kamgam.SettingsGenerator
{
	public class ShadowConnection : Connection<bool>
	{
		protected Dictionary<RenderPipelineAsset, float> previousValue;

		public override bool Get()
		{
			return false;
		}

		public override void Set(bool enable)
		{
		}

		protected void remember()
		{
		}

		protected void revert()
		{
		}
	}
}

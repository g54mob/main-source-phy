using UnityEngine;

namespace Kamgam.SettingsGenerator;

public class FogConnection : Connection<bool>
{
	public override bool Get()
	{
		return RenderSettings.fog;
	}

	public override void Set(bool enable)
	{
		RenderSettings.fog = enable;
	}
}

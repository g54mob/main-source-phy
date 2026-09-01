using UnityEngine;

public class MuteAudio : ClickBehaviour
{
	public Material redMaterial;

	public Material darkMaterial;

	public Renderer vinylRenderer;

	public Texture2D vinylOnTex;

	public Texture2D vinylOffTex;

	private void Awake()
	{
		if (OptionsMaster.BesiegeConfig.MusicEnabled)
		{
			GetComponent<Renderer>().material = redMaterial;
			vinylRenderer.material.mainTexture = vinylOnTex;
		}
		else
		{
			GetComponent<Renderer>().material = darkMaterial;
			vinylRenderer.material.mainTexture = vinylOffTex;
		}
	}

	public override void OnClicked()
	{
		Set();
	}

	private void Set()
	{
		if (!SingleInstance<MusicController>.Instance.HasAnySources)
		{
			OptionsMaster.BesiegeConfig.MusicEnabled = false;
			return;
		}
		if (OptionsMaster.BesiegeConfig.MusicEnabled)
		{
			GetComponent<Renderer>().material = darkMaterial;
			vinylRenderer.material.mainTexture = vinylOffTex;
			SingleInstance<MusicController>.Instance.Mute();
		}
		else
		{
			GetComponent<Renderer>().material = redMaterial;
			vinylRenderer.material.mainTexture = vinylOnTex;
			SingleInstance<MusicController>.Instance.Resume();
		}
		OptionsMaster.BesiegeConfig.MusicEnabled = !OptionsMaster.BesiegeConfig.MusicEnabled;
	}
}

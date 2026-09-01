using UnityEngine;
using UnityEngine.Playables;

public class LightmapInterpolatorTextureSwapperBehaviour : PlayableBehaviour
{
	public Texture2D m_texture01;

	public Texture2D m_texture02;

	public override void ProcessFrame(Playable playable, FrameData info, object playerData)
	{
		LightmapInterpolatorTextureSwapper lightmapInterpolatorTextureSwapper = playerData as LightmapInterpolatorTextureSwapper;
		if (lightmapInterpolatorTextureSwapper != null)
		{
			lightmapInterpolatorTextureSwapper.SetTextures(m_texture01, m_texture02);
		}
	}
}

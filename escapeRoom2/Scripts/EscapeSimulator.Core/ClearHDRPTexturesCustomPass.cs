using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

[Serializable]
public class ClearHDRPTexturesCustomPass : CustomPass
{
	private Texture2D whiteTexture;

	private Texture2D blackTexture;

	public bool clearAmbientOcclussion;

	public bool clearShadowMap;

	public bool clearDecals;

	public bool clearExposure;

	protected override void Setup(ScriptableRenderContext renderContext, CommandBuffer cmd)
	{
		whiteTexture = Texture2D.whiteTexture;
		blackTexture = Texture2D.blackTexture;
	}

	protected override void Execute(CustomPassContext ctx)
	{
		if (clearAmbientOcclussion)
		{
			ctx.cmd.SetGlobalTexture("_AmbientOcclusionTexture", whiteTexture);
		}
		if (clearShadowMap)
		{
			ctx.cmd.SetGlobalTexture("_ShadowmapAreaAtlas", whiteTexture);
			ctx.cmd.SetGlobalTexture("_ShadowmapAtlas", whiteTexture);
			ctx.cmd.SetGlobalTexture("_ShadowmapCascadeAtlas", whiteTexture);
		}
		if (clearDecals)
		{
			ctx.cmd.DisableShaderKeyword("DECALS_4RT");
		}
		if (clearExposure)
		{
			ctx.cmd.SetGlobalTexture("_ExposureTexture", whiteTexture);
			ctx.cmd.SetGlobalTexture("_PrevExposureTexture", whiteTexture);
		}
	}
}

using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.Rendering.RendererUtils;

[Serializable]
public class PineCustomDrawRenderers : CustomPass
{
	public enum ShaderPass
	{
		DepthPrepass = 1,
		Forward = 0
	}

	public enum OverrideMaterialMode
	{
		None = 0,
		Material = 1,
		Shader = 2
	}

	[SerializeField]
	internal bool filterFoldout;

	[SerializeField]
	internal bool rendererFoldout;

	public Camera overrideCameraValues;

	public RenderQueueType renderQueueType = RenderQueueType.AllOpaque;

	public LayerMask layerMask = 1;

	public SortingCriteria sortingCriteria = SortingCriteria.SortingLayer | SortingCriteria.RenderQueue | SortingCriteria.OptimizeStateChanges | SortingCriteria.CanvasOrder;

	public OverrideMaterialMode overrideMode = OverrideMaterialMode.Material;

	public Material overrideMaterial;

	[SerializeField]
	private int overrideMaterialPassIndex;

	public string overrideMaterialPassName = "Forward";

	public Shader overrideShader;

	[SerializeField]
	private int overrideShaderPassIndex;

	public string overrideShaderPassName = "Forward";

	public bool overrideDepthState;

	public CompareFunction depthCompareFunction = CompareFunction.LessEqual;

	public bool depthWrite = true;

	public bool overrideStencil;

	public int stencilReferenceValue = 64;

	public int stencilWriteMask = 192;

	public int stencilReadMask = 192;

	public CompareFunction stencilCompareFunction = CompareFunction.Always;

	public StencilOp stencilPassOperation;

	public StencilOp stencilFailOperation;

	public StencilOp stencilDepthFailOperation;

	public ShaderPass shaderPass;

	private int fadeValueId;

	private static ShaderTagId[] forwardShaderTags;

	private static ShaderTagId[] depthShaderTags;

	private ShaderTagId[] cachedShaderTagIDs;

	protected override void Setup(ScriptableRenderContext renderContext, CommandBuffer cmd)
	{
		fadeValueId = Shader.PropertyToID("_FadeValue");
		if (string.IsNullOrEmpty(overrideMaterialPassName) && overrideMaterial != null)
		{
			overrideMaterialPassName = overrideMaterial.GetPassName(overrideMaterialPassIndex);
		}
		if (string.IsNullOrEmpty(overrideShaderPassName) && overrideShader != null)
		{
			overrideShaderPassName = new Material(overrideShader).GetPassName(overrideShaderPassIndex);
		}
		forwardShaderTags = new ShaderTagId[4]
		{
			HDShaderPassNames.s_ForwardName,
			HDShaderPassNames.s_ForwardOnlyName,
			HDShaderPassNames.s_SRPDefaultUnlitName,
			HDShaderPassNames.s_EmptyName
		};
		depthShaderTags = new ShaderTagId[3]
		{
			HDShaderPassNames.s_DepthForwardOnlyName,
			HDShaderPassNames.s_DepthOnlyName,
			HDShaderPassNames.s_EmptyName
		};
	}

	private ShaderTagId[] GetShaderTagIds()
	{
		if (shaderPass == ShaderPass.DepthPrepass)
		{
			return depthShaderTags;
		}
		return forwardShaderTags;
	}

	protected override void Execute(CustomPassContext ctx)
	{
		ShaderTagId[] shaderTagIds = GetShaderTagIds();
		if (overrideMaterial != null)
		{
			shaderTagIds[^1] = new ShaderTagId(overrideMaterialPassName);
			overrideMaterial.SetFloat(fadeValueId, base.fadeValue);
		}
		if (shaderTagIds.Length == 0)
		{
			Debug.LogWarning("Attempt to call DrawRenderers with an empty shader passes. Skipping the call to avoid errors");
			return;
		}
		RenderStateMask renderStateMask = (overrideDepthState ? RenderStateMask.Depth : RenderStateMask.Nothing);
		renderStateMask = (RenderStateMask)((int)renderStateMask | ((overrideDepthState && !depthWrite) ? 8 : 0));
		if (overrideStencil)
		{
			renderStateMask |= RenderStateMask.Stencil;
		}
		RenderStateBlock renderStateBlock = new RenderStateBlock(renderStateMask);
		renderStateBlock.depthState = new DepthState(depthWrite, depthCompareFunction);
		renderStateBlock.stencilState = new StencilState(overrideStencil, (byte)stencilReadMask, (byte)stencilWriteMask, stencilCompareFunction, stencilPassOperation, stencilFailOperation, stencilDepthFailOperation);
		renderStateBlock.stencilReference = (overrideStencil ? stencilReferenceValue : 0);
		RenderStateBlock value = renderStateBlock;
		PerObjectData rendererConfiguration = HDUtils.GetRendererConfiguration(ctx.hdCamera.frameSettings.IsEnabled(FrameSettingsField.AdaptiveProbeVolume), ctx.hdCamera.frameSettings.IsEnabled(FrameSettingsField.Shadowmask));
		Material material = ((overrideShader != null) ? new Material(overrideShader) : null);
		if (!(overrideCameraValues != null) || !overrideCameraValues.TryGetCullingParameters(out var parameters))
		{
			return;
		}
		ScriptableRenderContext renderContext = ctx.renderContext;
		parameters.cullingOptions &= ~CullingOptions.OcclusionCull;
		parameters.cullingMask |= (uint)(int)layerMask;
		CullingResults cullingResult = renderContext.Cull(ref parameters);
		RendererListDesc rendererListDesc = new RendererListDesc(shaderTagIds, cullingResult, overrideCameraValues);
		rendererListDesc.rendererConfiguration = rendererConfiguration;
		rendererListDesc.renderQueueRange = GetRenderQueueRange(renderQueueType);
		rendererListDesc.sortingCriteria = sortingCriteria;
		rendererListDesc.excludeObjectMotionVectors = false;
		rendererListDesc.overrideShader = ((overrideMode == OverrideMaterialMode.Shader) ? overrideShader : null);
		rendererListDesc.overrideMaterial = ((overrideMode == OverrideMaterialMode.Material) ? overrideMaterial : null);
		rendererListDesc.overrideMaterialPassIndex = ((overrideMaterial != null) ? overrideMaterial.FindPass(overrideMaterialPassName) : 0);
		rendererListDesc.overrideShaderPassIndex = ((overrideShader != null) ? material.FindPass(overrideShaderPassName) : 0);
		rendererListDesc.stateBlock = value;
		rendererListDesc.layerMask = layerMask;
		RendererListDesc desc = rendererListDesc;
		UnityEngine.Object.DestroyImmediate(material);
		RendererList rendererList = ctx.renderContext.CreateRendererList(desc);
		bool opaque = renderQueueType == RenderQueueType.AllOpaque || renderQueueType == RenderQueueType.OpaqueAlphaTest || renderQueueType == RenderQueueType.OpaqueNoAlphaTest;
		using (new CustomPassUtils.OverrideCameraRendering(ctx, overrideCameraValues))
		{
			RenderForwardRendererList(ctx.hdCamera.frameSettings, rendererList, opaque, ctx.renderContext, ctx.cmd);
		}
	}

	private static void RenderForwardRendererList(FrameSettings frameSettings, RendererList rendererList, bool opaque, ScriptableRenderContext renderContext, CommandBuffer cmd)
	{
		bool flag = opaque && frameSettings.IsEnabled(FrameSettingsField.FPTLForForwardOpaque);
		CoreUtils.SetKeyword(cmd, "USE_FPTL_LIGHTLIST", flag);
		CoreUtils.SetKeyword(cmd, "USE_CLUSTERED_LIGHTLIST", !flag);
		if (opaque)
		{
			if (frameSettings.IsEnabled(FrameSettingsField.OpaqueObjects))
			{
				CoreUtils.DrawRendererList(renderContext, cmd, rendererList);
			}
		}
		else if (frameSettings.IsEnabled(FrameSettingsField.TransparentObjects))
		{
			CoreUtils.DrawRendererList(renderContext, cmd, rendererList);
		}
	}
}

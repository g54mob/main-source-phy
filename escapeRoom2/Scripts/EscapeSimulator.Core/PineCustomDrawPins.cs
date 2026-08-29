using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

[Serializable]
public class PineCustomDrawPins : CustomPass
{
	public Game game;

	public Camera utilityCamera;

	protected override void Execute(CustomPassContext ctx)
	{
		if (game == null || utilityCamera == null)
		{
			return;
		}
		Camera camera = ctx.hdCamera.camera;
		CommandBuffer cmd = ctx.cmd;
		cmd.SetViewProjectionMatrices(camera.worldToCameraMatrix, utilityCamera.projectionMatrix);
		cmd.SetGlobalColor("_OverlayColor", game.itemUIOverlayColor);
		cmd.SetGlobalFloat("_OverlayExposure", game.itemUIExposure);
		cmd.SetGlobalFloat("_OverlaySaturation", game.itemUISaturation);
		foreach (Game.PinnedItemInfo pinnedItem in game.pinnedItems)
		{
			ctx.cmd.ClearRenderTarget(clearDepth: true, clearColor: false, Color.white);
			foreach (RendererCommandParams item in pinnedItem.renderersOpaque)
			{
				ctx.cmd.DrawRenderer(item.renderer, item.material, item.index);
			}
			foreach (RendererCommandParams item2 in pinnedItem.renderersTransparent)
			{
				ctx.cmd.DrawRenderer(item2.renderer, item2.material, item2.index);
			}
		}
		cmd.SetViewProjectionMatrices(camera.worldToCameraMatrix, camera.projectionMatrix);
	}
}

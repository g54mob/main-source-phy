using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

[Serializable]
public class PineCustomDrawItems : CustomPass
{
	public Game game;

	public Camera uiCamera;

	protected override void Execute(CustomPassContext ctx)
	{
		if (game == null || uiCamera == null)
		{
			return;
		}
		Camera camera = ctx.hdCamera.camera;
		CommandBuffer cmd = ctx.cmd;
		cmd.SetViewProjectionMatrices(uiCamera.worldToCameraMatrix, uiCamera.projectionMatrix);
		cmd.SetGlobalColor("_OverlayColor", game.itemUIOverlayColor);
		cmd.SetGlobalFloat("_OverlayExposure", game.itemUIExposure);
		cmd.SetGlobalFloat("_OverlaySaturation", game.itemUISaturation);
		foreach (Game.InventoryItem item in game.inventory)
		{
			foreach (RendererCommandParams item2 in item.impostorClone.renderersOpaque)
			{
				if (item2.renderer != null && item2.material != null)
				{
					ctx.cmd.DrawRenderer(item2.renderer, item2.material, item2.index);
				}
			}
			foreach (RendererCommandParams item3 in item.impostorClone.renderersTransparent)
			{
				if (item3.renderer != null && item3.material != null)
				{
					ctx.cmd.DrawRenderer(item3.renderer, item3.material, item3.index);
				}
			}
		}
		if (game.trashInventoryCanvas.gameObject.activeSelf)
		{
			foreach (Game.InventoryItem item4 in game.trashInventory)
			{
				foreach (RendererCommandParams item5 in item4.impostorClone.renderersOpaque)
				{
					if (item5.renderer != null && item5.material != null)
					{
						ctx.cmd.DrawRenderer(item5.renderer, item5.material, item5.index);
					}
				}
				foreach (RendererCommandParams item6 in item4.impostorClone.renderersTransparent)
				{
					if (item6.renderer != null && item6.material != null)
					{
						ctx.cmd.DrawRenderer(item6.renderer, item6.material, item6.index);
					}
				}
			}
		}
		if (game.inOtherPlayerInventoryFocus)
		{
			foreach (Game.InventoryItem item7 in game.currentOtherPlayerInventory)
			{
				foreach (RendererCommandParams item8 in item7.impostorClone.renderersOpaque)
				{
					if (item8.renderer != null && item8.material != null)
					{
						ctx.cmd.DrawRenderer(item8.renderer, item8.material, item8.index);
					}
				}
				foreach (RendererCommandParams item9 in item7.impostorClone.renderersTransparent)
				{
					if (item9.renderer != null && item9.material != null)
					{
						ctx.cmd.DrawRenderer(item9.renderer, item9.material, item9.index);
					}
				}
			}
		}
		if (game.previewPlayerId != null)
		{
			foreach (Game.InventoryItem item10 in game.previewPlayerInventory)
			{
				foreach (RendererCommandParams item11 in item10.impostorClone.renderersOpaque)
				{
					if (item11.renderer != null && item11.material != null)
					{
						ctx.cmd.DrawRenderer(item11.renderer, item11.material, item11.index);
					}
				}
				foreach (RendererCommandParams item12 in item10.impostorClone.renderersTransparent)
				{
					if (item12.renderer != null && item12.material != null)
					{
						ctx.cmd.DrawRenderer(item12.renderer, item12.material, item12.index);
					}
				}
			}
		}
		cmd.SetViewProjectionMatrices(camera.worldToCameraMatrix, camera.projectionMatrix);
	}
}

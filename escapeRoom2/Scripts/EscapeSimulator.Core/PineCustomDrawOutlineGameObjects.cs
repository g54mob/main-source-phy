using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

[Serializable]
public class PineCustomDrawOutlineGameObjects : CustomPass
{
	public List<Renderer> overrideCustomRenderers = new List<Renderer>(16);

	public Material overrideMaterial;

	protected override void Execute(CustomPassContext ctx)
	{
		foreach (Renderer overrideCustomRenderer in overrideCustomRenderers)
		{
			if (overrideCustomRenderer != null && overrideCustomRenderer.enabled)
			{
				for (int i = 0; i < overrideCustomRenderer.sharedMaterials.Length; i++)
				{
					ctx.cmd.SetGlobalTexture("_MainTex", overrideCustomRenderer.sharedMaterials[i].HasTexture("_BaseColorMap") ? overrideCustomRenderer.sharedMaterials[i].GetTexture("_BaseColorMap") : null);
					ctx.cmd.SetGlobalColor("_SelectionColor", Color.white);
					ctx.cmd.DrawRenderer(overrideCustomRenderer, overrideMaterial, i);
				}
			}
		}
	}
}

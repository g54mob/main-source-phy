using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

[Serializable]
public class PineCustomDrawEditorSelection : CustomPass
{
	public List<Renderer> selectedRenderers = new List<Renderer>(16);

	public List<Renderer> specialSelectedRenderers = new List<Renderer>(16);

	public Material overrideMaterial;

	protected override void Execute(CustomPassContext ctx)
	{
		foreach (Renderer selectedRenderer in selectedRenderers)
		{
			if (!(selectedRenderer == null) && selectedRenderer.enabled)
			{
				for (int i = 0; i < selectedRenderer.sharedMaterials.Length; i++)
				{
					ctx.cmd.SetGlobalTexture("_MainTex", selectedRenderer.sharedMaterials[i].HasTexture("_BaseColorMap") ? selectedRenderer.sharedMaterials[i].GetTexture("_BaseColorMap") : null);
					ctx.cmd.SetGlobalColor("_SelectionColor", Color.red);
					ctx.cmd.DrawRenderer(selectedRenderer, overrideMaterial, i);
				}
			}
		}
		foreach (Renderer specialSelectedRenderer in specialSelectedRenderers)
		{
			if (!(specialSelectedRenderer == null) && specialSelectedRenderer.enabled)
			{
				for (int j = 0; j < specialSelectedRenderer.sharedMaterials.Length; j++)
				{
					ctx.cmd.SetGlobalTexture("_MainTex", specialSelectedRenderer.sharedMaterials[j].HasTexture("_BaseColorMap") ? specialSelectedRenderer.sharedMaterials[j].GetTexture("_BaseColorMap") : null);
					ctx.cmd.SetGlobalColor("_SelectionColor", Color.white);
					ctx.cmd.DrawRenderer(specialSelectedRenderer, overrideMaterial, j);
				}
			}
		}
	}
}

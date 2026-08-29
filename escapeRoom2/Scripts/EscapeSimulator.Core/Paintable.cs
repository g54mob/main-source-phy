using System;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Renderer))]
public class Paintable : Interactive
{
	public const int MAX_EDITS_TO_SAVE = 1000;

	public const float SIMILAR_EDIT_RADIUS = 0.04f;

	[Header("Paintable")]
	[DontSave]
	public PaintableMode paintMode;

	[DontSave]
	public Texture2D brush;

	[DontSave]
	public Color brushColor = Color.white;

	[DontSave]
	public float brushSize = 0.25f;

	[DontSave]
	public PaintableSizeSpace brushSizeMode;

	[DontSave]
	public Item requiredItem;

	[NonSerialized]
	[DontSave]
	public Texture originalTexture;

	[NonSerialized]
	[DontSave]
	public RenderTexture albedoRT;

	[NonSerialized]
	[DontSave]
	public List<PaintableEdit> edits = new List<PaintableEdit>(1000);

	[Tooltip("Sound played when interaction with this Paintable starts.")]
	[DontSave]
	[HideInInspector]
	public EventReference startSound;

	[Tooltip("Sound played when this Paintable is being painted.")]
	[DontSave]
	[HideInInspector]
	public EventReference soundPaint;

	[Tooltip("Sound played when interaction with this Paintable ends.")]
	[DontSave]
	[HideInInspector]
	public EventReference endSound;

	public override Game.ScreenTargetType getTargetType()
	{
		return Game.ScreenTargetType.Paintable;
	}

	protected override Game.PCCrosshair getBaseCursor()
	{
		return Game.PCCrosshair.Use;
	}

	public override ItemCheck itemCheck(Item item)
	{
		if (requiredItem == null)
		{
			return ItemCheck.NotRequired;
		}
		if (item == requiredItem)
		{
			return ItemCheck.Passes;
		}
		return ItemCheck.Fails;
	}

	public override void init()
	{
		Renderer component = GetComponent<Renderer>();
		RenderTextureFormat format = RenderTextureFormat.Default;
		if (SystemInfo.SupportsRenderTextureFormat(RenderTextureFormat.ARGB32) && SystemInfo.SupportsBlendingOnRenderTextureFormat(RenderTextureFormat.ARGB32))
		{
			format = RenderTextureFormat.ARGB32;
		}
		Texture texture = (originalTexture = component.material.GetTexture("_BaseColorMap"));
		albedoRT = new RenderTexture(texture.width, texture.height, 24, format)
		{
			filterMode = texture.filterMode
		};
		if (!albedoRT.Create())
		{
			Debug.LogError("Paintable.init albedoRT has not been created!");
		}
		else
		{
			Graphics.Blit(texture, albedoRT);
			component.material.SetTexture("_BaseColorMap", albedoRT);
		}
		soundEmitter = PineFmod.addSoundEmitter(base.gameObject, soundPaint);
	}

	[ContextMenu("reset")]
	public void reset()
	{
		if (!albedoRT.IsCreated())
		{
			Debug.LogError("Paintable.reset albedoRT has not been created!");
		}
		else
		{
			Graphics.Blit(originalTexture, albedoRT);
		}
		edits.Clear();
	}

	public void OnDestroy()
	{
		if (albedoRT != null)
		{
			albedoRT.Release();
			UnityEngine.Object.Destroy(albedoRT);
			albedoRT = null;
		}
	}

	public override void save(FastBinaryWriter writer)
	{
		base.save(writer);
	}

	public override void load(FastBinaryReader reader)
	{
		base.load(reader);
	}

	public override void read(FastBinaryReader reader, List<SaveFileProperty> saveFileProperties)
	{
		base.read(reader, saveFileProperties);
	}
}

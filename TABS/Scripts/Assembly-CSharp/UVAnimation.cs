using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UVAnimation : MonoBehaviour
{
	public enum RenderMode
	{
		World = 0,
		UI = 1
	}

	private Renderer rend;

	private Image image;

	public string texName = "_MainTex";

	public Vector2 offsetPerFrame;

	private Vector2 offset = Vector2.zero;

	public RenderMode renderMode;

	public bool useUnscaledDeltatime;

	public bool useSharedMaterial;

	public int materialIndex;

	private List<Material> materials = new List<Material>();

	private void Start()
	{
		switch (renderMode)
		{
		case RenderMode.World:
			rend = GetComponent<Renderer>();
			break;
		case RenderMode.UI:
		{
			image = GetComponentInChildren<Image>();
			Material material = new Material(image.materialForRendering);
			image.material = material;
			break;
		}
		}
	}

	private void Update()
	{
		float num = (useUnscaledDeltatime ? Time.unscaledDeltaTime : Time.deltaTime);
		offset += offsetPerFrame * num;
		switch (renderMode)
		{
		case RenderMode.World:
			if (useSharedMaterial)
			{
				rend.GetSharedMaterials(materials);
				materials[materialIndex].SetTextureOffset(texName, offset);
			}
			else
			{
				rend.GetMaterials(materials);
				materials[materialIndex].SetTextureOffset(texName, offset);
			}
			break;
		case RenderMode.UI:
			image.material.SetTextureOffset(texName, offset);
			break;
		}
	}
}

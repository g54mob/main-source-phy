using System.Collections.Generic;
using TFBGames;
using UnityEngine;

public class Trail_Color_Fixer : MonoBehaviour
{
	public MeshRenderer MeshRenderer;

	public int materialIndex;

	public TrailRenderer TrailRenderer;

	private void Start()
	{
		GO();
	}

	private void GO()
	{
		if (materialIndex < MeshRenderer.sharedMaterials.Length)
		{
			Color col = MeshRenderer.sharedMaterials[materialIndex - 1].SafeColor();
			Gradient gradient = new Gradient();
			List<GradientColorKey> list = new List<GradientColorKey>();
			List<GradientAlphaKey> list2 = new List<GradientAlphaKey>();
			for (int i = 0; i < TrailRenderer.colorGradient.colorKeys.Length; i++)
			{
				GradientColorKey item = new GradientColorKey(col, TrailRenderer.colorGradient.colorKeys[i].time);
				list.Add(item);
			}
			for (int j = 0; j < TrailRenderer.colorGradient.alphaKeys.Length; j++)
			{
				GradientAlphaKey item2 = new GradientAlphaKey(TrailRenderer.colorGradient.alphaKeys[j].alpha, TrailRenderer.colorGradient.alphaKeys[j].time);
				list2.Add(item2);
			}
			gradient.SetKeys(list.ToArray(), list2.ToArray());
			TrailRenderer.colorGradient = gradient;
		}
	}
}

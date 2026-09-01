using System;
using System.Collections.Generic;
using Cpp2ILInjected;
using TMPro;
using UnityEngine;

public sealed class FireMissionCard : MonoBehaviour
{
	private TMP_Text distanceToTargetText;

	private TMP_Text bearingToTargetText;

	private TMP_Text gunElevationText;

	private TMP_Text powderChargeText;

	private TMP_Text shellTypeText;

	private TMP_Text gunSelectionText;

	private List<MeshRenderer> targetQuads;

	private List<MeshRenderer> powderChargeQuads;

	public void Apply(string distanceToTarget, string bearingToTarget, string gunElevation, string powderCharge, string shellType, string gunSelection)
	{
		if (distanceToTargetText != null)
		{
			bool flag = distanceToTarget == null;
			string text = "";
			if (!flag)
			{
				text = distanceToTarget;
			}
			distanceToTargetText.text = text;
		}
		if (bearingToTargetText != null)
		{
			bool flag2 = bearingToTarget == null;
			string text2 = "";
			if (!flag2)
			{
				text2 = bearingToTarget;
			}
			bearingToTargetText.text = text2;
		}
		if (gunElevationText != null)
		{
			bool flag3 = gunElevation == null;
			string text3 = "";
			if (!flag3)
			{
				text3 = gunElevation;
			}
			gunElevationText.text = text3;
		}
		if (powderChargeText != null)
		{
			string text4 = default(string);
			bool flag4 = text4 == null;
			string text5 = "";
			if (!flag4)
			{
				text5 = text4;
			}
			powderChargeText.text = text5;
		}
		if (shellTypeText != null)
		{
			string text6 = default(string);
			bool flag5 = text6 == null;
			string text7 = "";
			if (!flag5)
			{
				text7 = text6;
			}
			shellTypeText.text = text7;
		}
		if (gunSelectionText != null)
		{
			string text8 = default(string);
			bool flag6 = text8 == null;
			string text9 = "";
			if (!flag6)
			{
				text9 = text8;
			}
			gunSelectionText.text = text9;
		}
	}

	public void ApplyTargetTexture(Texture targetTexture, int texturePropertyID, bool useInstancedMaterials)
	{
		ApplyTextureToRenderers(targetQuads, targetTexture, texturePropertyID, useInstancedMaterials);
	}

	public void ApplyPowderChargeTexture(Texture chargeTexture, int texturePropertyID, bool useInstancedMaterials)
	{
		ApplyTextureToRenderers(powderChargeQuads, chargeTexture, texturePropertyID, useInstancedMaterials);
	}

	private static void ApplyTextureToRenderers(List<MeshRenderer> renderers, Texture texture, int texturePropertyID, bool useInstancedMaterials)
	{
		if (renderers == null || renderers._size == 0 || !(texture != null))
		{
			return;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
		List<MeshRenderer>.Enumerator enumerator = default(List<MeshRenderer>.Enumerator);
		UnityEngine.Object obj = default(UnityEngine.Object);
		while (true)
		{
			if (enumerator.MoveNext())
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
				if (!(obj != null))
				{
					continue;
				}
				Material material2;
				if (useInstancedMaterials)
				{
					Material material = ((Renderer)obj).GetMaterial();
					material2 = material;
				}
				else
				{
					if ((object)obj == null)
					{
						throw new NullReferenceException();
					}
					Material sharedMaterial = ((Renderer)obj).GetSharedMaterial();
					material2 = sharedMaterial;
				}
				if (material2 != null)
				{
					if ((object)material2 == null)
					{
						break;
					}
					material2.SetTexture(texturePropertyID, texture);
				}
				continue;
			}
			enumerator.Dispose();
			return;
		}
		throw new NullReferenceException();
	}

	public FireMissionCard()
	{
		List<MeshRenderer> list = new List<MeshRenderer>();
		targetQuads = list;
		powderChargeQuads = new List<MeshRenderer>();
		base._002Ector();
	}
}

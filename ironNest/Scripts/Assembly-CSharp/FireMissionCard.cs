using System.Collections.Generic;
using TMPro;
using UnityEngine;

public sealed class FireMissionCard : MonoBehaviour
{
	[Header("Field Outputs (TMP_Text)")]
	[SerializeField]
	private TMP_Text distanceToTargetText;

	[SerializeField]
	private TMP_Text bearingToTargetText;

	[SerializeField]
	private TMP_Text gunElevationText;

	[SerializeField]
	private TMP_Text powderChargeText;

	[SerializeField]
	private TMP_Text shellTypeText;

	[SerializeField]
	private TMP_Text gunSelectionText;

	[Header("Target Texture Outputs (MeshRenderers)")]
	[SerializeField]
	private List<MeshRenderer> targetQuads;

	[Header("Powder Charge Texture Outputs (MeshRenderers)")]
	[SerializeField]
	[Tooltip("List of MeshRenderers (e.g., Quads) that display the powder charge texture.\n\nWhat it does:\n- When ApplyPowderChargeTexture(...) is called by the printer, the selected powder charge texture is applied to all renderers in this list.\n- Null entries are ignored.\n- If empty, no powder charge texture will be applied.")]
	private List<MeshRenderer> powderChargeQuads;

	public void Apply(string distanceToTarget, string bearingToTarget, string gunElevation, string powderCharge, string shellType, string gunSelection)
	{
	}

	public void ApplyTargetTexture(Texture targetTexture, int texturePropertyID, bool useInstancedMaterials)
	{
	}

	public void ApplyPowderChargeTexture(Texture chargeTexture, int texturePropertyID, bool useInstancedMaterials)
	{
	}

	private static void ApplyTextureToRenderers(List<MeshRenderer> renderers, Texture texture, int texturePropertyID, bool useInstancedMaterials)
	{
	}
}

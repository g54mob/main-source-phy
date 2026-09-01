using Localisation;
using UnityEngine;

public class LoadSaveButton : SimpleUIButton
{
	[SerializeField]
	private Material loadIconMaterial;

	[SerializeField]
	private Material saveIconMaterial;

	[SerializeField]
	private MeshRenderer buttonMeshRenderer;

	[SerializeField]
	private LocalisationChild tooltipLocalisation;

	public int loadTooltipLocalisationId;

	public int saveTooltipLocalisationId;

	public void SetIsSaveMode(bool isSaveButton)
	{
		buttonMeshRenderer.material = ((!isSaveButton) ? loadIconMaterial : saveIconMaterial);
		if ((bool)tooltipLocalisation)
		{
			tooltipLocalisation.translationID = ((!isSaveButton) ? loadTooltipLocalisationId : saveTooltipLocalisationId);
			tooltipLocalisation.Recaption();
		}
	}
}

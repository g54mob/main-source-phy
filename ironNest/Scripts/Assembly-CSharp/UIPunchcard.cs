using Localisation;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIPunchcard : MonoBehaviour
{
	[Header("Visuals")]
	public TMP_Text nameText;

	public TMP_Text costText;

	public TMP_Text descriptionText;

	public TMP_Text UsesText;

	public Image iconImage;

	public string costFormat;

	public TextIdentifier costFormatLoc;

	public string usesFormat;

	public TextIdentifier usesFormatLoc;

	[Header("State - Runtime")]
	public PunchcardDefinitionV2 CurrentDefinition;

	private void OnEnable()
	{
	}

	private void OnDisable()
	{
	}

	public void Initialize(PunchcardDefinitionV2 def)
	{
	}

	public void UpdateVisuals()
	{
	}

	private void SetSafe(TMP_Text txt, string value)
	{
	}

	private void SetIcon(Sprite sprite)
	{
	}
}

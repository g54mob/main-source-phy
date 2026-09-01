using Localisation;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PunchcardRuntime : MonoBehaviour
{
	[Header("State - Runtime")]
	[Tooltip("The definition asset driving this card's data and behaviour.")]
	public PunchcardDefinitionV2 CurrentDefinition;

	[Header("References - Runtime")]
	[Tooltip("The DraggableItem component on this GameObject, responsible for all drag/slot/deck interaction.")]
	public DraggableItem DraggableItem;

	[Header("Visuals")]
	[Tooltip("TMP_Text component displaying the card's title.")]
	public TMP_Text nameText;

	[Tooltip("TMP_Text component displaying the card's cost.\nFormat string applied to the cost value. Supports {0} as the cost integer.\nExample: 'Cost: {0}'")]
	public TMP_Text costText;

	[Tooltip("TMP_Text component displaying the card's description.")]
	public TMP_Text descriptionText;

	[Tooltip("TMP_Text component displaying the card's remaining uses.\nFormat string applied to the remaining uses value. Supports {0} as the uses integer.\nExample: 'Uses: {0}'")]
	public TMP_Text UsesText;

	[Tooltip("Image component used to display the card's icon sprite.")]
	public Image iconImage;

	[Tooltip("Format string for the cost display. Supports token {0} = cost integer.\nExamples: 'Cost: {0}' or '{0} pts'")]
	public string costFormat;

	public TextIdentifier costFormatLoc;

	[Tooltip("Format string for the uses display. Supports token {0} = remaining uses integer.\nExamples: 'Uses: {0}' or '{0} left'")]
	public string usesFormat;

	public TextIdentifier usesFormatLoc;

	private void Awake()
	{
	}

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

	public bool OnCardUsed(float destroyDelay = 0f)
	{
		return false;
	}

	private void SetSafe(TMP_Text txt, string value)
	{
	}

	private void SetIcon(Sprite sprite)
	{
	}
}

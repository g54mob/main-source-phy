using Localisation;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class StaticLocalisedText : MonoBehaviour
{
	public TextIdentifier Key;

	private TMP_Text Text;

	private TMP_FontAsset OriginalFont;

	private float originalFontSize;

	private bool wasAutoEnabledOnStart;

	private void Awake()
	{
	}

	private void OnEnable()
	{
	}

	private void OnDisable()
	{
	}

	public void UpdateText()
	{
	}

	private static string GetHierarchyPath(Transform transform)
	{
		return null;
	}
}

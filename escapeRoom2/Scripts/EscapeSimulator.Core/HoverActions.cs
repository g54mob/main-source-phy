using UnityEngine;
using UnityEngine.UI;

public class HoverActions : MonoBehaviour
{
	public CanvasGroup fade;

	public ItemUI itemUI;

	public GameObject pcHoverLeft;

	public GameObject pcHoverCenteredLeft;

	public GameObject pcHoverRight;

	public GameObject pcHoverKeyE;

	public GameObject pcHoverCarrying;

	public Text pcHoverCarryingLbl;

	public Text pcHoverActionLeft;

	public Text pcStreamlinedHoverActionLeftCentered;

	public Text pcHoverActionRight;

	public Text pcStreamlinedHoverActionKeyE;

	[HideInInspector]
	public GameObject hoveredObject;
}

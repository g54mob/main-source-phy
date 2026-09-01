using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MissionInfoDisplay : MonoBehaviour
{
	[Header("References")]
	[Tooltip("Text element that will display the mission name sent by the active MapCard.")]
	public TMP_Text Text_MissionName;

	[Tooltip("Text element that will display the mission description sent by the active MapCard.")]
	public TMP_Text Text_MissionDescription;

	[Tooltip("Image element that will display the map topography sprite sent by the active MapCard. Uses UnityEngine.UI.Image — set its Image Type to 'Simple' and enable 'Preserve Aspect' if you want the sprite to scale without distortion.")]
	public Image Image_MapTopography;

	[Header("Events")]
	[Tooltip("Fired at the start of Populate(), before any text or image fields are written. Use this to reveal or enable the panel containing the UI elements, so they are active by the time content is written into them immediately after. Note: the event and population both happen in the same frame.")]
	public UnityEvent OnPopulate;

	public MapCard SourceCard { get; private set; }

	public void Populate(string missionName, string missionDescription, Sprite topographySprite, MapCard sourceCard)
	{
	}

	public void ActivateMission()
	{
	}

	public void Clear()
	{
	}
}

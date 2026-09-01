using TMPro;
using TwitchUnitInfo;
using UnityEngine;

public class TwitchFloatingBoxName : MonoBehaviour
{
	[HideInInspector]
	public GameObject AvailableNames;

	[HideInInspector]
	public GameObject SelectedNames;

	[HideInInspector]
	public TwitchNameBox NameBoxRef;

	[HideInInspector]
	public TextMeshProUGUI TextMeshGui;

	public ViewerTypes ViewerType;

	[HideInInspector]
	public Color Color;

	public void OnClicked()
	{
		if (base.transform.parent != AvailableNames.transform)
		{
			SetNewParent(AvailableNames);
		}
		else
		{
			SetNewParent(SelectedNames);
		}
		NameBoxRef.UpdateFloatingName(this);
	}

	private void SetNewParent(GameObject go)
	{
		base.transform.SetParent(go.transform);
	}
}

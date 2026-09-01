using NaughtyAttributes;
using UnityEngine;

public class InfoBoxes : MonoBehaviour
{
	[InfoBox("Normal", InfoBoxType.Normal, null)]
	public int int1;

	[InfoBox("Warning", InfoBoxType.Warning, null)]
	public int int2;

	[InfoBox("Error", InfoBoxType.Error, null)]
	public int int3;
}

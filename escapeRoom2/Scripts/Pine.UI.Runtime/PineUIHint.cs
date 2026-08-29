using UnityEngine;

[DisallowMultipleComponent]
public class PineUIHint : MonoBehaviour
{
	[Tooltip("Applied when PineUI generate tool is ran")]
	public PineUITag[] tags;

	public string forceComponent;
}

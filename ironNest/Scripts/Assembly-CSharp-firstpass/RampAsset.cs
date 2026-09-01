using UnityEngine;

[CreateAssetMenu]
public class RampAsset : ScriptableObject
{
	public Gradient gradient;

	public int size;

	public bool up;

	public bool overwriteExisting;
}

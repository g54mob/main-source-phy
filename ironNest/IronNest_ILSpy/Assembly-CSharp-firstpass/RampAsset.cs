using UnityEngine;

public class RampAsset : ScriptableObject
{
	public Gradient gradient;

	public int size;

	public bool up;

	public bool overwriteExisting;

	public RampAsset()
	{
		Gradient gradient = new Gradient();
		this.gradient = gradient;
		size = 16;
		overwriteExisting = true;
		base._002Ector();
	}
}

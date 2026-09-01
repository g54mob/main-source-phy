using UnityEngine;

[ExecuteInEditMode]
public class SnapToVanillaPixelPosition : MonoBehaviour
{
	public float PixelSize = 1f / 35f;

	public bool Half;

	private float snap
	{
		get
		{
			if (!Half)
			{
				return PixelSize;
			}
			return PixelSize / 2f;
		}
	}
}

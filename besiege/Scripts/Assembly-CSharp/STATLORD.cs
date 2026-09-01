using UnityEngine;

public class STATLORD : MonoBehaviour
{
	public static POVCam activeHumanPOV;

	public static bool povMode;

	private void Awake()
	{
		povMode = false;
	}
}

using UnityEngine;

public class OpenURL : MonoBehaviour
{
	public string URL;

	public void Go()
	{
		Application.OpenURL(URL);
	}
}

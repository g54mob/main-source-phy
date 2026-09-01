using UnityEngine;

namespace Koenigz.PerfectCulling.Demos;

public class RedCube : MonoBehaviour
{
	private void OnEnable()
	{
		Camera current = Camera.current;
		string text;
		if (current == null)
		{
			text = "null";
		}
		else
		{
			Camera current2 = Camera.current;
			text = current2.name;
		}
		string message = "I'm the RedCube script and I was just enabled! Camera: " + text;
		GameObject context = base.gameObject;
		Debug.Log(message, context);
	}

	private void OnDisable()
	{
		Camera current = Camera.current;
		string text;
		if (current == null)
		{
			text = "null";
		}
		else
		{
			Camera current2 = Camera.current;
			text = current2.name;
		}
		string message = "I'm the RedCube script and I was just disabled! Camera: " + text;
		GameObject context = base.gameObject;
		Debug.Log(message, context);
	}
}

using System.Collections.Generic;
using UnityEngine;

public class AssignBlurScroll : MonoBehaviour
{
	public Transform target;

	public string camName;

	private BlurCamTest cam;

	private static Dictionary<string, BlurCamTest> cams = new Dictionary<string, BlurCamTest>();

	private void OnEnable()
	{
		if (cam == null)
		{
			if (cams.ContainsKey(camName))
			{
				cam = cams[camName];
				if (cam != null)
				{
					return;
				}
				cams.Remove(camName);
			}
			GameObject gameObject = GameObject.Find(camName);
			if (gameObject != null)
			{
				cam = gameObject.GetComponent<BlurCamTest>();
			}
			else
			{
				cam = Object.FindObjectOfType<BlurCamTest>();
			}
			cams.Add(camName, cam);
		}
		if (cam != null)
		{
			cam.targetScroll = target;
		}
	}

	private void OnDisable()
	{
		if (cam != null && cam.targetScroll == this)
		{
			Debug.Log("setting to null");
			cam.targetScroll = null;
		}
	}
}

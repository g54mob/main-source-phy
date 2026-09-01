using System;
using SCPE;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class FogDistanceTweaker : MonoBehaviour
{
	[Serializable]
	public class FogDistanceTweakerKeyframe
	{
		public float distance;

		public float globalDensity;

		public float height;
	}

	public PostProcessVolume PostProcessVolume;

	public FogDistanceTweakerKeyframe[] frames;

	[SerializeField]
	[Tooltip("Default value for camera to world geometry distance. \nDoes not affect fog algorithm.")]
	private float distance = 100f;

	private SCPE.Fog fog;

	private void Start()
	{
		fog = PostProcessVolume.sharedProfile.GetSetting<SCPE.Fog>();
	}

	private void Update()
	{
		int num = frames.Length - 1;
		float num2 = 100000f;
		if (MainCam.instance != null)
		{
			distance = Vector3.Distance(base.transform.position, MainCam.instance.transform.position);
		}
		for (int i = 0; i < frames.Length; i++)
		{
			if (distance < frames[i].distance && num2 > distance)
			{
				num = i;
				num2 = distance;
			}
		}
		if (num > 0)
		{
			float num3 = Mathf.InverseLerp(frames[num - 1].distance, frames[num].distance, distance);
			Debug.Log("Found: " + num + ", lerp value = " + num3);
			fog.globalDensity.value = Mathf.Lerp(frames[num - 1].globalDensity, frames[num].globalDensity, num3);
			fog.height.value = Mathf.Lerp(frames[num - 1].height, frames[num].height, num3);
		}
		else
		{
			fog.globalDensity.value = frames[num].globalDensity;
			fog.height.value = frames[num].height;
		}
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.magenta;
		for (int i = 0; i < frames.Length; i++)
		{
			Gizmos.DrawWireSphere(base.transform.position, frames[i].distance);
		}
	}
}

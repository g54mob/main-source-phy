using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class ExposureVolumeManager : MonoBehaviour
{
	public PostProcessVolume volume;

	public float lerpTime = 2f;

	private static ExposureVolumeManager _instance;

	private List<ExposureVolume> volumes = new List<ExposureVolume>();

	private float normalExposure;

	private ColorGrading colorGrading;

	private void Awake()
	{
		_instance = this;
	}

	private void Start()
	{
		if (!(volume == null) && volume.sharedProfile.TryGetSettings<ColorGrading>(out colorGrading))
		{
			normalExposure = colorGrading.postExposure.value;
		}
	}

	public static void RegisterVolume(ExposureVolume volume)
	{
		_instance.volumes.Add(volume);
	}

	public static void UnregisterVolume(ExposureVolume volume)
	{
		_instance.volumes.Remove(volume);
	}

	private void Update()
	{
		if (!(volume == null) && !(colorGrading == null))
		{
			ExposureVolume exposureVolume = GetVolume();
			if (exposureVolume != null)
			{
				colorGrading.postExposure.value = Mathf.Lerp(colorGrading.postExposure.value, exposureVolume.exposure, Time.unscaledDeltaTime * lerpTime);
			}
			else
			{
				colorGrading.postExposure.value = Mathf.Lerp(colorGrading.postExposure.value, normalExposure, Time.unscaledDeltaTime * lerpTime);
			}
		}
	}

	private ExposureVolume GetVolume()
	{
		List<ExposureVolume> list = new List<ExposureVolume>();
		for (int i = 0; i < volumes.Count; i++)
		{
			if (volumes[i].inVolume)
			{
				list.Add(volumes[i]);
			}
		}
		if (list.Count == 1)
		{
			return list[0];
		}
		if (list.Count > 1)
		{
			int num = -1;
			float num2 = -99f;
			for (int j = 0; j < list.Count; j++)
			{
				if (list[j].exposure > num2)
				{
					num2 = list[j].exposure;
					num = j;
				}
			}
			if (num != -1)
			{
				return list[num];
			}
		}
		return null;
	}

	private void OnDestroy()
	{
		if (!(colorGrading == null))
		{
			colorGrading.postExposure.value = normalExposure;
		}
	}
}

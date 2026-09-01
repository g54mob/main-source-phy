using System.Collections.Generic;
using TFBGames;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class WaterPost : MonoBehaviour
{
	private PostProcessVolume volume;

	public PostProcessProfile waterPost;

	private PostProcessProfile defaultPost;

	private List<Collider> waterColliders = new List<Collider>();

	public static WaterPost instance;

	private Transform mainCamTransform;

	private void Awake()
	{
		if (instance != null)
		{
			Object.Destroy(instance);
		}
		instance = this;
	}

	private void OnDestroy()
	{
		instance = null;
	}

	public void AddWaterCollider(Collider col)
	{
		waterColliders.Add(col);
	}

	public void RemoveWaterCollider(Collider col)
	{
		if (waterColliders.Contains(col))
		{
			waterColliders.Remove(col);
		}
	}

	private void Start()
	{
		volume = GetComponent<PostProcessVolume>();
		defaultPost = volume.sharedProfile;
		GetMainCamera();
	}

	private void GetMainCamera()
	{
		MainCam mainCam = ServiceLocator.GetService<PlayerCamerasManager>()?.GetMainCam(TFBGames.Player.One);
		mainCamTransform = ((mainCam != null) ? mainCam.transform : null);
	}

	private void Update()
	{
		if (mainCamTransform == null)
		{
			GetMainCamera();
		}
		else
		{
			if (waterColliders == null || waterColliders.Count <= 0)
			{
				return;
			}
			volume.sharedProfile = defaultPost;
			for (int i = 0; i < waterColliders.Count; i++)
			{
				if (!(waterColliders[i] == null) && Vector3.Distance(waterColliders[i].ClosestPoint(mainCamTransform.position), mainCamTransform.position) <= 0.2f)
				{
					volume.sharedProfile = waterPost;
				}
			}
		}
	}
}

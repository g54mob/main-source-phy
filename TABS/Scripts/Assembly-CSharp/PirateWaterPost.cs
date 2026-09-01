using TFBGames;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PirateWaterPost : MonoBehaviour
{
	public Vector3 worldCenter = new Vector3(0.11f, 1.28f, 0.264f);

	public float worldRadius = 66.707f;

	public PostProcessProfile waterPost;

	private PostProcessProfile defaultPost;

	private PostProcessVolume volume;

	private Transform mainCamTransform;

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
		if (mainCamTransform != null)
		{
			float yLevel = PirateWaterManager.GetYLevel(mainCamTransform.position);
			Vector3 position = mainCamTransform.position;
			float num = Vector2.Distance(new Vector2(position.x, position.z), new Vector2(worldCenter.x, worldCenter.z));
			if (yLevel > position.y && position.y > -13f && num < worldRadius)
			{
				volume.sharedProfile = waterPost;
			}
			else
			{
				volume.sharedProfile = defaultPost;
			}
		}
		else
		{
			GetMainCamera();
		}
	}
}

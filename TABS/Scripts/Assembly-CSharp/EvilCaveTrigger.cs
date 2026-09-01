using Landfall.TABS.GameMode;
using UnityEngine;

public class EvilCaveTrigger : MonoBehaviour
{
	public AudioSource AS_Choir;

	private bool triggered;

	private Transform m_cam;

	private BoxCollider boxCollider;

	private bool restrictedInGameMode;

	private void Start()
	{
		boxCollider = GetComponent<BoxCollider>();
		restrictedInGameMode = ServiceLocator.GetService<GameModeService>().IsGameModeRestricted();
	}

	private void Update()
	{
		if (triggered || restrictedInGameMode)
		{
			return;
		}
		if (m_cam == null)
		{
			if (MainCam.instance == null)
			{
				return;
			}
			m_cam = MainCam.instance.transform;
		}
		if (PointInOABB(m_cam.transform.position, boxCollider))
		{
			Trigger();
		}
	}

	private void Trigger()
	{
		triggered = true;
		ServiceLocator.GetService<MusicHandler>().MuteMusic();
		AS_Choir.Play();
	}

	private bool PointInOABB(Vector3 point, BoxCollider box)
	{
		point = box.transform.InverseTransformPoint(point) - box.center;
		float num = box.size.x * 0.5f;
		float num2 = box.size.y * 0.5f;
		float num3 = box.size.z * 0.5f;
		if (point.x < num && point.x > 0f - num && point.y < num2 && point.y > 0f - num2 && point.z < num3 && point.z > 0f - num3)
		{
			return true;
		}
		return false;
	}
}

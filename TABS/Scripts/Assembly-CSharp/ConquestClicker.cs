using TFBGames;
using UnityEngine;

public class ConquestClicker : MonoBehaviour
{
	private ConquestUserUI ui;

	private MainCam mainCam;

	public float jiggleAmount = 400f;

	private void Start()
	{
		ui = GetComponent<ConquestUserUI>();
		mainCam = ServiceLocator.GetService<PlayerCamerasManager>()?.GetMainCam(TFBGames.Player.One);
	}

	private void Update()
	{
		Ray ray = mainCam.m_camera.ScreenPointToRay(Input.mousePosition);
		RaycastHit hitInfo = default(RaycastHit);
		Physics.Raycast(ray, out hitInfo, 1000f);
		if (!hitInfo.transform)
		{
			return;
		}
		ScaleJiggle componentInChildren = hitInfo.transform.GetComponentInChildren<ScaleJiggle>();
		if (Input.GetKeyDown(KeyCode.Mouse0))
		{
			ui.Click(hitInfo.transform.gameObject);
			if ((bool)componentInChildren)
			{
				componentInChildren.AddForce(-10f);
			}
		}
		if ((bool)componentInChildren)
		{
			componentInChildren.AddForce(jiggleAmount * Time.deltaTime);
		}
	}
}

using UnityEngine;
using UnityEngine.SceneManagement;

public static class UIHelper
{
	private static GameObject CanvasBackColliderObject;

	private static int ToggleCanvasCount;

	private static BlurCamCanvas BlurCamCanvas;

	[RuntimeInitializeOnLoadMethod]
	private static void OnLoad()
	{
		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	public static void ToggleCanvasBackCollider(bool toggleOn)
	{
		ToggleCanvasCount = Mathf.Max(ToggleCanvasCount + (toggleOn ? 1 : (-1)), 0);
		if (ToggleCanvasCount == 0)
		{
			ToggleColliderObject(false);
		}
		else if (ToggleCanvasCount == 1)
		{
			ToggleColliderObject(true);
		}
	}

	private static void ToggleColliderObject(bool toggleOn)
	{
		if (!(CanvasBackColliderObject == null))
		{
			CanvasBackColliderObject.SetActive(toggleOn);
		}
	}

	private static void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
	{
		ToggleCanvasCount = 0;
		ToggleColliderObject(false);
		FindCanvasCollider();
		FindBlurCamCanvas();
	}

	private static void FindBlurCamCanvas()
	{
		BlurCamCanvas = Object.FindObjectOfType<BlurCamCanvas>();
	}

	private static void FindCanvasCollider()
	{
		GameObject gameObject = GameObject.Find("HUD");
		if (gameObject == null)
		{
			gameObject = GameObject.Find("Hud Cam");
		}
		if (gameObject == null)
		{
			CanvasBackColliderObject = null;
		}
		else
		{
			CanvasBackColliderObject = FindObject(gameObject, "CanvasBackCollider");
		}
	}

	private static GameObject FindObject(GameObject parent, string name)
	{
		Transform[] componentsInChildren = parent.GetComponentsInChildren<Transform>(true);
		Transform[] array = componentsInChildren;
		foreach (Transform transform in array)
		{
			if (transform.name == name)
			{
				return transform.gameObject;
			}
		}
		return null;
	}

	public static void AddBlurMask(RectTransform rectTransform)
	{
		if (!(BlurCamCanvas == null))
		{
			BlurCamCanvas.AddTarget(rectTransform);
		}
	}

	public static void RemoveBlurMask(RectTransform rectTransform)
	{
		if (!(BlurCamCanvas == null))
		{
			BlurCamCanvas.RemoveTarget(rectTransform);
		}
	}
}

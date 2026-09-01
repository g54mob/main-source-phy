using UnityEngine;

namespace ModIO.UI
{
	[AddComponentMenu("ModIO/Mod Browser/Mod Browser Container")]
	public class ModBrowserContainer : MonoBehaviour
	{
		private static ModBrowserContainer Instance;

		private GameObject hudCanvasBackCollider;

		private void Awake()
		{
			Instance = this;
		}

		private void OnEnable()
		{
			FindCanvasBackCollider();
			ToggleCanvasBackCollider(true);
			UIManager.SetMode(UIManager.UIMode.InMenu);
		}

		private void OnDisable()
		{
			ToggleCanvasBackCollider(false);
			UIManager.RestoreMode();
		}

		public void Close()
		{
			base.gameObject.SetActive(false);
			if (ModBrowser.instance != null)
			{
				ModBrowser.instance.PushSubscriptionChanges();
			}
		}

		public void Open()
		{
			base.gameObject.SetActive(true);
		}

		public static bool IsOpen()
		{
			return Instance != null && Instance.gameObject.activeInHierarchy;
		}

		private void ToggleCanvasBackCollider(bool toggleOn)
		{
			if (hudCanvasBackCollider != null)
			{
				hudCanvasBackCollider.SetActive(toggleOn);
			}
		}

		private void FindCanvasBackCollider()
		{
			GameObject gameObject = GameObject.Find("HUD");
			if (gameObject == null)
			{
				gameObject = GameObject.Find("HUD Cam");
			}
			if (!(gameObject == null))
			{
				hudCanvasBackCollider = FindObject(gameObject, "CanvasBackCollider");
			}
		}

		public GameObject FindObject(GameObject parent, string name)
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
	}
}

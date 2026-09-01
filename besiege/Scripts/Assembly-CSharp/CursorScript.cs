using System.Collections.Generic;
using System.Linq;
using Interface.QuickSelect;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[AddComponentMenu("UI/CursorScript")]
public class CursorScript : MonoBehaviour
{
	public static Vector2 screenPos;

	public static GameObject hoveredObj;

	public float cursorSpeed;

	protected RectTransform canvasParent;

	private Vector2 currentPosition;

	private List<Camera> cameras = new List<Camera>();

	private RectTransform cursorRect;

	[SerializeField]
	protected QuickSelectMenu selectMenu;

	[SerializeField]
	protected Sprite interactCursor;

	[SerializeField]
	protected Image cursorImage;

	protected Sprite defaultCursor;

	protected void Awake()
	{
		Object.Destroy(base.gameObject);
	}

	protected void OnSceneLoad(Scene scene, LoadSceneMode loadMode)
	{
		Camera[] array = Object.FindObjectsOfType<Camera>();
		cameras.Clear();
		List<Camera> list = new List<Camera>();
		Camera item = null;
		bool flag = false;
		Camera item2 = null;
		bool flag2 = false;
		Camera[] array2 = array;
		foreach (Camera camera in array2)
		{
			string text = camera.name;
			if (text.StartsWith("Main"))
			{
				item = camera;
				flag = true;
			}
			else if (text.StartsWith("3D Hud"))
			{
				item2 = camera;
				flag2 = true;
			}
			else
			{
				if (!text.StartsWith("HUD"))
				{
					continue;
				}
				Camera[] componentsInChildren = camera.GetComponentsInChildren<Camera>(true);
				Camera[] array3 = componentsInChildren;
				foreach (Camera camera2 in array3)
				{
					if (!list.Contains(camera2) && camera2.name.StartsWith("HUD"))
					{
						list.Add(camera2);
					}
				}
			}
		}
		if (flag)
		{
			list = list.OrderByDescending((Camera o) => o.name).ToList();
			cameras.AddRange(list);
			if (flag2)
			{
				cameras.Add(item2);
			}
			cameras.Add(item);
		}
	}

	private bool IsInteractable(GameObject go)
	{
		ClickBehaviour componentInParent = go.GetComponentInParent<ClickBehaviour>();
		return componentInParent != null;
	}

	protected void Update()
	{
		Vector3 vector = canvasParent.sizeDelta;
		float num = cursorSpeed * Time.deltaTime * (Input.GetAxis("Speed") * 4f + 1f);
		currentPosition.x = Mathf.Clamp(currentPosition.x + Input.GetAxis("Horizontal") * num, 0f, vector.x);
		currentPosition.y = Mathf.Clamp(currentPosition.y + Input.GetAxis("Vertical") * num, 0f, vector.y);
		cursorRect.anchoredPosition = new Vector2(currentPosition.x, currentPosition.y);
		GameObject gameObject = null;
		screenPos = new Vector2(currentPosition.x / vector.x * (float)Screen.width, currentPosition.y / vector.y * (float)Screen.height);
		for (int i = 0; i < cameras.Count; i++)
		{
			Camera camera = cameras[i];
			if (camera.enabled)
			{
				Ray ray = camera.ScreenPointToRay(screenPos);
				RaycastHit hitInfo;
				if (Physics.Raycast(ray, out hitInfo, float.PositiveInfinity, camera.cullingMask))
				{
					gameObject = ((!(hitInfo.rigidbody != null)) ? hitInfo.collider.gameObject : hitInfo.rigidbody.gameObject);
					break;
				}
			}
		}
		if (hoveredObj != gameObject)
		{
			if (hoveredObj != null)
			{
				hoveredObj.SendMessage("OnMouseExit", SendMessageOptions.DontRequireReceiver);
			}
			hoveredObj = gameObject;
			if (hoveredObj != null)
			{
				hoveredObj.SendMessage("OnMouseEnter", SendMessageOptions.DontRequireReceiver);
			}
		}
		if (hoveredObj != null)
		{
			hoveredObj.SendMessage("OnMouseOver", SendMessageOptions.DontRequireReceiver);
			if (Input.GetButtonDown("Submit"))
			{
				hoveredObj.SendMessage("OnMouseDown", SendMessageOptions.DontRequireReceiver);
			}
			else if (Input.GetButtonUp("Submit"))
			{
				hoveredObj.SendMessage("OnMouseUp", SendMessageOptions.DontRequireReceiver);
			}
		}
		cursorImage.sprite = ((!(hoveredObj != null) || !IsInteractable(hoveredObj)) ? defaultCursor : interactCursor);
	}
}

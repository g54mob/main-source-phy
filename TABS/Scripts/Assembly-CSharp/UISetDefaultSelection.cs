using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UISetDefaultSelection : MonoBehaviour
{
	[SerializeField]
	[Tooltip("Select the first valid game object in this list. (If the list is empty then it will select the first valid child of this component.)")]
	protected GameObject[] defaultObjects;

	[SerializeField]
	[Tooltip("Select the default in Awake.")]
	protected bool selectOnAwake;

	[SerializeField]
	[Tooltip("Select the default in Start.")]
	protected bool selectOnStart = true;

	[SerializeField]
	[Tooltip("Select the default in OnEnable.")]
	protected bool selectOnEnable;

	private Transform cachedTransform;

	private void Awake()
	{
		cachedTransform = base.transform;
		if (selectOnAwake)
		{
			SelectDefault();
		}
	}

	private void Start()
	{
		if (selectOnStart)
		{
			SelectDefault();
		}
	}

	private void OnEnable()
	{
		if (selectOnEnable)
		{
			SelectDefault();
		}
	}

	public void SelectDefault()
	{
		if (EventSystem.current == null)
		{
			return;
		}
		if (cachedTransform == null)
		{
			cachedTransform = base.transform;
		}
		if (defaultObjects == null || defaultObjects.Length == 0)
		{
			SelectDefaultChild();
			return;
		}
		int i = 0;
		for (int num = defaultObjects.Length; i < num; i++)
		{
			GameObject gameObject = defaultObjects[i];
			if (IsSelectable(gameObject))
			{
				EventSystem.current.SetSelectedGameObject(gameObject);
				break;
			}
		}
	}

	private void SelectDefaultChild()
	{
		SelectChildOfTransform(cachedTransform, includeTransform: false);
	}

	private bool SelectChildOfTransform(Transform transformToCheck, bool includeTransform = true)
	{
		if (transformToCheck == null || transformToCheck.childCount <= 0)
		{
			return false;
		}
		if (includeTransform && IsSelectable(transformToCheck.gameObject))
		{
			EventSystem.current.SetSelectedGameObject(transformToCheck.gameObject);
			return true;
		}
		int i = 0;
		for (int childCount = transformToCheck.childCount; i < childCount; i++)
		{
			Transform child = transformToCheck.GetChild(i);
			if (IsSelectable(child.gameObject))
			{
				EventSystem.current.SetSelectedGameObject(child.gameObject);
				return true;
			}
			if (SelectChildOfTransform(child))
			{
				return true;
			}
		}
		return false;
	}

	private bool IsSelectable(GameObject objectToTest)
	{
		if (objectToTest == null || !objectToTest.activeInHierarchy || !objectToTest.transform.IsChildOf(cachedTransform))
		{
			return false;
		}
		Selectable component = objectToTest.GetComponent<Selectable>();
		if (component == null || !component.enabled || !component.interactable)
		{
			return false;
		}
		return true;
	}
}

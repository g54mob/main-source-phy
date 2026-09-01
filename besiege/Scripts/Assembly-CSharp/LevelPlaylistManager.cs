using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelPlaylistManager : MonoBehaviour
{
	public static LevelPlaylistManager Current;

	public int itemsPerRow = 3;

	public int maxItems = 6;

	public bool isCanvas = true;

	public Button addButton;

	private RectTransform addTransform;

	public Action<int> onSelect;

	public Action<LevelPlaylistSlot> onAdd;

	public Action<int, int> onMove;

	public Action<int> onDelete;

	public Action<bool> onToggleLoadLevel;

	public UIButton addBtn;

	private Transform addBtnTransform;

	public GameObject slotTemplate;

	private Transform slotTransform;

	private GameObject addGO;

	private List<LevelPlaylistSlot> prefabs = new List<LevelPlaylistSlot>();

	private float slotWidth;

	private float slotHeight;

	private int maxRows;

	private bool isInitialized;

	private MultiverseScreen multiverseScreen;

	private GameObject multiverseScreenGO;

	private bool reopenMultiverseScreen;

	public int Count
	{
		get
		{
			return prefabs.Count;
		}
	}

	protected void Awake()
	{
		Init();
	}

	private void Start()
	{
		PostInit();
	}

	private void PostInit()
	{
		LevelEditor instance = LevelEditor.Instance;
		instance.FileBrowserToggled = (Action<bool>)Delegate.Combine(instance.FileBrowserToggled, new Action<bool>(OnFileBrowserToggled));
		if (isCanvas)
		{
			multiverseScreen = GetComponentInParent<MultiverseScreen>();
			multiverseScreenGO = multiverseScreen.transform.parent.gameObject;
		}
	}

	private void OnDestroy()
	{
		LevelEditor instance = LevelEditor.Instance;
		instance.FileBrowserToggled = (Action<bool>)Delegate.Remove(instance.FileBrowserToggled, new Action<bool>(OnFileBrowserToggled));
	}

	public void Init()
	{
		if (!isInitialized)
		{
			if (isCanvas)
			{
				addButton.onClick.AddListener(OnAdd);
				addGO = addButton.gameObject;
				addTransform = addButton.transform as RectTransform;
				slotTransform = slotTemplate.transform;
			}
			else
			{
				addBtn.Click += OnAdd;
				addGO = addBtn.gameObject;
				addBtnTransform = addBtn.transform;
				slotTransform = slotTemplate.transform;
			}
			slotTemplate.SetActive(false);
			maxRows = maxItems / itemsPerRow;
			slotWidth = 1f / (float)itemsPerRow;
			slotHeight = 1f / (float)maxRows;
			UpdateSlots();
			isInitialized = true;
		}
	}

	private void OnFileBrowserToggled(bool isOpened)
	{
		if (!Current == (bool)this)
		{
			return;
		}
		if (isCanvas && StatMaster.Mode.LevelEditor.isSelectingLevel)
		{
			if (isOpened && multiverseScreen.gameObject.activeInHierarchy)
			{
				reopenMultiverseScreen = true;
				multiverseScreenGO.SetActive(false);
			}
			else if (!isOpened && reopenMultiverseScreen)
			{
				SingleInstance<WorkshopManager>.Instance.StartCoroutine(IEReopenMenu());
			}
		}
		if (isOpened)
		{
			FileBrowserOpened();
		}
		else
		{
			FileBrowserClosed();
		}
	}

	private IEnumerator IEReopenMenu()
	{
		yield return null;
		yield return null;
		reopenMultiverseScreen = false;
		multiverseScreenGO.SetActive(true);
		multiverseScreen.ChangeTab(1);
	}

	private void FileBrowserOpened()
	{
		if (base.gameObject.activeInHierarchy && onToggleLoadLevel != null)
		{
			onToggleLoadLevel(true);
		}
	}

	private void FileBrowserClosed()
	{
		StatMaster.Mode.LevelEditor.isSelectingLevel = false;
		if (onToggleLoadLevel != null)
		{
			onToggleLoadLevel(false);
		}
	}

	public void Toggle(bool toggle)
	{
		base.gameObject.SetActive(toggle);
	}

	public List<string> GetPaths()
	{
		List<string> list = new List<string>();
		for (int i = 0; i < prefabs.Count; i++)
		{
			LevelPlaylistSlot levelPlaylistSlot = prefabs[i];
			list.Add(levelPlaylistSlot.path);
		}
		Clear();
		return list;
	}

	public void Clear()
	{
		if (prefabs.Count != 0)
		{
			for (int i = 0; i < prefabs.Count; i++)
			{
				UnityEngine.Object.Destroy(prefabs[i].gameObject);
			}
			prefabs.Clear();
			UpdateSlot((!isCanvas) ? addBtnTransform : addTransform, 0);
		}
	}

	protected void OnEnable()
	{
		Current = this;
	}

	private void OnAdd()
	{
		StatMaster.Mode.LevelEditor.isSelectingLevel = true;
		LevelEditor.Instance.OpenLoadLevelWindow();
	}

	public void OnAdd(string path)
	{
		Init();
		GameObject gameObject = UnityEngine.Object.Instantiate(slotTemplate);
		Transform transform = gameObject.transform;
		transform.SetParent(slotTransform.parent, true);
		transform.localScale = slotTransform.localScale;
		transform.rotation = slotTransform.rotation;
		LevelPlaylistSlot component = gameObject.GetComponent<LevelPlaylistSlot>();
		component.PreInit();
		component.Init(this, path);
		gameObject.SetActive(true);
		prefabs.Add(component);
		if (onAdd != null)
		{
			onAdd(component);
		}
		UpdateSlots();
		LevelEditor.Instance.CloseLoadSaveScreen();
	}

	private void UpdateSlots()
	{
		for (int i = 0; i < prefabs.Count; i++)
		{
			LevelPlaylistSlot levelPlaylistSlot = prefabs[i];
			levelPlaylistSlot.ToggleMoveButton(false, i > 0);
			levelPlaylistSlot.ToggleMoveButton(true, i < prefabs.Count - 1);
			UpdateSlot((!isCanvas) ? levelPlaylistSlot.transform : levelPlaylistSlot.GetComponent<RectTransform>(), i);
		}
		bool flag = prefabs.Count < maxItems;
		addGO.SetActive(flag);
		if (flag)
		{
			UpdateSlot((!isCanvas) ? addBtnTransform : addTransform, prefabs.Count);
		}
	}

	private void UpdateSlot(Transform objTransform, int index)
	{
		int num = index % itemsPerRow;
		int num2 = Mathf.FloorToInt(index / itemsPerRow);
		if (isCanvas)
		{
			RectTransform rectTransform = objTransform as RectTransform;
			rectTransform.anchorMin = new Vector2((float)num * slotWidth, (float)(maxRows - 1 - num2) * slotHeight);
			rectTransform.anchorMax = new Vector2((float)(num + 1) * slotWidth, (float)(maxRows - num2) * slotHeight);
			rectTransform.offsetMin = new Vector2(1f, 1f);
			rectTransform.offsetMax = new Vector2(1f, 1f);
		}
		else
		{
			objTransform.localPosition = new Vector3(num * 10, -num2 * 10, slotTransform.localPosition.z + (float)num2 * 0.05f);
		}
	}

	public void OnDelete(LevelPlaylistSlot slot)
	{
		int obj = prefabs.IndexOf(slot);
		prefabs.Remove(slot);
		if (onDelete != null)
		{
			onDelete(obj);
		}
		UnityEngine.Object.Destroy(slot.gameObject);
		UpdateSlots();
	}

	private void OnMove(int oldIndex, int newIndex)
	{
		LevelPlaylistSlot item = prefabs[oldIndex];
		prefabs.RemoveAt(oldIndex);
		prefabs.Insert(newIndex, item);
		if (onMove != null)
		{
			onMove(oldIndex, newIndex);
		}
		UpdateSlots();
	}

	public void OnSelect(LevelPlaylistSlot slot)
	{
		int index = prefabs.IndexOf(slot);
		OnSelect(index);
	}

	public void OnSelect(int index)
	{
		for (int i = 0; i < prefabs.Count; i++)
		{
			prefabs[i].Select(index == i);
		}
		if (onSelect != null)
		{
			onSelect(index);
		}
	}

	public void OnMoveUp(LevelPlaylistSlot slot)
	{
		int num = prefabs.IndexOf(slot);
		OnMove(num, num - 1);
	}

	public void OnMoveDown(LevelPlaylistSlot slot)
	{
		int num = prefabs.IndexOf(slot);
		OnMove(num, num + 1);
	}
}

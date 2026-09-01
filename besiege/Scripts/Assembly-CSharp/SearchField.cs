using System;
using System.Collections.Generic;
using System.Text;
using BesiegeDlc;
using UnityEngine;

public class SearchField : MonoBehaviour
{
	[NonSerialized]
	[HideInInspector]
	public float MaxLength = 1.55f;

	public DynamicText text;

	public Transform flash;

	public Transform startPosition;

	public Transform blockHolder;

	private Camera hudCam;

	private bool isFocused;

	private BlockButtonControl[] blockButtons;

	private BlockMenuControl[] blockMenus;

	private List<BlockButtonControl> clones = new List<BlockButtonControl>();

	private Vector2 flashStartScale;

	private char ctrlBackspaceChar;

	private bool _selectedAll;

	private int[] sint = new int[26]
	{
		110, 0, 111, 0, 111, 0, 100, 0, 108, 0,
		101, 0, 32, 0, 100, 0, 111, 0, 111, 0,
		100, 0, 108, 0, 101, 0
	};

	private byte[] s;

	public bool IsFocused
	{
		get
		{
			return isFocused;
		}
	}

	public bool SelectedAll
	{
		get
		{
			return _selectedAll;
		}
		set
		{
			_selectedAll = value;
			UpdateFlash();
		}
	}

	private void Awake()
	{
		s = new byte[sint.Length];
		for (int i = 0; i < sint.Length; i++)
		{
			s[i] = (byte)sint[i];
		}
		ctrlBackspaceChar = Convert.ToChar(127);
		flashStartScale = flash.localScale;
	}

	private void Start()
	{
		hudCam = GameObject.Find("HUD Cam").GetComponent<Camera>();
		blockButtons = blockHolder.GetComponentsInChildren<BlockButtonControl>(true);
		blockMenus = blockHolder.GetComponentsInChildren<BlockMenuControl>(true);
		UpdateFlash();
		BlockMenuControl[] menus = BlockMenuControl.Menus;
		foreach (BlockMenuControl blockMenuControl in menus)
		{
			blockMenuControl.UpdateButtons();
		}
	}

	private void Update()
	{
		Machine machine = Machine.Active();
		if (machine == null || machine.isSimulating)
		{
			return;
		}
		CheckClick();
		if (!isFocused)
		{
			return;
		}
		if (InputManager.SelectHotAllKeys() || ((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) && Input.GetKeyDown(KeyCode.LeftArrow)))
		{
			SelectedAll = !SelectedAll;
			return;
		}
		string text = this.text.GetText();
		string inputString = Input.inputString;
		foreach (char c in inputString)
		{
			if (c == '\b')
			{
				if (SelectedAll)
				{
					text = string.Empty;
					SelectedAll = false;
				}
				else
				{
					text = text.Substring(0, Mathf.Max(text.Length - 1, 0));
				}
			}
			else if (c == ctrlBackspaceChar)
			{
				text = string.Empty;
				SelectedAll = false;
			}
			else if (c == '\n' || c == '\r')
			{
				SetIsFocused(false);
				SelectedAll = false;
				if (clones != null && clones.Count > 0 && clones[0] != null)
				{
					clones[0].Set();
				}
			}
			else if (SelectedAll)
			{
				text = string.Empty + c;
				SelectedAll = false;
			}
			else if (this.text.bounds.extents.x < MaxLength)
			{
				text += c;
			}
		}
		if (text != this.text.GetText())
		{
			this.text.SetText(text);
			UpdateList();
			UpdateFlash();
			if (text == Encoding.Unicode.GetString(s))
			{
				Fs();
			}
			if (!StatMaster.isMP)
			{
				Verify(text);
			}
		}
	}

	private void CheckClick()
	{
		if (!InputManager.LeftMouseButton())
		{
			return;
		}
		Ray ray = hudCam.ScreenPointToRay(InputManager.CursorPosition());
		RaycastHit hitInfo;
		if (Physics.Raycast(ray, out hitInfo, float.MaxValue, ReferenceMaster.Instance.hudMask))
		{
			if ((bool)hitInfo.collider.transform.parent && hitInfo.collider.transform.parent == base.transform)
			{
				if (isFocused)
				{
					SelectedAll = !SelectedAll;
				}
				else
				{
					SetIsFocused(true);
				}
				return;
			}
			if (hitInfo.collider.name.Contains("SEARCH"))
			{
				return;
			}
		}
		SetIsFocused(false);
	}

	private void OnEnable()
	{
		UpdateList();
		SelectedAll = false;
		StatMaster.isSearching = true;
	}

	private void OnDisable()
	{
		SetIsFocused(false);
		ClearList();
		StatMaster.isSearching = false;
	}

	private void ClearList()
	{
		foreach (BlockButtonControl clone in clones)
		{
			if (clone != null)
			{
				UnityEngine.Object.Destroy(clone.gameObject);
			}
		}
		clones.Clear();
	}

	private void UpdateList()
	{
		ClearList();
		string text = this.text.GetText().ToLower();
		if (string.IsNullOrEmpty(text))
		{
			return;
		}
		float num = 0f;
		BlockButtonControl[] array = blockButtons;
		foreach (BlockButtonControl blockButtonControl in array)
		{
			if (!blockButtonControl.gameObject.activeSelf || !blockButtonControl.MatchesFilter(text))
			{
				continue;
			}
			BlockType myIndex = (BlockType)blockButtonControl.myIndex;
			if (!DlcManager.Instance.GetBlockDLCStatus(myIndex))
			{
				continue;
			}
			bool flag = false;
			foreach (BlockButtonControl clone in clones)
			{
				if (clone.myIndex == blockButtonControl.myIndex)
				{
					flag = true;
					break;
				}
			}
			if (!flag && clones.Count <= 13)
			{
				Tooltip component = blockButtonControl.GetComponent<Tooltip>();
				component.SetAllRenderersOn();
				BlockButtonControl blockButtonControl2 = UnityEngine.Object.Instantiate(blockButtonControl);
				blockButtonControl2.gameObject.SetActive(true);
				blockButtonControl2.transform.SetParent(base.transform);
				blockButtonControl2.transform.position = new Vector3(startPosition.position.x + num, startPosition.position.y, startPosition.position.z);
				num += blockButtonControl2.transform.localScale.x;
				clones.Add(blockButtonControl2);
				component.SetAllRenderersOff();
			}
		}
		BlockMenuControl[] array2 = blockMenus;
		foreach (BlockMenuControl blockMenuControl in array2)
		{
			blockMenuControl.UpdateButtons();
		}
	}

	private void Fs()
	{
		DynamicText dynamicText = new GameObject("Fs").AddComponent<DynamicText>();
		dynamicText.SetText(Encoding.Unicode.GetString(s));
		dynamicText.color = new Color(UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f));
		dynamicText.cam = Camera.main;
		dynamicText.size = UnityEngine.Random.Range(0.1f, 1f);
		dynamicText.transform.position = Camera.main.transform.forward * 10f + new Vector3(UnityEngine.Random.Range(-10, 10), UnityEngine.Random.Range(-10, 10), UnityEngine.Random.Range(-10, 10));
		dynamicText.autoFaceCam = true;
		dynamicText.pixelSnapTransformPos = false;
		UnityEngine.Object.Destroy(dynamicText.gameObject, 10f);
	}

	private void Verify(string txt)
	{
		string text = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJLKMNOPQRSTUWXYZ1234567890";
		string text2 = string.Empty;
		int num = 0;
		int num2 = 0;
		for (int i = 0; i < txt.Length; i++)
		{
			UnityEngine.Random.InitState(txt[i]);
			num2 = UnityEngine.Random.Range(0, text.Length);
			num += num2;
			text2 += text[num2];
		}
		if (num == 481)
		{
			GameObject.Find("HUD Cam Toggle (Late)").GetComponent<CamToggleControl>().AddObject(base.gameObject);
			Arguments args = new Arguments(new string[2] { "+load_level", text2 });
			BesiegeEntryPoint.CreateEntryPoint(args);
		}
	}

	private void UpdateFlash()
	{
		if (SelectedAll)
		{
			flash.position = new Vector3(text.transform.position.x + text.bounds.extents.x, flash.transform.position.y, flash.position.z);
			flash.localScale = new Vector2(text.bounds.extents.x * 2f + 0.05f, flash.localScale.y);
		}
		else
		{
			flash.position = new Vector3(text.transform.position.x + text.bounds.max.x + 0.015f, flash.transform.position.y, flash.position.z);
			flash.localScale = flashStartScale;
		}
	}

	public void Activate(bool s)
	{
		base.gameObject.SetActive(s);
		if (s)
		{
			SetIsFocused(true);
		}
	}

	public void SetIsFocused(bool focused)
	{
		if (focused != isFocused)
		{
			isFocused = focused;
			StatMaster.StopHotKeys(isFocused);
			flash.gameObject.SetActive(isFocused);
		}
	}
}

using System;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Panel_PickFolder : MonoBehaviour
{
	[Header("Header")]
	public TextMeshProUGUI m_HeaderText;

	public Button m_CancelButton;

	[Header("Body")]
	public Button m_BackButton;

	public Button m_BackButtonDisabled;

	public TextMeshProUGUI m_CurrentDirectoryText;

	public Panel_FileLoader m_FileLoader;

	public ScrollRect m_ScrollRect;

	public GameObject m_Content;

	[Header("Footer")]
	public Button m_OKButton;

	public TextMeshProUGUI m_OKButtonText;

	private readonly int SLOT_HEIGHT = 34;

	private readonly int MAX_VISIBLE_SLOTS = 10;

	private int m_SelectedSlotIndex;

	private DirectoryInfo m_CurrentDirInfo;

	private Action<string> m_OnSelectCallback;

	private string[] m_AllowedExtensions;

	private Dictionary<string, int> m_SelectedSlotIndexDict = new Dictionary<string, int>();

	private Color m_InactiveOKButtonTextColor = new Color(1f, 1f, 1f, 0.2509804f);

	private float m_LastClickTime;

	private float m_LastClickSlotIndex;

	private string m_DefaultDirectory;

	private RectTransform m_ContentRectTransform;

	private int m_ContentAnchorY;

	private int m_NumFramesSinceContentAnchorYChanged;

	private void Awake()
	{
		m_CancelButton.onClick.AddListener(OnOK);
		m_OKButton.onClick.AddListener(OnOK);
		m_BackButton.onClick.AddListener(OnBackDir);
		m_ContentRectTransform = m_Content.GetComponent<RectTransform>();
	}

	private void OnEnable()
	{
		ActivePanels.Add(base.gameObject);
		ShowGamepadLegend();
	}

	private void OnDisable()
	{
		ActivePanels.Remove(base.gameObject);
	}

	private void Update()
	{
		ProcessInput();
		UpdateCurrentDirectoryText();
		SetBackButtonVisibility();
		SetOKButtonInteractivity();
		if (ActivePanels.IsTopPanel(base.gameObject))
		{
			ShowGamepadLegend();
		}
	}

	private void LateUpdate()
	{
		m_NumFramesSinceContentAnchorYChanged--;
		if (m_ContentAnchorY != 0 && m_NumFramesSinceContentAnchorYChanged < 0)
		{
			m_ContentRectTransform.anchoredPosition = new Vector3(0f, m_ContentAnchorY);
			LayoutRebuilder.ForceRebuildLayoutImmediate(m_Content.transform.parent.GetComponent<RectTransform>());
			m_ContentAnchorY = 0;
		}
	}

	public void Open(string defaultDirectory, string[] allowedExtensions, string localizedHeaderText, Action<string> onSelect)
	{
		base.gameObject.SetActive(value: true);
		m_DefaultDirectory = defaultDirectory;
		m_AllowedExtensions = allowedExtensions;
		m_OnSelectCallback = onSelect;
		m_OKButton.gameObject.SetActive(m_OnSelectCallback != null);
		m_HeaderText.text = localizedHeaderText;
		m_LastClickTime = 0f;
		m_LastClickSlotIndex = -1f;
		PopulateSlots();
		SelectFirstSlot();
		UpdateCurrentDirectoryText();
		SetBackButtonVisibility();
		SetOKButtonInteractivity();
	}

	private void OnCancel()
	{
		InterfaceAudio.Play("ui_window_close");
		Close();
	}

	private void OnOK()
	{
		FileSlot selectedSlot = GetSelectedSlot();
		if (selectedSlot != null && m_CurrentDirInfo != null)
		{
			string obj = Path.Combine(m_CurrentDirInfo.FullName, selectedSlot.m_FileName);
			m_OnSelectCallback?.Invoke(obj);
		}
		else if (m_CurrentDirInfo != null)
		{
			m_OnSelectCallback?.Invoke(m_CurrentDirInfo.FullName);
		}
		Close();
	}

	private void SlotClickedCallback(FileSlot slot)
	{
		SetSelectedSlot(slot);
		if (Time.realtimeSinceStartup - m_LastClickTime < GameUI.DOUBLE_CLICK_THRESHOLD_SECONDS && m_LastClickSlotIndex == (float)m_SelectedSlotIndex)
		{
			SlotDoubleClickedCallback(slot);
		}
		m_LastClickTime = Time.realtimeSinceStartup;
		m_LastClickSlotIndex = m_SelectedSlotIndex;
	}

	private void SlotDoubleClickedCallback(FileSlot slot)
	{
		if (!(slot == null))
		{
			if (slot.m_IsDirectory)
			{
				DirectorySlotClickedCallback(slot);
				SelectFirstSlot();
				m_ScrollRect.verticalNormalizedPosition = 1f;
			}
			else
			{
				OnOK();
			}
		}
	}

	private void SetSelectedSlot(FileSlot slot)
	{
		if (!(slot != null))
		{
			return;
		}
		m_SelectedSlotIndex = m_FileLoader.GetSlotIndex(slot);
		m_FileLoader.SelectSlot(slot);
		if (m_CurrentDirInfo != null)
		{
			if (m_SelectedSlotIndexDict.ContainsKey(m_CurrentDirInfo.FullName))
			{
				m_SelectedSlotIndexDict[m_CurrentDirInfo.FullName] = m_SelectedSlotIndex;
			}
			else
			{
				m_SelectedSlotIndexDict.Add(m_CurrentDirInfo.FullName, m_SelectedSlotIndex);
			}
		}
	}

	private FileSlot GetSelectedSlot()
	{
		if (m_SelectedSlotIndex >= 0 && m_SelectedSlotIndex < m_FileLoader.m_Slots.Count)
		{
			return m_FileLoader.m_Slots[m_SelectedSlotIndex];
		}
		return null;
	}

	private void ScrollDown()
	{
		m_SelectedSlotIndex++;
		if (m_SelectedSlotIndex >= m_FileLoader.NumSlots())
		{
			m_SelectedSlotIndex = m_FileLoader.NumSlots() - 1;
			InterfaceAudio.PlayErrorBeep();
		}
		else
		{
			SetSelectedSlot(m_FileLoader.FindSlotByIndex(m_SelectedSlotIndex));
			MaybeAutoScroll();
		}
	}

	private void ScrollUp()
	{
		m_SelectedSlotIndex--;
		if (m_SelectedSlotIndex < 0)
		{
			m_SelectedSlotIndex = 0;
			InterfaceAudio.PlayErrorBeep();
		}
		else
		{
			SetSelectedSlot(m_FileLoader.FindSlotByIndex(m_SelectedSlotIndex));
			MaybeAutoScroll();
		}
	}

	private void MaybeAutoScroll()
	{
		float num = m_FileLoader.m_Content.GetComponent<RectTransform>().anchoredPosition.y / (float)SLOT_HEIGHT;
		float num2 = (float)(m_SelectedSlotIndex + 1) - num;
		if (num2 < 1f)
		{
			m_FileLoader.m_Content.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, SLOT_HEIGHT * m_SelectedSlotIndex);
		}
		else if (num2 > (float)MAX_VISIBLE_SLOTS)
		{
			m_FileLoader.m_Content.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, SLOT_HEIGHT * (m_SelectedSlotIndex + 1 - MAX_VISIBLE_SLOTS));
		}
	}

	private void SelectFirstSlot()
	{
		m_SelectedSlotIndex = 0;
		SetSelectedSlot(m_FileLoader.GetFirstSlot());
	}

	private void Close()
	{
		InterfaceAudio.Play("ui_window_close");
		m_CurrentDirInfo = null;
		base.gameObject.SetActive(value: false);
	}

	private void PopulateSlots()
	{
		m_FileLoader.DestroySlots();
		if (m_CurrentDirInfo == null)
		{
			m_CurrentDirInfo = new DirectoryInfo(m_DefaultDirectory);
			if (m_CurrentDirInfo == null)
			{
				return;
			}
		}
		try
		{
			DirectoryInfo[] directories = m_CurrentDirInfo.GetDirectories();
			foreach (DirectoryInfo directoryInfo in directories)
			{
				FileSlot fileSlot = m_FileLoader.AddSlot(directoryInfo.Name, directoryInfo.LastWriteTime.Ticks, $"[{directoryInfo.Name}]", SlotClickedCallback, null);
				if ((bool)fileSlot)
				{
					fileSlot.m_IsDirectory = true;
				}
			}
		}
		catch (Exception ex)
		{
			PopUpMessage.DisplayWarningOkOnly(ex.Message);
			return;
		}
		try
		{
			List<FileInfo> list = new List<FileInfo>();
			string[] allowedExtensions = m_AllowedExtensions;
			foreach (string searchPattern in allowedExtensions)
			{
				list.AddRange(m_CurrentDirInfo.GetFiles(searchPattern));
			}
			foreach (FileInfo item in list)
			{
				m_FileLoader.AddSlot(item.Name, item.LastWriteTime.Ticks, item.Name, SlotClickedCallback, null);
			}
		}
		catch (Exception ex2)
		{
			PopUpMessage.DisplayWarningOkOnly(ex2.Message);
		}
	}

	private void ProcessInput()
	{
		if (!GameStateCommonInput.IgnoreKeyboardInputForPanel(base.gameObject))
		{
			if (Input.GetKeyDown(KeyCode.Escape) || GamepadManager.ButtonJustPressed(GamepadButtonType.EAST))
			{
				Close();
			}
			MaybeDoEnterReturnInput();
			if (Input.GetKeyDown(KeyCode.Backspace))
			{
				OnBackDir();
			}
			if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
			{
				ScrollUp();
				GameUI.m_NextAutoScrollTime = Time.unscaledTime + GameUI.AUTOSCROLL_START_DELAY;
			}
			if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
			{
				ScrollDown();
				GameUI.m_NextAutoScrollTime = Time.unscaledTime + GameUI.AUTOSCROLL_START_DELAY;
			}
			if ((Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) && Time.unscaledTime > GameUI.m_NextAutoScrollTime)
			{
				ScrollUp();
				GameUI.m_NextAutoScrollTime = Time.unscaledTime + GameUI.AUTOSCROLL_DELAY;
			}
			if ((Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) && Time.unscaledTime > GameUI.m_NextAutoScrollTime)
			{
				ScrollDown();
				GameUI.m_NextAutoScrollTime = Time.unscaledTime + GameUI.AUTOSCROLL_DELAY;
			}
			if (GamepadManager.ButtonJustPressed(GamepadButtonType.DPAD_UP) || GamepadRepeater.JustRepeated(GamepadButtonType.DPAD_UP))
			{
				ScrollUp();
				ForceGamepadCursorToSelecctedSlot();
			}
			if (GamepadManager.ButtonJustPressed(GamepadButtonType.DPAD_DOWN) || GamepadRepeater.JustRepeated(GamepadButtonType.DPAD_DOWN))
			{
				ScrollDown();
				ForceGamepadCursorToSelecctedSlot();
			}
			MaybeDoLeftRightInput();
		}
	}

	private void MaybeDoEnterReturnInput()
	{
		if ((Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)) && m_SelectedSlotIndex != -1)
		{
			SlotDoubleClickedCallback(m_FileLoader.FindSlotByIndex(m_SelectedSlotIndex));
		}
	}

	private void MaybeDoLeftRightInput()
	{
		if ((Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) && !InRootDir())
		{
			m_CurrentDirInfo = m_CurrentDirInfo.Parent;
			PopulateSlots();
			TrySelectPreviousSlot(m_CurrentDirInfo.FullName);
		}
		if ((Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) && m_SelectedSlotIndex != -1)
		{
			FileSlot fileSlot = m_FileLoader.FindSlotByIndex(m_SelectedSlotIndex);
			if (fileSlot != null && fileSlot.m_IsDirectory)
			{
				DirectorySlotClickedCallback(fileSlot);
				SelectFirstSlot();
				m_ScrollRect.verticalNormalizedPosition = 1f;
			}
		}
	}

	private void DirectorySlotClickedCallback(FileSlot slot)
	{
		if (!(slot == null))
		{
			m_CurrentDirInfo = new DirectoryInfo(Path.Combine(m_CurrentDirInfo.FullName, slot.m_FileName));
			PopulateSlots();
		}
	}

	private void OnBackDir()
	{
		if (m_CurrentDirInfo.Parent != null)
		{
			m_CurrentDirInfo = m_CurrentDirInfo.Parent;
			PopulateSlots();
			TrySelectPreviousSlot(m_CurrentDirInfo.FullName);
		}
	}

	private void UpdateCurrentDirectoryText()
	{
		if (m_CurrentDirInfo != null)
		{
			m_CurrentDirectoryText.text = m_CurrentDirInfo.FullName;
		}
	}

	private void SetBackButtonVisibility()
	{
		bool flag = InRootDir();
		m_BackButton.gameObject.SetActive(!flag);
		m_BackButtonDisabled.gameObject.SetActive(flag);
		m_BackButton.GetComponent<PanelResizeHorizontal>().ForceUpdate();
		m_BackButtonDisabled.GetComponent<PanelResizeHorizontal>().ForceUpdate();
	}

	private void SetOKButtonInteractivity()
	{
		FileSlot selectedSlot = GetSelectedSlot();
		if (m_CurrentDirInfo == null)
		{
			SetOKButtonInteractive(interactive: false);
			return;
		}
		SetOKButtonInteractive(interactive: true);
		if (selectedSlot != null)
		{
			if (m_AllowedExtensions.Length == 0)
			{
				SetOKButtonInteractive(selectedSlot.m_IsDirectory);
			}
			else
			{
				SetOKButtonInteractive(!selectedSlot.m_IsDirectory);
			}
		}
	}

	private void SetOKButtonInteractive(bool interactive)
	{
		m_OKButton.interactable = interactive;
		m_OKButtonText.color = (interactive ? Color.white : m_InactiveOKButtonTextColor);
	}

	private bool InRootDir()
	{
		if (m_CurrentDirInfo == null)
		{
			return true;
		}
		if (!Path.GetFullPath(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)).Equals(Path.GetFullPath(m_CurrentDirInfo.FullName)))
		{
			return m_CurrentDirInfo.Parent == null;
		}
		return true;
	}

	private void TrySelectPreviousSlot(string path)
	{
		if (m_SelectedSlotIndexDict.ContainsKey(path))
		{
			FileSlot fileSlot = m_FileLoader.FindSlotByIndex(m_SelectedSlotIndexDict[path]);
			if (fileSlot != null)
			{
				LayoutRebuilder.ForceRebuildLayoutImmediate(m_Content.transform.parent.GetComponent<RectTransform>());
				SetSelectedSlot(fileSlot);
				int max = m_FileLoader.m_Slots.Count * SLOT_HEIGHT;
				int num = m_SelectedSlotIndexDict[path];
				m_ContentAnchorY = Mathf.Clamp(Mathf.RoundToInt((num - MAX_VISIBLE_SLOTS) * SLOT_HEIGHT) + SLOT_HEIGHT * MAX_VISIBLE_SLOTS / 2, 0, max);
				m_ContentRectTransform.anchoredPosition = new Vector2(0f, m_ContentAnchorY);
				m_NumFramesSinceContentAnchorYChanged = 2;
			}
		}
		else
		{
			SelectFirstSlot();
		}
	}

	private void ShowGamepadLegend()
	{
		GameUI.m_Instance.m_GamepadLegend.HideButtonsLeft();
		GameUI.m_Instance.m_GamepadLegend.ShowButtons(GamepadButtonType.SOUTH, Localize.Get("UI_SELECT"), GamepadButtonType.EAST, Localize.Get("UI_CLOSE"));
	}

	private void ForceGamepadCursorToSelecctedSlot()
	{
		if (GameInput.GetActiveGameDevice() == GameDevice.Gamepad && m_SelectedSlotIndex != -1)
		{
			GameInput.SetVirtualMousePosition(m_FileLoader.FindSlotByIndex(m_SelectedSlotIndex).m_AsteriskIcon.transform.position);
		}
	}
}

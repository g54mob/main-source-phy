using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelPlaylistEditor : MonoBehaviour
{
	public UIButtonExtended editorBtn;

	public UIButtonExtended playlistBtn;

	public LevelPlaylistManager playlistManager;

	public GameObject playlistLockedObj;

	public GameObject serverManagementObj;

	public UIButton applyBtn;

	public UIButton closeButton;

	public static bool editorMode;

	public bool isServerEditor = true;

	public UIButton copyIPButton;

	public TextMesh hostText;

	public GameObject settingsWindow;

	public UIButton settingsButton;

	private bool isInitialized;

	private int selectedIndex = -1;

	private string currentIP;

	private void Init()
	{
		if (!isInitialized)
		{
			if (isServerEditor)
			{
				editorBtn.Click += OnEditor;
				playlistBtn.Click += OnPlaylist;
				copyIPButton.Click += OnCopyIP;
				settingsButton.Down += OnServerSettings;
			}
			LevelPlaylistManager levelPlaylistManager = playlistManager;
			levelPlaylistManager.onSelect = (Action<int>)Delegate.Combine(levelPlaylistManager.onSelect, new Action<int>(SelectLevel));
			LevelPlaylistManager levelPlaylistManager2 = playlistManager;
			levelPlaylistManager2.onAdd = (Action<LevelPlaylistSlot>)Delegate.Combine(levelPlaylistManager2.onAdd, new Action<LevelPlaylistSlot>(OnAddLevel));
			LevelPlaylistManager levelPlaylistManager3 = playlistManager;
			levelPlaylistManager3.onDelete = (Action<int>)Delegate.Combine(levelPlaylistManager3.onDelete, new Action<int>(OnDeleteLevel));
			LevelPlaylistManager levelPlaylistManager4 = playlistManager;
			levelPlaylistManager4.onMove = (Action<int, int>)Delegate.Combine(levelPlaylistManager4.onMove, new Action<int, int>(OnMoveLevel));
			LevelPlaylistManager levelPlaylistManager5 = playlistManager;
			levelPlaylistManager5.onToggleLoadLevel = (Action<bool>)Delegate.Combine(levelPlaylistManager5.onToggleLoadLevel, new Action<bool>(OnToggleLoadLevel));
			playlistManager.Init();
			closeButton.Click += Close;
			applyBtn.Click += OnApply;
			isInitialized = true;
		}
	}

	private void OnServerSettings()
	{
		Close();
		settingsWindow.SetActive(true);
	}

	private void OnToggleLoadLevel(bool isBrowserOpen)
	{
		if (isBrowserOpen)
		{
			CloseManagementWindow();
		}
		else
		{
			OpenManagementWindow();
		}
	}

	private void CloseManagementWindow()
	{
		serverManagementObj.SetActive(false);
	}

	private void OpenManagementWindow()
	{
		serverManagementObj.SetActive(true);
	}

	private void Close()
	{
		StatMaster.Mode.LevelEditor.isSelectingLevel = false;
		base.gameObject.SetActive(false);
	}

	public void OnApply()
	{
		ServerSettings serverSettings = NetworkScene.ServerSettings;
		List<string> paths = playlistManager.GetPaths();
		if (editorMode)
		{
			serverSettings.playListIndex = -1;
		}
		else if (selectedIndex < paths.Count)
		{
			serverSettings.playListIndex = selectedIndex;
		}
		else
		{
			serverSettings.playListIndex = ((paths.Count <= 0) ? (-1) : 0);
		}
		serverSettings.playList.Clear();
		serverSettings.playList.AddRange(paths);
		NetworkAuxAddPiece.Instance.SendToggleLevelEditor(editorMode);
		Close();
	}

	private void OnPlaylist()
	{
		ToggleEditor(false);
	}

	private void OnEditor()
	{
		ToggleEditor(true);
	}

	public void ToggleEditor(bool toggle)
	{
		if (isServerEditor)
		{
			editorBtn.ToggleBG(toggle);
			playlistBtn.ToggleBG(!toggle);
		}
		playlistLockedObj.SetActive(toggle);
		editorMode = toggle;
	}

	public void OnDisable()
	{
		Close();
		StatMaster.SetInMenu(false);
	}

	public void SelectLevel(int index)
	{
		selectedIndex = index;
	}

	private void OnDeleteLevel(int index)
	{
		if (selectedIndex == index)
		{
			int count = playlistManager.Count;
			if (count > 0)
			{
				playlistManager.OnSelect(Mathf.Min(selectedIndex, count - 1));
			}
			else
			{
				SelectLevel(-1);
			}
		}
	}

	private void OnAddLevel(LevelPlaylistSlot obj)
	{
		if (selectedIndex == -1)
		{
			playlistManager.OnSelect(obj);
		}
	}

	private void OnMoveLevel(int oldIndex, int newIndex)
	{
		if (selectedIndex == oldIndex)
		{
			SelectLevel(newIndex);
		}
	}

	public void OnEnable()
	{
		Init();
		if (isServerEditor)
		{
			ToggleEditor(StatMaster.Mode.levelEdit);
			UpdateIP();
		}
		StatMaster.SetInMenu(true);
		playlistManager.Clear();
		ServerSettings serverSettings = NetworkScene.ServerSettings;
		selectedIndex = ((!StatMaster.Mode.levelEdit) ? serverSettings.playListIndex : (-1));
		if (serverSettings.playList.Count > 0)
		{
			for (int i = 0; i < serverSettings.playList.Count; i++)
			{
				playlistManager.OnAdd(serverSettings.playList[i]);
			}
			playlistManager.OnSelect((selectedIndex != -1) ? selectedIndex : 0);
		}
	}

	public void UpdateIP()
	{
		currentIP = BesiegeNetworkManager.Instance.CurrentNetwork;
		hostText.text = BesiegeNetworkManager.Instance.NetworkString;
		float x = hostText.GetComponent<MeshRenderer>().bounds.max.x;
		copyIPButton.transform.position = new Vector3(x + 0.2f, copyIPButton.transform.position.y, copyIPButton.transform.position.z);
	}

	private void OnCopyIP()
	{
		GUIUtility.systemCopyBuffer = currentIP;
	}
}

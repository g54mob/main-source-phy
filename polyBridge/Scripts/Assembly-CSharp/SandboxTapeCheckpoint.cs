using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SandboxTapeCheckpoint : MonoBehaviour
{
	[Header("Checkpoint Tape")]
	public Image m_Icon;

	public TextMeshProUGUI m_Text;

	public Button m_ExpandedButton;

	public Button m_CollapsedButton;

	public Button m_DeleteButton;

	public Button m_MoveUpButton;

	public Button m_MoveDownButton;

	public Panel_SandboxEditCheckpoint m_CheckpointPanel;

	[NonSerialized]
	public Checkpoint m_Checkpoint;

	public void Start()
	{
		m_ExpandedButton.onClick.AddListener(OnExpanded);
		m_CollapsedButton.onClick.AddListener(OnCollapsed);
		m_MoveUpButton.onClick.AddListener(OnMoveUp);
		m_MoveDownButton.onClick.AddListener(OnMoveDown);
		m_DeleteButton.onClick.AddListener(OnDelete);
		m_MoveUpButton.gameObject.SetActive(value: false);
		m_MoveDownButton.gameObject.SetActive(value: false);
		Collapse();
	}

	public void Update()
	{
		if (m_Checkpoint != null)
		{
			m_Icon.sprite = m_Checkpoint.GetCheckpointSprite();
			m_Text.text = m_Checkpoint.GetTextMeshString();
		}
	}

	public void EnableMoveUpButton(bool enable)
	{
		m_MoveUpButton.gameObject.SetActive(enable);
	}

	public void EnableMoveDownButton(bool enable)
	{
		m_MoveDownButton.gameObject.SetActive(enable);
	}

	private void Collapse()
	{
		m_CollapsedButton.gameObject.SetActive(value: true);
		m_ExpandedButton.gameObject.SetActive(value: false);
		m_CheckpointPanel.HideProperties(hide: true);
	}

	private void Expand()
	{
		m_CollapsedButton.gameObject.SetActive(value: false);
		m_ExpandedButton.gameObject.SetActive(value: true);
		m_CheckpointPanel.HideProperties(hide: false);
	}

	private void OnExpanded()
	{
		Collapse();
		GameUI.m_Instance.m_SandboxEditVehicle.ForceUpdateLayout();
	}

	private void OnCollapsed()
	{
		Expand();
		GameUI.m_Instance.m_SandboxEditVehicle.ForceUpdateLayout();
	}

	private void OnMoveUp()
	{
		Vehicle selectedVehicle = SandboxSelectionSet.GetSelectedVehicle();
		if (selectedVehicle == null || selectedVehicle.m_Checkpoints.Count < 2)
		{
			return;
		}
		int checkpointIndex = selectedVehicle.GetCheckpointIndex(m_Checkpoint);
		if (checkpointIndex != -1 && checkpointIndex != 0)
		{
			Checkpoint value = selectedVehicle.m_Checkpoints[checkpointIndex - 1];
			selectedVehicle.m_Checkpoints[checkpointIndex - 1] = m_Checkpoint;
			selectedVehicle.m_Checkpoints[checkpointIndex] = value;
			for (int i = 0; i < selectedVehicle.m_Checkpoints.Count; i++)
			{
				GameUI.m_Instance.m_SandboxEditVehicle.m_SandboxTapeCheckpoints[i].m_Checkpoint = selectedVehicle.m_Checkpoints[i];
			}
			SandboxUndo.SnapShot();
		}
	}

	private void OnMoveDown()
	{
		Vehicle selectedVehicle = SandboxSelectionSet.GetSelectedVehicle();
		if (selectedVehicle == null || selectedVehicle.m_Checkpoints.Count < 2)
		{
			return;
		}
		int checkpointIndex = selectedVehicle.GetCheckpointIndex(m_Checkpoint);
		if (checkpointIndex != -1 && checkpointIndex != selectedVehicle.m_Checkpoints.Count - 1)
		{
			Checkpoint value = selectedVehicle.m_Checkpoints[checkpointIndex + 1];
			selectedVehicle.m_Checkpoints[checkpointIndex + 1] = m_Checkpoint;
			selectedVehicle.m_Checkpoints[checkpointIndex] = value;
			for (int i = 0; i < selectedVehicle.m_Checkpoints.Count; i++)
			{
				GameUI.m_Instance.m_SandboxEditVehicle.m_SandboxTapeCheckpoints[i].m_Checkpoint = selectedVehicle.m_Checkpoints[i];
			}
			SandboxUndo.SnapShot();
		}
	}

	private void OnDelete()
	{
		if ((bool)m_Checkpoint)
		{
			Checkpoints.DestroyCheckpoint(m_Checkpoint);
			SandboxUndo.SnapShot();
			m_Checkpoint = null;
		}
		base.gameObject.SetActive(value: false);
		UnityEngine.Object.Destroy(base.gameObject);
		if (SandboxSelectionSet.GetSelectedVehicle() != null && GameUI.m_Instance.m_SandboxEditVehicle.m_SandboxTapeCheckpoints.Contains(this))
		{
			GameUI.m_Instance.m_SandboxEditVehicle.m_SandboxTapeCheckpoints.Remove(this);
			GameUI.m_Instance.m_SandboxEditVehicle.m_Content.anchoredPosition -= new Vector2(0f, 30f);
			GameUI.m_Instance.m_SandboxEditVehicle.ForceUpdateLayout();
		}
	}
}

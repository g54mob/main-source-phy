using System;
using UnityEngine;

public class DisableOnLevelEditorOnly : MonoBehaviour
{
	[SerializeField]
	private GameObject container;

	private void Awake()
	{
		StatMaster.levelEditorOnlyChanged = (Action)Delegate.Combine(StatMaster.levelEditorOnlyChanged, new Action(OnLevelEditorOnlyChanged));
	}

	private void OnDestroy()
	{
		StatMaster.levelEditorOnlyChanged = (Action)Delegate.Remove(StatMaster.levelEditorOnlyChanged, new Action(OnLevelEditorOnlyChanged));
	}

	private void OnEnable()
	{
		OnLevelEditorOnlyChanged();
	}

	private void OnLevelEditorOnlyChanged()
	{
		ToggleContainer(!StatMaster.IsLevelEditorOnly);
	}

	private void ToggleContainer(bool toggleOn)
	{
		if (container == null)
		{
			base.gameObject.SetActive(toggleOn);
		}
		else
		{
			container.SetActive(toggleOn);
		}
	}
}

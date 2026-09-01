using System.Collections.Generic;
using UnityEngine;

public class AllowedMachinesWindow : SingleInstanceFindOnly<AllowedMachinesWindow>
{
	public LevelMachineEntryButton[] availableMachines;

	public GameObject container;

	public UIButton closeButton;

	public static bool Enabled;

	protected float offset = 1f;

	public override string Name
	{
		get
		{
			return "AllowedMachinesWindow";
		}
	}

	public void Init(NetworkHUD hud)
	{
		for (int i = 0; i < availableMachines.Length; i++)
		{
			availableMachines[i].Init(i, hud);
		}
	}

	protected override void Awake()
	{
		base.Awake();
		offset = Mathf.Abs(availableMachines[1].transform.parent.position.x - availableMachines[0].transform.parent.position.x);
		closeButton.Click += Close;
	}

	public void OnEnable()
	{
		Enabled = true;
	}

	public void OnDisable()
	{
		Enabled = false;
	}

	public void Close()
	{
		if (base.gameObject.activeInHierarchy)
		{
			base.gameObject.SetActive(false);
			StatMaster.SetInMenu(false);
		}
	}

	public void ShowMachines(bool displayClose = false)
	{
		if (!base.gameObject.activeInHierarchy)
		{
			StatMaster.SetInMenu(true);
		}
		base.gameObject.SetActive(true);
		container.SetActive(true);
		closeButton.gameObject.SetActive(displayClose);
		int num = 0;
		for (num = 0; num < 5; num++)
		{
			availableMachines[num].transform.parent.gameObject.SetActive(false);
		}
		List<LevelSettings.LevelMachine> allowedMachines = LevelEditor.Instance.Settings.AllowedMachines;
		for (num = 0; num < allowedMachines.Count; num++)
		{
			availableMachines[num].transform.parent.gameObject.SetActive(true);
			availableMachines[num].LevelMachine = allowedMachines[num];
		}
		CenterMachines(allowedMachines.Count);
	}

	protected void CenterMachines(int count)
	{
		for (int i = 0; i < count; i++)
		{
			Transform parent = availableMachines[i].transform.parent;
			parent.position = new Vector3(0f + offset * ((float)i - ((float)count - 1f) / 2f), parent.position.y, parent.position.z);
		}
	}
}

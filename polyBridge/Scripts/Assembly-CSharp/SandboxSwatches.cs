using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SandboxSwatches : MonoBehaviour
{
	public Image m_SwatchGlow;

	public SandboxSwatch[] m_Swatches;

	private SandboxSwatch m_SelectedSwatch;

	public void Update()
	{
		if (m_SelectedSwatch != null)
		{
			m_SwatchGlow.transform.position = m_SelectedSwatch.transform.position;
		}
	}

	public void Refresh(Vehicle vehicle)
	{
		List<VehicleSkin> list = new List<VehicleSkin>();
		VehicleSkin[] skins = vehicle.m_Stub.m_Skins;
		foreach (VehicleSkin item in skins)
		{
			list.Add(item);
		}
		if (VehicleSkins.m_Skins.ContainsKey(vehicle.m_Stub.m_PrefabAddress))
		{
			foreach (VehicleSkin item2 in VehicleSkins.m_Skins[vehicle.m_Stub.m_PrefabAddress])
			{
				if (item2.m_IsMod)
				{
					list.Add(item2);
				}
			}
		}
		int num = Mathf.Min(list.Count, m_Swatches.Length);
		base.gameObject.SetActive(num > 0);
		m_SwatchGlow.gameObject.SetActive(num > 0);
		for (int j = 0; j < num; j++)
		{
			m_Swatches[j].gameObject.SetActive(value: true);
			m_Swatches[j].Init(list[j], SwatchSelected);
		}
		for (int k = num; k < m_Swatches.Length; k++)
		{
			m_Swatches[k].gameObject.SetActive(value: false);
		}
		for (int l = 0; l < num; l++)
		{
			if (m_Swatches[l].m_VehicleSkin.m_ID == vehicle.m_SkinID)
			{
				SwatchSelected(m_Swatches[l], silent: true);
			}
		}
	}

	private void SwatchSelected(SandboxSwatch selectedSwatch, bool silent)
	{
		SandboxSwatch[] swatches = m_Swatches;
		foreach (SandboxSwatch sandboxSwatch in swatches)
		{
			if (!(sandboxSwatch.m_VehicleSkin == null))
			{
				sandboxSwatch.Highlight(sandboxSwatch == selectedSwatch);
				if (sandboxSwatch == selectedSwatch)
				{
					m_SwatchGlow.color = new Color(selectedSwatch.m_VehicleSkin.GetColorForUI().r, selectedSwatch.m_VehicleSkin.GetColorForUI().g, selectedSwatch.m_VehicleSkin.GetColorForUI().b, 0.5882353f);
					m_SwatchGlow.transform.position = selectedSwatch.transform.position;
					m_SelectedSwatch = sandboxSwatch;
					OnVehicleSkinChanged(selectedSwatch.m_VehicleSkin, silent);
				}
			}
		}
	}

	private void OnVehicleSkinChanged(VehicleSkin skin, bool silent)
	{
		if (!silent)
		{
			InterfaceAudio.Play("ui_menu_select");
		}
		Vehicle selectedVehicle = SandboxSelectionSet.GetSelectedVehicle();
		if ((bool)selectedVehicle && !(selectedVehicle.m_SkinID == skin.m_ID))
		{
			selectedVehicle.m_SkinID = skin.m_ID;
			selectedVehicle.SetFlagAndCheckpointColor();
			selectedVehicle.MaybeLoadCurrentSkinTexture();
			selectedVehicle.UploadCurrentSkinToShader();
			GameUI.m_Instance.m_SandboxEditVehicle.RefreshIcon(selectedVehicle);
			EventTimelines.UpdateForVehicleSkinChange(selectedVehicle);
			selectedVehicle.ForceShowMeshBriefly();
			SandboxUndo.SnapShot();
		}
	}
}

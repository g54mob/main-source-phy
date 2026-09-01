using System;
using System.Collections.Generic;
using System.IO;
using Steamworks.Data;
using Steamworks.Ugc;
using UnityEngine;

public class WorkshopSubmit
{
	public static readonly int MIN_CHARS_IN_TITLE = 3;

	public static string m_Id;

	public static string m_Title;

	public static string m_Description;

	public static bool m_ShowPrebuilds;

	public static bool m_AutoPlay;

	public static bool m_AllowFeatured;

	public static bool m_RunSimulationBeforeSubmit;

	public static bool m_RanSimulation;

	public static bool m_SimulationPassed;

	public static List<string> m_OnlineTags = new List<string>();

	public static void Init()
	{
		WorkshopPreview.Init();
		Reset();
	}

	public static void Reset()
	{
		m_Id = string.Empty;
		m_Title = string.Empty;
		m_Description = string.Empty;
		m_ShowPrebuilds = true;
		m_AutoPlay = false;
		m_AllowFeatured = false;
	}

	public static void SetTitle(string title)
	{
		m_Title = title;
	}

	public static void SetDescription(string description)
	{
		m_Description = description;
	}

	public static List<string> GetAutomaticallyGeneratedTags(SandboxLayoutData layoutData)
	{
		List<string> list = new List<string>();
		if (Mods.GetAllModsInLayout(layoutData).Count > 0)
		{
			list.Add(WorkshopTags.REQUIRES_MODS);
		}
		if (SandboxSettings.m_Unbreakable)
		{
			list.Add(WorkshopTags.UNBREAKABLE_TAG);
		}
		if (EventTimelines.ContainsHydraulicsPhase() && Pistons.GetNumPistons() > 0)
		{
			list.Add(WorkshopTags.HYDRAULICS_TAG);
		}
		if (SandboxSettings.m_HydraulicControllerEnabled && list.Contains(WorkshopTags.HYDRAULICS_TAG))
		{
			list.Add(WorkshopTags.HYDRAULIC_CONTROLLER_TAG);
		}
		if (layoutData.m_Budget.m_AllowSpring && layoutData.m_Budget.m_SpringBudget > 0)
		{
			list.Add(WorkshopTags.SPRINGS_TAG);
		}
		if (layoutData.m_BuildZones.Count > 0)
		{
			list.Add(WorkshopTags.BUILD_REGIONS_TAG);
		}
		if (layoutData.m_Bridge.HasPrebuilts())
		{
			list.Add(WorkshopTags.PREBUILDS_TAG);
		}
		if (layoutData.m_CustomShapes.Count > 0)
		{
			list.Add(WorkshopTags.CUSTOM_SHAPES_TAG);
		}
		return list;
	}

	public static void Submit(byte[] previewBytes, SandboxLayoutData saveData, List<string> tags)
	{
		byte[] payloadBytes = saveData.SerializeBinary();
		SubmitAsync(previewBytes, payloadBytes, tags, Editor.NewCommunityFile);
	}

	public static void Overwrite(byte[] previewBytes, SandboxLayoutData saveData, List<string> tags, string itemID)
	{
		byte[] payloadBytes = saveData.SerializeBinary();
		PublishedFileId fileId = default(PublishedFileId);
		if (ulong.TryParse(itemID, out var result))
		{
			fileId.Value = result;
			SubmitAsync(previewBytes, payloadBytes, tags, new Editor(fileId));
		}
	}

	public static async void SubmitAsync(byte[] previewBytes, byte[] payloadBytes, List<string> tags, Editor editor)
	{
		string path = Utils.GenerateUniqueId();
		GameUI.m_Instance.m_Status.Open(Localize.Get("UI_STATUS_SUBMITTING_TO_WORKSHOP"));
		string previewPath = Path.Combine(Application.persistentDataPath, Workshop.LEVEL_PREVIEW_FILENAME);
		string payloadDir = Path.Combine(Application.persistentDataPath, path);
		string payloadPath = Path.Combine(payloadDir, Workshop.LEVEL_LAYOUT_FILENAME);
		Utils.CreateDirectory(payloadDir);
		try
		{
			File.WriteAllBytes(previewPath, previewBytes);
			File.WriteAllBytes(payloadPath, payloadBytes);
		}
		catch (Exception ex)
		{
			Debug.LogWarning("Exception handled in WorkshopSubmit.SubmitAsync(): " + ex.Message);
			OnSubmitComplete(success: false, string.Empty);
			return;
		}
		if (Mods.m_IsUsingGameplayMod)
		{
			try
			{
				List<string> list = new List<string>();
				foreach (string activeCheatModDirectory in Mods.GetActiveCheatModDirectories())
				{
					string fileName = Path.GetFileName(activeCheatModDirectory);
					if (list.Contains(fileName))
					{
						OnSubmitComplete(success: false, string.Empty);
						return;
					}
					list.Add(fileName);
					string targetDirectory = Path.Combine(payloadDir, fileName);
					Utils.CopyDirectoryRecursive(activeCheatModDirectory, targetDirectory);
				}
				File.WriteAllLines(Path.Combine(payloadDir, Mods.EMBEDDED_MODS_FILENAME), list);
			}
			catch (Exception ex2)
			{
				Debug.LogWarning("Exception handled in WorkshopSubmit.SubmitAsync(): " + ex2.Message);
				OnSubmitComplete(success: false, string.Empty);
			}
		}
		foreach (string tag in tags)
		{
			editor.WithTag(tag);
		}
		PublishResult result = await editor.WithTitle(m_Title).WithDescription(m_Description).WithPreviewFile(previewPath)
			.WithPublicVisibility()
			.WithContent(payloadDir)
			.WithMetaData(WorkshopMetaData.Create())
			.SubmitAsync();
		try
		{
			File.Delete(previewPath);
		}
		catch (Exception ex3)
		{
			Debug.LogWarning("Exception handled in WorkshopSubmit.SubmitAsync(): " + ex3.Message);
		}
		try
		{
			File.Delete(payloadPath);
		}
		catch (Exception ex4)
		{
			Debug.LogWarning("Exception handled in WorkshopSubmit.SubmitAsync(): " + ex4.Message);
		}
		try
		{
			Utils.RecursiveDelete(new DirectoryInfo(payloadDir));
		}
		catch (Exception ex5)
		{
			Debug.LogWarning("Exception handled in WorkshopSubmit.SubmitAsync(): " + ex5.Message);
		}
		if (!result.Success)
		{
			OnSubmitComplete(success: false, string.Empty);
			return;
		}
		ResultPage? resultPage = await Query.All.WithFileId(result.FileId).GetPageAsync(1);
		if (!resultPage.HasValue)
		{
			OnSubmitComplete(success: false, string.Empty);
			return;
		}
		foreach (Item item in resultPage.Value.Entries)
		{
			await item.Subscribe();
			item.Download();
		}
		if (result.NeedsWorkshopAgreement)
		{
			SteamUtils.OpenWorkshopAgreementOverlay();
		}
		OnSubmitComplete(success: true, result.FileId.Value.ToString());
		GameUI.m_Instance.m_WorkshopSubmit.Close();
	}

	public static bool VehiclesInLevel()
	{
		return Vehicles.m_Vehicles.Count > 0;
	}

	public static bool BridgeInLevel()
	{
		return BridgeEdges.GetNumActiveEdges() > 0;
	}

	public static bool BridgeHasIllegalNodePlacement()
	{
		return GetFirstIllegalNodeOrEdgePosition().x != float.MaxValue;
	}

	public static Vector3 GetFirstIllegalNodeOrEdgePosition()
	{
		Vector2[] array = new Vector2[BridgeJoints.m_Joints.Count];
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = BridgeJoints.m_Joints[i].transform.position;
		}
		foreach (BridgeJoint joint in BridgeJoints.m_Joints)
		{
			if (!joint.m_IsAnchor && joint.gameObject.activeInHierarchy)
			{
				if (BridgeJoints.JointOverlapsOtherJoints_Optimized(joint, 0f, GameSettings.NodeDiameter(), array))
				{
					return joint.transform.position;
				}
				if (BridgeJoints.NodeLocationOverlapsBlockingPolygonShape(joint.transform.position))
				{
					return joint.transform.position;
				}
				if (GameStateManager.GetState() == GameState.BUILD && !BuildZones.ContainsJoint(joint.transform.position) && !joint.IsConnectedToPrebuilt())
				{
					return joint.transform.position;
				}
			}
		}
		foreach (BridgeEdge edge in BridgeEdges.m_Edges)
		{
			if (edge.gameObject.activeInHierarchy && BridgeEdges.EdgeLocationOverlapsBlockingPolygonShape(edge.m_JointA.transform.position, edge.m_JointB.transform.position, edge.m_Material.m_MaterialType, edge.m_Material.m_EdgeMaterial.collisionRadius))
			{
				return (edge.m_JointA.transform.position + edge.m_JointB.transform.position) / 2f;
			}
		}
		return new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
	}

	public static WorkshopProxy Serialize()
	{
		return new WorkshopProxy(m_Id, m_AutoPlay, m_AllowFeatured, m_ShowPrebuilds);
	}

	public static void Deserialize(WorkshopProxy proxy)
	{
		if (proxy != null)
		{
			m_Id = proxy.m_Id;
			m_AutoPlay = proxy.m_AutoPlay;
			m_AllowFeatured = proxy.m_AllowFeatured;
			m_ShowPrebuilds = proxy.m_ShowPrebuilds;
		}
	}

	private static void OnSubmitComplete(bool success, string itemId)
	{
		bool flag = GameStateManager.GetState() == GameState.SANDBOX || GameStateManager.GetState() == GameState.DECOR;
		if (!success || !flag)
		{
			string text = Localize.Get("UI_STATUS_SUBMIT_WORKSHOP_FAILED");
			GameUI.m_Instance.m_Status.Complete(GameManager.IsSteamOffline() ? Localize.Get("UI_STATUS_SUBMIT_WORKSHOP_NETWORK ERROR") : text);
			return;
		}
		GameUI.m_Instance.m_Status.Complete(Localize.Get("UI_STATUS_SUBMIT_WORKSHOP_SUCCESS"));
		if (!string.IsNullOrEmpty(Sandbox.m_CurrentLayoutName))
		{
			m_Id = itemId;
			SandboxLayoutData sandboxLayoutData = SandboxLayout.Save(Sandbox.m_CurrentLayoutName);
			if (sandboxLayoutData != null)
			{
				Sandbox.m_CurrentLayoutData = sandboxLayoutData;
				Sandbox.m_UnsavedChanges = false;
			}
		}
	}
}

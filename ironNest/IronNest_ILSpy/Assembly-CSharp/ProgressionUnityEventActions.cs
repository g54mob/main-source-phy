using System;
using Cpp2ILInjected;
using SleepyNodes;
using UnityEngine;

public class ProgressionUnityEventActions : MonoBehaviour
{
	public OperationGraph Campaign;

	public MapCardManager MapCardManager;

	public void ResetAllUserProgress()
	{
		if (ProgressionManager._003CInstance_003Ek__BackingField != null)
		{
			ProgressionManager._003CInstance_003Ek__BackingField.ResetAllUserProgress();
			RefreshMapCardManager();
			UnlockableSceneObject.RefreshAll();
		}
		else
		{
			Debug.LogWarning("[ProgressionUnityEventActions] Unable to reset progress. No ProgressionManager instance.");
		}
	}

	public void ForceUnlockAndMarkAllMissionsComplete()
	{
		//IL_0141: Expected O, but got I
		if (ProgressionManager._003CInstance_003Ek__BackingField != null)
		{
			UnityEngine.Object obj;
			if (Campaign == null)
			{
				if (MapCardManager == null)
				{
					MapCardManager mapCardManager = GetMapCardManager();
					obj = ((!(mapCardManager != null)) ? null : mapCardManager.Campaign);
				}
				else
				{
					MapCardManager mapCardManager2 = MapCardManager;
					obj = mapCardManager2.Campaign;
				}
			}
			else
			{
				obj = Campaign;
			}
			if (obj != null)
			{
				int num = ProgressionManager._003CInstance_003Ek__BackingField.ForceCompleteMissions((OperationGraph)obj);
				Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v248 @ rbx_v3 (UnityEngine.Object)+58]");
				object arg = default(object);
				string message = $"[ProgressionUnityEventActions] Force completed {arg} missions for '{0}'.";
				Debug.Log(message);
				RefreshMapCardManager();
				if ((object)MapCardManager != null)
				{
					MapCardManager.ForceRevealAllComplete();
				}
			}
			else
			{
				Debug.LogWarning("[ProgressionUnityEventActions] Unable to force complete missions. No campaign assigned.");
			}
		}
		else
		{
			Debug.LogWarning("[ProgressionUnityEventActions] Unable to force complete missions. No ProgressionManager instance.");
		}
	}

	private OperationGraph GetCampaign()
	{
		bool flag = Campaign == null;
		ProgressionUnityEventActions progressionUnityEventActions = this;
		if (!flag)
		{
			goto IL_00a6;
		}
		if (MapCardManager == null)
		{
			MapCardManager mapCardManager = GetMapCardManager();
			if (!(mapCardManager != null))
			{
				return null;
			}
			bool flag2 = (object)mapCardManager == null;
			progressionUnityEventActions = (ProgressionUnityEventActions)(object)mapCardManager;
			if (!flag2)
			{
				goto IL_00a6;
			}
		}
		else
		{
			MapCardManager mapCardManager2 = MapCardManager;
			if ((object)MapCardManager != null)
			{
				return mapCardManager2.Campaign;
			}
		}
		return (OperationGraph)(object)new NullReferenceException();
		IL_00a6:
		return progressionUnityEventActions.Campaign;
	}

	private MapCardManager GetMapCardManager()
	{
		if (MapCardManager == null)
		{
			MapCardManager mapCardManager = UnityEngine.Object.FindFirstObjectByType<MapCardManager>();
			MapCardManager = mapCardManager;
		}
		return MapCardManager;
	}

	private void RefreshMapCardManager()
	{
		MapCardManager mapCardManager = GetMapCardManager();
		if (mapCardManager != null)
		{
			mapCardManager.UpdateMapCards();
		}
	}
}

using System;
using Cpp2ILInjected;
using UnityEngine;

public class UnlockSceneObjectRelay : MonoBehaviour
{
	private string _objectId;

	private bool _refreshImmediately = true;

	private bool _requireMissionComplete;

	public void UnlockSceneObject()
	{
		//IL_00ad: Expected O, but got I
		bool flag = ProgressionManager._003CInstance_003Ek__BackingField.IsSceneObjectUnlocked(_objectId);
		if (flag)
		{
			return;
		}
		if (_requireMissionComplete != flag)
		{
			if (!(MissionManager._003CInstance_003Ek__BackingField != null))
			{
				return;
			}
			MissionManager missionManager = MissionManager._003CInstance_003Ek__BackingField;
			if (missionManager.CurrentMissionState == null)
			{
				return;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803DB660");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v75 @ rax_v24+68]");
			object obj = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v76 @ rax_v25+10]");
			if ((nint)0 == 0)
			{
				return;
			}
		}
		bool flag2 = ProgressionManager._003CInstance_003Ek__BackingField.UnlockSceneObject(_objectId);
		ProgressionManager._003CInstance_003Ek__BackingField.SaveProgression();
		if (_refreshImmediately)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 278 Invalid \"Jump target not found in method: 0x1804638E0\"");
		}
	}

	private bool CanBeUnlocked()
	{
		//IL_0129: Expected I4, but got O
		//IL_00c5: Expected O, but got I
		if ((object)ProgressionManager._003CInstance_003Ek__BackingField != null)
		{
			bool flag = ProgressionManager._003CInstance_003Ek__BackingField.IsSceneObjectUnlocked(_objectId);
			if (!flag)
			{
				if (_requireMissionComplete == flag)
				{
					goto IL_010f;
				}
				if (MissionManager._003CInstance_003Ek__BackingField != null)
				{
					MissionManager missionManager = MissionManager._003CInstance_003Ek__BackingField;
					if ((object)MissionManager._003CInstance_003Ek__BackingField != null)
					{
						if (missionManager.CurrentMissionState == null)
						{
							goto IL_0115;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803DB660");
						object obj = default(object);
						if (obj != null)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v63 @ rax_v18+68]");
							object obj2 = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v63 @ rax_v18+68]");
							if ((nint)0 != 0)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v64 @ rax_v19+10]");
								if ((nint)0 != 0)
								{
									goto IL_010f;
								}
								goto IL_0115;
							}
						}
					}
					goto IL_011b;
				}
			}
			goto IL_0115;
		}
		goto IL_011b;
		IL_010f:
		return true;
		IL_011b:
		NullReferenceException ex = new NullReferenceException();
		return (byte)(int)ex != 0;
		IL_0115:
		return false;
	}
}

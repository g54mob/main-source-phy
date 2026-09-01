using System;
using Cpp2ILInjected;
using UnityEngine;

public class CorrectionPointerVariantSelector : MonoBehaviour
{
	public Transform arrowRoot;

	public bool retryUntilControllerFound = true;

	public float retryInterval = 0.25f;

	public bool debugLogs;

	private bool _applied;

	private float _nextRetryTime;

	private void OnEnable()
	{
		TryApply();
	}

	private void Update()
	{
		if (!_applied && retryUntilControllerFound)
		{
			float time = Time.time;
			if (!(time < _nextRetryTime))
			{
				TryApply();
			}
		}
	}

	private void TryApply()
	{
		//IL_0074: Expected O, but got I
		//IL_00a7: Expected O, but got I
		//IL_00d5: Expected O, but got I
		if ((bool)arrowRoot)
		{
			UnityEngine.Object obj = ImpactCorrectionTierController._003CInstance_003Ek__BackingField;
			if ((bool)ImpactCorrectionTierController._003CInstance_003Ek__BackingField)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v96 @ rbx_v4 (UnityEngine.Object)+40]");
				if ((bool)(UnityEngine.Object)0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v96 @ rbx_v4 (UnityEngine.Object)+28]");
					if ((bool)(UnityEngine.Object)0)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v96 @ rbx_v4 (UnityEngine.Object)+40]");
						Transform transform = ((Component)0).transform;
						int siblingIndex = transform.GetSiblingIndex();
						if (siblingIndex >= 0)
						{
							int childCount = arrowRoot.childCount;
							int num = childCount - 1;
							bool flag = siblingIndex < num;
							int index = siblingIndex;
							if (!flag)
							{
								index = num;
							}
							ActivateChild(index);
							if (debugLogs)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
								Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
								object arg = default(object);
								object arg2 = default(object);
								string message = $"[CorrectionPointerVariantSelector] Activated child index {arg} for tier sibling {arg2}.";
								Debug.Log(message, this);
							}
							goto IL_029c;
						}
					}
				}
				if (debugLogs)
				{
					Debug.Log("[CorrectionPointerVariantSelector] No active direction tier found; defaulting child 0.", this);
				}
				ActivateChild(0);
			}
			else
			{
				if (retryUntilControllerFound)
				{
					float time = Time.time;
					float nextRetryTime = time + retryInterval;
					_nextRetryTime = nextRetryTime;
					return;
				}
				if (debugLogs)
				{
					Debug.LogWarning("[CorrectionPointerVariantSelector] No controller found; leaving first child active fallback.", this);
				}
				ActivateChild(0);
			}
		}
		else if (debugLogs)
		{
			Debug.LogWarning("[CorrectionPointerVariantSelector] No arrowRoot assigned.", this);
		}
		goto IL_029c;
		IL_029c:
		_applied = true;
	}

	private int GetActiveDirectionTierSiblingIndex(ImpactCorrectionTierController controller)
	{
		//IL_00e0: Expected I4, but got O
		//IL_00d2: Expected I4, but got I8
		if ((object)controller != null)
		{
			if (!controller._003CActiveDirectionTier_003Ek__BackingField || !controller.directionTierRoot)
			{
				return -1;
			}
			if ((object)controller._003CActiveDirectionTier_003Ek__BackingField != null)
			{
				Transform transform = controller._003CActiveDirectionTier_003Ek__BackingField.transform;
				if ((object)transform != null)
				{
					return transform.GetSiblingIndex();
				}
			}
		}
		NullReferenceException ex = new NullReferenceException();
		return (int)ex;
	}

	private void ActivateChild(int index)
	{
		//IL_0084: Expected O, but got I4
		Transform transform = arrowRoot;
		int num = 0;
		int num2 = 0;
		while (true)
		{
			int childCount = transform.childCount;
			if (num < childCount)
			{
				Transform child = arrowRoot.GetChild(num2);
				GameObject gameObject = child.gameObject;
				object obj = num2 - index;
				bool active = obj == null;
				gameObject.SetActive(active);
				transform = arrowRoot;
				num2++;
				num = num2;
				continue;
			}
			break;
		}
	}
}

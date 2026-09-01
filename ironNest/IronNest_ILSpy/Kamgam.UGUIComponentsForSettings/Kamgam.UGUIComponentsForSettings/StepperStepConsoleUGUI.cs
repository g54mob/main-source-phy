using System;
using System.Collections.Generic;
using Cpp2ILInjected;
using UnityEngine;

namespace Kamgam.UGUIComponentsForSettings;

public class StepperStepConsoleUGUI : MonoBehaviour
{
	public GameObject Inactive;

	public GameObject Active;

	public static List<StepperStepConsoleUGUI> CreateSteps(Transform container, GameObject template, int totalSteps)
	{
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Expected O, but got Unknown
		//IL_00d9: Expected O, but got I4
		//IL_00e2: Expected O, but got I4
		//IL_01bc: Expected O, but got I4
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Expected O, but got Unknown
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_0185: Expected O, but got Unknown
		//IL_0376: Unknown result type (might be due to invalid IL or missing references)
		//IL_037b: Expected O, but got Unknown
		if (template != null)
		{
			if ((object)template != null)
			{
				GameObject gameObject = template.gameObject;
				if ((object)gameObject != null)
				{
					gameObject.SetActive(value: false);
					if ((object)container != null)
					{
						StepperStepConsoleUGUI[] componentsInChildren = container.GetComponentsInChildren<StepperStepConsoleUGUI>(includeInactive: true);
						if (componentsInChildren != null)
						{
							object obj = componentsInChildren + 32;
							object obj2 = 0;
							object obj3 = 0;
							StepperStepConsoleUGUI stepperStepConsoleUGUI = default(StepperStepConsoleUGUI);
							while (true)
							{
								if ((nint)obj3 < componentsInChildren.Length)
								{
									if (obj == null)
									{
										break;
									}
									string text = ((UnityEngine.Object)obj).name;
									if (text == null)
									{
										break;
									}
									if (!text.EndsWith("_Template"))
									{
										GameObject obj4 = ((Component)obj).gameObject;
										smartDestroy(obj4);
									}
									obj2++;
									obj += 8;
									obj3 = obj2;
									continue;
								}
								List<StepperStepConsoleUGUI> list = new List<StepperStepConsoleUGUI>();
								bool flag = totalSteps <= 0;
								object obj5 = 0;
								if (!flag)
								{
									while (true)
									{
										GameObject gameObject2 = UnityEngine.Object.Instantiate(template, container);
										string text2 = template.name;
										if (text2 == null)
										{
											break;
										}
										string text3 = text2.Replace("_Template", "");
										if ((object)gameObject2 == null)
										{
											break;
										}
										gameObject2.name = text3;
										gameObject2.SetActive(value: true);
										Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806D9540");
										if ((object)stepperStepConsoleUGUI == null || (object)stepperStepConsoleUGUI.Active == null)
										{
											break;
										}
										GameObject gameObject3 = stepperStepConsoleUGUI.Active.gameObject;
										if ((object)gameObject3 == null)
										{
											break;
										}
										gameObject3.SetActive(value: false);
										if ((object)stepperStepConsoleUGUI.Inactive == null)
										{
											break;
										}
										GameObject gameObject4 = stepperStepConsoleUGUI.Inactive.gameObject;
										if ((object)gameObject4 == null)
										{
											break;
										}
										gameObject4.SetActive(value: true);
										if (list == null)
										{
											break;
										}
										list.Add(stepperStepConsoleUGUI);
										obj5++;
										if ((nint)obj5 >= totalSteps)
										{
											goto IL_0397;
										}
									}
									break;
								}
								goto IL_0397;
								IL_0397:
								return list;
							}
						}
					}
				}
			}
			return (List<StepperStepConsoleUGUI>)(object)new NullReferenceException();
		}
		return null;
	}

	public static void SetActive(List<StepperStepConsoleUGUI> steps, int activeSteps)
	{
		//IL_000e: Expected O, but got I4
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Expected O, but got Unknown
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Expected I4, but got Unknown
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Expected I4, but got Unknown
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Expected O, but got Unknown
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Expected I4, but got Unknown
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Expected I4, but got Unknown
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Expected O, but got Unknown
		if (steps != null)
		{
			object obj = 0;
			GameObject gameObject = default(GameObject);
			GameObject gameObject2 = default(GameObject);
			while ((nint)obj < steps._size)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180E443F0");
				object obj2 = obj - activeSteps;
				int num = obj ^ activeSteps;
				object obj3 = obj ^ obj2;
				int num2 = num & obj3;
				bool flag = num2 < 0;
				bool flag2 = (nint)obj2 < 0;
				bool active = flag2 != flag;
				gameObject.SetActive(active);
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180E443F0");
				object obj4 = obj - activeSteps;
				int num3 = obj ^ activeSteps;
				object obj5 = obj ^ obj4;
				int num4 = num3 & obj5;
				bool flag3 = num4 < 0;
				bool flag4 = (nint)obj4 < 0;
				bool active2 = flag4 == flag3;
				gameObject2.SetActive(active2);
				obj++;
			}
		}
	}

	private static void smartDestroy(UnityEngine.Object obj)
	{
		if (obj != null)
		{
			UnityEngine.Object.Destroy(obj);
		}
	}

	public void SetActive(bool active)
	{
		GameObject gameObject = Active.gameObject;
		gameObject.SetActive(active);
		GameObject gameObject2 = Inactive.gameObject;
		bool active2 = (byte)((active ? 1u : 0u) ^ 1u) != 0;
		gameObject2.SetActive(active2);
	}
}

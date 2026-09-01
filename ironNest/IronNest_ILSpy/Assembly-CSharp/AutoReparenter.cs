using System;
using System.Collections.Generic;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AutoReparenter : MonoBehaviour
{
	public string masterParentTag;

	public string missionObjectTag;

	private void Awake()
	{
		ReparentMissionObjects();
	}

	private unsafe void ReparentMissionObjects()
	{
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Expected O, but got Unknown
		//IL_01f4: Expected O, but got Ref
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Expected O, but got Unknown
		//IL_00d2: Expected O, but got I4
		//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a8: Expected O, but got Unknown
		//IL_01b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b6: Expected O, but got Unknown
		//IL_0182: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Expected O, but got Unknown
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_0195: Expected O, but got Unknown
		GameObject gameObject = GameObject.FindWithTag(masterParentTag);
		if (gameObject != null)
		{
			GameObject gameObject2 = base.gameObject;
			Scene scene = gameObject2.scene;
			List<GameObject> list = new List<GameObject>();
			Scene scene2 = default(Scene);
			GameObject[] rootGameObjects = scene2.GetRootGameObjects();
			object obj = rootGameObjects + 32;
			GameObject gameObject3 = null;
			GameObject gameObject4 = null;
			List<GameObject>.Enumerator enumerator = default(List<GameObject>.Enumerator);
			GameObject gameObject5 = default(GameObject);
			while (true)
			{
				if ((nint)gameObject4 < rootGameObjects.Length)
				{
					if ((nint)gameObject3 >= rootGameObjects.Length)
					{
						break;
					}
					gameObject4 = (GameObject)obj;
					Transform[] componentsInChildren = ((GameObject)obj).GetComponentsInChildren<Transform>(true);
					object obj2 = componentsInChildren + 32;
					object obj3 = 0;
					while ((nint)obj3 < componentsInChildren.Length)
					{
						if ((nint)obj3 >= componentsInChildren.Length)
						{
							goto end_IL_0332;
						}
						bool flag = ((Component)obj2).CompareTag(missionObjectTag);
						bool flag2 = !flag;
						gameObject4 = (GameObject)obj2;
						if (!flag2)
						{
							GameObject item = ((Component)obj2).gameObject;
							list.Add(item);
							gameObject4 = (GameObject)(object)list;
						}
						obj3++;
						obj2 += 8;
					}
					gameObject3 = (GameObject)(gameObject3 + 1);
					obj += 8;
					gameObject4 = gameObject3;
					continue;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
				while (true)
				{
					if (enumerator.MoveNext())
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
						bool flag3 = (object)gameObject5 == null;
						gameObject4 = (GameObject)(&enumerator);
						if (!flag3)
						{
							Transform transform = gameObject5.transform;
							if ((object)gameObject != null)
							{
								Transform parent = gameObject.transform;
								if ((object)transform == null)
								{
									break;
								}
								transform.SetParent(parent, worldPositionStays: false);
								string text = gameObject5.name;
								string text2 = gameObject.name;
								string message = "AutoReparenter: Reparented " + text + " to " + text2;
								Debug.Log(message);
								continue;
							}
							throw new NullReferenceException();
						}
						throw new NullReferenceException();
					}
					enumerator.Dispose();
					return;
				}
				throw new NullReferenceException();
				continue;
				end_IL_0332:
				break;
			}
			throw new IndexOutOfRangeException();
		}
		string message2 = "AutoReparenter: Master parent with tag '" + masterParentTag + "' not found!";
		Debug.LogWarning(message2);
	}

	public AutoReparenter()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A8D3]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		masterParentTag = "MainCanvas";
		missionObjectTag = "MissionTarget";
		base._002Ector();
	}
}

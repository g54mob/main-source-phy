using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class DebugShowSelectedUIItem : MonoBehaviour
{
	private const int FindGroupsDelay = 10;

	[SerializeField]
	[Tooltip("Don't destroy this game object when the scene loads. Only works if it doesn't have a parent.")]
	protected bool dontDestroyOnLoad = true;

	[SerializeField]
	[Tooltip("Show the name of the UINavigationGroup the selected object belongs to.")]
	protected bool showUiNavigationGroup = true;

	[SerializeField]
	[Tooltip("Show all enabled UINavigationGroups in the scene. (This is expensive because it uses FindObjectsOfType.)")]
	protected bool showEnabledUiNavigationGroups;

	private DebugShowSelectedUIItem instance;

	private int findGroupsDelay;

	private List<UINavigationGroup> groups = new List<UINavigationGroup>();

	private int enabledGroupsCount;

	private Vector2 scrollPos;

	private bool didShowWarning;

	private UINavigationGroup previousGroup;

	private GameObject previousSelected;

	private static readonly string NewLine = Environment.NewLine;

	public void Awake()
	{
		if (!(instance != null) || !(this != instance))
		{
			instance = this;
			if (dontDestroyOnLoad && base.transform.parent == null)
			{
				UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
			}
			SceneManager.sceneLoaded += OnSceneLoaded;
		}
	}

	private void Update()
	{
		if (findGroupsDelay > 0)
		{
			findGroupsDelay--;
			if (findGroupsDelay <= 0)
			{
				groups.Clear();
				groups.AddRange(UnityEngine.Object.FindObjectsOfType<UINavigationGroup>());
			}
		}
	}

	private void OnGUI()
	{
		DebugDraw();
	}

	private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		groups.Clear();
		findGroupsDelay = (showUiNavigationGroup ? 10 : 0);
	}

	private void DebugDraw()
	{
		EventSystem current = EventSystem.current;
		if (current == null)
		{
			return;
		}
		float num = 20f;
		float num2 = 5f;
		float num3 = 30f;
		float num4 = ((enabledGroupsCount > 0) ? ((float)(enabledGroupsCount + 1) * num3) : 0f);
		float num5 = (didShowWarning ? num3 : 0f);
		Vector2 vector = new Vector2(400f, 100f + num4 + num5);
		Rect rect = new Rect((float)Screen.width - vector.x - num, 30f + num, vector.x, vector.y);
		GameObject gameObject = ((current != null) ? current.currentSelectedGameObject : null);
		string text = "(none)";
		UINavigationGroup uINavigationGroup = null;
		if (gameObject != null)
		{
			uINavigationGroup = (showUiNavigationGroup ? gameObject.GetComponentInParent<UINavigationGroup>() : null);
			text = ((!gameObject.activeInHierarchy) ? $"{gameObject.name} [disabled]" : gameObject.name);
		}
		GUI.Box(rect, string.Empty);
		GUI.Box(rect, string.Empty);
		rect.xMin += num2;
		rect.xMax -= num2;
		rect.yMin += num2;
		rect.yMax += num2;
		GUILayout.BeginArea(rect);
		scrollPos = GUILayout.BeginScrollView(scrollPos);
		if (uINavigationGroup != null)
		{
			string text2 = (uINavigationGroup.NavigationEnabled ? string.Empty : " [navDisabled]");
			string text3 = (uINavigationGroup.enabled ? string.Empty : " [compDisabled]");
			GUILayout.Label("Currently Selected:" + NewLine + text + NewLine + NewLine + "UINavigationGroup:" + NewLine + uINavigationGroup.name + text2 + text3);
		}
		else
		{
			GUILayout.Label("Currently Selected:" + NewLine + text);
		}
		if (showEnabledUiNavigationGroups)
		{
			if (previousGroup != uINavigationGroup || previousSelected != gameObject)
			{
				previousGroup = uINavigationGroup;
				previousSelected = gameObject;
				findGroupsDelay = Mathf.Max(findGroupsDelay, 1);
			}
			DrawEnabledNavigationGroups();
		}
		Color color = GUI.color;
		GUI.color = Color.red;
		GUILayout.Label("Remove " + GetType().Name + " from release builds!");
		GUI.color = color;
		didShowWarning = true;
		GUILayout.EndScrollView();
		GUILayout.EndArea();
	}

	private void DrawEnabledNavigationGroups()
	{
		enabledGroupsCount = 0;
		int count = groups.Count;
		if (count <= 0)
		{
			return;
		}
		Color color = GUI.color;
		bool flag = false;
		for (int i = 0; i < count; i++)
		{
			UINavigationGroup uINavigationGroup = groups[i];
			if (!(uINavigationGroup == null) && uINavigationGroup.NavigationEnabled)
			{
				if (!flag)
				{
					flag = true;
					GUI.color = Color.yellow;
					GUILayout.Label("Enabled groups:");
					GUI.color = color;
				}
				GUILayout.Label(uINavigationGroup.name);
				enabledGroupsCount++;
			}
		}
	}
}

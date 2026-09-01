using System;
using System.Collections;
using Localisation;
using UnityEngine;
using UnityEngine.SceneManagement;

[AddComponentMenu("Levels/Display Level Title")]
public class DisplayLevelTitle : MonoBehaviour
{
	[Serializable]
	public class Shadow
	{
		public Transform parent;

		public GameObject prefab;

		public float size = 10f;

		public float scale = 1f;

		public float iconRatio = 0.001f;

		public Vector3 offset;
	}

	[Serializable]
	public class Transition
	{
		public float delay = 0.5f;

		public float sustain = 1.75f;

		public float fadeDuration = 1.5f;
	}

	public DynamicText[] superTitles;

	public DynamicText[] leftTitles;

	public DynamicText rightTitle;

	public MeshRenderer leftSymbol;

	public MeshRenderer rightSymbol;

	private float symbolOffset = 0.4f;

	public Transform leftSuperLine;

	public Transform rightSuperLine;

	public Transform titleLines;

	public Shadow shadow;

	public Transition transition;

	protected MeshRenderer lr;

	protected MeshRenderer rr;

	protected MeshRenderer[] rens;

	protected MeshRenderer[] shadowRens;

	protected Color[] start;

	protected Color[] shadowStart;

	protected bool campaign;

	protected int zoneIndex = -1;

	private void Start()
	{
		AssignVariables();
		if (StatMaster.isMP)
		{
			base.gameObject.SetActive(false);
		}
		else
		{
			DisplayLevelName(1f, transition.delay);
		}
	}

	public void AssignVariables()
	{
		ReferenceMaster.onLevelLoad = (Action)Delegate.Combine(ReferenceMaster.onLevelLoad, new Action(LoadMPLevel));
		lr = leftTitles[0].GetComponent<MeshRenderer>();
		rr = rightTitle.GetComponent<MeshRenderer>();
		RememberStartColor();
		if (int.TryParse(SceneManager.GetActiveScene().name, out zoneIndex))
		{
			campaign = true;
		}
	}

	public void RememberStartColor()
	{
		int num = 0;
		rens = new MeshRenderer[superTitles.Length + leftTitles.Length + titleLines.childCount + 5];
		start = new Color[rens.Length];
		for (int i = 0; i < superTitles.Length; i++)
		{
			rens[num] = superTitles[i].GetComponent<MeshRenderer>();
			start[num] = rens[num].material.color;
			num++;
		}
		rens[num] = rr;
		start[num] = rr.material.color;
		num++;
		for (int j = 0; j < leftTitles.Length; j++)
		{
			rens[num] = leftTitles[j].GetComponent<MeshRenderer>();
			start[num] = rens[num].material.color;
			num++;
		}
		rens[num] = leftSymbol;
		start[num] = leftSymbol.material.color;
		num++;
		rens[num] = rightSymbol;
		start[num] = rightSymbol.material.color;
		num++;
		rens[num] = leftSuperLine.GetChild(0).GetComponent<MeshRenderer>();
		start[num] = rens[num].material.color;
		num++;
		rens[num] = rightSuperLine.GetChild(0).GetComponent<MeshRenderer>();
		start[num] = rens[num].material.color;
		num++;
		foreach (Transform titleLine in titleLines)
		{
			rens[num] = titleLine.GetComponent<MeshRenderer>();
			start[num] = rens[num].material.color;
			num++;
		}
	}

	public void LoadMPLevel()
	{
		LevelSettings settings = LevelEditor.Instance.Settings;
		if (string.IsNullOrEmpty(settings.Name))
		{
			DisplayLevelName(1.1f, 0f, LocalisationManager.GetTranslation(3374));
			return;
		}
		WorkshopManager.VerifyString(ReferenceMaster.CamelCaseToSpaces(settings.Name).ToUpper(), delegate(WorkshopManager.VerifyStringResult res, string str)
		{
			DisplayLevelName(1.1f, 0f, str);
		});
	}

	public void DisplayLevelName(float speed, float delay, string name = null)
	{
		if (!StatMaster.isHeadless && !(base.gameObject == null))
		{
			if (name == null && LevelAttributes.instance.levelNameLocalisationIndex == 0)
			{
				base.gameObject.SetActive(false);
				return;
			}
			base.gameObject.SetActive(true);
			StopAllCoroutines();
			SetText(name);
			AlignText();
			AlignSymbols();
			ScaleLines();
			ApplyDropShadow();
			Fade(speed, delay);
		}
	}

	public void SetText(string force = null)
	{
		if (!StatMaster.isMP && campaign)
		{
			campaign = true;
			string text = LocalisationManager.GetTranslation(IslandToTranslation(ReferenceMaster.LevelToIsland(zoneIndex))).ToUpper();
			if (text == string.Empty)
			{
				leftSuperLine.parent.gameObject.SetActive(false);
			}
			else
			{
				ReferenceMaster.SetDynamicText(superTitles[0], LocalisationManager.GetTranslation((ReferenceMaster.LevelToIsland(zoneIndex) != Island.Water) ? 200 : 4528));
				ReferenceMaster.SetDynamicText(superTitles[1], text);
			}
		}
		else
		{
			leftSuperLine.parent.gameObject.SetActive(false);
		}
		bool flag = false;
		string[] array = new string[0];
		string text2;
		if (StatMaster.isMP || force != null)
		{
			text2 = force;
			array = ((!string.IsNullOrEmpty(text2)) ? text2.Split(new string[1] { " " }, StringSplitOptions.RemoveEmptyEntries) : new string[0]);
			flag = array.Length > 0;
		}
		else if (LevelAttributes.instance != null)
		{
			text2 = LocalisationManager.GetTranslation(LevelAttributes.instance.levelNameLocalisationIndex).ToUpper();
			array = text2.Split(new string[1] { " " }, StringSplitOptions.RemoveEmptyEntries);
			flag = array.Length > 0;
		}
		if (!flag)
		{
			if (StatMaster.isMP)
			{
				text2 = "UNTITLED";
			}
			else
			{
				Debug.LogWarning("DisplayLevelTitle received empty title");
				text2 = SceneManager.GetActiveScene().name.ToUpper();
				if (campaign)
				{
					text2 = "ZONE " + zoneIndex;
				}
			}
			array = text2.Split(new string[1] { " " }, StringSplitOptions.RemoveEmptyEntries);
		}
		int num = 1;
		text2 = array[0];
		if (text2.Length < 4 && array.Length > 2)
		{
			num++;
			text2 = text2 + " " + array[1];
		}
		for (int i = 0; i < leftTitles.Length; i++)
		{
			ReferenceMaster.SetDynamicText(leftTitles[i], text2);
		}
		text2 = string.Empty;
		for (int i = num; i < array.Length; i++)
		{
			text2 = text2 + array[i] + " ";
		}
		text2.TrimEnd();
		ReferenceMaster.SetDynamicText(rightTitle, text2);
	}

	protected int IslandToTranslation(Island i)
	{
		switch (i)
		{
		case Island.Ipsilon:
			return 2182;
		case Island.Tolbrynd:
			return 2156;
		case Island.Valfross:
			return 3248;
		case Island.Krolmar:
			return 3500;
		case Island.Water:
			return 4527;
		default:
			return int.MaxValue;
		}
	}

	public void AlignText()
	{
		RelativeTitleAlignment();
		CenterTitle();
	}

	protected void RelativeTitleAlignment()
	{
		float x = lr.bounds.max.x;
		Vector3 position = rightTitle.transform.position;
		rightTitle.transform.position = new Vector3(x + 0.25f + rr.bounds.extents.x, position.y, position.z);
	}

	protected void CenterTitle()
	{
		Vector3 vector = ((!(rr.bounds.size.x > 0f)) ? lr.bounds.center : ((lr.bounds.min + rr.bounds.max) * 0.5f));
		vector -= base.transform.position;
		lr.transform.position -= new Vector3(vector.x, 0f, 0f);
		rr.transform.position -= new Vector3(vector.x, 0f, 0f);
	}

	public void AlignSymbols()
	{
		float num = ((!(rr.bounds.size.x > 0f)) ? lr.bounds.max.x : rr.bounds.max.x);
		float x = lr.bounds.min.x;
		leftSymbol.transform.position = new Vector3(x - symbolOffset, leftSymbol.transform.position.y, leftSymbol.transform.position.z);
		rightSymbol.transform.position = new Vector3(num + symbolOffset - 0.055f, rightSymbol.transform.position.y, rightSymbol.transform.position.z);
	}

	public void ScaleLines()
	{
		ScaleTitleLines();
		ScaleSuperlines();
	}

	protected void ScaleTitleLines()
	{
		Bounds bounds = new Bounds(base.transform.position, Vector3.zero);
		bounds.Encapsulate(lr.bounds);
		bounds.Encapsulate(rr.bounds);
		titleLines.transform.localScale = new Vector3(bounds.size.x - 0.25f, titleLines.transform.localScale.y, 1f);
	}

	protected void ScaleSuperlines()
	{
		Bounds bounds = new Bounds(base.transform.position, Vector3.zero);
		for (int i = 0; i < superTitles.Length; i++)
		{
			bounds.Encapsulate(superTitles[i].GetComponent<MeshRenderer>().bounds);
		}
		float x = rightSuperLine.localScale.x;
		float num = rr.bounds.max.x * 0.9f;
		rightSuperLine.position = new Vector3(bounds.max.x + 0.25f, rightSuperLine.position.y, rightSuperLine.position.z);
		leftSuperLine.position = new Vector3(bounds.min.x - 0.25f, leftSuperLine.position.y, leftSuperLine.position.z);
		float value = num - rightSuperLine.position.x;
		rightSuperLine.localScale = new Vector3(Mathf.Clamp(value, 0f, x), rightSuperLine.localScale.y, rightSuperLine.localScale.z);
		leftSuperLine.localScale = new Vector3(Mathf.Clamp(value, 0f, x), leftSuperLine.localScale.y, leftSuperLine.localScale.z);
	}

	public void ApplyDropShadow()
	{
		ClearDropShadow();
		if (leftSuperLine.parent.gameObject.activeSelf)
		{
			for (int i = 0; i < superTitles.Length; i++)
			{
				ApplyDropShadow(superTitles[i]);
			}
		}
		ApplyDropShadow(rightTitle);
		ApplyDropShadow(leftTitles[0]);
		ApplyDropShadow(leftSymbol);
		ApplyDropShadow(rightSymbol);
		shadowRens = new MeshRenderer[shadow.parent.childCount];
		shadowStart = new Color[shadowRens.Length];
		for (int j = 0; j < shadowRens.Length; j++)
		{
			shadowRens[j] = shadow.parent.GetChild(j).GetComponent<MeshRenderer>();
			shadowStart[j] = shadowRens[j].material.color;
		}
	}

	protected void ClearDropShadow()
	{
		foreach (Transform item in shadow.parent)
		{
			UnityEngine.Object.DestroyImmediate(item.gameObject);
		}
		shadowRens = new MeshRenderer[0];
	}

	public void ApplyDropShadow(MeshRenderer icon)
	{
		MeshRenderer component = shadow.prefab.GetComponent<MeshRenderer>();
		CreateDropShadow(icon.transform.position + shadow.offset, component.bounds.size.x * shadow.size * shadow.iconRatio);
	}

	public void ApplyDropShadow(DynamicText text)
	{
		MeshRenderer component = text.GetComponent<MeshRenderer>();
		MeshRenderer component2 = shadow.prefab.GetComponent<MeshRenderer>();
		Bounds bounds = component.bounds;
		if (bounds.size == Vector3.zero)
		{
			return;
		}
		string text2 = text.textSB.ToString();
		float num = bounds.size.x / (float)text2.Length;
		float num2 = num * (1f - text.letterSpacing / (float)(text2.Length - 1) * (float)text2.Length);
		if (num2 == 0f || num2 == float.PositiveInfinity || num2 == float.NegativeInfinity)
		{
			return;
		}
		Vector3 pos = new Vector3(bounds.min.x + num2 / 2f, bounds.center.y, bounds.center.z) + shadow.offset * (num2 * 5f + 1f) / 2f;
		float size = num2 / component2.bounds.size.x * shadow.size;
		string text3 = text2;
		foreach (char c in text3)
		{
			if (!char.IsWhiteSpace(c))
			{
				CreateDropShadow(pos, size);
			}
			pos.x += num;
		}
	}

	private void CreateDropShadow(Vector3 pos, float size)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate(shadow.prefab, pos, Quaternion.Euler(90f, 0f, 0f)) as GameObject;
		gameObject.transform.localScale *= size;
		gameObject.transform.SetParent(shadow.parent, true);
	}

	public void Fade(float speed, float delay)
	{
		StartCoroutine(IEFade(speed, delay));
	}

	public IEnumerator IEFade(float speed, float delay)
	{
		SetTextAndSymbols(0f);
		SetShadows(0f);
		yield return new WaitForSeconds(delay);
		StartCoroutine(FadeShadows(transition.fadeDuration * 1.1f / speed, 0f, 1f));
		yield return StartCoroutine(FadeTextAndSymbols(transition.fadeDuration / speed, 0f, 1f));
		yield return new WaitForSeconds(transition.sustain);
		StartCoroutine(FadeShadows(transition.fadeDuration * 1.25f / speed, 1f, 0f));
		StartCoroutine(FadeTextAndSymbols(transition.fadeDuration / speed, 1f, 0f));
	}

	public IEnumerator FadeTextAndSymbols(float duration, float from, float to)
	{
		for (float f = 0f; f < duration; f += Time.unscaledDeltaTime)
		{
			float percent = f / duration;
			SetTextAndSymbols(Mathf.Lerp(from, to, percent));
			yield return null;
		}
		SetTextAndSymbols(to);
	}

	public void SetTextAndSymbols(float alpha)
	{
		for (int i = 0; i < rens.Length; i++)
		{
			rens[i].material.color = new Color(1f, 1f, 1f, alpha * start[i].a);
		}
	}

	public IEnumerator FadeShadows(float duration, float from, float to)
	{
		for (float f = 0f; f < duration; f += Time.unscaledDeltaTime)
		{
			float percent = f / duration;
			SetShadows(Mathf.Lerp(from, to, percent));
			yield return null;
		}
		SetShadows(to);
	}

	private void OnDestroy()
	{
		ReferenceMaster.onLevelLoad = (Action)Delegate.Remove(ReferenceMaster.onLevelLoad, new Action(LoadMPLevel));
	}

	public void SetShadows(float alpha)
	{
		for (int i = 0; i < shadowRens.Length; i++)
		{
			shadowRens[i].material.color = new Color(shadowStart[i].r, shadowStart[i].g, shadowStart[i].b, alpha * shadowStart[i].a);
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Localisation;
using UnityEngine;

public class InfoBoxController : MonoBehaviour
{
	public TextMesh nameText;

	public TextMesh fullNameText;

	public TextMesh infoText;

	public int myID;

	public Transform visParent;

	public float fadeInSpeed = 0.2f;

	public float fadeOutSpeed = 0.2f;

	public Texture2D[] icons;

	public Renderer iconRendy;

	public Renderer iconBGRendy;

	private static string[,] namesHuman;

	private List<Renderer> myRenderers = new List<Renderer>();

	private List<Color> rendererOnCols = new List<Color>();

	private List<Color> rendererOffCols = new List<Color>();

	private List<TextMesh> textMeshes = new List<TextMesh>();

	private List<Color> textMeshOnColours = new List<Color>();

	private List<Color> textMeshOffColours = new List<Color>();

	private ListOfNames nameHolder;

	private void Awake()
	{
		nameHolder = SingleInstance<ListOfNames>.Instance;
	}

	private void Start()
	{
		GetNames();
		myRenderers.Clear();
		for (int i = 0; i < visParent.childCount; i++)
		{
			if ((bool)visParent.GetChild(i).GetComponent<TextMesh>())
			{
				textMeshes.Add(visParent.GetChild(i).GetComponent<TextMesh>());
				textMeshOnColours.Add(visParent.GetChild(i).GetComponent<TextMesh>().color);
				Color color = visParent.GetChild(i).GetComponent<TextMesh>().color;
				color.a = 0f;
				textMeshOffColours.Add(color);
				continue;
			}
			Transform child = visParent.GetChild(i);
			Renderer component = child.GetComponent<Renderer>();
			if ((bool)component)
			{
				myRenderers.Add(component);
				rendererOnCols.Add(component.material.GetColor("_TintColor"));
				Color color2 = component.material.GetColor("_TintColor");
				color2.a = 0f;
				rendererOffCols.Add(color2);
			}
		}
	}

	public void SetInfo(string shortName, string fullName, InjuryType info)
	{
		string empty = string.Empty;
		switch (info)
		{
		case InjuryType.Blunt:
			empty = LocalisationManager.GetTranslation(2031);
			break;
		case InjuryType.Sharp:
			empty = LocalisationManager.GetTranslation(2032);
			break;
		case InjuryType.Fire:
			empty = LocalisationManager.GetTranslation(2033);
			break;
		case InjuryType.Suffocateing:
			empty = LocalisationManager.GetTranslation(4452);
			break;
		default:
			empty = LocalisationManager.GetTranslation(2031);
			break;
		}
		SetInfo(shortName, fullName, info, empty);
	}

	public void SetInfo(string shortName, string fullName, InjuryType info, string text)
	{
		infoText.text = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(text.ToLower());
		switch (info)
		{
		case InjuryType.Blunt:
			iconRendy.material.mainTexture = icons[Random.Range(0, 2)];
			break;
		case InjuryType.Sharp:
			iconRendy.material.mainTexture = icons[2];
			break;
		case InjuryType.Fire:
			iconRendy.material.mainTexture = icons[3];
			break;
		case InjuryType.Suffocateing:
			if (icons.Length > 4)
			{
				iconRendy.material.mainTexture = icons[4];
			}
			else
			{
				iconRendy.material.mainTexture = icons[1];
			}
			break;
		}
		if (shortName == null || shortName == string.Empty)
		{
			shortName = namesHuman[Random.Range(0, namesHuman.GetUpperBound(0)), 0];
		}
		if (fullName == null || fullName == string.Empty)
		{
			fullName = shortName + " " + namesHuman[0, Random.Range(0, namesHuman.GetUpperBound(1))];
		}
		fullNameText.text = fullName;
		nameText.text = shortName.ToUpper();
	}

	public void FadeIn()
	{
		GetComponent<Collider>().enabled = true;
		StopAllCoroutines();
		for (int i = 0; i < myRenderers.Count; i++)
		{
			StartCoroutine(FadeInLerp(i));
		}
		for (int j = 0; j < textMeshes.Count; j++)
		{
			StartCoroutine(FadeInText(j));
		}
	}

	public void FadeOut()
	{
		GetComponent<Collider>().enabled = false;
		StopAllCoroutines();
		for (int i = 0; i < myRenderers.Count; i++)
		{
			StartCoroutine(FadeOutLerp(i));
		}
		for (int j = 0; j < textMeshes.Count; j++)
		{
			StartCoroutine(FadeOutText(j));
		}
	}

	private IEnumerator FadeInLerp(int index)
	{
		myRenderers[index].enabled = true;
		float cTime = 0f;
		float rate = 1f / fadeInSpeed;
		Color startCol = rendererOffCols[index];
		while (cTime < 1f)
		{
			cTime += TimeSlider.Instance.deltaTime * rate;
			Color delegateColor = Color.Lerp(startCol, rendererOnCols[index], cTime);
			myRenderers[index].material.SetColor("_TintColor", delegateColor);
			yield return null;
		}
	}

	private IEnumerator FadeOutLerp(int index)
	{
		float cTime = 0f;
		float rate = 1f / fadeOutSpeed;
		Color startCol = myRenderers[index].material.GetColor("_TintColor");
		while (cTime < 1f)
		{
			cTime += TimeSlider.Instance.deltaTime * rate;
			Color delegateColor = Color.Lerp(startCol, rendererOffCols[index], cTime);
			myRenderers[index].material.SetColor("_TintColor", delegateColor);
			yield return null;
		}
		myRenderers[index].enabled = false;
	}

	private IEnumerator FadeInText(int index)
	{
		textMeshes[index].GetComponent<Renderer>().enabled = true;
		float cTime = 0f;
		float rate = 1f / fadeInSpeed;
		Color startCol = textMeshOffColours[index];
		while (cTime < 1f)
		{
			cTime += TimeSlider.Instance.deltaTime * rate;
			textMeshes[index].color = Color.Lerp(startCol, textMeshOnColours[index], cTime);
			yield return null;
		}
	}

	private IEnumerator FadeOutText(int index)
	{
		float cTime = 0f;
		float rate = 1f / fadeOutSpeed;
		Color startCol = textMeshes[index].color;
		while (cTime < 1f)
		{
			cTime += TimeSlider.Instance.deltaTime * rate;
			textMeshes[index].color = Color.Lerp(startCol, textMeshOffColours[index], cTime);
			yield return null;
		}
		textMeshes[index].GetComponent<Renderer>().enabled = false;
	}

	private void GetNames()
	{
		string[] array = ((!SingleInstance<StatMaster>.Instance.LowViolence) ? nameHolder.firstNames : nameHolder.surnames);
		string[] array2 = ((!SingleInstance<StatMaster>.Instance.LowViolence) ? nameHolder.surnames : nameHolder.firstNames);
		namesHuman = new string[array.Length, array2.Length];
		for (int i = 0; i < array.Length; i++)
		{
			namesHuman[i, 0] = array[i];
		}
		for (int j = 0; j < array2.Length; j++)
		{
			namesHuman[0, j] = array2[j];
		}
	}

	public static string GetName()
	{
		string text = namesHuman[Random.Range(0, namesHuman.GetUpperBound(0)), 0];
		return text + " " + namesHuman[0, Random.Range(0, namesHuman.GetUpperBound(1))];
	}
}

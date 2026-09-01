using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInOnSimulate : MonoBehaviour
{
	public Renderer[] renderers;

	public TextMesh[] textMeshes;

	public GameObject[] gameObjects;

	public bool turnOffOnAwake = true;

	public float lerpInSpeed = 0.8f;

	public float lerpOutSpeed = 0.8f;

	public float waitAtStartBeforeFade;

	private bool objectsActive = true;

	private List<Color> textMeshOnColours = new List<Color>();

	private List<Color> textMeshOffColours = new List<Color>();

	private List<Color> rendererOnCols = new List<Color>();

	private List<Color> rendererOffCols = new List<Color>();

	public bool isActivated = true;

	private void Awake()
	{
		for (int i = 0; i < renderers.Length; i++)
		{
			Color color = renderers[i].material.GetColor("_TintColor");
			rendererOnCols.Add(color);
			color.a = 0f;
			rendererOffCols.Add(color);
		}
		UpdateTextMeshes();
		if (gameObjects.Length == 0)
		{
			RotateBob[] componentsInChildren = GetComponentsInChildren<RotateBob>();
			List<GameObject> list = new List<GameObject>();
			RotateBob[] array = componentsInChildren;
			foreach (RotateBob rotateBob in array)
			{
				list.Add(rotateBob.gameObject);
			}
			gameObjects = list.ToArray();
		}
		if (turnOffOnAwake)
		{
			SetAllRenderersOff();
		}
	}

	private IEnumerator Start()
	{
		yield return new WaitForSeconds((!StatMaster.isMP) ? waitAtStartBeforeFade : 0f);
		StartFade();
	}

	private void OnEnable()
	{
		ReferenceMaster.onLevelSimulation = (Action<bool>)Delegate.Combine(ReferenceMaster.onLevelSimulation, new Action<bool>(OnSimulationToggle));
	}

	private void OnDisable()
	{
		ReferenceMaster.onLevelSimulation = (Action<bool>)Delegate.Remove(ReferenceMaster.onLevelSimulation, new Action<bool>(OnSimulationToggle));
	}

	private void OnSimulationToggle(bool toggle)
	{
		if (isActivated)
		{
			StopAllCoroutines();
			if (toggle)
			{
				EndFade();
			}
			else
			{
				StartFade();
			}
		}
	}

	public void UpdateTextMeshes()
	{
		textMeshOnColours.Clear();
		textMeshOffColours.Clear();
		for (int i = 0; i < textMeshes.Length; i++)
		{
			TextMesh textMesh = textMeshes[i];
			if (!(textMesh == null))
			{
				Color color = textMesh.color;
				textMeshOnColours.Add(color);
				color.a = 0f;
				textMeshOffColours.Add(color);
			}
		}
	}

	private void StartFade()
	{
		ToggleObjects(true);
		for (int i = 0; i < renderers.Length; i++)
		{
			StartCoroutine(IEFadeIn(i));
		}
		for (int j = 0; j < textMeshes.Length; j++)
		{
			StartCoroutine(IEFadeInText(j));
		}
	}

	private void EndFade()
	{
		StartCoroutine(IEFadeOutAll());
		for (int i = 0; i < textMeshes.Length; i++)
		{
			if (textMeshes[i] != null)
			{
				StartCoroutine(IEFadeOutText(i));
			}
		}
	}

	private IEnumerator IEFadeIn(int index)
	{
		Renderer currentRenderer = renderers[index];
		currentRenderer.enabled = true;
		float cTime = 0f;
		float rate = 1f / lerpInSpeed;
		Color startCol = rendererOffCols[index];
		while (cTime < 1f)
		{
			cTime += TimeSlider.Instance.deltaTime * rate;
			Color delegateColor = Color.Lerp(startCol, rendererOnCols[index], cTime);
			currentRenderer.material.SetColor("_TintColor", delegateColor);
			yield return null;
		}
	}

	private IEnumerator IEFadeOutAll()
	{
		float cTime = 0f;
		float rate = 1f / lerpOutSpeed;
		Color[] startCol = new Color[renderers.Length];
		for (int i = 0; i < startCol.Length; i++)
		{
			startCol[i] = renderers[i].material.GetColor("_TintColor");
		}
		while (cTime < 1f)
		{
			cTime += TimeSlider.Instance.deltaTime * rate;
			for (int j = 0; j < startCol.Length; j++)
			{
				Color delegateColor = Color.Lerp(startCol[j], rendererOffCols[j], cTime);
				renderers[j].material.SetColor("_TintColor", delegateColor);
			}
			yield return null;
		}
		for (int k = 0; k < startCol.Length; k++)
		{
			renderers[k].enabled = false;
		}
	}

	private IEnumerator IEFadeInText(int index)
	{
		TextMesh textMesh = textMeshes[index];
		if (!(textMesh == null))
		{
			textMesh.GetComponent<Renderer>().enabled = true;
			float cTime = 0f;
			float rate = 1f / lerpInSpeed;
			Color startCol = textMeshOffColours[index];
			while (cTime < 1f)
			{
				cTime += TimeSlider.Instance.deltaTime * rate;
				textMesh.color = Color.Lerp(startCol, textMeshOnColours[index], cTime);
				yield return null;
			}
		}
	}

	private IEnumerator IEFadeOutText(int index)
	{
		TextMesh textMesh = textMeshes[index];
		float cTime = 0f;
		float rate = 1f / lerpOutSpeed;
		Color startCol = textMesh.color;
		while (cTime < 1f)
		{
			cTime += TimeSlider.Instance.deltaTime * rate;
			textMesh.color = Color.Lerp(startCol, textMeshOffColours[index], cTime);
			yield return null;
		}
		textMesh.GetComponent<Renderer>().enabled = false;
		ToggleObjects(false);
	}

	public void Toggle(bool toggle)
	{
		isActivated = toggle;
	}

	private void ToggleObjects(bool toggle)
	{
		if (objectsActive != toggle)
		{
			for (int i = 0; i < gameObjects.Length; i++)
			{
				gameObjects[i].SetActive(toggle);
			}
			objectsActive = toggle;
		}
	}

	public void SetAllRenderersOff()
	{
		for (int i = 0; i < renderers.Length; i++)
		{
			renderers[i].enabled = false;
			renderers[i].material.SetColor("_TintColor", rendererOffCols[i]);
		}
		for (int j = 0; j < textMeshes.Length; j++)
		{
			TextMesh textMesh = textMeshes[j];
			if (!(textMesh == null))
			{
				textMesh.color = textMeshOffColours[j];
				textMesh.GetComponent<Renderer>().enabled = false;
			}
		}
		ToggleObjects(false);
	}
}

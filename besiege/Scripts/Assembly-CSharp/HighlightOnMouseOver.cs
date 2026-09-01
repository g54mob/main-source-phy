using System.Collections;
using UnityEngine;

public class HighlightOnMouseOver : MonoBehaviour
{
	public Renderer rendy;

	public Renderer[] extraRenderes = new Renderer[0];

	public Material highlightMaterial;

	public bool lerpValue;

	public float lerpSpeed;

	public string valueToLerp = "_Emission";

	public Color colourToLerpTo;

	public int mask = -1;

	private Material[] startMaterial;

	private Color[] startCol;

	private bool isMouseOver;

	private IEnumerator fadeColCoroutine;

	private void Awake()
	{
		startMaterial = new Material[1 + extraRenderes.Length];
		startCol = new Color[1 + extraRenderes.Length];
		startMaterial[0] = rendy.material;
		if (lerpValue)
		{
			startCol[0] = rendy.material.GetColor(valueToLerp);
		}
		for (int i = 0; i < extraRenderes.Length; i++)
		{
			startMaterial[i + 1] = extraRenderes[i].material;
			if (lerpValue)
			{
				startCol[i + 1] = extraRenderes[i].material.GetColor(valueToLerp);
			}
		}
	}

	private void OnMouseEnter()
	{
		if (!UIMask.InsideMask(mask, base.transform.position))
		{
			if (isMouseOver)
			{
				OnMouseExit();
			}
			return;
		}
		isMouseOver = true;
		if (lerpValue)
		{
			if (fadeColCoroutine != null)
			{
				StopCoroutine(fadeColCoroutine);
			}
			fadeColCoroutine = LerpCol(colourToLerpTo);
			StartCoroutine(fadeColCoroutine);
			return;
		}
		highlightMaterial.SetColor(valueToLerp, colourToLerpTo);
		rendy.material = highlightMaterial;
		for (int i = 0; i < extraRenderes.Length; i++)
		{
			highlightMaterial.SetColor(valueToLerp, colourToLerpTo);
			extraRenderes[i].material = highlightMaterial;
		}
	}

	private void OnMouseOver()
	{
		if (!UIMask.InsideMask(mask, base.transform.position) && isMouseOver)
		{
			OnMouseExit();
		}
	}

	private void OnMouseExit()
	{
		isMouseOver = false;
		if (lerpValue)
		{
			if (fadeColCoroutine != null)
			{
				StopCoroutine(fadeColCoroutine);
			}
			fadeColCoroutine = LerpCol(startCol);
			StartCoroutine(fadeColCoroutine);
		}
		else
		{
			rendy.material = startMaterial[0];
			for (int i = 0; i < extraRenderes.Length; i++)
			{
				extraRenderes[i].material = startMaterial[i + 1];
			}
		}
	}

	private void OnDisable()
	{
		isMouseOver = false;
		if (lerpValue)
		{
			rendy.material.SetColor(valueToLerp, startCol[0]);
			for (int i = 1; i < extraRenderes.Length; i++)
			{
				extraRenderes[i].material.SetColor(valueToLerp, startCol[i]);
			}
		}
		else
		{
			rendy.material = startMaterial[0];
			for (int j = 0; j < extraRenderes.Length; j++)
			{
				extraRenderes[j].material = startMaterial[j + 1];
			}
		}
	}

	private IEnumerator LerpCol(Color[] endCol)
	{
		float cTime = 0f;
		float rate = 1f / lerpSpeed;
		Color[] currentCol = new Color[1 + extraRenderes.Length];
		currentCol[0] = rendy.material.GetColor(valueToLerp);
		for (int i = 0; i < extraRenderes.Length; i++)
		{
			currentCol[i + 1] = extraRenderes[i].material.GetColor(valueToLerp);
		}
		while (cTime < 1f)
		{
			cTime += Time.deltaTime * rate;
			rendy.material.SetColor(valueToLerp, Color.Lerp(currentCol[0], endCol[0], cTime));
			for (int j = 0; j < extraRenderes.Length; j++)
			{
				extraRenderes[j].material.SetColor(valueToLerp, Color.Lerp(currentCol[j + 1], endCol[j + 1], cTime));
			}
			yield return null;
		}
	}

	private IEnumerator LerpCol(Color endCol)
	{
		float cTime = 0f;
		float rate = 1f / lerpSpeed;
		Color[] currentCol = new Color[1 + extraRenderes.Length];
		currentCol[0] = rendy.material.GetColor(valueToLerp);
		for (int i = 1; i < extraRenderes.Length; i++)
		{
			currentCol[i] = extraRenderes[i].material.GetColor(valueToLerp);
		}
		while (cTime < 1f)
		{
			cTime += Time.deltaTime * rate;
			rendy.material.SetColor(valueToLerp, Color.Lerp(currentCol[0], endCol, cTime));
			for (int j = 0; j < extraRenderes.Length; j++)
			{
				extraRenderes[j].material.SetColor(valueToLerp, Color.Lerp(currentCol[j + 1], endCol, cTime));
			}
			yield return null;
		}
	}
}

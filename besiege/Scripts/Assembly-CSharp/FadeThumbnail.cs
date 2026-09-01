using UnityEngine;

public class FadeThumbnail : MonoBehaviour
{
	public Material mat;

	public float alphaToLerpTo = 0.6f;

	public Color colToBe;

	public bool mouseOver;

	public float lerpSpeed = 0.015f;

	private void Start()
	{
		colToBe = mat.GetColor("_TintColor");
	}

	private void OnMouseEnter()
	{
		mouseOver = true;
	}

	private void OnMouseExit()
	{
		mouseOver = false;
	}

	private void Update()
	{
		if (mouseOver)
		{
			alphaToLerpTo = 0.6f;
		}
		else
		{
			alphaToLerpTo = 0f;
		}
		colToBe.a = Mathf.Lerp(colToBe.a, alphaToLerpTo, Time.deltaTime * lerpSpeed);
		mat.SetColor("_TintColor", colToBe);
	}
}

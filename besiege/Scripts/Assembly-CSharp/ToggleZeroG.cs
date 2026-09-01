using System.Collections;
using Localisation;
using UnityEngine;

public class ToggleZeroG : ToggleGodModeButton, ILocalisationAware
{
	private float startGrav = -32.81f;

	public TextMesh[] texts;

	public Transform iconPivot;

	private Vector3[] orgScales;

	private Vector3[] actualScales;

	private void Awake()
	{
		if (IsRuleOn())
		{
			Set();
		}
		string text = texts[0].text;
		string text2 = texts[1].text;
		texts[0].text = "ZERO";
		texts[1].text = "G";
		orgScales = new Vector3[texts.Length];
		for (int i = 0; i < texts.Length; i++)
		{
			orgScales[i] = texts[i].GetComponent<Renderer>().bounds.size;
		}
		actualScales = new Vector3[texts.Length];
		for (int j = 0; j < texts.Length; j++)
		{
			actualScales[j] = texts[j].transform.localScale;
		}
		texts[0].text = text;
		texts[1].text = text2;
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		StartCoroutine(Align());
	}

	public void ResetScale()
	{
		for (int i = 0; i < texts.Length; i++)
		{
			texts[i].transform.localScale = actualScales[i];
		}
	}

	public void Scale()
	{
		for (int i = 0; i < texts.Length; i++)
		{
			Vector3 size = texts[i].GetComponent<Renderer>().bounds.size;
			float num = orgScales[i].x / size.x;
			texts[i].transform.localScale = actualScales[i] * (num + 1f) * 0.5f;
		}
	}

	public void Center()
	{
		Bounds bounds = new Bounds(iconPivot.transform.position, Vector3.zero);
		for (int i = 0; i < texts.Length; i++)
		{
			bounds.Encapsulate(texts[i].GetComponent<Renderer>().bounds);
		}
		float num = bounds.center.y - iconPivot.transform.position.y;
		for (int j = 0; j < texts.Length; j++)
		{
			texts[j].transform.position -= num * Vector3.up;
		}
	}

	public override string GetModeName()
	{
		return "ZeroG";
	}

	public override bool IsRuleOn()
	{
		return StatMaster.GodTools.GravityDisabled;
	}

	public override void ToggleRule(bool toggle)
	{
		StatMaster.GodTools.GravityDisabled = toggle;
		Physics.gravity = new Vector3(Physics.gravity.x, (!toggle) ? startGrav : 0f, Physics.gravity.z);
	}

	public void OnLocalisationChange()
	{
		if (base.gameObject.activeInHierarchy)
		{
			StartCoroutine(Align());
		}
	}

	public IEnumerator Align()
	{
		if (lockIcon != null)
		{
			lockIcon.SetActive(IsRuleLocked());
		}
		if ((bool)iconPivot)
		{
			ResetScale();
			yield return new WaitForEndOfFrame();
			Scale();
			Center();
		}
	}
}

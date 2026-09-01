using System;
using System.Collections;
using UnityEngine;

public class ToggleSettings : ClickBehaviour
{
	public Transform settingsParent;

	public bool settingsOn;

	public float lerpOutSpeed = 0.15f;

	public float lerpInSpeed = 0.15f;

	public Vector3 lerpPosDirection = Vector3.up;

	public Renderer bgRenderer;

	public GameObject hideOnClient;

	public GameObject showOnClient;

	public Material redMaterial;

	public Material darkMaterial;

	private Vector3 settingsParentStartPos;

	public GameObject[] hide;

	public bool onHover;

	public CursorHoverHook[] additionalHoverAreas;

	private int hoverCount;

	private void Start()
	{
		bgRenderer.enabled = false;
		settingsParentStartPos = settingsParent.localPosition;
		settingsParent.localPosition = settingsParentStartPos - lerpPosDirection;
		settingsParent.gameObject.SetActive(false);
		for (int i = 0; i < hide.Length; i++)
		{
			hide[i].SetActive(true);
		}
		if (onHover)
		{
			for (int j = 0; j < additionalHoverAreas.Length; j++)
			{
				CursorHoverHook obj = additionalHoverAreas[j];
				obj.onCursorEnter = (Action)Delegate.Combine(obj.onCursorEnter, new Action(OnMouseEnter));
				CursorHoverHook obj2 = additionalHoverAreas[j];
				obj2.onCursorExit = (Action)Delegate.Combine(obj2.onCursorExit, new Action(OnMouseExit));
			}
		}
	}

	public override void OnDisable()
	{
		base.OnDisable();
		hoverCount = 0;
	}

	public override void OnClicked()
	{
		if (!onHover)
		{
			Changed();
		}
	}

	public void OnMouseEnter()
	{
		hoverCount++;
	}

	public void OnMouseExit()
	{
		hoverCount--;
	}

	protected override void LateUpdate()
	{
		base.LateUpdate();
		if (!onHover)
		{
			return;
		}
		if (settingsOn)
		{
			if (hoverCount <= 0)
			{
				hoverCount = 0;
				Changed();
			}
		}
		else if (hoverCount > 0)
		{
			Changed();
		}
	}

	public void Changed()
	{
		if ((bool)hideOnClient)
		{
			hideOnClient.SetActive(!StatMaster.isClient);
		}
		if ((bool)showOnClient)
		{
			showOnClient.SetActive(StatMaster.isClient);
		}
		Toggle();
	}

	public void Toggle()
	{
		if (settingsOn)
		{
			settingsOn = false;
			bgRenderer.enabled = false;
			if (base.gameObject.activeInHierarchy)
			{
				StartCoroutine(LerpPosOut());
			}
			else
			{
				settingsParent.localPosition = settingsParentStartPos - lerpPosDirection;
			}
		}
		else
		{
			settingsOn = true;
			bgRenderer.enabled = true;
			if (base.gameObject.activeInHierarchy)
			{
				StartCoroutine(LerpPosIn());
			}
			else
			{
				settingsParent.localPosition = settingsParentStartPos;
			}
		}
	}

	private IEnumerator LerpPosIn()
	{
		float cTime = 0f;
		float rate = 1f / lerpInSpeed;
		settingsParent.gameObject.SetActive(true);
		for (int i = 0; i < hide.Length; i++)
		{
			hide[i].SetActive(false);
		}
		Vector3 startPosy = settingsParent.localPosition;
		while (cTime < 1f)
		{
			cTime += TimeSlider.Instance.deltaTime * rate;
			settingsParent.localPosition = Vector3.Lerp(startPosy - lerpPosDirection, settingsParentStartPos, cTime);
			yield return null;
		}
	}

	private IEnumerator LerpPosOut()
	{
		float cTime = 0f;
		float rate = 1f / lerpOutSpeed;
		Vector3 startPosy = settingsParent.localPosition;
		while (cTime < 1f)
		{
			cTime += TimeSlider.Instance.deltaTime * rate;
			settingsParent.localPosition = Vector3.Lerp(startPosy, settingsParentStartPos - lerpPosDirection, cTime);
			yield return null;
		}
		settingsParent.gameObject.SetActive(false);
		for (int i = 0; i < hide.Length; i++)
		{
			hide[i].SetActive(true);
		}
	}
}

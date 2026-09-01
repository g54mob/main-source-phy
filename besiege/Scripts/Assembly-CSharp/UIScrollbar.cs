using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[AddComponentMenu("UI/UI Scrollbar")]
public class UIScrollbar : ClickBehaviour
{
	public Transform scroller;

	public Transform scrollBG;

	public GameObject[] objectsToToggle;

	public Camera camUsed;

	public Transform contentParent;

	public Transform contentMask;

	public float scrollStep = 1f;

	public bool hideScrollFully = true;

	protected Vector3 orgContentPos = Vector3.zero;

	protected Vector3 orgFirstElementLocalPos = Vector3.zero;

	protected Bounds contentBounds;

	protected float contentSize = 1f;

	protected float maskMax = 1f;

	protected float maskMin;

	protected float yOffset;

	protected float yMax = 1f;

	protected float yMin;

	protected float spectrumSize = 1f;

	protected float relativeScrollerPosPct;

	protected bool setup;

	protected Vector3 mp = Vector3.zero;

	protected UIMask cMask;

	protected bool hasCMask;

	private bool isDirty;

	[HideInInspector]
	public bool active = true;

	private void Awake()
	{
		SetupCamUsed();
		hasCMask = contentMask != null;
		if (hasCMask)
		{
			cMask = contentMask.GetComponent<UIMask>();
		}
		UpdateOriginalContentPos();
		StartCoroutine(WaitAndUpdateBoundsIE());
		ReferenceMaster.onResolutionChanged = (Action)Delegate.Combine(ReferenceMaster.onResolutionChanged, new Action(OnResolutionChanged));
	}

	private void OnResolutionChanged()
	{
		UpdateOriginalContentPos();
		if (base.gameObject.activeInHierarchy)
		{
			StartCoroutine(WaitAndUpdateBoundsIE());
		}
	}

	private void OnDestroy()
	{
		ReferenceMaster.onResolutionChanged = (Action)Delegate.Remove(ReferenceMaster.onResolutionChanged, new Action(OnResolutionChanged));
	}

	private void SetupCamUsed()
	{
		if (camUsed != null)
		{
			return;
		}
		GameObject[] array = GameObject.FindGameObjectsWithTag("hudCamera");
		for (int i = 0; i < array.Length; i++)
		{
			if (!(array[i] == null))
			{
				if (array[i].name.Contains("(Clone)"))
				{
					UnityEngine.Object.Destroy(array[i]);
				}
				else if (camUsed == null)
				{
					camUsed = array[i].GetComponent<Camera>();
				}
			}
		}
	}

	private IEnumerator WaitAndUpdateBoundsIE()
	{
		yield return new WaitForEndOfFrame();
		UpdateBounds();
		isDirty = false;
	}

	private void UpdateOriginalContentPos()
	{
		_wasClicked = false;
		orgContentPos = contentParent.localPosition;
		isDirty = true;
	}

	public void ResetContentPos()
	{
		_wasClicked = false;
		SetPct(0f);
	}

	private void OnEnable()
	{
		UpdateOriginalContentPos();
		if (isDirty)
		{
			StartCoroutine(WaitAndUpdateBoundsIE());
		}
	}

	public override void OnDisable()
	{
		base.OnDisable();
		ResetContentPos();
		StatMaster.stopCamZoom = false;
	}

	private void Update()
	{
		bool flag = false;
		if (hasCMask)
		{
			mp = new Vector3(InputManager.CursorPosition().x, InputManager.CursorPosition().y, 10f);
			Vector3 position = camUsed.ScreenToWorldPoint(mp);
			flag = cMask.InsideMask(position);
			if (flag)
			{
				flag = position.y > contentBounds.min.y;
			}
			StatMaster.stopCamZoom = flag;
		}
		if (!hasCMask || flag)
		{
			float num = InputManager.ScrollFieldValue();
			if (num != 0f && spectrumSize != 0f && !(contentSize < contentMask.lossyScale.y))
			{
				float yPos = Mathf.Clamp(scroller.position.y + num * scrollStep, yMin, yMax);
				SetPosition(yPos);
			}
		}
	}

	public void UpdateBounds()
	{
		UpdateBounds(contentParent.GetComponentsInChildren<MeshRenderer>());
	}

	public void UpdateBounds(Transform[] parents)
	{
		List<MeshRenderer> list = new List<MeshRenderer>();
		for (int i = 0; i < parents.Length; i++)
		{
			list.Concat(parents[i].GetComponentsInChildren<MeshRenderer>());
		}
		UpdateBounds(list.ToArray());
	}

	public void UpdateBounds(MeshRenderer[] renderers)
	{
		_wasClicked = false;
		bool flag = false;
		if (renderers.Length <= 0 || renderers[0] == null)
		{
			contentSize = 0f;
			flag = true;
			DisableScrollbar();
		}
		else
		{
			orgFirstElementLocalPos = contentParent.GetChild(0).localPosition;
			EnableScrollbar();
			contentBounds = new Bounds(new Vector3(0f, renderers[0].transform.position.y, 0f), Vector3.zero);
			foreach (Renderer renderer in renderers)
			{
				if (!(renderer == null) && !renderer.gameObject.CompareTag("BlockIcon") && !renderer.gameObject.CompareTag("TooltipIgnored") && !(renderer.gameObject.GetComponent<DynamicText>() != null) && renderer.bounds.size != Vector3.zero)
				{
					contentBounds.Encapsulate(renderer.bounds);
				}
			}
			contentSize = contentBounds.size.y;
			if (contentSize < contentMask.lossyScale.y)
			{
				DisableScrollbar();
				flag = true;
			}
			else
			{
				scroller.localScale = new Vector3(scroller.localScale.x, contentMask.localScale.y / contentSize * scrollBG.lossyScale.y, scroller.localScale.z);
			}
		}
		maskMax = contentMask.position.y - 0.5f * contentMask.lossyScale.y + contentSize;
		maskMin = contentMask.position.y + 0.5f * contentMask.lossyScale.y;
		yMax = scrollBG.position.y + 0.5f * scrollBG.lossyScale.y - 0.5f * scroller.lossyScale.y;
		yMin = scrollBG.position.y - 0.5f * scrollBG.lossyScale.y + 0.5f * scroller.lossyScale.y;
		if (yMax < yMin)
		{
			float num = yMin;
			yMin = yMax;
			yMax = num;
		}
		spectrumSize = yMax - yMin;
		if (spectrumSize == 0f)
		{
			relativeScrollerPosPct = 0f;
			DisableScrollbar();
			flag = true;
		}
		else if (!setup)
		{
			relativeScrollerPosPct = 0f;
			SetPct(relativeScrollerPosPct);
			setup = !flag;
		}
		else
		{
			float num2 = maskMax - maskMin;
			relativeScrollerPosPct = Mathf.Clamp((contentParent.position.y - maskMin) / num2, 0f, 1f);
			SetPct(relativeScrollerPosPct);
		}
	}

	public void EnableScrollbar()
	{
		_wasClicked = false;
		active = true;
		scroller.gameObject.SetActive(true);
		if (hideScrollFully)
		{
			scrollBG.gameObject.SetActive(true);
		}
		for (int i = 0; i < objectsToToggle.Length; i++)
		{
			objectsToToggle[i].SetActive(true);
		}
		base.gameObject.GetComponent<Collider>().enabled = true;
	}

	public void DisableScrollbar()
	{
		_wasClicked = false;
		setup = false;
		active = false;
		scroller.gameObject.SetActive(false);
		if (hideScrollFully)
		{
			scrollBG.gameObject.SetActive(false);
		}
		for (int i = 0; i < objectsToToggle.Length; i++)
		{
			objectsToToggle[i].SetActive(false);
		}
		base.gameObject.GetComponent<Collider>().enabled = false;
	}

	public override void OnClicked()
	{
		mp = new Vector3(InputManager.CursorPosition().x, InputManager.CursorPosition().y, 10f);
		float y = camUsed.ScreenToWorldPoint(mp).y;
		yOffset = y - scroller.position.y;
		if (y > scroller.position.y + 0.5f * scroller.lossyScale.y || y < scroller.position.y - 0.5f * scroller.lossyScale.y)
		{
			float yPos = Mathf.Clamp(y, yMin, yMax);
			SetPosition(yPos);
			yOffset = y - scroller.position.y;
		}
	}

	public override void OnClickHeld()
	{
		mp = new Vector3(InputManager.CursorPosition().x, InputManager.CursorPosition().y, 10f);
		float yPos = Mathf.Clamp(camUsed.ScreenToWorldPoint(mp).y - yOffset, yMin, yMax);
		SetPosition(yPos);
	}

	public void ScrollToElement(Transform element)
	{
		float num = base.transform.parent.position.y + orgContentPos.y * base.transform.parent.lossyScale.y;
		float num2 = num - (element.localPosition.y - orgFirstElementLocalPos.y) * contentParent.lossyScale.y;
		contentParent.position = new Vector3(contentParent.position.x, num2, contentParent.position.z);
		relativeScrollerPosPct = (num2 - maskMin) / (contentSize - contentMask.lossyScale.y);
		float num3 = (0f - relativeScrollerPosPct) * spectrumSize + yMax;
		SetPosition(num3);
		if (num3 > yMax || num3 < yMin)
		{
			num3 = Mathf.Clamp(num3, yMin, yMax);
			SetPosition(num3);
		}
	}

	public void SetPct(float pct)
	{
		scroller.position = new Vector3(scroller.position.x, Mathf.Lerp(yMin, yMax, 1f - pct), scroller.position.z);
		relativeScrollerPosPct = pct;
		SetContentPct(pct);
	}

	public void SetPosition(float yPos, bool setContent = true)
	{
		scroller.position = new Vector3(scroller.position.x, yPos, scroller.position.z);
		relativeScrollerPosPct = 1f - Mathf.InverseLerp(yMin, yMax, yPos);
		SetContentPct(relativeScrollerPosPct);
	}

	public void SetContentPct(float pct)
	{
		ClampContentPosition(maskMin + (contentSize - contentMask.lossyScale.y) * pct);
	}

	public void ClampContentPosition(float contentYPos)
	{
		contentParent.position = new Vector3(contentParent.position.x, contentYPos, contentParent.position.z);
	}
}

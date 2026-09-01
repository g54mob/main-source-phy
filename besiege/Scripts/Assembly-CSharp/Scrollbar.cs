using System.Collections;
using UnityEngine;

public class Scrollbar : ClickBehaviour
{
	public Transform scroller;

	public Transform scrollBG;

	public Camera camUsed;

	public Transform contentParent;

	public Transform contentMask;

	public GameObject highlightMsg;

	public float scrollStep = 1f;

	protected Vector3 orgContentPos = Vector3.zero;

	protected Bounds contentBounds;

	protected float contentSize = 1f;

	protected float maskMax = 1f;

	protected float maskMin;

	protected float yOffset;

	protected float yMax = 1f;

	protected float yMin;

	protected float spectrumSize = 1f;

	protected float relativeScrollerPosPct;

	protected Vector3 mp = Vector3.zero;

	private void OnEnable()
	{
		if (camUsed == null)
		{
			camUsed = Camera.main;
		}
		StartCoroutine(SetBounds());
		orgContentPos = contentParent.position;
		StatMaster.stopCamZoom = true;
	}

	public override void OnDisable()
	{
		base.OnDisable();
		contentParent.position = orgContentPos;
		StatMaster.stopCamZoom = false;
	}

	private void Update()
	{
		if (InputManager.ScrollFieldValue() != 0f)
		{
			float y = Mathf.Clamp(scroller.position.y + InputManager.ScrollFieldValue() * scrollStep, yMin, yMax);
			scroller.position = new Vector3(scroller.position.x, y, scroller.position.z);
			relativeScrollerPosPct = (yMax - scroller.position.y) / spectrumSize;
			float y2 = maskMin + (contentSize - contentMask.localScale.y) * relativeScrollerPosPct;
			contentParent.position = new Vector3(contentParent.position.x, y2, contentParent.position.z);
		}
	}

	private IEnumerator SetBounds()
	{
		yield return new WaitForEndOfFrame();
		Collider[] contentCols = contentParent.GetComponentsInChildren<Collider>();
		if (contentCols.Length == 0)
		{
			Debug.LogWarning("ContentParent has no content children, therefore no Colliders.");
			yield break;
		}
		contentBounds = new Bounds(new Vector3(0f, contentCols[0].transform.position.y, 0f), Vector3.zero);
		Collider[] array = contentCols;
		foreach (Collider col in array)
		{
			contentBounds.Encapsulate(col.bounds);
		}
		contentSize = contentBounds.size.y;
		if (contentSize < contentMask.localScale.y)
		{
			scroller.gameObject.SetActive(false);
			scrollBG.gameObject.SetActive(false);
			base.gameObject.GetComponent<Collider>().enabled = false;
		}
		else
		{
			scroller.gameObject.SetActive(true);
			scrollBG.gameObject.SetActive(true);
			base.gameObject.GetComponent<Collider>().enabled = true;
			scroller.localScale = new Vector3(scroller.localScale.x, contentMask.localScale.y / contentSize * scrollBG.localScale.y, scroller.localScale.z);
		}
		maskMax = contentMask.position.y - 0.5f * contentMask.localScale.y + contentSize;
		maskMin = contentMask.position.y + 0.5f * contentMask.localScale.y;
		yMax = scrollBG.position.y + 0.5f * scrollBG.localScale.y - 0.5f * scroller.localScale.y;
		yMin = scrollBG.position.y - 0.5f * scrollBG.localScale.y + 0.5f * scroller.localScale.y;
		spectrumSize = yMax - yMin;
		scroller.position = new Vector3(scroller.position.x, yMax, scroller.position.z);
		relativeScrollerPosPct = (yMax - scroller.position.y) / spectrumSize;
		float contentYPos = maskMin + (contentSize - contentMask.localScale.y) * relativeScrollerPosPct;
		contentParent.position = new Vector3(contentParent.position.x, contentYPos, contentParent.position.z);
		highlightMsg.SendMessage("SetHighlight", contentCols[0].transform, SendMessageOptions.DontRequireReceiver);
	}

	public override void OnClicked()
	{
		mp = new Vector3(InputManager.CursorPosition().x, InputManager.CursorPosition().y, 10f);
		float y = camUsed.ScreenToWorldPoint(mp).y;
		yOffset = y - scroller.position.y;
		if (y > scroller.position.y + 0.5f * scroller.localScale.y || y < scroller.position.y - 0.5f * scroller.localScale.y)
		{
			yOffset = 0f;
			float y2 = Mathf.Clamp(y, yMin, yMax);
			scroller.position = new Vector3(scroller.position.x, y2, scroller.position.z);
			relativeScrollerPosPct = (yMax - scroller.position.y) / spectrumSize;
			float y3 = maskMin + (contentSize - contentMask.localScale.y) * relativeScrollerPosPct;
			contentParent.position = new Vector3(contentParent.position.x, y3, contentParent.position.z);
		}
	}

	public override void OnClickHeld()
	{
		mp = new Vector3(InputManager.CursorPosition().x, InputManager.CursorPosition().y, 10f);
		float y = Mathf.Clamp(camUsed.ScreenToWorldPoint(mp).y - yOffset, yMin, yMax);
		scroller.position = new Vector3(scroller.position.x, y, scroller.position.z);
		relativeScrollerPosPct = (yMax - scroller.position.y) / spectrumSize;
		float y2 = maskMin + (contentSize - contentMask.localScale.y) * relativeScrollerPosPct;
		contentParent.position = new Vector3(contentParent.position.x, y2, contentParent.position.z);
	}
}

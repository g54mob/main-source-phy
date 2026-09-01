using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkshopMessage : MonoBehaviour
{
	[SerializeField]
	protected float interval = 0.3f;

	[SerializeField]
	protected TextMesh messageTextMesh;

	[SerializeField]
	protected TextMesh ellipsisTextMesh;

	[SerializeField]
	protected GameObject steamIconObject;

	[SerializeField]
	protected GameObject weGameIconObject;

	[SerializeField]
	protected GameObject modIOIconObject;

	[SerializeField]
	protected Renderer backgroundMeshRenderer;

	private readonly string[] ellipsisArray = new string[4]
	{
		string.Empty,
		".",
		"..",
		"..."
	};

	private int ellipsisIndex;

	private float lastUpdate;

	private float messageTextSizeX;

	private Renderer messageRenderer;

	private IEnumerator destroySelfCoroutine;

	private SimpleUIButton backgroundButton;

	private float destroyAfterSeconds = 5f;

	public void Setup(WorkshopType workshopType, string message, bool showEllipsis, float destroyTime)
	{
		ToggleWorkshopIcon(workshopType);
		SetMessageText(message);
		ResizeBackground();
		destroyAfterSeconds = destroyTime;
		ResetSelfDestroy();
		SetupBackgroundButton();
		if (!showEllipsis)
		{
			ellipsisTextMesh.gameObject.SetActive(false);
		}
	}

	private void SetupBackgroundButton()
	{
		backgroundButton = backgroundMeshRenderer.gameObject.GetComponent<SimpleUIButton>();
		backgroundButton.Click += BackgroundButtonClick;
	}

	private void BackgroundButtonClick()
	{
		backgroundButton.Click -= BackgroundButtonClick;
		Object.Destroy(base.gameObject);
	}

	private void ResetSelfDestroy()
	{
		if (destroySelfCoroutine != null)
		{
			StopCoroutine(destroySelfCoroutine);
		}
		destroySelfCoroutine = DestroySelfIE();
		StartCoroutine(destroySelfCoroutine);
	}

	private IEnumerator DestroySelfIE()
	{
		yield return new WaitForSecondsRealtime(destroyAfterSeconds);
		Object.Destroy(base.gameObject);
	}

	private void ResizeBackground()
	{
		Bounds bounds = backgroundMeshRenderer.bounds;
		float num = bounds.center.x - bounds.min.x;
		float num2 = (GetContentBounds().size.x + 0.3f) / bounds.size.x;
		Vector3 localScale = backgroundMeshRenderer.transform.localScale;
		if (num2 > 1f)
		{
			localScale.x *= num2;
		}
		backgroundMeshRenderer.transform.localScale = localScale;
		float num3 = backgroundMeshRenderer.bounds.center.x - backgroundMeshRenderer.bounds.min.x;
		float num4 = (num3 - num) / base.transform.localScale.x;
		Vector3 localPosition = backgroundMeshRenderer.transform.localPosition;
		localPosition.x += num4;
		backgroundMeshRenderer.transform.localPosition = localPosition;
		base.transform.localPosition = new Vector3(base.transform.localPosition.x - num4, base.transform.localPosition.y);
	}

	private Bounds GetContentBounds()
	{
		Bounds result = new Bounds(backgroundMeshRenderer.bounds.center, Vector3.zero);
		List<Renderer> list = new List<Renderer>();
		list.Add(messageRenderer);
		if (weGameIconObject.activeSelf)
		{
			list.Add(weGameIconObject.GetComponent<Renderer>());
		}
		else
		{
			list.Add(steamIconObject.GetComponent<Renderer>());
		}
		list.Add(ellipsisTextMesh.GetComponent<Renderer>());
		foreach (Renderer item in list)
		{
			if (!item.bounds.size.Equals(Vector3.zero))
			{
				result.Encapsulate(item.bounds);
			}
		}
		return result;
	}

	private void SetMessageText(string message)
	{
		messageTextMesh.text = message;
	}

	private void ToggleWorkshopIcon(WorkshopType workshopType)
	{
		steamIconObject.SetActive(false);
		weGameIconObject.SetActive(false);
		modIOIconObject.SetActive(false);
		switch (workshopType)
		{
		case WorkshopType.Steam:
			steamIconObject.SetActive(true);
			break;
		case WorkshopType.WeGame:
			weGameIconObject.SetActive(true);
			break;
		case WorkshopType.ModIO:
			modIOIconObject.SetActive(true);
			break;
		}
	}

	private void Awake()
	{
		messageRenderer = messageTextMesh.GetComponent<Renderer>();
	}

	private void Update()
	{
		if (Time.time > lastUpdate + interval)
		{
			UpdateEllipsis();
		}
	}

	private void UpdateEllipsis()
	{
		if (messageRenderer.bounds.size.x != messageTextSizeX)
		{
			ellipsisTextMesh.transform.position = new Vector3(messageRenderer.bounds.max.x, ellipsisTextMesh.transform.position.y, ellipsisTextMesh.transform.position.z);
			messageTextSizeX = messageRenderer.bounds.size.x;
		}
		lastUpdate = Time.time;
		ellipsisTextMesh.text = ellipsisArray[ellipsisIndex++ % 4];
	}
}

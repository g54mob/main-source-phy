using System.Collections;
using Steamworks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(RawImage))]
public class SteamVideoItem : MonoBehaviour, IEventSystemHandler, IPointerEnterHandler, IPointerExitHandler
{
	public RectTransform detailPanel;

	public Color highlightColor;

	public float moveAmount = 10f;

	public float moveSpeed = 0.5f;

	public Text title;

	public Text author;

	public Text upvotes;

	private Color initialColor;

	private RawImage buttonBg;

	private float endY;

	private bool isOver;

	private string linkURL;

	public void Awake()
	{
		buttonBg = GetComponent<RawImage>();
		Button component = GetComponent<Button>();
		component.onClick.AddListener(OnButtonDown);
		initialColor = buttonBg.color;
		endY = detailPanel.anchoredPosition.y;
		detailPanel.gameObject.SetActive(false);
	}

	public void SetEntry(SteamVideoParser.VideoEntry entry)
	{
		upvotes.text = entry.Upvotes.ToString();
		title.text = ProcessText(entry.Title).ToUpper();
		author.text = entry.Owner;
		linkURL = entry.ContentLink;
		StartCoroutine(LoadImage(entry.ImageURL));
	}

	private string ProcessText(string txt)
	{
		return txt.Replace("&nbsp;", string.Empty);
	}

	private void OnButtonDown()
	{
		SteamFriends.ActivateGameOverlayToWebPage(linkURL);
	}

	private IEnumerator LoadImage(string url)
	{
		buttonBg.enabled = false;
		WWW www = new WWW(url);
		yield return www;
		buttonBg.enabled = true;
		if (string.IsNullOrEmpty(www.error))
		{
			buttonBg.texture = www.texture;
		}
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		detailPanel.anchoredPosition = new Vector2(detailPanel.anchoredPosition.x, endY - moveAmount);
		detailPanel.gameObject.SetActive(true);
		buttonBg.color = highlightColor;
		isOver = true;
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		buttonBg.color = initialColor;
		isOver = false;
		detailPanel.gameObject.SetActive(false);
	}

	public void Update()
	{
		if (isOver || detailPanel.gameObject.activeInHierarchy)
		{
			Vector2 anchoredPosition = detailPanel.anchoredPosition;
			if (isOver && anchoredPosition.y < endY)
			{
				detailPanel.anchoredPosition = new Vector2(anchoredPosition.x, anchoredPosition.y + Time.deltaTime * moveSpeed);
			}
			else if (isOver)
			{
			}
		}
	}
}

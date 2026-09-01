using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(TextMeshProUGUI))]
public class OpenHyperlinks : MonoBehaviour, IPointerClickHandler, IEventSystemHandler
{
	public bool doesColorChangeOnHover = true;

	public Color hoverColor = new Color(0.23529412f, 0.47058824f, 1f);

	private TextMeshProUGUI pTextMeshPro;

	private Canvas pCanvas;

	private Camera pCamera;

	private int pCurrentLink = -1;

	private List<Color32[]> pOriginalVertexColors = new List<Color32[]>();

	public bool isLinkHighlighted => pCurrentLink != -1;

	protected virtual void Awake()
	{
		pTextMeshPro = GetComponent<TextMeshProUGUI>();
		pCanvas = GetComponentInParent<Canvas>();
		if (pCanvas.renderMode == RenderMode.ScreenSpaceOverlay)
		{
			pCamera = null;
		}
		else
		{
			pCamera = pCanvas.worldCamera;
		}
	}

	private void LateUpdate()
	{
		bool flag = TMP_TextUtilities.IsIntersectingRectTransform(pTextMeshPro.rectTransform, GameInput.GetMousePosition(), pCamera);
		int num = (flag ? TMP_TextUtilities.FindIntersectingLink(pTextMeshPro, GameInput.GetMousePosition(), pCamera) : (-1));
		if (pCurrentLink != -1 && num != pCurrentLink)
		{
			SetLinkToColor(pCurrentLink, (int linkIdx, int vertIdx) => pOriginalVertexColors[linkIdx][vertIdx]);
			pOriginalVertexColors.Clear();
			pCurrentLink = -1;
		}
		if (num != -1 && num != pCurrentLink)
		{
			pCurrentLink = num;
			if (doesColorChangeOnHover)
			{
				pOriginalVertexColors = SetLinkToColor(num, (int _linkIdx, int _vertIdx) => hoverColor);
			}
		}
		if (flag && GameInput.GetMouseButtonJustPressed(0))
		{
			OnPointerClick(null);
		}
		else if (flag && num >= 0 && num < pTextMeshPro.textInfo.linkInfo.Length)
		{
			TMP_LinkInfo tMP_LinkInfo = pTextMeshPro.textInfo.linkInfo[num];
			GameUI.ToolTipEnable(tMP_LinkInfo.GetLinkID(), null);
		}
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		int num = TMP_TextUtilities.FindIntersectingLink(pTextMeshPro, GameInput.GetMousePosition(), pCamera);
		if (num != -1)
		{
			TMP_LinkInfo tMP_LinkInfo = pTextMeshPro.textInfo.linkInfo[num];
			Application.OpenURL(tMP_LinkInfo.GetLinkID());
		}
	}

	private List<Color32[]> SetLinkToColor(int linkIndex, Func<int, int, Color32> colorForLinkAndVert)
	{
		TMP_LinkInfo tMP_LinkInfo = pTextMeshPro.textInfo.linkInfo[linkIndex];
		List<Color32[]> list = new List<Color32[]>();
		for (int i = 0; i < tMP_LinkInfo.linkTextLength; i++)
		{
			int num = tMP_LinkInfo.linkTextfirstCharacterIndex + i;
			TMP_CharacterInfo tMP_CharacterInfo = pTextMeshPro.textInfo.characterInfo[num];
			int materialReferenceIndex = tMP_CharacterInfo.materialReferenceIndex;
			int vertexIndex = tMP_CharacterInfo.vertexIndex;
			Color32[] colors = pTextMeshPro.textInfo.meshInfo[materialReferenceIndex].colors32;
			list.Add(colors.ToArray());
			if (tMP_CharacterInfo.isVisible)
			{
				colors[vertexIndex] = colorForLinkAndVert(i, vertexIndex);
				colors[vertexIndex + 1] = colorForLinkAndVert(i, vertexIndex + 1);
				colors[vertexIndex + 2] = colorForLinkAndVert(i, vertexIndex + 2);
				colors[vertexIndex + 3] = colorForLinkAndVert(i, vertexIndex + 3);
			}
		}
		pTextMeshPro.UpdateVertexData(TMP_VertexDataUpdateFlags.All);
		return list;
	}
}

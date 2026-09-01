using System;
using Cpp2ILInjected;
using TMPro;
using UnityEngine;

public class GridSquareHighlighterWithSubsector : MonoBehaviour
{
	public RectTransform gridSquarePrefab;

	public Canvas worldSpaceCanvas;

	public RectTransform gridParent;

	public VirtualCursor virtualCursor;

	public TextMeshProUGUI subSectorTextPrefab;

	private RectTransform spawnedSquare;

	private TextMeshProUGUI spawnedSubSectorText;

	private const float gridSize = 1f;

	private const int subSectorCount = 10;

	private const float subSectorSize = 0.1f;

	private void Start()
	{
		object message;
		if (gridSquarePrefab != null && worldSpaceCanvas != null && gridParent != null && subSectorTextPrefab != null)
		{
			if (virtualCursor != null)
			{
				RectTransform rectTransform = UnityEngine.Object.Instantiate(gridSquarePrefab, gridParent);
				spawnedSquare = rectTransform;
				GameObject gameObject = spawnedSquare.gameObject;
				gameObject.SetActive(value: true);
				TextMeshProUGUI textMeshProUGUI = UnityEngine.Object.Instantiate(subSectorTextPrefab, gridParent);
				spawnedSubSectorText = textMeshProUGUI;
				GameObject gameObject2 = spawnedSubSectorText.gameObject;
				gameObject2.SetActive(value: true);
				return;
			}
			message = "GridSquareHighlighterWithSubsector: VirtualCursor is required. Assign it in the inspector.";
		}
		else
		{
			message = "GridSquareHighlighterWithSubsector: Please assign all required references.";
		}
		Debug.LogError(message);
		base.enabled = false;
	}

	private void Update()
	{
		//IL_02ec: Expected F8, but got O
		//IL_015a: Invalid comparison between F8 and I4
		//IL_0290: Invalid comparison between F8 and I4
		//IL_0177: Invalid comparison between F8 and I4
		//IL_019c: Invalid comparison between F8 and I4
		Camera worldCamera = worldSpaceCanvas.worldCamera;
		Vector2 vector = default(Vector2);
		GameObject gameObject2;
		bool active;
		if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(gridParent, vector, worldCamera, out var localPoint))
		{
			GameObject gameObject = spawnedSquare.gameObject;
			gameObject.SetActive(value: false);
			gameObject2 = spawnedSubSectorText.gameObject;
			active = false;
		}
		else
		{
			double num = Math.Floor((double)localPoint);
			double num3 = default(double);
			double num2 = Math.Floor(num3);
			double num4 = num2 + 0.5;
			spawnedSquare.anchoredPosition = vector;
			GameObject gameObject3 = spawnedSquare.gameObject;
			if (!gameObject3.activeSelf)
			{
				GameObject gameObject4 = spawnedSquare.gameObject;
				gameObject4.SetActive(value: true);
			}
			double num5 = (double)localPoint - num;
			double d = num5 / 0.10000000149011612;
			double num6 = Math.Floor(d);
			double num7 = num3 - num2;
			double d2 = num7 / 0.10000000149011612;
			double num8 = Math.Floor(d2);
			if (num6 < 0.0 || num6 > 9.0)
			{
			}
			if (num8 < 0.0 || num8 > 9.0)
			{
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsi2ss xmm6,ebp\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsi2ss xmm0,r14d\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsi2ss xmm7,esi\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsi2ss xmm0,r15d\"");
			RectTransform rectTransform = spawnedSubSectorText.rectTransform;
			rectTransform.anchoredPosition = vector;
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			object arg = default(object);
			object arg2 = default(object);
			string text = $"{arg}:{arg2}";
			spawnedSubSectorText.text = text;
			GameObject gameObject5 = spawnedSubSectorText.gameObject;
			if (gameObject5.activeSelf)
			{
				return;
			}
			gameObject2 = spawnedSubSectorText.gameObject;
			active = true;
		}
		gameObject2.SetActive(active);
	}
}

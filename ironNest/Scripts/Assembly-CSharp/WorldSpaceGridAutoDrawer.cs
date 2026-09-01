using TMPro;
using UnityEngine;

[ExecuteAlways]
public class WorldSpaceGridAutoDrawer : MonoBehaviour
{
	public RectTransform targetCanvas;

	public Color gridColor;

	public float lineWidth;

	public int gridSpacing;

	public bool generateLines;

	public bool generateLabels;

	public Color labelColor;

	public int labelFontSize;

	public Vector3 labelOffset;

	public Vector3 labelScale;

	public TextMeshProUGUI labelPrefab;

	public void GenerateGrid()
	{
	}

	public void ClearGrid()
	{
	}

	private void CreateLine(Vector3 start, Vector3 end)
	{
	}

	private void CreateLabel(string label, Vector3 position)
	{
	}

	private string GetGridLabel(int x, int y)
	{
		return null;
	}
}

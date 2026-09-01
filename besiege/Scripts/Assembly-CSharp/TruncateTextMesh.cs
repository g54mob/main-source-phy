using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(TextMesh))]
public class TruncateTextMesh : MonoBehaviour
{
	[SerializeField]
	private float maxRendererWidth = 1.5f;

	[SerializeField]
	private bool debug;

	private string originalText;

	private TextMesh textMesh;

	private MeshRenderer textMeshRender;

	private void Awake()
	{
		textMesh = GetComponent<TextMesh>();
		textMeshRender = GetComponent<MeshRenderer>();
	}

	private void Start()
	{
		Truncate();
	}

	private bool IsOverSized()
	{
		float num = Mathf.Abs(textMeshRender.bounds.size.x);
		if (debug)
		{
			Debug.LogFormat("TextMeshRender has a width of {0}", num);
		}
		return num > maxRendererWidth;
	}

	public void Truncate()
	{
		originalText = textMesh.text;
		string text = originalText;
		int num = 1;
		while (IsOverSized())
		{
			if (debug)
			{
				Debug.LogFormat("TextMesh is oversized by {0}, truncating text to {1} characters", Mathf.Abs(textMeshRender.bounds.size.x) - maxRendererWidth, text.Length - num);
			}
			text = text.Truncate(originalText.Length - num++);
			textMesh.text = text;
		}
	}
}

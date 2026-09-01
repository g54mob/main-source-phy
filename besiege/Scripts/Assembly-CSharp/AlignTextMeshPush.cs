using UnityEngine;

public class AlignTextMeshPush : AlignTextMesh
{
	public GameObject blocker;

	public float maxWidth = 10f;

	public Vector3 offsetPos = Vector3.zero;

	private Vector3 originalPos;

	private Vector3 otherOriginalPos;

	private MeshRenderer textRenderer;

	protected override void Awake()
	{
		originalPos = base.transform.position;
		otherOriginalPos = anchorRenderer.transform.position;
		textMesh = GetComponent<TextMesh>();
		textRenderer = textMesh.GetComponent<MeshRenderer>();
		base.Awake();
	}

	protected override void Start()
	{
		StartCoroutine(IEStart());
	}

	private void ResetPositions()
	{
		base.transform.position = originalPos;
		anchorRenderer.transform.position = otherOriginalPos;
	}

	protected override void UpdateAlignment()
	{
		ResetPositions();
		if (((!(textRenderer == null)) ? (anchorRenderer.bounds.size + textRenderer.bounds.size) : (anchorRenderer.bounds.size * 2f)).x > maxWidth && (blocker == null || blocker.activeSelf))
		{
			anchorRenderer.transform.localPosition += offsetPos;
			base.transform.position = new Vector3(anchorRenderer.transform.position.x, base.transform.position.y, base.transform.position.z);
		}
		else
		{
			base.UpdateAlignment();
		}
	}
}

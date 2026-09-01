using UnityEngine;

public class SpecialThanksController : MonoBehaviour
{
	public Color textColor;

	public Color shadowColor;

	private Color initialTextColor;

	private Color initialShadowColor;

	public TextMesh titleTextMesh;

	public TextMesh titleDropShadows;

	public TextMesh listTextMesh;

	public TextMesh listDropShadows;

	[Header("Scrolling")]
	public Transform textParent;

	public float targetHeight;

	public float scrollingTime = 30f;

	private float scrollingStep;

	private float scrollingTimer;

	private float initialHeight;

	public float fadeInTime = 2f;

	private float timer;

	private float lerpStep;

	private void Start()
	{
		initialShadowColor = titleDropShadows.color;
		initialTextColor = titleTextMesh.color;
		initialHeight = textParent.position.y;
	}

	private void FixedUpdate()
	{
		if (!(titleTextMesh.transform.position.y >= targetHeight))
		{
			scrollingTimer += Time.deltaTime;
			scrollingStep = scrollingTimer / scrollingTime;
			textParent.position = new Vector3(textParent.position.x, Mathf.Lerp(initialHeight, targetHeight, scrollingStep), textParent.position.x);
			if (!(titleTextMesh.color == textColor))
			{
				timer += Time.deltaTime;
				lerpStep = timer / fadeInTime;
				titleTextMesh.color = Color.Lerp(initialTextColor, textColor, lerpStep);
				listTextMesh.color = Color.Lerp(initialTextColor, textColor, lerpStep);
				titleDropShadows.color = Color.Lerp(initialShadowColor, shadowColor, lerpStep);
				listDropShadows.color = Color.Lerp(initialShadowColor, shadowColor, lerpStep);
			}
		}
	}
}

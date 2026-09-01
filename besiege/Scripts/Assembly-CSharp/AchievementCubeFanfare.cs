using System;
using System.Collections;
using System.Collections.Generic;
using GameGrind;
using Localisation;
using UnityEngine;

[AddComponentMenu("UI/Achievement Cube Fanfare")]
public class AchievementCubeFanfare : MonoBehaviour, ILevelCompletionAnim
{
	[Serializable]
	public class Shadow
	{
		public Transform parent;

		public GameObject prefab;

		public float size = 10f;

		public float scale = 1f;

		public Vector3 offset;
	}

	[Serializable]
	public class RewardSkins
	{
		public int achievementID;

		public Mesh blockMesh;

		public Material blockMaterial;
	}

	public Renderer block3D;

	public float scaleUpAmount = 1.5f;

	public float scaleDuration = 0.2f;

	public Vector3 blockStartOffset = new Vector3(0f, 2f, 0f);

	public float fadeDuration = 0.1f;

	public float fadeOutDuration = 1f;

	public ParticleSystem dustParticles;

	public float startWaitDuration = 1f;

	public DynamicText[] lefthandText;

	public DynamicText[] righthandText;

	public MeshRenderer[] glows;

	public AudioSource stampAudio;

	private Vector3 startScale;

	private Color startColour;

	private Color shadowColor = Color.black;

	private Vector3 glowStartSize = Vector3.one;

	private IEnumerator messageCoroutine;

	private IEnumerator lerpSizeCoroutine;

	private bool hasRun;

	private bool isRunning;

	public Shadow shadow;

	private MeshRenderer mesh;

	private MeshRenderer shadowMesh;

	private float posToAdd;

	private float letterSize;

	private Vector3 startpos;

	private float shadowScale;

	private List<MeshRenderer> shadows = new List<MeshRenderer>();

	public RewardSkins[] rewardSkins;

	private void Start()
	{
		AchievementEvents.OnAchievementGrant += AchievementGranted;
		Setup();
	}

	private void Setup()
	{
		startScale = block3D.transform.localScale;
		block3D.transform.localScale = startScale * scaleUpAmount;
		startColour = block3D.material.GetColor("_Color");
		block3D.enabled = false;
		for (int i = 0; i < lefthandText.Length; i++)
		{
			lefthandText[i].gameObject.SetActive(false);
		}
		for (int j = 0; j < righthandText.Length; j++)
		{
			righthandText[j].gameObject.SetActive(false);
		}
		glowStartSize = Vector3.one;
		if (glows.Length != 0)
		{
			glowStartSize = glows[0].transform.localScale;
		}
	}

	private void AchievementGranted(Achievement achievement)
	{
		for (int i = 0; i < rewardSkins.Length; i++)
		{
			if (achievement.id == rewardSkins[i].achievementID)
			{
				block3D.material = rewardSkins[i].blockMaterial;
				block3D.GetComponent<MeshFilter>().mesh = rewardSkins[i].blockMesh;
				startColour = block3D.material.GetColor("_Color");
				OnGoalAchieved();
			}
		}
	}

	public void OnGoalAchieved()
	{
		if (!isRunning)
		{
			messageCoroutine = DisplayMessage();
			StartCoroutine(messageCoroutine);
		}
	}

	public void LevelReset()
	{
		StopAllCoroutines();
		block3D.enabled = false;
		for (int i = 0; i < glows.Length; i++)
		{
			glows[i].gameObject.SetActive(false);
		}
		for (int j = 0; j < lefthandText.Length; j++)
		{
			lefthandText[j].gameObject.SetActive(false);
		}
		for (int k = 0; k < righthandText.Length; k++)
		{
			righthandText[k].gameObject.SetActive(false);
		}
		if (shadows.Count != 0)
		{
			for (int num = shadows.Count - 1; num >= 0; num--)
			{
				UnityEngine.Object.Destroy(shadows[num].gameObject);
			}
		}
		shadows.Clear();
		hasRun = false;
	}

	private IEnumerator DisplayMessage()
	{
		isRunning = true;
		yield return new WaitForSecondsRealtime(startWaitDuration);
		string left = LocalisationManager.GetTranslation(5000).ToUpper().Trim();
		string right = LocalisationManager.GetTranslation(5001).ToUpper().Trim();
		float diff = right.Length - left.Length;
		block3D.transform.localPosition += blockStartOffset;
		block3D.enabled = true;
		for (int i = 0; i < glows.Length; i++)
		{
			glows[i].gameObject.SetActive(true);
			glows[i].transform.localScale = glowStartSize;
		}
		for (int j = 0; j < lefthandText.Length; j++)
		{
			lefthandText[j].gameObject.SetActive(true);
			lefthandText[j].letterSpacing = Mathf.Clamp(diff * 0.1f, 0.05f, 0.3f);
			if (j == 0)
			{
				ReferenceMaster.SetDynamicText(lefthandText[j], left);
				float width = lefthandText[j].GetComponent<MeshRenderer>().bounds.size.x;
				lefthandText[j].transform.localPosition = width * Vector3.left;
			}
			ReferenceMaster.SetDynamicText(lefthandText[j], string.Empty);
		}
		for (int k = 0; k < righthandText.Length; k++)
		{
			righthandText[k].gameObject.SetActive(true);
			righthandText[k].letterSpacing = Mathf.Clamp(-1f * diff * 0.1f, 0.05f, 0.3f);
			ReferenceMaster.SetDynamicText(righthandText[k], string.Empty);
		}
		stampAudio.Play();
		lerpSizeCoroutine = LerpSizeAndPosition();
		yield return StartCoroutine(lerpSizeCoroutine);
		for (int l = 0; l < lefthandText.Length; l++)
		{
			InitText(lefthandText[l], left, l == 0);
		}
		for (int m = 0; m < righthandText.Length; m++)
		{
			InitText(righthandText[m], right, m == 0);
		}
		StartCoroutine(LerpShadows(true));
		StartCoroutine(LerpTypeAlpha(lefthandText, true));
		StartCoroutine(LerpTypeAlpha(righthandText, true));
		hasRun = true;
		yield return new WaitForSecondsRealtime(fadeOutDuration + 0.25f);
		StartCoroutine(LerpTypeAlpha(lefthandText, false));
		StartCoroutine(LerpTypeAlpha(righthandText, false));
		StartCoroutine(LerpShadows(false));
		StartCoroutine(LerpGlowAndBlock());
		yield return new WaitForSecondsRealtime(fadeOutDuration);
		isRunning = false;
	}

	protected void InitText(DynamicText t, string s, bool first)
	{
		t.color.a = 0f;
		if (first)
		{
			ReferenceMaster.SetDynamicText(t, s);
			mesh = t.GetComponent<MeshRenderer>();
			shadowMesh = shadow.prefab.GetComponent<MeshRenderer>();
			Bounds bounds = mesh.bounds;
			posToAdd = bounds.size.x / (float)s.Length;
			int num = ((s.Length >= 2) ? s.Length : 2);
			letterSize = posToAdd * (1f - t.letterSpacing / (float)(num - 1) * (float)num);
			startpos = new Vector3(bounds.min.x + letterSize / 2f, bounds.center.y, bounds.center.z) + shadow.offset;
			shadowScale = letterSize / shadowMesh.bounds.size.x * shadow.size;
			ReferenceMaster.SetDynamicText(t, string.Empty);
		}
		for (int i = 1; i <= s.Length; i++)
		{
			if (first && !hasRun)
			{
				CreateDropShadow(startpos, shadowScale);
				startpos.x += posToAdd;
			}
			ReferenceMaster.SetDynamicText(t, s.Substring(0, i));
		}
	}

	private void CreateDropShadow(Vector3 pos, float size)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate(shadow.prefab, pos, Quaternion.Euler(90f, 0f, 0f)) as GameObject;
		gameObject.transform.localScale *= size;
		gameObject.transform.SetParent(shadow.parent, true);
		MeshRenderer component = gameObject.GetComponent<MeshRenderer>();
		if (shadowColor == Color.black)
		{
			shadowColor = component.material.color;
		}
		shadows.Add(component);
	}

	private IEnumerator LerpSizeAndPosition()
	{
		float cTime = 0f;
		float rate = 1f / scaleDuration;
		Vector3 sizeToBe = startScale * scaleUpAmount;
		Vector3 startPosition = block3D.transform.localPosition;
		Vector3 endPosition = block3D.transform.localPosition - blockStartOffset;
		while (cTime < 1f)
		{
			cTime += Time.unscaledDeltaTime * rate;
			block3D.transform.localPosition = Vector3.Lerp(startPosition, endPosition, cTime);
			block3D.transform.localScale = Vector3.Lerp(sizeToBe, startScale, cTime);
			yield return null;
		}
		dustParticles.Play();
	}

	private IEnumerator LerpTypeAlpha(DynamicText[] text, bool flipped)
	{
		float cTime = 0f;
		float rate = 2f / fadeOutDuration;
		while (cTime < 1f)
		{
			cTime += Time.unscaledDeltaTime * rate;
			for (int i = 0; i < text.Length; i++)
			{
				text[i].color.a = Mathf.Lerp(1f, 0f, (!flipped) ? cTime : (1f - cTime));
			}
			yield return null;
		}
	}

	private IEnumerator LerpShadows(bool flipped)
	{
		float cTime = 0f;
		float rate = 1f / fadeOutDuration;
		float a = shadowColor.a;
		while (cTime < 1f)
		{
			cTime += Time.unscaledDeltaTime * rate;
			float pct = Mathf.Sqrt(cTime);
			pct = ((!flipped) ? pct : (1f - pct));
			for (int i = 0; i < shadows.Count; i++)
			{
				shadows[i].material.color = new Color(shadowColor.r, shadowColor.g, shadowColor.b, Mathf.Lerp(a, 0f, pct));
			}
			yield return null;
		}
	}

	private IEnumerator LerpGlowAndBlock()
	{
		float cTime = 0f;
		float rate = 1f / fadeOutDuration;
		while (cTime < 1f)
		{
			cTime += Time.unscaledDeltaTime * rate;
			float pct = cTime * cTime;
			block3D.transform.localScale = Vector3.Lerp(startScale, Vector3.zero, pct);
			for (int i = 0; i < glows.Length; i++)
			{
				glows[i].transform.localScale = Vector3.Lerp(glowStartSize, Vector3.zero, pct);
			}
			yield return null;
		}
	}

	public void LevelCompleted()
	{
		LevelReset();
	}
}

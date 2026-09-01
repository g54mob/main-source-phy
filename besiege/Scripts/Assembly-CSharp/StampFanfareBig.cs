using System;
using System.Collections;
using System.Collections.Generic;
using Localisation;
using UnityEngine;

[AddComponentMenu("UI/Stamp Fanfare (Big)")]
public class StampFanfareBig : MonoBehaviour, ILevelCompletionAnim
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

	public Renderer[] stamp;

	public Renderer[] extra;

	private float[] extraAlpha;

	public float scaleUpAmount = 1.5f;

	public float scaleDuration = 0.2f;

	public float fadeDuration = 0.1f;

	public ParticleSystem dustParticles;

	public float startWaitDuration = 1f;

	public DynamicText[] islandText;

	public DynamicText[] destroyedText;

	public ParticleSystem[] embers;

	public AudioSource stampAudio;

	public RandomSoundController randomSoundCode;

	private Vector3 startScale;

	private Color[] startColour;

	private List<Coroutine> typing = new List<Coroutine>();

	private IEnumerator stampCoroutine;

	private IEnumerator lerpAlphaCoroutine;

	private IEnumerator lerpSizeCoroutine;

	public Shadow shadow;

	private MeshRenderer mesh;

	private MeshRenderer shadowMesh;

	private float posToAdd;

	private float letterSize;

	private Vector3 startpos;

	private float shadowScale;

	private void Start()
	{
		startScale = stamp[0].transform.localScale;
		stamp[0].transform.localScale = startScale * scaleUpAmount;
		startColour = new Color[stamp.Length];
		for (int i = 0; i < stamp.Length; i++)
		{
			startColour[i] = stamp[i].material.GetColor("_TintColor");
			stamp[i].enabled = false;
		}
		for (int j = 0; j < islandText.Length; j++)
		{
			islandText[j].gameObject.SetActive(false);
		}
		for (int k = 0; k < destroyedText.Length; k++)
		{
			destroyedText[k].gameObject.SetActive(false);
		}
	}

	public void LevelCompleted()
	{
		stampCoroutine = Stamp();
		StartCoroutine(stampCoroutine);
	}

	public void LevelReset()
	{
		StopAllCoroutines();
		stamp[0].transform.localScale = startScale * scaleUpAmount;
		for (int i = 0; i < stamp.Length; i++)
		{
			stamp[i].enabled = false;
		}
		for (int j = 0; j < islandText.Length; j++)
		{
			islandText[j].gameObject.SetActive(false);
		}
		for (int k = 0; k < destroyedText.Length; k++)
		{
			destroyedText[k].gameObject.SetActive(false);
		}
		for (int num = shadow.parent.childCount - 1; num >= 0; num--)
		{
			UnityEngine.Object.Destroy(shadow.parent.GetChild(num).gameObject);
		}
		for (int l = 0; l < embers.Length; l++)
		{
			embers[l].Stop();
		}
	}

	private IEnumerator Stamp()
	{
		extraAlpha = new float[extra.Length];
		for (int i = 0; i < extra.Length; i++)
		{
			Color c = extra[i].material.GetColor("_TintColor");
			extraAlpha[i] = c.a;
			c.a = 0f;
			extra[i].material.SetColor("_TintColor", c);
		}
		yield return new WaitForSecondsRealtime(startWaitDuration);
		string left = LocalisationManager.GetTranslation(3202).ToUpper().Trim();
		string right = LocalisationManager.GetTranslation(3261).ToUpper().Trim();
		float diff = right.Length - left.Length;
		for (int j = 0; j < stamp.Length; j++)
		{
			stamp[j].enabled = true;
		}
		for (int k = 0; k < islandText.Length; k++)
		{
			islandText[k].gameObject.SetActive(true);
			islandText[k].letterSpacing = Mathf.Clamp(diff * 0.1f, 0.05f, 0.3f);
			if (k == 0)
			{
				ReferenceMaster.SetDynamicText(islandText[k], left);
				float width = islandText[k].GetComponent<MeshRenderer>().bounds.size.x;
				islandText[k].transform.localPosition = width * Vector3.left;
			}
			ReferenceMaster.SetDynamicText(islandText[k], string.Empty);
		}
		for (int l = 0; l < destroyedText.Length; l++)
		{
			destroyedText[l].gameObject.SetActive(true);
			destroyedText[l].letterSpacing = Mathf.Clamp(-1f * diff * 0.1f, 0.05f, 0.3f);
			ReferenceMaster.SetDynamicText(destroyedText[l], string.Empty);
		}
		lerpAlphaCoroutine = LerpAlpha();
		yield return StartCoroutine(lerpAlphaCoroutine);
		stampAudio.Play();
		lerpSizeCoroutine = LerpSize();
		yield return StartCoroutine(lerpSizeCoroutine);
		typing.Clear();
		for (int m = 0; m < islandText.Length; m++)
		{
			typing.Add(StartCoroutine(Type(islandText[m], left, m == 0)));
		}
		for (int n = 0; n < typing.Count; n++)
		{
			yield return typing[n];
		}
		typing.Clear();
		for (int num = 0; num < destroyedText.Length; num++)
		{
			typing.Add(StartCoroutine(Type(destroyedText[num], right, num == 0)));
		}
		for (int num2 = 0; num2 < typing.Count; num2++)
		{
			yield return typing[num2];
		}
		for (int num3 = 0; num3 < embers.Length; num3++)
		{
			embers[num3].Play();
			embers[num3].GetComponent<AudioSource>().Play();
		}
	}

	protected IEnumerator Type(DynamicText t, string s, bool first)
	{
		if (first)
		{
			ReferenceMaster.SetDynamicText(t, s);
			mesh = t.GetComponent<MeshRenderer>();
			shadowMesh = shadow.prefab.GetComponent<MeshRenderer>();
			Bounds meshBounds = mesh.bounds;
			posToAdd = meshBounds.size.x / (float)s.Length;
			int length = ((s.Length >= 2) ? s.Length : 2);
			letterSize = posToAdd * (1f - t.letterSpacing / (float)(length - 1) * (float)length);
			startpos = new Vector3(meshBounds.min.x + letterSize / 2f, meshBounds.center.y, meshBounds.center.z) + shadow.offset;
			shadowScale = letterSize / shadowMesh.bounds.size.x * shadow.size;
			ReferenceMaster.SetDynamicText(t, string.Empty);
		}
		for (int i = 1; i <= s.Length; i++)
		{
			if (first)
			{
				CreateDropShadow(startpos, shadowScale);
				startpos.x += posToAdd;
				randomSoundCode.Play();
			}
			ReferenceMaster.SetDynamicText(t, s.Substring(0, i));
			yield return new WaitForSecondsRealtime(0.1f);
		}
	}

	private void CreateDropShadow(Vector3 pos, float size)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate(shadow.prefab, pos, Quaternion.Euler(90f, 0f, 0f)) as GameObject;
		gameObject.transform.localScale *= size;
		gameObject.transform.SetParent(shadow.parent, true);
	}

	private IEnumerator LerpSize()
	{
		float cTime = 0f;
		float rate = 1f / scaleDuration;
		Vector3 sizeToBe = startScale * scaleUpAmount;
		while (cTime < 1f)
		{
			cTime += TimeSlider.Instance.deltaTime * rate;
			stamp[0].transform.localScale = Vector3.Lerp(sizeToBe, startScale, cTime);
			yield return null;
		}
		dustParticles.Play();
	}

	private IEnumerator LerpAlpha()
	{
		float cTime = 0f;
		float rate = 1f / fadeDuration;
		while (cTime < 1f)
		{
			cTime += TimeSlider.Instance.deltaTime * rate;
			for (int i = 0; i < stamp.Length; i++)
			{
				stamp[i].material.SetColor("_TintColor", new Color(startColour[i].r, startColour[i].g, startColour[i].b, Mathf.Lerp(0f, startColour[i].a, cTime)));
			}
			for (int j = 0; j < extra.Length; j++)
			{
				Color c = extra[j].material.GetColor("_TintColor");
				c.a = extraAlpha[j] * cTime;
				extra[j].material.SetColor("_TintColor", c);
			}
			yield return null;
		}
	}
}

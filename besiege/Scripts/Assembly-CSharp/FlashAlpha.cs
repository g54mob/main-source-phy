using System.Collections;
using UnityEngine;

public class FlashAlpha : SimBehaviour
{
	public Renderer rendy;

	[SerializeField]
	[HideInInspector]
	protected Color _startCol = Color.black;

	public float lerpSpeed = 1f;

	public float maxAlpha = 1f;

	public bool flashOnCollide;

	private bool flashing;

	private bool fading;

	private bool initialized;

	public Color startCol
	{
		get
		{
			return _startCol;
		}
		private set
		{
			if (!base.isSimulating)
			{
				_startCol = value;
			}
		}
	}

	public bool Transitioning
	{
		get
		{
			return flashing || fading;
		}
	}

	protected override void Awake()
	{
		Init();
	}

	private void Init()
	{
		if (initialized)
		{
			return;
		}
		if (_startCol.r == 0f && _startCol.g == 0f && _startCol.b == 0f)
		{
			Color color = rendy.material.GetColor("_TintColor");
			if (color.r != 0f || color.g != 0f || color.b != 0f)
			{
				_startCol = color;
			}
		}
		else if (!base.isSimulating)
		{
			_startCol = rendy.material.GetColor("_TintColor");
		}
		if (_startCol.a == 0f)
		{
			_startCol = new Color(_startCol.r, _startCol.g, _startCol.b, 0.11f);
		}
		initialized = true;
	}

	public void SetColor(Color color)
	{
		Init();
		rendy.material.SetColor("_TintColor", color);
		startCol = new Color(color.r, color.g, color.b, startCol.a);
	}

	private void OnCollisionEnter()
	{
		if (flashOnCollide && !flashing)
		{
			Flash();
		}
	}

	public void OnDisable()
	{
		StopAllCoroutines();
		fading = false;
		flashing = false;
		if (rendy.material.HasProperty("_TintColor"))
		{
			Color color = rendy.material.GetColor("_TintColor");
			rendy.material.SetColor("_TintColor", new Color(color.r, color.g, color.b, 0f));
		}
		else
		{
			Debug.LogWarning(rendy.transform.name + "'s material does not have a property called _TintColor");
		}
	}

	private void OnEnable()
	{
		if (base.gameObject.activeInHierarchy)
		{
			Init();
			if (!fading && !flashing)
			{
				StopAllCoroutines();
				StartCoroutine(FadeIn());
			}
		}
	}

	public IEnumerator FadeIn()
	{
		Init();
		float cTime = 0f;
		fading = true;
		Color currentColor = rendy.material.GetColor("_TintColor");
		while (cTime < 1f)
		{
			if (flashing)
			{
				fading = false;
				yield break;
			}
			rendy.material.SetColor("_TintColor", new Color(startCol.r, startCol.g, startCol.b, Mathf.Lerp(currentColor.a, startCol.a, cTime)));
			cTime += Time.deltaTime * 4f;
			yield return null;
		}
		rendy.material.SetColor("_TintColor", startCol);
		fading = false;
	}

	public IEnumerator FadeOut()
	{
		Init();
		float cTime = 0f;
		fading = true;
		Color currentColor = rendy.material.GetColor("_TintColor");
		if (currentColor.a != 0f)
		{
			while (cTime < 1f)
			{
				if (flashing)
				{
					fading = false;
					yield break;
				}
				rendy.material.SetColor("_TintColor", new Color(startCol.r, startCol.g, startCol.b, Mathf.Lerp(currentColor.a, 0f, cTime)));
				cTime += TimeSlider.Instance.DeltaTime();
				yield return null;
			}
			rendy.material.SetColor("_TintColor", new Color(startCol.r, startCol.g, startCol.b, 0f));
		}
		fading = false;
		yield return null;
	}

	private void FinishLine()
	{
		if (base.gameObject.activeInHierarchy)
		{
			Init();
			Flash();
		}
	}

	public void Flash(bool b = false)
	{
		if (base.gameObject.activeInHierarchy)
		{
			Init();
			StopAllCoroutines();
			flashing = true;
			fading = false;
			StartCoroutine(IEFlash(b));
		}
	}

	private IEnumerator IEFlash(bool b)
	{
		float startAlpha = startCol.a;
		float cTime = 0f;
		float rate = 1f / lerpSpeed;
		float end = ((!b) ? 0f : startAlpha);
		flashing = true;
		while (cTime < 1f)
		{
			cTime += TimeSlider.Instance.DeltaTime() * rate;
			rendy.material.SetColor("_TintColor", new Color(startCol.r, startCol.g, startCol.b, Mathf.Lerp(maxAlpha, end, cTime)));
			yield return null;
		}
		flashing = false;
	}
}

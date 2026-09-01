using UnityEngine;

public class ScaleJiggleAnimation : MonoBehaviour
{
	public AnimationCurve curve;

	public bool isPlaying;

	private float currentTime;

	public float speed = 1f;

	private ScaleJiggle jiggle;

	private void Awake()
	{
		jiggle = GetComponent<ScaleJiggle>();
	}

	private void Update()
	{
		if (isPlaying && curve != null && curve.keys != null)
		{
			currentTime += Time.unscaledDeltaTime * speed;
			if (currentTime > curve.keys[curve.keys.Length - 1].time)
			{
				currentTime = 0f;
			}
			jiggle.extraScale = curve.Evaluate(currentTime);
		}
		else
		{
			jiggle.extraScale = Mathf.Lerp(jiggle.extraScale, 0f, Time.unscaledDeltaTime * 5f);
		}
	}

	public void Play()
	{
		currentTime = 0f;
		isPlaying = true;
	}

	public void Stop()
	{
		isPlaying = false;
	}
}

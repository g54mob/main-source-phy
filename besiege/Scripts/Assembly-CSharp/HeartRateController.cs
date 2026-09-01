using System.Collections;
using UnityEngine;

public class HeartRateController : MonoBehaviour
{
	public float bpm = 100f;

	public Transform heartObj;

	public float lerpSmooth = 10f;

	public float contractDuration = 0.08f;

	public float expandDuration = 0.12f;

	public Transform particleObj;

	public float waveAmplitude = 0.1f;

	public float distanceScaler;

	public TextMesh bpmText;

	private Vector3 startScale;

	private Vector3 scaleToBe;

	private float timey;

	private float waveStartPosY;

	private void Start()
	{
		startScale = heartObj.localScale;
		waveStartPosY = particleObj.localPosition.y;
	}

	private void Update()
	{
		timey += Time.deltaTime;
		if (timey > 60f / bpm)
		{
			timey = 0f;
			StartCoroutine(BeatHeart());
		}
		bpm = 300f - Mathf.Clamp(distanceScaler * POVCam.distanceToMachine, 0f, 240f);
		bpm = Mathf.Clamp(bpm, 0f, 1000f);
		if (STATLORD.activeHumanPOV != null && STATLORD.activeHumanPOV.isDead)
		{
			bpm = 0f;
		}
		heartObj.localScale = Vector3.Lerp(heartObj.localScale, scaleToBe, Time.deltaTime * lerpSmooth);
		particleObj.localPosition = new Vector3(particleObj.localPosition.x, waveStartPosY + (heartObj.localScale.x - startScale.x) * waveAmplitude, particleObj.localPosition.z);
		bpmText.text = bpm.ToString("f0");
	}

	private IEnumerator BeatHeart()
	{
		scaleToBe = startScale / 2f;
		yield return new WaitForSeconds(contractDuration);
		scaleToBe = startScale * 1.5f;
		yield return new WaitForSeconds(expandDuration);
		scaleToBe = startScale;
	}
}

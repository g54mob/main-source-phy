using System.Collections;
using UnityEngine;

public class GroundCorruption : MonoBehaviour
{
	[SerializeField]
	private AudioSource sfx;

	[SerializeField]
	[Header("Rune")]
	private MeshRenderer runeRenderer;

	[SerializeField]
	private ParticleSystemRenderer particlesRenderer;

	[SerializeField]
	[Header("Ground")]
	private MeshRenderer objectRenderer;

	[SerializeField]
	[Header("Artefact")]
	private MeshRenderer[] artefactRenderer;

	[SerializeField]
	private GameObject corruptionParticles;

	[SerializeField]
	[Header("Blast")]
	private ParticleSystem Blast;

	[SerializeField]
	private CameraShaker shake;

	[SerializeField]
	private CorruptionWave pushWave;

	[SerializeField]
	private CorruptionWave corruptWave;

	[Header("Timer and transition")]
	[SerializeField]
	private float transitionStartWait;

	[SerializeField]
	private float transitionTimeGround = 3f;

	[SerializeField]
	private float transitionTimeRune = 3f;

	[SerializeField]
	private float corruptionWaveWait = 0.4f;

	private bool first = true;

	private void Start()
	{
	}

	private void Update()
	{
		if (WinCondition.hasWon && first)
		{
			StartCoroutine(Animate());
			first = false;
		}
	}

	protected IEnumerator Animate()
	{
		sfx.Play();
		StartCoroutine(AnimateInsignia());
		yield return new WaitForSeconds(transitionStartWait);
		pushWave.Animate();
		shake.shouldShake = true;
		corruptionParticles.SetActive(true);
		StartCoroutine(AnimateCorruption());
		yield return new WaitForSeconds(corruptionWaveWait);
		corruptWave.Animate();
	}

	protected IEnumerator AnimateInsignia()
	{
		for (float t = 0f; t < transitionTimeRune; t += Time.deltaTime)
		{
			Color newColor = Color.Lerp(t: t / transitionTimeRune, a: runeRenderer.material.GetColor("_TintColor"), b: Color.black);
			runeRenderer.material.SetColor("_TintColor", newColor);
			particlesRenderer.material.SetColor("_TintColor", newColor);
			yield return null;
		}
	}

	protected IEnumerator AnimateCorruption()
	{
		for (float t = 0f; t < transitionTimeGround; t += Time.deltaTime)
		{
			float pct = t / transitionTimeGround;
			objectRenderer.material.SetFloat("_Progress", Mathf.Lerp(0f, 1f, pct));
			MeshRenderer[] array = artefactRenderer;
			foreach (MeshRenderer rendy in array)
			{
				rendy.material.SetFloat("_Blend", Mathf.Lerp(0f, 1f, pct));
			}
			yield return null;
		}
	}
}

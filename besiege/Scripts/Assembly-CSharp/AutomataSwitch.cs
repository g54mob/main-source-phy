using UnityEngine;

public class AutomataSwitch : MonoBehaviour
{
	public int triggerCount;

	public bool buttonPressed;

	public ParticleSystem particles;

	public ParticleSystem particleGlow;

	public FlashAlpha flashObj;

	public AudioSource audioComp;

	public Color glowColor;

	public float glowLerpSpeed = 12f;

	public Renderer glowRender;

	public Renderer glowRender2;

	private Color startCol;

	private Color targetCol;

	private Color currentCol;

	private Color glowRender2startCol;

	private void Start()
	{
		startCol = glowRender.material.GetColor("_TintColor");
		currentCol = startCol;
		targetCol = startCol;
		glowRender2startCol = glowRender2.material.GetColor("_TintColor");
	}

	private void OnTriggerEnter(Collider other)
	{
		if (StatMaster.levelSimulating)
		{
			Rigidbody attachedRigidbody = other.attachedRigidbody;
			if (attachedRigidbody != null && attachedRigidbody.mass > 0.2f)
			{
				triggerCount++;
			}
			CheckIfButtonDown();
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (StatMaster.levelSimulating)
		{
			Rigidbody attachedRigidbody = other.attachedRigidbody;
			if (attachedRigidbody != null && attachedRigidbody.mass > 0.2f)
			{
				triggerCount--;
			}
			CheckIfButtonDown();
		}
	}

	private void CheckIfButtonDown()
	{
		if (triggerCount > 0)
		{
			PlayEffects();
			buttonPressed = true;
		}
		else
		{
			StopEffects();
			buttonPressed = false;
		}
	}

	private void PlayEffects()
	{
		if (!buttonPressed)
		{
			particles.Play();
			audioComp.Play();
			targetCol = glowColor;
		}
	}

	private void StopEffects()
	{
		if (buttonPressed)
		{
			particles.Stop();
			targetCol = startCol;
		}
	}

	private void Update()
	{
		currentCol = Color.Lerp(currentCol, targetCol, Time.deltaTime * glowLerpSpeed);
		glowRender.material.SetColor("_TintColor", currentCol);
		glowRender2startCol.a = currentCol.a * 2f;
		glowRender2.material.SetColor("_TintColor", glowRender2startCol);
	}
}

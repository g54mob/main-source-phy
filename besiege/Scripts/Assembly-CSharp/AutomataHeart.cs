using UnityEngine;

public class AutomataHeart : MonoBehaviour
{
	public Transform ribParent;

	public Vector3[] ribStartPositions;

	public Vector3 ribPosToBe;

	public float speed = 16f;

	public float ribcageOpenAmount = 3f;

	public AutomataSwitch buttons1;

	public AutomataSwitch buttons2;

	public AudioSource slidingStoneSFX;

	public float maxVolume = 0.15f;

	public bool ribCageOpen;

	private float volumeToBe = 0.15f;

	private float[] randomTimes;

	private float timer;

	private void Start()
	{
		ribStartPositions = new Vector3[ribParent.childCount];
		randomTimes = new float[ribParent.childCount];
		for (int i = 0; i < ribParent.childCount; i++)
		{
			ribStartPositions[i] = ribParent.GetChild(i).position;
			randomTimes[i] = Random.Range(0.5f, 2f);
		}
		slidingStoneSFX.Play();
		slidingStoneSFX.volume = 0f;
	}

	private void Update()
	{
		AnimateRibs();
		LerpAudio();
	}

	private void AnimateRibs()
	{
		for (int i = 0; i < ribParent.childCount; i++)
		{
			if (buttons1.buttonPressed && buttons2.buttonPressed)
			{
				ribPosToBe = ribStartPositions[i] + ribParent.GetChild(i).right * ribcageOpenAmount;
				ribCageOpen = true;
			}
			else
			{
				ribPosToBe = ribStartPositions[i];
				ribCageOpen = false;
			}
			ribParent.GetChild(i).position = Vector3.Lerp(ribParent.GetChild(i).position, ribPosToBe, Time.deltaTime * speed * randomTimes[i]);
		}
	}

	private void LerpAudio()
	{
		if (buttons1.buttonPressed && buttons2.buttonPressed)
		{
			timer += Time.deltaTime;
		}
		else
		{
			timer -= Time.deltaTime;
		}
		timer = Mathf.Clamp(timer, 0f, 1f);
		if (timer > 0f && timer < 1f)
		{
			volumeToBe = maxVolume;
		}
		else
		{
			volumeToBe = 0f;
		}
		slidingStoneSFX.volume = Mathf.Lerp(slidingStoneSFX.volume, volumeToBe, Time.deltaTime * speed);
	}
}

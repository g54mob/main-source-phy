using UnityEngine;

[AddComponentMenu("Levels/GateProgress")]
public class GateProgress : MonoBehaviour
{
	[SerializeField]
	protected int objectiveObjCount = 25;

	[SerializeField]
	protected float winDelay = 2f;

	[SerializeField]
	protected Rigidbody gate;

	[SerializeField]
	protected Rigidbody cog;

	private float amountToObjective;

	private float previousGatePositionY;

	[SerializeField]
	[Header("Audio Settings")]
	protected float tickingLerp = 15f;

	[SerializeField]
	protected AudioSource[] audios;

	[SerializeField]
	protected AudioSource complete;

	private float targetVolume;

	private float[] startVolume;

	private void Awake()
	{
		Object.FindObjectOfType<WinCondition>().objectiveObjectCount = objectiveObjCount;
		amountToObjective = gate.GetComponent<ConfigurableJoint>().linearLimit.limit / (float)objectiveObjCount;
		amountToObjective -= 1E-05f;
		previousGatePositionY = gate.transform.position.y;
	}

	private void Start()
	{
		if (StatMaster.levelSimulating)
		{
			startVolume = new float[audios.Length];
			for (int i = 0; i < audios.Length; i++)
			{
				audios[i].Play();
				startVolume[i] = audios[i].volume;
				audios[i].volume = 0f;
			}
		}
	}

	private void OnDestroy()
	{
	}

	private void LateUpdate()
	{
		if (!StatMaster.levelSimulating)
		{
			return;
		}
		if (gate.isKinematic)
		{
			SetAudioVolume(0f);
			return;
		}
		SetAudioVolume(cog.angularVelocity.sqrMagnitude + cog.angularVelocity.sqrMagnitude);
		float num = gate.transform.position.y - previousGatePositionY;
		if (Mathf.Abs(num) > amountToObjective)
		{
			bool flag = num > 0f;
			WinCondition.currentObjsCompleted += (flag ? 1 : (-1));
			if (WinCondition.currentObjsCompleted < 0)
			{
				WinCondition.currentObjsCompleted = 0;
			}
			previousGatePositionY += ((!flag) ? (0f - amountToObjective) : amountToObjective);
		}
		if (WinCondition.Instance.ObjectiveMet)
		{
			gate.isKinematic = true;
			complete.Play();
		}
	}

	private void SetAudioVolume(float ang)
	{
		if (ang > 0.1f)
		{
			targetVolume = 1f;
		}
		else
		{
			targetVolume = 0f;
		}
		for (int i = 0; i < audios.Length; i++)
		{
			audios[i].volume = Mathf.Lerp(audios[i].volume, startVolume[i] * targetVolume, Time.deltaTime * tickingLerp);
		}
	}
}

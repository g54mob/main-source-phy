using System.Collections;
using UnityEngine;

[AddComponentMenu("Levels/Valve Wheel")]
public class ValveWheel : MonoBehaviour
{
	public int revolutionsToWin = 3;

	public float angleToWin = 1080f;

	public float degreesPerObjectivePoint = 10f;

	public float lerpDragDuration;

	public float endDrag = 200f;

	public Rigidbody rigid;

	public AudioSource clickAudio;

	public AudioSource squeakAudio;

	public AudioSource clunkAudio;

	public float squeakVol = 3f;

	public int objectiveObjCount = 60;

	private bool hasWon;

	private float prevFurthestRotation;

	private float angle;

	private float revolutions;

	private Vector3 lastForward;

	private float angleDelta;

	private void Awake()
	{
		WinCondition winCondition = Object.FindObjectOfType<WinCondition>();
		if (winCondition != null)
		{
			winCondition.objectiveObjectCount = objectiveObjCount;
		}
		else
		{
			Debug.LogError("Couldn't find WinCondition!", base.gameObject);
		}
	}

	private void Start()
	{
		lastForward = base.transform.forward;
		if (StatMaster.levelSimulating)
		{
			squeakAudio.Play();
		}
	}

	private void Update()
	{
		if (StatMaster.levelSimulating)
		{
			angleDelta = GetAngle(base.transform.forward, lastForward, base.transform.up);
			angle += angleDelta;
			squeakAudio.volume = Mathf.Lerp(squeakAudio.volume, Mathf.Abs(angleDelta) * squeakVol, Time.deltaTime * 10f);
			lastForward = base.transform.forward;
			revolutions = angle * -1f / 360f;
			CheckAngle();
		}
	}

	private float GetAngle(Vector3 v1, Vector3 v2, Vector3 n)
	{
		return Mathf.Atan2(Vector3.Dot(n, Vector3.Cross(v1, v2)), Vector3.Dot(v1, v2)) * 57.29578f;
	}

	private void CheckRevolutions()
	{
		if (revolutions > (float)revolutionsToWin && !hasWon)
		{
			WinCondition.currentObjsCompleted++;
			hasWon = true;
			StartCoroutine(LerpAngularDrag());
		}
	}

	private void CheckAngle()
	{
		if (!hasWon)
		{
			if (prevFurthestRotation < angle * -1f - 18f)
			{
				prevFurthestRotation = angle * -1f;
				WinCondition.currentObjsCompleted++;
				clickAudio.volume = Random.Range(0.08f, 0.16f);
				clickAudio.Play();
			}
			if (WinCondition.hasWon)
			{
				hasWon = true;
				rigid.isKinematic = true;
				clunkAudio.Play();
			}
		}
	}

	private IEnumerator LerpAngularDrag()
	{
		float i = 0f;
		float rate = 1f / lerpDragDuration;
		float startDrag = rigid.angularDrag;
		while (i < 1f)
		{
			i += Time.deltaTime * rate;
			rigid.angularDrag = Mathf.Lerp(startDrag, endDrag, i);
			yield return null;
		}
		rigid.isKinematic = true;
	}
}

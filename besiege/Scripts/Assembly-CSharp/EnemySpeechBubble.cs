using System.Collections;
using UnityEngine;

public class EnemySpeechBubble : MonoBehaviour
{
	public Transform groupToPickFrom;

	public LerpAndFade lerpCode;

	public float warningDuration = 0.6f;

	public float timeBetweenSpeech = 1f;

	public float randomTimeAmount = 0.5f;

	public string[] speeches;

	public TextMesh textMeshy;

	public Transform bgObj;

	private float randTime;

	private float timer;

	private Transform myTransform;

	private void Start()
	{
		myTransform = base.transform;
		randTime = timeBetweenSpeech + Random.Range(0f - randomTimeAmount, randomTimeAmount);
	}

	private void Update()
	{
		timer += Time.deltaTime;
		if (timer > randTime)
		{
			timer = 0f;
			randTime = timeBetweenSpeech + Random.Range(0f - randomTimeAmount, randomTimeAmount);
			WriteSpeech();
		}
	}

	private void WriteSpeech()
	{
		Vector3 position = groupToPickFrom.GetChild(Random.Range(0, groupToPickFrom.childCount)).position;
		myTransform.position = new Vector3(position.x, myTransform.position.y, position.z);
		textMeshy.text = speeches[Random.Range(0, speeches.Length)];
		bgObj.localScale = new Vector3(textMeshy.GetComponent<Renderer>().bounds.extents.x * 2f + 0.4f, bgObj.localScale.y, bgObj.localScale.z);
	}

	private IEnumerator Warning()
	{
		GetComponent<AudioSource>().Play();
		lerpCode.LerpIn();
		yield return new WaitForSeconds(warningDuration);
		lerpCode.LerpOut();
	}
}

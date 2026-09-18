using UnityEngine;

namespace Synty.Interface.FantasyMenus.Samples
{
	public class SampleScrollerInputs : MonoBehaviour
	{
		[Header("References")]
		public RectTransform viewport;

		public RectTransform content;

		[Header("Parameters")]
		public float speed;

		public float speedChangeIncrement;

		public float slowDownDistance = 200f;

		public float maxSpeed;

		private Vector3 startPosition;

		private float startSpeed;

		private void Start()
		{
			startSpeed = speed;
			startPosition = content.localPosition;
		}

		private void Reset()
		{
			content.localPosition = startPosition;
			speed = startSpeed;
		}

		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
			{
				speed = Mathf.Clamp(speed - speedChangeIncrement, 0f - maxSpeed, 0f);
			}
			else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
			{
				speed = Mathf.Clamp(speed + speedChangeIncrement, 0f, maxSpeed);
			}
			float num = content.sizeDelta.y - viewport.rect.height;
			float num2 = Vector3.Distance(content.localPosition, startPosition);
			float value = num - num2;
			if (speed < 0f)
			{
				value = num2;
			}
			Vector3 vector = Vector3.up * Mathf.Lerp(speed, 0f, Mathf.InverseLerp(slowDownDistance, 0f, value)) * Time.deltaTime;
			content.localPosition += vector;
		}
	}
}

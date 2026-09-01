using System.Collections;
using UnityEngine;

namespace Landfall
{
	public class CodeStateAnimation : MonoBehaviour
	{
		public enum AnimationType
		{
			Position = 0,
			Scale = 1,
			Rotation = 2
		}

		public AnimationType animationType;

		public bool state1 = true;

		private bool isAnimating;

		private bool isState1 = true;

		public bool useX = true;

		public bool useY = true;

		public bool useZ = true;

		[Header("State 1")]
		public AnimationCurve curve;

		public float duration = 1f;

		public float multiplier = 1f;

		[Header("")]
		[Header("State 2")]
		public AnimationCurve curve2;

		public float duration2 = 1f;

		public float multiplier2 = 1f;

		private float baseX;

		private float baseY;

		private float baseZ;

		public bool useTimeScale = true;

		private RectTransform rectTrans;

		private void Awake()
		{
			rectTrans = GetComponent<RectTransform>();
			Initialize();
		}

		public void Initialize()
		{
			switch (animationType)
			{
			case AnimationType.Position:
				if ((bool)rectTrans)
				{
					baseX = rectTrans.anchoredPosition.x;
					baseY = rectTrans.anchoredPosition.y;
					if (!state1)
					{
						float num2 = curve2.Evaluate(1f) * multiplier2;
						Vector2 anchoredPosition = rectTrans.anchoredPosition;
						if (useX)
						{
							anchoredPosition.x = num2;
						}
						if (useY)
						{
							anchoredPosition.y = num2;
						}
						rectTrans.anchoredPosition = anchoredPosition;
						isState1 = false;
					}
				}
				else
				{
					baseX = base.transform.localPosition.x;
					baseY = base.transform.localPosition.y;
					baseZ = base.transform.localPosition.z;
				}
				break;
			case AnimationType.Rotation:
				baseX = base.transform.localRotation.eulerAngles.x;
				baseY = base.transform.localRotation.eulerAngles.y;
				baseZ = base.transform.localRotation.eulerAngles.z;
				break;
			case AnimationType.Scale:
				baseX = base.transform.localScale.x;
				baseY = base.transform.localScale.y;
				baseZ = base.transform.localScale.z;
				if (!state1)
				{
					float num = curve2.Evaluate(1f);
					Vector3 localScale = base.transform.localScale;
					if (useX)
					{
						localScale.x = num * baseX;
					}
					if (useY)
					{
						localScale.y = num * baseY;
					}
					if (useZ)
					{
						localScale.z = num * baseZ;
					}
					base.transform.localScale = localScale;
					isState1 = false;
				}
				break;
			}
		}

		private void Update()
		{
			if (state1 && !isState1 && !isAnimating)
			{
				StartCoroutine(Animation1());
				isState1 = true;
			}
			if (!state1 && isState1 && !isAnimating)
			{
				StartCoroutine(Animation2());
				isState1 = false;
			}
		}

		private IEnumerator Animation1()
		{
			isAnimating = true;
			float t = 0f;
			while (t < duration)
			{
				t = ((!useTimeScale) ? (t + Time.unscaledDeltaTime) : (t + Time.deltaTime));
				float num = curve.Evaluate(t / duration) * multiplier2;
				if (animationType == AnimationType.Position)
				{
					if ((bool)rectTrans)
					{
						Vector2 anchoredPosition = rectTrans.anchoredPosition;
						if (useX)
						{
							anchoredPosition.x = num + baseX;
						}
						if (useY)
						{
							anchoredPosition.y = num + baseY;
						}
						rectTrans.anchoredPosition = anchoredPosition;
					}
					else
					{
						Vector3 localPosition = base.transform.localPosition;
						if (useX)
						{
							localPosition.x = num + baseX;
						}
						if (useY)
						{
							localPosition.y = num + baseY;
						}
						if (useZ)
						{
							localPosition.z = num + baseZ;
						}
						base.transform.localPosition = localPosition;
					}
				}
				if (animationType == AnimationType.Rotation)
				{
					Vector3 eulerAngles = base.transform.localRotation.eulerAngles;
					if (useX)
					{
						eulerAngles.x = num + baseX;
					}
					if (useY)
					{
						eulerAngles.y = num + baseY;
					}
					if (useZ)
					{
						eulerAngles.z = num + baseZ;
					}
					base.transform.localRotation = Quaternion.Euler(eulerAngles);
				}
				if (animationType == AnimationType.Scale)
				{
					Vector3 localScale = base.transform.localScale;
					if (useX)
					{
						localScale.x = num * baseX;
					}
					if (useY)
					{
						localScale.y = num * baseY;
					}
					if (useZ)
					{
						localScale.z = num * baseZ;
					}
					base.transform.localScale = localScale;
				}
				yield return null;
			}
			isAnimating = false;
		}

		private IEnumerator Animation2()
		{
			isAnimating = true;
			float t = 0f;
			while (t < duration2)
			{
				t = ((!useTimeScale) ? (t + Time.unscaledDeltaTime) : (t + Time.deltaTime));
				float num = curve2.Evaluate(t / duration2) * multiplier2;
				if (animationType == AnimationType.Position)
				{
					if ((bool)rectTrans)
					{
						Vector2 anchoredPosition = rectTrans.anchoredPosition;
						if (useX)
						{
							anchoredPosition.x = num + baseX;
						}
						if (useY)
						{
							anchoredPosition.y = num + baseY;
						}
						rectTrans.anchoredPosition = anchoredPosition;
					}
					else
					{
						Vector3 localPosition = base.transform.localPosition;
						if (useX)
						{
							localPosition.x = num + baseX;
						}
						if (useY)
						{
							localPosition.y = num + baseY;
						}
						if (useZ)
						{
							localPosition.z = num + baseZ;
						}
						base.transform.localPosition = localPosition;
					}
				}
				if (animationType == AnimationType.Rotation)
				{
					Vector3 eulerAngles = base.transform.localRotation.eulerAngles;
					if (useX)
					{
						eulerAngles.x = num + baseX;
					}
					if (useY)
					{
						eulerAngles.y = num + baseY;
					}
					if (useZ)
					{
						eulerAngles.z = num + baseZ;
					}
					base.transform.localRotation = Quaternion.Euler(eulerAngles);
				}
				if (animationType == AnimationType.Scale)
				{
					Vector3 localScale = base.transform.localScale;
					if (useX)
					{
						localScale.x = num * baseX;
					}
					if (useY)
					{
						localScale.y = num * baseY;
					}
					if (useZ)
					{
						localScale.z = num * baseZ;
					}
					base.transform.localScale = localScale;
				}
				yield return null;
			}
			isAnimating = false;
		}
	}
}

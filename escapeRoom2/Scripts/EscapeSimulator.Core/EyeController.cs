using System.Collections.Generic;
using UnityEngine;

public class EyeController : MonoBehaviour
{
	private struct EyeBlendShapes
	{
		public int eyeLookOutRight;

		public int eyeLookInRight;

		public int eyeLookUpRight;

		public int eyeLookDownRight;

		public int eyeLookOutLeft;

		public int eyeLookInLeft;

		public int eyeLookUpLeft;

		public int eyeLookDownLeft;

		public int eyeBlinkLeft;

		public int eyeBlinkRight;
	}

	public SkinnedMeshRenderer[] renderers;

	private List<EyeBlendShapes> blendShapeIds = new List<EyeBlendShapes>();

	public Transform head;

	public Transform eyeLeft;

	public Transform eyeRight;

	public Transform target;

	public AnimationCurve eyeCloseCurve = new AnimationCurve();

	public AnimationCurve gptCurve = new AnimationCurve(new Keyframe(0f, 0f, 0f, -10f), new Keyframe(0.2f, 1f, -10f, 0f), new Keyframe(0.4f, 1f, 0f, 0f), new Keyframe(1f, 0f, 0f, 2f));

	private Animator animator;

	private float blinkMin = 3.52f;

	private float blinkMax = 4.28f;

	private float blinkDuration = 0.25f;

	private float timeToBlink = -1f;

	private float blinkAnimation = -1f;

	public float maxEyeAngleYaw = 30f;

	public float maxEyeAnglePitch = 30f;

	public float blendShapeSpeed = 8f;

	private float currentEyeYaw;

	private float currentEyePitch;

	private Vector3 interpolatedPosition;

	private void Awake()
	{
		animator = GetComponent<Animator>();
		SkinnedMeshRenderer[] array = renderers;
		for (int i = 0; i < array.Length; i++)
		{
			Mesh sharedMesh = array[i].sharedMesh;
			blendShapeIds.Add(new EyeBlendShapes
			{
				eyeLookOutRight = sharedMesh.GetBlendShapeIndex("eyeLookOutRight"),
				eyeLookInRight = sharedMesh.GetBlendShapeIndex("eyeLookInRight"),
				eyeLookUpRight = sharedMesh.GetBlendShapeIndex("eyeLookUpRight"),
				eyeLookDownRight = sharedMesh.GetBlendShapeIndex("eyeLookDownRight"),
				eyeLookOutLeft = sharedMesh.GetBlendShapeIndex("eyeLookOutLeft"),
				eyeLookInLeft = sharedMesh.GetBlendShapeIndex("eyeLookInLeft"),
				eyeLookUpLeft = sharedMesh.GetBlendShapeIndex("eyeLookUpLeft"),
				eyeLookDownLeft = sharedMesh.GetBlendShapeIndex("eyeLookDownLeft"),
				eyeBlinkLeft = sharedMesh.GetBlendShapeIndex("eyeBlinkLeft"),
				eyeBlinkRight = sharedMesh.GetBlendShapeIndex("eyeBlinkRight")
			});
		}
	}

	private void Start()
	{
		timeToBlink = Random.Range(blinkMin, blinkMax);
		interpolatedPosition = target.position;
	}

	private void Update()
	{
		if (blinkAnimation <= -1f)
		{
			timeToBlink -= Time.deltaTime;
		}
		if (timeToBlink <= 0f)
		{
			blinkAnimation = 0f;
			timeToBlink = Random.Range(blinkMin, blinkMax);
		}
		if (blinkAnimation >= 0f)
		{
			blinkAnimation += Time.deltaTime / blinkDuration;
		}
		if (blinkAnimation >= 1f)
		{
			blinkAnimation = -1f;
		}
		float value = gptCurve.Evaluate(blinkAnimation) * 100f;
		Vector3 vector = (eyeRight.position + eyeLeft.position) * 0.5f;
		Vector3 normalized = (target.position - vector).normalized;
		Vector3 vector2 = Quaternion.Euler(-90f, 0f, 0f) * head.InverseTransformDirection(normalized);
		float value2 = Mathf.Atan2(vector2.y, vector2.z) * 57.29578f;
		float value3 = Mathf.Atan2(0f - vector2.x, vector2.z) * 57.29578f;
		float num = Mathf.Clamp(value2, 0f - maxEyeAngleYaw, maxEyeAngleYaw);
		float num2 = Mathf.Clamp(value3, 0f - maxEyeAnglePitch, maxEyeAnglePitch);
		currentEyeYaw = Mathf.MoveTowards(currentEyeYaw, num, Time.deltaTime * blendShapeSpeed);
		currentEyePitch = Mathf.MoveTowards(currentEyePitch, num2, Time.deltaTime * blendShapeSpeed);
		for (int i = 0; i < renderers.Length; i++)
		{
			SkinnedMeshRenderer obj = renderers[i];
			EyeBlendShapes eyeBlendShapes = blendShapeIds[i];
			obj.SetBlendShapeWeight(eyeBlendShapes.eyeBlinkLeft, value);
			obj.SetBlendShapeWeight(eyeBlendShapes.eyeBlinkRight, value);
			float value4 = Mathf.Clamp01(Mathf.InverseLerp(0f, maxEyeAngleYaw, currentEyeYaw)) * 100f;
			float value5 = Mathf.Clamp01(Mathf.InverseLerp(0f, 0f - maxEyeAngleYaw, currentEyeYaw)) * 100f;
			float value6 = Mathf.Clamp01(Mathf.InverseLerp(0f, maxEyeAnglePitch, currentEyePitch)) * 100f;
			float value7 = Mathf.Clamp01(Mathf.InverseLerp(0f, 0f - maxEyeAnglePitch, currentEyePitch)) * 100f;
			obj.SetBlendShapeWeight(eyeBlendShapes.eyeLookOutRight, value4);
			obj.SetBlendShapeWeight(eyeBlendShapes.eyeLookInRight, value5);
			obj.SetBlendShapeWeight(eyeBlendShapes.eyeLookUpRight, value6);
			obj.SetBlendShapeWeight(eyeBlendShapes.eyeLookDownRight, value7);
			obj.SetBlendShapeWeight(eyeBlendShapes.eyeLookOutLeft, value5);
			obj.SetBlendShapeWeight(eyeBlendShapes.eyeLookInLeft, value4);
			obj.SetBlendShapeWeight(eyeBlendShapes.eyeLookUpLeft, value6);
			obj.SetBlendShapeWeight(eyeBlendShapes.eyeLookDownLeft, value7);
		}
		interpolatedPosition = Vector3.MoveTowards(interpolatedPosition, target.position, Time.deltaTime * 10f);
	}

	public void forceOpenEyes()
	{
		for (int i = 0; i < renderers.Length; i++)
		{
			SkinnedMeshRenderer obj = renderers[i];
			EyeBlendShapes eyeBlendShapes = blendShapeIds[i];
			obj.SetBlendShapeWeight(eyeBlendShapes.eyeBlinkLeft, 0f);
			obj.SetBlendShapeWeight(eyeBlendShapes.eyeBlinkRight, 0f);
			obj.SetBlendShapeWeight(eyeBlendShapes.eyeLookOutRight, 0f);
			obj.SetBlendShapeWeight(eyeBlendShapes.eyeLookInRight, 0f);
			obj.SetBlendShapeWeight(eyeBlendShapes.eyeLookUpRight, 0f);
			obj.SetBlendShapeWeight(eyeBlendShapes.eyeLookDownRight, 0f);
			obj.SetBlendShapeWeight(eyeBlendShapes.eyeLookOutLeft, 0f);
			obj.SetBlendShapeWeight(eyeBlendShapes.eyeLookInLeft, 0f);
			obj.SetBlendShapeWeight(eyeBlendShapes.eyeLookUpLeft, 0f);
			obj.SetBlendShapeWeight(eyeBlendShapes.eyeLookDownLeft, 0f);
		}
	}

	private void OnAnimatorIK(int layerIndex)
	{
		animator.SetLookAtPosition(target.position);
		animator.SetLookAtWeight(1f, 0.1f, 0.7f, 0.2f);
	}
}

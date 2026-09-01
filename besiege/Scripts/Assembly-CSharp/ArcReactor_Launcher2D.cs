using System;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Arc Reactor Rays/Ray Launcher 2D")]
public class ArcReactor_Launcher2D : MonoBehaviour
{
	public class RayInfo
	{
		public ArcReactor_Arc arc;

		public Transform[] shape;

		public GameObject startObject;

		public GameObject endObject;

		public float distance;
	}

	[Serializable]
	public class ReflectionSettings
	{
		public ReflectSettings reflections;

		public Transform[] reflectors;

		public LayerMask reflectLayers;

		public float thickness = 0.05f;

		public bool sendMessageToReflectors;
	}

	[Serializable]
	public class InertialSettings
	{
		public InertiaMethod type;

		public float speed;

		public float detalization = 10f;

		public bool localDetalization = true;

		public AnimationCurve snapbackForceCurve;

		public float maxSnapBackDistance = 100f;
	}

	public enum InertiaMethod
	{
		none = 0,
		linespeed = 1
	}

	public enum LaunchMethod
	{
		forward_raycast = 0,
		double_raycast = 1
	}

	public enum RayTransformBehaivour
	{
		immobile = 0,
		stick = 1,
		follow_raycast = 2
	}

	public enum ReflectSettings
	{
		no_reflections = 0,
		reflect_specified_objects = 1,
		reflect_by_layer = 2
	}

	private const int maxReflections = 100;

	private const float reflectGap = 0.01f;

	public GameObject arcPrefab;

	public GameObject helperPrefab;

	public LaunchMethod launchMethod;

	public float Distance = 100f;

	public float PropagationSpeed = 10000f;

	public LayerMask layers;

	public RayTransformBehaivour startBehaviour = RayTransformBehaivour.stick;

	public RayTransformBehaivour endBehaviour = RayTransformBehaivour.follow_raycast;

	public bool SendMessageToHitObjects;

	public bool SendMessageToTouchedObjects;

	public LayerMask touchLayers;

	public ReflectionSettings reflectionSettings;

	public InertialSettings rayInertiaSettings;

	public Transform globalSpaceTransform;

	public List<RayInfo> rays;

	protected Vector3[] posArray = new Vector3[200];

	protected int posArrayLen;

	protected Vector3[] positions = new Vector3[100];

	protected RaycastHit2D hit;

	protected List<RayInfo> destrArr;

	public List<RayInfo> Rays
	{
		get
		{
			return rays;
		}
	}

	private void Awake()
	{
		rays = new List<RayInfo>();
		hit = default(RaycastHit2D);
		destrArr = new List<RayInfo>();
	}

	protected bool CheckReflectObject(Transform checkTr)
	{
		Transform[] reflectors = reflectionSettings.reflectors;
		foreach (Transform transform in reflectors)
		{
			if (transform == checkTr)
			{
				return true;
			}
		}
		return false;
	}

	protected Vector2 Get2DVect(Vector3 input)
	{
		return new Vector2(input.x, input.y);
	}

	protected Vector3 Get3DVect(Vector2 input, float Z)
	{
		return new Vector3(input.x, input.y, Z);
	}

	protected void FillPosArray(Vector3 position, Vector3 direction, float maxDistance, RayInfo rayInfo)
	{
		RaycastHit2D raycastHit2D = default(RaycastHit2D);
		if ((bool)(raycastHit2D = Physics2D.Raycast(Get2DVect(position), Get2DVect(direction), maxDistance, layers.value | reflectionSettings.reflectLayers.value)))
		{
			if (SendMessageToHitObjects)
			{
				ArcReactorHitInfo2D arcReactorHitInfo2D = new ArcReactorHitInfo2D();
				arcReactorHitInfo2D.launcher = this;
				arcReactorHitInfo2D.rayInfo = rayInfo;
				arcReactorHitInfo2D.raycastHit = raycastHit2D;
				raycastHit2D.transform.gameObject.SendMessage("ArcReactorHit", arcReactorHitInfo2D, SendMessageOptions.DontRequireReceiver);
			}
			posArray[posArrayLen] = raycastHit2D.point;
			posArrayLen++;
			if (SendMessageToTouchedObjects)
			{
				RaycastHit2D[] array = Physics2D.RaycastAll(Get2DVect(position), Get2DVect(direction), Vector2.Distance(Get2DVect(position), raycastHit2D.point), touchLayers);
				RaycastHit2D[] array2 = array;
				for (int i = 0; i < array2.Length; i++)
				{
					RaycastHit2D raycastHit = array2[i];
					ArcReactorHitInfo2D arcReactorHitInfo2D2 = new ArcReactorHitInfo2D();
					arcReactorHitInfo2D2.launcher = this;
					arcReactorHitInfo2D2.rayInfo = rayInfo;
					arcReactorHitInfo2D2.raycastHit = raycastHit;
					raycastHit.transform.gameObject.SendMessage("ArcReactorTouch", arcReactorHitInfo2D2, SendMessageOptions.DontRequireReceiver);
				}
			}
			if ((reflectionSettings.reflections == ReflectSettings.reflect_by_layer || CheckReflectObject(raycastHit2D.transform)) && (reflectionSettings.reflectLayers.value & (1 << raycastHit2D.transform.gameObject.layer)) > 0)
			{
				if (reflectionSettings.sendMessageToReflectors)
				{
					ArcReactorHitInfo2D arcReactorHitInfo2D3 = new ArcReactorHitInfo2D();
					arcReactorHitInfo2D3.launcher = this;
					arcReactorHitInfo2D3.rayInfo = rayInfo;
					arcReactorHitInfo2D3.raycastHit = raycastHit2D;
					raycastHit2D.transform.gameObject.SendMessage("ArcReactorReflection", arcReactorHitInfo2D3, SendMessageOptions.DontRequireReceiver);
				}
				FillPosArray(raycastHit2D.point + raycastHit2D.normal * reflectionSettings.thickness, Vector3.Reflect(direction, raycastHit2D.normal), maxDistance - Vector3.Distance(position, raycastHit2D.point), rayInfo);
			}
			return;
		}
		if (SendMessageToTouchedObjects)
		{
			RaycastHit2D[] array3 = Physics2D.RaycastAll(Get2DVect(position), Get2DVect(direction), maxDistance, touchLayers);
			RaycastHit2D[] array4 = array3;
			for (int j = 0; j < array4.Length; j++)
			{
				RaycastHit2D raycastHit2 = array4[j];
				ArcReactorHitInfo2D arcReactorHitInfo2D4 = new ArcReactorHitInfo2D();
				arcReactorHitInfo2D4.launcher = this;
				arcReactorHitInfo2D4.rayInfo = rayInfo;
				arcReactorHitInfo2D4.raycastHit = raycastHit2;
				raycastHit2.transform.gameObject.SendMessage("ArcReactorTouch", arcReactorHitInfo2D4, SendMessageOptions.DontRequireReceiver);
			}
		}
		posArray[posArrayLen] = position + direction.normalized * maxDistance;
		posArrayLen++;
	}

	[ContextMenu("Launch Ray")]
	public void LaunchRay()
	{
		if (launchMethod == LaunchMethod.forward_raycast && startBehaviour == RayTransformBehaivour.follow_raycast)
		{
			Debug.LogError("Launch method 'forward_raycast' and start behaviour 'follow_raycast' are incompatible. Change one of the settings.");
			return;
		}
		if (arcPrefab == null)
		{
			Debug.LogError("No arc prefab set.");
			return;
		}
		Transform transform = base.transform;
		GameObject gameObject = new GameObject("rayEndPoint");
		RaycastHit2D raycastHit2D = default(RaycastHit2D);
		Transform transform2 = gameObject.transform;
		if ((bool)(raycastHit2D = Physics2D.Raycast(base.transform.position, base.transform.forward, Distance, layers.value)))
		{
			transform2.position = new Vector3(raycastHit2D.point.x, raycastHit2D.point.y, base.transform.position.z);
		}
		else
		{
			transform2.position = base.transform.position + base.transform.forward * Distance;
		}
		if (endBehaviour == RayTransformBehaivour.stick && raycastHit2D.transform != null)
		{
			transform2.parent = raycastHit2D.transform;
		}
		else
		{
			transform2.parent = globalSpaceTransform;
		}
		switch (launchMethod)
		{
		case LaunchMethod.double_raycast:
			gameObject = new GameObject("rayStartPoint");
			transform = gameObject.transform;
			if ((bool)(raycastHit2D = Physics2D.Raycast(base.transform.position, -base.transform.forward, Distance, layers.value)))
			{
				transform.position = raycastHit2D.point;
			}
			else
			{
				transform.position = base.transform.position - base.transform.forward * Distance;
			}
			if (startBehaviour == RayTransformBehaivour.stick && raycastHit2D.transform != null)
			{
				transform.parent = raycastHit2D.transform;
			}
			break;
		case LaunchMethod.forward_raycast:
			gameObject = new GameObject("rayStartPoint");
			transform = gameObject.transform;
			transform.position = base.transform.position;
			if (startBehaviour == RayTransformBehaivour.stick)
			{
				transform.parent = base.transform;
				transform.rotation = base.transform.rotation;
				if (helperPrefab != null)
				{
					gameObject = UnityEngine.Object.Instantiate(helperPrefab);
					gameObject.transform.parent = transform;
					gameObject.transform.position = transform.transform.position;
					gameObject.transform.rotation = transform.transform.rotation;
				}
			}
			else
			{
				transform.parent = globalSpaceTransform;
			}
			break;
		}
		RayInfo rayInfo = new RayInfo();
		gameObject = ((!(ArcReactor_PoolManager.Instance != null)) ? UnityEngine.Object.Instantiate(arcPrefab) : ArcReactor_PoolManager.Instance.GetFreeEntity(arcPrefab));
		gameObject.transform.parent = globalSpaceTransform;
		rayInfo.arc = gameObject.GetComponent<ArcReactor_Arc>();
		switch (rayInertiaSettings.type)
		{
		case InertiaMethod.none:
			rayInfo.shape = new Transform[2];
			rayInfo.shape[0] = transform;
			rayInfo.shape[1] = transform2;
			rayInfo.arc.shapeTransforms = rayInfo.shape;
			break;
		case InertiaMethod.linespeed:
		{
			int num = 0;
			num = ((!rayInertiaSettings.localDetalization) ? (Mathf.CeilToInt(Vector3.Distance(transform.position, transform2.position) / rayInertiaSettings.detalization) + 2) : (Mathf.CeilToInt(rayInertiaSettings.detalization) + 2));
			rayInfo.shape = new Transform[num];
			rayInfo.shape[0] = transform;
			rayInfo.shape[num - 1] = transform2;
			for (int i = 1; i < num - 1; i++)
			{
				gameObject = new GameObject("rayInertiaPoint");
				gameObject.transform.position = Vector3.Lerp(transform.position, transform2.position, (float)i / (float)(num - 1));
				gameObject.transform.parent = globalSpaceTransform;
				rayInfo.shape[i] = gameObject.transform;
			}
			break;
		}
		}
		rayInfo.arc.shapeTransforms = rayInfo.shape;
		rays.Add(rayInfo);
	}

	private void LateUpdate()
	{
		for (int i = 0; i < rays.Count; i++)
		{
			if (rays[i].arc == null)
			{
				destrArr.Add(rays[i]);
				continue;
			}
			rays[i].distance = Mathf.Clamp(rays[i].distance + PropagationSpeed * Time.deltaTime, 0f, Distance);
			Vector3 zero = Vector3.zero;
			switch (reflectionSettings.reflections)
			{
			case ReflectSettings.no_reflections:
				if (startBehaviour == RayTransformBehaivour.follow_raycast)
				{
					if ((bool)(hit = Physics2D.Raycast(Get2DVect(base.transform.position), Get2DVect(-base.transform.forward), rays[i].distance, layers.value)))
					{
						if (SendMessageToHitObjects)
						{
							ArcReactorHitInfo2D arcReactorHitInfo2D = new ArcReactorHitInfo2D();
							arcReactorHitInfo2D.launcher = this;
							arcReactorHitInfo2D.rayInfo = rays[i];
							arcReactorHitInfo2D.raycastHit = hit;
							hit.transform.gameObject.SendMessage("ArcReactorHit", arcReactorHitInfo2D, SendMessageOptions.DontRequireReceiver);
						}
						if (SendMessageToTouchedObjects)
						{
							RaycastHit2D[] array = Physics2D.RaycastAll(Get2DVect(base.transform.position), Get2DVect(-base.transform.forward), Vector3.Distance(base.transform.position, hit.point), touchLayers);
							RaycastHit2D[] array2 = array;
							for (int num3 = 0; num3 < array2.Length; num3++)
							{
								RaycastHit2D raycastHit = array2[num3];
								ArcReactorHitInfo2D arcReactorHitInfo2D2 = new ArcReactorHitInfo2D();
								arcReactorHitInfo2D2.launcher = this;
								arcReactorHitInfo2D2.rayInfo = rays[i];
								arcReactorHitInfo2D2.raycastHit = raycastHit;
								raycastHit.transform.gameObject.SendMessage("ArcReactorTouch", arcReactorHitInfo2D2, SendMessageOptions.DontRequireReceiver);
							}
						}
						rays[i].startObject = hit.transform.gameObject;
						rays[i].shape[0].position = base.transform.position + (base.transform.position - Get3DVect(hit.point, base.transform.position.z)).normalized * (float)((double)(base.transform.position - Get3DVect(hit.point, base.transform.position.z)).magnitude - 0.05);
					}
					else
					{
						if (SendMessageToTouchedObjects)
						{
							RaycastHit2D[] array3 = Physics2D.RaycastAll(base.transform.position, -base.transform.forward, rays[i].distance, touchLayers);
							RaycastHit2D[] array4 = array3;
							for (int num4 = 0; num4 < array4.Length; num4++)
							{
								RaycastHit2D raycastHit2 = array4[num4];
								ArcReactorHitInfo2D arcReactorHitInfo2D3 = new ArcReactorHitInfo2D();
								arcReactorHitInfo2D3.launcher = this;
								arcReactorHitInfo2D3.rayInfo = rays[i];
								arcReactorHitInfo2D3.raycastHit = raycastHit2;
								raycastHit2.transform.gameObject.SendMessage("ArcReactorTouch", arcReactorHitInfo2D3, SendMessageOptions.DontRequireReceiver);
							}
						}
						rays[i].startObject = null;
						rays[i].shape[0].position = base.transform.position - base.transform.forward * rays[i].distance;
					}
				}
				if (endBehaviour == RayTransformBehaivour.follow_raycast)
				{
					if ((bool)(hit = Physics2D.Raycast(base.transform.position, base.transform.forward, rays[i].distance, layers.value)))
					{
						if (SendMessageToHitObjects)
						{
							ArcReactorHitInfo2D arcReactorHitInfo2D4 = new ArcReactorHitInfo2D();
							arcReactorHitInfo2D4.launcher = this;
							arcReactorHitInfo2D4.rayInfo = rays[i];
							arcReactorHitInfo2D4.raycastHit = hit;
							hit.transform.gameObject.SendMessage("ArcReactorHit", arcReactorHitInfo2D4, SendMessageOptions.DontRequireReceiver);
						}
						if (SendMessageToTouchedObjects)
						{
							RaycastHit2D[] array5 = Physics2D.RaycastAll(base.transform.position, base.transform.forward, Vector3.Distance(base.transform.position, hit.point), touchLayers);
							RaycastHit2D[] array6 = array5;
							for (int num5 = 0; num5 < array6.Length; num5++)
							{
								RaycastHit2D raycastHit3 = array6[num5];
								ArcReactorHitInfo2D arcReactorHitInfo2D5 = new ArcReactorHitInfo2D();
								arcReactorHitInfo2D5.launcher = this;
								arcReactorHitInfo2D5.rayInfo = rays[i];
								arcReactorHitInfo2D5.raycastHit = raycastHit3;
								raycastHit3.transform.gameObject.SendMessage("ArcReactorTouch", arcReactorHitInfo2D5, SendMessageOptions.DontRequireReceiver);
							}
						}
						rays[i].endObject = hit.transform.gameObject;
						zero = base.transform.position + (Get3DVect(hit.point, base.transform.position.z) - base.transform.position).normalized * (float)((double)(Get3DVect(hit.point, base.transform.position.z) - base.transform.position).magnitude - 0.05);
					}
					else
					{
						if (SendMessageToTouchedObjects)
						{
							RaycastHit2D[] array7 = Physics2D.RaycastAll(base.transform.position, base.transform.forward, rays[i].distance, touchLayers);
							RaycastHit2D[] array8 = array7;
							for (int num6 = 0; num6 < array8.Length; num6++)
							{
								RaycastHit2D raycastHit4 = array8[num6];
								ArcReactorHitInfo2D arcReactorHitInfo2D6 = new ArcReactorHitInfo2D();
								arcReactorHitInfo2D6.launcher = this;
								arcReactorHitInfo2D6.rayInfo = rays[i];
								arcReactorHitInfo2D6.raycastHit = raycastHit4;
								raycastHit4.transform.gameObject.SendMessage("ArcReactorTouch", arcReactorHitInfo2D6, SendMessageOptions.DontRequireReceiver);
							}
						}
						rays[i].endObject = null;
						zero = base.transform.position + base.transform.forward * rays[i].distance;
					}
				}
				else
				{
					zero = rays[i].shape[rays[i].shape.Length - 1].position;
				}
				switch (rayInertiaSettings.type)
				{
				case InertiaMethod.none:
					rays[i].shape[rays[i].shape.Length - 1].position = zero;
					break;
				case InertiaMethod.linespeed:
				{
					int num7 = rays[i].shape.Length;
					for (int num8 = 1; num8 < num7; num8++)
					{
						Vector3 vector = Vector3.Lerp(rays[i].shape[0].position, zero, (float)num8 / (float)(num7 - 1));
						rays[i].shape[num8].position = Vector3.MoveTowards(rays[i].shape[num8].position, vector, rayInertiaSettings.speed * rayInertiaSettings.snapbackForceCurve.Evaluate(Vector3.Distance(rays[i].shape[num8].position, vector) / rayInertiaSettings.maxSnapBackDistance) * Time.deltaTime);
					}
					break;
				}
				}
				break;
			case ReflectSettings.reflect_specified_objects:
			case ReflectSettings.reflect_by_layer:
			{
				int num;
				if (startBehaviour == RayTransformBehaivour.follow_raycast)
				{
					posArrayLen = 0;
					FillPosArray(base.transform.position, -base.transform.forward, rays[i].distance, rays[i]);
					for (int j = 0; j < posArrayLen; j++)
					{
						positions[j] = posArray[j];
					}
					num = posArrayLen;
				}
				else
				{
					num = 1;
					positions[0] = rays[i].shape[0].position;
				}
				if (endBehaviour == RayTransformBehaivour.follow_raycast)
				{
					posArrayLen = 0;
					FillPosArray(base.transform.position, base.transform.forward, rays[i].distance, rays[i]);
					for (int k = 0; k < posArrayLen; k++)
					{
						positions[num + k] = posArray[k];
					}
					num += posArrayLen;
				}
				else
				{
					positions[num] = rays[i].shape[rays[i].shape.Length - 1].position;
					num++;
				}
				if (rays[i].shape.Length == num)
				{
				}
				if (rays[i].shape.Length > num)
				{
					for (int l = num - 1; l < rays[i].shape.Length - 1; l++)
					{
						UnityEngine.Object.Destroy(rays[i].shape[l].gameObject);
					}
					rays[i].shape[num - 1] = rays[i].shape[rays[i].shape.Length - 1];
					Array.Resize(ref rays[i].shape, num);
				}
				if (rays[i].shape.Length < num)
				{
					int num2 = rays[i].shape.Length;
					Array.Resize(ref rays[i].shape, num);
					rays[i].shape[rays[i].shape.Length - 1] = rays[i].shape[num2 - 1];
					for (int m = num2 - 1; m < num - 1; m++)
					{
						GameObject gameObject = new GameObject("RayPoint" + (m + 1));
						gameObject.transform.parent = globalSpaceTransform;
						rays[i].shape[m] = gameObject.transform;
					}
				}
				for (int n = 0; n < num; n++)
				{
					rays[i].shape[n].position = positions[n];
				}
				rays[i].arc.shapeTransforms = rays[i].shape;
				break;
			}
			}
		}
		for (int num9 = 0; num9 < destrArr.Count; num9++)
		{
			Transform[] shape = destrArr[num9].shape;
			foreach (Transform transform in shape)
			{
				UnityEngine.Object.Destroy(transform.gameObject);
			}
			rays.RemoveAt(num9);
		}
		if (destrArr.Count > 0)
		{
			destrArr.Clear();
		}
	}
}

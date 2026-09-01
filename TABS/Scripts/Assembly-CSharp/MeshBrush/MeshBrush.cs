using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using UnityEngine;

namespace MeshBrush
{
	public class MeshBrush : MonoBehaviour
	{
		public const float version = 1.9f;

		public bool active = true;

		public string groupName = "<group name>";

		public bool[] layerMask = new bool[32]
		{
			true, true, true, true, true, true, true, true, true, true,
			true, true, true, true, true, true, true, true, true, true,
			true, true, true, true, true, true, true, true, true, true,
			true, true
		};

		public float radius = 0.3f;

		public Color color = Color.white;

		public Vector2 quantityRange = Vector2.one;

		public bool useDensity;

		public Vector2 densityRange = new Vector2(0.5f, 0.5f);

		public float delay = 0.25f;

		public Vector2 offsetRange;

		public Vector2 slopeInfluenceRange = new Vector2(95f, 100f);

		public bool useSlopeFilter;

		public Vector2 angleThresholdRange = new Vector2(25f, 30f);

		public bool inverseSlopeFilter;

		public Vector3 slopeReferenceVector = Vector3.up;

		public Vector3 slopeReferenceVectorSampleLocation = Vector3.zero;

		public bool yAxisTangent;

		public bool strokeAlignment;

		public bool autoIgnoreRaycast;

		public Vector2 scatteringRange = new Vector2(70f, 80f);

		public bool useOverlapFilter;

		public Vector2 minimumAbsoluteDistanceRange = new Vector2(0.25f, 0.5f);

		public bool uniformRandomScale = true;

		public bool uniformAdditiveScale = true;

		public Vector2 randomScaleRange = Vector2.one;

		public Vector2 randomScaleRangeX = Vector2.one;

		public Vector2 randomScaleRangeY = Vector2.one;

		public Vector2 randomScaleRangeZ = Vector2.one;

		public Vector2 additiveScaleRange = Vector2.zero;

		public Vector3 additiveScaleNonUniform = Vector3.zero;

		public AnimationCurve randomScaleCurve = AnimationCurve.Linear(0f, 1f, 1f, 1f);

		public float randomScaleCurveVariation;

		public Vector2 randomRotationRange = new Vector2(0f, 5f);

		public bool positionBrushRandomizer;

		public bool rotationBrushRandomizer = true;

		public bool scaleBrushRandomizer = true;

		public KeyCode paintKey = KeyCode.P;

		public KeyCode deleteKey = KeyCode.L;

		public KeyCode combineKey = KeyCode.K;

		public KeyCode randomizeKey = KeyCode.J;

		public KeyCode increaseRadiusKey = KeyCode.O;

		public KeyCode decreaseRadiusKey = KeyCode.I;

		[SerializeField]
		private int maxQuantityLimit = 100;

		[SerializeField]
		private float maxDelayLimit = 1f;

		[SerializeField]
		private float maxDensityLimit = 10f;

		[SerializeField]
		private float minOffsetLimit = -50f;

		[SerializeField]
		private float maxOffsetLimit = 50f;

		[SerializeField]
		private float maxMinimumAbsoluteDistanceLimit = 3f;

		[SerializeField]
		private float maxAdditiveScaleLimit = 3f;

		[SerializeField]
		private float maxRandomScaleLimit = 3f;

		public bool helpFoldout;

		public bool helpTemplatesFoldout;

		public bool helpGeneralUsageFoldout;

		public bool helpOptimizationFoldout;

		public bool meshesFoldout = true;

		public bool templatesFoldout = true;

		public bool keyBindingsFoldout;

		public bool brushFoldout = true;

		public bool slopesFoldout = true;

		public bool randomizersFoldout = true;

		public bool overlapFilterFoldout = true;

		public bool additiveScaleFoldout = true;

		public bool optimizationFoldout = true;

		[SerializeField]
		private bool globalPaintingMode;

		public bool collapsed;

		public bool stats;

		public bool lockSceneView;

		public bool classicUI;

		public float previewIconSize = 60f;

		public bool manualReferenceVectorSampling;

		public bool showReferenceVectorInSceneView = true;

		public bool autoStatic;

		public bool autoSelectOnCombine = true;

		private Transform cachedTransform;

		private Collider cachedCollider;

		private GameObject brush;

		private Transform brushTransform;

		private Transform holderObj;

		private const string minString = "min";

		private const string maxString = "max";

		private const string trueString = "true";

		private const string falseString = "false";

		private const string enabledString = "enabled";

		public Vector3 lastPaintLocation;

		public Vector3 brushStrokeDirection;

		[SerializeField]
		private List<GameObject> meshes = new List<GameObject>(5) { null };

		private List<Transform> paintedMeshes = new List<Transform>(200);

		private List<Transform> paintedMeshesInsideBrushArea = new List<Transform>(50);

		private float nextFeasibleStrokeTime;

		public Transform CachedTransform
		{
			get
			{
				if (cachedTransform == null)
				{
					cachedTransform = base.transform;
				}
				return cachedTransform;
			}
		}

		public Collider CachedCollider
		{
			get
			{
				if (cachedCollider == null)
				{
					cachedCollider = GetComponent<Collider>();
				}
				return cachedCollider;
			}
		}

		public GameObject Brush
		{
			get
			{
				CheckBrush();
				return brush;
			}
		}

		public Transform BrushTransform
		{
			get
			{
				CheckBrush();
				return brushTransform;
			}
		}

		public Transform HolderObj
		{
			get
			{
				CheckHolder();
				return holderObj;
			}
		}

		public void OnValidate()
		{
			ValidateKeyBindings();
			ValidateRangeLimits();
			if (meshes.Count == 0)
			{
				meshes.Add(null);
			}
			if (layerMask.Length != 32)
			{
				layerMask = new bool[32];
				for (int num = layerMask.Length - 1; num >= 0; num--)
				{
					layerMask[num] = true;
				}
			}
			if (layerMask[2])
			{
				layerMask[2] = false;
			}
			if (radius < 0.01f)
			{
				radius = 0.01f;
			}
			radius = (float)Math.Round(radius, 3);
			VectorClampingUtility.ClampVector(ref quantityRange, 1f, maxQuantityLimit, 1f, maxQuantityLimit);
			VectorClampingUtility.ClampVector(ref densityRange, 0.1f, maxDensityLimit, 0.1f, maxDensityLimit);
			delay = Mathf.Clamp(delay, 0.03f, maxDelayLimit);
			randomScaleCurveVariation = Mathf.Clamp(randomScaleCurveVariation, 0f, 3f);
			VectorClampingUtility.ClampVector(ref offsetRange, minOffsetLimit, maxOffsetLimit, minOffsetLimit, maxOffsetLimit);
			VectorClampingUtility.ClampVector(ref scatteringRange, 0f, 100f, 0f, 100f);
			VectorClampingUtility.ClampVector(ref slopeInfluenceRange, 0f, 100f, 0f, 100f);
			VectorClampingUtility.ClampVector(ref angleThresholdRange, 1f, 180f, 1f, 180f);
			VectorClampingUtility.ClampVector(ref minimumAbsoluteDistanceRange, 0f, maxMinimumAbsoluteDistanceLimit, 0f, maxMinimumAbsoluteDistanceLimit);
			VectorClampingUtility.ClampVector(ref randomScaleRange, 0.01f, maxRandomScaleLimit, 0f, maxRandomScaleLimit);
			VectorClampingUtility.ClampVector(ref randomScaleRangeX, 0.01f, maxRandomScaleLimit, 0f, maxRandomScaleLimit);
			VectorClampingUtility.ClampVector(ref randomScaleRangeY, 0.01f, maxRandomScaleLimit, 0f, maxRandomScaleLimit);
			VectorClampingUtility.ClampVector(ref randomScaleRangeZ, 0.01f, maxRandomScaleLimit, 0f, maxRandomScaleLimit);
			VectorClampingUtility.ClampVector(ref randomRotationRange, 0f, 100f, 0f, 100f);
			VectorClampingUtility.ClampVector(ref additiveScaleRange, -0.9f, maxAdditiveScaleLimit, -0.9f, maxAdditiveScaleLimit);
			VectorClampingUtility.ClampVector(ref additiveScaleNonUniform, -0.9f, maxAdditiveScaleLimit, -0.9f, maxAdditiveScaleLimit, -0.9f, maxAdditiveScaleLimit);
		}

		private void ValidateRangeLimits()
		{
			maxQuantityLimit = Mathf.Clamp(maxQuantityLimit, 1, 1000);
			maxDensityLimit = Mathf.Clamp(maxDensityLimit, 1f, 1000f);
			maxDelayLimit = Mathf.Clamp(maxDelayLimit, 1f, 10f);
			minOffsetLimit = Mathf.Clamp(minOffsetLimit, -1000f, -1f);
			maxOffsetLimit = Mathf.Clamp(maxOffsetLimit, 1f, 1000f);
			maxMinimumAbsoluteDistanceLimit = Mathf.Clamp(maxMinimumAbsoluteDistanceLimit, 3f, 1000f);
			maxAdditiveScaleLimit = Mathf.Clamp(maxAdditiveScaleLimit, 3f, 1000f);
			maxRandomScaleLimit = Mathf.Clamp(maxRandomScaleLimit, 3f, 1000f);
		}

		private void ValidateKeyBindings()
		{
			if (paintKey == KeyCode.None)
			{
				paintKey = KeyCode.P;
			}
			if (deleteKey == KeyCode.None)
			{
				deleteKey = KeyCode.L;
			}
			if (randomizeKey == KeyCode.None)
			{
				randomizeKey = KeyCode.J;
			}
			if (combineKey == KeyCode.None)
			{
				combineKey = KeyCode.K;
			}
			if (increaseRadiusKey == KeyCode.None)
			{
				increaseRadiusKey = KeyCode.O;
			}
			if (decreaseRadiusKey == KeyCode.None)
			{
				decreaseRadiusKey = KeyCode.I;
			}
		}

		public void GatherPaintedMeshes()
		{
			paintedMeshes = HolderObj.GetComponentsInChildren<Transform>().ToList();
		}

		public void CleanSetOfMeshesToPaint()
		{
			if (meshes.Count <= 1)
			{
				return;
			}
			for (int num = meshes.Count - 1; num >= 0; num--)
			{
				if (meshes[num] == null)
				{
					meshes.RemoveAt(num);
				}
			}
			if (meshes.Count == 0)
			{
				meshes.Add(null);
			}
		}

		private void GatherMeshesInsideBrushArea(RaycastHit brushLocation)
		{
			paintedMeshesInsideBrushArea.Clear();
			foreach (Transform paintedMesh in paintedMeshes)
			{
				if (paintedMesh != null && paintedMesh != BrushTransform && paintedMesh != HolderObj && Vector3.Distance(brushLocation.point, paintedMesh.position) < radius)
				{
					paintedMeshesInsideBrushArea.Add(paintedMesh);
				}
			}
		}

		public void PaintMeshes(RaycastHit brushLocation)
		{
			if (nextFeasibleStrokeTime >= Time.realtimeSinceStartup)
			{
				return;
			}
			nextFeasibleStrokeTime = Time.realtimeSinceStartup + delay;
			CheckBrush();
			brushStrokeDirection = brushLocation.point - lastPaintLocation;
			int num = (useDensity ? ((int)(radius * radius * (float)Math.PI * UnityEngine.Random.Range(densityRange.x, densityRange.y))) : ((int)UnityEngine.Random.Range(quantityRange.x, quantityRange.y + 1f)));
			if (num <= 0)
			{
				num = 1;
			}
			if (useOverlapFilter)
			{
				GatherMeshesInsideBrushArea(brushLocation);
			}
			bool flag = false;
			for (int num2 = num; num2 > 0; num2--)
			{
				float num3 = radius * 0.01f * UnityEngine.Random.Range(scatteringRange.x, scatteringRange.y);
				brushTransform.position = brushLocation.point + brushLocation.normal * 0.5f;
				brushTransform.rotation = Quaternion.LookRotation(brushLocation.normal);
				brushTransform.up = brushTransform.forward;
				if (num > 1)
				{
					brushTransform.Translate(UnityEngine.Random.Range((0f - UnityEngine.Random.insideUnitCircle.x) * num3, UnityEngine.Random.insideUnitCircle.x * num3), 0f, UnityEngine.Random.Range((0f - UnityEngine.Random.insideUnitCircle.y) * num3, UnityEngine.Random.insideUnitCircle.y * num3), Space.Self);
				}
				if (globalPaintingMode ? Physics.Raycast(new Ray(brushTransform.position, -brushLocation.normal), out var hitInfo, 2.5f) : CachedCollider.Raycast(new Ray(brushTransform.position, -brushLocation.normal), out hitInfo, 2.5f))
				{
					float num4 = (useSlopeFilter ? Vector3.Angle(hitInfo.normal, manualReferenceVectorSampling ? slopeReferenceVector : Vector3.up) : (inverseSlopeFilter ? 180f : 0f));
					if ((inverseSlopeFilter ? (num4 > UnityEngine.Random.Range(angleThresholdRange.x, angleThresholdRange.y)) : (num4 < UnityEngine.Random.Range(angleThresholdRange.x, angleThresholdRange.y))) && (!useOverlapFilter || !CheckOverlap(hitInfo.point)))
					{
						GameObject gameObject = null;
						gameObject = UnityEngine.Object.Instantiate(meshes[UnityEngine.Random.Range(0, meshes.Count)]);
						if (gameObject == null)
						{
							if (!flag)
							{
								flag = true;
								Debug.LogError("MeshBrush: one or more fields in the set of meshes to paint is null. Please assign all fields before painting (or remove empty ones).");
							}
						}
						else
						{
							if (autoIgnoreRaycast)
							{
								gameObject.layer = 2;
							}
							Transform transform = gameObject.transform;
							OrientPaintedMesh(transform, hitInfo);
							if (Mathf.Abs(offsetRange.x) > float.Epsilon || Mathf.Abs(offsetRange.y) > float.Epsilon)
							{
								MeshTransformationUtility.ApplyMeshOffset(transform, UnityEngine.Random.Range(offsetRange.x, offsetRange.y), brushLocation.normal);
							}
							if (uniformRandomScale)
							{
								if (Mathf.Abs(randomScaleRange.x - 1f) > float.Epsilon || Mathf.Abs(randomScaleRange.y - 1f) > float.Epsilon)
								{
									MeshTransformationUtility.ApplyRandomScale(transform, randomScaleRange);
								}
							}
							else if (Mathf.Abs(randomScaleRangeX.x - 1f) > float.Epsilon || Mathf.Abs(randomScaleRangeX.y - 1f) > float.Epsilon || Mathf.Abs(randomScaleRangeY.x - 1f) > float.Epsilon || Mathf.Abs(randomScaleRangeY.y - 1f) > float.Epsilon || Mathf.Abs(randomScaleRangeZ.x - 1f) > float.Epsilon || Mathf.Abs(randomScaleRangeZ.y - 1f) > float.Epsilon)
							{
								MeshTransformationUtility.ApplyRandomScale(transform, randomScaleRangeX, randomScaleRangeY, randomScaleRangeZ);
							}
							transform.localScale *= Mathf.Abs(randomScaleCurve.Evaluate(Vector3.Distance(transform.position, brushLocation.point) / radius) + UnityEngine.Random.Range(0f - randomScaleCurveVariation, randomScaleCurveVariation));
							if (uniformAdditiveScale)
							{
								if (Mathf.Abs(additiveScaleRange.x) > float.Epsilon || Mathf.Abs(additiveScaleRange.y) > float.Epsilon)
								{
									MeshTransformationUtility.AddConstantScale(transform, additiveScaleRange);
								}
							}
							else if (Mathf.Abs(additiveScaleNonUniform.x) > float.Epsilon || Mathf.Abs(additiveScaleNonUniform.y) > float.Epsilon || Mathf.Abs(additiveScaleNonUniform.z) > float.Epsilon)
							{
								MeshTransformationUtility.AddConstantScale(transform, additiveScaleNonUniform.x, additiveScaleNonUniform.y, additiveScaleNonUniform.z);
							}
							if (randomRotationRange.x > 0f || randomRotationRange.y > 0f)
							{
								MeshTransformationUtility.ApplyRandomRotation(transform, UnityEngine.Random.Range(randomRotationRange.x, randomRotationRange.y));
							}
							transform.parent = HolderObj;
							gameObject.isStatic |= autoStatic;
							paintedMeshes.Add(transform);
						}
					}
				}
			}
			lastPaintLocation = brushLocation.point;
		}

		public void RandomizeMeshes(RaycastHit brushLocation)
		{
			if (nextFeasibleStrokeTime >= Time.realtimeSinceStartup)
			{
				return;
			}
			nextFeasibleStrokeTime = Time.realtimeSinceStartup + delay;
			GatherMeshesInsideBrushArea(brushLocation);
			for (int num = paintedMeshesInsideBrushArea.Count - 1; num >= 0; num--)
			{
				Transform transform = paintedMeshesInsideBrushArea[num];
				if (transform != null)
				{
					if (positionBrushRandomizer)
					{
						float num2 = radius * 0.01f * UnityEngine.Random.Range(scatteringRange.x, scatteringRange.y);
						brushTransform.position = brushLocation.point + brushLocation.normal * 0.5f;
						brushTransform.rotation = Quaternion.LookRotation(brushLocation.normal);
						brushTransform.up = brushTransform.forward;
						brushTransform.Translate(UnityEngine.Random.Range((0f - UnityEngine.Random.insideUnitCircle.x) * num2, UnityEngine.Random.insideUnitCircle.x * num2), 0f, UnityEngine.Random.Range((0f - UnityEngine.Random.insideUnitCircle.y) * num2, UnityEngine.Random.insideUnitCircle.y * num2), Space.Self);
						if (globalPaintingMode ? Physics.Raycast(new Ray(brushTransform.position, -brushLocation.normal), out var hitInfo, 2.5f) : CachedCollider.Raycast(new Ray(brushTransform.position, -brushLocation.normal), out hitInfo, 2.5f))
						{
							OrientPaintedMesh(transform, hitInfo);
						}
						if (Mathf.Abs(offsetRange.x) > float.Epsilon || Mathf.Abs(offsetRange.y) > float.Epsilon)
						{
							MeshTransformationUtility.ApplyMeshOffset(transform, UnityEngine.Random.Range(offsetRange.x, offsetRange.y), brushLocation.normal);
						}
					}
					if (rotationBrushRandomizer && (randomRotationRange.x > 0f || randomRotationRange.y > 0f))
					{
						MeshTransformationUtility.ApplyRandomRotation(transform, UnityEngine.Random.Range(randomRotationRange.x, randomRotationRange.y));
					}
					if (scaleBrushRandomizer)
					{
						if (uniformRandomScale)
						{
							if (Mathf.Abs(randomScaleRange.x - 1f) > float.Epsilon || Mathf.Abs(randomScaleRange.y - 1f) > float.Epsilon)
							{
								MeshTransformationUtility.ApplyRandomScale(transform, randomScaleRange);
							}
						}
						else if (Mathf.Abs(randomScaleRangeX.x - 1f) > float.Epsilon || Mathf.Abs(randomScaleRangeX.y - 1f) > float.Epsilon || Mathf.Abs(randomScaleRangeY.x - 1f) > float.Epsilon || Mathf.Abs(randomScaleRangeY.y - 1f) > float.Epsilon || Mathf.Abs(randomScaleRangeZ.x - 1f) > float.Epsilon || Mathf.Abs(randomScaleRangeZ.y - 1f) > float.Epsilon)
						{
							MeshTransformationUtility.ApplyRandomScale(transform, randomScaleRangeX, randomScaleRangeY, randomScaleRangeZ);
						}
						transform.localScale *= Mathf.Abs(randomScaleCurve.Evaluate(Vector3.Distance(transform.position, brushLocation.point) / radius) + UnityEngine.Random.Range(0f - randomScaleCurveVariation, randomScaleCurveVariation));
					}
				}
			}
		}

		public void DeleteMeshes(RaycastHit brushLocation)
		{
			if (!(nextFeasibleStrokeTime >= Time.realtimeSinceStartup))
			{
				nextFeasibleStrokeTime = Time.realtimeSinceStartup + delay;
				GatherMeshesInsideBrushArea(brushLocation);
				for (int num = paintedMeshesInsideBrushArea.Count - 1; num >= 0; num--)
				{
					paintedMeshes.Remove(paintedMeshesInsideBrushArea[num]);
					UnityEngine.Object.Destroy(paintedMeshesInsideBrushArea[num].gameObject);
				}
			}
		}

		public void CombineMeshes(RaycastHit brushLocation)
		{
			if (nextFeasibleStrokeTime >= Time.realtimeSinceStartup)
			{
				return;
			}
			nextFeasibleStrokeTime = Time.realtimeSinceStartup + delay;
			GatherMeshesInsideBrushArea(brushLocation);
			if (paintedMeshesInsideBrushArea.Count > 0)
			{
				HolderObj.GetComponent<MeshBrushParent>().CombinePaintedMeshes(autoSelectOnCombine, paintedMeshesInsideBrushArea.Select((Transform mesh) => mesh.GetComponent<MeshFilter>()).ToArray());
			}
		}

		public void SampleReferenceVector(Vector3 referenceVector, Vector3 sampleLocation)
		{
			slopeReferenceVector = referenceVector;
			slopeReferenceVectorSampleLocation = sampleLocation;
		}

		private void OrientPaintedMesh(Transform targetTransform, RaycastHit targetLocation)
		{
			targetTransform.position = targetLocation.point;
			targetTransform.rotation = Quaternion.LookRotation(targetLocation.normal);
			Vector3 normal = Vector3.Lerp(yAxisTangent ? targetTransform.up : Vector3.up, targetTransform.forward, UnityEngine.Random.Range(slopeInfluenceRange.x, slopeInfluenceRange.y) * 0.01f);
			Vector3 tangent = ((strokeAlignment && brushStrokeDirection != Vector3.zero && lastPaintLocation != Vector3.zero) ? brushStrokeDirection : targetTransform.forward);
			Vector3.OrthoNormalize(ref normal, ref tangent);
			targetTransform.rotation = Quaternion.LookRotation(tangent, normal);
		}

		private bool CheckOverlap(Vector3 objPos)
		{
			if (paintedMeshes == null || paintedMeshes.Count < 1)
			{
				return false;
			}
			foreach (Transform paintedMesh in paintedMeshes)
			{
				if (paintedMesh != null && paintedMesh != BrushTransform && paintedMesh != HolderObj && Vector3.Distance(objPos, paintedMesh.position) < UnityEngine.Random.Range(minimumAbsoluteDistanceRange.x, minimumAbsoluteDistanceRange.y))
				{
					return true;
				}
			}
			return false;
		}

		private void CheckHolder()
		{
			MeshBrushParent[] componentsInChildren = GetComponentsInChildren<MeshBrushParent>();
			if (componentsInChildren.Length != 0)
			{
				holderObj = null;
				for (int i = 0; i < componentsInChildren.Length; i++)
				{
					if (componentsInChildren[i] != null && string.CompareOrdinal(componentsInChildren[i].name, groupName) == 0)
					{
						holderObj = componentsInChildren[i].transform;
					}
				}
				if (holderObj == null)
				{
					CreateHolder();
				}
			}
			else
			{
				CreateHolder();
			}
		}

		private void CheckBrush()
		{
			CheckHolder();
			brushTransform = holderObj.Find("Brush");
			if (brushTransform == null)
			{
				CreateBrush();
			}
		}

		private void CreateHolder()
		{
			GameObject gameObject = new GameObject(groupName);
			gameObject.AddComponent<MeshBrushParent>();
			gameObject.transform.rotation = CachedTransform.rotation;
			gameObject.transform.parent = CachedTransform;
			gameObject.transform.localPosition = Vector3.zero;
			holderObj = gameObject.transform;
		}

		private void CreateBrush()
		{
			brush = new GameObject("Brush");
			brushTransform = brush.transform;
			brushTransform.position = CachedTransform.position;
			brushTransform.parent = holderObj;
		}

		public void ResetKeyBindings()
		{
			paintKey = KeyCode.P;
			deleteKey = KeyCode.L;
			combineKey = KeyCode.K;
			randomizeKey = KeyCode.J;
			increaseRadiusKey = KeyCode.O;
			decreaseRadiusKey = KeyCode.I;
		}

		public void ResetSlopeSettings()
		{
			slopeInfluenceRange = new Vector2(95f, 100f);
			angleThresholdRange = new Vector2(25f, 30f);
			useSlopeFilter = false;
			inverseSlopeFilter = false;
			manualReferenceVectorSampling = false;
			showReferenceVectorInSceneView = true;
		}

		public void ResetRandomizers()
		{
			randomScaleRange = Vector2.one;
			randomScaleRangeX = (randomScaleRangeY = (randomScaleRangeZ = Vector2.one));
			randomScaleCurve = AnimationCurve.Linear(0f, 1f, 1f, 1f);
			randomScaleCurveVariation = 0f;
			randomRotationRange = new Vector2(0f, 5f);
			positionBrushRandomizer = false;
			rotationBrushRandomizer = true;
			scaleBrushRandomizer = true;
		}

		public void ResetAdditiveScale()
		{
			uniformRandomScale = true;
			additiveScaleRange = Vector2.zero;
			additiveScaleNonUniform = Vector3.zero;
		}

		public void ResetOverlapFilterSettings()
		{
			useOverlapFilter = false;
			minimumAbsoluteDistanceRange = new Vector2(0.25f, 0.5f);
		}

		public XDocument SaveTemplate(string filePath)
		{
			XDocument xDocument = new XDocument(new XElement("meshBrushTemplate", new XAttribute("version", 1.9f), new XElement("instance", new XElement("active", active), new XElement("name", groupName), new XElement("stats", stats), new XElement("lockSceneView", lockSceneView)), new XElement("meshes", new XElement("ui", new XElement("style", classicUI ? "classic" : "modern"), new XElement("iconSize", previewIconSize))), new XElement("keyBindings", new XElement("paint", paintKey), new XElement("delete", deleteKey), new XElement("combine", combineKey), new XElement("randomize", randomizeKey), new XElement("increaseRadius", increaseRadiusKey), new XElement("decreaseRadius", decreaseRadiusKey)), new XElement("brush", new XElement("radius", radius), new XElement("color", new XElement("r", color.r), new XElement("g", color.g), new XElement("b", color.b), new XElement("a", color.a)), new XElement("quantity", new XElement("min", quantityRange.x), new XElement("max", quantityRange.y)), new XElement("useDensity", useDensity), new XElement("density", new XElement("min", densityRange.x), new XElement("max", densityRange.y)), new XElement("offset", new XElement("min", offsetRange.x), new XElement("max", offsetRange.y)), new XElement("scattering", new XElement("min", scatteringRange.x), new XElement("max", scatteringRange.y)), new XElement("delay", delay), new XElement("yAxisTangent", yAxisTangent), new XElement("strokeAlignment", strokeAlignment)), new XElement("slopes", new XElement("slopeInfluence", new XElement("min", slopeInfluenceRange.x), new XElement("max", slopeInfluenceRange.y)), new XElement("slopeFilter", new XElement("enabled", useSlopeFilter), new XElement("inverse", inverseSlopeFilter), new XElement("angleThreshold", new XElement("min", angleThresholdRange.x), new XElement("max", angleThresholdRange.y)), new XElement("manualReferenceVectorSampling", manualReferenceVectorSampling), new XElement("showReferenceVectorInSceneView", showReferenceVectorInSceneView), new XElement("referenceVector", new XElement("x", slopeReferenceVector.x), new XElement("y", slopeReferenceVector.y), new XElement("z", slopeReferenceVector.z)), new XElement("referenceVectorSampleLocation", new XElement("x", slopeReferenceVectorSampleLocation.x), new XElement("y", slopeReferenceVectorSampleLocation.y), new XElement("z", slopeReferenceVectorSampleLocation.z)))), new XElement("randomizers", new XElement("scale", new XElement("scaleUniformly", uniformRandomScale), new XElement("uniform", new XElement("min", randomScaleRange.x), new XElement("max", randomScaleRange.y)), new XElement("nonUniform", new XElement("x", new XElement("min", randomScaleRangeX.x), new XElement("max", randomScaleRangeX.y)), new XElement("y", new XElement("min", randomScaleRangeY.x), new XElement("max", randomScaleRangeY.y)), new XElement("z", new XElement("min", randomScaleRangeZ.x), new XElement("max", randomScaleRangeZ.y))), new XElement("curve", new XElement("variation", randomScaleCurveVariation), new XElement("keys", randomScaleCurve.keys.Select((Keyframe key) => new XElement("key", new XElement("time", key.time), new XElement("value", key.value), new XElement("inTangent", key.inTangent), new XElement("outTangent", key.outTangent)))))), new XElement("rotation", new XElement("min", randomRotationRange.x), new XElement("max", randomRotationRange.y)), new XElement("randomizerBrush", new XElement("position", positionBrushRandomizer), new XElement("rotation", rotationBrushRandomizer), new XElement("scale", scaleBrushRandomizer))), new XElement("overlapFilter", new XElement("enabled", useOverlapFilter), new XElement("minimumAbsoluteDistance", new XElement("min", minimumAbsoluteDistanceRange.x), new XElement("max", minimumAbsoluteDistanceRange.y))), new XElement("additiveScale", new XElement("scaleUniformly", uniformAdditiveScale), new XElement("uniform", new XElement("min", additiveScaleRange.x), new XElement("max", additiveScaleRange.y)), new XElement("nonUniform", new XElement("x", additiveScaleNonUniform.x), new XElement("y", additiveScaleNonUniform.y), new XElement("z", additiveScaleNonUniform.z))), new XElement("optimization", new XElement("autoIgnoreRaycast", autoIgnoreRaycast), new XElement("autoSelectOnCombine", autoSelectOnCombine), new XElement("autoStatic", autoStatic)), new XElement("rangeLimits", new XElement("quantity", new XElement("max", maxQuantityLimit)), new XElement("density", new XElement("max", maxDensityLimit)), new XElement("offset", new XElement("min", minOffsetLimit), new XElement("max", maxOffsetLimit)), new XElement("delay", new XElement("max", maxDelayLimit)), new XElement("minimumAbsoluteDistance", new XElement("max", maxMinimumAbsoluteDistanceLimit)), new XElement("randomScale", new XElement("max", maxRandomScaleLimit)), new XElement("additiveScale", new XElement("max", maxAdditiveScaleLimit))), new XElement("inspectorFoldouts", new XElement("help", helpFoldout), new XElement("templatesHelp", helpTemplatesFoldout), new XElement("generalUsageHelp", helpGeneralUsageFoldout), new XElement("optimizationHelp", helpOptimizationFoldout), new XElement("meshes", meshesFoldout), new XElement("templates", templatesFoldout), new XElement("keyBindings", keyBindingsFoldout), new XElement("brush", brushFoldout), new XElement("slopes", slopesFoldout), new XElement("randomizers", randomizersFoldout), new XElement("overlapFilter", overlapFilterFoldout), new XElement("additiveScale", additiveScaleFoldout), new XElement("optimization", optimizationFoldout)), new XElement("globalPaintingMode", new XElement("enabled", globalPaintingMode), new XElement("layerMask", layerMask.Select((bool layer, int index) => new XElement("layer", new XAttribute("index", index), layer))))));
			xDocument.Save(filePath);
			return xDocument;
		}

		public bool LoadTemplate(string filePath)
		{
			if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath))
			{
				Debug.LogError("MeshBrush: the specified template file path is invalid or does not exist! Cancelling loading procedure...");
				return false;
			}
			XDocument xDocument = XDocument.Load(filePath);
			if (xDocument == null)
			{
				Debug.LogError("MeshBrush: the specified template file couldn't be loaded.");
				return false;
			}
			foreach (XElement item in xDocument.Root.Elements())
			{
				switch (item.Name.LocalName)
				{
				case "instance":
					foreach (XElement item2 in item.Elements())
					{
						switch (item2.Name.LocalName)
						{
						case "active":
							active = string.CompareOrdinal(item2.Value, "true") == 0;
							break;
						case "name":
							groupName = item2.Value;
							break;
						case "stats":
							stats = string.CompareOrdinal(item2.Value, "true") == 0;
							break;
						case "lockSceneView":
							lockSceneView = string.CompareOrdinal(item2.Value, "true") == 0;
							break;
						}
					}
					break;
				case "meshes":
					foreach (XElement item3 in item.Elements())
					{
						string localName = item3.Name.LocalName;
						if (localName == "ui")
						{
							classicUI = string.CompareOrdinal(item3.Element("style").Value, "classic") == 0;
							previewIconSize = float.Parse(item3.Element("iconSize").Value);
						}
					}
					break;
				case "keyBindings":
					foreach (XElement item4 in item.Elements())
					{
						switch (item4.Name.LocalName)
						{
						case "paint":
							paintKey = (KeyCode)Enum.Parse(typeof(KeyCode), item4.Value);
							break;
						case "delete":
							deleteKey = (KeyCode)Enum.Parse(typeof(KeyCode), item4.Value);
							break;
						case "combine":
							combineKey = (KeyCode)Enum.Parse(typeof(KeyCode), item4.Value);
							break;
						case "randomize":
							randomizeKey = (KeyCode)Enum.Parse(typeof(KeyCode), item4.Value);
							break;
						case "increaseRadius":
							increaseRadiusKey = (KeyCode)Enum.Parse(typeof(KeyCode), item4.Value);
							break;
						case "decreaseRadius":
							decreaseRadiusKey = (KeyCode)Enum.Parse(typeof(KeyCode), item4.Value);
							break;
						}
					}
					break;
				case "brush":
					foreach (XElement item5 in item.Elements())
					{
						switch (item5.Name.LocalName)
						{
						case "radius":
							radius = float.Parse(item5.Value);
							break;
						case "color":
							color = new Color(float.Parse(item5.Element("r").Value), float.Parse(item5.Element("g").Value), float.Parse(item5.Element("b").Value), float.Parse(item5.Element("a").Value));
							break;
						case "quantity":
							quantityRange = new Vector2(float.Parse(item5.Element("min").Value), float.Parse(item5.Element("max").Value));
							break;
						case "useDensity":
							useDensity = string.CompareOrdinal(item5.Value, "true") == 0;
							break;
						case "density":
							densityRange = new Vector2(float.Parse(item5.Element("min").Value), float.Parse(item5.Element("max").Value));
							break;
						case "offset":
							offsetRange = new Vector2(float.Parse(item5.Element("min").Value), float.Parse(item5.Element("max").Value));
							break;
						case "scattering":
							scatteringRange = new Vector2(float.Parse(item5.Element("min").Value), float.Parse(item5.Element("max").Value));
							break;
						case "delay":
							delay = float.Parse(item5.Value);
							break;
						case "yAxisTangent":
							yAxisTangent = string.CompareOrdinal(item5.Value, "true") == 0;
							break;
						case "strokeAlignment":
							strokeAlignment = string.CompareOrdinal(item5.Value, "true") == 0;
							break;
						}
					}
					break;
				case "slopes":
					foreach (XElement item6 in item.Descendants())
					{
						switch (item6.Name.LocalName)
						{
						case "slopeInfluence":
							slopeInfluenceRange = new Vector2(float.Parse(item6.Element("min").Value), float.Parse(item6.Element("max").Value));
							break;
						case "enabled":
							useSlopeFilter = string.CompareOrdinal(item6.Value, "true") == 0;
							break;
						case "inverse":
							inverseSlopeFilter = string.CompareOrdinal(item6.Value, "true") == 0;
							break;
						case "angleThreshold":
							angleThresholdRange = new Vector2(float.Parse(item6.Element("min").Value), float.Parse(item6.Element("max").Value));
							break;
						case "manualReferenceVectorSampling":
							manualReferenceVectorSampling = string.CompareOrdinal(item6.Value, "true") == 0;
							break;
						case "showReferenceVectorInSceneView":
							showReferenceVectorInSceneView = string.CompareOrdinal(item6.Value, "true") == 0;
							break;
						case "referenceVector":
							slopeReferenceVector = new Vector3(float.Parse(item6.Element("x").Value), float.Parse(item6.Element("y").Value), float.Parse(item6.Element("z").Value));
							break;
						case "referenceVectorSampleLocation":
							slopeReferenceVectorSampleLocation = new Vector3(float.Parse(item6.Element("x").Value), float.Parse(item6.Element("y").Value), float.Parse(item6.Element("z").Value));
							break;
						}
					}
					break;
				case "randomizers":
					foreach (XElement item7 in item.Elements())
					{
						switch (item7.Name.LocalName)
						{
						case "scale":
							foreach (XElement item8 in item7.Descendants())
							{
								switch (item8.Name.LocalName)
								{
								case "scaleUniformly":
									uniformRandomScale = string.CompareOrdinal(item8.Value, "true") == 0;
									break;
								case "uniform":
									randomScaleRange = new Vector2(float.Parse(item8.Element("min").Value), float.Parse(item8.Element("max").Value));
									break;
								case "x":
									randomScaleRangeX = new Vector2(float.Parse(item8.Element("min").Value), float.Parse(item8.Element("max").Value));
									break;
								case "y":
									randomScaleRangeY = new Vector2(float.Parse(item8.Element("min").Value), float.Parse(item8.Element("max").Value));
									break;
								case "z":
									randomScaleRangeZ = new Vector2(float.Parse(item8.Element("min").Value), float.Parse(item8.Element("max").Value));
									break;
								case "variation":
									randomScaleCurveVariation = float.Parse(item8.Value);
									break;
								case "keys":
									randomScaleCurve = new AnimationCurve((from key in item8.Descendants("key")
										select new Keyframe(float.Parse(key.Element("time").Value), float.Parse(key.Element("value").Value), float.Parse(key.Element("inTangent").Value), float.Parse(key.Element("outTangent").Value))).ToArray());
									break;
								}
							}
							break;
						case "rotation":
							if (string.CompareOrdinal(item7.Parent.Name.LocalName, "randomizerBrush") != 0)
							{
								randomRotationRange = new Vector2(float.Parse(item7.Element("min").Value), float.Parse(item7.Element("max").Value));
							}
							break;
						case "randomizerBrush":
						{
							XElement xElement = item7.Element("position");
							if (xElement != null)
							{
								positionBrushRandomizer = string.CompareOrdinal(xElement.Value, "true") == 0;
							}
							xElement = item7.Element("rotation");
							if (xElement != null)
							{
								rotationBrushRandomizer = string.CompareOrdinal(xElement.Value, "true") == 0;
							}
							xElement = item7.Element("scale");
							if (xElement != null)
							{
								scaleBrushRandomizer = string.CompareOrdinal(xElement.Value, "true") == 0;
							}
							break;
						}
						}
					}
					break;
				case "overlapFilter":
					foreach (XElement item9 in item.Elements())
					{
						string localName = item9.Name.LocalName;
						if (!(localName == "enabled"))
						{
							if (localName == "minimumAbsoluteDistance")
							{
								minimumAbsoluteDistanceRange = new Vector2(float.Parse(item9.Element("min").Value), float.Parse(item9.Element("max").Value));
							}
						}
						else
						{
							useOverlapFilter = string.CompareOrdinal(item9.Value, "true") == 0;
						}
					}
					break;
				case "additiveScale":
					foreach (XElement item10 in item.Elements())
					{
						switch (item10.Name.LocalName)
						{
						case "scaleUniformly":
							uniformAdditiveScale = string.CompareOrdinal(item10.Value, "true") == 0;
							break;
						case "uniform":
							additiveScaleRange = new Vector2(float.Parse(item10.Element("min").Value), float.Parse(item10.Element("max").Value));
							break;
						case "nonUniform":
							additiveScaleNonUniform = new Vector3(float.Parse(item10.Element("x").Value), float.Parse(item10.Element("y").Value), float.Parse(item10.Element("z").Value));
							break;
						}
					}
					break;
				case "optimization":
					foreach (XElement item11 in item.Elements())
					{
						switch (item11.Name.LocalName)
						{
						case "autoIgnoreRaycast":
							autoIgnoreRaycast = string.CompareOrdinal(item11.Value, "true") == 0;
							break;
						case "autoSelectOnCombine":
							autoSelectOnCombine = string.CompareOrdinal(item11.Value, "true") == 0;
							break;
						case "autoStatic":
							autoStatic = string.CompareOrdinal(item11.Value, "true") == 0;
							break;
						}
					}
					break;
				case "rangeLimits":
					foreach (XElement item12 in item.Elements())
					{
						switch (item12.Name.LocalName)
						{
						case "quantity":
							maxQuantityLimit = int.Parse(item12.Element("max").Value);
							break;
						case "density":
							maxDensityLimit = float.Parse(item12.Element("max").Value);
							break;
						case "offset":
							minOffsetLimit = float.Parse(item12.Element("min").Value);
							maxOffsetLimit = float.Parse(item12.Element("max").Value);
							break;
						case "delay":
							maxDelayLimit = float.Parse(item12.Element("max").Value);
							break;
						case "minimumAbsoluteDistance":
							maxMinimumAbsoluteDistanceLimit = float.Parse(item12.Element("max").Value);
							break;
						case "randomScale":
							maxRandomScaleLimit = float.Parse(item12.Element("max").Value);
							break;
						case "additiveScale":
							maxAdditiveScaleLimit = float.Parse(item12.Element("max").Value);
							break;
						}
					}
					break;
				case "inspectorFoldouts":
					foreach (XElement item13 in item.Elements())
					{
						switch (item13.Name.LocalName)
						{
						case "help":
							helpFoldout = string.CompareOrdinal(item13.Value, "true") == 0;
							break;
						case "templatesHelp":
							helpTemplatesFoldout = string.CompareOrdinal(item13.Value, "true") == 0;
							break;
						case "generalUsageHelp":
							helpGeneralUsageFoldout = string.CompareOrdinal(item13.Value, "true") == 0;
							break;
						case "optimizationHelp":
							helpOptimizationFoldout = string.CompareOrdinal(item13.Value, "true") == 0;
							break;
						case "meshes":
							meshesFoldout = string.CompareOrdinal(item13.Value, "true") == 0;
							break;
						case "templates":
							templatesFoldout = string.CompareOrdinal(item13.Value, "true") == 0;
							break;
						case "keyBindings":
							keyBindingsFoldout = string.CompareOrdinal(item13.Value, "true") == 0;
							break;
						case "brush":
							brushFoldout = string.CompareOrdinal(item13.Value, "true") == 0;
							break;
						case "slopes":
							slopesFoldout = string.CompareOrdinal(item13.Value, "true") == 0;
							break;
						case "randomizers":
							randomizersFoldout = string.CompareOrdinal(item13.Value, "true") == 0;
							break;
						case "overlapFilter":
							overlapFilterFoldout = string.CompareOrdinal(item13.Value, "true") == 0;
							break;
						case "additiveScale":
							additiveScaleFoldout = string.CompareOrdinal(item13.Value, "true") == 0;
							break;
						case "optimization":
							optimizationFoldout = string.CompareOrdinal(item13.Value, "true") == 0;
							break;
						}
					}
					break;
				case "globalPaintingMode":
					globalPaintingMode = string.CompareOrdinal(item.Element("enabled").Value, "true") == 0;
					layerMask = (from layerElement in item.Descendants("layer")
						select string.CompareOrdinal(layerElement.Value, "false") != 0).ToArray();
					break;
				}
			}
			return true;
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;

namespace MagicaCloth2
{
	[Serializable]
	public class ClothSerializeData : IDataValidate, IValid, ITransform
	{
		public enum PaintMode
		{
			Manual = 0,
			[InspectorName("Texture Fixed(RD) Move(GR) Ignore(BK)")]
			Texture_Fixed_Move = 1,
			[InspectorName("Texture Fixed(RD) Move(GR) Limit(BL) Ignore(BK)")]
			Texture_Fixed_Move_Limit = 2
		}

		private class TempBuffer
		{
			private ClothProcess.ClothType clothType;

			private List<Renderer> sourceRenderers;

			private ClothMeshWriteMode meshWriteMode;

			private PaintMode paintMode;

			private List<Texture2D> paintMaps;

			private List<Transform> rootBones;

			private RenderSetupData.BoneConnectionMode connectionMode;

			private float rotationalInterpolation;

			private float rootRotation;

			private ClothUpdateMode updateMode;

			private float animationPoseRatio;

			private ReductionSettings reductionSetting;

			private CustomSkinningSettings customSkinningSetting;

			private NormalAlignmentSettings normalAlignmentSetting;

			private ClothNormalAxis normalAxis;

			private List<ColliderComponent> colliderList;

			private List<Transform> collisionBones;

			private MagicaCloth synchronization;

			private float stablizationTimeAfterReset;

			private float blendWeight;

			private CullingSettings cullingSetting;

			private Transform anchor;

			private float anchorInertia;

			internal TempBuffer(ClothSerializeData sdata)
			{
				Push(sdata);
			}

			internal void Push(ClothSerializeData sdata)
			{
				clothType = sdata.clothType;
				sourceRenderers = new List<Renderer>(sdata.sourceRenderers);
				meshWriteMode = sdata.meshWriteMode;
				paintMode = sdata.paintMode;
				paintMaps = new List<Texture2D>(sdata.paintMaps);
				rootBones = new List<Transform>(sdata.rootBones);
				connectionMode = sdata.connectionMode;
				rotationalInterpolation = sdata.rotationalInterpolation;
				rootRotation = sdata.rootRotation;
				updateMode = sdata.updateMode;
				animationPoseRatio = sdata.animationPoseRatio;
				reductionSetting = sdata.reductionSetting.Clone();
				customSkinningSetting = sdata.customSkinningSetting.Clone();
				normalAlignmentSetting = sdata.normalAlignmentSetting.Clone();
				normalAxis = sdata.normalAxis;
				colliderList = new List<ColliderComponent>(sdata.colliderCollisionConstraint.colliderList);
				collisionBones = new List<Transform>(sdata.colliderCollisionConstraint.collisionBones);
				synchronization = sdata.selfCollisionConstraint.syncPartner;
				stablizationTimeAfterReset = sdata.stablizationTimeAfterReset;
				blendWeight = sdata.blendWeight;
				cullingSetting = sdata.cullingSettings.Clone();
				anchor = sdata.inertiaConstraint.anchor;
				anchorInertia = sdata.inertiaConstraint.anchorInertia;
			}

			internal void Pop(ClothSerializeData sdata)
			{
				sdata.clothType = clothType;
				sdata.sourceRenderers = sourceRenderers;
				sdata.meshWriteMode = meshWriteMode;
				sdata.paintMode = paintMode;
				sdata.paintMaps = paintMaps;
				sdata.rootBones = rootBones;
				sdata.connectionMode = connectionMode;
				sdata.rotationalInterpolation = rotationalInterpolation;
				sdata.rootRotation = rootRotation;
				sdata.updateMode = updateMode;
				sdata.animationPoseRatio = animationPoseRatio;
				sdata.reductionSetting = reductionSetting;
				sdata.customSkinningSetting = customSkinningSetting;
				sdata.normalAlignmentSetting = normalAlignmentSetting;
				sdata.normalAxis = normalAxis;
				sdata.colliderCollisionConstraint.colliderList = colliderList;
				sdata.colliderCollisionConstraint.collisionBones = collisionBones;
				sdata.selfCollisionConstraint.syncPartner = synchronization;
				sdata.stablizationTimeAfterReset = stablizationTimeAfterReset;
				sdata.blendWeight = blendWeight;
				sdata.cullingSettings = cullingSetting;
				sdata.inertiaConstraint.anchor = anchor;
				sdata.inertiaConstraint.anchorInertia = anchorInertia;
			}
		}

		public ClothProcess.ClothType clothType;

		public List<Renderer> sourceRenderers = new List<Renderer>();

		public ClothMeshWriteMode meshWriteMode;

		public PaintMode paintMode;

		public List<Texture2D> paintMaps = new List<Texture2D>();

		public List<Transform> rootBones = new List<Transform>();

		public RenderSetupData.BoneConnectionMode connectionMode;

		[Range(0f, 1f)]
		public float rotationalInterpolation = 0.5f;

		[Range(0f, 1f)]
		public float rootRotation = 0.5f;

		public ClothUpdateMode updateMode = ClothUpdateMode.AnimatorLinkage;

		[Range(0f, 1f)]
		public float animationPoseRatio;

		public ReductionSettings reductionSetting = new ReductionSettings();

		public CustomSkinningSettings customSkinningSetting = new CustomSkinningSettings();

		public NormalAlignmentSettings normalAlignmentSetting = new NormalAlignmentSettings();

		public CullingSettings cullingSettings = new CullingSettings();

		public ClothNormalAxis normalAxis = ClothNormalAxis.Up;

		[Range(0f, 10f)]
		public float gravity = 5f;

		public float3 gravityDirection = new float3(0f, -1f, 0f);

		[Range(0f, 1f)]
		public float gravityFalloff;

		[Range(0f, 1f)]
		public float stablizationTimeAfterReset = 0.1f;

		[Range(0f, 1f)]
		public float blendWeight = 1f;

		public CurveSerializeData damping = new CurveSerializeData(0.05f);

		public CurveSerializeData radius = new CurveSerializeData(0.02f);

		public InertiaConstraint.SerializeData inertiaConstraint = new InertiaConstraint.SerializeData();

		public TetherConstraint.SerializeData tetherConstraint = new TetherConstraint.SerializeData();

		public DistanceConstraint.SerializeData distanceConstraint = new DistanceConstraint.SerializeData();

		public TriangleBendingConstraint.SerializeData triangleBendingConstraint = new TriangleBendingConstraint.SerializeData();

		public AngleConstraint.RestorationSerializeData angleRestorationConstraint = new AngleConstraint.RestorationSerializeData();

		public AngleConstraint.LimitSerializeData angleLimitConstraint = new AngleConstraint.LimitSerializeData();

		public MotionConstraint.SerializeData motionConstraint = new MotionConstraint.SerializeData();

		public ColliderCollisionConstraint.SerializeData colliderCollisionConstraint = new ColliderCollisionConstraint.SerializeData();

		public SelfCollisionConstraint.SerializeData selfCollisionConstraint = new SelfCollisionConstraint.SerializeData();

		public WindSettings wind = new WindSettings();

		public SpringConstraint.SerializeData springConstraint = new SpringConstraint.SerializeData();

		private ResultCode verificationResult;

		public Define.Result VerificationResult => verificationResult.Result;

		public bool IsValid()
		{
			verificationResult.SetResult(Define.Result.Empty);
			switch (clothType)
			{
			case ClothProcess.ClothType.BoneCloth:
			case ClothProcess.ClothType.BoneSpring:
				if (rootBones == null || rootBones.Count == 0)
				{
					return false;
				}
				if (rootBones.Count((Transform x) => x != null) == 0)
				{
					return false;
				}
				if (rootBones.Distinct().Count() != rootBones.Count)
				{
					verificationResult.SetError(Define.Result.SerializeData_DuplicateRootBone);
					return false;
				}
				break;
			case ClothProcess.ClothType.MeshCloth:
				if (sourceRenderers == null || sourceRenderers.Count == 0)
				{
					return false;
				}
				if (sourceRenderers.Count((Renderer x) => x != null) == 0)
				{
					return false;
				}
				if (sourceRenderers.Count > 31)
				{
					verificationResult.SetError(Define.Result.SerializeData_Over31Renderers);
					return false;
				}
				if (sourceRenderers.Distinct().Count() != sourceRenderers.Count)
				{
					verificationResult.SetError(Define.Result.SerializeData_DuplicateRenderer);
					return false;
				}
				break;
			default:
				return false;
			}
			verificationResult.SetSuccess();
			return true;
		}

		public void DataValidate()
		{
			rotationalInterpolation = Mathf.Clamp01(rotationalInterpolation);
			rootRotation = Mathf.Clamp01(rootRotation);
			animationPoseRatio = Mathf.Clamp01(animationPoseRatio);
			reductionSetting.DataValidate();
			customSkinningSetting.DataValidate();
			normalAlignmentSetting.DataValidate();
			cullingSettings.DataValidate();
			gravity = Mathf.Clamp(gravity, 0f, 20f);
			if (math.length(gravityDirection) > 1E-08f)
			{
				gravityDirection = math.normalize(gravityDirection);
			}
			else
			{
				gravityDirection = 0;
			}
			gravityFalloff = Mathf.Clamp01(gravityFalloff);
			stablizationTimeAfterReset = Mathf.Clamp01(stablizationTimeAfterReset);
			blendWeight = Mathf.Clamp01(blendWeight);
			damping.DataValidate(0f, 1f);
			radius.DataValidate(0.001f, 1f);
			inertiaConstraint.DataValidate();
			tetherConstraint.DataValidate();
			distanceConstraint.DataValidate();
			triangleBendingConstraint.DataValidate();
			angleRestorationConstraint.DataValidate();
			angleLimitConstraint.DataValidate();
			motionConstraint.DataValidate();
			colliderCollisionConstraint.DataValidate();
			selfCollisionConstraint.DataValidate();
			wind.DataValidate();
		}

		public override int GetHashCode()
		{
			int num = 0;
			num = (int)(num + clothType);
			foreach (Renderer sourceRenderer in sourceRenderers)
			{
				num += sourceRenderer?.GetInstanceID() ?? (-3910836);
			}
			foreach (Transform rootBone in rootBones)
			{
				Stack<Transform> stack = new Stack<Transform>(30);
				stack.Push(rootBone);
				while (stack.Count > 0)
				{
					Transform transform = stack.Pop();
					if (transform == null)
					{
						num += -3910836;
						continue;
					}
					num += transform.GetInstanceID();
					num += transform.localPosition.GetHashCode();
					num += transform.localRotation.GetHashCode();
					int childCount = transform.childCount;
					for (int i = 0; i < childCount; i++)
					{
						stack.Push(transform.GetChild(i));
					}
				}
			}
			num += (int)connectionMode * 10;
			num += reductionSetting.GetHashCode();
			num += customSkinningSetting.GetHashCode();
			num += normalAlignmentSetting.GetHashCode();
			num += cullingSettings.GetHashCode();
			num = (int)(num + paintMode);
			foreach (Texture2D paintMap in paintMaps)
			{
				if ((bool)paintMap)
				{
					num += paintMap.GetInstanceID();
					num += (paintMap.isReadable ? 1 : 0);
				}
			}
			return num + colliderCollisionConstraint.GetHashCode();
		}

		public ClothParameters GetClothParameters()
		{
			ClothParameters result = default(ClothParameters);
			result.gravity = ((clothType == ClothProcess.ClothType.BoneSpring) ? 0f : gravity);
			result.worldGravityDirection = gravityDirection;
			result.gravityFalloff = gravityFalloff;
			result.stablizationTimeAfterReset = stablizationTimeAfterReset;
			result.blendWeight = blendWeight;
			result.dampingCurveData = damping.ConvertFloatArray() * 0.2f;
			result.radiusCurveData = radius.ConvertFloatArray();
			result.normalAxis = normalAxis;
			result.rotationalInterpolation = rotationalInterpolation;
			result.rootRotation = rootRotation;
			result.culling.Convert(cullingSettings);
			result.inertiaConstraint.Convert(inertiaConstraint);
			result.tetherConstraint.Convert(tetherConstraint, clothType);
			result.distanceConstraint.Convert(distanceConstraint, clothType);
			result.triangleBendingConstraint.Convert(triangleBendingConstraint);
			result.angleConstraint.Convert(angleRestorationConstraint, angleLimitConstraint);
			result.motionConstraint.Convert(motionConstraint, clothType);
			result.colliderCollisionConstraint.Convert(colliderCollisionConstraint, clothType);
			result.selfCollisionConstraint.Convert(selfCollisionConstraint, clothType);
			result.wind.Convert(wind, clothType);
			result.springConstraint.Convert(springConstraint, clothType);
			return result;
		}

		public string ExportJson()
		{
			return JsonUtility.ToJson(this);
		}

		public bool ImportJson(string json)
		{
			try
			{
				TempBuffer tempBuffer = new TempBuffer(this);
				JsonUtility.FromJsonOverwrite(json, this);
				tempBuffer.Pop(this);
				DataValidate();
				return true;
			}
			catch (Exception exception)
			{
				Debug.LogException(exception);
				return false;
			}
		}

		public void Import(ClothSerializeData sdata, bool deepCopy = false)
		{
			TempBuffer tempBuffer = (deepCopy ? null : new TempBuffer(this));
			if (deepCopy)
			{
				clothType = sdata.clothType;
				sourceRenderers = new List<Renderer>(sdata.sourceRenderers);
				paintMode = sdata.paintMode;
				paintMaps = new List<Texture2D>(sdata.paintMaps);
				rootBones = new List<Transform>(sdata.rootBones);
				connectionMode = sdata.connectionMode;
				rotationalInterpolation = sdata.rotationalInterpolation;
				rootRotation = sdata.rootRotation;
				updateMode = sdata.updateMode;
				animationPoseRatio = sdata.animationPoseRatio;
				reductionSetting = sdata.reductionSetting.Clone();
				customSkinningSetting = sdata.customSkinningSetting.Clone();
				normalAlignmentSetting = sdata.normalAlignmentSetting.Clone();
				normalAxis = sdata.normalAxis;
				stablizationTimeAfterReset = sdata.stablizationTimeAfterReset;
				blendWeight = sdata.blendWeight;
				cullingSettings = sdata.cullingSettings.Clone();
			}
			gravity = sdata.gravity;
			gravityDirection = sdata.gravityDirection;
			gravityFalloff = sdata.gravityFalloff;
			damping = sdata.damping.Clone();
			radius = sdata.radius.Clone();
			inertiaConstraint = sdata.inertiaConstraint.Clone();
			tetherConstraint = sdata.tetherConstraint.Clone();
			distanceConstraint = sdata.distanceConstraint.Clone();
			triangleBendingConstraint = sdata.triangleBendingConstraint.Clone();
			angleRestorationConstraint = sdata.angleRestorationConstraint.Clone();
			angleLimitConstraint = sdata.angleLimitConstraint.Clone();
			motionConstraint = sdata.motionConstraint.Clone();
			colliderCollisionConstraint = sdata.colliderCollisionConstraint.Clone();
			selfCollisionConstraint = sdata.selfCollisionConstraint.Clone();
			wind = sdata.wind.Clone();
			if (!deepCopy)
			{
				tempBuffer.Pop(this);
			}
		}

		public void Import(MagicaCloth src, bool deepCopy = false)
		{
			Import(src.SerializeData, deepCopy);
		}

		public void GetUsedTransform(HashSet<Transform> transformSet)
		{
			foreach (Transform rootBone in rootBones)
			{
				if ((bool)rootBone)
				{
					transformSet.Add(rootBone);
				}
			}
			customSkinningSetting.GetUsedTransform(transformSet);
			normalAlignmentSetting.GetUsedTransform(transformSet);
			colliderCollisionConstraint.GetUsedTransform(transformSet);
		}

		public void ReplaceTransform(Dictionary<int, Transform> replaceDict)
		{
			for (int i = 0; i < rootBones.Count; i++)
			{
				Transform transform = rootBones[i];
				if ((bool)transform && replaceDict.ContainsKey(transform.GetInstanceID()))
				{
					rootBones[i] = replaceDict[transform.GetInstanceID()];
				}
			}
			customSkinningSetting.ReplaceTransform(replaceDict);
			normalAlignmentSetting.ReplaceTransform(replaceDict);
			colliderCollisionConstraint.ReplaceTransform(replaceDict);
		}

		public bool IsBoneSpring()
		{
			return clothType == ClothProcess.ClothType.BoneSpring;
		}
	}
}

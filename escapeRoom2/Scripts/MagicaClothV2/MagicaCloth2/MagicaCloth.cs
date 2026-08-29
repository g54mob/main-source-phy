using System;
using System.Collections.Generic;
using UnityEngine;

namespace MagicaCloth2
{
	[AddComponentMenu("MagicaCloth2/MagicaCloth")]
	[HelpURL("https://magicasoft.jp/en/mc2_magicaclothcomponent/")]
	public class MagicaCloth : ClothBehaviour, IValid
	{
		[SerializeField]
		private ClothSerializeData serializeData = new ClothSerializeData();

		[SerializeField]
		internal ClothSerializeData2 serializeData2 = new ClothSerializeData2();

		private ClothProcess process = new ClothProcess();

		[HideInInspector]
		public float animationPoseRatioProperty;

		private float _animationPoseRatioProperty;

		[HideInInspector]
		public float gravityProperty;

		private float _gravityProperty;

		[HideInInspector]
		public float dampingProperty;

		private float _dampingProperty;

		[HideInInspector]
		public float worldInertiaProperty;

		private float _worldInertiaProperty;

		[HideInInspector]
		public float localInertiaProperty;

		private float _localInertiaProperty;

		[HideInInspector]
		public float windInfluenceProperty;

		private float _windInfluenceProperty;

		[HideInInspector]
		public float blendWeightProperty;

		private float _blendWeightProperty;

		public Action<MagicaCloth, bool> OnBuildComplete;

		public Action<MagicaCloth, Renderer, bool> OnRendererMeshChange;

		public ClothSerializeData SerializeData => serializeData;

		public ClothProcess Process
		{
			get
			{
				process.cloth = this;
				return process;
			}
		}

		public Transform ClothTransform => base.transform;

		public MagicaCloth SyncPartnerCloth
		{
			get
			{
				MagicaCloth magicaCloth = (SerializeData.IsBoneSpring() ? null : SerializeData.selfCollisionConstraint.GetSyncPartner());
				if (!(magicaCloth == this))
				{
					return magicaCloth;
				}
				return null;
			}
		}

		public bool IsValid()
		{
			if (MagicaManager.IsPlaying() && Process.IsValid())
			{
				return Process.TeamId > 0;
			}
			return false;
		}

		private void Reset()
		{
		}

		private void OnValidate()
		{
			Process.DataUpdate();
		}

		private void Awake()
		{
			if (MagicaManager.initializationLocation == MagicaManager.InitializationLocation.Awake)
			{
				Process.Init();
				MagicaManager.Team.RemoveMonitoringProcess(Process);
			}
		}

		private void OnEnable()
		{
			Process.StartUse();
		}

		private void OnDisable()
		{
			Process.EndUse();
		}

		private void Start()
		{
			if (MagicaManager.initializationLocation == MagicaManager.InitializationLocation.Start)
			{
				Process.Init();
				MagicaManager.Team.RemoveMonitoringProcess(Process);
			}
			Process.AutoBuild();
		}

		private void OnDestroy()
		{
			Process.Dispose();
		}

		public override int GetMagicaHashCode()
		{
			return SerializeData.GetHashCode() + serializeData2.GetHashCode() + (base.isActiveAndEnabled ? GetInstanceID() : 0);
		}

		internal void InitAnimationProperty()
		{
			animationPoseRatioProperty = serializeData.animationPoseRatio;
			_animationPoseRatioProperty = animationPoseRatioProperty;
			gravityProperty = serializeData.gravity;
			_gravityProperty = gravityProperty;
			dampingProperty = serializeData.damping.value;
			_dampingProperty = dampingProperty;
			worldInertiaProperty = serializeData.inertiaConstraint.worldInertia;
			_worldInertiaProperty = worldInertiaProperty;
			localInertiaProperty = serializeData.inertiaConstraint.localInertia;
			_localInertiaProperty = localInertiaProperty;
			windInfluenceProperty = serializeData.wind.influence;
			_windInfluenceProperty = windInfluenceProperty;
			blendWeightProperty = serializeData.blendWeight;
			_blendWeightProperty = blendWeightProperty;
		}

		private void OnDidApplyAnimationProperties()
		{
			if (Application.isPlaying)
			{
				if (animationPoseRatioProperty != _animationPoseRatioProperty)
				{
					_animationPoseRatioProperty = animationPoseRatioProperty;
					serializeData.animationPoseRatio = animationPoseRatioProperty;
					SetParameterChange();
				}
				if (gravityProperty != _gravityProperty)
				{
					_gravityProperty = gravityProperty;
					serializeData.gravity = gravityProperty;
					SetParameterChange();
				}
				if (dampingProperty != _dampingProperty)
				{
					_dampingProperty = dampingProperty;
					serializeData.damping.value = dampingProperty;
					SetParameterChange();
				}
				if (worldInertiaProperty != _worldInertiaProperty)
				{
					_worldInertiaProperty = worldInertiaProperty;
					serializeData.inertiaConstraint.worldInertia = worldInertiaProperty;
					SetParameterChange();
				}
				if (localInertiaProperty != _localInertiaProperty)
				{
					_localInertiaProperty = localInertiaProperty;
					serializeData.inertiaConstraint.localInertia = localInertiaProperty;
					SetParameterChange();
				}
				if (windInfluenceProperty != _windInfluenceProperty)
				{
					_windInfluenceProperty = windInfluenceProperty;
					serializeData.wind.influence = windInfluenceProperty;
					SetParameterChange();
				}
				if (blendWeightProperty != _blendWeightProperty)
				{
					_blendWeightProperty = blendWeightProperty;
					serializeData.blendWeight = blendWeightProperty;
					SetParameterChange();
				}
			}
		}

		public ClothSerializeData2 GetSerializeData2()
		{
			return serializeData2;
		}

		public void Initialize()
		{
			if (Application.isPlaying)
			{
				Process.Init();
			}
		}

		public void DisableAutoBuild()
		{
			if (Application.isPlaying)
			{
				Process.SetState(7, sw: true);
			}
		}

		public bool BuildAndRun()
		{
			bool flag = false;
			bool flag2 = true;
			try
			{
				if (!Application.isPlaying)
				{
					throw new MagicaClothProcessingException();
				}
				DisableAutoBuild();
				if (Process.IsState(5))
				{
					Develop.LogError((object)("Already built.:" + base.name));
					throw new MagicaClothProcessingException();
				}
				if (!Process.GenerateInitialization())
				{
					throw new MagicaClothProcessingException();
				}
				if (!serializeData2.preBuildData.UsePreBuild())
				{
					ClothProcess.ClothType clothType = serializeData.clothType;
					if (clothType == ClothProcess.ClothType.BoneCloth || clothType == ClothProcess.ClothType.BoneSpring)
					{
						SelectionData selectionData = serializeData2.selectionData;
						if ((selectionData == null || !selectionData.IsValid() || !selectionData.IsUserEdit()) && !Process.GenerateBoneClothSelection())
						{
							throw new MagicaClothProcessingException();
						}
					}
					flag = Process.StartRuntimeBuild();
					if (flag)
					{
						flag2 = false;
					}
				}
				else
				{
					flag = Process.PreBuildDataConstruction();
				}
			}
			catch (MagicaClothProcessingException)
			{
			}
			catch (Exception exception)
			{
				Debug.LogException(exception);
			}
			finally
			{
				if (flag2)
				{
					OnBuildComplete?.Invoke(this, flag);
				}
			}
			return flag;
		}

		public void ReplaceTransform(Dictionary<string, Transform> targetTransformDict)
		{
			HashSet<Transform> hashSet = new HashSet<Transform>();
			Process.GetUsedTransform(hashSet);
			Dictionary<int, Transform> dictionary = new Dictionary<int, Transform>();
			foreach (Transform item in hashSet)
			{
				if ((bool)item && targetTransformDict.ContainsKey(item.name))
				{
					dictionary.Add(item.GetInstanceID(), targetTransformDict[item.name]);
				}
			}
			Process.ReplaceTransform(dictionary);
		}

		public void SetParameterChange()
		{
			if (IsValid())
			{
				Process.DataUpdate();
			}
		}

		public void SetTimeScale(float timeScale)
		{
			if (IsValid())
			{
				MagicaManager.Team.GetTeamDataRef(Process.TeamId).timeScale = Mathf.Clamp01(timeScale);
			}
		}

		public float GetTimeScale()
		{
			if (IsValid())
			{
				return MagicaManager.Team.GetTeamDataRef(Process.TeamId).timeScale;
			}
			return 1f;
		}

		public void ResetCloth(bool keepPose = false)
		{
			if (IsValid())
			{
				ref TeamManager.TeamData teamDataRef = ref MagicaManager.Team.GetTeamDataRef(Process.TeamId);
				if (keepPose)
				{
					teamDataRef.flag.SetBits(9, value: true);
					return;
				}
				teamDataRef.flag.SetBits(2, value: true);
				teamDataRef.flag.SetBits(3, value: true);
				teamDataRef.flag.SetBits(12, value: false);
				Process.SetState(9, sw: false);
				Process.UpdateRendererUse();
			}
		}

		public Vector3 GetCenterPosition()
		{
			if (IsValid())
			{
				ref InertiaConstraint.CenterData centerDataRef = ref MagicaManager.Team.GetCenterDataRef(Process.TeamId);
				return ClothTransform.TransformPoint(centerDataRef.frameLocalPosition);
			}
			return Vector3.zero;
		}

		public void AddForce(Vector3 forceDirection, float forceVelocity, ClothForceMode fmode = ClothForceMode.VelocityAdd)
		{
			if (IsValid() && forceDirection.magnitude > 0f && forceVelocity > 0f && fmode != ClothForceMode.None)
			{
				ref TeamManager.TeamData teamDataRef = ref MagicaManager.Team.GetTeamDataRef(Process.TeamId);
				teamDataRef.forceMode = fmode;
				teamDataRef.impactForce = forceDirection.normalized * forceVelocity;
			}
		}

		public void SetSkipWriting(bool sw)
		{
			if (IsValid())
			{
				Process.SetSkipWriting(sw);
			}
		}

		private RenderData GetRenderData(Renderer ren)
		{
			if (!IsValid() || ren == null)
			{
				return null;
			}
			int instanceID = ren.GetInstanceID();
			return MagicaManager.Render.GetRendererData(instanceID);
		}

		public Mesh GetOriginalMesh(Renderer ren)
		{
			return GetRenderData(ren)?.originalMesh ?? null;
		}

		public Mesh GetCustomMesh(Renderer ren)
		{
			return GetRenderData(ren)?.customMesh ?? null;
		}

		public List<Transform> GetCustomBones(Renderer ren)
		{
			return GetRenderData(ren)?.transformList ?? null;
		}
	}
}

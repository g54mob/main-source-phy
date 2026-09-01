using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;

namespace MTAssets.UltimateLODSystem
{
	[AddComponentMenu("MT Assets/Ultimate LOD System/Ultimate Level Of Detail")]
	[ExecuteInEditMode]
	public class UltimateLevelOfDetail : MonoBehaviour
	{
		public enum ScanMeshesMode
		{
			ScanInChildrenGameObjectsOnly = 0,
			ScanInThisGameObjectOnly = 1
		}

		public enum ForceOfSimplification
		{
			Normal = 0,
			Strong = 1,
			VeryStrong = 2,
			ExtremelyStrong = 3,
			Destroyer = 4
		}

		public enum CullingMode
		{
			Disabled = 0,
			CullingMeshes = 1,
			CullingRenderer = 2
		}

		public enum CameraDetectionMode
		{
			CurrentCamera = 0,
			MainCamera = 1,
			CustomCamera = 2
		}

		[Serializable]
		public class ScannedMeshItem
		{
			[Serializable]
			public class MeshMaterials
			{
				public Material[] materialArray;
			}

			public GameObject originalGameObject;

			public SkinnedMeshRenderer originalSkinnedMeshRenderer;

			public MeshFilter originalMeshFilter;

			public MeshRenderer originalMeshRenderer;

			public Mesh[] allMeshLods;

			public string[] allMeshLodsPaths;

			public bool canChangeMaterialsOnThisMeshLods;

			public MeshMaterials[] allMeshLodsMaterials;

			public UltimateLevelOfDetailMeshes originalMeshLodsManager;

			public Mesh beforeCullingData_lastMeshOfThis;

			public bool beforeCullingData_isForcedToRenderizationOff;

			public void InitializeAllMeshLodsMaterialsArray()
			{
			}
		}

		[CompilerGenerated]
		private sealed class _003COnRenderObject_HookEmulationForHDRP_003Ed__61 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public UltimateLevelOfDetail _003C_003E4__this;

			private WaitForEndOfFrame _003CwaitForEndOfFrame_003E5__2;

			object IEnumerator<object>.Current
			{
				[DebuggerHidden]
				get
				{
					return null;
				}
			}

			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return null;
				}
			}

			[DebuggerHidden]
			public _003COnRenderObject_HookEmulationForHDRP_003Ed__61(int _003C_003E1__state)
			{
			}

			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			private bool MoveNext()
			{
				return false;
			}

			bool IEnumerator.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				return this.MoveNext();
			}

			[DebuggerHidden]
			void IEnumerator.Reset()
			{
			}
		}

		[CompilerGenerated]
		private sealed class _003CScanForMeshesAndGenerateAllLodGroups_AsyncProcessing_003Ed__51 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public UltimateLevelOfDetail _003C_003E4__this;

			private List<MeshFilter> _003CmeshFiltersFound_003E5__2;

			private List<ScannedMeshItem> _003CscannedMeshItems_003E5__3;

			private float _003CcurrentMesh_003E5__4;

			private float _003CcurrentLod_003E5__5;

			private List<SkinnedMeshRenderer>.Enumerator _003C_003E7__wrap5;

			private SkinnedMeshRenderer _003Csmr_003E5__7;

			private long _003Cticks_003E5__8;

			private ScannedMeshItem _003CthisScannedMeshItem_003E5__9;

			private int _003Ci_003E5__10;

			private List<MeshFilter>.Enumerator _003C_003E7__wrap10;

			private MeshFilter _003Cmf_003E5__12;

			object IEnumerator<object>.Current
			{
				[DebuggerHidden]
				get
				{
					return null;
				}
			}

			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return null;
				}
			}

			[DebuggerHidden]
			public _003CScanForMeshesAndGenerateAllLodGroups_AsyncProcessing_003Ed__51(int _003C_003E1__state)
			{
			}

			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			private bool MoveNext()
			{
				return false;
			}

			bool IEnumerator.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				return this.MoveNext();
			}

			private void _003C_003Em__Finally1()
			{
			}

			private void _003C_003Em__Finally2()
			{
			}

			[DebuggerHidden]
			void IEnumerator.Reset()
			{
			}
		}

		private WaitForEndOfFrame WAIT_FOR_END_OF_FRAME;

		private Camera cacheOfMainCamera;

		private GameObject cacheOfUlodData;

		private RuntimeInstancesDetector cacheOfUlodDataRuntimeInstancesDetector;

		private float lastDistanceFromMainCamera;

		private int currentLodAccordingToDistance;

		private float currentDistanceFromMainCamera;

		private float currentRealDistanceFromMainCamera;

		private bool forcedToDisableLodsOfThisComponent;

		private int forcedToShowOnlyALodIndependentOfDistance;

		[HideInInspector]
		public List<ScannedMeshItem> currentScannedMeshesList;

		[HideInInspector]
		public ScanMeshesMode modeOfMeshesScanning;

		[HideInInspector]
		public bool scanInactiveGameObjects;

		[HideInInspector]
		public List<GameObject> gameObjectsToIgnore;

		[HideInInspector]
		public int levelsOfDetailToGenerate;

		[HideInInspector]
		public float[] percentOfVerticesForEachLod;

		[HideInInspector]
		public bool saveGeneratedLodsInAssets;

		[HideInInspector]
		public bool skinnedAnimsCompatibilityMode;

		[HideInInspector]
		public bool preventArtifacts;

		[HideInInspector]
		public bool optimizeResultingMeshes;

		[HideInInspector]
		public bool enableLightmapsSupport;

		[HideInInspector]
		public bool enableMaterialsChanges;

		[HideInInspector]
		public ForceOfSimplification forceOfSimplification;

		[HideInInspector]
		public CullingMode cullingMode;

		[HideInInspector]
		[SerializeField]
		private Transform _customPivotToSimulateLods;

		[HideInInspector]
		public CameraDetectionMode cameraDetectionMode;

		[HideInInspector]
		public bool useCacheForMainCameraInDetection;

		[HideInInspector]
		public Camera customCameraForSimulationOfLods;

		[HideInInspector]
		public float[] minDistanceOfViewForEachLod;

		[HideInInspector]
		public float minDistanceOfViewForCull;

		public UnityEvent onDoneScan;

		public UnityEvent onUndoScan;

		[HideInInspector]
		public bool forceChangeLodsOfSkinnedInEditor;

		[HideInInspector]
		public bool drawGizmoOnThisPivot;

		[HideInInspector]
		public Color colorOfGizmo;

		[HideInInspector]
		public float sizeOfGizmo;

		[HideInInspector]
		public bool forceShowHiddenSettings;

		public Transform customPivotToSimulateLods
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		private void ValidateAllParameters(bool isGoingToScan)
		{
		}

		private void CreateHierarchyOfFoldersIfNotExists()
		{
		}

		private string SaveGeneratedLodInAssets(string lodNumber, long ticks, Mesh generatedLodMesh)
		{
			return null;
		}

		private Mesh GetGeneratedLodForThisMesh(Mesh originalMesh, float percentOfVertices, bool isSkinnedMesh)
		{
			return null;
		}

		private Material[] GetCopyOfExistentArrayOfMaterials(Material[] sourceArray)
		{
			return null;
		}

		private void ScanForMeshesAndGenerateAllLodGroups_StartProcessing(bool showProgressBar)
		{
		}

		[IteratorStateMachine(typeof(_003CScanForMeshesAndGenerateAllLodGroups_AsyncProcessing_003Ed__51))]
		private IEnumerator ScanForMeshesAndGenerateAllLodGroups_AsyncProcessing(bool showProgressBar)
		{
			return null;
		}

		private void UndoAllMeshesScannedAndAllLodGroups(bool showProgressBar, bool deleteAllGeneratedMeshes, bool runMonoIl2CppGc, bool runUnityGc)
		{
		}

		private bool isLodsSimulationEnabledInThisSceneForEditorSceneViewMode()
		{
			return false;
		}

		private Camera GetLastActiveSceneViewCamera()
		{
			return null;
		}

		private void CullThisLodMeshOfRenderer(ScannedMeshItem meshItem)
		{
		}

		private void UncullThisLodMeshOfRenderer(ScannedMeshItem meshItem)
		{
		}

		private void ChangeLodMeshAndMaterialsOfRenderer(ScannedMeshItem meshItem, int lodLevel)
		{
		}

		private void CalculateCorrectLodForDistanceBeforeChange(float distance)
		{
		}

		public void OnRenderObject()
		{
		}

		public void Awake()
		{
		}

		[IteratorStateMachine(typeof(_003COnRenderObject_HookEmulationForHDRP_003Ed__61))]
		private IEnumerator OnRenderObject_HookEmulationForHDRP()
		{
			return null;
		}

		public void OnEnable()
		{
		}

		public int GetCurrentLodLevel()
		{
			return 0;
		}

		public float GetCurrentCameraDistance()
		{
			return 0f;
		}

		public float GetCurrentRealCameraDistance()
		{
			return 0f;
		}

		public int GetNumberOfLodsGenerated()
		{
			return 0;
		}

		public bool isScannedMeshesCurrentCulled()
		{
			return false;
		}

		public UltimateLevelOfDetailMeshes[] GetListOfAllMeshesScanned()
		{
			return null;
		}

		public void ForceShowLod(bool force, int level)
		{
		}

		public bool isThisComponentForcedToShowLod()
		{
			return false;
		}

		public void ForceDisableLodChangesInThisComponent(bool force)
		{
		}

		public bool isThisComponentForcedToDisableLodChanges()
		{
			return false;
		}

		public void ForceThisComponentToUpdateLodsRender()
		{
		}

		public bool isMeshesCurrentScannedAndLodsWorkingInThisComponent()
		{
			return false;
		}

		public void ScanAllMeshesAndGenerateLodsGroups()
		{
		}

		public void UndoCurrentScanWorkingAndDeleteGeneratedMeshes(bool runMonoIl2CppGc, bool runUnityGc)
		{
		}

		public UltimateLevelOfDetail[] GetListOfAllUlodsInThisScene()
		{
			return null;
		}

		public UltimateLevelOfDetailOptimizer[] GetListOfAllUlodsOptimizerInThisScene()
		{
			return null;
		}

		public void SetNewCustomCameraForThisAndAllUlodsInThisScene(Camera newCustomCamera)
		{
		}

		public void ConvertThisToDefaultUnityLods()
		{
		}
	}
}

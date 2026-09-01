using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using MTAssets.UltimateLODSystem.MeshSimplifier;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;

namespace MTAssets.UltimateLODSystem;

public class UltimateLevelOfDetail : MonoBehaviour
{
	public enum ScanMeshesMode
	{
		ScanInChildrenGameObjectsOnly,
		ScanInThisGameObjectOnly
	}

	public enum ForceOfSimplification
	{
		Normal,
		Strong,
		VeryStrong,
		ExtremelyStrong,
		Destroyer
	}

	public enum CullingMode
	{
		Disabled,
		CullingMeshes,
		CullingRenderer
	}

	public enum CameraDetectionMode
	{
		CurrentCamera,
		MainCamera,
		CustomCamera
	}

	[Serializable]
	public class ScannedMeshItem
	{
		[Serializable]
		public class MeshMaterials
		{
			public Material[] materialArray;

			public MeshMaterials()
			{
				Material[] array = new Material[0];
				materialArray = array;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803797D0");
			}
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
			//IL_013b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0140: Expected O, but got Unknown
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0034: Expected O, but got Unknown
			//IL_003d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0042: Expected O, but got Unknown
			//IL_006d: Expected O, but got I4
			//IL_00a6: Expected I, but got O
			//IL_00b6: Expected O, but got I
			//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
			//IL_00f8: Expected O, but got Unknown
			//IL_0101: Unknown result type (might be due to invalid IL or missing references)
			//IL_0106: Expected O, but got Unknown
			object obj = allMeshLodsMaterials + 32;
			bool flag = allMeshLodsMaterials == null;
			MeshMaterials meshMaterials = null;
			MeshMaterials meshMaterials2 = null;
			MeshMaterials meshMaterials3 = null;
			if (!flag)
			{
				do
				{
					if (obj == null)
					{
						meshMaterials = (MeshMaterials)(meshMaterials + 1);
						obj += 8;
						continue;
					}
					return;
				}
				while ((nint)meshMaterials < 9);
				MeshMaterials meshMaterials4 = null;
				object obj2 = 32;
				object obj3 = default(object);
				object obj4 = default(object);
				while (true)
				{
					MeshMaterials[] array = allMeshLodsMaterials;
					MeshMaterials meshMaterials5 = new MeshMaterials();
					Material[] materialArray = new Material[0];
					meshMaterials5.materialArray = materialArray;
					meshMaterials5._002Ector();
					bool flag2 = allMeshLodsMaterials == null;
					meshMaterials2 = null;
					meshMaterials3 = meshMaterials5;
					if (flag2)
					{
						break;
					}
					nint num = (nint)array;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v304 @ rdx_v13 (Il2CppClass<MeshMaterials[]>)+40]");
					meshMaterials2 = (MeshMaterials)0;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
					bool flag3 = obj3 == null;
					meshMaterials3 = meshMaterials5;
					if (!flag3)
					{
						meshMaterials4 = (MeshMaterials)(meshMaterials4 + 1);
						obj2 += 8;
						if ((nint)meshMaterials4 >= 9)
						{
							return;
						}
						continue;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
					throw obj4;
				}
			}
			throw new NullReferenceException();
		}

		public ScannedMeshItem()
		{
			Mesh[] array = new Mesh[9];
			allMeshLods = array;
			string[] array2 = new string[9];
			allMeshLodsPaths = array2;
			MeshMaterials[] array3 = new MeshMaterials[9];
			allMeshLodsMaterials = array3;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803797D0");
		}
	}

	private sealed class _003COnRenderObject_HookEmulationForHDRP_003Ed__61 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public UltimateLevelOfDetail _003C_003E4__this;

		private WaitForEndOfFrame _003CwaitForEndOfFrame_003E5__2;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003COnRenderObject_HookEmulationForHDRP_003Ed__61(int _003C_003E1__state)
		{
			((IDisposable)this).Dispose();
			this._003C_003E1__state = _003C_003E1__state;
		}

		void IDisposable.Dispose()
		{
		}

		private bool MoveNext()
		{
			//IL_0014: Expected I4, but got I8
			//IL_0082: Expected I4, but got I8
			//IL_010a: Expected I4, but got O
			if (_003C_003E1__state == 0)
			{
				_003C_003E1__state = -1;
				WaitForEndOfFrame waitForEndOfFrame = new WaitForEndOfFrame();
				_003CwaitForEndOfFrame_003E5__2 = waitForEndOfFrame;
				if ((object)_003C_003E4__this != null)
				{
					goto IL_00b1;
				}
			}
			else
			{
				if (_003C_003E1__state != 1)
				{
					goto IL_00f6;
				}
				_003C_003E1__state = -1;
				if ((object)_003C_003E4__this != null)
				{
					_003C_003E4__this.OnRenderObject();
					goto IL_00b1;
				}
			}
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
			IL_00f6:
			return false;
			IL_00b1:
			if (_003C_003E4__this.enabled)
			{
				_003C_003E2__current = _003CwaitForEndOfFrame_003E5__2;
				_003C_003E1__state = 1;
				return true;
			}
			goto IL_00f6;
		}

		bool IEnumerator.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			return this.MoveNext();
		}

		void IEnumerator.Reset()
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
			NotSupportedException ex = new NotSupportedException();
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
			throw ex;
		}
	}

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

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CScanForMeshesAndGenerateAllLodGroups_AsyncProcessing_003Ed__51(int _003C_003E1__state)
		{
			this._003C_003E1__state = _003C_003E1__state;
		}

		void IDisposable.Dispose()
		{
			//IL_0010: Expected O, but got I4
			//IL_003d: Expected O, but got I4
			//IL_004a: Expected O, but got I8
			//IL_0064: Expected O, but got I8
			object obj = _003C_003E1__state + 4;
			if ((nint)obj <= 8)
			{
				object obj2 = _003C_003E1__state + 4;
				object obj3 = 6442450944L;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v21 @ rdx_v2+3B2588+v19 @ rax_v2*4]");
				object obj4 = 0 + 6442450944L;
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v23 @ rax_v4 (should have been resolved before IL gen)");
			}
		}

		private bool MoveNext()
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 127 Invalid \"Jump target not found in method: 0x1803B2133\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v49 @ stack_8_v2+10]");
			return false;
		}

		bool IEnumerator.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			return this.MoveNext();
		}

		private unsafe void _003C_003Em__Finally1()
		{
			//IL_0014: Expected I4, but got I8
			//IL_001a: Unknown result type (might be due to invalid IL or missing references)
			//IL_001f: Expected O, but got Unknown
			_003C_003E1__state = -1;
			List<SkinnedMeshRenderer>.Enumerator enumerator = (List<SkinnedMeshRenderer>.Enumerator)(this + 64);
			((List<SkinnedMeshRenderer>.Enumerator*)enumerator)->Dispose();
		}

		private unsafe void _003C_003Em__Finally2()
		{
			//IL_0014: Expected I4, but got I8
			//IL_001a: Unknown result type (might be due to invalid IL or missing references)
			//IL_001f: Expected O, but got Unknown
			_003C_003E1__state = -1;
			List<MeshFilter>.Enumerator enumerator = (List<MeshFilter>.Enumerator)(this + 120);
			((List<MeshFilter>.Enumerator*)enumerator)->Dispose();
		}

		void IEnumerator.Reset()
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
			NotSupportedException ex = new NotSupportedException();
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
			throw ex;
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

	public List<ScannedMeshItem> currentScannedMeshesList;

	public ScanMeshesMode modeOfMeshesScanning;

	public bool scanInactiveGameObjects;

	public List<GameObject> gameObjectsToIgnore;

	public int levelsOfDetailToGenerate;

	public float[] percentOfVerticesForEachLod;

	public bool saveGeneratedLodsInAssets;

	public bool skinnedAnimsCompatibilityMode;

	public bool preventArtifacts;

	public bool optimizeResultingMeshes;

	public bool enableLightmapsSupport;

	public bool enableMaterialsChanges;

	public ForceOfSimplification forceOfSimplification;

	public CullingMode cullingMode;

	private Transform _customPivotToSimulateLods;

	public CameraDetectionMode cameraDetectionMode;

	public bool useCacheForMainCameraInDetection;

	public Camera customCameraForSimulationOfLods;

	public float[] minDistanceOfViewForEachLod;

	public float minDistanceOfViewForCull;

	public UnityEvent onDoneScan;

	public UnityEvent onUndoScan;

	public bool forceChangeLodsOfSkinnedInEditor;

	public bool drawGizmoOnThisPivot;

	public Color colorOfGizmo;

	public float sizeOfGizmo;

	public bool forceShowHiddenSettings;

	public Transform customPivotToSimulateLods
	{
		get
		{
			return _customPivotToSimulateLods;
		}
		set
		{
			if (value != null)
			{
				GameObject gameObject = base.gameObject;
				Transform parent = gameObject.transform;
				if (!value.IsChildOf(parent))
				{
					Debug.LogError("We were unable to define a custom pivot. Make sure that the GameObject that will be the new personalized pivot is the child of the desired ULOD component.");
				}
				else
				{
					_customPivotToSimulateLods = value;
				}
			}
			else
			{
				_customPivotToSimulateLods = null;
			}
		}
	}

	private void ValidateAllParameters(bool isGoingToScan)
	{
		//IL_0418: Expected O, but got I4
		//IL_044c: Expected O, but got I4
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Expected O, but got Unknown
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Expected O, but got Unknown
		//IL_00a7: Expected F4, but got I
		//IL_0463: Unknown result type (might be due to invalid IL or missing references)
		//IL_0468: Expected O, but got Unknown
		//IL_00ee: Expected O, but got I
		//IL_0112: Invalid comparison between O and F4
		//IL_0157: Expected O, but got I4
		//IL_04b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ba: Expected O, but got Unknown
		//IL_0257: Expected O, but got I4
		//IL_0260: Expected O, but got I4
		//IL_04e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e5: Expected O, but got Unknown
		//IL_04ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f3: Expected O, but got Unknown
		//IL_04fe: Expected O, but got I4
		//IL_0197: Expected O, but got I4
		//IL_026f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0274: Expected O, but got Unknown
		//IL_01ac: Expected F4, but got I
		//IL_03cb: Invalid comparison between O and F4
		//IL_02b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02be: Expected O, but got Unknown
		//IL_02d3: Expected F4, but got I
		//IL_02fb: Invalid comparison between F4 and I4
		//IL_0249: Expected O, but got I
		//IL_0362: Unknown result type (might be due to invalid IL or missing references)
		//IL_0367: Expected O, but got Unknown
		//IL_0382: Expected O, but got I
		bool flag = levelsOfDetailToGenerate < 0;
		object obj = 0;
		if (!flag)
		{
			object obj2 = minDistanceOfViewForEachLod + 32;
			bool flag2 = false;
			do
			{
				obj = obj2;
				flag2 = (byte)((flag2 ? 1u : 0u) + 1u) != 0;
				obj2 += 4;
			}
			while ((flag2 ? 1 : 0) <= levelsOfDetailToGenerate);
		}
		if (forceOfSimplification != ForceOfSimplification.Normal)
		{
			preventArtifacts = false;
		}
		float[] array = new float[9] { 0f, 1f, 5f, 10f, 15f, 20f, 25f, 30f, 35f };
		float[] array2 = (float[])32;
		Array array3 = array;
		do
		{
			float[] array4 = minDistanceOfViewForEachLod;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v505 @ r8_v12 (System.Single[])+v252 @ rax_v7 (System.Single[])]");
			float num = 0f;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v505 @ r8_v12 (System.Single[])+v252 @ rax_v7 (System.Single[])]");
			nint num2 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v505 @ r8_v12 (System.Single[])+v138 @ rax_v10 (System.Single[])]");
			bool flag3 = num2 <= 0;
			float[] array5 = array4;
			if (!flag3)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v505 @ r8_v12 (System.Single[])+v252 @ rax_v7 (System.Single[])]");
				array5 = (float[])0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v505 @ r8_v12 (System.Single[])+v252 @ rax_v7 (System.Single[])]");
				_ = 0;
				array3 = array4;
			}
			array2 = (float[])(array2 + 4);
		}
		while ((nint)array2 < 68);
		if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj) >= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)minDistanceOfViewForCull))
		{
			float num = (float)obj + 10f;
			minDistanceOfViewForCull = num;
		}
		if (!isGoingToScan)
		{
			return;
		}
		bool flag4 = false;
		float[] array6 = (float[])32;
		do
		{
			if (flag4 && levelsOfDetailToGenerate >= (flag4 ? 1 : 0))
			{
				array3 = percentOfVerticesForEachLod;
				float[] array5 = (float[])((flag4 ? 1 : 0) - 1);
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v135 @ rdx_v8 (System.Single[])+v525 @ rcx_v11 (System.Array)]");
				float num = 0f;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v135 @ rdx_v8 (System.Single[])+v525 @ rcx_v11 (System.Array)]");
				nint num3 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v525 @ rcx_v11 (System.Array)+FFFFFFFC+v135 @ rdx_v8 (System.Single[])]");
				if (num3 > 0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v525 @ rcx_v11 (System.Array)+FFFFFFFC+v135 @ rdx_v8 (System.Single[])]");
					num = 0f - 1f;
					array5 = percentOfVerticesForEachLod;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v135 @ rdx_v8 (System.Single[])+v507 @ rax_v16 (System.Single[])]");
					if ((nint)0 >= (nint)0)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v135 @ rdx_v8 (System.Single[])+FFFFFFFC+v507 @ rax_v16 (System.Single[])]");
						_ = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v135 @ rdx_v8 (System.Single[])+FFFFFFFC+v507 @ rax_v16 (System.Single[])]");
						array5 = (float[])0;
					}
				}
			}
			flag4 = (byte)((flag4 ? 1u : 0u) + 1u) != 0;
			array6 = (float[])(array6 + 4);
		}
		while ((flag4 ? 1 : 0) < 9);
		object obj3 = 60;
		object obj4 = 8;
		object obj7;
		do
		{
			bool flag5 = (nint)obj4 < 0;
			if (obj4 != null)
			{
				object obj5 = levelsOfDetailToGenerate - obj4;
				flag5 = (nint)obj5 < 0;
				if (levelsOfDetailToGenerate >= (nint)obj4)
				{
					float[] array7 = minDistanceOfViewForEachLod;
					float[] array5 = (float[])(obj4 - 1);
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v136 @ rdx_v11+v131 @ r8_v13 (System.Single[])]");
					float num = 0f;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v136 @ rdx_v11+v131 @ r8_v13 (System.Single[])]");
					float num4 = 0f;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v136 @ rdx_v11+4+v131 @ r8_v13 (System.Single[])]");
					float num5 = num4 - 0f;
					flag5 = num5 < 0f;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v136 @ rdx_v11+v131 @ r8_v13 (System.Single[])]");
					nint num6 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v136 @ rdx_v11+4+v131 @ r8_v13 (System.Single[])]");
					if (num6 >= 0)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v136 @ rdx_v11+4+v131 @ r8_v13 (System.Single[])]");
						num = 0f - 1f;
						float[] array8 = minDistanceOfViewForEachLod;
						array5 = (float[])(obj4 - 1);
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v136 @ rdx_v11+v132 @ r8_v14 (System.Single[])]");
						object obj6 = -0;
						flag5 = (nint)obj6 < 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v136 @ rdx_v11+v132 @ r8_v14 (System.Single[])]");
						if ((nint)0 >= (nint)0)
						{
							_ = 1065353216;
						}
					}
				}
			}
			obj3 -= 4;
			obj4--;
			obj7 = !flag5;
		}
		while (obj7 != null);
		if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj) >= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)minDistanceOfViewForCull))
		{
			float num7 = (float)obj + 10f;
			minDistanceOfViewForCull = num7;
		}
	}

	private void CreateHierarchyOfFoldersIfNotExists()
	{
	}

	private string SaveGeneratedLodInAssets(string lodNumber, long ticks, Mesh generatedLodMesh)
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B39E0F]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		return "";
	}

	private unsafe Mesh GetGeneratedLodForThisMesh(Mesh originalMesh, float percentOfVertices, bool isSkinnedMesh)
	{
		//IL_002e: Expected O, but got I4
		//IL_0299: Expected O, but got I4
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Expected O, but got Unknown
		//IL_00f0: Expected O, but got Ref
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Expected O, but got Unknown
		MTAssets.UltimateLODSystem.MeshSimplifier.MeshSimplifier meshSimplifier = new MTAssets.UltimateLODSystem.MeshSimplifier.MeshSimplifier();
		bool flag = forceOfSimplification == ForceOfSimplification.Normal;
		float num;
		if (!flag)
		{
			object obj = forceOfSimplification - 1;
			if (!flag)
			{
				object obj2 = obj - 1;
				if (!flag)
				{
					object obj3 = obj2 - 1;
					if (!flag)
					{
						bool flag2 = (nint)obj3 != 1;
						num = 1E-05f;
						if (!flag2)
						{
							num = 0.2f;
						}
					}
					else
					{
						num = 0.4f;
					}
				}
				else
				{
					num = 0.6f;
				}
			}
			else
			{
				num = 0.8f;
			}
		}
		else
		{
			num = 1f;
		}
		bool flag3 = !preventArtifacts;
		if (flag3)
		{
			object obj4 = !flag3;
			if (obj4 == null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Warning: Method ends with non empty stack (-A0), the output could be wrong!");
				/*Error: End of method reached without returning.*/;
			}
		}
		Mesh mesh;
		if (meshSimplifier != null)
		{
			object obj5 = default(object);
			meshSimplifier.SimplificationOptions = (SimplificationOptions)(&obj5);
			meshSimplifier.Initialize(originalMesh);
			float num2 = percentOfVertices / 100f;
			float quality = num2 * num;
			meshSimplifier.SimplifyMesh(quality);
			mesh = meshSimplifier.ToMesh();
			if (optimizeResultingMeshes)
			{
				if ((object)mesh == null)
				{
					goto IL_0220;
				}
				mesh.Optimize();
			}
			if (!isSkinnedMesh || !skinnedAnimsCompatibilityMode)
			{
				goto IL_021b;
			}
			if ((object)originalMesh != null)
			{
				Matrix4x4[] bindposes = originalMesh.bindposes;
				if ((object)mesh != null)
				{
					mesh.bindposes = bindposes;
					goto IL_021b;
				}
			}
		}
		goto IL_0220;
		IL_021b:
		return mesh;
		IL_0220:
		return (Mesh)(object)new NullReferenceException();
	}

	private Material[] GetCopyOfExistentArrayOfMaterials(Material[] sourceArray)
	{
		//IL_000f: Expected O, but got I4
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Expected O, but got Unknown
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Expected O, but got Unknown
		//IL_0050: Expected O, but got I4
		//IL_00d4: Expected I, but got O
		//IL_00e4: Expected O, but got I
		//IL_010d: Expected O, but got I
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Expected O, but got Unknown
		//IL_015a: Expected O, but got I
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Expected O, but got Unknown
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0183: Expected O, but got Unknown
		//IL_018c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Expected O, but got Unknown
		//IL_01a1: Expected O, but got I
		Material[] array = default(Material[]);
		if (array != null)
		{
			Material[] array2 = (Material[])array.Length;
			Material[] array3 = new Material[array.Length];
			object obj = array + 24;
			object obj2 = (object)array - (object)array3;
			object obj3 = array3 + 32;
			object obj4 = 0;
			UltimateLevelOfDetail typeFromHandle = (UltimateLevelOfDetail)(object)typeof(Material[]);
			object obj5 = default(object);
			object obj6 = default(object);
			while (true)
			{
				if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj4) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj))
				{
					if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj4) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj))
					{
						bool flag = array3 == null;
						array = array2;
						if (flag)
						{
							break;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v69 @ r15_v5+v53 @ r14_v5]");
						if ((nint)0 != 0)
						{
							nint num = (nint)array3;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v242 @ rdx_v9 (Il2CppClass<UnityEngine.Material[]>)+40]");
							array = (Material[])0;
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
							bool flag2 = obj5 == null;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v69 @ r15_v5+v53 @ r14_v5]");
							typeFromHandle = (UltimateLevelOfDetail)0;
							if (flag2)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
								throw obj6;
							}
						}
						if ((nint)obj4 < array3.Length)
						{
							object obj7 = obj4 + 4;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v69 @ r15_v5+v53 @ r14_v5]");
							obj3 = 0;
							object obj8 = obj7 * 8;
							typeFromHandle = (UltimateLevelOfDetail)(object)((object)array3 + obj8);
							obj4++;
							obj3 += 8;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v69 @ r15_v5+v53 @ r14_v5]");
							array2 = (Material[])0;
							continue;
						}
					}
					return (Material[])(object)new IndexOutOfRangeException();
				}
				return array3;
			}
		}
		throw new NullReferenceException();
	}

	private void ScanForMeshesAndGenerateAllLodGroups_StartProcessing(bool showProgressBar)
	{
		_003CScanForMeshesAndGenerateAllLodGroups_AsyncProcessing_003Ed__51 obj = new _003CScanForMeshesAndGenerateAllLodGroups_AsyncProcessing_003Ed__51(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		Coroutine coroutine = StartCoroutine(obj);
	}

	private IEnumerator ScanForMeshesAndGenerateAllLodGroups_AsyncProcessing(bool showProgressBar)
	{
		_003CScanForMeshesAndGenerateAllLodGroups_AsyncProcessing_003Ed__51 obj = new _003CScanForMeshesAndGenerateAllLodGroups_AsyncProcessing_003Ed__51(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		return obj;
	}

	private unsafe void UndoAllMeshesScannedAndAllLodGroups(bool showProgressBar, bool deleteAllGeneratedMeshes, bool runMonoIl2CppGc, bool runUnityGc)
	{
		//IL_0036: Expected O, but got Ref
		//IL_005b: Expected O, but got I
		//IL_021b: Expected O, but got I
		//IL_0089: Expected O, but got I
		//IL_040f: Expected O, but got I
		//IL_024f: Expected O, but got I
		//IL_0443: Expected O, but got I
		//IL_027c: Expected O, but got I
		//IL_0118: Expected O, but got I
		//IL_0118: Expected O, but got I
		//IL_014e: Expected O, but got I
		//IL_030c: Expected O, but got I
		//IL_030c: Expected O, but got I
		//IL_01ab: Expected O, but got I
		//IL_0342: Expected O, but got I
		//IL_039f: Expected O, but got I
		if (currentScannedMeshesList != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
			List<ScannedMeshItem>.Enumerator enumerator = default(List<ScannedMeshItem>.Enumerator);
			object obj = default(object);
			while (enumerator.MoveNext())
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
				bool flag = obj == null;
				MeshFilter meshFilter = (MeshFilter)(&enumerator);
				if (!flag)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v69 @ stack_-58+18]");
					if ((UnityEngine.Object)0 != null)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v69 @ stack_-58+30]");
						object obj2 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v69 @ stack_-58+30]");
						if ((nint)0 == 0)
						{
							throw new NullReferenceException();
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v155 @ rax_v82+18]");
						if ((nint)0 <= (nint)0)
						{
							throw new IndexOutOfRangeException();
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v69 @ stack_-58+18]");
						if ((nint)0 == 0)
						{
							throw new NullReferenceException();
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v69 @ stack_-58+18]");
						nint num = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v155 @ rax_v82+20]");
						((SkinnedMeshRenderer)num).sharedMesh = (Mesh)0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v69 @ stack_-58+40]");
						if ((nint)0 != 0)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v69 @ stack_-58+48]");
							object obj3 = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v69 @ stack_-58+48]");
							if ((nint)0 == 0)
							{
								throw new NullReferenceException();
							}
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v246 @ rax_v84+18]");
							if ((nint)0 <= (nint)0)
							{
								throw new IndexOutOfRangeException();
							}
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v246 @ rax_v84+20]");
							object obj4 = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v246 @ rax_v84+20]");
							if ((nint)0 == 0)
							{
								throw new NullReferenceException();
							}
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v69 @ stack_-58+18]");
							if ((nint)0 == 0)
							{
								throw new NullReferenceException();
							}
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181D9C660");
						}
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v69 @ stack_-58+20]");
					if ((UnityEngine.Object)0 != null)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v69 @ stack_-58+28]");
						if ((UnityEngine.Object)0 != null)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v69 @ stack_-58+30]");
							object obj5 = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v69 @ stack_-58+30]");
							if ((nint)0 == 0)
							{
								throw new NullReferenceException();
							}
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v301 @ rax_v76+18]");
							if ((nint)0 <= (nint)0)
							{
								throw new IndexOutOfRangeException();
							}
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v69 @ stack_-58+20]");
							if ((nint)0 == 0)
							{
								throw new NullReferenceException();
							}
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v69 @ stack_-58+20]");
							nint num2 = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v301 @ rax_v76+20]");
							((MeshFilter)num2).sharedMesh = (Mesh)0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v69 @ stack_-58+40]");
							if ((nint)0 != 0)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v69 @ stack_-58+48]");
								object obj6 = 0;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v69 @ stack_-58+48]");
								if ((nint)0 == 0)
								{
									throw new NullReferenceException();
								}
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v765 @ rax_v78+18]");
								if ((nint)0 <= (nint)0)
								{
									throw new IndexOutOfRangeException();
								}
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v765 @ rax_v78+20]");
								object obj7 = 0;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v765 @ rax_v78+20]");
								if ((nint)0 == 0)
								{
									throw new NullReferenceException();
								}
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v69 @ stack_-58+28]");
								if ((nint)0 == 0)
								{
									throw new NullReferenceException();
								}
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181D9C660");
							}
						}
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v69 @ stack_-58+50]");
					if ((UnityEngine.Object)0 != null)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v69 @ stack_-58+50]");
						UnityEngine.Object.Destroy((UnityEngine.Object)0);
					}
					continue;
				}
				throw new NullReferenceException();
			}
			enumerator.Dispose();
			List<ScannedMeshItem> list = currentScannedMeshesList;
			if (currentScannedMeshesList != null)
			{
				int version = list._version + 1;
				list._version = version;
				((List<ScannedMeshItem>.Enumerator*)null)->Dispose();
				object obj8 = default(object);
				if (obj8 == null)
				{
					list._size = 0;
				}
				else
				{
					list._size = 0;
					if (list._size > 0)
					{
						Array.Clear(list._items, 0, list._size);
					}
				}
				if (runMonoIl2CppGc)
				{
					GC.Collect();
				}
				object obj9 = default(object);
				if (obj9 != null)
				{
					AsyncOperation asyncOperation = Resources.UnloadUnusedAssets();
				}
				lastDistanceFromMainCamera = -1f;
				if (Application.isPlaying && onUndoScan != null)
				{
					onUndoScan.Invoke();
				}
				GameObject gameObject = base.gameObject;
				if ((object)gameObject != null)
				{
					string text = gameObject.name;
					string message = "All scanned meshes in GameObject \"" + text + "\" were restored to the original meshes. The scan was undone.";
					Debug.Log(message);
					return;
				}
			}
		}
		throw new NullReferenceException();
	}

	private bool isLodsSimulationEnabledInThisSceneForEditorSceneViewMode()
	{
		return true;
	}

	private Camera GetLastActiveSceneViewCamera()
	{
		return null;
	}

	private void CullThisLodMeshOfRenderer(ScannedMeshItem meshItem)
	{
		if (cullingMode == CullingMode.Disabled)
		{
			return;
		}
		if (cullingMode == CullingMode.CullingMeshes)
		{
			if (meshItem.originalSkinnedMeshRenderer != null)
			{
				Mesh sharedMesh = meshItem.originalSkinnedMeshRenderer.sharedMesh;
				if (sharedMesh != null)
				{
					Mesh sharedMesh2 = meshItem.originalSkinnedMeshRenderer.sharedMesh;
					meshItem.beforeCullingData_lastMeshOfThis = sharedMesh2;
					meshItem.originalSkinnedMeshRenderer.sharedMesh = null;
				}
			}
			if (meshItem.originalMeshFilter != null)
			{
				Mesh sharedMesh3 = meshItem.originalMeshFilter.sharedMesh;
				if (sharedMesh3 != null)
				{
					Mesh sharedMesh4 = meshItem.originalMeshFilter.sharedMesh;
					meshItem.beforeCullingData_lastMeshOfThis = sharedMesh4;
					meshItem.originalMeshFilter.sharedMesh = null;
				}
			}
		}
		if (cullingMode == CullingMode.CullingRenderer)
		{
			if (meshItem.originalSkinnedMeshRenderer != null)
			{
				bool forceRenderingOff = meshItem.originalSkinnedMeshRenderer.forceRenderingOff;
				meshItem.beforeCullingData_isForcedToRenderizationOff = forceRenderingOff;
				meshItem.originalSkinnedMeshRenderer.forceRenderingOff = true;
			}
			if (meshItem.originalMeshRenderer != null)
			{
				bool forceRenderingOff2 = meshItem.originalMeshRenderer.forceRenderingOff;
				meshItem.beforeCullingData_isForcedToRenderizationOff = forceRenderingOff2;
				meshItem.originalMeshRenderer.forceRenderingOff = true;
			}
		}
	}

	private void UncullThisLodMeshOfRenderer(ScannedMeshItem meshItem)
	{
		if (cullingMode == CullingMode.Disabled)
		{
			return;
		}
		if (cullingMode == CullingMode.CullingMeshes)
		{
			if (meshItem.originalSkinnedMeshRenderer != null)
			{
				Mesh sharedMesh = meshItem.originalSkinnedMeshRenderer.sharedMesh;
				if (sharedMesh == null)
				{
					meshItem.originalSkinnedMeshRenderer.sharedMesh = meshItem.beforeCullingData_lastMeshOfThis;
				}
			}
			if (meshItem.originalMeshFilter != null)
			{
				Mesh sharedMesh2 = meshItem.originalMeshFilter.sharedMesh;
				if (sharedMesh2 == null)
				{
					meshItem.originalMeshFilter.sharedMesh = meshItem.beforeCullingData_lastMeshOfThis;
				}
			}
		}
		if (cullingMode == CullingMode.CullingRenderer)
		{
			if (meshItem.originalSkinnedMeshRenderer != null)
			{
				meshItem.originalSkinnedMeshRenderer.forceRenderingOff = meshItem.beforeCullingData_isForcedToRenderizationOff;
			}
			if (meshItem.originalMeshRenderer != null)
			{
				meshItem.originalMeshRenderer.forceRenderingOff = meshItem.beforeCullingData_isForcedToRenderizationOff;
			}
		}
	}

	private void ChangeLodMeshAndMaterialsOfRenderer(ScannedMeshItem meshItem, int lodLevel)
	{
		//IL_0221: Expected O, but got I4
		//IL_022a: Expected O, but got I4
		//IL_00be: Expected O, but got I4
		//IL_00c8: Expected O, but got I4
		//IL_02ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d3: Expected O, but got Unknown
		//IL_02dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e1: Expected O, but got Unknown
		//IL_0164: Unknown result type (might be due to invalid IL or missing references)
		//IL_0169: Expected O, but got Unknown
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Expected O, but got Unknown
		//IL_0267: Expected O, but got I
		//IL_00ff: Expected O, but got I
		//IL_02af: Expected O, but got I
		//IL_0145: Expected O, but got I
		if (meshItem.originalSkinnedMeshRenderer != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180742270");
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180742270");
			object obj = default(object);
			object obj2 = default(object);
			if ((obj != null && forceChangeLodsOfSkinnedInEditor) || obj2 == null)
			{
				object obj3 = 0;
				object obj4 = 32;
				do
				{
					if ((nint)obj3 == lodLevel)
					{
						Mesh[] allMeshLods = meshItem.allMeshLods;
						SkinnedMeshRenderer originalSkinnedMeshRenderer = meshItem.originalSkinnedMeshRenderer;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v115 @ rsi_v7+v71 @ rdx_v18 (UnityEngine.Mesh[])]");
						originalSkinnedMeshRenderer.sharedMesh = (Mesh)0;
						if (meshItem.canChangeMaterialsOnThisMeshLods)
						{
							ScannedMeshItem.MeshMaterials[] allMeshLodsMaterials = meshItem.allMeshLodsMaterials;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v115 @ rsi_v7+v92 @ rax_v30 (MeshMaterials[])]");
							object obj5 = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181D9C660");
						}
					}
					obj3++;
					obj4 += 8;
				}
				while ((nint)obj4 < 104);
				meshItem.originalSkinnedMeshRenderer.enabled = false;
				meshItem.originalSkinnedMeshRenderer.enabled = true;
			}
		}
		if (!(meshItem.originalMeshFilter != null))
		{
			return;
		}
		bool flag = meshItem.originalMeshRenderer != null;
		bool flag2 = !flag;
		object obj6 = 32;
		object obj7 = 0;
		if (flag2)
		{
			return;
		}
		do
		{
			if ((nint)obj7 == lodLevel)
			{
				Mesh[] allMeshLods2 = meshItem.allMeshLods;
				MeshFilter originalMeshFilter = meshItem.originalMeshFilter;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v66 @ r14_v6+v76 @ rdx_v10 (UnityEngine.Mesh[])]");
				originalMeshFilter.sharedMesh = (Mesh)0;
				if (meshItem.canChangeMaterialsOnThisMeshLods)
				{
					ScannedMeshItem.MeshMaterials[] allMeshLodsMaterials2 = meshItem.allMeshLodsMaterials;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v66 @ r14_v6+v96 @ rax_v17 (MeshMaterials[])]");
					object obj8 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181D9C660");
				}
			}
			obj7++;
			obj6 += 8;
		}
		while ((nint)obj6 < 104);
	}

	private void CalculateCorrectLodForDistanceBeforeChange(float distance)
	{
		//IL_00a0: Expected I4, but got I8
		List<ScannedMeshItem> list = currentScannedMeshesList;
		if (currentScannedMeshesList != null)
		{
			if (list._size == 0)
			{
				return;
			}
			bool flag = lastDistanceFromMainCamera == distance;
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 00000001803B3A56h\"");
			if (flag)
			{
				return;
			}
			float[] array = minDistanceOfViewForEachLod;
			if (minDistanceOfViewForEachLod != null)
			{
				bool flag2 = !(array[1] > distance);
				int num = -1;
				if (!flag2)
				{
					num = 0;
				}
				if (!(distance < array[1]))
				{
					num = 1;
				}
				if (levelsOfDetailToGenerate >= 2 && !(distance < array[2]))
				{
					num = 2;
				}
				if (levelsOfDetailToGenerate >= 3 && !(distance < array[3]))
				{
					num = 3;
				}
				if (levelsOfDetailToGenerate >= 4)
				{
					float[] array2 = minDistanceOfViewForEachLod;
					if (!(distance < array2[4]))
					{
						num = 4;
					}
				}
				if (levelsOfDetailToGenerate >= 5)
				{
					float[] array3 = minDistanceOfViewForEachLod;
					if (!(distance < array3[5]))
					{
						num = 5;
					}
				}
				if (levelsOfDetailToGenerate >= 6)
				{
					float[] array4 = minDistanceOfViewForEachLod;
					if (!(distance < array4[6]))
					{
						num = 6;
					}
				}
				if (levelsOfDetailToGenerate >= 7)
				{
					float[] array5 = minDistanceOfViewForEachLod;
					if (!(distance < array5[7]))
					{
						num = 7;
					}
				}
				if (levelsOfDetailToGenerate >= 8)
				{
					float[] array6 = minDistanceOfViewForEachLod;
					if (!(distance < array6[8]))
					{
						num = 8;
					}
				}
				if (cullingMode == CullingMode.Disabled || distance < minDistanceOfViewForCull)
				{
					if (num <= 8 && currentLodAccordingToDistance != num)
					{
						if (currentScannedMeshesList == null)
						{
							goto IL_03d7;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
						List<ScannedMeshItem>.Enumerator enumerator = default(List<ScannedMeshItem>.Enumerator);
						ScannedMeshItem meshItem = default(ScannedMeshItem);
						while (enumerator.MoveNext())
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
							UncullThisLodMeshOfRenderer(meshItem);
							ChangeLodMeshAndMaterialsOfRenderer(meshItem, num);
							currentLodAccordingToDistance = num;
						}
						enumerator.Dispose();
					}
					if (num != 9)
					{
						goto IL_04aa;
					}
				}
				if (currentLodAccordingToDistance != 9)
				{
					if (currentScannedMeshesList == null)
					{
						goto IL_03d7;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
					List<ScannedMeshItem>.Enumerator enumerator2 = default(List<ScannedMeshItem>.Enumerator);
					ScannedMeshItem meshItem2 = default(ScannedMeshItem);
					while (enumerator2.MoveNext())
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
						CullThisLodMeshOfRenderer(meshItem2);
						currentLodAccordingToDistance = 9;
					}
					enumerator2.Dispose();
				}
				goto IL_04aa;
			}
		}
		goto IL_03d7;
		IL_03d7:
		throw new NullReferenceException();
		IL_04aa:
		lastDistanceFromMainCamera = distance;
	}

	public void OnRenderObject()
	{
		//IL_09d6: Expected O, but got I4
		//IL_09de: Unknown result type (might be due to invalid IL or missing references)
		//IL_09e3: Expected O, but got Unknown
		//IL_03bf: Expected I, but got O
		//IL_0316: Expected I, but got O
		//IL_0268: Expected I, but got O
		//IL_033d: Expected I, but got O
		//IL_028f: Expected I, but got O
		//IL_041d: Expected I, but got O
		//IL_0704: Unknown result type (might be due to invalid IL or missing references)
		//IL_0709: Expected O, but got Unknown
		//IL_0712: Unknown result type (might be due to invalid IL or missing references)
		//IL_0717: Expected O, but got Unknown
		//IL_056f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0574: Expected O, but got Unknown
		//IL_057d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0582: Expected O, but got Unknown
		//IL_07bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_07c1: Expected O, but got Unknown
		//IL_07ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_07cf: Expected O, but got Unknown
		//IL_0641: Unknown result type (might be due to invalid IL or missing references)
		//IL_0646: Expected O, but got Unknown
		//IL_064f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0654: Expected O, but got Unknown
		Camera camera;
		float distance;
		if (UltimateLevelOfDetailGlobal.enableGlobalUlodSystem && !forcedToDisableLodsOfThisComponent)
		{
			if (forcedToShowOnlyALodIndependentOfDistance == -1)
			{
				bool flag = cameraDetectionMode != CameraDetectionMode.MainCamera;
				camera = null;
				if (!flag)
				{
					bool isPlaying = Application.isPlaying;
					bool flag2 = !isPlaying;
					camera = null;
					if (!flag2)
					{
						bool flag3 = useCacheForMainCameraInDetection;
						camera = null;
						if (!flag3)
						{
							Camera main = Camera.main;
							camera = main;
						}
						if (useCacheForMainCameraInDetection)
						{
							if (cacheOfMainCamera != null)
							{
								if (cacheOfMainCamera.enabled)
								{
									GameObject gameObject = cacheOfMainCamera.gameObject;
									if (gameObject.activeSelf)
									{
										GameObject gameObject2 = cacheOfMainCamera.gameObject;
										if (gameObject2.activeInHierarchy)
										{
											goto IL_01cc;
										}
									}
								}
								cacheOfMainCamera = null;
							}
							goto IL_01cc;
						}
						goto IL_024a;
					}
				}
				goto IL_0299;
			}
			int num = forcedToShowOnlyALodIndependentOfDistance ^ forcedToShowOnlyALodIndependentOfDistance;
			int num2 = forcedToShowOnlyALodIndependentOfDistance & num;
			bool flag4 = num2 < 0;
			bool flag5 = forcedToShowOnlyALodIndependentOfDistance < 0;
			bool flag6 = forcedToShowOnlyALodIndependentOfDistance == 0;
			if (forcedToShowOnlyALodIndependentOfDistance == 0)
			{
				CalculateCorrectLodForDistanceBeforeChange(0f);
				int num3 = forcedToShowOnlyALodIndependentOfDistance ^ forcedToShowOnlyALodIndependentOfDistance;
				int num4 = forcedToShowOnlyALodIndependentOfDistance & num3;
				flag4 = num4 < 0;
				flag5 = forcedToShowOnlyALodIndependentOfDistance < 0;
				flag6 = forcedToShowOnlyALodIndependentOfDistance == 0;
			}
			bool flag7 = flag5 == flag4;
			object obj = !flag7;
			object obj2 = obj | flag6;
			if (obj2 != null)
			{
				return;
			}
			float[] array = minDistanceOfViewForEachLod;
			int num5 = forcedToShowOnlyALodIndependentOfDistance;
			distance = array[num5];
			goto IL_09f1;
		}
		CalculateCorrectLodForDistanceBeforeChange(0f);
		return;
		IL_0299:
		bool flag8 = cameraDetectionMode != CameraDetectionMode.CurrentCamera;
		UnityEngine.Object obj3 = camera;
		nint num6;
		string text;
		if (!flag8)
		{
			bool isPlaying2 = Application.isPlaying;
			bool flag9 = !isPlaying2;
			obj3 = camera;
			if (!flag9)
			{
				obj3 = UltimateLevelOfDetailGlobal.currentCameraThatIsOnTopOfScreenInThisScene;
				bool flag10 = UltimateLevelOfDetailGlobal.currentCameraThatIsOnTopOfScreenInThisScene == null;
				bool flag11 = !flag10;
				num6 = unchecked((nint)null);
				text = null;
				if (!flag11)
				{
					Debug.LogError("It was not possible to find a current camera at the moment, it seems that there are no cameras in the scene, or Unity was unable to make references. Please try to switch to \"Main Camera\" mode.");
					num6 = unchecked((nint)null);
					text = null;
				}
			}
		}
		if (cameraDetectionMode == CameraDetectionMode.CustomCamera && Application.isPlaying)
		{
			obj3 = customCameraForSimulationOfLods;
			bool flag12 = customCameraForSimulationOfLods == null;
			bool flag13 = !flag12;
			num6 = unchecked((nint)null);
			text = null;
			if (!flag13)
			{
				GameObject gameObject3 = base.gameObject;
				string text2 = gameObject3.name;
				string message = "No custom camera for calculating distance and simulating LODs has been provided in \"" + text2 + "\".";
				Debug.LogError(message);
				num6 = unchecked((nint)null);
				text = "\".";
			}
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180742270");
		object obj4 = default(object);
		if (obj4 != null && !Application.isPlaying)
		{
			bool flag14 = (UnityEngine.Object)null == (UnityEngine.Object)null;
			bool flag15 = !flag14;
			obj3 = null;
			if (!flag15)
			{
				Debug.LogError("It was not possible to find a camera that is currently viewing a scene. Make sure the scene view window is active and in focus.");
				obj3 = null;
			}
		}
		if (obj3 != null)
		{
			object obj6 = default(object);
			if (_customPivotToSimulateLods == null)
			{
				GameObject gameObject4 = base.gameObject;
				Transform transform = gameObject4.transform;
				Vector3 position = transform.position;
				Transform transform2 = ((Component)obj3).transform;
				Vector3 position2 = transform2.position;
				object obj5 = obj6 - 48;
				object obj7 = obj6 - 32;
				_ = position2.x;
				_ = position2.z;
				_ = position.x;
				_ = position.z;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803716B0");
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A37F0");
				float num7 = position2.x * position2.x;
				currentDistanceFromMainCamera = num7;
				GameObject gameObject5 = base.gameObject;
				Transform transform3 = gameObject5.transform;
				Vector3 position3 = transform3.position;
				Transform transform4 = ((Component)obj3).transform;
				Vector3 position4 = transform4.position;
				object obj8 = obj6 - 32;
				object obj9 = obj6 - 48;
				_ = position4.x;
				_ = position4.z;
				_ = position3.x;
				_ = position3.z;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803716B0");
				currentRealDistanceFromMainCamera = position4.x;
			}
			if (_customPivotToSimulateLods != null)
			{
				Vector3 position5 = _customPivotToSimulateLods.position;
				Transform transform5 = ((Component)obj3).transform;
				Vector3 position6 = transform5.position;
				object obj10 = obj6 - 32;
				object obj11 = obj6 - 48;
				_ = position6.x;
				_ = position6.z;
				_ = position5.x;
				_ = position5.z;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803716B0");
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A37F0");
				float num8 = position6.x * position6.x;
				currentDistanceFromMainCamera = num8;
				Vector3 position7 = _customPivotToSimulateLods.position;
				Transform transform6 = ((Component)obj3).transform;
				Vector3 position8 = transform6.position;
				object obj12 = obj6 - 32;
				object obj13 = obj6 - 48;
				_ = position8.x;
				_ = position8.z;
				_ = position7.x;
				_ = position7.z;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803716B0");
				currentRealDistanceFromMainCamera = position8.x;
			}
		}
		if (obj3 == null)
		{
			currentDistanceFromMainCamera = 0f;
		}
		distance = currentDistanceFromMainCamera;
		goto IL_09f1;
		IL_024a:
		bool flag16 = camera == null;
		bool flag17 = !flag16;
		num6 = unchecked((nint)null);
		text = null;
		if (!flag17)
		{
			Debug.LogError("It was not possible to find a main camera to calculate LODs. Please make sure that the main camera in your scene has the \"MainCamera\" tag defined in the GameObject in which it is located.");
			num6 = unchecked((nint)null);
			text = null;
		}
		goto IL_0299;
		IL_09f1:
		CalculateCorrectLodForDistanceBeforeChange(distance);
		return;
		IL_01cc:
		if (cacheOfMainCamera == null)
		{
			Camera main2 = Camera.main;
			cacheOfMainCamera = main2;
		}
		if (cacheOfMainCamera != null)
		{
			camera = cacheOfMainCamera;
		}
		goto IL_024a;
	}

	public unsafe void Awake()
	{
		//IL_0074: Expected O, but got I
		//IL_0122: Expected O, but got Ref
		CalculateCorrectLodForDistanceBeforeChange(0f);
		GameObject gameObject = GameObject.Find("Ultimate LOD Data");
		RuntimeInstancesDetector runtimeInstancesDetector = default(RuntimeInstancesDetector);
		if (gameObject != null && Application.isPlaying)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806D9540");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v92 @ stack_18_v6 (MTAssets.UltimateLODSystem.RuntimeInstancesDetector)+20]");
			((List<UltimateLevelOfDetail>)0).Add(this);
			cacheOfUlodData = gameObject;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806D9540");
			cacheOfUlodDataRuntimeInstancesDetector = runtimeInstancesDetector;
		}
		if (gameObject == null && Application.isPlaying)
		{
			GameObject gameObject2 = new GameObject("Ultimate LOD Data");
			Transform transform = gameObject2.transform;
			object obj = default(object);
			transform.position = (Vector3)(&obj);
			RuntimeCameraDetector runtimeCameraDetector = gameObject2.AddComponent<RuntimeCameraDetector>();
			RuntimeInstancesDetector runtimeInstancesDetector2 = gameObject2.AddComponent<RuntimeInstancesDetector>();
			runtimeInstancesDetector2.instancesOfUlodInThisScene.Add(this);
			cacheOfUlodData = gameObject2;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806D9540");
			cacheOfUlodDataRuntimeInstancesDetector = runtimeInstancesDetector;
		}
	}

	private IEnumerator OnRenderObject_HookEmulationForHDRP()
	{
		_003COnRenderObject_HookEmulationForHDRP_003Ed__61 obj = new _003COnRenderObject_HookEmulationForHDRP_003Ed__61(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		return obj;
	}

	public void OnEnable()
	{
		if (Application.isPlaying)
		{
			List<ScannedMeshItem> list = currentScannedMeshesList;
			if (list._size != 0 && CurrentRenderPipeline.haveAnotherSrpPackages && CurrentRenderPipeline.packageDetected == "HDRP")
			{
				_003COnRenderObject_HookEmulationForHDRP_003Ed__61 obj = new _003COnRenderObject_HookEmulationForHDRP_003Ed__61(0);
				obj._003C_003E1__state = 0;
				obj._003C_003E4__this = this;
				Coroutine coroutine = StartCoroutine(obj);
			}
		}
	}

	public int GetCurrentLodLevel()
	{
		//IL_0032: Expected O, but got I4
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Expected I4, but got Unknown
		//IL_0098: Expected O, but got I4
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Expected O, but got Unknown
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Expected O, but got Unknown
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bb: Expected O, but got Unknown
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Expected O, but got Unknown
		//IL_01e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01eb: Expected O, but got Unknown
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		//IL_018b: Expected O, but got Unknown
		//IL_0216: Unknown result type (might be due to invalid IL or missing references)
		//IL_021b: Expected I4, but got Unknown
		if (currentLodAccordingToDistance == 9)
		{
			object obj = levelsOfDetailToGenerate - 2;
			int num = levelsOfDetailToGenerate ^ 2;
			int num2 = levelsOfDetailToGenerate ^ obj;
			int num3 = num & num2;
			bool flag = num3 < 0;
			bool flag2 = (nint)obj < 0;
			bool flag3 = flag2 == flag;
			object obj2 = (flag3 ? 1 : 0) + 1;
			object obj3 = obj2 + 1;
			if (levelsOfDetailToGenerate < 3)
			{
				obj3 = obj2;
			}
			object obj4 = obj3 + 1;
			if (levelsOfDetailToGenerate < 4)
			{
				obj4 = obj3;
			}
			object obj5 = obj4 + 1;
			if (levelsOfDetailToGenerate < 5)
			{
				obj5 = obj4;
			}
			object obj6 = obj5 + 1;
			if (levelsOfDetailToGenerate < 6)
			{
				obj6 = obj5;
			}
			object obj7 = obj6 + 1;
			if (levelsOfDetailToGenerate < 7)
			{
				obj7 = obj6;
			}
			object obj8 = obj7 + 1;
			if (levelsOfDetailToGenerate < 8)
			{
				obj8 = obj7;
			}
			return obj8 - 1;
		}
		return currentLodAccordingToDistance;
	}

	public float GetCurrentCameraDistance()
	{
		return currentDistanceFromMainCamera;
	}

	public float GetCurrentRealCameraDistance()
	{
		return currentRealDistanceFromMainCamera;
	}

	public int GetNumberOfLodsGenerated()
	{
		//IL_0010: Expected O, but got I4
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Expected I4, but got Unknown
		object obj = levelsOfDetailToGenerate - 2;
		int num = levelsOfDetailToGenerate ^ 2;
		int num2 = levelsOfDetailToGenerate ^ obj;
		int num3 = num & num2;
		bool flag = num3 < 0;
		bool flag2 = (nint)obj < 0;
		bool flag3 = flag2 == flag;
		int num4 = (flag3 ? 1 : 0) + 1;
		int num5 = num4 + 1;
		if (levelsOfDetailToGenerate < 3)
		{
			num5 = num4;
		}
		int num6 = num5 + 1;
		if (levelsOfDetailToGenerate < 4)
		{
			num6 = num5;
		}
		int num7 = num6 + 1;
		if (levelsOfDetailToGenerate < 5)
		{
			num7 = num6;
		}
		int num8 = num7 + 1;
		if (levelsOfDetailToGenerate < 6)
		{
			num8 = num7;
		}
		int num9 = num8 + 1;
		if (levelsOfDetailToGenerate < 7)
		{
			num9 = num8;
		}
		int result = num9 + 1;
		if (levelsOfDetailToGenerate < 8)
		{
			result = num9;
		}
		return result;
	}

	public bool isScannedMeshesCurrentCulled()
	{
		//IL_0010: Expected O, but got I4
		object obj = currentLodAccordingToDistance - 9;
		return obj == null;
	}

	public unsafe UltimateLevelOfDetailMeshes[] GetListOfAllMeshesScanned()
	{
		//IL_0135: Expected O, but got Ref
		//IL_0036: Expected O, but got Ref
		//IL_005b: Expected O, but got Ref
		//IL_007e: Expected O, but got I
		List<UltimateLevelOfDetailMeshes> list = new List<UltimateLevelOfDetailMeshes>();
		if (currentScannedMeshesList != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
			List<ScannedMeshItem>.Enumerator enumerator = default(List<ScannedMeshItem>.Enumerator);
			object obj = default(object);
			List<ScannedMeshItem>.Enumerator enumerator2;
			while (enumerator.MoveNext())
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
				bool flag = obj == null;
				enumerator2 = (List<ScannedMeshItem>.Enumerator)(&enumerator);
				if (!flag)
				{
					bool flag2 = list == null;
					enumerator2 = (List<ScannedMeshItem>.Enumerator)(&enumerator);
					if (!flag2)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v117 @ stack_8_v5+50]");
						list.Add((UltimateLevelOfDetailMeshes)0);
						continue;
					}
					throw new NullReferenceException();
				}
				throw new NullReferenceException();
			}
			enumerator.Dispose();
			bool flag3 = list == null;
			enumerator2 = (List<ScannedMeshItem>.Enumerator)(&enumerator);
			if (!flag3)
			{
				return list.ToArray();
			}
		}
		throw new NullReferenceException();
	}

	public void ForceShowLod(bool force, int level)
	{
		//IL_00b8: Expected I4, but got I8
		if (force)
		{
			string text;
			string text2;
			if (level >= 0)
			{
				if (level <= levelsOfDetailToGenerate)
				{
					forcedToShowOnlyALodIndependentOfDistance = level;
					return;
				}
				GameObject gameObject = base.gameObject;
				text = gameObject.name;
				text2 = "\". The level provided is greater than the number of levels of detail that this component has generated.";
			}
			else
			{
				GameObject gameObject2 = base.gameObject;
				text = gameObject2.name;
				text2 = "\". The level provided is less than zero.";
			}
			string message = "It was not possible to force a LOD on the ULOD component of \"" + text + text2;
			Debug.LogError(message);
		}
		else
		{
			forcedToShowOnlyALodIndependentOfDistance = -1;
		}
	}

	public bool isThisComponentForcedToShowLod()
	{
		//IL_0010: Expected O, but got I4
		object obj = forcedToShowOnlyALodIndependentOfDistance - -1;
		bool flag = obj == null;
		return !flag;
	}

	public void ForceDisableLodChangesInThisComponent(bool force)
	{
		forcedToDisableLodsOfThisComponent = force;
	}

	public bool isThisComponentForcedToDisableLodChanges()
	{
		return forcedToDisableLodsOfThisComponent;
	}

	public void ForceThisComponentToUpdateLodsRender()
	{
		//IL_0033: Expected I4, but got I8
		float num = UnityEngine.Random.Range(0.1f, 1f);
		float num2 = num + lastDistanceFromMainCamera;
		currentLodAccordingToDistance = -1;
		lastDistanceFromMainCamera = num2;
		OnRenderObject();
	}

	public bool isMeshesCurrentScannedAndLodsWorkingInThisComponent()
	{
		//IL_00b2: Expected I4, but got O
		List<ScannedMeshItem> list = currentScannedMeshesList;
		if (currentScannedMeshesList != null)
		{
			int num = list._size ^ list._size;
			int num2 = list._size & num;
			bool flag = num2 < 0;
			bool flag2 = list._size < 0;
			bool flag3 = list._size == 0;
			if (!flag3)
			{
				bool flag4 = flag2 == flag;
				bool flag5 = !flag3;
				return flag5 & flag4;
			}
			return false;
		}
		NullReferenceException ex = new NullReferenceException();
		return (byte)(int)ex != 0;
	}

	public void ScanAllMeshesAndGenerateLodsGroups()
	{
		//IL_0102: Expected O, but got I4
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Expected O, but got Unknown
		List<ScannedMeshItem> list = currentScannedMeshesList;
		if (list._size > 0)
		{
			GameObject gameObject = base.gameObject;
			string text = gameObject.name;
			string message = "It was not possible to start scanning meshes to generate LODs. Component in " + text + " already has an active scan. It is necessary to undo the current scan before starting a new one.";
			Debug.LogError(message);
			return;
		}
		List<ScannedMeshItem> list2 = currentScannedMeshesList;
		int num = list2._size ^ list2._size;
		int num2 = list2._size & num;
		bool flag = num2 < 0;
		bool flag2 = list2._size < 0;
		bool flag3 = list2._size == 0;
		if (!flag3)
		{
			bool flag4 = flag2 == flag;
			object obj = !flag3;
			object obj2 = flag4 & obj;
			if (obj2 != null)
			{
				return;
			}
		}
		_003CScanForMeshesAndGenerateAllLodGroups_AsyncProcessing_003Ed__51 obj3 = new _003CScanForMeshesAndGenerateAllLodGroups_AsyncProcessing_003Ed__51(0);
		obj3._003C_003E1__state = 0;
		obj3._003C_003E4__this = this;
		Coroutine coroutine = StartCoroutine(obj3);
	}

	public void UndoCurrentScanWorkingAndDeleteGeneratedMeshes(bool runMonoIl2CppGc, bool runUnityGc)
	{
		List<ScannedMeshItem> list = currentScannedMeshesList;
		if (list._size > 0)
		{
			List<ScannedMeshItem> list2 = currentScannedMeshesList;
			if (list2._size > 0)
			{
				bool runUnityGc2 = default(bool);
				UndoAllMeshesScannedAndAllLodGroups(showProgressBar: false, deleteAllGeneratedMeshes: true, runMonoIl2CppGc, runUnityGc2);
			}
		}
		else
		{
			GameObject gameObject = base.gameObject;
			string text = gameObject.name;
			string message = "It was not possible to undo the LODs scan existing in the " + text + " component. There is no scan done yet, it is necessary to perform one before.";
			Debug.LogError(message);
		}
	}

	public UltimateLevelOfDetail[] GetListOfAllUlodsInThisScene()
	{
		if (Application.isPlaying)
		{
			RuntimeInstancesDetector runtimeInstancesDetector = cacheOfUlodDataRuntimeInstancesDetector;
			if ((object)cacheOfUlodDataRuntimeInstancesDetector != null && runtimeInstancesDetector.instancesOfUlodInThisScene != null)
			{
				return runtimeInstancesDetector.instancesOfUlodInThisScene.ToArray();
			}
			return (UltimateLevelOfDetail[])(object)new NullReferenceException();
		}
		Debug.LogError("It is only possible to obtain the list of ULODs in this scene, if the application is being executed.");
		return null;
	}

	public UltimateLevelOfDetailOptimizer[] GetListOfAllUlodsOptimizerInThisScene()
	{
		//IL_00a3: Expected O, but got I
		if (Application.isPlaying)
		{
			if ((object)cacheOfUlodData != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806D9540");
				object obj = default(object);
				if (obj != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v86 @ stack_18_v2+28]");
					if ((nint)0 != 0)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v86 @ stack_18_v2+28]");
						return ((List<UltimateLevelOfDetailOptimizer>)0).ToArray();
					}
				}
			}
			return (UltimateLevelOfDetailOptimizer[])(object)new NullReferenceException();
		}
		Debug.LogError("It is only possible to obtain the list of ULODs Optimizers in this scene, if the application is being executed.");
		return null;
	}

	public void SetNewCustomCameraForThisAndAllUlodsInThisScene(Camera newCustomCamera)
	{
		if (Application.isPlaying)
		{
			customCameraForSimulationOfLods = newCustomCamera;
			RuntimeInstancesDetector runtimeInstancesDetector = cacheOfUlodDataRuntimeInstancesDetector;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
			List<UltimateLevelOfDetail>.Enumerator enumerator = default(List<UltimateLevelOfDetail>.Enumerator);
			object obj = default(object);
			do
			{
				if (enumerator.MoveNext())
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
					continue;
				}
				enumerator.Dispose();
				return;
			}
			while (obj != null);
			throw new NullReferenceException();
		}
		Debug.LogError("It is not possible to define a custom camera for all ULODs components in this scene. This method is only usable at run time.");
	}

	public unsafe void ConvertThisToDefaultUnityLods()
	{
		//IL_1740: Expected O, but got I4
		//IL_0084: Expected I, but got O
		//IL_00a9: Expected O, but got I
		//IL_0286: Expected O, but got I
		//IL_029d: Expected O, but got I
		//IL_00ec: Expected I, but got O
		//IL_00f1: Expected I, but got O
		//IL_0101: Expected O, but got I
		//IL_02cb: Expected O, but got I
		//IL_0120: Expected O, but got I
		//IL_0138: Expected I, but got O
		//IL_013d: Expected I, but got O
		//IL_014d: Expected O, but got I
		//IL_02e2: Expected O, but got I
		//IL_0182: Expected I, but got O
		//IL_031b: Expected I, but got O
		//IL_0320: Expected I, but got O
		//IL_0330: Expected O, but got I
		//IL_054b: Expected O, but got Ref
		//IL_0554: Expected O, but got I4
		//IL_034f: Expected O, but got I
		//IL_0367: Expected I, but got O
		//IL_036c: Expected I, but got O
		//IL_037c: Expected O, but got I
		//IL_0239: Expected I, but got O
		//IL_01d9: Expected O, but got I
		//IL_01fc: Expected O, but got I
		//IL_0202: Expected I, but got O
		//IL_0212: Expected O, but got I
		//IL_138c: Expected I, but got O
		//IL_03a7: Expected O, but got I
		//IL_03c1: Expected I, but got O
		//IL_0261: Expected O, but got I
		//IL_0493: Expected I, but got O
		//IL_05fa: Expected I, but got O
		//IL_0412: Expected I, but got O
		//IL_0433: Expected O, but got I
		//IL_0456: Expected O, but got I
		//IL_045c: Expected I, but got O
		//IL_046c: Expected O, but got I
		//IL_147b: Expected I, but got O
		//IL_061f: Expected O, but got I
		//IL_0dd7: Expected O, but got I
		//IL_0654: Expected O, but got I4
		//IL_065e: Expected I, but got O
		//IL_066e: Expected O, but got I
		//IL_1322: Expected O, but got Ref
		//IL_1330: Expected O, but got I4
		//IL_06a9: Expected O, but got I4
		//IL_06b3: Expected I, but got O
		//IL_06c3: Expected O, but got I
		//IL_0e0d: Expected O, but got I
		//IL_06e2: Expected O, but got I
		//IL_06fe: Expected O, but got I4
		//IL_0708: Expected I, but got O
		//IL_0718: Expected O, but got I
		//IL_179b: Expected O, but got I4
		//IL_17a5: Expected I, but got O
		//IL_0e8a: Expected O, but got I
		//IL_0761: Expected O, but got Ref
		//IL_17db: Expected O, but got I4
		//IL_17e5: Expected I, but got O
		//IL_1869: Expected I, but got O
		//IL_0786: Expected O, but got Ref
		//IL_0edd: Expected O, but got Ref
		//IL_181b: Expected O, but got I4
		//IL_1829: Expected I, but got O
		//IL_182e: Expected I, but got O
		//IL_1896: Expected I, but got O
		//IL_15be: Expected O, but got I
		//IL_07b4: Expected O, but got Ref
		//IL_0f02: Expected O, but got Ref
		//IL_07d6: Expected O, but got I
		//IL_07f6: Expected O, but got I4
		//IL_0801: Expected I, but got O
		//IL_18c3: Expected I, but got O
		//IL_0839: Expected O, but got I4
		//IL_0844: Expected I, but got O
		//IL_084c: Expected O, but got I4
		//IL_0f30: Expected O, but got Ref
		//IL_0872: Expected O, but got I4
		//IL_087d: Expected I, but got O
		//IL_0885: Expected O, but got I4
		//IL_0f52: Expected O, but got I
		//IL_08a8: Expected O, but got I
		//IL_08c8: Expected O, but got I4
		//IL_08dd: Expected I, but got O
		//IL_08ed: Expected O, but got I
		//IL_0f99: Expected O, but got I4
		//IL_0fa6: Expected I, but got O
		//IL_090c: Expected O, but got I
		//IL_0942: Expected O, but got I4
		//IL_094a: Expected I, but got O
		//IL_094f: Expected I, but got O
		//IL_095f: Expected O, but got I
		//IL_0fe6: Expected O, but got I
		//IL_097e: Expected O, but got I
		//IL_1008: Expected O, but got I
		//IL_09b4: Expected O, but got I4
		//IL_09bc: Expected I, but got O
		//IL_09c1: Expected I, but got O
		//IL_09d1: Expected O, but got I
		//IL_104f: Expected O, but got I4
		//IL_1055: Expected O, but got I
		//IL_105a: Expected I, but got O
		//IL_1062: Expected O, but got I4
		//IL_09f0: Expected O, but got I
		//IL_0a26: Expected O, but got I4
		//IL_0a33: Expected I, but got O
		//IL_0a43: Expected O, but got I
		//IL_1080: Expected O, but got I
		//IL_0a62: Expected O, but got I
		//IL_0a98: Expected O, but got I4
		//IL_0aa5: Expected I, but got O
		//IL_0ab5: Expected O, but got I
		//IL_0ad4: Expected O, but got I
		//IL_0b0a: Expected O, but got I4
		//IL_0b17: Expected I, but got O
		//IL_0b27: Expected O, but got I
		//IL_1102: Expected O, but got I
		//IL_0b46: Expected O, but got I
		//IL_0b7c: Expected O, but got I4
		//IL_0b89: Expected I, but got O
		//IL_0b99: Expected O, but got I
		//IL_114e: Expected O, but got I
		//IL_0bb8: Expected O, but got I
		//IL_0bde: Expected O, but got I
		//IL_0bfe: Expected O, but got I4
		//IL_0c0b: Expected I, but got O
		//IL_119a: Expected O, but got I
		//IL_0c43: Expected O, but got I4
		//IL_0c50: Expected I, but got O
		//IL_0c58: Expected O, but got I4
		//IL_11e6: Expected O, but got I
		//IL_0c96: Expected O, but got I4
		//IL_0c9b: Expected I, but got O
		//IL_0ca3: Expected O, but got I4
		//IL_0ce3: Expected O, but got I4
		//IL_0ce8: Expected I, but got O
		//IL_1221: Expected I, but got O
		//IL_1231: Expected O, but got I
		//IL_1253: Expected O, but got I4
		//IL_1258: Expected I, but got O
		//IL_0d06: Expected I, but got O
		//IL_0d3b: Expected O, but got I4
		//IL_0d40: Expected I, but got O
		//IL_1502: Expected I, but got O
		//IL_0d73: Expected O, but got I4
		//IL_0d78: Expected I, but got O
		Component component = (Component)(object)currentScannedMeshesList;
		bool flag = currentScannedMeshesList == null;
		LOD lOD = (LOD)0;
		if (!flag)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
			float num2 = default(float);
			float num = num2;
			LOD lOD2 = default(LOD);
			lOD = lOD2;
			nint num3 = 0;
			List<ScannedMeshItem>.Enumerator enumerator = default(List<ScannedMeshItem>.Enumerator);
			object obj = default(object);
			LOD lOD4 = default(LOD);
			int num7 = default(int);
			LOD zeroVector2 = default(LOD);
			LOD zeroVector3 = default(LOD);
			LOD oneVector = default(LOD);
			Vector3 zeroVector4 = default(Vector3);
			Vector3 zeroVector5 = default(Vector3);
			LOD oneVector2 = default(LOD);
			object obj10 = default(object);
			UnityEngine.Object obj11 = default(UnityEngine.Object);
			while (enumerator.MoveNext())
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
				List<LOD> list = new List<LOD>();
				Renderer[] array = new Renderer[1];
				bool flag2 = obj == null;
				nint num4 = (nint)typeof(Renderer[]);
				if (!flag2)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v228 @ stack_-130_v59+18]");
					bool flag3 = (UnityEngine.Object)0 != null;
					bool flag4 = !flag3;
					LODGroup lODGroup = null;
					LODGroup lODGroup2 = null;
					if (!flag4)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v228 @ stack_-130_v59+18]");
						bool flag5 = (nint)0 == 0;
						nint num5 = unchecked((nint)null);
						num3 = unchecked((nint)null);
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v228 @ stack_-130_v59+18]");
						GameObject gameObject = (GameObject)0;
						if (flag5)
						{
							throw new NullReferenceException();
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v228 @ stack_-130_v59+18]");
						GameObject gameObject2 = ((Component)0).gameObject;
						bool flag6 = (object)gameObject2 == null;
						num5 = unchecked((nint)null);
						num3 = unchecked((nint)null);
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v228 @ stack_-130_v59+18]");
						gameObject = (GameObject)0;
						if (flag6)
						{
							throw new NullReferenceException();
						}
						LODGroup lODGroup3 = gameObject2.AddComponent<LODGroup>();
						bool flag7 = array == null;
						num5 = 0;
						num3 = unchecked((nint)null);
						gameObject = gameObject2;
						if (flag7)
						{
							throw new NullReferenceException();
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v228 @ stack_-130_v59+18]");
						bool flag8 = (nint)0 == 0;
						Component component2 = (Component)(object)gameObject2;
						if (!flag8)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v228 @ stack_-130_v59+18]");
							List<ScannedMeshItem>.Enumerator enumerator2 = ((List<ScannedMeshItem>)0).GetEnumerator();
							bool flag9 = (object)enumerator2 == null;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v228 @ stack_-130_v59+18]");
							component2 = (Component)0;
							num3 = unchecked((nint)null);
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v228 @ stack_-130_v59+18]");
							component = (Component)0;
							if (flag9)
							{
								List<ScannedMeshItem>.Enumerator enumerator3 = ((List<ScannedMeshItem>)(object)component).GetEnumerator();
								component2 = (Component)enumerator3;
								throw enumerator3;
							}
						}
						bool flag10 = array.Length <= 0;
						num3 = unchecked((nint)null);
						if (flag10)
						{
							throw new IndexOutOfRangeException();
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v228 @ stack_-130_v59+18]");
						array[0] = (Renderer)0;
						lODGroup = lODGroup3;
						lODGroup2 = lODGroup3;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v228 @ stack_-130_v59+28]");
					UnityEngine.Object obj2 = (UnityEngine.Object)0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v228 @ stack_-130_v59+28]");
					if ((UnityEngine.Object)0 != null)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v228 @ stack_-130_v59+20]");
						obj2 = (UnityEngine.Object)0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v228 @ stack_-130_v59+20]");
						if ((UnityEngine.Object)0 != null)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v228 @ stack_-130_v59+28]");
							bool flag11 = (nint)0 == 0;
							nint num5 = unchecked((nint)null);
							num3 = unchecked((nint)null);
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v228 @ stack_-130_v59+28]");
							GameObject gameObject = (GameObject)0;
							if (flag11)
							{
								throw new NullReferenceException();
							}
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v228 @ stack_-130_v59+28]");
							GameObject gameObject3 = ((Component)0).gameObject;
							bool flag12 = (object)gameObject3 == null;
							num5 = unchecked((nint)null);
							num3 = unchecked((nint)null);
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v228 @ stack_-130_v59+28]");
							gameObject = (GameObject)0;
							if (flag12)
							{
								throw new NullReferenceException();
							}
							LODGroup lODGroup4 = gameObject3.AddComponent<LODGroup>();
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v228 @ stack_-130_v59+28]");
							obj2 = (UnityEngine.Object)0;
							bool flag13 = array == null;
							num5 = 0;
							num3 = unchecked((nint)null);
							gameObject = gameObject3;
							if (flag13)
							{
								throw new NullReferenceException();
							}
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v228 @ stack_-130_v59+28]");
							bool flag14 = (nint)0 == 0;
							num5 = 0;
							gameObject = gameObject3;
							if (!flag14)
							{
								nint num6 = (nint)array;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2367 @ rdx_v161 (Il2CppClass<UnityEngine.Renderer[]>)+40]");
								num5 = 0;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v228 @ stack_-130_v59+28]");
								LODGroup lODGroup5 = ((GameObject)0).AddComponent<LODGroup>();
								bool flag15 = (object)lODGroup5 == null;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v228 @ stack_-130_v59+28]");
								gameObject = (GameObject)0;
								num3 = unchecked((nint)null);
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v228 @ stack_-130_v59+28]");
								Component component2 = (Component)0;
								if (flag15)
								{
									GameObject enumerator4 = (GameObject)((List<ScannedMeshItem>)(object)component2).GetEnumerator();
									num5 = unchecked((nint)null);
									gameObject = enumerator4;
									throw enumerator4;
								}
							}
							bool flag16 = array.Length <= 0;
							num3 = unchecked((nint)null);
							if (flag16)
							{
								throw new IndexOutOfRangeException();
							}
							array[0] = (Renderer)obj2;
							lODGroup = lODGroup4;
							lODGroup2 = lODGroup4;
						}
					}
					if ((object)lODGroup2 != null)
					{
						lODGroup2.fadeMode = LODFadeMode.CrossFade;
						lODGroup2.animateCrossFading = true;
						LOD lOD3 = new LOD(0.7f, array);
						bool flag17 = list == null;
						num4 = (nint)(&lOD3);
						if (!flag17)
						{
							list.Add((LOD)(&lOD4));
							object obj3 = 0;
							num = 0.7f;
							lOD = lOD3;
							for (int i = 0; i <= levelsOfDetailToGenerate; i++)
							{
								if (i == 0)
								{
									continue;
								}
								string text = num7.ToString();
								string text2 = "LOD " + text + " (Generated By ULOD)";
								GameObject gameObject4 = new GameObject(text2);
								Renderer[] array2 = new Renderer[1];
								bool flag18 = obj == null;
								num4 = (nint)typeof(Renderer[]);
								if (!flag18)
								{
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v228 @ stack_-130_v59+18]");
									if ((UnityEngine.Object)0 != null)
									{
										bool flag19 = (object)gameObject4 == null;
										obj3 = 0;
										UnityEngine.Object obj4 = null;
										num3 = unchecked((nint)null);
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v228 @ stack_-130_v59+18]");
										UnityEngine.Object obj5 = (UnityEngine.Object)0;
										if (flag19)
										{
											throw new NullReferenceException();
										}
										Transform transform = gameObject4.transform;
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v228 @ stack_-130_v59+18]");
										bool flag20 = (nint)0 == 0;
										obj3 = 0;
										obj4 = null;
										num3 = unchecked((nint)null);
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v228 @ stack_-130_v59+18]");
										obj5 = (UnityEngine.Object)0;
										if (flag20)
										{
											throw new NullReferenceException();
										}
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v228 @ stack_-130_v59+18]");
										Transform parentInternal = ((Component)0).transform;
										bool flag21 = (object)transform == null;
										obj3 = 0;
										obj4 = null;
										num3 = unchecked((nint)null);
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v228 @ stack_-130_v59+18]");
										obj5 = (UnityEngine.Object)0;
										if (flag21)
										{
											throw new NullReferenceException();
										}
										transform.parentInternal = parentInternal;
										Transform transform2 = gameObject4.transform;
										bool flag22 = (object)transform2 == null;
										obj3 = 0;
										obj4 = null;
										num3 = unchecked((nint)null);
										obj5 = (UnityEngine.Object)(object)typeof(Vector3);
										if (flag22)
										{
											throw new NullReferenceException();
										}
										LOD zeroVector = (LOD)Vector3.zeroVector;
										transform2.localPosition = (Vector3)(&zeroVector2);
										Transform transform3 = gameObject4.transform;
										bool flag23 = (object)transform3 == null;
										obj3 = 0;
										obj4 = null;
										num3 = unchecked((nint)null);
										obj5 = (UnityEngine.Object)(object)typeof(Vector3);
										if (flag23)
										{
											lOD = zeroVector;
											throw new NullReferenceException();
										}
										transform3.localEulerAngles = (Vector3)(&zeroVector3);
										Transform transform4 = gameObject4.transform;
										bool flag24 = (object)transform4 == null;
										obj3 = 0;
										lOD = (LOD)Vector3.zeroVector;
										nint num5 = unchecked((nint)null);
										num3 = unchecked((nint)null);
										GameObject typeFromHandle = (GameObject)(object)typeof(Vector3);
										if (flag24)
										{
											zeroVector = lOD;
											obj4 = (UnityEngine.Object)num5;
											obj5 = typeFromHandle;
											throw new NullReferenceException();
										}
										lOD = (LOD)Vector3.oneVector;
										transform4.localScale = (Vector3)(&oneVector);
										SkinnedMeshRenderer skinnedMeshRenderer = gameObject4.AddComponent<SkinnedMeshRenderer>();
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v228 @ stack_-130_v59+30]");
										object obj6 = 0;
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v228 @ stack_-130_v59+30]");
										bool flag25 = (nint)0 == 0;
										obj3 = 0;
										num5 = 0;
										num3 = unchecked((nint)null);
										typeFromHandle = gameObject4;
										if (flag25)
										{
											throw new NullReferenceException();
										}
										int num8 = num7;
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1008 @ rax_v242+18]");
										bool flag26 = (nint)num8 >= (nint)0;
										obj3 = 0;
										num5 = 0;
										num3 = unchecked((nint)null);
										GameObject gameObject = (GameObject)num7;
										if (flag26)
										{
											throw new IndexOutOfRangeException();
										}
										bool flag27 = (object)skinnedMeshRenderer == null;
										obj3 = 0;
										num5 = 0;
										num3 = unchecked((nint)null);
										typeFromHandle = (GameObject)num7;
										if (flag27)
										{
											throw new NullReferenceException();
										}
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1008 @ rax_v242+20+v952 @ stack_18_v63 (System.Int32)*8]");
										skinnedMeshRenderer.sharedMesh = (Mesh)0;
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v228 @ stack_-130_v59+18]");
										bool flag28 = (nint)0 == 0;
										obj3 = 0;
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1008 @ rax_v242+20+v952 @ stack_18_v63 (System.Int32)*8]");
										num5 = 0;
										num3 = unchecked((nint)null);
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v228 @ stack_-130_v59+18]");
										typeFromHandle = (GameObject)0;
										if (flag28)
										{
											throw new NullReferenceException();
										}
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v228 @ stack_-130_v59+18]");
										Transform[] array3 = (skinnedMeshRenderer.bones = ((SkinnedMeshRenderer)0).bones);
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v228 @ stack_-130_v59+18]");
										bool flag29 = (nint)0 == 0;
										obj3 = 0;
										num5 = (nint)array3;
										num3 = unchecked((nint)null);
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v228 @ stack_-130_v59+18]");
										typeFromHandle = (GameObject)0;
										if (flag29)
										{
											throw new NullReferenceException();
										}
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v228 @ stack_-130_v59+18]");
										Transform transform5 = (skinnedMeshRenderer.rootBone = ((SkinnedMeshRenderer)0).rootBone);
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v228 @ stack_-130_v59+18]");
										bool flag30 = (nint)0 == 0;
										obj3 = 0;
										num5 = (nint)transform5;
										num3 = unchecked((nint)null);
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v228 @ stack_-130_v59+18]");
										typeFromHandle = (GameObject)0;
										if (flag30)
										{
											throw new NullReferenceException();
										}
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v228 @ stack_-130_v59+18]");
										bool flag31 = (skinnedMeshRenderer.updateWhenOffscreen = ((SkinnedMeshRenderer)0).updateWhenOffscreen);
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v228 @ stack_-130_v59+18]");
										bool flag32 = (nint)0 == 0;
										obj3 = 0;
										num5 = (flag31 ? 1 : 0);
										num3 = unchecked((nint)null);
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v228 @ stack_-130_v59+18]");
										typeFromHandle = (GameObject)0;
										if (flag32)
										{
											throw new NullReferenceException();
										}
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v228 @ stack_-130_v59+18]");
										bool flag33 = (skinnedMeshRenderer.receiveShadows = ((Renderer)0).receiveShadows);
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v228 @ stack_-130_v59+18]");
										bool flag34 = (nint)0 == 0;
										obj3 = 0;
										num5 = (flag33 ? 1 : 0);
										num3 = unchecked((nint)null);
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v228 @ stack_-130_v59+18]");
										typeFromHandle = (GameObject)0;
										if (flag34)
										{
											throw new NullReferenceException();
										}
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v228 @ stack_-130_v59+18]");
										ShadowCastingMode shadowCastingMode = (skinnedMeshRenderer.shadowCastingMode = ((Renderer)0).shadowCastingMode);
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v228 @ stack_-130_v59+18]");
										bool flag35 = (nint)0 == 0;
										obj3 = 0;
										num5 = (nint)shadowCastingMode;
										num3 = unchecked((nint)null);
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v228 @ stack_-130_v59+18]");
										typeFromHandle = (GameObject)0;
										if (flag35)
										{
											throw new NullReferenceException();
										}
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v228 @ stack_-130_v59+18]");
										bool flag36 = (skinnedMeshRenderer.skinnedMotionVectors = ((SkinnedMeshRenderer)0).skinnedMotionVectors);
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v228 @ stack_-130_v59+18]");
										bool flag37 = (nint)0 == 0;
										obj3 = 0;
										num5 = (flag36 ? 1 : 0);
										num3 = unchecked((nint)null);
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v228 @ stack_-130_v59+18]");
										typeFromHandle = (GameObject)0;
										if (flag37)
										{
											throw new NullReferenceException();
										}
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v228 @ stack_-130_v59+18]");
										bool flag38 = (skinnedMeshRenderer.allowOcclusionWhenDynamic = ((Renderer)0).allowOcclusionWhenDynamic);
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v228 @ stack_-130_v59+48]");
										object obj7 = 0;
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v228 @ stack_-130_v59+48]");
										bool flag39 = (nint)0 == 0;
										obj3 = 0;
										num5 = (flag38 ? 1 : 0);
										num3 = unchecked((nint)null);
										typeFromHandle = (GameObject)(object)skinnedMeshRenderer;
										if (flag39)
										{
											throw new NullReferenceException();
										}
										int num9 = num7;
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1485 @ rax_v258+18]");
										bool flag40 = (nint)num9 >= (nint)0;
										obj3 = 0;
										num5 = (flag38 ? 1 : 0);
										num3 = unchecked((nint)null);
										gameObject = (GameObject)num7;
										if (flag40)
										{
											throw new IndexOutOfRangeException();
										}
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1485 @ rax_v258+20+v952 @ stack_18_v63 (System.Int32)*8]");
										num5 = 0;
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1485 @ rax_v258+20+v952 @ stack_18_v63 (System.Int32)*8]");
										bool flag41 = (nint)0 == 0;
										obj3 = 0;
										num3 = unchecked((nint)null);
										typeFromHandle = (GameObject)num7;
										if (flag41)
										{
											throw new NullReferenceException();
										}
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3002 @ rdx_v32 (Il2CppMethodInfo)+10]");
										num5 = 0;
										Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181D9C660");
										bool flag42 = array2 == null;
										obj3 = 0;
										num3 = unchecked((nint)null);
										typeFromHandle = (GameObject)(object)skinnedMeshRenderer;
										if (flag42)
										{
											throw new NullReferenceException();
										}
										nint num10 = (nint)array2;
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4816 @ rdx_v153 (Il2CppClass<UnityEngine.LODGroup>)+40]");
										num5 = 0;
										LODGroup lODGroup6 = ((GameObject)(object)skinnedMeshRenderer).AddComponent<LODGroup>();
										bool flag43 = (object)lODGroup6 == null;
										obj3 = 0;
										num3 = unchecked((nint)null);
										gameObject = (GameObject)(object)skinnedMeshRenderer;
										if (flag43)
										{
											LODGroup lODGroup7 = gameObject.AddComponent<LODGroup>();
											num5 = unchecked((nint)null);
											typeFromHandle = (GameObject)(object)lODGroup7;
											throw lODGroup7;
										}
										bool flag44 = array2.Length <= 0;
										obj3 = 0;
										num3 = unchecked((nint)null);
										typeFromHandle = (GameObject)(object)skinnedMeshRenderer;
										if (flag44)
										{
											throw new IndexOutOfRangeException();
										}
										array2[0] = skinnedMeshRenderer;
										oneVector = (LOD)Vector3.oneVector;
										zeroVector3 = (LOD)Vector3.zeroVector;
										zeroVector2 = (LOD)Vector3.zeroVector;
									}
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v228 @ stack_-130_v59+28]");
									if ((UnityEngine.Object)0 != null)
									{
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v228 @ stack_-130_v59+20]");
										if ((UnityEngine.Object)0 != null)
										{
											if ((object)gameObject4 == null)
											{
												throw new NullReferenceException();
											}
											Transform transform6 = gameObject4.transform;
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v228 @ stack_-130_v59+28]");
											if ((nint)0 == 0)
											{
												throw new NullReferenceException();
											}
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v228 @ stack_-130_v59+28]");
											Transform parentInternal2 = ((Component)0).transform;
											if ((object)transform6 == null)
											{
												throw new NullReferenceException();
											}
											transform6.parentInternal = parentInternal2;
											Transform transform7 = gameObject4.transform;
											bool flag45 = (object)transform7 == null;
											num4 = (nint)typeof(Vector3);
											if (flag45)
											{
												throw new NullReferenceException();
											}
											transform7.localPosition = (Vector3)(&zeroVector4);
											Transform transform8 = gameObject4.transform;
											bool flag46 = (object)transform8 == null;
											num4 = (nint)typeof(Vector3);
											if (flag46)
											{
												throw new NullReferenceException();
											}
											transform8.localEulerAngles = (Vector3)(&zeroVector5);
											Transform transform9 = gameObject4.transform;
											bool flag47 = (object)transform9 == null;
											num4 = (nint)typeof(Vector3);
											if (flag47)
											{
												throw new NullReferenceException();
											}
											lOD = (LOD)Vector3.oneVector;
											transform9.localScale = (Vector3)(&oneVector2);
											MeshFilter meshFilter = gameObject4.AddComponent<MeshFilter>();
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v228 @ stack_-130_v59+30]");
											GameObject typeFromHandle = (GameObject)0;
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v228 @ stack_-130_v59+30]");
											if ((nint)0 == 0)
											{
												throw new NullReferenceException();
											}
											int num11 = num7;
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3006 @ rcx_v34 (UnityEngine.GameObject)+18]");
											bool flag48 = (nint)num11 >= (nint)0;
											obj3 = 0;
											nint num5 = num7;
											num3 = unchecked((nint)null);
											if (flag48)
											{
												throw new IndexOutOfRangeException();
											}
											if ((object)meshFilter == null)
											{
												throw new NullReferenceException();
											}
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3006 @ rcx_v34 (UnityEngine.GameObject)+20+v952 @ stack_18_v63 (System.Int32)*8]");
											meshFilter.mesh = (Mesh)0;
											MeshRenderer meshRenderer = gameObject4.AddComponent<MeshRenderer>();
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v228 @ stack_-130_v59+48]");
											object obj8 = 0;
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v228 @ stack_-130_v59+48]");
											if ((nint)0 == 0)
											{
												throw new NullReferenceException();
											}
											int num12 = num7;
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3476 @ rax_v206+18]");
											bool flag49 = (nint)num12 >= (nint)0;
											obj3 = 0;
											UnityEngine.Object obj4 = (UnityEngine.Object)0;
											num3 = unchecked((nint)null);
											UnityEngine.Object obj5 = (UnityEngine.Object)num7;
											if (flag49)
											{
												throw new IndexOutOfRangeException();
											}
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3476 @ rax_v206+20+v952 @ stack_18_v63 (System.Int32)*8]");
											object obj9 = 0;
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3476 @ rax_v206+20+v952 @ stack_18_v63 (System.Int32)*8]");
											if ((nint)0 == 0)
											{
												throw new NullReferenceException();
											}
											if ((object)meshRenderer == null)
											{
												throw new NullReferenceException();
											}
											Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181D9C660");
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v228 @ stack_-130_v59+28]");
											if ((nint)0 == 0)
											{
												throw new NullReferenceException();
											}
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v228 @ stack_-130_v59+28]");
											ShadowCastingMode shadowCastingMode3 = ((Renderer)0).shadowCastingMode;
											meshRenderer.shadowCastingMode = shadowCastingMode3;
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v228 @ stack_-130_v59+28]");
											if ((nint)0 == 0)
											{
												throw new NullReferenceException();
											}
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v228 @ stack_-130_v59+28]");
											bool receiveShadows2 = ((Renderer)0).receiveShadows;
											meshRenderer.receiveShadows = receiveShadows2;
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v228 @ stack_-130_v59+28]");
											if ((nint)0 == 0)
											{
												throw new NullReferenceException();
											}
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v228 @ stack_-130_v59+28]");
											MotionVectorGenerationMode motionVectorGenerationMode = ((Renderer)0).motionVectorGenerationMode;
											meshRenderer.motionVectorGenerationMode = motionVectorGenerationMode;
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v228 @ stack_-130_v59+28]");
											if ((nint)0 == 0)
											{
												throw new NullReferenceException();
											}
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v228 @ stack_-130_v59+28]");
											bool allowOcclusionWhenDynamic2 = ((Renderer)0).allowOcclusionWhenDynamic;
											meshRenderer.allowOcclusionWhenDynamic = allowOcclusionWhenDynamic2;
											if (array2 == null)
											{
												throw new NullReferenceException();
											}
											nint num13 = (nint)array2;
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4815 @ rdx_v123 (Il2CppClass<UnityEngine.LODGroup>)+40]");
											obj4 = (UnityEngine.Object)0;
											Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
											bool flag50 = obj10 == null;
											obj3 = 0;
											num3 = unchecked((nint)null);
											obj5 = meshRenderer;
											if (flag50)
											{
												Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
												throw obj11;
											}
											if (array2.Length <= 0)
											{
												throw new IndexOutOfRangeException();
											}
											array2[0] = meshRenderer;
											oneVector2 = (LOD)Vector3.oneVector;
											zeroVector5 = Vector3.zeroVector;
											zeroVector4 = Vector3.zeroVector;
										}
									}
									float num14 = 0.3f / (float)levelsOfDetailToGenerate;
									float num15 = num14 * 0.97f;
									float num16 = num15 * (float)num7;
									num = 0.3f - num16;
									LOD lOD5 = new LOD(num, array2);
									list.Add((LOD)(&lOD2));
									obj3 = 0;
									obj2 = gameObject4;
									lODGroup2 = (LODGroup)(object)array2;
									lOD = lOD5;
									lOD2 = lOD5;
									i = num7;
									continue;
								}
								throw new NullReferenceException();
							}
							LOD[] lODs = list.ToArray();
							lODGroup.SetLODs(lODs);
							lODGroup.RecalculateBounds();
							num3 = unchecked((nint)null);
							continue;
						}
						throw new NullReferenceException();
					}
					throw new NullReferenceException();
				}
				throw new NullReferenceException();
			}
			enumerator.Dispose();
			GameObject gameObject5 = base.gameObject;
			bool flag51 = (object)gameObject5 == null;
			component = this;
			if (!flag51)
			{
				string text3 = gameObject5.name;
				bool runUnityGc = default(bool);
				UndoAllMeshesScannedAndAllLodGroups(showProgressBar: false, deleteAllGeneratedMeshes: false, runMonoIl2CppGc: true, runUnityGc);
				if (!Application.isPlaying)
				{
					UnityEngine.Object.DestroyImmediate(this);
				}
				if (Application.isPlaying)
				{
					UnityEngine.Object.Destroy(this);
				}
				string message = "The Ultimate Level Of Detail component in \"" + text3 + "\" has been removed and all scanned meshes are now managed by Unity's standard \"LOD Group\" components.";
				Debug.Log(message);
				return;
			}
		}
		throw new NullReferenceException();
	}

	public UltimateLevelOfDetail()
	{
		//IL_001f: Expected I4, but got I8
		//IL_0105: Expected O, but got I
		WaitForEndOfFrame wAIT_FOR_END_OF_FRAME = new WaitForEndOfFrame();
		WAIT_FOR_END_OF_FRAME = wAIT_FOR_END_OF_FRAME;
		lastDistanceFromMainCamera = -1f;
		forcedToShowOnlyALodIndependentOfDistance = -1;
		currentScannedMeshesList = new List<ScannedMeshItem>();
		gameObjectsToIgnore = new List<GameObject>();
		levelsOfDetailToGenerate = 3;
		percentOfVerticesForEachLod = new float[9] { 100f, 80f, 70f, 55f, 35f, 25f, 15f, 10f, 5f };
		saveGeneratedLodsInAssets = true;
		preventArtifacts = true;
		cullingMode = CullingMode.CullingMeshes;
		useCacheForMainCameraInDetection = true;
		minDistanceOfViewForEachLod = new float[9] { 0f, 30f, 70f, 120f, 150f, 180f, 200f, 220f, 250f };
		minDistanceOfViewForCull = 270f;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206E40]");
		colorOfGizmo = (Color)0;
		sizeOfGizmo = 0.2f;
		base._002Ector();
	}
}

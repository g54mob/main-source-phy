using System;
using System.Collections.Generic;
using System.Globalization;
using FluffyUnderware.DevTools;
using FluffyUnderware.DevTools.Extensions;
using UnityEngine;
using UnityEngine.Rendering;

namespace FluffyUnderware.Curvy.Generator.Modules
{
	[ModuleInfo("Create/Mesh", ModuleName = "Create Mesh")]
	[HelpURL("https://curvyeditor.com/doclink/cgcreatemesh")]
	public class CreateMesh : CGModule
	{
		private const string DefaultTag = "Untagged";

		[HideInInspector]
		[InputSlotInfo(new Type[] { typeof(CGVMesh) }, Array = true, Name = "VMesh")]
		public CGModuleInputSlot InVMeshArray = new CGModuleInputSlot();

		[HideInInspector]
		[InputSlotInfo(new Type[] { typeof(CGSpots) }, Name = "Spots", Optional = true)]
		public CGModuleInputSlot InSpots = new CGModuleInputSlot();

		[SerializeField]
		[CGResourceCollectionManager("Mesh", ShowCount = true)]
		private CGMeshResourceCollection m_MeshResources = new CGMeshResourceCollection();

		[Tab("General")]
		[Tooltip("Merge meshes")]
		[FieldCondition("canUpdate", true, false, ActionAttribute.ActionEnum.Show, null, ActionAttribute.ActionPositionEnum.Below, Action = ActionAttribute.ActionEnum.Enable)]
		[SerializeField]
		private bool m_Combine;

		[Tooltip("Merge meshes sharing the same Index")]
		[SerializeField]
		private bool m_GroupMeshes = true;

		[SerializeField]
		[FieldCondition("canUpdate", true, false, ActionAttribute.ActionEnum.Show, null, ActionAttribute.ActionPositionEnum.Below, Action = ActionAttribute.ActionEnum.Enable)]
		private CGYesNoAuto m_AddNormals = CGYesNoAuto.Auto;

		[SerializeField]
		[FieldCondition("canUpdate", true, false, ActionAttribute.ActionEnum.Show, null, ActionAttribute.ActionPositionEnum.Below, Action = ActionAttribute.ActionEnum.Enable)]
		private CGYesNoAuto m_AddTangents = CGYesNoAuto.No;

		[SerializeField]
		[FieldCondition("canUpdate", true, false, ActionAttribute.ActionEnum.Show, null, ActionAttribute.ActionPositionEnum.Below, Action = ActionAttribute.ActionEnum.Enable)]
		private bool m_AddUV2 = true;

		[SerializeField]
		[Tooltip("If enabled, meshes will have the Static flag set, and will not be updated in Play Mode")]
		[FieldCondition("canModifyStaticFlag", true, false, ActionAttribute.ActionEnum.Show, null, ActionAttribute.ActionPositionEnum.Below, Action = ActionAttribute.ActionEnum.Enable)]
		private bool m_MakeStatic;

		[SerializeField]
		[Tooltip("The Layer of the created game object")]
		[FieldCondition("canUpdate", true, false, ActionAttribute.ActionEnum.Show, null, ActionAttribute.ActionPositionEnum.Below, Action = ActionAttribute.ActionEnum.Enable)]
		[Layer("", "")]
		private int m_Layer;

		[SerializeField]
		[Tooltip("The Tag of the created game object")]
		[FieldCondition("canUpdate", true, false, ActionAttribute.ActionEnum.Show, null, ActionAttribute.ActionPositionEnum.Below, Action = ActionAttribute.ActionEnum.Enable)]
		[Tag("", "")]
		private string m_Tag = "Untagged";

		[Tab("Renderer")]
		[SerializeField]
		[FieldCondition("canUpdate", true, false, ActionAttribute.ActionEnum.Show, null, ActionAttribute.ActionPositionEnum.Below, Action = ActionAttribute.ActionEnum.Enable)]
		private bool m_RendererEnabled = true;

		[SerializeField]
		[FieldCondition("canUpdate", true, false, ActionAttribute.ActionEnum.Show, null, ActionAttribute.ActionPositionEnum.Below, Action = ActionAttribute.ActionEnum.Enable)]
		private ShadowCastingMode m_CastShadows = ShadowCastingMode.On;

		[SerializeField]
		[FieldCondition("canUpdate", true, false, ActionAttribute.ActionEnum.Show, null, ActionAttribute.ActionPositionEnum.Below, Action = ActionAttribute.ActionEnum.Enable)]
		private bool m_ReceiveShadows = true;

		[SerializeField]
		[FieldCondition("canUpdate", true, false, ActionAttribute.ActionEnum.Show, null, ActionAttribute.ActionPositionEnum.Below, Action = ActionAttribute.ActionEnum.Enable)]
		private LightProbeUsage m_LightProbeUsage = LightProbeUsage.BlendProbes;

		[HideInInspector]
		[SerializeField]
		[FieldCondition("canUpdate", true, false, ActionAttribute.ActionEnum.Show, null, ActionAttribute.ActionPositionEnum.Below, Action = ActionAttribute.ActionEnum.Enable)]
		private bool m_UseLightProbes = true;

		[SerializeField]
		[FieldCondition("canUpdate", true, false, ActionAttribute.ActionEnum.Show, null, ActionAttribute.ActionPositionEnum.Below, Action = ActionAttribute.ActionEnum.Enable)]
		private ReflectionProbeUsage m_ReflectionProbes = ReflectionProbeUsage.BlendProbes;

		[SerializeField]
		[FieldCondition("canUpdate", true, false, ActionAttribute.ActionEnum.Show, null, ActionAttribute.ActionPositionEnum.Below, Action = ActionAttribute.ActionEnum.Enable)]
		private Transform m_AnchorOverride;

		[Tab("Collider")]
		[SerializeField]
		[FieldCondition("canUpdate", true, false, ActionAttribute.ActionEnum.Show, null, ActionAttribute.ActionPositionEnum.Below, Action = ActionAttribute.ActionEnum.Enable)]
		private CGColliderEnum m_Collider = CGColliderEnum.Mesh;

		[FieldCondition("m_Collider", CGColliderEnum.Mesh, false, ActionAttribute.ActionEnum.Show, null, ActionAttribute.ActionPositionEnum.Below)]
		[SerializeField]
		[FieldCondition("canUpdate", true, false, ActionAttribute.ActionEnum.Show, null, ActionAttribute.ActionPositionEnum.Below, Action = ActionAttribute.ActionEnum.Enable)]
		private bool m_Convex;

		[Tooltip("Options used to enable or disable certain features in Collider mesh cooking. See Unity's MeshCollider.cookingOptions for more details")]
		[FieldCondition("m_Collider", CGColliderEnum.Mesh, false, ActionAttribute.ActionEnum.Show, null, ActionAttribute.ActionPositionEnum.Below)]
		[SerializeField]
		[EnumFlag("", "")]
		[FieldCondition("canUpdate", true, false, ActionAttribute.ActionEnum.Show, null, ActionAttribute.ActionPositionEnum.Below, Action = ActionAttribute.ActionEnum.Enable)]
		private MeshColliderCookingOptions m_CookingOptions = MeshColliderCookingOptions.CookForFasterSimulation | MeshColliderCookingOptions.EnableMeshCleaning | MeshColliderCookingOptions.WeldColocatedVertices;

		[Label("Auto Update", "")]
		[SerializeField]
		private bool m_AutoUpdateColliders = true;

		[SerializeField]
		private PhysicMaterial m_Material;

		private int mCurrentMeshCount;

		public bool Combine
		{
			get
			{
				return m_Combine;
			}
			set
			{
				if (m_Combine != value)
				{
					m_Combine = value;
				}
				base.Dirty = true;
			}
		}

		public bool GroupMeshes
		{
			get
			{
				return m_GroupMeshes;
			}
			set
			{
				if (m_GroupMeshes != value)
				{
					m_GroupMeshes = value;
				}
				base.Dirty = true;
			}
		}

		public CGYesNoAuto AddNormals
		{
			get
			{
				return m_AddNormals;
			}
			set
			{
				if (m_AddNormals != value)
				{
					m_AddNormals = value;
				}
				base.Dirty = true;
			}
		}

		public CGYesNoAuto AddTangents
		{
			get
			{
				return m_AddTangents;
			}
			set
			{
				if (m_AddTangents != value)
				{
					m_AddTangents = value;
				}
				base.Dirty = true;
			}
		}

		public bool AddUV2
		{
			get
			{
				return m_AddUV2;
			}
			set
			{
				if (m_AddUV2 != value)
				{
					m_AddUV2 = value;
				}
				base.Dirty = true;
			}
		}

		public int Layer
		{
			get
			{
				return m_Layer;
			}
			set
			{
				int num = Mathf.Clamp(value, 0, 32);
				if (m_Layer != num)
				{
					m_Layer = num;
				}
				base.Dirty = true;
			}
		}

		public string Tag
		{
			get
			{
				return m_Tag;
			}
			set
			{
				if (m_Tag != value)
				{
					m_Tag = value;
				}
				base.Dirty = true;
			}
		}

		public bool MakeStatic
		{
			get
			{
				return m_MakeStatic;
			}
			set
			{
				if (m_MakeStatic != value)
				{
					m_MakeStatic = value;
				}
				base.Dirty = true;
			}
		}

		public bool RendererEnabled
		{
			get
			{
				return m_RendererEnabled;
			}
			set
			{
				if (m_RendererEnabled != value)
				{
					m_RendererEnabled = value;
				}
				base.Dirty = true;
			}
		}

		public ShadowCastingMode CastShadows
		{
			get
			{
				return m_CastShadows;
			}
			set
			{
				if (m_CastShadows != value)
				{
					m_CastShadows = value;
				}
				base.Dirty = true;
			}
		}

		public bool ReceiveShadows
		{
			get
			{
				return m_ReceiveShadows;
			}
			set
			{
				if (m_ReceiveShadows != value)
				{
					m_ReceiveShadows = value;
				}
				base.Dirty = true;
			}
		}

		public bool UseLightProbes
		{
			get
			{
				return m_UseLightProbes;
			}
			set
			{
				if (m_UseLightProbes != value)
				{
					m_UseLightProbes = value;
				}
				base.Dirty = true;
			}
		}

		public LightProbeUsage LightProbeUsage
		{
			get
			{
				return m_LightProbeUsage;
			}
			set
			{
				if (m_LightProbeUsage != value)
				{
					m_LightProbeUsage = value;
				}
				base.Dirty = true;
			}
		}

		public ReflectionProbeUsage ReflectionProbes
		{
			get
			{
				return m_ReflectionProbes;
			}
			set
			{
				if (m_ReflectionProbes != value)
				{
					m_ReflectionProbes = value;
				}
				base.Dirty = true;
			}
		}

		public Transform AnchorOverride
		{
			get
			{
				return m_AnchorOverride;
			}
			set
			{
				if (m_AnchorOverride != value)
				{
					m_AnchorOverride = value;
				}
				base.Dirty = true;
			}
		}

		public CGColliderEnum Collider
		{
			get
			{
				return m_Collider;
			}
			set
			{
				if (m_Collider != value)
				{
					m_Collider = value;
				}
				base.Dirty = true;
			}
		}

		public bool AutoUpdateColliders
		{
			get
			{
				return m_AutoUpdateColliders;
			}
			set
			{
				if (m_AutoUpdateColliders != value)
				{
					m_AutoUpdateColliders = value;
				}
				base.Dirty = true;
			}
		}

		public bool Convex
		{
			get
			{
				return m_Convex;
			}
			set
			{
				if (m_Convex != value)
				{
					m_Convex = value;
				}
				base.Dirty = true;
			}
		}

		public MeshColliderCookingOptions CookingOptions
		{
			get
			{
				return m_CookingOptions;
			}
			set
			{
				if (m_CookingOptions != value)
				{
					m_CookingOptions = value;
				}
				base.Dirty = true;
			}
		}

		public PhysicMaterial Material
		{
			get
			{
				return m_Material;
			}
			set
			{
				if (m_Material != value)
				{
					m_Material = value;
				}
				base.Dirty = true;
			}
		}

		public CGMeshResourceCollection Meshes => m_MeshResources;

		public int MeshCount => Meshes.Count;

		public int VertexCount { get; private set; }

		private bool canGroupMeshes
		{
			get
			{
				if (InSpots.IsLinked)
				{
					return m_Combine;
				}
				return false;
			}
		}

		private bool canModifyStaticFlag => false;

		private bool canUpdate
		{
			get
			{
				if (Application.isPlaying)
				{
					return !MakeStatic;
				}
				return true;
			}
		}

		public override void Reset()
		{
			base.Reset();
			Combine = false;
			GroupMeshes = true;
			AddNormals = CGYesNoAuto.Auto;
			AddTangents = CGYesNoAuto.No;
			MakeStatic = false;
			Material = null;
			Convex = false;
			Layer = 0;
			Tag = "Untagged";
			CastShadows = ShadowCastingMode.On;
			RendererEnabled = true;
			ReceiveShadows = true;
			UseLightProbes = true;
			LightProbeUsage = LightProbeUsage.BlendProbes;
			ReflectionProbes = ReflectionProbeUsage.BlendProbes;
			AnchorOverride = null;
			Collider = CGColliderEnum.Mesh;
			AutoUpdateColliders = true;
			Convex = false;
			AddUV2 = true;
			CookingOptions = MeshColliderCookingOptions.CookForFasterSimulation | MeshColliderCookingOptions.EnableMeshCleaning | MeshColliderCookingOptions.WeldColocatedVertices;
			Clear();
		}

		public override void OnTemplateCreated()
		{
			Clear();
		}

		public void Clear()
		{
			mCurrentMeshCount = 0;
			removeUnusedResource();
			Resources.UnloadUnusedAssets();
		}

		public override void OnStateChange()
		{
			base.OnStateChange();
			if (!IsConfigured)
			{
				Clear();
			}
		}

		public override void Refresh()
		{
			base.Refresh();
			if (canUpdate)
			{
				List<CGVMesh> vMeshes = InVMeshArray.GetAllData<CGVMesh>(Array.Empty<CGDataRequestParameter>());
				CGSpots spots = InSpots.GetData<CGSpots>(Array.Empty<CGDataRequestParameter>());
				mCurrentMeshCount = 0;
				VertexCount = 0;
				if (vMeshes.Count > 0 && (!InSpots.IsLinked || (spots != null && spots.Count > 0)))
				{
					if (spots != null && spots.Count > 0)
					{
						createSpotMeshes(ref vMeshes, ref spots, Combine);
					}
					else
					{
						createMeshes(ref vMeshes, Combine);
					}
				}
				removeUnusedResource();
				if (AutoUpdateColliders)
				{
					UpdateColliders();
				}
			}
			else
			{
				UIMessages.Add("In Play Mode, and when Make Static is enabled, mesh generation is stopped to avoid overriding the optimizations Unity do to static game objects'meshs.");
			}
		}

		public GameObject SaveToScene(Transform parent = null)
		{
			GetManagedResources(out var components, out var _);
			if (components.Count == 0)
			{
				return null;
			}
			if (components.Count > 1)
			{
				Transform transform = new GameObject(base.ModuleName).transform;
				transform.transform.parent = ((parent == null) ? base.Generator.transform.parent : parent);
				for (int i = 0; i < components.Count; i++)
				{
					MeshFilter component = components[i].GetComponent<MeshFilter>();
					GameObject obj = components[i].gameObject.DuplicateGameObject(transform.transform);
					obj.name = components[i].name;
					obj.GetComponent<CGMeshResource>().Destroy();
					obj.GetComponent<MeshFilter>().sharedMesh = UnityEngine.Object.Instantiate(component.sharedMesh);
				}
				return transform.gameObject;
			}
			MeshFilter component2 = components[0].GetComponent<MeshFilter>();
			GameObject obj2 = components[0].gameObject.DuplicateGameObject(parent);
			obj2.name = components[0].name;
			obj2.GetComponent<CGMeshResource>().Destroy();
			obj2.GetComponent<MeshFilter>().sharedMesh = UnityEngine.Object.Instantiate(component2.sharedMesh);
			return obj2;
		}

		public void UpdateColliders()
		{
			bool flag = true;
			for (int i = 0; i < m_MeshResources.Count; i++)
			{
				if (!(m_MeshResources.Items[i] == null) && !m_MeshResources.Items[i].UpdateCollider(Collider, Convex, Material, CookingOptions))
				{
					flag = false;
				}
			}
			if (!flag)
			{
				UIMessages.Add("Error setting collider!");
			}
		}

		private void createMeshes(ref List<CGVMesh> vMeshes, bool combine)
		{
			if (combine && vMeshes.Count > 1)
			{
				int i = 0;
				while (i < vMeshes.Count)
				{
					int startIndex = i;
					int num;
					for (num = 0; i < vMeshes.Count && num + vMeshes[i].Count <= 65534; i++)
					{
						num += vMeshes[i].Count;
					}
					if (num == 0)
					{
						UIMessages.Add(string.Format(CultureInfo.InvariantCulture, "Mesh of index {0}, and subsequent ones, skipped because vertex count {2} > {1}", i, 65534, vMeshes[i].Count));
						break;
					}
					CGVMesh vmesh = new CGVMesh();
					vmesh.MergeVMeshes(vMeshes, startIndex, i - 1);
					writeVMeshToMesh(ref vmesh);
				}
				return;
			}
			for (int j = 0; j < vMeshes.Count; j++)
			{
				CGVMesh vmesh2 = vMeshes[j];
				if (vmesh2.Count < 65534)
				{
					writeVMeshToMesh(ref vmesh2);
				}
				else
				{
					UIMessages.Add(string.Format(CultureInfo.InvariantCulture, "Mesh of index {0} skipped because vertex count {2} > {1}", j, 65534, vmesh2.Count));
				}
			}
		}

		private void createSpotMeshes(ref List<CGVMesh> vMeshes, ref CGSpots spots, bool combine)
		{
			int num = 0;
			int count = vMeshes.Count;
			if (combine)
			{
				List<CGSpot> list = new List<CGSpot>(spots.Points);
				if (GroupMeshes)
				{
					list.Sort((CGSpot a, CGSpot b) => a.Index.CompareTo(b.Index));
				}
				CGSpot cGSpot = list[0];
				CGVMesh vmesh = new CGVMesh(vMeshes[cGSpot.Index]);
				if (cGSpot.Position != Vector3.zero || cGSpot.Rotation != Quaternion.identity || cGSpot.Scale != Vector3.one)
				{
					vmesh.TRS(cGSpot.Matrix);
				}
				for (int num2 = 1; num2 < list.Count; num2++)
				{
					cGSpot = list[num2];
					if (cGSpot.Index <= -1 || cGSpot.Index >= count)
					{
						continue;
					}
					if (vmesh.Count + vMeshes[cGSpot.Index].Count > 65534 || (GroupMeshes && cGSpot.Index != list[num2 - 1].Index))
					{
						writeVMeshToMesh(ref vmesh);
						vmesh = new CGVMesh(vMeshes[cGSpot.Index]);
						if (!cGSpot.Matrix.isIdentity)
						{
							vmesh.TRS(cGSpot.Matrix);
						}
					}
					else if (!cGSpot.Matrix.isIdentity)
					{
						vmesh.MergeVMesh(vMeshes[cGSpot.Index], cGSpot.Matrix);
					}
					else
					{
						vmesh.MergeVMesh(vMeshes[cGSpot.Index]);
					}
				}
				writeVMeshToMesh(ref vmesh);
			}
			else
			{
				for (int num3 = 0; num3 < spots.Count; num3++)
				{
					CGSpot cGSpot = spots.Points[num3];
					if (cGSpot.Index <= -1 || cGSpot.Index >= count)
					{
						continue;
					}
					if (vMeshes[cGSpot.Index].Count < 65535)
					{
						CGVMesh vmesh2 = vMeshes[cGSpot.Index];
						CGMeshResource cGMeshResource = writeVMeshToMesh(ref vmesh2);
						if (cGSpot.Position != Vector3.zero || cGSpot.Rotation != Quaternion.identity || cGSpot.Scale != Vector3.one)
						{
							cGSpot.ToTransform(cGMeshResource.Filter.transform);
						}
					}
					else
					{
						num++;
					}
				}
			}
			if (num > 0)
			{
				UIMessages.Add(string.Format(CultureInfo.InvariantCulture, "{0} meshes skipped (VertexCount>65534)", num));
			}
		}

		private CGMeshResource writeVMeshToMesh(ref CGVMesh vmesh)
		{
			bool num = AddNormals != CGYesNoAuto.No;
			bool flag = AddTangents != CGYesNoAuto.No;
			CGMeshResource newMesh = getNewMesh();
			if (canModifyStaticFlag)
			{
				newMesh.Filter.gameObject.isStatic = false;
			}
			Mesh msh = newMesh.Prepare();
			newMesh.gameObject.layer = Layer;
			newMesh.gameObject.tag = Tag;
			vmesh.ToMesh(ref msh);
			VertexCount += vmesh.Count;
			if (AddUV2 && !vmesh.HasUV2)
			{
				msh.uv2 = CGUtility.CalculateUV2(vmesh.UV);
			}
			if (num && !vmesh.HasNormals)
			{
				msh.RecalculateNormals();
			}
			if (flag && !vmesh.HasTangents)
			{
				newMesh.Filter.CalculateTangents();
			}
			newMesh.Filter.transform.localPosition = Vector3.zero;
			newMesh.Filter.transform.localRotation = Quaternion.identity;
			newMesh.Filter.transform.localScale = Vector3.one;
			if (canModifyStaticFlag)
			{
				newMesh.Filter.gameObject.isStatic = MakeStatic;
			}
			newMesh.Renderer.sharedMaterials = vmesh.GetMaterials();
			return newMesh;
		}

		private void removeUnusedResource()
		{
			for (int i = mCurrentMeshCount; i < Meshes.Count; i++)
			{
				DeleteManagedResource("Mesh", Meshes.Items[i]);
			}
			Meshes.Items.RemoveRange(mCurrentMeshCount, Meshes.Count - mCurrentMeshCount);
		}

		private CGMeshResource getNewMesh()
		{
			CGMeshResource cGMeshResource;
			if (mCurrentMeshCount < Meshes.Count)
			{
				cGMeshResource = Meshes.Items[mCurrentMeshCount];
				if (cGMeshResource == null)
				{
					cGMeshResource = (CGMeshResource)AddManagedResource("Mesh", "", mCurrentMeshCount);
					Meshes.Items[mCurrentMeshCount] = cGMeshResource;
				}
			}
			else
			{
				cGMeshResource = (CGMeshResource)AddManagedResource("Mesh", "", mCurrentMeshCount);
				Meshes.Items.Add(cGMeshResource);
			}
			cGMeshResource.Renderer.shadowCastingMode = CastShadows;
			cGMeshResource.Renderer.enabled = RendererEnabled;
			cGMeshResource.Renderer.receiveShadows = ReceiveShadows;
			cGMeshResource.Renderer.lightProbeUsage = LightProbeUsage;
			cGMeshResource.Renderer.reflectionProbeUsage = ReflectionProbes;
			cGMeshResource.Renderer.probeAnchor = AnchorOverride;
			if (!cGMeshResource.ColliderMatches(Collider))
			{
				cGMeshResource.RemoveCollider();
			}
			mCurrentMeshCount++;
			return cGMeshResource;
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using DM;
using TFBGames;
using UnityEngine;

namespace Landfall.TABS
{
	public abstract class CharacterItem : MonoBehaviour, IDatabaseEntity
	{
		public enum TagType
		{
			Faction = 0
		}

		[Flags]
		public enum UnitBaseRestrictions
		{
			None = 0,
			Wobbler = 1,
			Stiffy = 2,
			Halfling = 4
		}

		[Serializable]
		public struct Tag
		{
			public TagType tagType;

			public string value;

			public Tag(TagType type, string v)
			{
				tagType = type;
				value = v;
			}
		}

		public class RendererMaterialWrapper
		{
			public List<Renderer> m_renderers = new List<Renderer>();

			public List<int> m_materialIndices = new List<int>();

			public Material m_material;

			public bool m_hasTeamColor;

			public int m_paletteIndex = -1;

			public Action<int, bool> m_updated;

			private bool m_teamCallback;

			private int[] m_submeshIndex;

			public RendererMaterialWrapper()
			{
			}

			public RendererMaterialWrapper(RendererMaterialWrapper other)
			{
				m_renderers = new List<Renderer>(other.m_renderers);
				m_materialIndices = new List<int>(other.m_materialIndices);
				m_material = other.m_material;
				m_hasTeamColor = other.m_hasTeamColor;
				m_paletteIndex = other.m_paletteIndex;
			}

			public void RegisterTeamColorCallback()
			{
				if (!m_teamCallback)
				{
					UnitEditorTeamButtons._OnTeamChanged = (Action<Team>)Delegate.Combine(UnitEditorTeamButtons._OnTeamChanged, new Action<Team>(UpdateTeamColor));
					m_teamCallback = true;
				}
			}

			public void OnDestroy()
			{
				if (m_teamCallback)
				{
					UnitEditorTeamButtons._OnTeamChanged = (Action<Team>)Delegate.Remove(UnitEditorTeamButtons._OnTeamChanged, new Action<Team>(UpdateTeamColor));
				}
			}

			public void UpdateTeamColor(Team team)
			{
				if (!m_hasTeamColor)
				{
					return;
				}
				TeamColorPaletteData[] teamColors = ContentDatabase.Instance().GetUnitEditorColorPalette().TeamColors;
				if (teamColors != null && teamColors.Length != 0)
				{
					TeamColorPaletteData teamColorPaletteData = teamColors[m_paletteIndex];
					Material material = teamColorPaletteData.GetMaterial(team);
					if (!(material == null))
					{
						SetMaterial(material, m_paletteIndex, teamColor: true);
					}
				}
			}

			public void SetMaterial(Material mat, int paletteIndex, bool teamColor)
			{
				if (!(mat == null))
				{
					m_hasTeamColor = teamColor;
					m_paletteIndex = paletteIndex;
					SetMaterial(mat);
					m_updated?.Invoke(paletteIndex, teamColor);
				}
			}

			public void SetMaterial(Material mat)
			{
				if (m_renderers == null || m_renderers.Count <= 0 || m_materialIndices == null || m_materialIndices.Count <= 0 || mat == null)
				{
					return;
				}
				for (int i = 0; i < m_renderers.Count; i++)
				{
					Renderer renderer = m_renderers[i];
					if (!(renderer == null))
					{
						Material[] sharedMaterialsNonAlloc = renderer.GetSharedMaterialsNonAlloc();
						m_material = mat;
						if (sharedMaterialsNonAlloc != null && sharedMaterialsNonAlloc.Length != 0 && m_materialIndices != null && m_materialIndices.Count > 0 && m_materialIndices[i] >= 0 && m_materialIndices[i] < sharedMaterialsNonAlloc.Length)
						{
							sharedMaterialsNonAlloc[m_materialIndices[i]] = mat;
							renderer.sharedMaterials = sharedMaterialsNonAlloc;
						}
					}
				}
			}

			public void AddSubMeshIndices()
			{
				MeshFilter component = m_renderers[0].GetComponent<MeshFilter>();
				Mesh mesh = null;
				if (component == null)
				{
					SkinnedMeshRenderer skinnedMeshRenderer = m_renderers[0] as SkinnedMeshRenderer;
					if (skinnedMeshRenderer != null)
					{
						mesh = skinnedMeshRenderer.sharedMesh;
					}
				}
				else
				{
					mesh = component.sharedMesh;
				}
				if (mesh != null && mesh != null)
				{
					m_submeshIndex = mesh.GetTriangles(m_materialIndices[0]);
				}
			}

			public bool ContainIndex(int index, Renderer r)
			{
				if (!m_renderers.Contains(r))
				{
					return false;
				}
				if (m_submeshIndex == null)
				{
					Debug.LogError("WEHy is this null");
					return false;
				}
				int num = m_submeshIndex.Length;
				for (int i = 0; i < num; i++)
				{
					if (m_submeshIndex[i] == index)
					{
						return true;
					}
				}
				return false;
			}
		}

		public List<Tag> tags = new List<Tag>();

		[SerializeField]
		private DatabaseEntity m_entity;

		[SerializeField]
		private bool m_showInEditor = true;

		[SerializeField]
		private UnitBaseRestrictions m_unitBasesRestriction = UnitBaseRestrictions.Wobbler | UnitBaseRestrictions.Stiffy | UnitBaseRestrictions.Halfling;

		[SerializeField]
		private UnitRig.GearType m_gearType;

		[SerializeField]
		private bool m_forceBoneAttach;

		[HideInInspector]
		public float[] SubmeshArea;

		protected PropItemData m_propData;

		protected Team m_team;

		protected GameObject[] m_gameObjects;

		protected List<GameObject> m_extraObjects;

		private RendererMaterialWrapper[] m_defaultColors;

		private RendererMaterialWrapper[] m_materials;

		private Material m_storedMaterial;

		private LodGroupMaterialIndexer materialIndexer;

		private SettingsInstance m_flipColorsSetting;

		private bool m_isVisible = true;

		private bool m_forceHidden;

		private List<MeshCollider> m_staticColliders;

		private List<MeshCollider> m_skinnedColliders;

		public string DisplayName
		{
			get
			{
				if (!string.IsNullOrEmpty(Entity.Name))
				{
					return Entity.Name;
				}
				return base.gameObject.name;
			}
		}

		public DatabaseEntity Entity => m_entity;

		public bool ShowInEditor => m_showInEditor;

		public UnitRig.GearType GearT => m_gearType;

		public PropItemData PropData
		{
			get
			{
				return m_propData;
			}
			set
			{
				m_propData = value;
			}
		}

		public RendererMaterialWrapper[] SharedMaterials => m_materials;

		public RendererMaterialWrapper[] DefaultColors => m_defaultColors;

		public bool IsReallyEquipped { get; set; }

		public int NumObjects
		{
			get
			{
				if (m_gameObjects == null)
				{
					return 0;
				}
				return m_gameObjects.Length;
			}
		}

		public string PropName
		{
			get
			{
				string text = Entity.Name;
				if (string.IsNullOrEmpty(text))
				{
					text = base.gameObject.name;
				}
				return text;
			}
		}

		public event Action EOnEquip;

		public event Action EOnRemove;

		public event Action<bool> EOnSetVisibility;

		public bool ContainsTag(TagType tag)
		{
			for (int i = 0; i < tags.Count; i++)
			{
				if (tags[i].tagType == tag)
				{
					return true;
				}
			}
			return false;
		}

		public int NumTagValues(TagType tag)
		{
			int num = 0;
			for (int i = 0; i < tags.Count; i++)
			{
				if (tags[i].tagType == tag)
				{
					num++;
				}
			}
			return num;
		}

		public bool ContainsValue(TagType tag, string value)
		{
			for (int i = 0; i < tags.Count; i++)
			{
				if (tags[i].tagType == tag && tags[i].value == value)
				{
					return true;
				}
			}
			return false;
		}

		public bool ContainsKey(TagType tagType)
		{
			for (int i = 0; i < tags.Count; i++)
			{
				if (tags[i].tagType == tagType)
				{
					return true;
				}
			}
			return false;
		}

		public void RemoveTag(TagType tag)
		{
			for (int num = tags.Count - 1; num >= 0; num--)
			{
				if (tags[num].tagType == tag)
				{
					tags.RemoveAt(num);
				}
			}
		}

		public bool GetTag(TagType tagType, out Tag tag)
		{
			for (int i = 0; i < tags.Count; i++)
			{
				if (tags[i].tagType == tagType)
				{
					tag = tags[i];
					return true;
				}
			}
			tag = default(Tag);
			return false;
		}

		public bool UnitbaseAllowed(UnitBaseRestrictions unitBase)
		{
			return (m_unitBasesRestriction & unitBase) == unitBase;
		}

		protected virtual void Awake()
		{
			materialIndexer = GetComponent<LodGroupMaterialIndexer>();
			_ = materialIndexer == null;
			m_flipColorsSetting = ServiceLocator.GetService<GlobalSettingsHandler>().GetSettingsInstance("GAMEPLAY_FLIP_COLORS");
		}

		protected virtual void Start()
		{
		}

		protected void GetRenderers()
		{
			int num = 0;
			int num2 = 0;
			List<Renderer> list = new List<Renderer>();
			for (int i = 0; i < m_gameObjects.Length; i++)
			{
				if (i % 3 != 0)
				{
					continue;
				}
				LODGroup component = m_gameObjects[i].GetComponent<LODGroup>();
				if (component != null)
				{
					LOD[] lODs = component.GetLODs();
					if (lODs != null && lODs.Length != 0)
					{
						if (component.enabled)
						{
							Renderer[] renderers = lODs[0].renderers;
							list.AddRange(renderers);
						}
						else
						{
							LOD[] array = lODs;
							for (int j = 0; j < array.Length; j++)
							{
								LOD lOD = array[j];
								try
								{
									Renderer[] renderers2 = lOD.renderers;
									list.AddRange(renderers2.Where((Renderer lodRenderer) => lodRenderer.gameObject.activeInHierarchy));
								}
								catch (Exception)
								{
									Debug.LogError("Issue found with LOD for prop " + component.gameObject.name, component.gameObject);
								}
							}
						}
					}
				}
				else
				{
					list.AddRange(from item in m_gameObjects[i].GetComponentsInChildren<MeshRenderer>(includeInactive: false)
						where item.enabled
						select item);
					list.AddRange(from item in m_gameObjects[i].GetComponentsInChildren<SkinnedMeshRenderer>(includeInactive: false)
						where item.enabled
						select item);
				}
				for (int num3 = num; num3 < list.Count; num3++)
				{
					if (list[num3] != null && list[num3].sharedMaterials != null)
					{
						num2 += list[num3].sharedMaterials.Length;
					}
				}
				num++;
				m_materials = new RendererMaterialWrapper[num2];
				int num4 = 0;
				int num5 = 0;
				for (int num6 = 0; num6 < m_materials.Length; num6++)
				{
					Renderer renderer = list[num4];
					if (renderer == null || renderer.sharedMaterials == null)
					{
						num4++;
						num5 = 0;
						if (num4 >= list.Count)
						{
							break;
						}
						continue;
					}
					if (m_materials[num6] == null)
					{
						m_materials[num6] = new RendererMaterialWrapper();
						m_materials[num6].m_renderers.Add(renderer);
						m_materials[num6].m_material = renderer.sharedMaterials[num5];
						m_materials[num6].m_materialIndices.Add(num5);
						int colorID = num6;
						RendererMaterialWrapper obj = m_materials[num6];
						obj.m_updated = (Action<int, bool>)Delegate.Combine(obj.m_updated, (Action<int, bool>)delegate(int paletteIndex, bool flag)
						{
							int num10 = colorID;
							m_propData.m_colors[num10] = paletteIndex;
							m_propData.m_isTeamColor[num10] = flag;
						});
						TeamColor[] components = renderer.GetComponents<TeamColor>();
						foreach (TeamColor teamColor in components)
						{
							if (teamColor.materialID == num5)
							{
								teamColor.RegisterRenderersMaterialWrapper(m_materials[num6]);
								m_materials[num6].m_hasTeamColor = true;
								m_materials[num6].m_paletteIndex = teamColor.TeamColorIndex;
							}
						}
					}
					else if (m_materials[num6].m_material == renderer.sharedMaterials[num5])
					{
						m_materials[num6].m_renderers.Add(renderer);
						m_materials[num6].m_materialIndices.Add(num5);
					}
					num5++;
					if (num5 >= renderer.sharedMaterials.Length)
					{
						num4++;
						num5 = 0;
						if (num4 >= list.Count)
						{
							break;
						}
					}
				}
			}
			int[] colors = m_propData.m_colors;
			int[] array2 = new int[num2];
			bool[] isTeamColor = m_propData.m_isTeamColor;
			bool[] array3 = new bool[num2];
			for (int num8 = 0; num8 < num2; num8++)
			{
				array2[num8] = ((num8 < colors.Length) ? colors[num8] : (-1));
				RendererMaterialWrapper rendererMaterialWrapper = m_materials[num8];
				array3[num8] = ((num8 < isTeamColor.Length) ? isTeamColor[num8] : (rendererMaterialWrapper?.m_hasTeamColor ?? false));
			}
			m_propData.m_colors = array2;
			m_propData.m_isTeamColor = array3;
			m_defaultColors = new RendererMaterialWrapper[m_materials.Length];
			for (int num9 = 0; num9 < m_defaultColors.Length; num9++)
			{
				if (m_materials[num9] != null)
				{
					m_defaultColors[num9] = new RendererMaterialWrapper(m_materials[num9]);
				}
				else
				{
					m_defaultColors[num9] = new RendererMaterialWrapper();
				}
			}
		}

		private Mesh GetMesh(Renderer renderer)
		{
			renderer.GetType();
			if (renderer is MeshRenderer meshRenderer)
			{
				MeshFilter component = meshRenderer.GetComponent<MeshFilter>();
				if (component == null)
				{
					return null;
				}
				return component.sharedMesh;
			}
			if (renderer is SkinnedMeshRenderer skinnedMeshRenderer)
			{
				return skinnedMeshRenderer.sharedMesh;
			}
			return null;
		}

		private float GetSubmeshCount(Mesh mesh, int submeshIndex)
		{
			float num = 0f;
			if (submeshIndex < 0 || submeshIndex >= mesh.subMeshCount)
			{
				Debug.LogError($"Cannot get submesh count on {base.gameObject.name}. Submesh index {submeshIndex} is out of bounds", base.gameObject);
				return num;
			}
			int[] triangles = mesh.GetTriangles(submeshIndex);
			for (int i = 0; i < triangles.Length; i += 3)
			{
				num += GetTriangleArea(mesh.vertices, triangles[i], triangles[i + 1], triangles[i + 2]);
			}
			return num;
		}

		private float GetTriangleArea(Vector3[] vertices, int p1, int p2, int p3)
		{
			Vector3 pt = vertices[p1];
			Vector3 pt2 = vertices[p2];
			Vector3 pt3 = vertices[p3];
			return AreaOfTriangle(pt, pt2, pt3);
		}

		public float AreaOfTriangle(Vector3 pt1, Vector3 pt2, Vector3 pt3)
		{
			float num = Vector3.Distance(pt1, pt2);
			float num2 = Vector3.Distance(pt2, pt3);
			float num3 = Vector3.Distance(pt3, pt1);
			float num4 = (num + num2 + num3) / 2f;
			return Mathf.Sqrt(num4 * (num4 - num) * (num4 - num2) * (num4 - num3));
		}

		public void CalculateSubMeshes()
		{
			for (int i = 0; i < m_materials.Length; i++)
			{
				m_materials[i].AddSubMeshIndices();
			}
		}

		public void ClickSubMeshIndex(int index, Renderer r)
		{
			for (int i = 0; i < m_materials.Length; i++)
			{
				if (m_materials[i].ContainIndex(index, r))
				{
					Debug.Log("Found submesh : " + m_materials[i].m_materialIndices[0], r.gameObject);
					break;
				}
			}
		}

		public void RegisterTeamCallbacks()
		{
			for (int i = 0; i < m_materials.Length; i++)
			{
				if (m_materials[i] != null)
				{
					m_materials[i].RegisterTeamColorCallback();
				}
			}
		}

		public void SetConstantHighlight(bool isOn)
		{
		}

		public void Hover()
		{
		}

		public void SetEquipType(UnitRig.EquipType equipType)
		{
			m_propData.m_equip = equipType;
			if (m_gameObjects.Length > 1)
			{
				switch (equipType)
				{
				case UnitRig.EquipType.LEFT:
					m_gameObjects[0].SetActive(value: true);
					m_gameObjects[1].SetActive(value: false);
					break;
				case UnitRig.EquipType.RIGHT:
					m_gameObjects[0].SetActive(value: false);
					m_gameObjects[1].SetActive(value: true);
					break;
				case UnitRig.EquipType.BOTH:
					m_gameObjects[0].SetActive(value: true);
					m_gameObjects[1].SetActive(value: true);
					break;
				}
			}
		}

		public void SetMaterial(Material mat, int index, bool teamColor, int colorIndex = -1)
		{
			if (teamColor)
			{
				m_materials[index].SetMaterial(mat, colorIndex, teamColor);
			}
		}

		public void SetMaterial(ColorPaletteData color, int index)
		{
			Material material = color.m_material;
			if (materialIndexer != null && material != null)
			{
				SetMaterialViaIndexer(index, material, color.ColorIndex, hasTeamColor: false);
			}
			else
			{
				m_materials[index].SetMaterial(material, color.ColorIndex, teamColor: false);
			}
		}

		public void SetMaterial(TeamColorPaletteData color, int index, Team team)
		{
			if (m_flipColorsSetting.currentValue == 1)
			{
				switch (team)
				{
				case Team.Red:
					team = Team.Blue;
					break;
				case Team.Blue:
					team = Team.Red;
					break;
				}
			}
			Material material = color.GetMaterial(team);
			if (materialIndexer != null && material != null)
			{
				SetMaterialViaIndexer(index, material, color.ColorIndex, hasTeamColor: true);
			}
			else
			{
				m_materials[index].SetMaterial(material, color.ColorIndex, teamColor: true);
			}
		}

		private void SetMaterialViaIndexer(int submeshIndex, Material materialToSet, int colorIndex, bool hasTeamColor)
		{
			if (materialIndexer != null && materialToSet != null)
			{
				materialIndexer.SetCustomMaterial(submeshIndex, materialToSet);
				materialIndexer.SetMaterial(submeshIndex, materialToSet);
				RendererMaterialWrapper obj = m_materials[submeshIndex];
				obj.m_hasTeamColor = hasTeamColor;
				obj.m_paletteIndex = colorIndex;
				obj.m_updated?.Invoke(colorIndex, hasTeamColor);
			}
		}

		public void SetTemporaryMaterial(Material mat, int index)
		{
			if (materialIndexer != null)
			{
				m_storedMaterial = materialIndexer.GetMaterialAtIndex(index);
				materialIndexer.SetMaterial(index, mat);
			}
			else
			{
				m_storedMaterial = m_materials[index].m_material;
				m_materials[index].SetMaterial(mat);
			}
		}

		public void ResetTemporaryMaterial(int index)
		{
			if (m_storedMaterial == null)
			{
				return;
			}
			if (materialIndexer != null)
			{
				materialIndexer.ResetMaterialAtIndex(index);
				m_storedMaterial = null;
				return;
			}
			if (index >= 0 && index < m_materials.Length)
			{
				m_materials[index].SetMaterial(m_storedMaterial);
			}
			m_storedMaterial = null;
		}

		public void SetVisibility(bool visible, bool forceHidden = false)
		{
			if (visible == m_isVisible || (visible && m_forceHidden && !forceHidden))
			{
				return;
			}
			m_isVisible = visible;
			m_forceHidden = !visible && forceHidden;
			if (m_gameObjects == null)
			{
				Debug.LogError("Error: " + base.name, base.gameObject);
				return;
			}
			for (int i = 0; i < m_gameObjects.Length; i++)
			{
				if (m_gameObjects[i] != null)
				{
					m_gameObjects[i].SetActive(visible);
				}
			}
			if (m_extraObjects != null)
			{
				foreach (GameObject extraObject in m_extraObjects)
				{
					if (extraObject != null)
					{
						extraObject.SetActive(visible);
					}
				}
			}
			this.EOnSetVisibility?.Invoke(visible);
		}

		public void SetTeamColors(Team team)
		{
			for (int i = 0; i < m_materials.Length; i++)
			{
				if (m_materials[i] != null)
				{
					m_materials[i].UpdateTeamColor(team);
				}
			}
		}

		public void SetOriginalTeamColors(Team team)
		{
			for (int i = 0; i < m_gameObjects.Length; i++)
			{
				TeamColor[] componentsInChildren = m_gameObjects[i].GetComponentsInChildren<TeamColor>();
				for (int j = 0; j < componentsInChildren.Length; j++)
				{
					componentsInChildren[j].SetTeamColor(team);
				}
			}
		}

		public List<Action> AttachColliders(ObjectPool<Mesh> meshPool, bool ignoreSkinned)
		{
			List<Action> list = new List<Action>();
			for (int i = 0; i < m_gameObjects.Length; i++)
			{
				List<Action> list2 = AttachCollider(m_gameObjects[i], meshPool, ignoreSkinned);
				if (list2 != null)
				{
					list.AddRange(list2);
				}
			}
			return list;
		}

		private List<Action> AttachCollider(GameObject obj, ObjectPool<Mesh> meshPool, bool ignoreSkinned)
		{
			List<Action> list = new List<Action>();
			Rigidbody componentInParent = obj.GetComponentInParent<Rigidbody>();
			if (componentInParent != null)
			{
				componentInParent.isKinematic = true;
			}
			SkinnedMeshRenderer[] componentsInChildren = obj.GetComponentsInChildren<SkinnedMeshRenderer>();
			if (componentsInChildren != null && componentsInChildren.Length != 0 && !ignoreSkinned)
			{
				if (m_skinnedColliders == null)
				{
					m_skinnedColliders = new List<MeshCollider>();
				}
				foreach (SkinnedMeshRenderer skinnedMesh in componentsInChildren)
				{
					skinnedMesh.gameObject.layer = LayerMask.NameToLayer("CharacterProp");
					MeshCollider collider = skinnedMesh.gameObject.AddComponent<MeshCollider>();
					m_skinnedColliders.Add(collider);
					Mesh colliderMesh = meshPool.GetObject();
					list.Add(delegate
					{
						if (skinnedMesh == null)
						{
							Debug.Log("FIX ME", base.gameObject);
						}
						else
						{
							skinnedMesh.BakeMesh(colliderMesh);
							collider.sharedMesh = null;
							collider.sharedMesh = colliderMesh;
						}
					});
				}
			}
			MeshRenderer componentInChildren = obj.GetComponentInChildren<MeshRenderer>();
			if ((bool)componentInChildren)
			{
				if (m_staticColliders == null)
				{
					m_staticColliders = new List<MeshCollider>();
				}
				componentInChildren.gameObject.layer = LayerMask.NameToLayer("CharacterProp");
				MeshCollider item = componentInChildren.gameObject.AddComponent<MeshCollider>();
				m_staticColliders.Add(item);
			}
			return list;
		}

		public void RemoveStaticColliders()
		{
			if (m_staticColliders == null)
			{
				return;
			}
			for (int i = 0; i < m_staticColliders.Count; i++)
			{
				Rigidbody componentInParent = m_staticColliders[i].GetComponentInParent<Rigidbody>();
				UnityEngine.Object.DestroyImmediate(m_staticColliders[i]);
				if (componentInParent != null)
				{
					componentInParent.isKinematic = false;
				}
			}
			m_staticColliders.Clear();
		}

		public void RemoveSkinnedColliders(ObjectPool<Mesh> meshPool)
		{
			if (m_skinnedColliders != null)
			{
				for (int i = 0; i < m_skinnedColliders.Count; i++)
				{
					meshPool.ReleaseObject(m_skinnedColliders[i].sharedMesh);
					UnityEngine.Object.DestroyImmediate(m_skinnedColliders[i]);
				}
				m_skinnedColliders.Clear();
			}
		}

		private void UpdateTeamColors(int value)
		{
			if (m_propData != null)
			{
				SetPropData(m_propData, m_team);
			}
		}

		public void SetPropData(PropItemData propData, Team team)
		{
			UnitEditorColorPalette unitEditorColorPalette = ContentDatabase.Instance().GetUnitEditorColorPalette();
			int num = Mathf.Min(propData.m_colors.Length, m_defaultColors.Length);
			for (int i = 0; i < num; i++)
			{
				if (propData.m_colors[i] < 0)
				{
					if (!m_defaultColors[i].m_hasTeamColor)
					{
						SetMaterial(m_defaultColors[i].m_material, i, m_defaultColors[i].m_hasTeamColor);
					}
				}
				else
				{
					if (i >= propData.m_isTeamColor.Length)
					{
						continue;
					}
					if (!propData.m_isTeamColor[i])
					{
						int num2 = propData.m_colors[i];
						if (num2 >= 0 && num2 < unitEditorColorPalette.Colors.Length)
						{
							ColorPaletteData color = unitEditorColorPalette.Colors[num2];
							SetMaterial(color, i);
						}
					}
					else
					{
						TeamColorPaletteData color2 = unitEditorColorPalette.TeamColors[propData.m_colors[i]];
						SetMaterial(color2, i, team);
					}
				}
			}
		}

		public virtual void Equip()
		{
			if (this.EOnEquip != null)
			{
				this.EOnEquip();
			}
		}

		public GameObject[] Equip(GameObject unitRoot, PropItemData propData, Stitcher.TransformCatalog catalog, Team team, bool isUnitEditor = false)
		{
			Equip();
			UnitRig component = unitRoot.GetComponent<UnitRig>();
			GameObject gameObject = null;
			if (base.gameObject.GetComponentInChildren<SkinnedMeshRenderer>() != null && !m_forceBoneAttach)
			{
				EyeSpawner component2 = GetComponent<EyeSpawner>();
				if (component2 != null)
				{
					for (int i = 0; i < component2.eyeSets.Length; i++)
					{
						component.GetBonesForEquip(component2.eyeSets[i].m_gearType, out var bone, out var _);
						component2.eyeSets[i].obj.transform.SetParent(bone, worldPositionStays: false);
						m_extraObjects = new List<GameObject> { component2.eyeSets[i].obj };
					}
				}
				GoToBodyPart[] componentsInChildren = GetComponentsInChildren<GoToBodyPart>();
				if (componentsInChildren.Length != 0 && m_extraObjects == null)
				{
					m_extraObjects = new List<GameObject>();
				}
				GoToBodyPart[] array = componentsInChildren;
				foreach (GoToBodyPart goToBodyPart in array)
				{
					m_extraObjects.Add(goToBodyPart.gameObject);
				}
				if (isUnitEditor)
				{
					m_gameObjects = Stitcher.StitchUnitEditor(base.gameObject, unitRoot, catalog);
				}
				else
				{
					m_gameObjects = Stitcher.StitchUnitEditor(base.gameObject, unitRoot, catalog);
				}
			}
			else
			{
				Vector3 scale = propData.m_scale;
				UnitRig.GearType gearT = base.gameObject.GetComponent<CharacterItem>().GearT;
				component.GetBonesForEquip(gearT, out var bone3, out var bone4);
				if (bone3 != null && bone4 != null)
				{
					if (!isUnitEditor && propData.m_equip != UnitRig.EquipType.BOTH)
					{
						if (propData.m_equip == UnitRig.EquipType.RIGHT)
						{
							gameObject = base.gameObject;
							bone3 = bone4;
						}
					}
					else
					{
						GameObject gameObject2 = UnityEngine.Object.Instantiate(base.gameObject);
						UnityEngine.Object.Destroy(gameObject2.GetComponent<CharacterItem>());
						gameObject2.AddComponent<PropItemProxy>().m_propItemReference = this;
						gameObject2.transform.SetParent(bone4, worldPositionStays: false);
						gameObject2.transform.localPosition = propData.m_positionOffset;
						gameObject2.transform.localRotation = Quaternion.identity;
						gameObject2.transform.localScale = scale;
						gameObject = gameObject2;
						if (gameObject != null)
						{
							LodGroupMaterialIndexer component3 = GetComponent<LodGroupMaterialIndexer>();
							LodGroupMaterialIndexer component4 = gameObject.GetComponent<LodGroupMaterialIndexer>();
							if (component3 != null && component4 != null)
							{
								component3.SetMirroredObject(component4);
							}
						}
						m_gameObjects = new GameObject[2] { base.gameObject, gameObject2 };
					}
				}
				if (m_gameObjects == null)
				{
					m_gameObjects = new GameObject[1] { base.gameObject };
				}
				base.transform.SetParent(bone3, worldPositionStays: false);
				base.transform.localPosition = propData.m_positionOffset;
				base.transform.localRotation = Quaternion.identity;
				base.transform.localScale = scale;
			}
			if (isUnitEditor && m_gameObjects.Length > 1)
			{
				if (propData.m_equip == UnitRig.EquipType.LEFT)
				{
					m_gameObjects[0].SetActive(value: true);
					m_gameObjects[1].SetActive(value: false);
				}
				else if (propData.m_equip == UnitRig.EquipType.RIGHT)
				{
					m_gameObjects[0].SetActive(value: false);
					m_gameObjects[1].SetActive(value: true);
				}
			}
			base.gameObject.AddComponent<PropItemProxy>().m_propItemReference = this;
			m_propData = new PropItemData(propData);
			m_team = team;
			GetRenderers();
			SetOriginalTeamColors(team);
			SetPropData(propData, team);
			if (gameObject != null)
			{
				Vector3 localScale = gameObject.transform.localScale;
				localScale.x *= -1f;
				gameObject.transform.localScale = localScale;
			}
			return m_gameObjects;
		}

		public void SetCombinedMesh(bool hasMirror)
		{
			GameObject[] gameObjects = m_gameObjects;
			foreach (GameObject itemObject in gameObjects)
			{
				RuntimeBakeObject(hasMirror, itemObject);
			}
		}

		public void RuntimeBakeGameObject(bool hasMirror)
		{
			RuntimeBakeObject(hasMirror, base.gameObject);
		}

		private void RuntimeBakeObject(bool hasMirror, GameObject itemObject)
		{
			Renderer[] componentsInChildren = itemObject.GetComponentsInChildren<Renderer>();
			if (componentsInChildren == null)
			{
				return;
			}
			Renderer[] array = componentsInChildren;
			foreach (Renderer renderer in array)
			{
				MeshFilter meshFilter = null;
				Mesh mesh = null;
				SkinnedMeshRenderer skinnedMeshRenderer;
				if ((object)(skinnedMeshRenderer = renderer as SkinnedMeshRenderer) == null)
				{
					skinnedMeshRenderer = null;
					meshFilter = renderer.GetComponent<MeshFilter>();
				}
				if (skinnedMeshRenderer != null)
				{
					mesh = skinnedMeshRenderer.sharedMesh;
				}
				else if (meshFilter != null)
				{
					mesh = meshFilter.sharedMesh;
				}
				if (!(mesh == null) && mesh.isReadable && !(MeshInstanceManager.Instance == null))
				{
					OptimizedItem optimizedItem = MeshInstanceManager.Instance.RequestOptimizedMesh(mesh, renderer.sharedMaterials, hasMirror);
					if (skinnedMeshRenderer != null)
					{
						skinnedMeshRenderer.sharedMesh = optimizedItem.mesh;
					}
					else if (meshFilter != null)
					{
						meshFilter.sharedMesh = optimizedItem.mesh;
					}
					renderer.sharedMaterials = optimizedItem.materials;
				}
			}
			ApplySetParentIfNeeded();
		}

		private void ApplySetParentIfNeeded()
		{
			SetParent[] componentsInChildren = GetComponentsInChildren<SetParent>();
			foreach (SetParent setParent in componentsInChildren)
			{
				if (setParent.DelayReparenting)
				{
					setParent.Doit();
				}
			}
		}

		private void SetMirroredMesh(Renderer[] renderers)
		{
			if (renderers == null)
			{
				return;
			}
			MeshInstanceManager instance = MeshInstanceManager.Instance;
			if (instance == null)
			{
				return;
			}
			foreach (Renderer renderer in renderers)
			{
				if (renderer == null)
				{
					continue;
				}
				SkinnedMeshRenderer skinnedMeshRenderer = renderer as SkinnedMeshRenderer;
				if ((bool)skinnedMeshRenderer)
				{
					Mesh sharedMesh = instance.RequestMirroredMesh(skinnedMeshRenderer.sharedMesh);
					skinnedMeshRenderer.sharedMesh = sharedMesh;
					continue;
				}
				MeshFilter componentInChildren = renderer.GetComponentInChildren<MeshFilter>();
				if (componentInChildren == null)
				{
					break;
				}
				Mesh sharedMesh2 = instance.RequestMirroredMesh(componentInChildren.sharedMesh);
				componentInChildren.sharedMesh = sharedMesh2;
			}
		}

		public virtual void Remove()
		{
			this.EOnRemove?.Invoke();
			bool flag = false;
			if (m_gameObjects == null)
			{
				return;
			}
			for (int i = 0; i < m_gameObjects.Length; i++)
			{
				if (!flag && base.gameObject == m_gameObjects[i])
				{
					flag = true;
				}
				UnityEngine.Object.Destroy(m_gameObjects[i]);
			}
			m_gameObjects = null;
			if (!flag)
			{
				UnityEngine.Object.Destroy(base.gameObject);
			}
			if (m_extraObjects != null)
			{
				for (int j = 0; j < m_extraObjects.Count; j++)
				{
					UnityEngine.Object.Destroy(m_extraObjects[j]);
				}
				m_extraObjects.Clear();
			}
		}

		protected virtual void OnDestroy()
		{
			if (m_materials != null)
			{
				for (int i = 0; i < m_materials.Length; i++)
				{
					m_materials[i]?.OnDestroy();
				}
			}
		}
	}
}

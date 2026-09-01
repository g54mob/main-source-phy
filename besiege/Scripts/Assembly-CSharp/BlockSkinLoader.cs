using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Besiege;
using BesiegeDlc;
using GameGrind;
using Localisation;
using MultithreadCoroutines;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BlockSkinLoader : SingleInstance<BlockSkinLoader>
{
	public class SModifier
	{
	}

	public class SkinPack : SModifier
	{
		public class Config
		{
			public int version;

			public bool useSingleTexture;

			public bool allowTiling;

			public int displayBlock = -1;

			public string sharedTexPath;

			public Texture sharedTex;
		}

		public class Skin : SModifier
		{
			public string path;

			public string objPath;

			public string brokenObjPath;

			public string texPath;

			[NonSerialized]
			public Skin shortSkin;

			[NonSerialized]
			public bool isDefault;

			[NonSerialized]
			public bool enabled = true;

			[NonSerialized]
			public bool isLoading;

			[NonSerialized]
			public bool _doneLoading;

			[NonSerialized]
			public bool _meshLoaded;

			[NonSerialized]
			public bool _short;

			[NonSerialized]
			public bool _isInvalidSkin;

			[NonSerialized]
			protected Mesh _mesh;

			[NonSerialized]
			protected Material _material;

			[NonSerialized]
			protected Material[] _materials;

			[NonSerialized]
			protected Material _ghostMaterial;

			[NonSerialized]
			protected Material[] _ghostMaterials;

			[NonSerialized]
			protected Texture _texture;

			[NonSerialized]
			public int ID = -1;

			[NonSerialized]
			public BlockPrefab prefab;

			[NonSerialized]
			public SkinPack pack;

			[NonSerialized]
			public Guid ModId = Guid.Empty;

			[NonSerialized]
			public int LocalId;

			[NonSerialized]
			protected List<MonoBehaviour> _scriptsUsingThis = new List<MonoBehaviour>();

			protected Material _bannedIconMat;

			public bool doneLoading
			{
				get
				{
					return _doneLoading;
				}
			}

			public bool meshLoaded
			{
				get
				{
					return _meshLoaded;
				}
			}

			protected bool HasTexture
			{
				get
				{
					return (!pack.settings.useSingleTexture) ? (_texture != null) : (pack.settings.sharedTex != null);
				}
			}

			protected bool SkinAvailable
			{
				get
				{
					return enabled && OptionsMaster.skinsEnabled && (HasTexture || !doneLoading);
				}
			}

			protected bool MaterialAssigned
			{
				get
				{
					return _material != null && (_material.mainTexture != null || !HasTexture);
				}
			}

			protected bool GhostMaterialAssigned
			{
				get
				{
					return _ghostMaterial != null && _ghostMaterial.mainTexture != null;
				}
			}

			public Texture texture
			{
				get
				{
					if (pack.settings.useSingleTexture)
					{
						return pack.settings.sharedTex;
					}
					Material material = this.material;
					if (material != null)
					{
						return material.mainTexture;
					}
					return _texture;
				}
				set
				{
					if (!pack.settings.useSingleTexture)
					{
						_texture = value;
					}
				}
			}

			public virtual Mesh mesh
			{
				get
				{
					if (pack != defaultPack)
					{
						if (!enabled || !meshLoaded || !OptionsMaster.skinsEnabled)
						{
							Skin defaultOrShortDefault = GetDefaultOrShortDefault();
							if (defaultOrShortDefault != null)
							{
								return defaultOrShortDefault.mesh;
							}
						}
						else if (_mesh == null)
						{
							_meshLoaded = true;
							Skin defaultOrShortDefault2 = GetDefaultOrShortDefault();
							if (defaultOrShortDefault2 != null)
							{
								return defaultOrShortDefault2.mesh;
							}
						}
					}
					return _mesh;
				}
				set
				{
					_mesh = value;
				}
			}

			public Material material
			{
				get
				{
					if (pack != defaultPack)
					{
						if (!SkinAvailable)
						{
							Skin defaultOrShortDefault = GetDefaultOrShortDefault();
							if (defaultOrShortDefault != null)
							{
								return defaultOrShortDefault.material;
							}
						}
						else
						{
							if (!doneLoading)
							{
								return SingleInstance<BlockSkinLoader>.Instance.LoadingMaterial;
							}
							if (!MaterialAssigned)
							{
								Skin defaultOrShortDefault2 = GetDefaultOrShortDefault();
								if (defaultOrShortDefault2 != null)
								{
									_material = SetMaterialFrom(defaultOrShortDefault2.material, Color.white);
								}
							}
						}
					}
					return _material;
				}
			}

			public Material[] materials
			{
				get
				{
					if (pack != defaultPack)
					{
						if (!SkinAvailable)
						{
							Skin defaultOrShortDefault = GetDefaultOrShortDefault();
							if (defaultOrShortDefault != null)
							{
								return defaultOrShortDefault.materials;
							}
						}
						_materials = new Material[1] { material };
					}
					return _materials;
				}
			}

			public Material ghostMaterial
			{
				get
				{
					if (pack != defaultPack)
					{
						if (!SkinAvailable)
						{
							if (prefab != null)
							{
								return prefab.DefaultSkin.ghostMaterial;
							}
						}
						else if (!doneLoading)
						{
							return SingleInstance<BlockSkinLoader>.Instance.LoadingGhostMaterial;
						}
					}
					if (!GhostMaterialAssigned && prefab != null)
					{
						Material material = prefab.ghostController.startingMaterials[0];
						_ghostMaterial = SetMaterialFrom(prefab.ghostController.startingMaterials[0]);
					}
					return _ghostMaterial;
				}
			}

			public Material[] ghostMaterials
			{
				get
				{
					Material material = ghostMaterial;
					if (pack != defaultPack && SkinAvailable)
					{
						_ghostMaterials = new Material[prefab.ghostController.startingMaterials.Length];
						for (int i = 0; i < _ghostMaterials.Length; i++)
						{
							_ghostMaterials[i] = material;
						}
					}
					else
					{
						_ghostMaterials = new Material[prefab.ghostController.startingMaterials.Length];
						for (int j = 0; j < _ghostMaterials.Length; j++)
						{
							_ghostMaterials[j] = ((j <= 1) ? ghostMaterial : SetMaterialFrom(prefab.ghostController.startingMaterials[j]));
						}
					}
					return _ghostMaterials;
				}
			}

			public Material BannedIconMat
			{
				get
				{
					if (_bannedIconMat == null)
					{
						_bannedIconMat = new Material(ReferenceMaster.Instance.bannedButtonShader);
						_bannedIconMat.SetFloat("_Saturation", 0.59f);
						Color color = material.color;
						color.a = 0.4f;
						_bannedIconMat.SetColor("_Color", color * 0.75f);
						_bannedIconMat.SetTexture("_MainTex", material.mainTexture);
						if (material.HasProperty("_RimPower"))
						{
							_bannedIconMat.SetFloat("_RimPower", material.GetFloat("_RimPower") * 1.5f);
							_bannedIconMat.SetColor("_RimColor", material.GetColor("_RimColor") * 0.65f);
						}
					}
					return _bannedIconMat;
				}
			}

			protected Skin()
			{
			}

			public Skin(BlockPrefab prefab, SkinPack pack)
			{
				_short = false;
				ID = prefab.ID;
				this.prefab = prefab;
				this.pack = pack;
				isDefault = pack.isDefault;
				this.pack.skins.Add(this);
				this.pack.hasValidSkins = true;
				if (pack.type != PackType.Official)
				{
					this.prefab.downloadedSkins.Add(this);
				}
				else
				{
					this.prefab.officialSkins.Add(this);
				}
				ModId = prefab.modId;
				LocalId = prefab.localId;
			}

			public void SetID(int ID)
			{
				this.ID = ID;
				PrefabMaster.BlockPrefabs.TryGetValue(ID, out prefab);
			}

			public virtual Skin GetDefaultOrShortDefault()
			{
				Skin skin = null;
				if (_short && ID != -1 && PrefabMaster.BlockPrefabs[ID].DefaultSkin.shortSkin != null)
				{
					skin = PrefabMaster.BlockPrefabs[ID].DefaultSkin.shortSkin;
				}
				else if (prefab != null)
				{
					skin = prefab.DefaultSkin;
				}
				if (skin == null)
				{
					UnityEngine.Debug.LogError(string.Concat("Block '", prefab, "' has no default skin, short? ", _short, ", ID: ", ID));
				}
				return skin;
			}

			public static int Decode(byte[] data, int offset, out Skin skin)
			{
				int num = offset;
				int num2 = data[offset];
				offset++;
				byte[] array = new byte[num2];
				Buffer.BlockCopy(data, offset, array, 0, num2);
				string id = Encoding.UTF8.GetString(array);
				offset += num2;
				int count;
				offset += NetworkCompression.UnpackUInt(data, offset, true, out count);
				byte[] array2 = new byte[count];
				Buffer.BlockCopy(data, offset, array2, 0, count);
				offset += count;
				string name = Encoding.UTF8.GetString(array2);
				skin = Holder(name, id, string.Empty);
				return offset - num;
			}

			public Skin Register(MonoBehaviour script)
			{
				if (script != null && pack != defaultPack && !_scriptsUsingThis.Contains(script))
				{
					_scriptsUsingThis.Add(script);
					if (!doneLoading && !isLoading)
					{
						LoadSkin();
					}
				}
				return this;
			}

			public Skin Unregister(MonoBehaviour script)
			{
				if (script != null && pack != defaultPack && _scriptsUsingThis.Contains(script))
				{
					_scriptsUsingThis.Remove(script);
					if (_scriptsUsingThis.Count <= 0 && doneLoading)
					{
						UnLoadSkin();
					}
				}
				return this;
			}

			public static Skin GetDefaultOfType(BlockType blockType)
			{
				if (PrefabMaster.BlockPrefabs.ContainsKey((int)blockType) && PrefabMaster.BlockPrefabs[(int)blockType].DefaultSkin != null)
				{
					return PrefabMaster.BlockPrefabs[(int)blockType].DefaultSkin;
				}
				return null;
			}

			public static Skin GetEmpty()
			{
				return new Skin();
			}

			public static Skin Invalid(int ID, SkinPack pack)
			{
				Skin skin = new Skin();
				skin.pack = pack;
				skin._isInvalidSkin = true;
				skin.pack.hasInvalidSkins = true;
				skin.SetActive(false, false);
				skin.SetID(ID);
				return skin;
			}

			public static Skin Incomplete(int ID, SkinPack pack)
			{
				Skin skin = new Skin();
				skin._short = true;
				skin.pack = pack;
				skin.SetID(ID);
				return skin;
			}

			public static Skin Holder(string name, string id, string url)
			{
				Skin skin = new Skin();
				skin.pack = new SkinPack();
				skin.pack.name = name;
				skin.pack.id = id;
				skin.pack.isDefault = id.Equals(defaultString);
				return skin;
			}

			public Skin SetPath(string path)
			{
				this.path = path;
				return this;
			}

			public Skin SetObj(string objPath)
			{
				if (objPath != null)
				{
					this.objPath = objPath;
				}
				return this;
			}

			public Skin SetTex(string texPath)
			{
				if (pack.settings.useSingleTexture)
				{
					return this;
				}
				if (texPath != null)
				{
					this.texPath = texPath;
				}
				return this;
			}

			public virtual Skin Conclude()
			{
				if (_mesh == null && objPath != null)
				{
					_mesh = new Mesh();
				}
				return this;
			}

			public Skin SetInfo(string path, string objPath, string texPath, Material[] materials)
			{
				_material = material;
				return SetPath(path).SetObj(objPath).SetTex(texPath).Conclude();
			}

			public void SetPreloaded(Mesh mesh, Texture texture, Material[] materials)
			{
				_mesh = mesh;
				_texture = texture;
				if (materials != null && materials.Length > 0)
				{
					_material = materials[0];
				}
				_materials = materials;
				if ((bool)_material)
				{
					_material.mainTexture = texture;
				}
				_doneLoading = true;
				_meshLoaded = true;
			}

			public Skin ForceMaterial(Material mat)
			{
				_material = mat;
				return this;
			}

			public Skin ForceGhostMaterial(Material mat)
			{
				_ghostMaterial = mat;
				return this;
			}

			public void SetActive(bool enable, bool update = true)
			{
				enabled = enable;
				if (pack.isDefault || prefab == null)
				{
					return;
				}
				if (enabled)
				{
					if (pack.type == PackType.Official)
					{
						if (!prefab.officialSkins.Contains(this))
						{
							prefab.officialSkins.Add(this);
						}
					}
					else if (!prefab.downloadedSkins.Contains(this))
					{
						prefab.downloadedSkins.Add(this);
					}
					if (!isLoading && !doneLoading)
					{
						LoadSkin();
					}
				}
				else if (pack.type == PackType.Official)
				{
					if (prefab.officialSkins.Contains(this))
					{
						prefab.officialSkins.Remove(this);
					}
				}
				else if (prefab.downloadedSkins.Contains(this))
				{
					prefab.downloadedSkins.Remove(this);
				}
				if (update && prefab != null && pack != null && BlockSkinLoader.SkinModified != null)
				{
					BlockSkinLoader.SkinModified(this);
				}
			}

			public Material SetMaterialFrom(Material baseMat)
			{
				return SetMaterialFrom(baseMat, baseMat.color);
			}

			public Material SetMaterialFrom(Material baseMat, Color color)
			{
				Material material = new Material(baseMat);
				Texture texture = ((!pack.settings.useSingleTexture) ? _texture : pack.settings.sharedTex);
				if (texture == null && prefab != null)
				{
					material.mainTexture = prefab.DefaultSkin.texture;
				}
				else
				{
					material.mainTexture = texture;
				}
				material.color = new Color(color.r, color.g, color.b, baseMat.color.a);
				return material;
			}

			public virtual Skin LoadSkin()
			{
				if (pack == defaultPack || pack.type == PackType.Official)
				{
					return this;
				}
				if (enabled)
				{
					isLoading = true;
					AssetImporter.StartImport.Async.Skin(this);
				}
				loadedSkins.Add(this);
				return this;
			}

			public Skin UnLoadSkin(bool refresh = true)
			{
				if (pack == defaultPack || pack.type == PackType.Official)
				{
					return this;
				}
				_doneLoading = false;
				for (int i = 0; i < _materials.Length; i++)
				{
					if (_materials != null && (bool)_materials[i] && SingleInstance<BlockSkinLoader>.hasInstance() && _materials[i] != SingleInstance<BlockSkinLoader>.Instance.LoadingMaterial && _materials[i] != prefab.DefaultSkin.material)
					{
						UnityEngine.Object.Destroy(_materials[i]);
					}
				}
				for (int j = 0; j < _ghostMaterials.Length; j++)
				{
					if (_ghostMaterials != null && (bool)_ghostMaterials[j] && SingleInstance<BlockSkinLoader>.hasInstance() && _ghostMaterials[j] != SingleInstance<BlockSkinLoader>.Instance.LoadingGhostMaterial && _ghostMaterials[j] != prefab.DefaultSkin.ghostMaterial)
					{
						UnityEngine.Object.Destroy(_ghostMaterials[j]);
					}
				}
				UnityEngine.Object.Destroy(_material);
				_material = null;
				UnityEngine.Object.Destroy(_ghostMaterial);
				_ghostMaterial = null;
				UnityEngine.Object.Destroy(_texture);
				_texture = null;
				_materials = null;
				_ghostMaterials = null;
				if (refresh && prefab != null && pack != null && BlockSkinLoader.SkinModified != null)
				{
					BlockSkinLoader.SkinModified(this);
				}
				loadedSkins.Remove(this);
				return this;
			}

			public Skin ResetMesh()
			{
				if (pack == defaultPack || pack.type == PackType.Official)
				{
					return this;
				}
				_meshLoaded = false;
				_mesh = new Mesh();
				if (enabled)
				{
					isLoading = true;
					AssetImporter.StartImport.Async.Skin(this);
				}
				return this;
			}

			public void DoneLoading()
			{
				_meshLoaded = true;
				isLoading = false;
				_doneLoading = true;
				_materials = new Material[1] { material };
				_ghostMaterials = ghostMaterials;
				if (prefab != null && pack != null && BlockSkinLoader.SkinModified != null)
				{
					BlockSkinLoader.SkinModified(this);
				}
				if (_scriptsUsingThis.Count <= 0)
				{
					UnLoadSkin();
				}
			}
		}

		public class SkinCollection : Skin
		{
			public Dictionary<string, string[]> meshPaths = new Dictionary<string, string[]>();

			[NonSerialized]
			public Dictionary<string, Mesh[]> collection;

			public override Mesh mesh
			{
				get
				{
					if (pack != defaultPack && (!enabled || !base.meshLoaded || !OptionsMaster.skinsEnabled || !collection.ContainsKey("idle")))
					{
						Skin defaultOrShortDefault = GetDefaultOrShortDefault();
						if (defaultOrShortDefault != null)
						{
							return defaultOrShortDefault.mesh;
						}
					}
					return collection["idle"][0];
				}
				set
				{
					throw new Exception("SkinCollection can't assign mesh like this");
				}
			}

			public Mesh icon
			{
				get
				{
					if (pack != defaultPack && (!enabled || !base.meshLoaded || !OptionsMaster.skinsEnabled || !collection.ContainsKey("icon")))
					{
						SkinCollection skinCollection = GetDefaultOrShortDefault() as SkinCollection;
						if (skinCollection != null)
						{
							return skinCollection.icon;
						}
					}
					return collection["icon"][0];
				}
				set
				{
					throw new Exception("SkinCollection can't assign mesh like this");
				}
			}

			protected SkinCollection()
			{
			}

			public SkinCollection(BlockPrefab prefab, SkinPack pack)
				: base(prefab, pack)
			{
			}

			public override Skin GetDefaultOrShortDefault()
			{
				if (prefab != null)
				{
					return prefab.DefaultSkin;
				}
				UnityEngine.Debug.LogError(string.Concat("Block '", prefab, "' has no default skin, short? ", _short, ", ID: ", ID));
				return null;
			}

			public SkinCollection SetObj(string key, params string[] objPath)
			{
				if (objPath == null || objPath.Length == 0)
				{
					UnityEngine.Debug.LogError("SkinCollection paths are empty");
					return this;
				}
				for (int i = 0; i < objPath.Length; i++)
				{
					if (string.IsNullOrEmpty(objPath[i]))
					{
						UnityEngine.Debug.LogError("SkinCollection path " + i + " is empty");
						return this;
					}
				}
				meshPaths.Add(key, objPath);
				return this;
			}

			public override Skin Conclude()
			{
				collection = new Dictionary<string, Mesh[]>();
				foreach (KeyValuePair<string, string[]> meshPath in meshPaths)
				{
					if (!collection.ContainsKey(meshPath.Key))
					{
						Mesh[] value = new Mesh[0];
						collection.Add(meshPath.Key, value);
					}
				}
				return this;
			}

			public void SetPreloaded(Dictionary<string, Mesh[]> mesh, Texture texture, Material[] materials)
			{
				collection = mesh;
				_texture = texture;
				if (materials != null && materials.Length > 0)
				{
					_material = materials[0];
				}
				_materials = materials;
				if ((bool)base.material)
				{
					_material.mainTexture = texture;
				}
				_doneLoading = true;
				_meshLoaded = true;
			}

			public override Skin LoadSkin()
			{
				if (isDefault)
				{
					return this;
				}
				if (enabled)
				{
					isLoading = true;
					AssetImporter.StartImport.Async.SkinCollection(this);
				}
				loadedSkins.Add(this);
				return this;
			}
		}

		public string name;

		[HideInInspector]
		public string id = defaultString;

		[NonSerialized]
		public string path;

		[NonSerialized]
		public string workshopURL;

		[NonSerialized]
		public WorkshopManager.WorkshopItem workshopItem;

		[NonSerialized]
		public List<Skin> skins = new List<Skin>();

		[NonSerialized]
		public List<BlockPrefab> prefabs = new List<BlockPrefab>();

		[NonSerialized]
		public PackType type;

		[NonSerialized]
		public bool isDefault;

		[NonSerialized]
		public bool hasInvalidSkins;

		[NonSerialized]
		public Config settings = new Config();

		public bool deleted;

		internal bool hasValidSkins;

		public SkinPack()
		{
		}

		public SkinPack(int version, bool allowTiling)
		{
			settings.version = version;
			settings.allowTiling = allowTiling;
		}

		public Skin CreateSkinFor(BlockPrefab prefab)
		{
			return new Skin(prefab, this);
		}

		public byte[] Encode()
		{
			byte[] bytes = Encoding.UTF8.GetBytes(name);
			int count = bytes.Length;
			int num = NetworkCompression.PackedUIntLength(count, true);
			byte[] bytes2 = Encoding.UTF8.GetBytes(id);
			byte[] array = new byte[1 + bytes2.Length + num + bytes.Length];
			int num2 = 0;
			array[num2] = (byte)bytes2.Length;
			num2++;
			Buffer.BlockCopy(bytes2, 0, array, num2, bytes2.Length);
			num2 += bytes2.Length;
			NetworkCompression.PackUInt(count, array, num2, true, num);
			num2 += num;
			Buffer.BlockCopy(bytes, 0, array, num2, bytes.Length);
			return array;
		}

		public void Delete()
		{
			if (type == PackType.Official)
			{
				return;
			}
			deleted = true;
			for (int i = 0; i < skins.Count; i++)
			{
				skins[i].SetActive(false, false);
			}
			if (SkinPacks.Contains(this))
			{
				SkinPacks.Remove(this);
			}
			if (type == PackType.Local)
			{
				if (Directory.Exists(path))
				{
					Directory.Delete(path, true);
				}
				if (skinPacksDirs.Contains(path))
				{
					skinPacksDirs = skinPacksDirs.Where((string p) => p != path).ToArray();
				}
			}
			else
			{
				string text = path.Replace("\\", "/");
				if (ReferenceMaster.FolderToWorkshop.ContainsKey(text))
				{
					ulong num = ReferenceMaster.FolderToWorkshop[text];
					ReferenceMaster.Unsub(num);
				}
				if (workshopPacksDirs.Contains(text))
				{
					workshopPacksDirs = workshopPacksDirs.Where((string p) => p != path).ToArray();
				}
			}
			if (BlockSkinLoader.SkinModified != null)
			{
				BlockSkinLoader.SkinModified(this);
			}
		}

		public Skin FindAvailableSkin()
		{
			int num = 0;
			int num2 = -1;
			int num3 = -1;
			if (skins.Count == 0)
			{
				UnityEngine.Debug.LogWarning("no available skins in " + name);
				return null;
			}
			for (num = 0; num < skins.Count; num++)
			{
				Skin skin = skins[num];
				if (skin.enabled)
				{
					if (num3 == -1)
					{
						num3 = num;
					}
					if (skin.prefab.ID == settings.displayBlock)
					{
						break;
					}
					if (skin.prefab.ID == 1)
					{
						if (settings.displayBlock <= -1)
						{
							break;
						}
						num2 = num;
					}
					else if (skin.prefab.ID == 0)
					{
						num2 = num;
					}
				}
				if (num + 1 == skins.Count)
				{
					num = ((num2 == -1) ? ((num3 != -1) ? num3 : 0) : num2);
					break;
				}
			}
			return skins[num];
		}
	}

	[CompilerGenerated]
	private sealed class _003CCreateFile_003Ec__Iterator1A7 : IDisposable, IEnumerator, IEnumerator<object>
	{
		internal bool _003Cdone_003E__0;

		internal string path;

		internal int _0024PC;

		internal object _0024current;

		internal string _003C_0024_003Epath;

		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return _0024current;
			}
		}

		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return _0024current;
			}
		}

		public bool MoveNext()
		{
			uint num = (uint)_0024PC;
			_0024PC = -1;
			switch (num)
			{
			case 0u:
				_003Cdone_003E__0 = false;
				goto IL_0071;
			case 1u:
				{
					try
					{
						File.Create(path + "/disabled.mpt");
					}
					catch (IOException)
					{
					}
					finally
					{
						_003C_003E__Finally0();
					}
					goto IL_0071;
				}
				IL_0071:
				if (!_003Cdone_003E__0)
				{
					_0024current = new WaitForEndOfFrame();
					_0024PC = 1;
					return true;
				}
				break;
			}
			return false;
		}

		[DebuggerHidden]
		public void Dispose()
		{
			_0024PC = -1;
		}

		[DebuggerHidden]
		public void Reset()
		{
			throw new NotSupportedException();
		}

		private void _003C_003E__Finally0()
		{
			_003Cdone_003E__0 = true;
		}
	}

	[CompilerGenerated]
	private sealed class _003CDeleteFile_003Ec__Iterator1A8 : IDisposable, IEnumerator, IEnumerator<object>
	{
		internal bool _003Cdone_003E__0;

		internal string path;

		internal int _0024PC;

		internal object _0024current;

		internal string _003C_0024_003Epath;

		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return _0024current;
			}
		}

		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return _0024current;
			}
		}

		public bool MoveNext()
		{
			uint num = (uint)_0024PC;
			_0024PC = -1;
			switch (num)
			{
			case 0u:
				_003Cdone_003E__0 = false;
				goto IL_0070;
			case 1u:
				{
					try
					{
						File.Delete(path + "/disabled.mpt");
					}
					catch (IOException)
					{
					}
					finally
					{
						_003C_003E__Finally0();
					}
					goto IL_0070;
				}
				IL_0070:
				if (!_003Cdone_003E__0)
				{
					_0024current = new WaitForEndOfFrame();
					_0024PC = 1;
					return true;
				}
				break;
			}
			return false;
		}

		[DebuggerHidden]
		public void Dispose()
		{
			_0024PC = -1;
		}

		[DebuggerHidden]
		public void Reset()
		{
			throw new NotSupportedException();
		}

		private void _003C_003E__Finally0()
		{
			_003Cdone_003E__0 = true;
		}
	}

	private const int numOfBlocksByte = 96;

	public static SModifier UpdateAll = new SModifier();

	public static SModifier UpdateUI = new SModifier();

	public static string defaultString = "default";

	public Material LoadingMaterial;

	public Material LoadingGhostMaterial;

	public bool UseAnimation;

	public static SkinPack defaultPack = new SkinPack(1, true);

	public static List<SkinPack> SkinPacks = new List<SkinPack>();

	public static List<SkinPack.Skin> loadedSkins = new List<SkinPack.Skin>();

	protected static FileSystemWatcher watcher;

	protected static Dictionary<string, string> newPathsInSkins = new Dictionary<string, string>();

	protected static string[] skinPacksDirs = new string[0];

	protected static string[] workshopPacksDirs = new string[0];

	protected static string skinsRoot;

	protected static bool packsGenerated = false;

	protected static bool workshopPacksGenerated = false;

	private IEnumerator deleteFileCoroutine;

	private IEnumerator createFileCoroutine;

	protected static bool runningWorkshopRefresh = false;

	protected static string[] imgExts = new string[3] { ".jpg", ".png", ".jpeg" };

	public override string Name
	{
		get
		{
			return "BlockSkinLoader";
		}
	}

	public static event SkinModified SkinModified;

	public static event Action SkinPacksAdded;

	public static event SetupSkins SetupSkins;

	protected void Awake()
	{
		CreateDefaultPack();
		SingleInstance<PrefabMaster>.Instance.Awake();
		skinsRoot = StaticSettings.DataPath + "/Skins/";
		if (SingleInstance<BlockSkinLoader>.Instance == this)
		{
			UnityEngine.Object.DontDestroyOnLoad(SingleInstance<BlockSkinLoader>.Instance);
		}
		else
		{
			UnityEngine.Object.DestroyImmediate(this);
		}
		if (!Directory.Exists(skinsRoot))
		{
			Directory.CreateDirectory(skinsRoot);
		}
		SceneManager.sceneLoaded += OnSceneLoad;
	}

	public static void CreateDefaultPack()
	{
		if (defaultPack.isDefault)
		{
			return;
		}
		defaultPack.isDefault = true;
		defaultPack.name = LocalisationManager.GetTranslation(781);
		if (!SkinPacks.Contains(defaultPack))
		{
			SkinPacks.Add(defaultPack);
			if (BlockSkinLoader.SkinPacksAdded != null)
			{
				BlockSkinLoader.SkinPacksAdded();
			}
		}
	}

	private void OnDestroy()
	{
		if (watcher != null)
		{
			watcher.Dispose();
			watcher = null;
		}
	}

	public static void ToggleSkins()
	{
		if (OptionsMaster.skinsEnabled)
		{
			if (!packsGenerated)
			{
				SetCutomSkinPacksUp();
				if (BlockSkinLoader.SetupSkins != null)
				{
					BlockSkinLoader.SetupSkins();
				}
			}
			else
			{
				UpdateEverythingToDoWithSkins();
			}
		}
		else if (packsGenerated)
		{
			UpdateEverythingToDoWithSkins();
		}
	}

	public static void SetCutomSkinPacksUp()
	{
		if (!packsGenerated)
		{
			LoadNewSkins();
			if (watcher != null)
			{
				watcher.Dispose();
				watcher = null;
			}
			watcher = new FileSystemWatcher();
			watcher.Path = skinsRoot;
			watcher.NotifyFilter = NotifyFilters.DirectoryName;
			watcher.Created += OnChanged;
			watcher.EnableRaisingEvents = true;
		}
	}

	private static void OnChanged(object source, FileSystemEventArgs e)
	{
		if (!Path.HasExtension(e.FullPath))
		{
			string text = e.FullPath.Replace("\\", "/");
			string text2 = Directory.GetParent(e.FullPath).FullName.Replace("\\", "/");
			Directory.GetParent(text2).FullName.Replace("\\", "/");
			if (!text2.EndsWith("/"))
			{
				text2 += "/";
			}
			if (!newPathsInSkins.Keys.Contains(text))
			{
				newPathsInSkins.Add(text, text2);
			}
		}
	}

	protected void Update()
	{
		if (newPathsInSkins.Count == 0)
		{
			return;
		}
		string text = skinsRoot.Replace("\\", "/");
		List<string> list = newPathsInSkins.Keys.ToList();
		foreach (string item in list)
		{
			if (!newPathsInSkins.ContainsKey(item))
			{
				continue;
			}
			string text2 = newPathsInSkins[item];
			if (text2 == text)
			{
				LoadNewPackFromDirectory(item, ref skinPacksDirs, PackType.Local);
				continue;
			}
			foreach (SkinPack skinPack in SkinPacks)
			{
				if (skinPack.path == text2)
				{
					LoadNewSkinsIn(skinPack);
					break;
				}
			}
		}
		newPathsInSkins.Clear();
		UpdateEverythingToDoWithSkins();
	}

	private void OnSceneLoad(Scene scene, LoadSceneMode m)
	{
		if (AddPiece.IsMenuScene(scene.name))
		{
			BlockSkinLoader.SkinModified = null;
			foreach (int key in PrefabMaster.BlockPrefabs.Keys)
			{
				BlockSkinLoader.SkinModified = (SkinModified)Delegate.Combine(BlockSkinLoader.SkinModified, new SkinModified(PrefabMaster.BlockPrefabs[key].SkinModified));
			}
			ReferenceMaster.ClearBuildingBlocks();
		}
		else
		{
			StartCoroutine(setupMPSkin());
		}
	}

	private IEnumerator setupMPSkin()
	{
		if (!OptionsMaster.skinsEnabled)
		{
			OptionsMaster.skinsEnabled = true;
			yield return null;
			OptionsMaster.skinsEnabled = false;
		}
	}

	public static void UpdateEverythingToDoWithSkins()
	{
		if (BlockSkinLoader.SkinModified != null)
		{
			BlockSkinLoader.SkinModified(UpdateAll);
		}
	}

	public static void UpdateSkinsUI()
	{
		if (BlockSkinLoader.SkinModified != null)
		{
			BlockSkinLoader.SkinModified(UpdateUI);
		}
	}

	public static void SetAllPrefabsToPack(SkinPack pack)
	{
		for (int i = 0; i < PrefabMaster.BlockPrefabs.Count; i++)
		{
			PrefabMaster.BlockPrefabs.ElementAt(i).Value.VisualController.UpdateVisFromPack(pack);
		}
	}

	public static void SetAllBlocksToPack(SkinPack pack)
	{
		SetAllBlocksToPack(pack, Machine.Active());
	}

	public static void SetAllBlocksToPack(SkinPack pack, Machine machine)
	{
		List<BlockBehaviour> buildingBlocks = ReferenceMaster.GetBuildingBlocks(machine.PlayerID);
		SetBlocksToPack(pack, machine, buildingBlocks);
	}

	public static void SetSelectionToPack(SkinPack pack, Machine machine)
	{
		BlockSelectionTool selectionController = AdvancedBlockEditor.Instance.selectionController;
		List<BlockBehaviour> blockList = ((!StatMaster.advancedBuilding || selectionController.Count <= 0) ? ReferenceMaster.GetBuildingBlocks(machine.PlayerID) : selectionController.MachineSelection);
		SetBlocksToPack(pack, machine, blockList);
	}

	public static void SetBlocksToPack(SkinPack pack, Machine machine, List<BlockBehaviour> blockList)
	{
		List<UndoAction> list = new List<UndoAction>();
		List<BlockBehaviour> list2 = new List<BlockBehaviour>();
		for (int i = 0; i < blockList.Count; i++)
		{
			BlockBehaviour blockBehaviour = blockList[i];
			if (blockBehaviour != null)
			{
				SkinPack.Skin selectedSkin = blockBehaviour.VisualController.selectedSkin;
				if (selectedSkin != null && selectedSkin.pack != pack)
				{
					blockBehaviour.VisualController.UpdateVisFromPack(pack);
					if (machine.isLocalMachine)
					{
						list.Add(new UndoActionSkin(machine, blockBehaviour.Guid, blockBehaviour.VisualController.selectedSkin, selectedSkin));
						list2.Add(blockBehaviour);
					}
				}
			}
			else
			{
				blockList.RemoveAt(i--);
			}
		}
		if (!machine.isLocalMachine || list.Count <= 0)
		{
			return;
		}
		if (StatMaster.isMP)
		{
			byte[] array = pack.Encode();
			int num = 0;
			byte[][] array2 = new byte[list2.Count][];
			for (int j = 0; j < list2.Count; j++)
			{
				int num2 = NetworkCompression.PackedUIntLength(list2[j].BuildIndex, false);
				num += num2;
				byte[] array3 = new byte[num2];
				NetworkCompression.PackUInt(list2[j].BuildIndex, array3, 0, false, num2);
				array2[j] = array3;
			}
			int num3 = NetworkCompression.PackedUIntLength(list2.Count, false);
			byte[] array4 = new byte[num3 + num + array.Length];
			int num4 = 0;
			NetworkCompression.PackUInt(list2.Count, array4, num4, false, num3);
			num4 += num3;
			NetworkCompression.WriteArray(array2, array4, num4);
			num4 += num;
			Buffer.BlockCopy(array, 0, array4, num4, array.Length);
			byte[] messageData = CLZF2.Compress(array4);
			NetworkAuxAddPiece.Instance.SendFragmentedNetworkMessage(RPCMessageType.ApplyMachineSkin, messageData);
		}
		machine.UndoSystem.AddActions(list);
	}

	public static void ColourCodeClusters(bool enabled)
	{
		StatMaster.clusterCoded = enabled;
		List<uint> list = ReferenceMaster.AllBuildingBlockIndices();
		for (int i = 0; i < list.Count; i++)
		{
			uint index = list[i];
			List<BlockBehaviour> buildingBlocks = ReferenceMaster.GetBuildingBlocks(index);
			for (int j = 0; j < buildingBlocks.Count; j++)
			{
				BlockBehaviour blockBehaviour = buildingBlocks[j];
				if (blockBehaviour.Prefab.hasBVC)
				{
					blockBehaviour.VisualController.SetNormal();
					if (blockBehaviour.hasSimBlock)
					{
						blockBehaviour.SimBlock.VisualController.SetNormal();
					}
				}
			}
		}
	}

	public static void SetAero(bool enabled)
	{
		if (enabled != StatMaster.aeroCoded)
		{
			StatMaster.aeroCoded = enabled;
			SetIntensity(enabled);
		}
	}

	public static void SetStress(bool enabled)
	{
		if (enabled != StatMaster.stressCoded)
		{
			StatMaster.stressCoded = enabled;
			SetIntensity(enabled);
		}
	}

	public static void SetIntensity(bool enabled)
	{
		bool flag = StatMaster.aeroCoded && StatMaster.isClient && !StatMaster.isLocalSim;
		List<uint> list = ReferenceMaster.AllBuildingBlockIndices();
		for (int i = 0; i < list.Count; i++)
		{
			uint index = list[i];
			List<BlockBehaviour> buildingBlocks = ReferenceMaster.GetBuildingBlocks(index);
			for (int j = 0; j < buildingBlocks.Count; j++)
			{
				BlockBehaviour blockBehaviour = buildingBlocks[j];
				if (!blockBehaviour.Prefab.hasBVC)
				{
					continue;
				}
				if (blockBehaviour.hasSimBlock && blockBehaviour.ShowInVisualiser())
				{
					BlockBehaviour simBlock = blockBehaviour.SimBlock;
					if (simBlock.HasParentMachine && !simBlock.RegisteredSimUpdate)
					{
						if (enabled)
						{
							if (!simBlock.ParentMachine.ContainedInSimUpdate(simBlock))
							{
								simBlock.ParentMachine.AddToSimUpdate(simBlock);
								if (flag)
								{
									simBlock.ClientAddToExternalForceObjects();
								}
							}
						}
						else if (simBlock.ParentMachine.ContainedInSimUpdate(simBlock))
						{
							simBlock.ParentMachine.RemoveFromSimUpdate(simBlock);
							if (flag)
							{
								simBlock.ClientAddToExternalForceObjects(true);
							}
						}
					}
				}
				blockBehaviour.VisualController.SetNormal();
				if (blockBehaviour.hasSimBlock)
				{
					blockBehaviour.SimBlock.VisualController.SetNormal();
				}
			}
		}
		if (flag)
		{
			ReferenceMaster.Instance.StartCoroutine(ReferenceMaster.UpdateExtrenalForceArray());
		}
	}

	public static void DisableBlockSkin(SkinPack.Skin vis)
	{
		SingleInstance<BlockSkinLoader>.Instance.DisableSkin(vis);
	}

	public static void EnableBlockSkin(SkinPack.Skin vis)
	{
		SingleInstance<BlockSkinLoader>.Instance.EnableSkin(vis);
	}

	public void DisableSkin(SkinPack.Skin vis)
	{
		if (vis.path != null)
		{
			if (deleteFileCoroutine != null)
			{
				StopCoroutine(deleteFileCoroutine);
			}
			vis.SetActive(false);
			if (!File.Exists(vis.path + "/disabled.mpt"))
			{
				createFileCoroutine = CreateFile(vis.path);
				StartCoroutine(createFileCoroutine);
			}
		}
	}

	public void EnableSkin(SkinPack.Skin vis)
	{
		if (vis.path != null)
		{
			if (createFileCoroutine != null)
			{
				SingleInstance<BlockSkinLoader>.Instance.StopCoroutine(createFileCoroutine);
			}
			vis.SetActive(true);
			if (File.Exists(vis.path + "/disabled.mpt"))
			{
				deleteFileCoroutine = DeleteFile(vis.path);
				SingleInstance<BlockSkinLoader>.Instance.StartCoroutine(deleteFileCoroutine);
			}
		}
	}

	protected static IEnumerator CreateFile(string path)
	{
		bool done = false;
		while (!done)
		{
			yield return new WaitForEndOfFrame();
			try
			{
				File.Create(path + "/disabled.mpt");
			}
			catch (IOException)
			{
			}
			finally
			{
				((_003CCreateFile_003Ec__Iterator1A7)(object)this)._003C_003E__Finally0();
			}
		}
	}

	protected static IEnumerator DeleteFile(string path)
	{
		bool done = false;
		while (!done)
		{
			yield return new WaitForEndOfFrame();
			try
			{
				File.Delete(path + "/disabled.mpt");
			}
			catch (IOException)
			{
			}
			finally
			{
				((_003CDeleteFile_003Ec__Iterator1A8)(object)this)._003C_003E__Finally0();
			}
		}
	}

	protected void GenerateSkinPacks()
	{
		packsGenerated = true;
		int num = 1;
		foreach (PrefabMaster.PreloadedSkins officialBlockSkin in PrefabMaster.OfficialBlockSkins)
		{
			if (!DlcManager.Instance.IsSupporter() && officialBlockSkin.id == "supporter")
			{
				foreach (PrefabMaster.PreloadedSkin blockSkin in officialBlockSkin.blockSkins)
				{
					blockSkin.mesh = null;
					blockSkin.texture = null;
					blockSkin.material = null;
				}
				continue;
			}
			if (officialBlockSkin.id == "AchievementCube" && !Journal.GetAchievement(51).completed)
			{
				foreach (PrefabMaster.PreloadedSkin blockSkin2 in officialBlockSkin.blockSkins)
				{
					blockSkin2.mesh = null;
					blockSkin2.texture = null;
					blockSkin2.material = null;
				}
				continue;
			}
			if (officialBlockSkin.id == "AchievementCubeSS" && !Journal.GetAchievement(52).completed)
			{
				foreach (PrefabMaster.PreloadedSkin blockSkin3 in officialBlockSkin.blockSkins)
				{
					blockSkin3.mesh = null;
					blockSkin3.texture = null;
					blockSkin3.material = null;
				}
				continue;
			}
			SkinPack skinPack = new SkinPack(1, true);
			SkinPacks.Insert(num, skinPack);
			skinPack.name = officialBlockSkin.name;
			skinPack.id = officialBlockSkin.id;
			skinPack.type = PackType.Official;
			foreach (PrefabMaster.PreloadedSkin blockSkin4 in officialBlockSkin.blockSkins)
			{
				BlockPrefab blockPrefab = PrefabMaster.BlockPrefabs[(int)blockSkin4.type];
				if (!blockPrefab.LoadNewVisuals)
				{
					continue;
				}
				bool flag = blockSkin4.mesh != null;
				bool flag2 = blockSkin4.texture != null;
				Texture texture = blockSkin4.texture;
				if (flag || flag2)
				{
					if (!flag2)
					{
						texture = ((!(blockSkin4.material != null)) ? blockPrefab.DefaultSkin.texture : blockSkin4.material.mainTexture);
					}
					if (flag || texture != null)
					{
						bool flag3 = blockPrefab.hasShortVis;
						SkinPack.Skin skin = skinPack.CreateSkinFor(blockPrefab);
						skin.SetPreloaded(blockSkin4.mesh, texture, new Material[1] { blockSkin4.material });
						skin.SetActive(true);
						switch (blockSkin4.type)
						{
						case BlockType.Brace:
						case BlockType.Spring:
						case BlockType.RopeWinch:
						case BlockType.RopeMeasure:
							flag3 = true;
							break;
						case BlockType.DoubleWoodenBlock:
							flag3 = false;
							break;
						}
						if (flag3 && blockSkin4.extra.Length > 0)
						{
							SkinPack.Skin skin2 = SkinPack.Skin.Incomplete((int)blockSkin4.type, skinPack);
							skin2.SetPreloaded(blockSkin4.extra[0], null, null);
							skin.shortSkin = skin2;
						}
					}
				}
				else
				{
					UnityEngine.Debug.LogWarning("[BlockSkinLoader]: failed to create preloaded skin for " + blockSkin4.type);
				}
			}
			num++;
		}
		LoadNewPacksFromDirectory(Directory.GetDirectories(skinsRoot), ref skinPacksDirs, PackType.Local);
	}

	private static bool IsWorkshopInitialized()
	{
		if (SteamManager.Initialized)
		{
			return true;
		}
		return false;
	}

	public static void LoadNewSkins()
	{
		if (packsGenerated)
		{
			LoadNewPacksFromDirectory(Directory.GetDirectories(skinsRoot), ref skinPacksDirs, PackType.Local, true);
			for (int i = 0; i < SkinPacks.Count; i++)
			{
				LoadNewSkinsIn(SkinPacks[i]);
			}
			if (IsWorkshopInitialized())
			{
				ReferenceMaster.RefreshWorkshop();
			}
		}
		else
		{
			SingleInstance<BlockSkinLoader>.Instance.GenerateSkinPacks();
		}
		UpdateEverythingToDoWithSkins();
	}

	public bool WorkshopTryRefresh(string[] newWorkshopDirs)
	{
		if (runningWorkshopRefresh)
		{
			return false;
		}
		runningWorkshopRefresh = true;
		if (packsGenerated)
		{
			LoadNewPacksFromDirectory(newWorkshopDirs, ref workshopPacksDirs, PackType.Workshop, true);
		}
		runningWorkshopRefresh = false;
		workshopPacksGenerated = true;
		UpdateEverythingToDoWithSkins();
		return true;
	}

	public static void LoadNewPacksFromDirectory(string[] currentDirs, ref string[] loadedDirs, PackType type, bool hardCheck = false)
	{
		if (!hardCheck && currentDirs.Length <= loadedDirs.Length)
		{
			return;
		}
		List<string> list = loadedDirs.ToList();
		string text = null;
		for (int i = 0; i < currentDirs.Length; i++)
		{
			string text2 = currentDirs[i].Replace("\\", "/");
			string text3 = new DirectoryInfo(text2).Name;
			string text4 = text3.ToLower();
			if (!(text4 == "template") && !(text4 == "3dprint") && !list.Contains(text2))
			{
				SkinPacks.Add(new SkinPack(0, false));
				SkinPack skinPack = SkinPacks.Last();
				skinPack.settings = GetSkinConfig(text2);
				skinPack.name = text3;
				skinPack.type = type;
				skinPack.path = text2;
				LoadNewSkinsIn(skinPack);
				list.Add(text2);
				if (text != null)
				{
					text += ", ";
				}
				text = text + "'" + skinPack.name + "'";
			}
		}
		if (text != null && BesiegeLogFilter.logInfo)
		{
			UnityEngine.Debug.Log("[BlockSkinLoader] Found new skin packs: " + text + ".");
			if (BlockSkinLoader.SkinPacksAdded != null)
			{
				BlockSkinLoader.SkinPacksAdded();
			}
		}
		loadedDirs = list.ToArray();
	}

	public static void LoadNewPackFromDirectory(string path, ref string[] loadedDirs, PackType type)
	{
		string text = new DirectoryInfo(path).Name;
		string text2 = text.ToLower();
		if (!(text2 == "template") && !(text2 == "3dprint"))
		{
			List<string> list = new List<string>(loadedDirs);
			SkinPack skinPack = new SkinPack(0, false);
			skinPack.settings = GetSkinConfig(path);
			SkinPacks.Add(skinPack);
			skinPack.name = text;
			skinPack.type = type;
			skinPack.path = path;
			LoadNewSkinsIn(skinPack);
			UnityEngine.Debug.Log("[BlockSkinLoader] Found new skin pack: " + text + ".");
			list.Add(path);
			loadedDirs = list.ToArray();
		}
	}

	private static bool IsImage(string extension)
	{
		for (int i = 0; i < imgExts.Length; i++)
		{
			if (extension.Equals(imgExts[i]))
			{
				return true;
			}
		}
		return false;
	}

	private static bool IsModel(string extension)
	{
		return extension.Equals(".obj");
	}

	private static string FixPath(string path)
	{
		return path.Replace("\\", "/");
	}

	private static string GetFileName(string fullPath)
	{
		return fullPath.Substring(fullPath.LastIndexOf(Path.DirectorySeparatorChar) + 1);
	}

	private static string GetPath(string fullPath)
	{
		return fullPath.Substring(0, fullPath.LastIndexOf(Path.DirectorySeparatorChar));
	}

	private static string GetParentDir(string fullPath)
	{
		string path = GetPath(fullPath);
		return path.Substring(path.LastIndexOf(Path.DirectorySeparatorChar) + 1);
	}

	private static string GetDirName(string fullPath)
	{
		return fullPath.Substring(fullPath.LastIndexOf(Path.DirectorySeparatorChar) + 1);
	}

	private static List<SkinPack.Skin> GetSkins(SkinPack pack)
	{
		Dictionary<string, SkinPack.Skin> dictionary = new Dictionary<string, SkinPack.Skin>();
		List<SkinPack.Skin> list = new List<SkinPack.Skin>();
		KeyValuePair<int, BlockPrefab> keyValuePair = default(KeyValuePair<int, BlockPrefab>);
		if (!Directory.Exists(pack.path))
		{
			return list;
		}
		string[] directories = Directory.GetDirectories(pack.path);
		if (pack.settings.useSingleTexture)
		{
			pack.settings.sharedTexPath = pack.path + "/tex.png";
		}
		for (int i = 0; i < directories.Length; i++)
		{
			SkinPack.Skin value = null;
			string prefabName = GetDirName(directories[i]);
			KeyValuePair<int, BlockPrefab> keyValuePair2 = PrefabMaster.BlockPrefabs.FirstOrDefault((KeyValuePair<int, BlockPrefab> x) => x.Value.name.Equals(prefabName));
			if (keyValuePair2.Equals(keyValuePair))
			{
				continue;
			}
			BlockPrefab value2 = keyValuePair2.Value;
			if (!value2.LoadNewVisuals)
			{
				continue;
			}
			if (dictionary.TryGetValue(prefabName, out value))
			{
				if (value == null)
				{
					continue;
				}
			}
			else
			{
				if (pack.prefabs.Contains(value2))
				{
					continue;
				}
				value = pack.CreateSkinFor(value2).SetPath(directories[i]);
				dictionary.Add(prefabName, value);
			}
			if (value2.hasShortVis && value2.Type != BlockType.DoubleWoodenBlock)
			{
				value.shortSkin = value2.DefaultSkin.shortSkin;
				string text = FixPath(directories[i]);
				if (Directory.Exists(text + "/Short"))
				{
					string[] files = Directory.GetFiles(text + "/Short");
					foreach (string text2 in files)
					{
						if (IsModel(Path.GetExtension(text2)))
						{
							value.shortSkin = SkinPack.Skin.Incomplete((int)value2.Type, pack).SetPath(text + "/Short").SetObj(text2);
							break;
						}
					}
				}
			}
			string[] files2 = Directory.GetFiles(directories[i]);
			for (int num2 = 0; num2 < files2.Length; num2++)
			{
				string extension = Path.GetExtension(files2[num2]);
				if (IsModel(extension))
				{
					value.SetObj(FixPath(files2[num2]));
				}
				else if (IsImage(extension))
				{
					value.SetTex(FixPath(files2[num2]));
				}
				else if (GetFileName(files2[num2]).Equals("disabled.mpt"))
				{
					value.SetActive(false, false);
				}
			}
			list.Add(value.Conclude());
		}
		return list;
	}

	public static SkinPack.Config GetSkinConfig(string path)
	{
		return SkinXmlLoader.Load(path);
	}

	public static void LoadNewSkinsIn(SkinPack pack)
	{
		if (pack.path != null)
		{
			SingleInstance<BlockSkinLoader>.Instance.StartCoroutineAsync(IELoadNewSkinsIn(pack, GetSkins(pack)));
		}
	}

	public static IEnumerator IELoadNewSkinsIn(SkinPack pack, List<SkinPack.Skin> skins)
	{
		SkinPack.Skin doubleWoodSkin = null;
		BitArray bitfield = new BitArray(96);
		for (int i = 0; i < PrefabMaster.BlockPrefabs.Count; i++)
		{
			BlockPrefab prefab = PrefabMaster.BlockPrefabs.ElementAt(i).Value;
			if (pack.prefabs.Contains(prefab))
			{
				if (prefab.ID < 96)
				{
					bitfield[prefab.ID] = true;
				}
				continue;
			}
			SkinPack.Skin curr = skins.Find((SkinPack.Skin s) => s.prefab.Equals(prefab));
			if (curr != null)
			{
				if (prefab.ID < 96)
				{
					bitfield[prefab.ID] = true;
				}
				pack.prefabs.Add(prefab);
				if (prefab.ID == 1)
				{
					doubleWoodSkin = curr;
				}
				else if (prefab.ID == 15 && doubleWoodSkin != null)
				{
					doubleWoodSkin.shortSkin = curr;
				}
			}
		}
		if (pack.type == PackType.Workshop)
		{
			pack.path = pack.path.Replace("\\", "/");
			ulong workshopId;
			if (ReferenceMaster.FolderToWorkshop.TryGetValue(pack.path, out workshopId))
			{
				pack.id = workshopId.ToString();
				pack.workshopURL = "http://steamcommunity.com/sharedfiles/filedetails/?id=" + pack.id;
			}
			else if (File.Exists(pack.path + "/workshopid.txt"))
			{
				ulong fileWorkshopId = 0uL;
				try
				{
					StreamReader sr = new StreamReader(pack.path + "/workshopid.txt");
					fileWorkshopId = ulong.Parse(sr.ReadLine());
					sr.Close();
				}
				catch (FileNotFoundException ex)
				{
					FileNotFoundException fnfe = ex;
					UnityEngine.Debug.Log("Error locating file: " + fnfe);
				}
				catch (Exception ex2)
				{
					Exception e = ex2;
					UnityEngine.Debug.Log("Error reading file: " + e);
				}
				pack.id = fileWorkshopId.ToString();
				pack.workshopURL = "http://steamcommunity.com/sharedfiles/filedetails/?id=" + pack.id;
			}
			else
			{
				pack.id = pack.name[0] + bConvert.ByteArrayTo64String(bConvert.ConvertBitsToBytes(bitfield));
			}
		}
		else
		{
			pack.id = pack.name[0] + bConvert.ByteArrayTo64String(bConvert.ConvertBitsToBytes(bitfield));
		}
		yield break;
	}
}

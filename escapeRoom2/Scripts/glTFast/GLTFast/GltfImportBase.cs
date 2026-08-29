using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GLTFast.Addons;
using GLTFast.Jobs;
using GLTFast.Loading;
using GLTFast.Logging;
using GLTFast.Materials;
using GLTFast.Schema;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering;

namespace GLTFast
{
	public abstract class GltfImportBase<TRoot> : GltfImportBase, IGltfReadable<TRoot>, IGltfReadable, IMaterialProvider, IMaterialsVariantsProvider where TRoot : RootBase
	{
		private TRoot m_Root;

		protected override RootBase Root
		{
			get
			{
				return m_Root;
			}
			set
			{
				m_Root = (TRoot)value;
			}
		}

		public GltfImportBase(IDownloadProvider downloadProvider = null, IDeferAgent deferAgent = null, IMaterialGenerator materialGenerator = null, ICodeLogger logger = null)
			: base(downloadProvider, deferAgent, materialGenerator, logger)
		{
		}

		public TRoot GetSourceRoot()
		{
			return m_Root;
		}
	}
	public abstract class GltfImportBase : IGltfReadable, IMaterialProvider, IMaterialsVariantsProvider, IGltfBuffers, IDisposable
	{
		internal const int DefaultBatchCount = 512;

		private const int k_JsonParseSpeed = 80000000;

		private const int k_Base64DecodeSpeed = 150000000;

		private const string k_PrimitiveName = "Primitive";

		private static readonly HashSet<string> k_SupportedExtensions = new HashSet<string> { "KHR_materials_pbrSpecularGlossiness", "KHR_materials_unlit", "KHR_materials_variants", "KHR_texture_transform", "KHR_mesh_quantization", "KHR_materials_transmission", "EXT_mesh_gpu_instancing", "KHR_lights_punctual", "KHR_materials_clearcoat" };

		private static IDeferAgent s_DefaultDeferAgent;

		private static MeshComparer s_MeshComparer = new MeshComparer();

		private IDownloadProvider m_DownloadProvider;

		private IMaterialGenerator m_MaterialGenerator;

		private Dictionary<Type, ImportAddonInstance> m_ImportInstances;

		private ImportSettings m_Settings;

		private NativeArray<byte>.ReadOnly[] m_Buffers;

		private List<IDisposable> m_VolatileDisposables;

		private GlbBinChunk[] m_BinChunks;

		private Dictionary<int, Task<IDownload>> m_DownloadTasks;

		private Dictionary<int, TextureDownloadBase> m_TextureDownloadTasks;

		private IDisposable[] m_AccessorData;

		private AccessorUsage[] m_AccessorUsage;

		private JobHandle m_AccessorJobsHandle;

		private List<MeshOrder> m_MeshOrders;

		private List<ImageCreateContext> m_ImageCreateContexts;

		private Texture2D[] m_Images;

		private Texture2D[] m_Textures;

		private ImageFormat[] m_ImageFormats;

		private bool[] m_ImageReadable;

		private bool[] m_ImageGamma;

		private GlbBinChunk? m_GlbBinChunk;

		private HashSet<int> m_MaterialPointsSupport;

		private bool m_DefaultMaterialPointsSupport;

		private UnityEngine.Material[] m_Materials;

		private List<UnityEngine.Object> m_Resources;

		private string[] m_NodeNames;

		private List<UnityEngine.Mesh> m_Meshes;

		private FlatArray<MeshAssignment> m_MeshAssignments;

		private Matrix4x4[][] m_SkinsInverseBindMatrices;

		private AnimationClip[] m_AnimationClips;

		public ICodeLogger Logger { get; }

		public IDeferAgent DeferAgent { get; }

		protected abstract RootBase Root { get; set; }

		public bool LoadingDone { get; private set; }

		public bool LoadingError { get; private set; }

		public int MaterialCount
		{
			get
			{
				UnityEngine.Material[] materials = m_Materials;
				if (materials == null)
				{
					return 0;
				}
				return materials.Length;
			}
		}

		public int ImageCount
		{
			get
			{
				Texture2D[] images = m_Images;
				if (images == null)
				{
					return 0;
				}
				return images.Length;
			}
		}

		public int TextureCount
		{
			get
			{
				Texture2D[] textures = m_Textures;
				if (textures == null)
				{
					return 0;
				}
				return textures.Length;
			}
		}

		public int? DefaultSceneIndex
		{
			get
			{
				if (Root == null || Root.scene < 0)
				{
					return null;
				}
				return Root.scene;
			}
		}

		public int SceneCount => (Root?.Scenes?.Count).GetValueOrDefault();

		public IReadOnlyCollection<UnityEngine.Mesh> Meshes => m_Meshes;

		public int MaterialsVariantsCount => Root.MaterialsVariantsCount;

		public event Action LoadAccessorDataEvent;

		public event Action<int, int, int[]> MeshResultAssigned;

		public GltfImportBase(IDownloadProvider downloadProvider = null, IDeferAgent deferAgent = null, IMaterialGenerator materialGenerator = null, ICodeLogger logger = null)
		{
			m_DownloadProvider = downloadProvider ?? new DefaultDownloadProvider();
			if (deferAgent == null)
			{
				if (s_DefaultDeferAgent == null || (s_DefaultDeferAgent is UnityEngine.Object obj && obj == null))
				{
					GameObject gameObject = new GameObject("glTF-StableFramerate");
					UnityEngine.Object.DontDestroyOnLoad(gameObject);
					SetDefaultDeferAgent(gameObject.AddComponent<TimeBudgetPerFrameDeferAgent>());
					gameObject.AddComponent<DefaultDeferAgent>();
				}
				DeferAgent = s_DefaultDeferAgent;
			}
			else
			{
				DeferAgent = deferAgent;
			}
			m_MaterialGenerator = materialGenerator ?? MaterialGenerator.GetDefaultMaterialGenerator();
			Logger = logger;
			ImportAddonRegistry.InjectAllAddons(this);
		}

		public static void SetDefaultDeferAgent(IDeferAgent deferAgent)
		{
			s_DefaultDeferAgent = deferAgent;
		}

		public static void UnsetDefaultDeferAgent(IDeferAgent deferAgent)
		{
			if (s_DefaultDeferAgent == deferAgent)
			{
				s_DefaultDeferAgent = null;
			}
		}

		public void AddImportAddonInstance<T>(T importInstance) where T : ImportAddonInstance
		{
			if (m_ImportInstances == null)
			{
				m_ImportInstances = new Dictionary<Type, ImportAddonInstance>();
			}
			m_ImportInstances[typeof(T)] = importInstance;
		}

		public T GetImportAddonInstance<T>() where T : ImportAddonInstance
		{
			if (m_ImportInstances == null)
			{
				return null;
			}
			if (m_ImportInstances.TryGetValue(typeof(T), out var value))
			{
				return (T)value;
			}
			return null;
		}

		public async Task<bool> Load(string url, ImportSettings importSettings = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return await Load(new Uri(url, UriKind.RelativeOrAbsolute), importSettings, cancellationToken);
		}

		public async Task<bool> Load(Uri url, ImportSettings importSettings = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			m_Settings = importSettings ?? new ImportSettings();
			return await LoadFromUri(url, cancellationToken);
		}

		public async Task<bool> Load(byte[] data, Uri uri = null, ImportSettings importSettings = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			ManagedNativeArray<byte, byte> managedNativeArray = new ManagedNativeArray<byte, byte>(data);
			if (m_VolatileDisposables == null)
			{
				m_VolatileDisposables = new List<IDisposable>();
			}
			m_VolatileDisposables.Add(managedNativeArray);
			return await Load(managedNativeArray.nativeArray.AsReadOnly(), uri, importSettings, cancellationToken);
		}

		public async Task<bool> Load(NativeArray<byte>.ReadOnly data, Uri uri = null, ImportSettings importSettings = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (GltfGlobals.IsGltfBinary(data))
			{
				return await LoadGltfBinaryInternal(data, uri, importSettings, cancellationToken);
			}
			string json = Encoding.UTF8.GetString(data.ToArray(), 0, data.Length);
			return await LoadGltfJson(json, uri, importSettings, cancellationToken);
		}

		public async Task<bool> LoadFile(string localPath, Uri uri = null, ImportSettings importSettings = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			FileStream fs = new FileStream(localPath, FileMode.Open, FileAccess.Read);
			bool result = await LoadStream(fs, uri, importSettings, cancellationToken);
			fs.Dispose();
			return result;
		}

		public async Task<bool> LoadStream(Stream stream, Uri uri = null, ImportSettings importSettings = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!stream.CanRead)
			{
				Logger?.Error(LogCode.StreamError, "Not readable");
				return false;
			}
			long initialStreamPosition = (stream.CanSeek ? stream.Position : (-1));
			byte[] firstBytes = new byte[4];
			if (!(await stream.ReadToArrayAsync(firstBytes, 0, firstBytes.Length, cancellationToken)))
			{
				Logger?.Error(LogCode.StreamError, "First bytes");
				return false;
			}
			if (cancellationToken.IsCancellationRequested)
			{
				return false;
			}
			if (GltfGlobals.IsGltfBinary(firstBytes))
			{
				byte[] glbHeader = new byte[8];
				if (!(await stream.ReadToArrayAsync(glbHeader, 0, glbHeader.Length, cancellationToken)))
				{
					Logger?.Error(LogCode.StreamError, "glb header");
					return false;
				}
				uint length = BitConverter.ToUInt32(glbHeader, 4);
				if (length >= int.MaxValue)
				{
					Logger?.Error("glb exceeds 2GB limit.");
					return false;
				}
				using NativeArray<byte> data = new NativeArray<byte>((int)length, Allocator.Persistent);
				UnmanagedMemoryStream dataStream = data.ToUnmanagedMemoryStream();
				await dataStream.WriteAsync(firstBytes, cancellationToken);
				await dataStream.WriteAsync(glbHeader, cancellationToken);
				await stream.CopyToAsync(dataStream, (int)(length - dataStream.Position), cancellationToken);
				return await LoadGltfBinaryInternal(data.AsReadOnly(), uri, importSettings, cancellationToken);
			}
			StreamReader reader = new StreamReader(stream);
			string json;
			if (stream.CanSeek)
			{
				stream.Seek(initialStreamPosition, SeekOrigin.Begin);
				json = await reader.ReadToEndAsync();
			}
			else
			{
				string text = Encoding.UTF8.GetString(firstBytes);
				json = text + await reader.ReadToEndAsync();
			}
			reader.Dispose();
			bool flag = !cancellationToken.IsCancellationRequested;
			if (flag)
			{
				flag = await LoadGltfJson(json, uri, importSettings, cancellationToken);
			}
			return flag;
		}

		[Obsolete("Use the generic Load instead.")]
		public async Task<bool> LoadGltfBinary(byte[] bytes, Uri uri = null, ImportSettings importSettings = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			ManagedNativeArray<byte, byte> managedNativeArray = new ManagedNativeArray<byte, byte>(bytes);
			if (m_VolatileDisposables == null)
			{
				m_VolatileDisposables = new List<IDisposable>();
			}
			m_VolatileDisposables.Add(managedNativeArray);
			return await LoadGltfBinaryInternal(managedNativeArray.nativeArray.AsReadOnly(), uri, importSettings, cancellationToken);
		}

		public async Task<bool> LoadGltfJson(string json, Uri uri = null, ImportSettings importSettings = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			m_Settings = importSettings ?? new ImportSettings();
			bool success = await LoadGltf(json, uri);
			if (success)
			{
				await LoadContent();
			}
			bool flag = success;
			if (flag)
			{
				flag = await Prepare();
			}
			success = flag;
			DisposeVolatileData();
			LoadingError = !success;
			LoadingDone = true;
			return success;
		}

		[Obsolete("Use InstantiateMainSceneAsync for increased performance and safety. Consult the Upgrade Guide for instructions.")]
		public bool InstantiateMainScene(Transform parent)
		{
			return InstantiateMainSceneAsync(parent).Result;
		}

		[Obsolete("Use InstantiateMainSceneAsync for increased performance and safety. Consult the Upgrade Guide for instructions.")]
		public bool InstantiateMainScene(IInstantiator instantiator)
		{
			return InstantiateMainSceneAsync(instantiator).Result;
		}

		[Obsolete("Use InstantiateSceneAsync for increased performance and safety. Consult the Upgrade Guide for instructions.")]
		public bool InstantiateScene(Transform parent, int sceneIndex = 0)
		{
			return InstantiateSceneAsync(parent, sceneIndex).Result;
		}

		[Obsolete("Use InstantiateSceneAsync for increased performance and safety. Consult the Upgrade Guide for instructions.")]
		public bool InstantiateScene(IInstantiator instantiator, int sceneIndex = 0)
		{
			return InstantiateSceneAsync(instantiator, sceneIndex).Result;
		}

		public async Task<bool> InstantiateMainSceneAsync(Transform parent, CancellationToken cancellationToken = default(CancellationToken))
		{
			GameObjectInstantiator instantiator = new GameObjectInstantiator(this, parent);
			return await InstantiateMainSceneAsync(instantiator, cancellationToken);
		}

		public async Task<bool> InstantiateMainSceneAsync(IInstantiator instantiator, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!LoadingDone || LoadingError)
			{
				return false;
			}
			if (Root.scene < 0)
			{
				return true;
			}
			return await InstantiateSceneAsync(instantiator, Root.scene, cancellationToken);
		}

		public async Task<bool> InstantiateSceneAsync(Transform parent, int sceneIndex = 0, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!LoadingDone || LoadingError)
			{
				return false;
			}
			if (sceneIndex < 0 || sceneIndex > Root.Scenes.Count)
			{
				return false;
			}
			GameObjectInstantiator instantiator = new GameObjectInstantiator(this, parent);
			return await InstantiateSceneAsync(instantiator, sceneIndex, cancellationToken);
		}

		public async Task<bool> InstantiateSceneAsync(IInstantiator instantiator, int sceneIndex = 0, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!LoadingDone || LoadingError)
			{
				return false;
			}
			if (sceneIndex < 0 || sceneIndex > Root.Scenes.Count)
			{
				return false;
			}
			await InstantiateSceneInternal(instantiator, sceneIndex);
			return true;
		}

		public void Dispose()
		{
			if (m_ImportInstances != null)
			{
				foreach (KeyValuePair<Type, ImportAddonInstance> importInstance in m_ImportInstances)
				{
					importInstance.Value.Dispose();
				}
				m_ImportInstances = null;
			}
			m_NodeNames = null;
			DisposeArray(m_Materials);
			m_Materials = null;
			DisposeArray(m_AnimationClips);
			m_AnimationClips = null;
			DisposeArray(m_Textures);
			m_Textures = null;
			if (m_AccessorData != null)
			{
				IDisposable[] accessorData = m_AccessorData;
				for (int i = 0; i < accessorData.Length; i++)
				{
					accessorData[i]?.Dispose();
				}
				m_AccessorData = null;
			}
			m_MeshAssignments = null;
			DisposeArray(m_Meshes);
			m_Meshes = null;
			DisposeArray(m_Resources);
			m_Resources = null;
			static void DisposeArray(IEnumerable<UnityEngine.Object> objects)
			{
				if (objects != null)
				{
					foreach (UnityEngine.Object @object in objects)
					{
						SafeDestroy(@object);
					}
				}
			}
		}

		public string GetSceneName(int sceneIndex)
		{
			return Root?.Scenes?[sceneIndex]?.name;
		}

		public UnityEngine.Material GetMaterial(int index = 0)
		{
			if (m_Materials != null && index >= 0 && index < m_Materials.Length)
			{
				return m_Materials[index];
			}
			return null;
		}

		public async Task<UnityEngine.Material> GetMaterialAsync(int index)
		{
			return await GetMaterialAsync(index, default(CancellationToken));
		}

		public Task<UnityEngine.Material> GetMaterialAsync(int index, CancellationToken cancellationToken)
		{
			return Task.FromResult(GetMaterial(index));
		}

		public UnityEngine.Material GetDefaultMaterial()
		{
			m_MaterialGenerator.SetLogger(Logger);
			UnityEngine.Material defaultMaterial = m_MaterialGenerator.GetDefaultMaterial(m_DefaultMaterialPointsSupport);
			m_MaterialGenerator.SetLogger(null);
			return defaultMaterial;
		}

		public async Task<UnityEngine.Material> GetDefaultMaterialAsync()
		{
			return await GetDefaultMaterialAsync(default(CancellationToken));
		}

		public Task<UnityEngine.Material> GetDefaultMaterialAsync(CancellationToken cancellationToken)
		{
			return Task.FromResult(GetDefaultMaterial());
		}

		public Texture2D GetImage(int index = 0)
		{
			if (m_Images != null && index >= 0 && index < m_Images.Length)
			{
				return m_Images[index];
			}
			return null;
		}

		public Texture2D GetTexture(int index = 0)
		{
			if (m_Textures != null && index >= 0 && index < m_Textures.Length)
			{
				return m_Textures[index];
			}
			return null;
		}

		public bool IsTextureYFlipped(int index = 0)
		{
			return false;
		}

		public AnimationClip[] GetAnimationClips()
		{
			return m_AnimationClips;
		}

		[Obsolete("Use Meshes instead.")]
		public UnityEngine.Mesh[] GetMeshes()
		{
			if (m_Meshes == null || m_Meshes.Count < 1)
			{
				return Array.Empty<UnityEngine.Mesh>();
			}
			return m_Meshes.ToArray();
		}

		public int GetMeshCount(int meshIndex)
		{
			return m_MeshAssignments.GetLength(meshIndex);
		}

		public IEnumerable<UnityEngine.Mesh> GetMeshes(int meshIndex)
		{
			foreach (MeshAssignment item in m_MeshAssignments.Values(meshIndex))
			{
				yield return item.mesh;
			}
		}

		public UnityEngine.Mesh GetMesh(int meshIndex, int meshNumeration)
		{
			return m_MeshAssignments.GetValue(meshIndex, meshNumeration).mesh;
		}

		public CameraBase GetSourceCamera(uint index)
		{
			if (Root?.Cameras != null && index < Root.Cameras.Count)
			{
				return Root.Cameras[(int)index];
			}
			return null;
		}

		public LightPunctual GetSourceLightPunctual(uint index)
		{
			if (Root?.Extensions?.KHR_lights_punctual.lights != null && index < Root.Extensions.KHR_lights_punctual.lights.Length)
			{
				return Root.Extensions.KHR_lights_punctual.lights[index];
			}
			return null;
		}

		public Scene GetSourceScene(int index = 0)
		{
			if (Root?.Scenes != null && index >= 0 && index < Root.Scenes.Count)
			{
				return Root.Scenes[index];
			}
			return null;
		}

		public MaterialBase GetSourceMaterial(int index = 0)
		{
			if (Root?.Materials != null && index >= 0 && index < Root.Materials.Count)
			{
				return Root.Materials[index];
			}
			return null;
		}

		public MeshBase GetSourceMesh(int meshIndex)
		{
			if (Root?.Meshes != null && meshIndex >= 0 && meshIndex < Root.Meshes.Count)
			{
				return Root.Meshes[meshIndex];
			}
			return null;
		}

		public MeshPrimitiveBase GetSourceMeshPrimitive(int meshIndex, int primitiveIndex)
		{
			if (Root?.Meshes != null && meshIndex >= 0 && meshIndex < Root.Meshes.Count)
			{
				MeshBase meshBase = Root.Meshes[meshIndex];
				if (meshBase?.Primitives != null && primitiveIndex >= 0 && primitiveIndex < meshBase.Primitives.Count)
				{
					return meshBase.Primitives[primitiveIndex];
				}
			}
			return null;
		}

		public IMaterialsVariantsSlot[] GetMaterialsVariantsSlots(int meshIndex, int meshNumeration)
		{
			List<IMaterialsVariantsSlot> list = null;
			int[] primitives = m_MeshAssignments.GetValue(meshIndex, meshNumeration).primitives;
			foreach (int primitiveIndex in primitives)
			{
				MeshPrimitiveBase sourceMeshPrimitive = GetSourceMeshPrimitive(meshIndex, primitiveIndex);
				if (sourceMeshPrimitive.Extensions?.KHR_materials_variants?.mappings != null)
				{
					if (list == null)
					{
						list = new List<IMaterialsVariantsSlot>();
					}
					list.Add(sourceMeshPrimitive);
				}
			}
			return list?.ToArray();
		}

		public NodeBase GetSourceNode(int index = 0)
		{
			if (Root?.Nodes != null && index >= 0 && index < Root.Nodes.Count)
			{
				return Root.Nodes[index];
			}
			return null;
		}

		public TextureBase GetSourceTexture(int index = 0)
		{
			if (Root?.Textures != null && index >= 0 && index < Root.Textures.Count)
			{
				return Root.Textures[index];
			}
			return null;
		}

		public Image GetSourceImage(int index = 0)
		{
			if (Root?.Images != null && index >= 0 && index < Root.Images.Count)
			{
				return Root.Images[index];
			}
			return null;
		}

		public Matrix4x4[] GetBindPoses(int skinId)
		{
			if (m_SkinsInverseBindMatrices == null)
			{
				return null;
			}
			if (m_SkinsInverseBindMatrices[skinId] != null)
			{
				return m_SkinsInverseBindMatrices[skinId];
			}
			Matrix4x4[] array = new Matrix4x4[Root.Skins[skinId].joints.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = Matrix4x4.identity;
			}
			m_SkinsInverseBindMatrices[skinId] = array;
			return array;
		}

		[Obsolete("Use GetAccessorData instead.")]
		public NativeSlice<byte> GetAccessor(int accessorIndex)
		{
			return GetAccessorData(accessorIndex);
		}

		public NativeSlice<byte> GetAccessorData(int accessorIndex)
		{
			if (Root?.Accessors == null || accessorIndex < 0 || accessorIndex >= Root?.Accessors.Count)
			{
				return default(NativeSlice<byte>);
			}
			AccessorBase accessorBase = Root.Accessors[accessorIndex];
			int byteStride;
			return ((IGltfBuffers)this).GetBufferView(accessorBase.bufferView, out byteStride, accessorBase.byteOffset, accessorBase.ByteSize);
		}

		void IGltfBuffers.GetAccessorDataAndByteStride(int index, out NativeSlice<byte> data, out int byteStride)
		{
			if (Root?.Accessors == null || index < 0 || index >= Root?.Accessors.Count)
			{
				data = default(NativeSlice<byte>);
				byteStride = 0;
			}
			AccessorBase accessorBase = Root.Accessors[index];
			data = ((IGltfBuffers)this).GetBufferView(accessorBase.bufferView, out byteStride, accessorBase.byteOffset, accessorBase.ByteSize);
		}

		public string GetMaterialsVariantName(int index)
		{
			return Root.GetMaterialsVariantName(index);
		}

		private async Task<bool> LoadFromUri(Uri url, CancellationToken cancellationToken)
		{
			IDownload download = await m_DownloadProvider.Request(url);
			bool flag = download.Success;
			if (cancellationToken.IsCancellationRequested)
			{
				return true;
			}
			if (flag)
			{
				if ((download.IsBinary ?? UriHelper.IsGltfBinary(url)) == true)
				{
					if (m_VolatileDisposables == null)
					{
						m_VolatileDisposables = new List<IDisposable>();
					}
					NativeArray<byte>.ReadOnly bytes;
					if (download is INativeDownload nativeDownload)
					{
						bytes = nativeDownload.NativeData;
					}
					else
					{
						ManagedNativeArray<byte, byte> managedNativeArray = new ManagedNativeArray<byte, byte>(download.Data);
						m_VolatileDisposables.Add(managedNativeArray);
						bytes = managedNativeArray.nativeArray.AsReadOnly();
					}
					m_VolatileDisposables.Add(download);
					flag = await LoadGltfBinaryBuffer(bytes, url);
				}
				else
				{
					string text = download.Text;
					download.Dispose();
					flag = await LoadGltf(text, url);
				}
				if (flag)
				{
					flag = await LoadContent();
				}
				bool flag2 = flag;
				if (flag2)
				{
					flag2 = await Prepare();
				}
				flag = flag2;
			}
			else
			{
				Logger?.Error(LogCode.Download, download.Error, url.ToString());
			}
			DisposeVolatileData();
			LoadingError = !flag;
			LoadingDone = true;
			return flag;
		}

		private async Task<bool> LoadGltfBinaryInternal(NativeArray<byte>.ReadOnly bytes, Uri uri, ImportSettings importSettings, CancellationToken cancellationToken)
		{
			m_Settings = importSettings ?? new ImportSettings();
			bool success = await LoadGltfBinaryBuffer(bytes, uri);
			if (success)
			{
				await LoadContent();
			}
			bool flag = success;
			if (flag)
			{
				flag = await Prepare();
			}
			success = flag;
			DisposeVolatileData();
			LoadingError = !success;
			LoadingDone = true;
			return success;
		}

		private async Task<bool> LoadContent()
		{
			bool flag = await WaitForBufferDownloads();
			if (m_TextureDownloadTasks != null)
			{
				bool flag2 = flag;
				if (flag2)
				{
					flag2 = await WaitForTextureDownloads();
				}
				flag = flag2;
				m_TextureDownloadTasks.Clear();
			}
			return flag;
		}

		protected abstract RootBase ParseJson(string json);

		private async Task<bool> ParseJsonAndLoadBuffers(string json, Uri baseUri)
		{
			float duration = (float)json.Length / 80000000f;
			if (DeferAgent.ShouldDefer(duration))
			{
				Root = await Task.Run(() => ParseJson(json));
			}
			else
			{
				Root = ParseJson(json);
			}
			if (Root == null)
			{
				UnityEngine.Debug.LogError("JsonParsingFailed");
				Logger?.Error(LogCode.JsonParsingFailed);
				return false;
			}
			if (!CheckExtensionSupport())
			{
				return false;
			}
			if (Root.Buffers != null)
			{
				int bufferCount = Root.Buffers.Count;
				if (bufferCount > 0)
				{
					m_Buffers = new NativeArray<byte>.ReadOnly[bufferCount];
					m_BinChunks = new GlbBinChunk[bufferCount];
				}
				for (int i = 0; i < bufferCount; i++)
				{
					GLTFast.Schema.Buffer buffer = Root.Buffers[i];
					if (string.IsNullOrEmpty(buffer.uri))
					{
						continue;
					}
					if (buffer.uri.StartsWith("data:"))
					{
						Tuple<byte[], string> tuple = await DecodeEmbedBufferAsync(buffer.uri, timeCritical: true);
						if (tuple?.Item1 == null)
						{
							Logger?.Error(LogCode.EmbedBufferLoadFailed);
							return false;
						}
						ManagedNativeArray<byte, byte> managedNativeArray = new ManagedNativeArray<byte, byte>(tuple.Item1);
						if (m_VolatileDisposables == null)
						{
							m_VolatileDisposables = new List<IDisposable>();
						}
						m_VolatileDisposables.Add(managedNativeArray);
						m_Buffers[i] = managedNativeArray.nativeArray.AsReadOnly();
					}
					else
					{
						LoadBuffer(i, UriHelper.GetUriString(buffer.uri, baseUri));
					}
				}
			}
			return true;
		}

		private bool CheckExtensionSupport()
		{
			if (!CheckExtensionSupport(Root.extensionsRequired))
			{
				return false;
			}
			CheckExtensionSupport(Root.extensionsUsed, required: false);
			return true;
		}

		private bool CheckExtensionSupport(IEnumerable<string> extensions, bool required = true)
		{
			if (extensions == null)
			{
				return true;
			}
			bool result = true;
			foreach (string extension in extensions)
			{
				bool flag = k_SupportedExtensions.Contains(extension);
				if (!flag && m_ImportInstances != null)
				{
					foreach (KeyValuePair<Type, ImportAddonInstance> importInstance in m_ImportInstances)
					{
						if (importInstance.Value.SupportsGltfExtension(extension))
						{
							flag = true;
							break;
						}
					}
				}
				if (flag)
				{
					continue;
				}
				switch (extension)
				{
				case "KHR_draco_mesh_compression":
					Logger?.Log((!required) ? LogType.Warning : LogType.Error, LogCode.PackageMissing, "Draco for Unity", extension);
					break;
				case "EXT_meshopt_compression":
					Logger?.Log((!required) ? LogType.Warning : LogType.Error, LogCode.PackageMissing, "meshoptimizer decompression for Unity", extension);
					break;
				case "KHR_texture_basisu":
					Logger?.Log((!required) ? LogType.Warning : LogType.Error, LogCode.PackageMissing, "KTX for Unity", extension);
					break;
				default:
					if (required)
					{
						Logger?.Error(LogCode.ExtensionUnsupported, extension);
					}
					else
					{
						Logger?.Warning(LogCode.ExtensionUnsupported, extension);
					}
					break;
				}
				result = false;
			}
			return result;
		}

		private async Task<bool> LoadGltf(string json, Uri url)
		{
			Uri baseUri = UriHelper.GetBaseUri(url);
			bool success = await ParseJsonAndLoadBuffers(json, baseUri);
			if (success)
			{
				await LoadImages(baseUri);
			}
			return success;
		}

		private async Task LoadImages(Uri baseUri)
		{
			if (Root.Textures == null || Root.Images == null)
			{
				return;
			}
			m_Images = new Texture2D[Root.Images.Count];
			m_ImageFormats = new ImageFormat[Root.Images.Count];
			if (QualitySettings.activeColorSpace == ColorSpace.Linear)
			{
				m_ImageGamma = new bool[Root.Images.Count];
				if (Root.Materials != null)
				{
					for (int i = 0; i < Root.Materials.Count; i++)
					{
						MaterialBase materialBase = Root.Materials[i];
						if (materialBase.PbrMetallicRoughness != null)
						{
							SetImageGamma(materialBase.PbrMetallicRoughness.BaseColorTexture);
						}
						SetImageGamma(materialBase.EmissiveTexture);
						if (materialBase.Extensions?.KHR_materials_pbrSpecularGlossiness != null)
						{
							SetImageGamma(materialBase.Extensions.KHR_materials_pbrSpecularGlossiness.diffuseTexture);
							SetImageGamma(materialBase.Extensions.KHR_materials_pbrSpecularGlossiness.specularGlossinessTexture);
						}
					}
				}
			}
			HashSet<int>[] array = new HashSet<int>[m_Images.Length];
			foreach (TextureBase texture in Root.Textures)
			{
				int imageIndex = texture.GetImageIndex();
				if (imageIndex >= 0 && imageIndex < Root.Images.Count)
				{
					if (array[imageIndex] == null)
					{
						array[imageIndex] = new HashSet<int>();
					}
					array[imageIndex].Add(texture.sampler);
				}
			}
			if (!m_Settings.TexturesReadable)
			{
				m_ImageReadable = new bool[m_Images.Length];
				for (int j = 0; j < m_Images.Length; j++)
				{
					m_ImageReadable[j] = array[j] != null && array[j].Count > 1;
				}
			}
			List<Task> list = null;
			for (int k = 0; k < Root.Images.Count; k++)
			{
				Image image = Root.Images[k];
				if (!string.IsNullOrEmpty(image.uri) && image.uri.StartsWith("data:"))
				{
					Task<Tuple<byte[], string>> decodeBufferTask = DecodeEmbedBufferAsync(image.uri);
					if (list == null)
					{
						list = new List<Task>();
					}
					Task item = LoadImageFromBuffer(decodeBufferTask, k, image);
					list.Add(item);
					continue;
				}
				ImageFormat imageFormat;
				if (m_ImageFormats[k] == ImageFormat.Unknown)
				{
					imageFormat = (string.IsNullOrEmpty(image.mimeType) ? UriHelper.GetImageFormatFromUri(image.uri) : GetImageFormatFromMimeType(image.mimeType));
					m_ImageFormats[k] = imageFormat;
				}
				else
				{
					imageFormat = m_ImageFormats[k];
				}
				if (imageFormat != ImageFormat.Unknown)
				{
					if (image.bufferView < 0)
					{
						if (!string.IsNullOrEmpty(image.uri))
						{
							LoadImage(k, UriHelper.GetUriString(image.uri, baseUri), !m_Settings.TexturesReadable && !m_ImageReadable[k], imageFormat == ImageFormat.Ktx);
						}
						else
						{
							Logger?.Error(LogCode.MissingImageURL);
						}
					}
				}
				else
				{
					Logger?.Error(LogCode.ImageFormatUnknown, k.ToString(), image.uri);
				}
			}
			if (list != null)
			{
				await Task.WhenAll(list);
			}
			void SetImageGamma(TextureInfoBase txtInfo)
			{
				if (txtInfo != null && txtInfo.index >= 0 && txtInfo.index < Root.Textures.Count)
				{
					int imageIndex2 = Root.Textures[txtInfo.index].GetImageIndex();
					m_ImageGamma[imageIndex2] = true;
				}
			}
		}

		private async Task LoadImageFromBuffer(Task<Tuple<byte[], string>> decodeBufferTask, int imageIndex, Image img)
		{
			Tuple<byte[], string> decodedBuffer = await decodeBufferTask;
			await DeferAgent.BreakPoint();
			byte[] item = decodedBuffer.Item1;
			ImageFormat imageFormatFromMimeType = GetImageFormatFromMimeType(decodedBuffer.Item2);
			if (item == null || imageFormatFromMimeType == ImageFormat.Unknown)
			{
				Logger?.Error(LogCode.EmbedImageLoadFailed);
				return;
			}
			if (m_ImageFormats[imageIndex] != ImageFormat.Unknown && m_ImageFormats[imageIndex] != imageFormatFromMimeType)
			{
				Logger?.Error(LogCode.EmbedImageInconsistentType, m_ImageFormats[imageIndex].ToString(), imageFormatFromMimeType.ToString());
			}
			m_ImageFormats[imageIndex] = imageFormatFromMimeType;
			if (m_ImageFormats[imageIndex] != ImageFormat.Jpeg && m_ImageFormats[imageIndex] != ImageFormat.PNG)
			{
				Logger?.Error(LogCode.EmbedImageUnsupportedType, m_ImageFormats[imageIndex].ToString());
			}
			bool forceSampleLinear = m_ImageGamma != null && !m_ImageGamma[imageIndex];
			Texture2D texture2D = CreateEmptyTexture(img, imageIndex, forceSampleLinear);
			texture2D.LoadImage(item, !m_Settings.TexturesReadable && !m_ImageReadable[imageIndex]);
			m_Images[imageIndex] = texture2D;
		}

		private async Task<bool> WaitForBufferDownloads()
		{
			if (m_DownloadTasks != null)
			{
				foreach (KeyValuePair<int, Task<IDownload>> downloadPair in m_DownloadTasks)
				{
					IDownload download = await downloadPair.Value;
					if (download.Success)
					{
						if (m_VolatileDisposables == null)
						{
							m_VolatileDisposables = new List<IDisposable>();
						}
						NativeArray<byte>.ReadOnly readOnly;
						if (download is INativeDownload nativeDownload)
						{
							readOnly = nativeDownload.NativeData;
						}
						else
						{
							ManagedNativeArray<byte, byte> managedNativeArray = new ManagedNativeArray<byte, byte>(download.Data);
							m_VolatileDisposables.Add(managedNativeArray);
							readOnly = managedNativeArray.nativeArray.AsReadOnly();
						}
						m_VolatileDisposables.Add(download);
						m_Buffers[downloadPair.Key] = readOnly;
						continue;
					}
					Logger?.Error(LogCode.BufferLoadFailed, download.Error, downloadPair.Key.ToString());
					return false;
				}
			}
			if (m_Buffers != null)
			{
				for (int i = 0; i < m_Buffers.Length; i++)
				{
					if (i != 0 || !m_GlbBinChunk.HasValue)
					{
						NativeArray<byte>.ReadOnly readOnly2 = m_Buffers[i];
						if (readOnly2.IsCreated)
						{
							m_BinChunks[i] = new GlbBinChunk(0, (uint)readOnly2.Length);
						}
					}
				}
			}
			return true;
		}

		private async Task<bool> WaitForTextureDownloads()
		{
			foreach (KeyValuePair<int, TextureDownloadBase> dl in m_TextureDownloadTasks)
			{
				await dl.Value.Load();
				IDownload download = dl.Value.Download;
				if (download == null)
				{
					Logger?.Error(LogCode.TextureDownloadFailed, "?", dl.Key.ToString());
					return false;
				}
				if (download.Success)
				{
					int key = dl.Key;
					Texture2D texture2D;
					if (LoadImageFromBytes(key))
					{
						bool forceSampleLinear = m_ImageGamma != null && !m_ImageGamma[key];
						texture2D = CreateEmptyTexture(Root.Images[key], key, forceSampleLinear);
						texture2D.LoadImage(download.Data, !m_Settings.TexturesReadable && !m_ImageReadable[key]);
					}
					else
					{
						texture2D = ((ITextureDownload)download).Texture;
						texture2D.name = GetImageName(Root.Images[key], key);
					}
					download.Dispose();
					m_Images[key] = texture2D;
					await DeferAgent.BreakPoint();
					continue;
				}
				Logger?.Error(LogCode.TextureDownloadFailed, download.Error, dl.Key.ToString());
				download.Dispose();
				return false;
			}
			return true;
		}

		private void LoadBuffer(int index, Uri url)
		{
			if (m_DownloadTasks == null)
			{
				m_DownloadTasks = new Dictionary<int, Task<IDownload>>();
			}
			m_DownloadTasks.Add(index, m_DownloadProvider.Request(url));
		}

		private async Task<Tuple<byte[], string>> DecodeEmbedBufferAsync(string encodedBytes, bool timeCritical = false)
		{
			float duration = (float)encodedBytes.Length / 150000000f;
			if (!timeCritical || DeferAgent.ShouldDefer(duration))
			{
				return await Task.Run(() => DecodeEmbedBuffer(encodedBytes, Logger));
			}
			await DeferAgent.BreakPoint(duration);
			return DecodeEmbedBuffer(encodedBytes, Logger);
		}

		private static Tuple<byte[], string> DecodeEmbedBuffer(string encodedBytes, ICodeLogger logger)
		{
			logger?.Warning(LogCode.EmbedSlow);
			int num = encodedBytes.IndexOf(';', 5, Math.Min(encodedBytes.Length - 5, 1000));
			if (num < 0)
			{
				return null;
			}
			string item = encodedBytes.Substring(5, num - 5);
			if (encodedBytes.Substring(num + 1, 7) != "base64,")
			{
				return null;
			}
			return new Tuple<byte[], string>(Convert.FromBase64String(encodedBytes.Substring(num + 8)), item);
		}

		private void LoadImage(int imageIndex, Uri url, bool nonReadable, bool isKtx)
		{
			if (isKtx)
			{
				Logger?.Error(LogCode.PackageMissing, "KTX for Unity", "KHR_texture_basisu");
				return;
			}
			TextureDownloadBase value = (LoadImageFromBytes(imageIndex) ? ((TextureDownloadBase)new TextureDownload<IDownload>(m_DownloadProvider.Request(url))) : ((TextureDownloadBase)new TextureDownload<ITextureDownload>(m_DownloadProvider.RequestTexture(url, nonReadable))));
			if (m_TextureDownloadTasks == null)
			{
				m_TextureDownloadTasks = new Dictionary<int, TextureDownloadBase>();
			}
			m_TextureDownloadTasks.Add(imageIndex, value);
		}

		private bool LoadImageFromBytes(int imageIndex)
		{
			if (m_ImageGamma == null || m_ImageGamma[imageIndex])
			{
				return m_Settings.GenerateMipMaps;
			}
			return true;
		}

		private async Task<bool> LoadGltfBinaryBuffer(NativeArray<byte>.ReadOnly bytes, Uri uri = null)
		{
			if (!GltfGlobals.IsGltfBinary(bytes))
			{
				Logger?.Error(LogCode.GltfNotBinary);
				return false;
			}
			uint num = bytes.ReadUInt32(4);
			if (num != 2)
			{
				Logger?.Error(LogCode.GltfUnsupportedVersion, num.ToString());
				return false;
			}
			int index = 12;
			Uri baseUri = UriHelper.GetBaseUri(uri);
			while (index < bytes.Length)
			{
				if (index + 8 > bytes.Length)
				{
					Logger?.Error(LogCode.ChunkIncomplete);
					return false;
				}
				uint chLength = bytes.ReadUInt32(index);
				index += 4;
				uint num2 = bytes.ReadUInt32(index);
				index += 4;
				if (index + chLength > bytes.Length)
				{
					Logger?.Error(LogCode.ChunkIncomplete);
					return false;
				}
				switch (num2)
				{
				case 5130562u:
					m_GlbBinChunk = new GlbBinChunk(index, chLength);
					break;
				case 1313821514u:
					if (!(await ParseJsonAndLoadBuffers(await new StreamReader(bytes.ToUnmanagedMemoryStream((uint)index, chLength)).ReadToEndAsync(), baseUri)))
					{
						return false;
					}
					break;
				default:
					Logger?.Error(LogCode.ChunkUnknown, num2.ToString());
					return false;
				}
				index += (int)chLength;
			}
			if (Root == null)
			{
				Logger?.Error(LogCode.ChunkJsonInvalid);
				return false;
			}
			if (m_GlbBinChunk.HasValue && m_BinChunks != null)
			{
				m_BinChunks[0] = m_GlbBinChunk.Value;
				m_Buffers[0] = bytes;
			}
			await LoadImages(baseUri);
			return true;
		}

		private NativeArray<byte>.ReadOnly GetBuffer(int index)
		{
			return m_Buffers[index];
		}

		NativeSlice<byte> IGltfBuffers.GetBufferView(int bufferViewIndex, out int byteStride, int offset, int length)
		{
			BufferViewBase bufferViewBase = Root.BufferViews[bufferViewIndex];
			byteStride = bufferViewBase.byteStride;
			return GetBufferViewSlice(bufferViewBase, offset, length);
		}

		private NativeSlice<byte> GetBufferViewSlice(IBufferView bufferView, int offset = 0, int length = 0)
		{
			if (length <= 0)
			{
				length = bufferView.ByteLength - offset;
			}
			int buffer = bufferView.Buffer;
			GlbBinChunk glbBinChunk = m_BinChunks[buffer];
			_ = ref m_Buffers[buffer];
			int start = glbBinChunk.Start + bufferView.ByteOffset + offset;
			return m_Buffers[buffer].Slice(start, length);
		}

		private async Task<bool> Prepare()
		{
			m_Resources = new List<UnityEngine.Object>();
			if (Root.Images != null && Root.Textures != null && Root.Materials != null)
			{
				if (m_Images == null)
				{
					m_Images = new Texture2D[Root.Images.Count];
				}
				m_ImageCreateContexts = new List<ImageCreateContext>();
				CreateTexturesFromBuffers(Root.Images, Root.BufferViews, m_ImageCreateContexts);
			}
			await DeferAgent.BreakPoint();
			bool success = true;
			if (Root.Accessors != null)
			{
				success = await LoadAccessorData();
				await DeferAgent.BreakPoint();
				while (!m_AccessorJobsHandle.IsCompleted)
				{
					await Task.Yield();
				}
				m_AccessorJobsHandle.Complete();
			}
			if (!success)
			{
				return success;
			}
			if (m_ImageCreateContexts != null)
			{
				await WaitForImageCreateContexts();
			}
			if (m_Images != null && Root.Textures != null)
			{
				PopulateTexturesAndImageVariants();
			}
			if (Root.Materials != null)
			{
				await GenerateMaterials();
			}
			await DeferAgent.BreakPoint();
			if (m_MeshOrders != null)
			{
				await WaitForAllMeshGenerators();
				await DeferAgent.BreakPoint();
				await AssignAllAccessorData();
				success = await CreateAllMeshAssignments();
			}
			if (Root.HasAnimation && m_Settings.NodeNameMethod != NameImportMethod.OriginalUnique)
			{
				Logger?.Info(LogCode.NamingOverride);
				m_Settings.NodeNameMethod = NameImportMethod.OriginalUnique;
			}
			int[] parentIndex = null;
			bool flag = Root.IsASkeletonMissing();
			if (Root.Nodes != null && Root.Nodes.Count > 0)
			{
				if (m_Settings.NodeNameMethod == NameImportMethod.OriginalUnique)
				{
					parentIndex = CreateUniqueNames();
				}
				else if (flag)
				{
					parentIndex = GetParentIndices();
				}
				if (flag)
				{
					CalculateSkinSkeletons(parentIndex);
				}
			}
			if (Root.HasAnimation && m_Settings.AnimationMethod != AnimationMethod.None)
			{
				CreateAnimationClips(parentIndex);
			}
			DisposeVolatileAccessorData();
			return success;
		}

		private void CreateAnimationClips(int[] parentIndex)
		{
			m_AnimationClips = new AnimationClip[Root.Animations.Count];
			for (int i = 0; i < Root.Animations.Count; i++)
			{
				AnimationBase animationBase = Root.Animations[i];
				m_AnimationClips[i] = new AnimationClip
				{
					name = (animationBase.name ?? $"Clip_{i}"),
					legacy = (m_Settings.AnimationMethod == AnimationMethod.Legacy),
					wrapMode = WrapMode.Loop
				};
				for (int j = 0; j < animationBase.Channels.Count; j++)
				{
					AnimationChannelBase animationChannelBase = animationBase.Channels[j];
					if (animationChannelBase.sampler < 0 || animationChannelBase.sampler >= animationBase.Samplers.Count)
					{
						Logger?.Error(LogCode.AnimationChannelSamplerInvalid, j.ToString());
						continue;
					}
					AnimationSampler animationSampler = animationBase.Samplers[animationChannelBase.sampler];
					if (animationSampler == null || animationSampler.output < 0 || animationSampler.output >= Root.Accessors.Count)
					{
						Logger?.Error(LogCode.AnimationChannelSamplerInvalid, j.ToString());
						continue;
					}
					if (animationChannelBase.Target.node < 0 || animationChannelBase.Target.node >= Root.Nodes.Count)
					{
						Logger?.Error(LogCode.AnimationChannelNodeInvalid, j.ToString());
						continue;
					}
					string text = AnimationUtils.CreateAnimationPath(animationChannelBase.Target.node, m_NodeNames, parentIndex);
					NativeArray<float> times = (NativeArray<float>)(object)m_AccessorData[animationSampler.input];
					IDisposable input = m_AccessorData[animationSampler.output];
					InterpolationType interpolationType = animationSampler.GetInterpolationType();
					switch (animationChannelBase.Target.GetPath())
					{
					case AnimationChannelBase.Path.Translation:
					{
						NativeArray<Vector3> translations2 = CastOrCreateTypedBuffer<Vector3>(input, times.Length, interpolationType);
						AnimationUtils.AddTranslationCurves(m_AnimationClips[i], text, times, translations2, interpolationType);
						break;
					}
					case AnimationChannelBase.Path.Rotation:
					{
						NativeArray<Quaternion> quaternions = CastOrCreateTypedBuffer<Quaternion>(input, times.Length, interpolationType);
						AnimationUtils.AddRotationCurves(m_AnimationClips[i], text, times, quaternions, interpolationType);
						break;
					}
					case AnimationChannelBase.Path.Scale:
					{
						NativeArray<Vector3> translations = CastOrCreateTypedBuffer<Vector3>(input, times.Length, interpolationType);
						AnimationUtils.AddScaleCurves(m_AnimationClips[i], text, times, translations, interpolationType);
						break;
					}
					case AnimationChannelBase.Path.Weights:
					{
						NativeArray<float> values = CastOrCreateTypedBuffer<float>(input, times.Length, interpolationType);
						NodeBase nodeBase = Root.Nodes[animationChannelBase.Target.node];
						if (nodeBase.mesh >= 0 && nodeBase.mesh < Root.Meshes.Count)
						{
							MeshBase meshBase = Root.Meshes[nodeBase.mesh];
							AnimationUtils.AddMorphTargetWeightCurves(m_AnimationClips[i], text, times, values, interpolationType, meshBase.Extras?.targetNames);
							string arg = (string.IsNullOrEmpty(meshBase.name) ? "Primitive" : meshBase.name);
							int length = m_MeshAssignments.GetLength(nodeBase.mesh);
							for (int k = 1; k < length; k++)
							{
								string text2 = $"{arg}_{k}";
								AnimationUtils.AddMorphTargetWeightCurves(m_AnimationClips[i], text + "/" + text2, times, values, interpolationType, meshBase.Extras?.targetNames);
							}
						}
						break;
					}
					case AnimationChannelBase.Path.Pointer:
						Logger?.Warning(LogCode.AnimationTargetPathUnsupported, animationChannelBase.Target.GetPath().ToString());
						break;
					default:
						Logger?.Error(LogCode.AnimationTargetPathUnsupported, animationChannelBase.Target.GetPath().ToString());
						break;
					}
				}
			}
		}

		private static NativeArray<T> CastOrCreateTypedBuffer<T>(IDisposable input, int expectedLength, InterpolationType interpolationType) where T : struct
		{
			if (input == null)
			{
				return new NativeArray<T>(expectedLength * ((interpolationType != InterpolationType.CubicSpline) ? 1 : 3), Allocator.Temp);
			}
			return (NativeArray<T>)(object)input;
		}

		private void CalculateSkinSkeletons(int[] parentIndex)
		{
			foreach (Skin skin in Root.Skins)
			{
				if (skin.skeleton < 0)
				{
					skin.skeleton = GetLowestCommonAncestorNode(skin.joints, parentIndex);
				}
			}
		}

		private void DisposeVolatileAccessorData()
		{
			if (m_AccessorData == null)
			{
				return;
			}
			for (int i = 0; i < m_AccessorData.Length; i++)
			{
				if ((m_AccessorUsage[i] & AccessorUsage.RequiredForInstantiation) == 0)
				{
					m_AccessorData[i]?.Dispose();
					m_AccessorData[i] = null;
				}
			}
		}

		private async Task<bool> CreateAllMeshAssignments()
		{
			foreach (MeshOrder meshOrder in m_MeshOrders)
			{
				UnityEngine.Mesh mesh = await meshOrder.generator.CreateMeshResult();
				if ((object)mesh != null)
				{
					foreach (MeshSubset recipient in meshOrder.Recipients)
					{
						MeshAssignment value = new MeshAssignment(mesh, recipient.primitives);
						m_MeshAssignments.SetValue(recipient.meshIndex, recipient.meshNumeration, value);
					}
					m_Meshes.Add(mesh);
					meshOrder.Dispose();
					await DeferAgent.BreakPoint();
					continue;
				}
				return false;
			}
			m_MeshOrders = null;
			return true;
		}

		private async Task WaitForAllMeshGenerators()
		{
			foreach (MeshOrder meshOrder in m_MeshOrders)
			{
				if (meshOrder.generator != null)
				{
					while (!meshOrder.generator.IsCompleted)
					{
						await Task.Yield();
					}
				}
			}
		}

		private async Task GenerateMaterials()
		{
			m_Materials = new UnityEngine.Material[Root.Materials.Count];
			for (int i = 0; i < m_Materials.Length; i++)
			{
				await DeferAgent.BreakPoint(0.0001f);
				m_MaterialGenerator.SetLogger(Logger);
				bool materialPointsSupport = GetMaterialPointsSupport(i);
				UnityEngine.Material material = m_MaterialGenerator.GenerateMaterial(Root.Materials[i], this, materialPointsSupport);
				m_Materials[i] = material;
				m_MaterialGenerator.SetLogger(null);
			}
		}

		private void PopulateTexturesAndImageVariants()
		{
			SamplerKey samplerKey = new SamplerKey(new Sampler());
			m_Textures = new Texture2D[Root.Textures.Count];
			Dictionary<SamplerKey, Texture2D>[] array = new Dictionary<SamplerKey, Texture2D>[m_Images.Length];
			for (int i = 0; i < Root.Textures.Count; i++)
			{
				TextureBase textureBase = Root.Textures[i];
				Sampler sampler = null;
				SamplerKey samplerKey2;
				if (textureBase.sampler >= 0)
				{
					sampler = Root.Samplers[textureBase.sampler];
					samplerKey2 = new SamplerKey(sampler);
				}
				else
				{
					samplerKey2 = samplerKey;
				}
				int imageIndex = textureBase.GetImageIndex();
				if (imageIndex >= 0 && imageIndex < Root.Images.Count)
				{
					Texture2D texture2D = m_Images[imageIndex];
					Texture2D value;
					if (array[imageIndex] == null)
					{
						sampler?.Apply(texture2D, m_Settings.DefaultMinFilterMode, m_Settings.DefaultMagFilterMode);
						array[imageIndex] = new Dictionary<SamplerKey, Texture2D> { [samplerKey2] = texture2D };
						m_Textures[i] = texture2D;
					}
					else if (array[imageIndex].TryGetValue(samplerKey2, out value))
					{
						m_Textures[i] = value;
					}
					else
					{
						Texture2D texture2D2 = UnityEngine.Object.Instantiate(texture2D);
						m_Resources.Add(texture2D2);
						sampler?.Apply(texture2D2, m_Settings.DefaultMinFilterMode, m_Settings.DefaultMagFilterMode);
						array[imageIndex][samplerKey2] = texture2D2;
						m_Textures[i] = texture2D2;
					}
				}
			}
		}

		private async Task WaitForImageCreateContexts()
		{
			bool imageCreateContextsLeft = true;
			while (imageCreateContextsLeft)
			{
				bool loadedAny = false;
				for (int i = m_ImageCreateContexts.Count - 1; i >= 0; i--)
				{
					ImageCreateContext imageCreateContext = m_ImageCreateContexts[i];
					if (imageCreateContext.jobHandle.IsCompleted)
					{
						imageCreateContext.jobHandle.Complete();
						m_Images[imageCreateContext.imageIndex].LoadImage(imageCreateContext.buffer, !m_Settings.TexturesReadable && !m_ImageReadable[imageCreateContext.imageIndex]);
						imageCreateContext.gcHandle.Free();
						m_ImageCreateContexts.RemoveAt(i);
						loadedAny = true;
						await DeferAgent.BreakPoint();
					}
				}
				imageCreateContextsLeft = m_ImageCreateContexts.Count > 0;
				if (!loadedAny && imageCreateContextsLeft)
				{
					await Task.Yield();
				}
			}
			m_ImageCreateContexts = null;
		}

		private void SetMaterialPointsSupport(int materialIndex)
		{
			if (m_MaterialPointsSupport == null)
			{
				m_MaterialPointsSupport = new HashSet<int>();
			}
			m_MaterialPointsSupport.Add(materialIndex);
		}

		private bool GetMaterialPointsSupport(int materialIndex)
		{
			if (m_MaterialPointsSupport != null)
			{
				return m_MaterialPointsSupport.Contains(materialIndex);
			}
			return false;
		}

		private int[] CreateUniqueNames()
		{
			m_NodeNames = new string[Root.Nodes.Count];
			int[] array = new int[Root.Nodes.Count];
			for (int i = 0; i < Root.Nodes.Count; i++)
			{
				array[i] = -1;
			}
			HashSet<string> hashSet = new HashSet<string>();
			for (int j = 0; j < Root.Nodes.Count; j++)
			{
				NodeBase nodeBase = Root.Nodes[j];
				if (nodeBase.children != null)
				{
					hashSet.Clear();
					uint[] children = nodeBase.children;
					foreach (uint num in children)
					{
						array[num] = j;
						m_NodeNames[num] = GetUniqueNodeName(Root, num, hashSet);
					}
				}
			}
			for (int l = 0; l < Root.Scenes.Count; l++)
			{
				hashSet.Clear();
				Scene scene = Root.Scenes[l];
				if (scene.nodes != null)
				{
					uint[] children = scene.nodes;
					foreach (uint num2 in children)
					{
						m_NodeNames[num2] = GetUniqueNodeName(Root, num2, hashSet);
					}
				}
			}
			return array;
		}

		private static string GetUniqueNodeName(RootBase gltf, uint index, ICollection<string> excludeNames)
		{
			if (gltf.Nodes == null || index >= gltf.Nodes.Count)
			{
				return null;
			}
			string text = gltf.Nodes[(int)index].name;
			if (string.IsNullOrWhiteSpace(text))
			{
				int mesh = gltf.Nodes[(int)index].mesh;
				if (mesh >= 0)
				{
					text = gltf.Meshes[mesh].name;
				}
			}
			if (string.IsNullOrWhiteSpace(text))
			{
				text = $"Node-{index}";
			}
			if (excludeNames != null)
			{
				if (excludeNames.Contains(text))
				{
					int num = 0;
					string text2;
					do
					{
						text2 = $"{text}_{num++}";
					}
					while (excludeNames.Contains(text2));
					excludeNames.Add(text2);
					return text2;
				}
				excludeNames.Add(text);
			}
			return text;
		}

		private void DisposeVolatileData()
		{
			m_Buffers = null;
			m_BinChunks = null;
			if (m_VolatileDisposables != null)
			{
				foreach (IDisposable volatileDisposable in m_VolatileDisposables)
				{
					volatileDisposable.Dispose();
				}
				m_VolatileDisposables = null;
			}
			if (m_DownloadTasks != null)
			{
				foreach (Task<IDownload> value in m_DownloadTasks.Values)
				{
					value?.Dispose();
				}
				m_DownloadTasks = null;
			}
			m_TextureDownloadTasks = null;
			m_AccessorUsage = null;
			m_ImageCreateContexts = null;
			m_Images = null;
			m_ImageFormats = null;
			m_ImageReadable = null;
			m_ImageGamma = null;
			m_GlbBinChunk = null;
			m_MaterialPointsSupport = null;
		}

		private async Task InstantiateSceneInternal(IInstantiator instantiator, int sceneId)
		{
			if (m_ImportInstances != null)
			{
				foreach (KeyValuePair<Type, ImportAddonInstance> importInstance in m_ImportInstances)
				{
					importInstance.Value.Inject(instantiator);
				}
			}
			Scene scene = Root.Scenes[sceneId];
			instantiator.BeginScene(scene.name, scene.nodes);
			if (instantiator is GameObjectInstantiator gameObjectInstantiator)
			{
				gameObjectInstantiator.ImportSettings = m_Settings;
			}
			instantiator.AddAnimation(m_AnimationClips);
			if (scene.nodes != null)
			{
				uint[] nodes = scene.nodes;
				foreach (uint nodeIndex in nodes)
				{
					await IterateNodes(nodeIndex, null, CreateHierarchy);
				}
				nodes = scene.nodes;
				foreach (uint nodeIndex2 in nodes)
				{
					await IterateNodes(nodeIndex2, null, PopulateHierarchy);
				}
			}
			instantiator.EndScene(scene.nodes);
			void CreateHierarchy(uint num, uint? parentIndex)
			{
				NodeBase nodeBase = Root.Nodes[(int)num];
				nodeBase.GetTransform(out var position, out var rotation, out var scale);
				instantiator.CreateNode(num, parentIndex, position, rotation, scale);
				string text = ((m_NodeNames == null) ? nodeBase.name : m_NodeNames[num]);
				if (text == null && nodeBase.mesh >= 0)
				{
					foreach (MeshAssignment item in m_MeshAssignments.Values(nodeBase.mesh))
					{
						UnityEngine.Mesh mesh = item.mesh;
						if (!string.IsNullOrEmpty(mesh.name))
						{
							text = mesh.name;
							break;
						}
					}
				}
				instantiator.SetNodeName(num, text);
			}
			async Task IterateNodes(uint num, uint? parentIndex, Action<uint, uint?> callback)
			{
				NodeBase node = Root.Nodes[(int)num];
				callback(num, parentIndex);
				await DeferAgent.BreakPoint();
				if (node.children != null)
				{
					uint[] children = node.children;
					foreach (uint nodeIndex3 in children)
					{
						await IterateNodes(nodeIndex3, num, callback);
					}
				}
			}
			void PopulateHierarchy(uint num, uint? parentIndex)
			{
				NodeBase nodeBase = Root.Nodes[(int)num];
				if (nodeBase.mesh >= 0)
				{
					int num2 = 0;
					foreach (MeshAssignment item2 in m_MeshAssignments.Values(nodeBase.mesh))
					{
						UnityEngine.Mesh mesh = item2.mesh;
						string text = (string.IsNullOrEmpty(mesh.name) ? null : mesh.name);
						uint[] joints = null;
						uint? rootJoint = null;
						if (mesh.HasVertexAttribute(VertexAttribute.BlendIndices))
						{
							if (nodeBase.skin >= 0)
							{
								Skin skin = Root.Skins[nodeBase.skin];
								mesh.bindposes = GetBindPoses(nodeBase.skin);
								if (skin.skeleton >= 0)
								{
									rootJoint = (uint)skin.skeleton;
								}
								joints = skin.joints;
							}
							else
							{
								Logger?.Warning(LogCode.SkinMissing);
							}
						}
						MeshGpuInstancing meshGpuInstancing = nodeBase.Extensions?.EXT_mesh_gpu_instancing;
						string meshName = ((num2 > 0) ? string.Format("{0}_{1}", text ?? "Primitive", num2) : (text ?? "Primitive"));
						MeshResult meshResult = new MeshResult(nodeBase.mesh, item2.primitives, GetMaterialIndices(nodeBase.mesh, item2.primitives), item2.mesh);
						if (meshGpuInstancing == null)
						{
							instantiator.AddPrimitive(num, meshName, meshResult, joints, rootJoint, Root.Meshes[nodeBase.mesh].weights, num2);
						}
						else
						{
							bool num3 = meshGpuInstancing.attributes.TRANSLATION > -1;
							bool flag = meshGpuInstancing.attributes.ROTATION > -1;
							bool flag2 = meshGpuInstancing.attributes.SCALE > -1;
							NativeArray<Vector3>? positions = null;
							NativeArray<Quaternion>? rotations = null;
							NativeArray<Vector3>? scales = null;
							uint instanceCount = 0u;
							if (num3)
							{
								positions = (NativeArray<Vector3>)(object)m_AccessorData[meshGpuInstancing.attributes.TRANSLATION];
								instanceCount = (uint)positions.Value.Length;
							}
							if (flag)
							{
								rotations = (NativeArray<Quaternion>)(object)m_AccessorData[meshGpuInstancing.attributes.ROTATION];
								instanceCount = (uint)rotations.Value.Length;
							}
							if (flag2)
							{
								scales = (NativeArray<Vector3>)(object)m_AccessorData[meshGpuInstancing.attributes.SCALE];
								instanceCount = (uint)scales.Value.Length;
							}
							instantiator.AddPrimitiveInstanced(num, meshName, meshResult, instanceCount, positions, rotations, scales, num2);
						}
						num2++;
					}
				}
				if (nodeBase.camera >= 0 && Root.Cameras != null && nodeBase.camera < Root.Cameras.Count)
				{
					instantiator.AddCamera(num, (uint)nodeBase.camera);
				}
				if (nodeBase.Extensions?.KHR_lights_punctual != null && Root.Extensions?.KHR_lights_punctual?.lights != null)
				{
					int light = nodeBase.Extensions.KHR_lights_punctual.light;
					if (light < Root.Extensions.KHR_lights_punctual.lights.Length)
					{
						instantiator.AddLightPunctual(num, (uint)light);
					}
				}
			}
		}

		private static int GetLowestCommonAncestorNode(IEnumerable<uint> nodes, IReadOnlyList<int> parentIndex)
		{
			List<int> chain = null;
			int commonAncestor = -1;
			foreach (uint node in nodes)
			{
				if (!CompareTo((int)node))
				{
					return -1;
				}
			}
			return commonAncestor;
			bool CompareTo(int nodeId)
			{
				List<int> list = new List<int>();
				for (int num = nodeId; num >= 0; num = parentIndex[num])
				{
					if (num == commonAncestor)
					{
						return true;
					}
					list.Insert(0, num);
				}
				if (chain == null)
				{
					chain = list;
				}
				else
				{
					int num2 = math.min(chain.Count, list.Count);
					for (int i = 0; i < num2; i++)
					{
						if (chain[i] != list[i])
						{
							if (i > 0)
							{
								chain.RemoveRange(i, chain.Count - i);
								break;
							}
							return false;
						}
					}
				}
				commonAncestor = chain[chain.Count - 1];
				return true;
			}
		}

		private int[] GetParentIndices()
		{
			int[] array = new int[Root.Nodes.Count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = -1;
			}
			for (int j = 0; j < Root.Nodes.Count; j++)
			{
				if (Root.Nodes[j].children != null)
				{
					uint[] children = Root.Nodes[j].children;
					foreach (uint num in children)
					{
						array[num] = j;
					}
				}
			}
			return array;
		}

		private int[] GetMaterialIndices(int meshIndex, IReadOnlyList<int> primitiveIndices)
		{
			int[] array = new int[primitiveIndices.Count];
			for (int i = 0; i < primitiveIndices.Count; i++)
			{
				int primitiveIndex = primitiveIndices[i];
				MeshPrimitiveBase sourceMeshPrimitive = GetSourceMeshPrimitive(meshIndex, primitiveIndex);
				array[i] = sourceMeshPrimitive.material;
			}
			return array;
		}

		private unsafe static NativeArray<T> Reinterpret<T>(NativeSlice<byte> slice, int count, int offset = 0) where T : struct
		{
			byte* unsafeReadOnlyPtr = (byte*)slice.GetUnsafeReadOnlyPtr();
			return NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<T>(unsafeReadOnlyPtr + offset, count, Allocator.None);
		}

		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private static void ReleaseReinterpret<T>(NativeArray<T> array) where T : struct
		{
		}

		private void CreateTexturesFromBuffers(IReadOnlyList<Image> srcImages, IReadOnlyList<BufferViewBase> bufferViews, ICollection<ImageCreateContext> contexts)
		{
			for (int i = 0; i < m_Images.Length; i++)
			{
				if (m_Images[i] != null)
				{
					m_Resources.Add(m_Images[i]);
				}
				Image image = srcImages[i];
				ImageFormat imageFormat = m_ImageFormats[i];
				if (imageFormat == ImageFormat.Unknown)
				{
					imageFormat = (string.IsNullOrEmpty(image.mimeType) ? UriHelper.GetImageFormatFromUri(image.uri) : GetImageFormatFromMimeType(image.mimeType));
				}
				if (imageFormat != ImageFormat.Unknown && image.bufferView >= 0)
				{
					if (imageFormat == ImageFormat.Ktx)
					{
						Logger?.Error(LogCode.PackageMissing, "KTX for Unity", "KHR_texture_basisu");
						continue;
					}
					BufferViewBase bufferViewBase = bufferViews[image.bufferView];
					NativeArray<byte>.ReadOnly buffer = GetBuffer(bufferViewBase.buffer);
					GlbBinChunk chunk = m_BinChunks[bufferViewBase.buffer];
					bool forceSampleLinear = m_ImageGamma != null && !m_ImageGamma[i];
					Texture2D texture2D = CreateEmptyTexture(image, i, forceSampleLinear);
					ImageCreateContext imageCreateContext = default(ImageCreateContext);
					imageCreateContext.imageIndex = i;
					imageCreateContext.buffer = new byte[bufferViewBase.byteLength];
					imageCreateContext.gcHandle = GCHandle.Alloc(imageCreateContext.buffer, GCHandleType.Pinned);
					MemCopyJob jobData = CreateMemCopyJob(bufferViewBase, buffer, chunk, imageCreateContext);
					imageCreateContext.jobHandle = jobData.Schedule();
					contexts.Add(imageCreateContext);
					m_Images[i] = texture2D;
					m_Resources.Add(texture2D);
				}
			}
		}

		private unsafe static MemCopyJob CreateMemCopyJob(BufferViewBase bufferView, NativeArray<byte>.ReadOnly buffer, GlbBinChunk chunk, ImageCreateContext icc)
		{
			MemCopyJob result = new MemCopyJob
			{
				bufferSize = bufferView.byteLength,
				input = (byte*)buffer.GetUnsafeReadOnlyPtr() + (bufferView.byteOffset + chunk.Start)
			};
			fixed (byte* ptr = &icc.buffer[0])
			{
				void* result2 = ptr;
				result.result = result2;
			}
			return result;
		}

		private Texture2D CreateEmptyTexture(Image img, int index, bool forceSampleLinear)
		{
			TextureCreationFlags textureCreationFlags = TextureCreationFlags.DontInitializePixels | TextureCreationFlags.DontUploadUponCreate;
			if (m_Settings.GenerateMipMaps)
			{
				textureCreationFlags |= TextureCreationFlags.MipChain;
			}
			return new Texture2D(4, 4, forceSampleLinear ? GraphicsFormat.R8G8B8A8_UNorm : GraphicsFormat.R8G8B8A8_SRGB, textureCreationFlags)
			{
				anisoLevel = m_Settings.AnisotropicFilterLevel,
				name = GetImageName(img, index)
			};
		}

		private static string GetImageName(Image img, int index)
		{
			if (!string.IsNullOrEmpty(img.name))
			{
				return img.name;
			}
			return $"image_{index}";
		}

		private static void SafeDestroy(UnityEngine.Object obj)
		{
			UnityEngine.Object.Destroy(obj);
		}

		private async Task<bool> LoadAccessorData()
		{
			m_AccessorUsage = new AccessorUsage[Root.Accessors.Count];
			this.LoadAccessorDataEvent?.Invoke();
			int num = Root.Meshes?.Count ?? 0;
			int[] array = null;
			if (num > 0)
			{
				m_MeshOrders = new List<MeshOrder>();
				array = new int[num + 1];
				array[0] = 0;
			}
			int num2 = 0;
			new Dictionary<MeshPrimitiveBase, MeshOrder>(s_MeshComparer);
			Dictionary<IReadOnlyList<MeshPrimitiveBase>, MeshOrder> dictionary = new Dictionary<IReadOnlyList<MeshPrimitiveBase>, MeshOrder>(s_MeshComparer);
			for (int i = 0; i < num; i++)
			{
				MeshBase meshBase = Root.Meshes[i];
				Dictionary<VertexBufferDescriptor, PrimitiveSet> dictionary2 = new Dictionary<VertexBufferDescriptor, PrimitiveSet>();
				for (int j = 0; j < meshBase.Primitives.Count; j++)
				{
					MeshPrimitiveBase meshPrimitiveBase = meshBase.Primitives[j];
					VertexBufferDescriptor key = VertexBufferDescriptor.FromPrimitive(meshPrimitiveBase);
					if (!dictionary2.ContainsKey(key))
					{
						dictionary2[key] = new PrimitiveSet();
					}
					dictionary2[key].Add(j, meshPrimitiveBase);
					if (meshPrimitiveBase.indices >= 0)
					{
						AccessorUsage newUsage = ((meshPrimitiveBase.mode == DrawMode.Triangles) ? AccessorUsage.IndexFlipped : AccessorUsage.Index);
						SetAccessorUsage(meshPrimitiveBase.indices, newUsage);
					}
					if (meshPrimitiveBase.material >= 0)
					{
						if (Root.Materials != null && meshPrimitiveBase.mode == DrawMode.Points)
						{
							SetMaterialPointsSupport(meshPrimitiveBase.material);
						}
					}
					else
					{
						m_DefaultMaterialPointsSupport |= meshPrimitiveBase.mode == DrawMode.Points;
					}
				}
				int num3 = 0;
				foreach (KeyValuePair<VertexBufferDescriptor, PrimitiveSet> item in dictionary2)
				{
					PrimitiveSet value = item.Value;
					int[] indices;
					if (dictionary.TryGetValue(value.Primitives, out var value2))
					{
						value.BuildAndDispose(out indices, out var _);
						value2.AddRecipient(new MeshSubset(i, num3, indices));
					}
					else
					{
						MeshPrimitiveBase[] primitives;
						value2 = (dictionary[primitives] = CreateMeshOrder(value, meshBase, i, num3, out indices, out primitives));
						m_MeshOrders.Add(value2);
					}
					this.MeshResultAssigned?.Invoke(num3, i, indices);
					num3++;
				}
				num2 = (array[i + 1] = num2 + dictionary2.Count);
			}
			if (Root.Skins != null)
			{
				m_SkinsInverseBindMatrices = new Matrix4x4[Root.Skins.Count][];
				foreach (Skin skin in Root.Skins)
				{
					if (skin.inverseBindMatrices >= 0)
					{
						SetAccessorUsage(skin.inverseBindMatrices, AccessorUsage.InverseBindMatrix);
					}
				}
			}
			if (Root.Nodes != null)
			{
				foreach (NodeBase node in Root.Nodes)
				{
					MeshGpuInstancing.Attributes attributes = node.Extensions?.EXT_mesh_gpu_instancing?.attributes;
					if (attributes != null)
					{
						if (attributes.TRANSLATION >= 0)
						{
							SetAccessorUsage(attributes.TRANSLATION, AccessorUsage.Translation | AccessorUsage.RequiredForInstantiation);
						}
						if (attributes.ROTATION >= 0)
						{
							SetAccessorUsage(attributes.ROTATION, AccessorUsage.Rotation | AccessorUsage.RequiredForInstantiation);
						}
						if (attributes.SCALE >= 0)
						{
							SetAccessorUsage(attributes.SCALE, AccessorUsage.Scale | AccessorUsage.RequiredForInstantiation);
						}
					}
				}
			}
			if (array != null)
			{
				m_Meshes = new List<UnityEngine.Mesh>();
				m_MeshAssignments = new FlatArray<MeshAssignment>(array);
			}
			List<JobHandle> tmpList = new List<JobHandle>();
			bool success = true;
			if (!success)
			{
				return false;
			}
			if (Root.HasAnimation)
			{
				for (int k = 0; k < Root.Animations.Count; k++)
				{
					AnimationBase animationBase = Root.Animations[k];
					foreach (AnimationSampler sampler in animationBase.Samplers)
					{
						SetAccessorUsage(sampler.input, AccessorUsage.AnimationTimes);
					}
					foreach (AnimationChannelBase channel in animationBase.Channels)
					{
						int output = animationBase.Samplers[channel.sampler].output;
						switch (channel.Target.GetPath())
						{
						case AnimationChannelBase.Path.Translation:
							SetAccessorUsage(output, AccessorUsage.Translation);
							break;
						case AnimationChannelBase.Path.Rotation:
							SetAccessorUsage(output, AccessorUsage.Rotation);
							break;
						case AnimationChannelBase.Path.Scale:
							SetAccessorUsage(output, AccessorUsage.Scale);
							break;
						case AnimationChannelBase.Path.Weights:
							SetAccessorUsage(output, AccessorUsage.Weight);
							break;
						}
					}
				}
			}
			m_AccessorData = new IDisposable[Root.Accessors.Count];
			for (int l = 0; l < m_AccessorData.Length; l++)
			{
				AccessorBase accessorBase = Root.Accessors[l];
				if (accessorBase.bufferView < 0)
				{
					continue;
				}
				switch (accessorBase.GetAttributeType())
				{
				case GltfAccessorAttributeType.MAT4:
					if (m_AccessorUsage[l] == AccessorUsage.InverseBindMatrix)
					{
						GetMatricesJob(l, out var matrices, out var jobHandle5);
						tmpList.Add(jobHandle5.Value);
						m_AccessorData[l] = matrices;
					}
					break;
				case GltfAccessorAttributeType.VEC3:
					if ((m_AccessorUsage[l] & AccessorUsage.Translation) != AccessorUsage.Unknown)
					{
						GetVector3Job(l, out var vectors, out var jobHandle2, flip: true);
						tmpList.Add(jobHandle2.Value);
						m_AccessorData[l] = vectors;
					}
					else if ((m_AccessorUsage[l] & AccessorUsage.Scale) != AccessorUsage.Unknown)
					{
						GetVector3Job(l, out var vectors2, out var jobHandle3, flip: false);
						tmpList.Add(jobHandle3.Value);
						m_AccessorData[l] = vectors2;
					}
					break;
				case GltfAccessorAttributeType.VEC4:
					if ((m_AccessorUsage[l] & AccessorUsage.Rotation) != AccessorUsage.Unknown)
					{
						GetVector4Job(l, out var vectors3, out var jobHandle4);
						tmpList.Add(jobHandle4.Value);
						m_AccessorData[l] = vectors3;
					}
					break;
				case GltfAccessorAttributeType.SCALAR:
					if (m_AccessorUsage[l] == AccessorUsage.AnimationTimes || m_AccessorUsage[l] == AccessorUsage.Weight)
					{
						GetScalarJob(l, out var scalars, out var jobHandle);
						if (scalars.HasValue)
						{
							m_AccessorData[l] = scalars.Value;
						}
						if (jobHandle.HasValue)
						{
							tmpList.Add(jobHandle.Value);
						}
					}
					break;
				}
				await DeferAgent.BreakPoint();
			}
			NativeArray<JobHandle> jobs = new NativeArray<JobHandle>(tmpList.ToArray(), Allocator.Persistent);
			m_AccessorJobsHandle = JobHandle.CombineDependencies(jobs);
			jobs.Dispose();
			JobHandle.ScheduleBatchedJobs();
			return success;
		}

		private MeshOrder CreateMeshOrder(IPrimitiveSet primitiveSet, MeshBase mesh, int meshIndex, int meshNumeration, out int[] primIndexArray, out MeshPrimitiveBase[] primitives)
		{
			string[] morphTargetNames = ((!primitiveSet.HasMorphTargets) ? null : mesh.Extras?.targetNames);
			primitiveSet.BuildAndDispose(out primIndexArray, out primitives, out var subMeshAssignments);
			MeshSubset subset = new MeshSubset(meshIndex, meshNumeration, primIndexArray);
			MeshGeneratorBase generator = new MeshGenerator(primitives, subMeshAssignments, morphTargetNames, mesh.name, this);
			MeshOrder result = new MeshOrder(generator);
			result.AddRecipient(subset);
			return result;
		}

		private void SetAccessorUsage(int index, AccessorUsage newUsage)
		{
			m_AccessorUsage[index] = newUsage;
		}

		private async Task AssignAllAccessorData()
		{
			if (Root.Skins == null)
			{
				return;
			}
			for (int s = 0; s < Root.Skins.Count; s++)
			{
				Skin skin = Root.Skins[s];
				if (skin.inverseBindMatrices >= 0)
				{
					m_SkinsInverseBindMatrices[s] = ((NativeArray<Matrix4x4>)(object)m_AccessorData[skin.inverseBindMatrices]).ToArray();
				}
				await DeferAgent.BreakPoint();
			}
		}

		private unsafe void GetMatricesJob(int accessorIndex, out NativeArray<Matrix4x4> matrices, out JobHandle? jobHandle)
		{
			AccessorBase accessorBase = Root.Accessors[accessorIndex];
			int byteStride;
			NativeSlice<byte> bufferView = ((IGltfBuffers)this).GetBufferView(accessorBase.bufferView, out byteStride, accessorBase.byteOffset, 0);
			matrices = new NativeArray<Matrix4x4>(accessorBase.count, Allocator.Persistent);
			if (accessorBase.IsSparse)
			{
				Logger?.Error(LogCode.SparseAccessor, "Matrix");
			}
			if (accessorBase.componentType == GltfComponentType.Float)
			{
				ConvertMatricesJob jobData = new ConvertMatricesJob
				{
					input = (float4x4*)bufferView.GetUnsafeReadOnlyPtr(),
					result = (float4x4*)matrices.GetUnsafePtr()
				};
				jobHandle = IJobParallelForExtensions.Schedule(jobData, accessorBase.count, 512);
			}
			else
			{
				Logger?.Error(LogCode.IndexFormatInvalid, accessorBase.componentType.ToString());
				jobHandle = null;
			}
		}

		private unsafe void GetVector3Job(int accessorIndex, out NativeArray<Vector3> vectors, out JobHandle? jobHandle, bool flip)
		{
			AccessorBase accessorBase = Root.Accessors[accessorIndex];
			int byteStride;
			NativeSlice<byte> bufferView = ((IGltfBuffers)this).GetBufferView(accessorBase.bufferView, out byteStride, accessorBase.byteOffset, 0);
			vectors = new NativeArray<Vector3>(accessorBase.count, Allocator.Persistent);
			if (accessorBase.IsSparse)
			{
				Logger?.Error(LogCode.SparseAccessor, "Vector3");
			}
			if (accessorBase.componentType == GltfComponentType.Float)
			{
				if (flip)
				{
					ConvertVector3FloatToFloatJob jobData = new ConvertVector3FloatToFloatJob
					{
						input = (float3*)bufferView.GetUnsafeReadOnlyPtr(),
						result = (float3*)vectors.GetUnsafePtr()
					};
					jobHandle = IJobParallelForExtensions.Schedule(jobData, accessorBase.count, 512);
				}
				else
				{
					MemCopyJob jobData2 = new MemCopyJob
					{
						input = bufferView.GetUnsafeReadOnlyPtr(),
						bufferSize = accessorBase.count * 12,
						result = vectors.GetUnsafePtr()
					};
					jobHandle = jobData2.Schedule();
				}
			}
			else
			{
				Logger?.Error(LogCode.IndexFormatInvalid, accessorBase.componentType.ToString());
				jobHandle = null;
			}
		}

		private unsafe void GetVector4Job(int accessorIndex, out NativeArray<Quaternion> vectors, out JobHandle? jobHandle)
		{
			AccessorBase accessorBase = Root.Accessors[accessorIndex];
			int byteStride;
			NativeSlice<byte> bufferView = ((IGltfBuffers)this).GetBufferView(accessorBase.bufferView, out byteStride, accessorBase.byteOffset, 0);
			vectors = new NativeArray<Quaternion>(accessorBase.count, Allocator.Persistent);
			if (accessorBase.IsSparse)
			{
				Logger?.Error(LogCode.SparseAccessor, "Vector4");
			}
			switch (accessorBase.componentType)
			{
			case GltfComponentType.Float:
			{
				ConvertRotationsFloatToFloatJob jobData = new ConvertRotationsFloatToFloatJob
				{
					input = (float4*)bufferView.GetUnsafeReadOnlyPtr(),
					result = (float4*)vectors.GetUnsafePtr()
				};
				jobHandle = IJobParallelForExtensions.Schedule(jobData, accessorBase.count, 512);
				break;
			}
			case GltfComponentType.Short:
			{
				ConvertRotationsInt16ToFloatJob jobData2 = new ConvertRotationsInt16ToFloatJob
				{
					input = (short*)bufferView.GetUnsafeReadOnlyPtr(),
					result = (float*)vectors.GetUnsafePtr()
				};
				jobHandle = IJobParallelForExtensions.Schedule(jobData2, accessorBase.count, 512);
				break;
			}
			case GltfComponentType.Byte:
			{
				ConvertRotationsInt8ToFloatJob jobData3 = new ConvertRotationsInt8ToFloatJob
				{
					input = (sbyte*)bufferView.GetUnsafeReadOnlyPtr(),
					result = (float*)vectors.GetUnsafePtr()
				};
				jobHandle = IJobParallelForExtensions.Schedule(jobData3, accessorBase.count, 512);
				break;
			}
			default:
				Logger?.Error(LogCode.IndexFormatInvalid, accessorBase.componentType.ToString());
				jobHandle = null;
				break;
			}
		}

		private unsafe void GetScalarJob(int accessorIndex, out NativeArray<float>? scalars, out JobHandle? jobHandle)
		{
			scalars = null;
			jobHandle = null;
			AccessorBase accessorBase = Root.Accessors[accessorIndex];
			int byteStride;
			NativeSlice<byte> bufferView = ((IGltfBuffers)this).GetBufferView(accessorBase.bufferView, out byteStride, accessorBase.byteOffset, 0);
			if (accessorBase.IsSparse)
			{
				Logger?.Error(LogCode.SparseAccessor, "scalars");
			}
			if (accessorBase.componentType == GltfComponentType.Float)
			{
				NativeArray<float> array = Reinterpret<float>(bufferView, accessorBase.count);
				scalars = new NativeArray<float>(array, Allocator.Persistent);
			}
			else if (accessorBase.normalized)
			{
				scalars = new NativeArray<float>(accessorBase.count, Allocator.Persistent);
				switch (accessorBase.componentType)
				{
				case GltfComponentType.Byte:
				{
					ConvertScalarInt8ToFloatNormalizedJob jobData = new ConvertScalarInt8ToFloatNormalizedJob
					{
						input = (sbyte*)bufferView.GetUnsafeReadOnlyPtr(),
						result = scalars.Value
					};
					jobHandle = IJobParallelForExtensions.Schedule(jobData, accessorBase.count, 512);
					break;
				}
				case GltfComponentType.UnsignedByte:
				{
					ConvertScalarUInt8ToFloatNormalizedJob jobData4 = new ConvertScalarUInt8ToFloatNormalizedJob
					{
						input = (byte*)bufferView.GetUnsafeReadOnlyPtr(),
						result = scalars.Value
					};
					jobHandle = IJobParallelForExtensions.Schedule(jobData4, accessorBase.count, 512);
					break;
				}
				case GltfComponentType.Short:
				{
					ConvertScalarInt16ToFloatNormalizedJob jobData3 = new ConvertScalarInt16ToFloatNormalizedJob
					{
						input = (short*)bufferView.GetUnsafeReadOnlyPtr(),
						result = scalars.Value
					};
					jobHandle = IJobParallelForExtensions.Schedule(jobData3, accessorBase.count, 512);
					break;
				}
				case GltfComponentType.UnsignedShort:
				{
					ConvertScalarUInt16ToFloatNormalizedJob jobData2 = new ConvertScalarUInt16ToFloatNormalizedJob
					{
						input = (ushort*)bufferView.GetUnsafeReadOnlyPtr(),
						result = scalars.Value
					};
					jobHandle = IJobParallelForExtensions.Schedule(jobData2, accessorBase.count, 512);
					break;
				}
				default:
					Logger?.Error(LogCode.AnimationFormatInvalid, accessorBase.componentType.ToString());
					break;
				}
			}
			else
			{
				Logger?.Error(LogCode.AnimationFormatInvalid, accessorBase.componentType.ToString());
			}
		}

		AccessorBase IGltfBuffers.GetAccessor(int index)
		{
			if (index >= 0 && Root.Accessors != null && index < Root.Accessors.Count)
			{
				return Root.Accessors[index];
			}
			return null;
		}

		unsafe void IGltfBuffers.GetAccessorAndData(int index, out AccessorBase accessor, out void* data, out int byteStride)
		{
			accessor = Root.Accessors[index];
			if (accessor.bufferView < 0 || accessor.bufferView >= Root.BufferViews.Count)
			{
				data = null;
				byteStride = 0;
				return;
			}
			BufferViewBase bufferViewBase = Root.BufferViews[accessor.bufferView];
			byteStride = bufferViewBase.byteStride;
			int buffer = bufferViewBase.buffer;
			NativeArray<byte>.ReadOnly buffer2 = GetBuffer(buffer);
			data = (byte*)buffer2.GetUnsafeReadOnlyPtr() + (accessor.byteOffset + bufferViewBase.byteOffset + m_BinChunks[buffer].Start);
		}

		public unsafe void GetAccessorSparseIndices(AccessorSparseIndices sparseIndices, out void* data)
		{
			BufferViewBase bufferViewBase = Root.BufferViews[(int)sparseIndices.bufferView];
			int buffer = bufferViewBase.buffer;
			NativeArray<byte>.ReadOnly buffer2 = GetBuffer(buffer);
			data = (byte*)buffer2.GetUnsafeReadOnlyPtr() + (sparseIndices.byteOffset + bufferViewBase.byteOffset + m_BinChunks[buffer].Start);
		}

		public unsafe void GetAccessorSparseValues(AccessorSparseValues sparseValues, out void* data)
		{
			BufferViewBase bufferViewBase = Root.BufferViews[(int)sparseValues.bufferView];
			int buffer = bufferViewBase.buffer;
			NativeArray<byte>.ReadOnly buffer2 = GetBuffer(buffer);
			data = (byte*)buffer2.GetUnsafeReadOnlyPtr() + (sparseValues.byteOffset + bufferViewBase.byteOffset + m_BinChunks[buffer].Start);
		}

		private static ImageFormat GetImageFormatFromMimeType(string mimeType)
		{
			if (!mimeType.StartsWith("image/"))
			{
				return ImageFormat.Unknown;
			}
			switch (mimeType.Substring(6))
			{
			case "jpeg":
				return ImageFormat.Jpeg;
			case "png":
				return ImageFormat.PNG;
			case "ktx":
			case "ktx2":
				return ImageFormat.Ktx;
			default:
				return ImageFormat.Unknown;
			}
		}
	}
}

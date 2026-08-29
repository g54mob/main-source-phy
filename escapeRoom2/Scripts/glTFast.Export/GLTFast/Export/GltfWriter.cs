using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using GLTFast.Logging;
using GLTFast.Schema;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering;

namespace GLTFast.Export
{
	public class GltfWriter : IGltfWritable
	{
		private enum State
		{
			Initialized = 0,
			ContentAdded = 1,
			Disposed = 2
		}

		private readonly struct AttributeData
		{
			public readonly VertexAttributeDescriptor descriptor;

			public readonly int inputOffset;

			public readonly int outputOffset;

			public int Size => GetAttributeSize(descriptor.format) * descriptor.dimension;

			public AttributeData(VertexAttributeDescriptor descriptor, int inputOffset, int outputOffset)
			{
				this.descriptor = descriptor;
				this.inputOffset = inputOffset;
				this.outputOffset = outputOffset;
			}
		}

		private const int k_MAXStreamCount = 4;

		private const int k_DefaultInnerLoopBatchCount = 512;

		private State m_State;

		private ExportSettings m_Settings;

		private IDeferAgent m_DeferAgent;

		private ICodeLogger m_Logger;

		private Root m_Gltf;

		private HashSet<Extension> m_ExtensionsUsedOnly;

		private HashSet<Extension> m_ExtensionsRequired;

		private List<Scene> m_Scenes;

		private List<Node> m_Nodes;

		private Dictionary<Transform, int> m_transformToNodeId;

		private List<GLTFast.Schema.Mesh> m_Meshes;

		private List<Skin> m_Skins;

		private Dictionary<int, int> m_MeshBindPoses;

		private List<int> m_SkinMesh;

		private List<GLTFast.Schema.Material> m_Materials;

		private List<GLTFast.Schema.Texture> m_Textures;

		private List<Image> m_Images;

		private List<GLTFast.Schema.Camera> m_Cameras;

		private List<LightPunctual> m_Lights;

		private List<Sampler> m_Samplers;

		private List<Accessor> m_Accessors;

		private List<BufferView> m_BufferViews;

		private List<ImageExportBase> m_ImageExports;

		private List<SamplerKey> m_SamplerKeys;

		private List<UnityEngine.Material> m_UnityMaterials;

		private List<UnityEngine.Mesh> m_UnityMeshes;

		private List<VertexAttributeUsage> m_MeshVertexAttributeUsage;

		private Dictionary<int, int[]> m_NodeMaterials;

		private Stream m_BufferStream;

		private string m_BufferPath;

		public GltfWriter(ExportSettings exportSettings = null, IDeferAgent deferAgent = null, ICodeLogger logger = null)
		{
			m_Gltf = new Root();
			m_Settings = exportSettings ?? new ExportSettings();
			m_Logger = logger;
			m_State = State.Initialized;
			m_DeferAgent = deferAgent ?? new UninterruptedDeferAgent();
		}

		public uint AddNode(float3? translation = null, quaternion? rotation = null, float3? scale = null, uint[] children = null, string name = null)
		{
			CertifyNotDisposed();
			m_State = State.ContentAdded;
			Node node = CreateNode(translation, rotation, scale, name);
			node.children = children;
			m_Nodes = m_Nodes ?? new List<Node>();
			m_Nodes.Add(node);
			return (uint)(m_Nodes.Count - 1);
		}

		[Obsolete("Use overload with skinning parameter.")]
		public void AddMeshToNode(int nodeId, UnityEngine.Mesh uMesh, int[] materialIds)
		{
			AddMeshToNode(nodeId, uMesh, materialIds, skinning: true);
		}

		[Obsolete("Use overload with skinning parameter.")]
		public void AddMeshToNode(int nodeId, UnityEngine.Mesh uMesh, int[] materialIds, bool skinning)
		{
			AddMeshToNode(nodeId, uMesh, materialIds, null);
		}

		public void AddMeshToNode(int nodeId, UnityEngine.Mesh uMesh, int[] materialIds, uint[] joints)
		{
			if ((m_Settings.ComponentMask & ComponentType.Mesh) == 0)
			{
				return;
			}
			CertifyNotDisposed();
			Node node = m_Nodes[nodeId];
			VertexAttributeUsage vertexAttributeUsage = VertexAttributeUsage.Position;
			bool flag = joints != null && joints.Length != 0;
			if (flag)
			{
				vertexAttributeUsage |= VertexAttributeUsage.Skinning;
			}
			bool flag2 = false;
			if (materialIds != null && materialIds.Length != 0)
			{
				if (m_NodeMaterials == null)
				{
					m_NodeMaterials = new Dictionary<int, int[]>();
				}
				m_NodeMaterials[nodeId] = materialIds;
				foreach (int num in materialIds)
				{
					if (num < 0)
					{
						flag2 = true;
					}
					else
					{
						vertexAttributeUsage |= GetVertexAttributeUsage(m_UnityMaterials[num].shader);
					}
				}
			}
			else
			{
				flag2 = true;
			}
			if (flag2)
			{
				vertexAttributeUsage |= VertexAttributeUsage.Normal | VertexAttributeUsage.Color;
			}
			if (!flag)
			{
				vertexAttributeUsage &= ~VertexAttributeUsage.Skinning;
			}
			node.mesh = AddMesh(uMesh, vertexAttributeUsage);
			if (flag)
			{
				node.skin = AddSkin(node.mesh, joints);
			}
		}

		public bool AddCamera(UnityEngine.Camera uCamera, out int cameraId)
		{
			if ((m_Settings.ComponentMask & ComponentType.Camera) == 0)
			{
				cameraId = -1;
				return false;
			}
			CertifyNotDisposed();
			GLTFast.Schema.Camera camera = new GLTFast.Schema.Camera();
			if (uCamera.orthographic)
			{
				camera.SetCameraType(CameraBase.Type.Orthographic);
				float orthographicSize = uCamera.orthographicSize;
				RenderTexture targetTexture = uCamera.targetTexture;
				float num = ((!(targetTexture == null)) ? ((float)targetTexture.width / (float)targetTexture.height) : ((float)Screen.width / (float)Screen.height));
				camera.orthographic = new CameraOrthographic
				{
					ymag = orthographicSize,
					xmag = orthographicSize * num,
					znear = uCamera.nearClipPlane,
					zfar = uCamera.farClipPlane
				};
			}
			else
			{
				camera.SetCameraType(CameraBase.Type.Perspective);
				camera.perspective = new CameraPerspective
				{
					yfov = uCamera.fieldOfView * (MathF.PI / 180f),
					znear = uCamera.nearClipPlane,
					zfar = uCamera.farClipPlane
				};
			}
			if (m_Cameras == null)
			{
				m_Cameras = new List<GLTFast.Schema.Camera>();
			}
			cameraId = m_Cameras.Count;
			m_Cameras.Add(camera);
			return true;
		}

		public bool AddLight(Light uLight, out int lightId)
		{
			if ((m_Settings.ComponentMask & ComponentType.Light) == 0)
			{
				lightId = -1;
				return false;
			}
			CertifyNotDisposed();
			LightPunctual lightPunctual = KhrLightsPunctual.ConvertToLight(uLight);
			lightPunctual.intensity *= m_Settings.LightIntensityFactor;
			if (m_Lights == null)
			{
				m_Lights = new List<LightPunctual>();
			}
			lightId = m_Lights.Count;
			m_Lights.Add(lightPunctual);
			return true;
		}

		public void AddCameraToNode(int nodeId, int cameraId)
		{
			CertifyNotDisposed();
			Node node = m_Nodes[nodeId];
			quaternion? rotation = quaternion.RotateY(MathF.PI);
			string name = node.name + "_Orientation";
			AddChildNode(nodeId, null, rotation, null, name).camera = cameraId;
		}

		public void AddLightToNode(int nodeId, int lightId)
		{
			CertifyNotDisposed();
			Node node = m_Nodes[nodeId];
			if (m_Lights[lightId].GetLightType() != LightPunctual.Type.Point)
			{
				quaternion? rotation = quaternion.RotateY(MathF.PI);
				string name = node.name + "_Orientation";
				node = AddChildNode(nodeId, null, rotation, null, name);
			}
			node.extensions = node.extensions ?? new NodeExtensions();
			node.Extensions.KHR_lights_punctual = new NodeLightsPunctual
			{
				light = lightId
			};
		}

		public uint AddScene(uint[] nodes, string name = null)
		{
			CertifyNotDisposed();
			m_Scenes = m_Scenes ?? new List<Scene>();
			Scene item = new Scene
			{
				name = name,
				nodes = nodes
			};
			m_Scenes.Add(item);
			if (m_Scenes.Count == 1)
			{
				m_Gltf.scene = 0;
			}
			return (uint)(m_Scenes.Count - 1);
		}

		public bool AddMaterial(UnityEngine.Material uMaterial, out int materialId, IMaterialExport materialExport)
		{
			if (m_Materials != null)
			{
				materialId = m_UnityMaterials.IndexOf(uMaterial);
				if (materialId >= 0)
				{
					return true;
				}
			}
			else
			{
				m_Materials = new List<GLTFast.Schema.Material>();
				m_UnityMaterials = new List<UnityEngine.Material>();
			}
			GLTFast.Schema.Material material;
			bool result = materialExport.ConvertMaterial(uMaterial, out material, this, m_Logger);
			materialId = m_Materials.Count;
			m_Materials.Add(material);
			m_UnityMaterials.Add(uMaterial);
			return result;
		}

		public int AddImage(ImageExportBase imageExport)
		{
			CertifyNotDisposed();
			int num;
			if (m_ImageExports != null)
			{
				num = m_ImageExports.IndexOf(imageExport);
				if (num >= 0)
				{
					return num;
				}
			}
			else
			{
				m_ImageExports = new List<ImageExportBase>();
				m_Images = new List<Image>();
			}
			num = m_ImageExports.Count;
			Image item = new Image
			{
				name = imageExport.FileName,
				mimeType = imageExport.MimeType
			};
			imageExport.JpgQuality = m_Settings.JpgQuality;
			m_ImageExports.Add(imageExport);
			m_Images.Add(item);
			return num;
		}

		public int AddTexture(int imageId, int samplerId)
		{
			CertifyNotDisposed();
			m_Textures = m_Textures ?? new List<GLTFast.Schema.Texture>();
			GLTFast.Schema.Texture texture = new GLTFast.Schema.Texture
			{
				source = imageId,
				sampler = samplerId
			};
			int num = m_Textures.FindIndex((GLTFast.Schema.Texture i) => TextureComparer.Equals(i, texture));
			if (num >= 0)
			{
				return num;
			}
			m_Textures.Add(texture);
			return m_Textures.Count - 1;
		}

		public int AddSampler(FilterMode filterMode, TextureWrapMode wrapModeU, TextureWrapMode wrapModeV)
		{
			if (filterMode == FilterMode.Bilinear && wrapModeU == TextureWrapMode.Repeat && wrapModeV == TextureWrapMode.Repeat)
			{
				return -1;
			}
			CertifyNotDisposed();
			m_Samplers = m_Samplers ?? new List<Sampler>();
			m_SamplerKeys = m_SamplerKeys ?? new List<SamplerKey>();
			SamplerKey item = new SamplerKey(filterMode, wrapModeU, wrapModeV);
			int num = m_SamplerKeys.IndexOf(item);
			if (num >= 0)
			{
				return num;
			}
			m_Samplers.Add(new Sampler(filterMode, wrapModeU, wrapModeV));
			m_SamplerKeys.Add(item);
			return m_Samplers.Count - 1;
		}

		public void RegisterExtensionUsage(Extension extension, bool required = true)
		{
			CertifyNotDisposed();
			if (required)
			{
				m_ExtensionsRequired = m_ExtensionsRequired ?? new HashSet<Extension>();
				m_ExtensionsRequired.Add(extension);
			}
			else if (m_ExtensionsRequired == null || !m_ExtensionsRequired.Contains(extension))
			{
				m_ExtensionsUsedOnly = m_ExtensionsUsedOnly ?? new HashSet<Extension>();
				m_ExtensionsUsedOnly.Add(extension);
			}
		}

		public async Task<bool> SaveToFileAndDispose(string path)
		{
			CertifyNotDisposed();
			string extension = Path.GetExtension(path);
			bool num = m_Settings.Format == GltfFormat.Binary;
			string bufferPath = null;
			if (!num)
			{
				bufferPath = ((!string.IsNullOrEmpty(extension)) ? (path.Substring(0, path.Length - extension.Length) + ".bin") : (path + ".bin"));
			}
			FileStream outStream = new FileStream(path, FileMode.Create);
			bool result = await SaveAndDispose(outStream, bufferPath, Path.GetDirectoryName(path));
			outStream.Close();
			return result;
		}

		public async Task<bool> SaveToStreamAndDispose(Stream stream)
		{
			CertifyNotDisposed();
			if (m_Settings.Format != GltfFormat.Binary || GetFinalImageDestination() == ImageDestination.SeparateFile)
			{
				m_Logger?.Error(LogCode.None, "Save to Stream currently only works for self-contained glTF-Binary");
				return false;
			}
			return await SaveAndDispose(stream);
		}

		private async Task<bool> SaveAndDispose(Stream outStream, string bufferPath = null, string directory = null)
		{
			m_BufferPath = bufferPath;
			if (!(await Bake(Path.GetFileName(m_BufferPath), directory)))
			{
				m_BufferStream?.Close();
				Dispose();
				return false;
			}
			if (m_Settings.Format == GltfFormat.Binary)
			{
				await WriteBytesToStream(outStream, BitConverter.GetBytes(1179937895u));
				await WriteBytesToStream(outStream, BitConverter.GetBytes(2u));
				MemoryStream jsonStream = null;
				bool outStreamCanSeek = outStream.CanSeek;
				uint jsonLength;
				if (outStreamCanSeek)
				{
					for (int i = 0; i < 12; i++)
					{
						outStream.WriteByte(0);
					}
					await WriteJsonToStream(outStream);
					jsonLength = (uint)(outStream.Length - 12 - 8);
				}
				else
				{
					jsonStream = new MemoryStream();
					await WriteJsonToStream(jsonStream);
					jsonLength = (uint)jsonStream.Length;
				}
				int jsonPad = GetPadByteCount(jsonLength);
				int binPad = 0;
				uint num = (uint)(20 + jsonLength + jsonPad);
				bool hasBufferContent = (m_BufferStream?.Length ?? 0) > 0;
				if (hasBufferContent)
				{
					binPad = GetPadByteCount((uint)m_BufferStream.Length);
					num += (uint)(int)(8 + m_BufferStream.Length + binPad);
				}
				if (outStreamCanSeek)
				{
					outStream.Seek(8L, SeekOrigin.Begin);
				}
				await WriteBytesToStream(outStream, BitConverter.GetBytes(num));
				await WriteBytesToStream(outStream, BitConverter.GetBytes((uint)(jsonLength + jsonPad)));
				await WriteBytesToStream(outStream, BitConverter.GetBytes(1313821514u));
				if (outStreamCanSeek)
				{
					outStream.Seek(0L, SeekOrigin.End);
				}
				else
				{
					jsonStream.WriteTo(outStream);
					jsonStream.Close();
				}
				for (int j = 0; j < jsonPad; j++)
				{
					outStream.WriteByte(32);
				}
				if (hasBufferContent)
				{
					await WriteBytesToStream(outStream, BitConverter.GetBytes((uint)(m_BufferStream.Length + binPad)));
					await WriteBytesToStream(outStream, BitConverter.GetBytes(5130562u));
					MemoryStream obj = (MemoryStream)m_BufferStream;
					obj.WriteTo(outStream);
					await obj.FlushAsync();
					for (int k = 0; k < binPad; k++)
					{
						outStream.WriteByte(0);
					}
				}
			}
			else
			{
				await WriteJsonToStream(outStream);
				if (outStream.CanSeek)
				{
					_ = outStream.Length;
				}
			}
			Dispose();
			return true;
		}

		private static async Task WriteBytesToStream(Stream outStream, byte[] bytes)
		{
			await outStream.WriteAsync(bytes);
		}

		private async Task WriteJsonToStream(Stream outStream)
		{
			StreamWriter streamWriter = new StreamWriter(outStream);
			m_Gltf.GltfSerialize(streamWriter);
			await streamWriter.FlushAsync();
		}

		private void CertifyNotDisposed()
		{
			if (m_State == State.Disposed)
			{
				throw new InvalidOperationException("GltfWriter was already disposed");
			}
		}

		private ImageDestination GetFinalImageDestination()
		{
			ImageDestination imageDestination = m_Settings.ImageDestination;
			if (imageDestination == ImageDestination.Automatic)
			{
				imageDestination = ((m_Settings.Format == GltfFormat.Binary) ? ImageDestination.MainBuffer : ImageDestination.SeparateFile);
			}
			return imageDestination;
		}

		private static int GetPadByteCount(uint length)
		{
			return (int)((4 - (length & 3)) & 3);
		}

		[Conditional("DEBUG")]
		private void LogSummary(long jsonLength, long bufferLength)
		{
		}

		private async Task<bool> Bake(string bufferPath, string directory)
		{
			if (m_Meshes != null && m_Meshes.Count > 0 && !(await BakeMeshes()))
			{
				return false;
			}
			AssignBindPosesToSkins();
			AssignMaterialsToMeshes();
			if (!(await BakeImages(directory)))
			{
				return false;
			}
			if (m_BufferStream != null && m_BufferStream.Length > 0)
			{
				m_Gltf.buffers = new GLTFast.Schema.Buffer[1]
				{
					new GLTFast.Schema.Buffer
					{
						uri = bufferPath,
						byteLength = (uint)m_BufferStream.Length
					}
				};
			}
			m_Gltf.scenes = m_Scenes?.ToArray();
			m_Gltf.nodes = m_Nodes?.ToArray();
			m_Gltf.meshes = m_Meshes?.ToArray();
			m_Gltf.skins = m_Skins?.ToArray();
			m_Gltf.accessors = m_Accessors?.ToArray();
			m_Gltf.bufferViews = m_BufferViews?.ToArray();
			m_Gltf.materials = m_Materials?.ToArray();
			m_Gltf.images = m_Images?.ToArray();
			m_Gltf.textures = m_Textures?.ToArray();
			m_Gltf.samplers = m_Samplers?.ToArray();
			m_Gltf.cameras = m_Cameras?.ToArray();
			if (m_Lights != null && m_Lights.Count > 0)
			{
				RegisterExtensionUsage(Extension.LightsPunctual);
				m_Gltf.extensions = m_Gltf.extensions ?? new RootExtensions();
				m_Gltf.extensions.KHR_lights_punctual = m_Gltf.extensions.KHR_lights_punctual ?? new LightsPunctual();
				m_Gltf.extensions.KHR_lights_punctual.lights = m_Lights.ToArray();
			}
			m_Gltf.asset = new Asset
			{
				version = "2.0",
				generator = "Unity " + Application.unityVersion + " glTFast 6.12.1"
			};
			BakeExtensions();
			return true;
		}

		private void BakeExtensions()
		{
			if (m_ExtensionsRequired != null)
			{
				int num = m_ExtensionsUsedOnly?.Count ?? 0;
				m_Gltf.extensionsRequired = new string[m_ExtensionsRequired.Count];
				m_Gltf.extensionsUsed = new string[m_ExtensionsRequired.Count + num];
				int num2 = 0;
				foreach (Extension item in m_ExtensionsRequired)
				{
					string name = item.GetName();
					m_Gltf.extensionsRequired[num2] = name;
					m_Gltf.extensionsUsed[num2] = name;
					num2++;
				}
			}
			if (m_ExtensionsUsedOnly == null)
			{
				return;
			}
			int num3 = 0;
			if (m_Gltf.extensionsUsed == null)
			{
				m_Gltf.extensionsUsed = new string[m_ExtensionsUsedOnly.Count];
			}
			else
			{
				num3 = m_Gltf.extensionsUsed.Length - m_ExtensionsUsedOnly.Count;
			}
			foreach (Extension item2 in m_ExtensionsUsedOnly)
			{
				m_Gltf.extensionsUsed[num3++] = item2.GetName();
			}
		}

		private void AssignBindPosesToSkins()
		{
			if (m_SkinMesh != null && m_MeshBindPoses != null)
			{
				for (int i = 0; i < m_SkinMesh.Count; i++)
				{
					int key = m_SkinMesh[i];
					int inverseBindMatrices = m_MeshBindPoses[key];
					m_Skins[i].inverseBindMatrices = inverseBindMatrices;
				}
				m_SkinMesh = null;
				m_MeshBindPoses = null;
			}
		}

		private void AssignMaterialsToMeshes()
		{
			if (m_NodeMaterials != null && m_Meshes != null)
			{
				Dictionary<MeshMaterialCombination, int> dictionary = new Dictionary<MeshMaterialCombination, int>(m_Meshes.Count);
				Dictionary<int, MeshMaterialCombination> dictionary2 = new Dictionary<int, MeshMaterialCombination>(m_Meshes.Count);
				foreach (KeyValuePair<int, int[]> nodeMaterial in m_NodeMaterials)
				{
					int key = nodeMaterial.Key;
					int[] value = nodeMaterial.Value;
					Node node = m_Nodes[key];
					int mesh = node.mesh;
					if (mesh >= 0)
					{
						GLTFast.Schema.Mesh mesh2 = m_Meshes[mesh];
						MeshMaterialCombination meshMaterialCombination = new MeshMaterialCombination(mesh, value);
						int value2;
						if (!dictionary2.ContainsKey(mesh))
						{
							AssignMaterialsToMesh(value, mesh2);
							dictionary2[mesh] = meshMaterialCombination;
							dictionary[meshMaterialCombination] = mesh;
						}
						else if (dictionary.TryGetValue(meshMaterialCombination, out value2))
						{
							node.mesh = value2;
						}
						else
						{
							int num = DuplicateMesh(mesh);
							mesh2 = m_Meshes[num];
							AssignMaterialsToMesh(value, mesh2);
							node.mesh = num;
							dictionary[meshMaterialCombination] = num;
						}
					}
				}
			}
			m_NodeMaterials = null;
		}

		private static void AssignMaterialsToMesh(int[] materialIds, GLTFast.Schema.Mesh mesh)
		{
			for (int i = 0; i < materialIds.Length && i < mesh.primitives.Length; i++)
			{
				mesh.primitives[i].material = ((materialIds[i] >= 0) ? materialIds[i] : (-1));
			}
		}

		private int DuplicateMesh(int meshId)
		{
			GLTFast.Schema.Mesh item = (GLTFast.Schema.Mesh)m_Meshes[meshId].Clone();
			m_Meshes.Add(item);
			return m_Meshes.Count - 1;
		}

		private async Task<bool> BakeMeshes()
		{
			if ((m_Settings.Compression & Compression.Draco) != 0)
			{
				m_Logger?.Error(LogCode.PackageMissing, "Draco For Unity", "KHR_draco_mesh_compression");
				return false;
			}
			if ((m_Settings.Compression & Compression.MeshOpt) != 0)
			{
				m_Logger?.Error("Meshopt compression is not supported yet.");
				return false;
			}
			List<Task> tasks = (m_Settings.Deterministic ? null : new List<Task>(m_Meshes.Count));
			UnityEngine.Mesh.MeshDataArray? meshDataArray;
			IMeshData[] meshData = CollectMeshData(out meshDataArray);
			for (int meshId = 0; meshId < m_Meshes.Count; meshId++)
			{
				Task task = BakeMesh(meshId, meshData[meshId]);
				if (m_Settings.Deterministic || tasks == null)
				{
					await task;
				}
				else
				{
					tasks.Add(task);
				}
				await m_DeferAgent.BreakPoint();
			}
			if (!m_Settings.Deterministic)
			{
				await Task.WhenAll(tasks);
			}
			meshDataArray?.Dispose();
			return true;
		}

		private IMeshData[] CollectMeshData(out UnityEngine.Mesh.MeshDataArray? meshDataArray)
		{
			IMeshData[] array = new IMeshData[m_UnityMeshes.Count];
			bool flag = false;
			int num = 0;
			List<UnityEngine.Mesh> list = null;
			List<int> list2 = null;
			for (int i = 0; i < m_UnityMeshes.Count; i++)
			{
				UnityEngine.Mesh mesh = m_UnityMeshes[i];
				if (mesh.isReadable)
				{
					if (flag)
					{
						if (list == null)
						{
							list = new List<UnityEngine.Mesh>();
							list2 = new List<int>();
						}
						list.Add(mesh);
						list2.Add(i);
					}
					num++;
					continue;
				}
				int num2 = i;
				IMeshData meshData2;
				if (mesh.indexFormat != IndexFormat.UInt16)
				{
					IMeshData meshData = new NonReadableMeshData<uint>(mesh);
					meshData2 = meshData;
				}
				else
				{
					IMeshData meshData = new NonReadableMeshData<ushort>(mesh);
					meshData2 = meshData;
				}
				array[num2] = meshData2;
				if (list == null && num > 0)
				{
					list = new List<UnityEngine.Mesh>(i);
					list2 = new List<int>(i);
					for (int j = 0; j < i; j++)
					{
						list.Add(m_UnityMeshes[j]);
						list2.Add(j);
					}
				}
				flag = true;
			}
			meshDataArray = null;
			if (num > 0)
			{
				if (list == null)
				{
					meshDataArray = UnityEngine.Mesh.AcquireReadOnlyMeshData(m_UnityMeshes);
					for (int k = 0; k < m_UnityMeshes.Count; k++)
					{
						int num3 = k;
						IMeshData meshData3;
						if (m_UnityMeshes[k].indexFormat != IndexFormat.UInt16)
						{
							IMeshData meshData = new MeshDataProxy<uint>(meshDataArray.Value[k]);
							meshData3 = meshData;
						}
						else
						{
							IMeshData meshData = new MeshDataProxy<ushort>(meshDataArray.Value[k]);
							meshData3 = meshData;
						}
						array[num3] = meshData3;
					}
				}
				else
				{
					meshDataArray = UnityEngine.Mesh.AcquireReadOnlyMeshData(list);
					for (int l = 0; l < list.Count; l++)
					{
						int num4 = list2[l];
						IMeshData meshData4;
						if (m_UnityMeshes[num4].indexFormat != IndexFormat.UInt16)
						{
							IMeshData meshData = new MeshDataProxy<uint>(meshDataArray.Value[l]);
							meshData4 = meshData;
						}
						else
						{
							IMeshData meshData = new MeshDataProxy<ushort>(meshDataArray.Value[l]);
							meshData4 = meshData;
						}
						array[num4] = meshData4;
					}
				}
			}
			return array;
		}

		private async Task BakeMesh(int meshId, IMeshData meshData)
		{
			GLTFast.Schema.Mesh mesh = m_Meshes[meshId];
			UnityEngine.Mesh uMesh = m_UnityMeshes[meshId];
			VertexAttributeUsage vertexAttributeUsage = m_Settings.PreservedVertexAttributes | m_MeshVertexAttributeUsage[meshId];
			VertexAttributeDescriptor[] vertexAttributes = uMesh.GetVertexAttributes();
			int[] inputStrides = new int[4];
			int[] outputStrides = new int[4];
			int[] alignments = new int[4];
			List<int>[] streamAccessorIds = new List<int>[4];
			Attributes attributes = new Attributes();
			int vertexCount = uMesh.vertexCount;
			Dictionary<VertexAttribute, AttributeData> attrDataDict = new Dictionary<VertexAttribute, AttributeData>();
			VertexAttributeDescriptor[] array = vertexAttributes;
			for (int i = 0; i < array.Length; i++)
			{
				VertexAttributeDescriptor descriptor = array[i];
				bool num = (descriptor.attribute.ToVertexAttributeUsage() & vertexAttributeUsage) == 0;
				int attributeSize = GetAttributeSize(descriptor.format);
				int num2 = descriptor.dimension * attributeSize;
				AttributeData value = new AttributeData(descriptor, inputStrides[descriptor.stream], outputStrides[descriptor.stream]);
				inputStrides[descriptor.stream] += num2;
				alignments[descriptor.stream] = math.max(alignments[descriptor.stream], attributeSize);
				if (!num)
				{
					outputStrides[descriptor.stream] += num2;
					Accessor accessor = new Accessor
					{
						byteOffset = value.outputOffset,
						componentType = AccessorBase.GetComponentType(descriptor.format),
						count = vertexCount
					};
					accessor.SetAttributeType(AccessorBase.GetAccessorAttributeType(descriptor.dimension));
					int num3 = AddAccessor(accessor);
					List<int>[] array2 = streamAccessorIds;
					int stream = descriptor.stream;
					if (array2[stream] == null)
					{
						array2[stream] = new List<int>();
					}
					streamAccessorIds[descriptor.stream].Add(num3);
					attrDataDict[descriptor.attribute] = value;
					switch (descriptor.attribute)
					{
					case VertexAttribute.Position:
					{
						Bounds bounds = uMesh.bounds;
						Vector3 max = bounds.max;
						Vector3 min = bounds.min;
						accessor.min = new float[3]
						{
							0f - max.x,
							min.y,
							min.z
						};
						accessor.max = new float[3]
						{
							0f - min.x,
							max.y,
							max.z
						};
						attributes.POSITION = num3;
						break;
					}
					case VertexAttribute.Normal:
						attributes.NORMAL = num3;
						break;
					case VertexAttribute.Tangent:
						attributes.TANGENT = num3;
						break;
					case VertexAttribute.Color:
						attributes.COLOR_0 = num3;
						break;
					case VertexAttribute.TexCoord0:
						attributes.TEXCOORD_0 = num3;
						break;
					case VertexAttribute.TexCoord1:
						attributes.TEXCOORD_1 = num3;
						break;
					case VertexAttribute.TexCoord2:
						attributes.TEXCOORD_2 = num3;
						break;
					case VertexAttribute.TexCoord3:
						attributes.TEXCOORD_3 = num3;
						break;
					case VertexAttribute.TexCoord4:
						attributes.TEXCOORD_4 = num3;
						break;
					case VertexAttribute.TexCoord5:
						attributes.TEXCOORD_5 = num3;
						break;
					case VertexAttribute.TexCoord6:
						attributes.TEXCOORD_6 = num3;
						break;
					case VertexAttribute.TexCoord7:
						attributes.TEXCOORD_7 = num3;
						break;
					case VertexAttribute.BlendWeight:
						attributes.WEIGHTS_0 = num3;
						break;
					case VertexAttribute.BlendIndices:
						attributes.JOINTS_0 = num3;
						accessor.componentType = GltfComponentType.UnsignedShort;
						break;
					default:
						throw new ArgumentOutOfRangeException();
					}
				}
			}
			await ExportBindPoses(meshId, uMesh);
			int streamCount = 1;
			for (int j = 0; j < outputStrides.Length; j++)
			{
				if (outputStrides[j] > 0)
				{
					streamCount = j + 1;
				}
			}
			GltfComponentType componentType = ((uMesh.indexFormat == IndexFormat.UInt16) ? GltfComponentType.UnsignedShort : GltfComponentType.UnsignedInt);
			mesh.primitives = new MeshPrimitive[meshData.subMeshCount];
			Accessor[] indexAccessors = new Accessor[meshData.subMeshCount];
			int num4 = 0;
			MeshTopology? topology = null;
			for (int k = 0; k < meshData.subMeshCount; k++)
			{
				MeshTopology topology2 = meshData.GetTopology(k);
				if (!topology.HasValue)
				{
					topology = topology2;
				}
				else if (topology.Value != topology2)
				{
					m_Logger?.Error(LogCode.TopologyUnsupported, "mixed");
					return;
				}
				DrawMode? drawMode = GetDrawMode(topology2);
				if (!drawMode.HasValue)
				{
					m_Logger?.Error(LogCode.TopologyUnsupported, topology2.ToString());
					drawMode = DrawMode.Points;
				}
				Accessor accessor2 = new Accessor
				{
					byteOffset = num4,
					componentType = componentType,
					count = meshData.GetIndexCount(k)
				};
				accessor2.SetAttributeType(GltfAccessorAttributeType.SCALAR);
				if (topology2 == MeshTopology.Quads)
				{
					accessor2.count = accessor2.count / 2 * 3;
				}
				int indices = AddAccessor(accessor2);
				indexAccessors[k] = accessor2;
				num4 += accessor2.count * AccessorBase.GetComponentTypeSize(componentType);
				mesh.primitives[k] = new MeshPrimitive
				{
					mode = drawMode.Value,
					attributes = attributes,
					indices = indices
				};
			}
			if (!topology.HasValue)
			{
				m_Logger?.Error(LogCode.TopologyUnsupported, "unknown");
				return;
			}
			int bufferView = await BakeMeshIndices(meshData, uMesh, topology);
			Accessor[] array3 = indexAccessors;
			for (int i = 0; i < array3.Length; i++)
			{
				array3[i].bufferView = bufferView;
			}
			NativeArray<byte>[] inputStreams = new NativeArray<byte>[streamCount];
			NativeArray<byte>[] outputStreams = new NativeArray<byte>[streamCount];
			for (int l = 0; l < streamCount; l++)
			{
				NativeArray<byte>[] array4 = inputStreams;
				int num5 = l;
				array4[num5] = await meshData.GetVertexData(l);
				outputStreams[l] = new NativeArray<byte>(outputStrides[l] * vertexCount, Allocator.Persistent);
			}
			foreach (KeyValuePair<VertexAttribute, AttributeData> item in attrDataDict)
			{
				VertexAttribute key = item.Key;
				AttributeData value2 = item.Value;
				switch (key)
				{
				case VertexAttribute.Position:
				case VertexAttribute.Normal:
					await ConvertPositionAttribute(value2, (uint)inputStrides[value2.descriptor.stream], (uint)outputStrides[value2.descriptor.stream], vertexCount, inputStreams[value2.descriptor.stream], outputStreams[value2.descriptor.stream]);
					break;
				case VertexAttribute.Tangent:
					await ConvertTangentAttribute(value2, (uint)inputStrides[value2.descriptor.stream], (uint)outputStrides[value2.descriptor.stream], vertexCount, inputStreams[value2.descriptor.stream], outputStreams[value2.descriptor.stream]);
					break;
				case VertexAttribute.TexCoord0:
				case VertexAttribute.TexCoord1:
				case VertexAttribute.TexCoord2:
				case VertexAttribute.TexCoord3:
				case VertexAttribute.TexCoord4:
				case VertexAttribute.TexCoord5:
				case VertexAttribute.TexCoord6:
				case VertexAttribute.TexCoord7:
					await ConvertTexCoordAttribute(value2, (uint)inputStrides[value2.descriptor.stream], (uint)outputStrides[value2.descriptor.stream], vertexCount, inputStreams[value2.descriptor.stream], outputStreams[value2.descriptor.stream]);
					break;
				case VertexAttribute.Color:
				case VertexAttribute.BlendWeight:
					await ConvertSkinWeightsAttribute(value2, (uint)inputStrides[value2.descriptor.stream], (uint)outputStrides[value2.descriptor.stream], vertexCount, inputStreams[value2.descriptor.stream], outputStreams[value2.descriptor.stream]);
					break;
				case VertexAttribute.BlendIndices:
					await ConvertSkinIndicesAttributes(value2, (uint)inputStrides[value2.descriptor.stream], (uint)outputStrides[value2.descriptor.stream], vertexCount, inputStreams[value2.descriptor.stream], outputStreams[value2.descriptor.stream]);
					break;
				default:
					await ConvertGenericAttribute(value2, (uint)inputStrides[value2.descriptor.stream], (uint)outputStrides[value2.descriptor.stream], vertexCount, inputStreams[value2.descriptor.stream], outputStreams[value2.descriptor.stream]);
					break;
				}
			}
			int[] array5 = new int[streamCount];
			for (int m = 0; m < streamCount; m++)
			{
				int bufferView2 = (array5[m] = WriteBufferViewToBuffer(outputStreams[m], BufferViewTarget.ArrayBuffer, outputStrides[m], alignments[m]));
				inputStreams[m].Dispose();
				outputStreams[m].Dispose();
				List<int> list = streamAccessorIds[m];
				if (list == null)
				{
					continue;
				}
				foreach (int item2 in list)
				{
					m_Accessors[item2].bufferView = bufferView2;
				}
			}
		}

		private async Task<int> BakeMeshIndices(IMeshData meshData, UnityEngine.Mesh uMesh, MeshTopology? topology)
		{
			NativeArray<ushort> indexData16;
			JobHandle job;
			NativeArray<ushort> destIndices;
			NativeArray<byte> bufferViewData;
			NativeArray<uint> indexData32;
			JobHandle job2;
			NativeArray<uint> destIndices2;
			if (uMesh.indexFormat == IndexFormat.UInt16)
			{
				indexData16 = await ((IMeshData<ushort>)meshData).GetIndexData();
				try
				{
					job = default(JobHandle);
					if (topology.Value == MeshTopology.Quads)
					{
						int num = indexData16.Length / 4;
						destIndices = new NativeArray<ushort>(num * 6, Allocator.TempJob);
						job = ConvertSubmeshIndices(0, job);
						for (int i = 1; i < uMesh.subMeshCount; i++)
						{
							job = ConvertSubmeshIndices(i, job);
						}
					}
					else
					{
						destIndices = new NativeArray<ushort>(indexData16.Length, Allocator.TempJob);
						job = ConvertSubmeshIndices2(0, job);
						for (int j = 1; j < uMesh.subMeshCount; j++)
						{
							job = ConvertSubmeshIndices2(j, job);
						}
					}
					while (!job.IsCompleted)
					{
						await Task.Yield();
					}
					job.Complete();
					bufferViewData = destIndices.Reinterpret<byte>(2);
				}
				finally
				{
					((IDisposable)indexData16/*cast due to .constrained prefix*/).Dispose();
				}
			}
			else
			{
				indexData32 = await ((IMeshData<uint>)meshData).GetIndexData();
				try
				{
					job2 = default(JobHandle);
					if (topology.Value == MeshTopology.Quads)
					{
						int num2 = indexData32.Length / 4;
						destIndices2 = new NativeArray<uint>(num2 * 6, Allocator.TempJob);
						job2 = ConvertSubmeshIndices3(0, job2);
						for (int k = 1; k < uMesh.subMeshCount; k++)
						{
							job2 = ConvertSubmeshIndices3(k, job2);
						}
					}
					else
					{
						destIndices2 = new NativeArray<uint>(indexData32.Length, Allocator.TempJob);
						job2 = ConvertSubmeshIndices4(0, job2);
						for (int l = 1; l < uMesh.subMeshCount; l++)
						{
							job2 = ConvertSubmeshIndices4(l, job2);
						}
					}
					while (!job2.IsCompleted)
					{
						await Task.Yield();
					}
					job2.Complete();
					bufferViewData = destIndices2.Reinterpret<byte>(4);
				}
				finally
				{
					((IDisposable)indexData32/*cast due to .constrained prefix*/).Dispose();
				}
			}
			int result = WriteBufferViewToBuffer(bufferViewData, BufferViewTarget.ElementArrayBuffer, null, 2);
			bufferViewData.Dispose();
			return result;
			JobHandle ConvertSubmeshIndices(int submeshIndex, JobHandle dependency)
			{
				SubMeshDescriptor subMesh = uMesh.GetSubMesh(submeshIndex);
				int start = subMesh.indexStart / 4 * 6;
				int length = subMesh.indexCount / 4 * 6;
				job = new ExportJobs.ConvertIndicesQuadFlippedJobUInt16
				{
					input = indexData16.GetSubArray(subMesh.indexStart, subMesh.indexCount),
					result = destIndices.GetSubArray(start, length),
					baseVertexOffset = (ushort)subMesh.baseVertex
				}.Schedule(subMesh.indexCount / 4, 512, dependency);
				return job;
			}
			JobHandle ConvertSubmeshIndices2(int submeshIndex, JobHandle dependency)
			{
				SubMeshDescriptor subMesh = uMesh.GetSubMesh(submeshIndex);
				job = new ExportJobs.ConvertIndicesFlippedJobUInt16
				{
					input = indexData16.GetSubArray(subMesh.indexStart, subMesh.indexCount),
					result = destIndices.GetSubArray(subMesh.indexStart, subMesh.indexCount),
					baseVertexOffset = (ushort)subMesh.baseVertex
				}.Schedule(subMesh.indexCount / 3, 512, dependency);
				return job;
			}
			JobHandle ConvertSubmeshIndices3(int submeshIndex, JobHandle dependency)
			{
				SubMeshDescriptor subMesh = uMesh.GetSubMesh(submeshIndex);
				int start = subMesh.indexStart / 4 * 6;
				int length = subMesh.indexCount / 4 * 6;
				job2 = new ExportJobs.ConvertIndicesQuadFlippedJobUInt32
				{
					input = indexData32.GetSubArray(subMesh.indexStart, subMesh.indexCount),
					result = destIndices2.GetSubArray(start, length),
					baseVertexOffset = (ushort)subMesh.baseVertex
				}.Schedule(subMesh.indexCount / 4, 512, dependency);
				return job2;
			}
			JobHandle ConvertSubmeshIndices4(int submeshIndex, JobHandle dependency)
			{
				SubMeshDescriptor subMesh = uMesh.GetSubMesh(submeshIndex);
				job2 = new ExportJobs.ConvertIndicesFlippedJobUInt32
				{
					input = indexData32.GetSubArray(subMesh.indexStart, subMesh.indexCount),
					result = destIndices2.GetSubArray(subMesh.indexStart, subMesh.indexCount),
					baseVertexOffset = (uint)subMesh.baseVertex
				}.Schedule(subMesh.indexCount / 3, 512, dependency);
				return job2;
			}
		}

		private async Task ExportBindPoses(int meshId, UnityEngine.Mesh uMesh)
		{
			Matrix4x4[] bindposes = uMesh.bindposes;
			if (bindposes != null && bindposes.Length != 0)
			{
				Accessor accessor = new Accessor
				{
					byteOffset = 0,
					componentType = GltfComponentType.Float,
					count = bindposes.Length
				};
				accessor.SetAttributeType(GltfAccessorAttributeType.MAT4);
				int value = AddAccessor(accessor);
				if (m_MeshBindPoses == null)
				{
					m_MeshBindPoses = new Dictionary<int, int>();
				}
				m_MeshBindPoses[meshId] = value;
				accessor.bufferView = await WriteBindPosesToBuffer(bindposes);
			}
		}

		private async Task<int> WriteBindPosesToBuffer(Matrix4x4[] bindposes)
		{
			ManagedNativeArray<Matrix4x4, float4x4> nativeBindPoses = new ManagedNativeArray<Matrix4x4, float4x4>(bindposes);
			NativeArray<float4x4> matrices = nativeBindPoses.nativeArray;
			JobHandle job = new ExportJobs.ConvertMatrixJob
			{
				matrices = matrices
			}.Schedule(bindposes.Length, 512);
			while (!job.IsCompleted)
			{
				await Task.Yield();
			}
			job.Complete();
			int result = WriteBufferViewToBuffer(matrices.Reinterpret<byte>(64), BufferViewTarget.None, null, 4);
			nativeBindPoses.Dispose();
			return result;
		}

		private int AddAccessor(Accessor accessor)
		{
			m_Accessors = m_Accessors ?? new List<Accessor>();
			int count = m_Accessors.Count;
			m_Accessors.Add(accessor);
			return count;
		}

		private async Task<bool> BakeImages(string directory)
		{
			HashSet<string> fileNames;
			if (m_ImageExports != null)
			{
				Dictionary<int, string> fileNameOverrides = null;
				ImageDestination imageDest = GetFinalImageDestination();
				bool overwrite = m_Settings.FileConflictResolution == FileConflictResolution.Overwrite;
				if (!overwrite && imageDest == ImageDestination.SeparateFile)
				{
					bool flag = false;
					fileNames = new HashSet<string>();
					for (int i = 0; i < m_ImageExports.Count; i++)
					{
						string filename = Path.GetFileName(m_ImageExports[i].FileName);
						if (GetUniqueFileName(ref filename))
						{
							fileNameOverrides = fileNameOverrides ?? new Dictionary<int, string>();
							fileNameOverrides[i] = filename;
						}
						fileNames.Add(filename);
						if (File.Exists(Path.Combine(directory, filename)))
						{
							flag = true;
						}
					}
					if (flag && m_Settings.FileConflictResolution == FileConflictResolution.Abort)
					{
						return false;
					}
				}
				for (int imageId = 0; imageId < m_ImageExports.Count; imageId++)
				{
					ImageExportBase imageExportBase = m_ImageExports[imageId];
					switch (imageDest)
					{
					case ImageDestination.MainBuffer:
					{
						byte[] data = imageExportBase.GetData();
						if (data != null)
						{
							m_Images[imageId].bufferView = WriteBufferViewToBuffer(data, BufferViewTarget.None);
						}
						break;
					}
					case ImageDestination.SeparateFile:
					{
						if (fileNameOverrides == null || !fileNameOverrides.TryGetValue(imageId, out var value))
						{
							value = imageExportBase.FileName;
						}
						if (imageExportBase.Write(Path.Combine(directory, value), overwrite))
						{
							m_Images[imageId].uri = value;
						}
						else
						{
							m_Images[imageId] = null;
						}
						break;
					}
					}
					await m_DeferAgent.BreakPoint();
				}
			}
			m_ImageExports = null;
			return true;
			bool GetUniqueFileName(ref string reference)
			{
				if (fileNames.Contains(reference))
				{
					int num = 0;
					string extension = Path.GetExtension(reference);
					string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(reference);
					string text;
					do
					{
						text = $"{fileNameWithoutExtension}_{num++}{extension}";
					}
					while (fileNames.Contains(text));
					reference = text;
					return true;
				}
				return false;
			}
		}

		private static async Task ConvertSkinWeightsAttribute(AttributeData attrData, uint inputByteStride, uint outputByteStride, int vertexCount, NativeArray<byte> inputStream, NativeArray<byte> outputStream)
		{
			JobHandle job = ConvertSkinWeightsAttributeJob(attrData, inputByteStride, outputByteStride, vertexCount, inputStream, outputStream);
			while (!job.IsCompleted)
			{
				await Task.Yield();
			}
			job.Complete();
		}

		private static async Task ConvertPositionAttribute(AttributeData attrData, uint inputByteStride, uint outputByteStride, int vertexCount, NativeArray<byte> inputStream, NativeArray<byte> outputStream)
		{
			JobHandle job = CreateConvertPositionAttributeJob(attrData, inputByteStride, outputByteStride, vertexCount, inputStream, outputStream);
			while (!job.IsCompleted)
			{
				await Task.Yield();
			}
			job.Complete();
		}

		private unsafe static JobHandle CreateConvertPositionAttributeJob(AttributeData attrData, uint inputByteStride, uint outputByteStride, int vertexCount, NativeArray<byte> inputStream, NativeArray<byte> outputStream)
		{
			if (attrData.descriptor.format == VertexAttributeFormat.Float16)
			{
				return new ExportJobs.ConvertPositionHalfJob
				{
					input = (byte*)inputStream.GetUnsafeReadOnlyPtr() + attrData.inputOffset,
					inputByteStride = inputByteStride,
					outputByteStride = outputByteStride,
					output = (byte*)outputStream.GetUnsafePtr() + attrData.outputOffset
				}.Schedule(vertexCount, 512);
			}
			return new ExportJobs.ConvertPositionFloatJob
			{
				input = (byte*)inputStream.GetUnsafeReadOnlyPtr() + attrData.inputOffset,
				inputByteStride = inputByteStride,
				outputByteStride = outputByteStride,
				output = (byte*)outputStream.GetUnsafePtr() + attrData.outputOffset
			}.Schedule(vertexCount, 512);
		}

		private static async Task ConvertTangentAttribute(AttributeData attrData, uint inputByteStride, uint outputByteStride, int vertexCount, NativeArray<byte> inputStream, NativeArray<byte> outputStream)
		{
			JobHandle job = CreateConvertTangentAttributeJob(attrData, inputByteStride, outputByteStride, vertexCount, inputStream, outputStream);
			while (!job.IsCompleted)
			{
				await Task.Yield();
			}
			job.Complete();
		}

		private unsafe static JobHandle CreateConvertTangentAttributeJob(AttributeData attrData, uint inputByteStride, uint outputByteStride, int vertexCount, NativeArray<byte> inputStream, NativeArray<byte> outputStream)
		{
			if (attrData.descriptor.format == VertexAttributeFormat.Float16)
			{
				return new ExportJobs.ConvertTangentHalfJob
				{
					input = (byte*)inputStream.GetUnsafeReadOnlyPtr() + attrData.inputOffset,
					inputByteStride = inputByteStride,
					outputByteStride = outputByteStride,
					output = (byte*)outputStream.GetUnsafePtr() + attrData.outputOffset
				}.Schedule(vertexCount, 512);
			}
			return new ExportJobs.ConvertTangentFloatJob
			{
				input = (byte*)inputStream.GetUnsafeReadOnlyPtr() + attrData.inputOffset,
				inputByteStride = inputByteStride,
				outputByteStride = outputByteStride,
				output = (byte*)outputStream.GetUnsafePtr() + attrData.outputOffset
			}.Schedule(vertexCount, 512);
		}

		private static async Task ConvertTexCoordAttribute(AttributeData attrData, uint inputByteStride, uint outputByteStride, int vertexCount, NativeArray<byte> inputStream, NativeArray<byte> outputStream)
		{
			JobHandle job = CreateConvertTexCoordAttributeJob(attrData, inputByteStride, outputByteStride, vertexCount, inputStream, outputStream);
			while (!job.IsCompleted)
			{
				await Task.Yield();
			}
			job.Complete();
		}

		private static async Task ConvertGenericAttribute(AttributeData attrData, uint inputByteStride, uint outputByteStride, int vertexCount, NativeArray<byte> inputStream, NativeArray<byte> outputStream)
		{
			JobHandle job = CreateConvertGenericAttributeJob(attrData, inputByteStride, outputByteStride, vertexCount, inputStream, outputStream);
			while (!job.IsCompleted)
			{
				await Task.Yield();
			}
			job.Complete();
		}

		private unsafe static JobHandle CreateConvertTexCoordAttributeJob(AttributeData attrData, uint inputByteStride, uint outputByteStride, int vertexCount, NativeArray<byte> inputStream, NativeArray<byte> outputStream)
		{
			if (attrData.descriptor.format == VertexAttributeFormat.Float16)
			{
				return new ExportJobs.ConvertTexCoordHalfJob
				{
					input = (byte*)inputStream.GetUnsafeReadOnlyPtr() + attrData.inputOffset,
					inputByteStride = inputByteStride,
					outputByteStride = outputByteStride,
					output = (byte*)outputStream.GetUnsafePtr() + attrData.outputOffset
				}.Schedule(vertexCount, 512);
			}
			return new ExportJobs.ConvertTexCoordFloatJob
			{
				input = (byte*)inputStream.GetUnsafeReadOnlyPtr() + attrData.inputOffset,
				inputByteStride = inputByteStride,
				outputByteStride = outputByteStride,
				output = (byte*)outputStream.GetUnsafePtr() + attrData.outputOffset
			}.Schedule(vertexCount, 512);
		}

		private unsafe static JobHandle ConvertSkinWeightsAttributeJob(AttributeData attrData, uint inputByteStride, uint outputByteStride, int vertexCount, NativeArray<byte> inputStream, NativeArray<byte> outputStream)
		{
			return new ExportJobs.ConvertSkinWeightsJob
			{
				input = (byte*)inputStream.GetUnsafeReadOnlyPtr() + attrData.inputOffset,
				inputByteStride = inputByteStride,
				outputByteStride = outputByteStride,
				output = (byte*)outputStream.GetUnsafePtr() + attrData.outputOffset
			}.Schedule(vertexCount, 512);
		}

		private static async Task ConvertSkinIndicesAttributes(AttributeData indicesAttrData, uint inputByteStride, uint outputByteStride, int vertexCount, NativeArray<byte> input, NativeArray<byte> output)
		{
			JobHandle job = CreateConvertSkinIndicesAttributesJob(indicesAttrData, inputByteStride, outputByteStride, vertexCount, input, output);
			while (!job.IsCompleted)
			{
				await Task.Yield();
			}
			job.Complete();
		}

		private unsafe static JobHandle CreateConvertSkinIndicesAttributesJob(AttributeData indicesAttrData, uint inputByteStride, uint outputByteStride, int vertexCount, NativeArray<byte> input, NativeArray<byte> output)
		{
			return new ExportJobs.ConvertSkinIndicesJob
			{
				input = (byte*)input.GetUnsafeReadOnlyPtr(),
				inputByteStride = inputByteStride,
				outputByteStride = outputByteStride,
				indicesOffset = indicesAttrData.inputOffset,
				output = (byte*)output.GetUnsafePtr()
			}.Schedule(vertexCount, 512);
		}

		private unsafe static JobHandle CreateConvertGenericAttributeJob(AttributeData attrData, uint inputByteStride, uint outputByteStride, int vertexCount, NativeArray<byte> inputStream, NativeArray<byte> outputStream)
		{
			return new ExportJobs.ConvertGenericJob
			{
				inputByteStride = inputByteStride,
				outputByteStride = outputByteStride,
				byteLength = (uint)attrData.Size,
				input = (byte*)inputStream.GetUnsafeReadOnlyPtr() + attrData.inputOffset,
				output = (byte*)outputStream.GetUnsafePtr() + attrData.outputOffset
			}.Schedule(vertexCount, 512);
		}

		private static DrawMode? GetDrawMode(MeshTopology topology)
		{
			return topology switch
			{
				MeshTopology.Quads => DrawMode.Triangles, 
				MeshTopology.Triangles => DrawMode.Triangles, 
				MeshTopology.Lines => DrawMode.Lines, 
				MeshTopology.LineStrip => DrawMode.LineStrip, 
				MeshTopology.Points => DrawMode.Points, 
				_ => null, 
			};
		}

		private Node AddChildNode(int parentId, float3? translation = null, quaternion? rotation = null, float3? scale = null, string name = null)
		{
			Node node = m_Nodes[parentId];
			Node node2 = CreateNode(translation, rotation, scale, name);
			m_Nodes.Add(node2);
			uint num = (uint)(m_Nodes.Count - 1);
			if (node.children == null)
			{
				node.children = new uint[1] { num };
			}
			else
			{
				uint[] array = new uint[node.children.Length + 1];
				array[0] = num;
				node.children.CopyTo(array, 1);
				node.children = array;
			}
			return node2;
		}

		private static Node CreateNode(float3? translation = null, quaternion? rotation = null, float3? scale = null, string name = null)
		{
			Node node = new Node
			{
				name = name
			};
			if (translation.HasValue && !translation.Equals(float3.zero))
			{
				node.translation = new float[3]
				{
					0f - translation.Value.x,
					translation.Value.y,
					translation.Value.z
				};
			}
			if (rotation.HasValue && !rotation.Equals(quaternion.identity))
			{
				node.rotation = new float[4]
				{
					rotation.Value.value.x,
					0f - rotation.Value.value.y,
					0f - rotation.Value.value.z,
					rotation.Value.value.w
				};
			}
			if (scale.HasValue && !scale.Equals(new float3(1f)))
			{
				node.scale = new float[3]
				{
					scale.Value.x,
					scale.Value.y,
					scale.Value.z
				};
			}
			return node;
		}

		private int AddMesh(UnityEngine.Mesh uMesh, VertexAttributeUsage attributeUsage)
		{
			if (!uMesh.isReadable && (m_Settings.Compression & Compression.Draco) != 0)
			{
				m_Logger?.Error(LogCode.MeshNotReadable, uMesh.name);
				return -1;
			}
			if (m_UnityMeshes != null)
			{
				int num = m_UnityMeshes.IndexOf(uMesh);
				if (num >= 0)
				{
					SetVertexAttributeUsage(num, attributeUsage);
					return num;
				}
			}
			GLTFast.Schema.Mesh item = new GLTFast.Schema.Mesh
			{
				name = uMesh.name
			};
			m_Meshes = m_Meshes ?? new List<GLTFast.Schema.Mesh>();
			m_UnityMeshes = m_UnityMeshes ?? new List<UnityEngine.Mesh>();
			if (m_MeshVertexAttributeUsage == null)
			{
				m_MeshVertexAttributeUsage = new List<VertexAttributeUsage>();
			}
			m_Meshes.Add(item);
			m_UnityMeshes.Add(uMesh);
			m_MeshVertexAttributeUsage.Add(attributeUsage);
			return m_Meshes.Count - 1;
		}

		private int AddSkin(int meshId, uint[] joints)
		{
			if (m_Skins == null)
			{
				m_Skins = new List<Skin>();
			}
			if (m_SkinMesh == null)
			{
				m_SkinMesh = new List<int>();
			}
			int count = m_Skins.Count;
			Skin item = new Skin
			{
				joints = joints
			};
			m_Skins.Add(item);
			m_SkinMesh.Add(meshId);
			return count;
		}

		private unsafe int WriteBufferViewToBuffer(byte[] bufferViewData, BufferViewTarget target, int? byteStride = null)
		{
			GCHandle gCHandle = GCHandle.Alloc(bufferViewData, GCHandleType.Pinned);
			fixed (byte* ptr = &bufferViewData[0])
			{
				void* dataPointer = ptr;
				NativeArray<byte> bufferViewData2 = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<byte>(dataPointer, bufferViewData.Length, Allocator.None);
				int result = WriteBufferViewToBuffer(bufferViewData2, target, byteStride);
				gCHandle.Free();
				return result;
			}
		}

		private Stream CertifyBuffer()
		{
			if (m_BufferStream == null)
			{
				if (m_BufferPath != null)
				{
					m_BufferStream = new FileStream(m_BufferPath, FileMode.Create);
				}
				else
				{
					m_BufferStream = new MemoryStream();
				}
			}
			return m_BufferStream;
		}

		private int WriteBufferViewToBuffer(NativeArray<byte> bufferViewData, BufferViewTarget bufferViewTarget, int? byteStride = null, int byteAlignment = 0)
		{
			Stream stream = CertifyBuffer();
			long length = stream.Length;
			if (byteAlignment > 0)
			{
				long num = (byteAlignment - length % byteAlignment) % byteAlignment;
				for (int i = 0; i < num; i++)
				{
					stream.WriteByte(0);
				}
				length = stream.Length;
			}
			stream.Write(bufferViewData);
			BufferView bufferView = new BufferView
			{
				buffer = 0,
				byteOffset = (int)length,
				byteLength = bufferViewData.Length,
				target = (int)bufferViewTarget
			};
			if (byteStride.HasValue)
			{
				bufferView.byteStride = byteStride.Value;
			}
			m_BufferViews = m_BufferViews ?? new List<BufferView>();
			int count = m_BufferViews.Count;
			m_BufferViews.Add(bufferView);
			return count;
		}

		private void SetVertexAttributeUsage(int meshId, VertexAttributeUsage attributeUsage)
		{
			VertexAttributeUsage vertexAttributeUsage = m_MeshVertexAttributeUsage[meshId];
			if (((vertexAttributeUsage ^ attributeUsage) & VertexAttributeUsage.Color) == VertexAttributeUsage.Color)
			{
				m_Logger.Warning(LogCode.InconsistentVertexColorUsage, meshId.ToString());
			}
			m_MeshVertexAttributeUsage[meshId] = attributeUsage | vertexAttributeUsage;
		}

		private void Dispose()
		{
			m_Settings = null;
			m_Logger = null;
			m_Gltf = null;
			m_ExtensionsUsedOnly = null;
			m_ExtensionsRequired = null;
			m_ImageExports = null;
			m_SamplerKeys = null;
			m_UnityMaterials = null;
			m_UnityMeshes = null;
			m_MeshVertexAttributeUsage = null;
			m_NodeMaterials = null;
			m_BufferStream?.Close();
			m_BufferStream = null;
			m_BufferPath = null;
			m_Scenes = null;
			m_Nodes = null;
			m_Meshes = null;
			m_Accessors = null;
			m_BufferViews = null;
			m_Materials = null;
			m_Images = null;
			m_Textures = null;
			m_Samplers = null;
			m_State = State.Disposed;
		}

		private unsafe static int GetAttributeSize(VertexAttributeFormat format)
		{
			return format switch
			{
				VertexAttributeFormat.Float32 => 4, 
				VertexAttributeFormat.Float16 => sizeof(half), 
				VertexAttributeFormat.UNorm8 => 1, 
				VertexAttributeFormat.SNorm8 => 1, 
				VertexAttributeFormat.UNorm16 => 2, 
				VertexAttributeFormat.SNorm16 => 2, 
				VertexAttributeFormat.UInt8 => 1, 
				VertexAttributeFormat.SInt8 => 1, 
				VertexAttributeFormat.UInt16 => 2, 
				VertexAttributeFormat.SInt16 => 2, 
				VertexAttributeFormat.UInt32 => 4, 
				VertexAttributeFormat.SInt32 => 4, 
				_ => throw new ArgumentOutOfRangeException("format", format, null), 
			};
		}

		private static VertexAttributeUsage GetVertexAttributeUsage(Shader shader)
		{
			string name = shader.name;
			if (name.EndsWith("unlit", StringComparison.InvariantCultureIgnoreCase))
			{
				return VertexAttributeUsage.TwoTexCoords | VertexAttributeUsage.Skinning | VertexAttributeUsage.Position | VertexAttributeUsage.Color;
			}
			if (name.StartsWith("Shader Graphs/glTF-", StringComparison.InvariantCulture) || name.StartsWith("glTF/", StringComparison.InvariantCulture) || name.StartsWith("Particles/Standard", StringComparison.InvariantCulture))
			{
				return VertexAttributeUsage.TwoTexCoords | VertexAttributeUsage.Skinning | VertexAttributeUsage.Position | VertexAttributeUsage.Normal | VertexAttributeUsage.Tangent | VertexAttributeUsage.Color;
			}
			return VertexAttributeUsage.AllTexCoords | VertexAttributeUsage.Skinning | VertexAttributeUsage.Position | VertexAttributeUsage.Normal | VertexAttributeUsage.Tangent;
		}
	}
}

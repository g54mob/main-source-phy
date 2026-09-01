using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using MultithreadCoroutines;
using UnityEngine;

namespace Besiege
{
	public class AssetImporter : SingleInstance<AssetImporter>
	{
		public class LoadingObject
		{
			public static List<LoadingObject> queue = new List<LoadingObject>();

			public Action<LoadingObject> doneLoading;

			public Coroutine routine;

			public Texture2D tex;

			public string texError = string.Empty;

			public bool isDone;

			public void Start(IEnumerator func)
			{
				routine = SingleInstance<AssetImporter>.Instance.StartCoroutine(func);
				queue.Add(this);
			}

			public void StartAsync(IEnumerator func)
			{
				routine = SingleInstance<AssetImporter>.Instance.StartCoroutineAsync(func);
				queue.Add(this);
			}

			public bool IsCurrent()
			{
				return queue.Count != 0 && this == queue[0];
			}

			public void Done()
			{
				if (!isDone)
				{
					queue.Remove(this);
					isDone = true;
					if (doneLoading != null)
					{
						doneLoading(this);
					}
				}
			}

			public virtual void Stop()
			{
				isDone = false;
				queue.Remove(this);
				if (SingleInstance<AssetImporter>.hasInstance())
				{
					if (routine != null)
					{
						SingleInstance<AssetImporter>.Instance.StopCoroutine(routine);
						routine = null;
					}
					if (tex != null)
					{
						UnityEngine.Object.DestroyImmediate(tex);
					}
				}
			}
		}

		public class MeshLoadingObject : LoadingObject
		{
			public Mesh mesh = new Mesh();

			public meshData data = new meshData();
		}

		public class CubeLoadingObject : LoadingObject
		{
			public Cubemap cube;

			public Color average;

			public override void Stop()
			{
				base.Stop();
				if (SingleInstance<AssetImporter>.hasInstance() && cube != null)
				{
					UnityEngine.Object.DestroyImmediate(cube);
				}
			}
		}

		public class meshData
		{
			public List<Vector3> vertices = new List<Vector3>();

			public List<Vector3> normals = new List<Vector3>();

			public List<Vector3> uv = new List<Vector3>();

			public List<Vector3> faceData = new List<Vector3>();

			public List<int> triangles = new List<int>();

			public Vector3[] newVerts;

			public Vector3[] newNormals;

			public Vector2[] newUVs;

			protected bool missingNormals;

			public void CreateNewDataBasedOnFaceData()
			{
				int count = faceData.Count;
				newVerts = new Vector3[count];
				newUVs = new Vector2[count];
				newNormals = new Vector3[count];
				if (normals.Count <= 0)
				{
					missingNormals = true;
				}
				for (int i = 0; i < count; i++)
				{
					Vector3 vector = faceData[i];
					newVerts[i] = vertices[(int)vector.x - 1];
					int num = (int)vector.y - 1;
					if (num >= 0 && num < uv.Count)
					{
						newUVs[i] = uv[num];
					}
					int num2 = (int)vector.z - 1;
					if (num2 >= 0 && num2 < normals.Count)
					{
						newNormals[i] = normals[num2];
					}
				}
			}

			public void PassNewDataToMesh(ref Mesh mesh, Vector3 position, Quaternion rotation, Vector3 scale)
			{
				newVerts = TransformMesh(newVerts, position, rotation, scale);
				PassNewDataToMesh(ref mesh);
			}

			public void PassNewDataToMesh(ref Mesh mesh, bool overrideReadable = false)
			{
				mesh.vertices = newVerts;
				mesh.uv = newUVs;
				mesh.normals = newNormals;
				mesh.triangles = triangles.ToArray();
				if (missingNormals)
				{
					mesh.RecalculateNormals();
				}
				mesh.RecalculateBounds();
				mesh.Optimize();
				mesh.UploadMeshData(!readableMeshes && !overrideReadable);
			}

			public static Vector3[] TransformMesh(Vector3[] vertices, Vector3 position, Quaternion rotation, Vector3 scale)
			{
				Matrix4x4 matrix4x = Matrix4x4.TRS(position, rotation, scale);
				for (int i = 0; i < vertices.Length; i++)
				{
					vertices[i] = matrix4x.MultiplyPoint(vertices[i]);
				}
				return vertices;
			}
		}

		public class StartImport
		{
			public class Async
			{
				public static MeshLoadingObject Skin(BlockSkinLoader.SkinPack.Skin vis)
				{
					MeshLoadingObject meshLoadingObject = new MeshLoadingObject();
					meshLoadingObject.StartAsync(ImportBlockSkin(meshLoadingObject, vis, true));
					return meshLoadingObject;
				}

				public static MeshLoadingObject SkinCollection(BlockSkinLoader.SkinPack.SkinCollection collection)
				{
					if (collection.collection == null)
					{
						UnityEngine.Debug.LogError("[BlockSkinLoader/AssetImporter] no collection found");
						return null;
					}
					Dictionary<string, HashSet<MeshLoadingObject>> dictionary = new Dictionary<string, HashSet<MeshLoadingObject>>();
					MeshLoadingObject item;
					foreach (KeyValuePair<string, string[]> meshPath in collection.meshPaths)
					{
						dictionary.Add(meshPath.Key, new HashSet<MeshLoadingObject>());
						for (int i = 0; i < meshPath.Value.Length; i++)
						{
							item = new MeshLoadingObject();
							dictionary[meshPath.Key].Add(item);
						}
					}
					item = new MeshLoadingObject();
					item.StartAsync(ImportBlockSkinCollection(item, dictionary, collection, true));
					return item;
				}

				public static LoadingObject MeshOnGameObject(GameObject go, string objFilePath, string textureFilePath = null, Material material = null)
				{
					MeshLoadingObject meshLoadingObject = new MeshLoadingObject();
					meshLoadingObject.StartAsync(ImportMeshOnGameObject(meshLoadingObject, go, objFilePath, textureFilePath, material, true));
					return meshLoadingObject;
				}

				public static MeshLoadingObject Mesh(ref Mesh mesh, string objFilePath)
				{
					MeshLoadingObject meshLoadingObject = new MeshLoadingObject();
					mesh = meshLoadingObject.mesh;
					meshLoadingObject.StartAsync(ImportMesh(meshLoadingObject, objFilePath, true));
					return meshLoadingObject;
				}
			}

			public static MeshLoadingObject Skin(BlockSkinLoader.SkinPack.Skin vis)
			{
				MeshLoadingObject meshLoadingObject = new MeshLoadingObject();
				meshLoadingObject.Start(ImportBlockSkin(meshLoadingObject, vis, false));
				return meshLoadingObject;
			}

			public static LoadingObject MeshOnGameObject(GameObject go, string objFilePath, string textureFilePath = null, Material material = null)
			{
				MeshLoadingObject meshLoadingObject = new MeshLoadingObject();
				meshLoadingObject.Start(ImportMeshOnGameObject(meshLoadingObject, go, objFilePath, textureFilePath, material, false));
				return meshLoadingObject;
			}

			public static MeshLoadingObject Mesh(ref Mesh mesh, string objFilePath)
			{
				MeshLoadingObject meshLoadingObject = new MeshLoadingObject();
				mesh = meshLoadingObject.mesh;
				meshLoadingObject.Start(ImportMesh(meshLoadingObject, objFilePath, false));
				return meshLoadingObject;
			}

			public static LoadingObject Texture(string path, Action<LoadingObject> doneCallback = null, bool mipmap = true, bool nonReadable = true)
			{
				return Texture(path, true, doneCallback, nonReadable);
			}

			public static LoadingObject Texture(string path, bool spaceOut, Action<LoadingObject> doneCallback = null, bool mipmap = true, bool nonReadable = true)
			{
				LoadingObject loadingObject = new LoadingObject();
				loadingObject.doneLoading = doneCallback;
				loadingObject.Start(LoadTexture(path, loadingObject, spaceOut, mipmap, nonReadable));
				return loadingObject;
			}

			public static CubeLoadingObject Cubemap(string path, bool spaceOut, bool async, Action<LoadingObject> doneCallback = null)
			{
				CubeLoadingObject cubeLoadingObject = new CubeLoadingObject();
				cubeLoadingObject.doneLoading = doneCallback;
				cubeLoadingObject.Start(LoadCubemap(path, cubeLoadingObject, spaceOut, async));
				return cubeLoadingObject;
			}
		}

		public static List<Stopwatch> timeToLoad = new List<Stopwatch>();

		public static int MAX_VERT_AMOUNT = 65000;

		public static bool readableMeshes = false;

		public override string Name
		{
			get
			{
				return "AssetImporter";
			}
		}

		protected void Start()
		{
			if (SingleInstance<AssetImporter>.Instance == this)
			{
				UnityEngine.Object.DontDestroyOnLoad(SingleInstance<AssetImporter>.Instance);
			}
			else
			{
				UnityEngine.Object.DestroyImmediate(this);
			}
		}

		public static Texture2D LoadTexture(string path, bool mipmap = false, bool nonReadable = true)
		{
			Texture2D texture2D = new Texture2D(2, 2, TextureFormat.RGBA32, false);
			byte[] data = File.ReadAllBytes(path);
			texture2D.LoadImage(data, nonReadable);
			return texture2D;
		}

		public static Texture2D ConvertTextureBytes(byte[] thumbBytes, bool mipmap = false, bool nonReadable = true)
		{
			int num = 512;
			Texture2D texture2D = new Texture2D(num, num, TextureFormat.RGBA32, false);
			texture2D.LoadImage(thumbBytes, nonReadable);
			return texture2D;
		}

		public static IEnumerator LoadTexture(string path, LoadingObject loadingObj, bool spaceOut, bool mipmap = true, bool nonReadable = true)
		{
			if (spaceOut)
			{
				while (!loadingObj.IsCurrent())
				{
					yield return null;
				}
				yield return null;
			}
			WWW www = new WWW("file:///" + path);
			while (!www.isDone)
			{
				yield return null;
			}
			loadingObj.tex = ((!string.IsNullOrEmpty(www.error)) ? null : ((!nonReadable) ? www.texture : www.textureNonReadable));
			if (!mipmap)
			{
				loadingObj.tex = new Texture2D(loadingObj.tex.width, loadingObj.tex.height, TextureFormat.RGBA32, false);
				loadingObj.tex.LoadImage(www.bytes);
			}
			loadingObj.texError = www.error;
			www.Dispose();
			loadingObj.Done();
		}

		public static IEnumerator LoadCubemap(string path, CubeLoadingObject loadingObj, bool spaceOut, bool async = false)
		{
			if (spaceOut)
			{
				while (!loadingObj.IsCurrent())
				{
					yield return null;
				}
				yield return null;
			}
			WWW www = new WWW("file:///" + path);
			while (!www.isDone)
			{
				yield return null;
			}
			if (string.IsNullOrEmpty(www.error))
			{
				yield return null;
				Texture2D tex = www.texture;
				yield return null;
				int width = tex.width;
				int height = tex.height;
				Color[] colors = tex.GetPixels();
				tex.Apply();
				yield return null;
				www.Dispose();
				UnityEngine.Object.Destroy(tex);
				int size = Mathf.FloorToInt(Mathf.Min((float)width / 4f, (float)height / 3f));
				if (!IsPowerOfTwo(size))
				{
					UnityEngine.Debug.LogError("Trying to load a Cubemap that doesn't have faces sized as a power of 2 (e.g. 128, 256, 512) face width x height: " + Mathf.FloorToInt((float)width / 4f) + ", " + Mathf.FloorToInt((float)height / 3f) + "\n path:" + path);
					size = ClosestPowerOfTwo(size);
				}
				Color[] front = new Color[size * size];
				Color[] back = new Color[size * size];
				Color[] left = new Color[size * size];
				Color[] right = new Color[size * size];
				Color[] top = new Color[size * size];
				Color[] bottom = new Color[size * size];
				if (!async)
				{
					for (int quad = 0; quad < 4; quad++)
					{
						int startX = quad * size;
						int triStart = ((quad != 1) ? 1 : 0);
						int triEnd = ((quad != 1) ? 2 : 3);
						for (int tri = triStart; tri < triEnd; tri++)
						{
							int startY = tri * size;
							int endY = startY + size;
							for (int y = startY; y < endY; y++)
							{
								int subY = y - startY;
								int trueY = height - y - 1;
								int source = startX + trueY * width;
								int coord = subY * size;
								if (quad == 0 && tri == 1)
								{
									Array.Copy(colors, source, left, coord, size);
									continue;
								}
								switch (quad)
								{
								case 1:
									switch (tri)
									{
									case 0:
										Array.Copy(colors, source, top, coord, size);
										break;
									case 1:
										Array.Copy(colors, source, front, coord, size);
										break;
									case 2:
										Array.Copy(colors, source, bottom, coord, size);
										break;
									}
									continue;
								case 2:
									if (tri == 1)
									{
										Array.Copy(colors, source, right, coord, size);
										continue;
									}
									break;
								}
								if (quad == 3 && tri == 1)
								{
									Array.Copy(colors, source, back, coord, size);
								}
							}
							yield return null;
						}
					}
				}
				else
				{
					yield return Ninja.JumpBack;
					Parallel.For(0, 4, delegate(int pQuad)
					{
						int num = pQuad * size;
						int num2 = ((pQuad != 1) ? 1 : 0);
						int num3 = ((pQuad != 1) ? 2 : 3);
						for (int i = num2; i < num3; i++)
						{
							int num4 = i * size;
							int num5 = num4 + size;
							for (int j = num4; j < num5; j++)
							{
								int num6 = j - num4;
								int num7 = height - j - 1;
								int sourceIndex = num + num7 * width;
								int destinationIndex = num6 * size;
								if (pQuad == 0 && i == 1)
								{
									Array.Copy(colors, sourceIndex, left, destinationIndex, size);
								}
								else
								{
									switch (pQuad)
									{
									case 1:
										switch (i)
										{
										case 0:
											Array.Copy(colors, sourceIndex, top, destinationIndex, size);
											break;
										case 1:
											Array.Copy(colors, sourceIndex, front, destinationIndex, size);
											break;
										case 2:
											Array.Copy(colors, sourceIndex, bottom, destinationIndex, size);
											break;
										}
										continue;
									case 2:
										if (i == 1)
										{
											Array.Copy(colors, sourceIndex, right, destinationIndex, size);
											continue;
										}
										break;
									}
									if (pQuad == 3 && i == 1)
									{
										Array.Copy(colors, sourceIndex, back, destinationIndex, size);
									}
								}
							}
						}
					});
					yield return Ninja.JumpToUnity;
				}
				loadingObj.cube = new Cubemap(size, TextureFormat.RGB24, false);
				yield return null;
				loadingObj.cube.SetPixels(left, CubemapFace.NegativeX);
				if (size > 750)
				{
					yield return null;
				}
				loadingObj.cube.SetPixels(right, CubemapFace.PositiveX);
				if (size > 1000)
				{
					yield return null;
				}
				loadingObj.cube.SetPixels(bottom, CubemapFace.NegativeY);
				if (size > 750)
				{
					yield return null;
				}
				loadingObj.cube.SetPixels(top, CubemapFace.PositiveY);
				if (size > 1000)
				{
					yield return null;
				}
				loadingObj.cube.SetPixels(back, CubemapFace.NegativeZ);
				if (size > 750)
				{
					yield return null;
				}
				loadingObj.cube.SetPixels(front, CubemapFace.PositiveZ);
				loadingObj.cube.Apply();
				yield return null;
				Color c = GetColor(left);
				c += GetColor(front);
				c += GetColor(right);
				c += GetColor(back);
				loadingObj.average = c * 0.25f;
				yield return null;
			}
			else
			{
				loadingObj.texError = www.error;
				www.Dispose();
			}
			loadingObj.Done();
		}

		public static Color GetColor(Color[] colors, int maxSteps = 100)
		{
			Color black = Color.black;
			int num = 0;
			int num2 = colors.Length;
			maxSteps = Mathf.Min(num2, maxSteps);
			for (int i = 0; i < maxSteps; i++)
			{
				double x = (double)i / (double)(maxSteps - 1);
				double num3 = MiddleDenseDistribution(x);
				int num4 = (int)(num3 * (double)(num2 - 1));
				black += colors[num4];
				num++;
			}
			return (num <= 0) ? Color.magenta : (black / num);
		}

		public static double MiddleDenseDistribution(double x)
		{
			x = 2.0 * x;
			return 0.5 * (Math.Cos((x + 1.5) * Math.PI) / Math.PI + x);
		}

		public static bool IsPowerOfTwo(int size)
		{
			return size > 0 && (size & (size - 1)) == 0;
		}

		public static int ClosestPowerOfTwo(int size)
		{
			if (size < 1)
			{
				return 0;
			}
			size |= size >> 1;
			size |= size >> 2;
			size |= size >> 4;
			size |= size >> 8;
			size |= size >> 16;
			return size - (size >> 1);
		}

		public static IEnumerator ImportMeshOnGameObject(MeshLoadingObject loadingObj, GameObject go, string objFileName, string textureName, Material material, bool async)
		{
			if (objFileName != null && !(go == null))
			{
				LoadMeshData(ref loadingObj.data, objFileName);
				if (async)
				{
					yield return Ninja.JumpToUnity;
				}
				loadingObj.data.PassNewDataToMesh(ref loadingObj.mesh);
				loadingObj.mesh.name = Path.GetFileNameWithoutExtension(objFileName);
				MeshFilter meshFiltery = go.GetComponent<MeshFilter>() ?? go.AddComponent<MeshFilter>();
				meshFiltery.mesh = loadingObj.mesh;
				MeshRenderer meshRendy = go.GetComponent<MeshRenderer>() ?? go.AddComponent<MeshRenderer>();
				if ((bool)material)
				{
					meshRendy.material = material;
				}
				else
				{
					meshRendy.material = new Material(Shader.Find("Diffuse"));
				}
				if (textureName != null)
				{
					yield return SingleInstance<AssetImporter>.Instance.StartCoroutine(LoadTexture(textureName, loadingObj, true, false));
				}
				loadingObj.Done();
			}
		}

		public static IEnumerator ImportBlockSkinCollection(MeshLoadingObject loadingTex, Dictionary<string, HashSet<MeshLoadingObject>> lo, BlockSkinLoader.SkinPack.SkinCollection vis, bool async)
		{
			foreach (KeyValuePair<string, string[]> path in vis.meshPaths)
			{
				string name = path.Key;
				HashSet<MeshLoadingObject> pose = lo[name];
				int i = 0;
				foreach (MeshLoadingObject loadingObj in pose)
				{
					LoadMeshData(ref loadingObj.data, path.Value[i]);
					i++;
					yield return null;
				}
			}
			if (async)
			{
				yield return Ninja.JumpToUnity;
			}
			foreach (KeyValuePair<string, HashSet<MeshLoadingObject>> pose2 in lo)
			{
				string name2 = pose2.Key;
				Mesh[] m = new Mesh[pose2.Value.Count];
				int i2 = 0;
				foreach (MeshLoadingObject loadingObj2 in pose2.Value)
				{
					loadingObj2.data.PassNewDataToMesh(ref loadingObj2.mesh);
					loadingObj2.mesh.name = name2 + " " + i2;
					m[i2] = loadingObj2.mesh;
					i2++;
				}
				vis.collection[name2] = m;
			}
			if (!string.IsNullOrEmpty(vis.texPath))
			{
				while (!loadingTex.IsCurrent())
				{
					yield return null;
				}
				yield return null;
				WWW www = new WWW("file:///" + vis.texPath);
				while (!www.isDone)
				{
					yield return null;
				}
				if (string.IsNullOrEmpty(www.error))
				{
					loadingTex.tex = www.textureNonReadable;
					vis.texture = loadingTex.tex;
				}
				www.Dispose();
			}
			vis.DoneLoading();
			loadingTex.Done();
		}

		public static IEnumerator ImportBlockSkin(MeshLoadingObject loadingObj, BlockSkinLoader.SkinPack.Skin vis, bool async)
		{
			bool getImage = false;
			string p = ((!vis.pack.settings.useSingleTexture) ? vis.texPath : vis.pack.settings.sharedTexPath);
			if (!vis.doneLoading && p != null)
			{
				getImage = true;
				if (vis.pack.settings.useSingleTexture && vis.pack.settings.sharedTex != null)
				{
					getImage = false;
				}
			}
			if (vis.objPath == null || vis.meshLoaded)
			{
				if (async)
				{
					yield return Ninja.JumpToUnity;
				}
			}
			else
			{
				LoadMeshData(ref loadingObj.data, vis.objPath);
				if (async)
				{
					yield return Ninja.JumpToUnity;
				}
				loadingObj.data.PassNewDataToMesh(ref loadingObj.mesh);
				loadingObj.mesh.name = Path.GetFileNameWithoutExtension(vis.objPath);
				vis.mesh = loadingObj.mesh;
				if (!getImage)
				{
					yield return null;
				}
			}
			if (getImage)
			{
				while (!loadingObj.IsCurrent())
				{
					yield return null;
				}
				yield return null;
				WWW www = new WWW("file:///" + p);
				while (!www.isDone)
				{
					yield return null;
				}
				if (string.IsNullOrEmpty(www.error))
				{
					loadingObj.tex = new Texture2D(2, 2, TextureFormat.RGB24, true, false);
					loadingObj.tex.LoadImage(www.bytes, true);
					loadingObj.tex.name = p;
					if (loadingObj.tex.width + loadingObj.tex.height < 65)
					{
						loadingObj.tex.filterMode = FilterMode.Point;
					}
					if (vis.pack.settings.useSingleTexture)
					{
						vis.pack.settings.sharedTex = loadingObj.tex;
					}
					else
					{
						vis.texture = loadingObj.tex;
					}
				}
				www.Dispose();
			}
			vis.DoneLoading();
			loadingObj.Done();
		}

		public static Texture2D LoadPNG(string filePath)
		{
			Texture2D texture2D = null;
			if (File.Exists(filePath))
			{
				byte[] data = File.ReadAllBytes(filePath);
				texture2D = new Texture2D(2, 2);
				texture2D.LoadImage(data, true);
			}
			return texture2D;
		}

		public static IEnumerator ImportMesh(MeshLoadingObject loadingObj, string objFileName, bool async)
		{
			LoadMeshData(ref loadingObj.data, objFileName);
			if (async)
			{
				yield return Ninja.JumpToUnity;
			}
			loadingObj.data.PassNewDataToMesh(ref loadingObj.mesh);
			loadingObj.mesh.name = Path.GetFileNameWithoutExtension(objFileName);
			loadingObj.Done();
		}

		public static void LoadMeshData(ref meshData mesh, string filename)
		{
			using (StreamReader streamReader = File.OpenText(filename))
			{
				string text = streamReader.ReadLine();
				char[] separator = new char[1] { ' ' };
				char[] separator2 = new char[1] { '/' };
				int num = 0;
				int num2 = 0;
				int num3 = 0;
				int num4 = 0;
				while (text != null)
				{
					text = text.Trim();
					text = text.Replace("  ", " ");
					string[] array = text.Split(separator, 50);
					switch (array[0])
					{
					case "v":
						mesh.vertices.Add(new Vector3(Convert.ToSingle(array[1]), Convert.ToSingle(array[2]), Convert.ToSingle(array[3])));
						break;
					case "vt":
						if (num2 < mesh.uv.Count)
						{
							mesh.uv[num2] = new Vector2(Convert.ToSingle(array[1]), Convert.ToSingle(array[2]));
						}
						else
						{
							mesh.uv.Add(new Vector2(Convert.ToSingle(array[1]), Convert.ToSingle(array[2])));
						}
						num2++;
						break;
					case "vt1":
						if (num2 < mesh.uv.Count)
						{
							mesh.uv[num3] = new Vector2(Convert.ToSingle(array[1]), Convert.ToSingle(array[2]));
						}
						else
						{
							mesh.uv.Add(new Vector2(Convert.ToSingle(array[1]), Convert.ToSingle(array[2])));
						}
						num3++;
						break;
					case "vt2":
						if (num2 < mesh.uv.Count)
						{
							mesh.uv[num4] = new Vector2(Convert.ToSingle(array[1]), Convert.ToSingle(array[2]));
						}
						else
						{
							mesh.uv.Add(new Vector2(Convert.ToSingle(array[1]), Convert.ToSingle(array[2])));
						}
						num4++;
						break;
					case "vn":
						mesh.normals.Add(new Vector3(Convert.ToSingle(array[1]), Convert.ToSingle(array[2]), Convert.ToSingle(array[3])));
						break;
					case "f":
					{
						int num5 = 1;
						List<int> list = new List<int>();
						while (num5 < array.Length && (string.Empty + array[num5]).Length > 0)
						{
							Vector3 item = default(Vector3);
							string[] array2 = array[num5].Split(separator2, 3);
							item.x = ConvertStringToInt(array2[0]);
							if (array2.Length > 2)
							{
								if (array2[1] != string.Empty)
								{
									item.y = ConvertStringToInt(array2[1]);
								}
								item.z = ConvertStringToInt(array2[2]);
							}
							else if (array2.Length > 1 && array2[1] != string.Empty)
							{
								item.y = ConvertStringToInt(array2[1]);
							}
							num5++;
							mesh.faceData.Add(item);
							list.Add(num);
							if (mesh.faceData.Count == MAX_VERT_AMOUNT)
							{
								UnityEngine.Debug.LogError("Error while reading '" + filename + "': Too many vertices (MAX_VERT_AMOUNT=" + MAX_VERT_AMOUNT + ")! It may be partly invisible.");
								mesh.CreateNewDataBasedOnFaceData();
								return;
							}
							num++;
						}
						for (num5 = 1; num5 + 2 < array.Length; num5++)
						{
							mesh.triangles.Add(list[0]);
							mesh.triangles.Add(list[num5]);
							mesh.triangles.Add(list[num5 + 1]);
						}
						break;
					}
					}
					text = streamReader.ReadLine();
				}
			}
			mesh.CreateNewDataBasedOnFaceData();
		}

		public static int ConvertStringToInt(string s)
		{
			int num = 0;
			for (int i = 0; i < s.Length; i++)
			{
				num = num * 10 + (s[i] - 48);
			}
			return num;
		}
	}
}

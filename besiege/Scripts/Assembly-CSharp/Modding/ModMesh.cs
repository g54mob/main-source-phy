using System;
using System.Collections;
using System.IO;
using Besiege;
using InternalModding.Misc;
using Modding.Serialization;
using MultithreadCoroutines;
using UnityEngine;

namespace Modding
{
	public class ModMesh : ModResource
	{
		private string error;

		private bool hasError;

		private bool loaded;

		public override bool HasError
		{
			get
			{
				return hasError;
			}
		}

		public override string Error
		{
			get
			{
				return error;
			}
		}

		public override bool Loaded
		{
			get
			{
				return loaded;
			}
		}

		public Mesh Mesh { get; private set; }

		internal ModMesh()
		{
		}

		internal override IEnumerator Load()
		{
			AssetImporter.meshData meshData;
			try
			{
				meshData = new AssetImporter.meshData();
				AssetImporter.LoadMeshData(ref meshData, base.Info.Path);
			}
			catch (Exception ex)
			{
				Exception e = ex;
				hasError = true;
				error = e.ToString();
				loaded = true;
				UnityMainThreadDispatcher.Instance().Enqueue(base.TriggerOnLoad);
				yield break;
			}
			yield return Ninja.JumpToUnity;
			try
			{
				Mesh mesh = new Mesh();
				meshData.PassNewDataToMesh(ref mesh, true);
				mesh.name = Path.GetFileNameWithoutExtension(base.Info.Path);
				Mesh = mesh;
				loaded = true;
				TriggerOnLoad();
			}
			catch (Exception ex2)
			{
				Exception e2 = ex2;
				hasError = true;
				error = e2.ToString();
				loaded = true;
				TriggerOnLoad();
			}
		}

		public void SetOnObject(GameObject go, MeshReference r, Action<GameObject> postSetAction = null, Action prefabPostSetAction = null)
		{
			SetOnObject(go, delegate(GameObject obj)
			{
				if (r != null)
				{
					r.SetTransformValues(obj.transform);
				}
				if (postSetAction != null)
				{
					postSetAction(obj);
				}
			}, prefabPostSetAction);
		}

		internal override void ApplyToObject(GameObject go)
		{
			MeshFilter component = go.GetComponent<MeshFilter>();
			if (component == null)
			{
				MLog.Warn("ModMesh.SetOnObject used with an object that has no MeshFilter!");
			}
			else
			{
				component.mesh = Mesh;
			}
		}

		public static implicit operator Mesh(ModMesh mesh)
		{
			return mesh.Mesh;
		}
	}
}

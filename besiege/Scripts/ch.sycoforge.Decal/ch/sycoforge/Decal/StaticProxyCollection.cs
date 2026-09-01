using System.Collections.Generic;
using UnityEngine;

namespace ch.sycoforge.Decal
{
	public class StaticProxyCollection : MonoBehaviour
	{
		public List<GameObject> Proxies = new List<GameObject>();

		[HideInInspector]
		[SerializeField]
		public List<ProxyMesh> MeshProxies = new List<ProxyMesh>();

		public int GetStaticVertexCount()
		{
			int num = 0;
			foreach (ProxyMesh meshProxy in MeshProxies)
			{
				if (!(meshProxy.MeshProxy == null))
				{
					num += meshProxy.MeshProxy.vertexCount;
				}
			}
			return num;
		}

		public Mesh GetMesh(GameObject obj)
		{
			ProxyMesh proxyMesh = MeshProxies.Find((ProxyMesh o) => o.ID == obj.GetInstanceID());
			if (proxyMesh == null)
			{
				return null;
			}
			return proxyMesh.MeshProxy;
		}

		public void InitializeProxies()
		{
			MeshProxies = new List<ProxyMesh>();
			foreach (GameObject proxy in Proxies)
			{
				if (proxy == null)
				{
					continue;
				}
				MeshFilter component = proxy.GetComponent<MeshFilter>();
				if (component != null)
				{
					ProxyMesh proxyMesh = new ProxyMesh();
					proxyMesh.ID = proxy.GetInstanceID();
					proxyMesh.transform = proxy.transform;
					proxyMesh.MeshProxy = CloneMesh(component.sharedMesh);
					Bounds bounds = new Bounds
					{
						center = proxy.transform.position
					};
					Vector3[] vertices = component.sharedMesh.vertices;
					foreach (Vector3 position in vertices)
					{
						bounds.Encapsulate(proxy.transform.TransformPoint(position));
					}
					proxyMesh.bounds = bounds;
					MeshProxies.Add(proxyMesh);
				}
			}
		}

		public static Mesh CloneMesh(Mesh mesh)
		{
			Mesh mesh2 = new Mesh();
			if (mesh != null)
			{
				mesh2.name = mesh.name;
				mesh2.bounds = mesh.bounds;
				mesh2.vertices = mesh.vertices;
				mesh2.triangles = mesh.triangles;
				mesh2.uv = mesh.uv;
				mesh2.colors = mesh.colors;
				mesh2.name = mesh2.name + "_" + GetMeshHash(mesh);
			}
			return mesh2;
		}

		private static int GetMeshHash(Mesh clone)
		{
			int num = 790237721;
			int[] triangles = clone.triangles;
			int[] array = triangles;
			foreach (int num2 in array)
			{
				num = num * 13 + num2.GetHashCode();
			}
			return num;
		}
	}
}

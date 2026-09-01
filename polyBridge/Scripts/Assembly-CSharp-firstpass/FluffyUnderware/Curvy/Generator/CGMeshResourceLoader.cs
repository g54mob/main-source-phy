using FluffyUnderware.DevTools.Extensions;
using UnityEngine;

namespace FluffyUnderware.Curvy.Generator
{
	[ResourceLoader("Mesh")]
	public class CGMeshResourceLoader : ICGResourceLoader
	{
		public Component Create(CGModule cgModule, string context)
		{
			return cgModule.Generator.PoolManager.GetComponentPool<CGMeshResource>().Pop();
		}

		public void Destroy(CGModule cgModule, Component obj, string context, bool kill)
		{
			if (!(obj != null))
			{
				return;
			}
			if (kill)
			{
				if (Application.isPlaying)
				{
					Object.Destroy(obj.gameObject);
				}
				else
				{
					Object.DestroyImmediate(obj.gameObject);
				}
			}
			else
			{
				obj.StripComponents(typeof(CGMeshResource), typeof(MeshFilter), typeof(MeshRenderer));
				cgModule.Generator.PoolManager.GetComponentPool<CGMeshResource>().Push(obj);
			}
		}
	}
}

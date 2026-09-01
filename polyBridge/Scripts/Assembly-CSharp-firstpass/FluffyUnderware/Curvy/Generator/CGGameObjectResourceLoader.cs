using UnityEngine;

namespace FluffyUnderware.Curvy.Generator
{
	[ResourceLoader("GameObject")]
	public class CGGameObjectResourceLoader : ICGResourceLoader
	{
		public Component Create(CGModule cgModule, string context)
		{
			GameObject gameObject = cgModule.Generator.PoolManager.GetPrefabPool(context).Pop();
			if (!gameObject)
			{
				return null;
			}
			return gameObject.transform;
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
				cgModule.Generator.PoolManager.GetPrefabPool(context).Push(obj.gameObject);
			}
		}
	}
}

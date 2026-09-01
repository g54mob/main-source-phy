using UnityEngine;

namespace FluffyUnderware.Curvy.Generator
{
	public interface ICGResourceLoader
	{
		Component Create(CGModule cgModule, string context);

		void Destroy(CGModule cgModule, Component obj, string context, bool kill);
	}
}

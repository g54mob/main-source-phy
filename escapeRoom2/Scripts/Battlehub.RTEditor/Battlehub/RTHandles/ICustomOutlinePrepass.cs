using UnityEngine;

namespace Battlehub.RTHandles
{
	public interface ICustomOutlinePrepass
	{
		Renderer GetRenderer();

		Material GetOutlinePrepassMaterial();
	}
}

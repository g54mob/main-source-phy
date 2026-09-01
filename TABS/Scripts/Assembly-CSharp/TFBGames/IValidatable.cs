using UnityEngine;

namespace TFBGames
{
	public interface IValidatable
	{
		[ContextMenu("Validate Audio")]
		bool Validate();
	}
}

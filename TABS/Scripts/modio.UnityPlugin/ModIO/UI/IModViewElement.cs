using UnityEngine;

namespace ModIO.UI
{
	public interface IModViewElement
	{
		GameObject gameObject { get; }

		void SetModView(ModView view);
	}
}

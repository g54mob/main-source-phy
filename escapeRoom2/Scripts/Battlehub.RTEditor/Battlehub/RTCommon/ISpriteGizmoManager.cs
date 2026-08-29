using System;
using UnityEngine;

namespace Battlehub.RTCommon
{
	public interface ISpriteGizmoManager
	{
		void Register(Type type, Material material);

		Material Unregister(Type type);

		void Refresh();
	}
}

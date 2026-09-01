using System;
using System.Collections.Generic;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Shapes;

internal static class IMMaterialPool
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		static _003C_003Ec()
		{
			_003C_003Ec obj = new _003C_003Ec();
			_003C_003E9 = obj;
		}

		internal void _003C_002Ecctor_003Eb__1_0(Scene scene)
		{
			FlushAllMaterials();
		}
	}

	public static Dictionary<RenderState, Material> pool;

	static IMMaterialPool()
	{
		Dictionary<RenderState, Material> dictionary = new Dictionary<RenderState, Material>();
		pool = dictionary;
		UnityAction<Scene> value = delegate
		{
			FlushAllMaterials();
		};
		SceneManager.sceneUnloaded += value;
	}

	internal unsafe static Material GetMaterial(ref RenderState state)
	{
		//IL_0018: Expected O, but got Ref
		//IL_0041: Expected O, but got Ref
		object obj = default(object);
		Material material = default(Material);
		if (!pool.TryGetValue((RenderState)(&obj), out var _))
		{
			material = state.CreateMaterial();
			if (pool == null)
			{
				return (Material)(object)new NullReferenceException();
			}
			pool.Add((RenderState)(&obj), material);
		}
		return material;
	}

	private static void FlushAllMaterials()
	{
		if (pool != null)
		{
			Dictionary<RenderState, Material>.ValueCollection values = pool.Values;
			if (values != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1807D9820");
				Dictionary<RenderState, Material>.ValueCollection.Enumerator enumerator = default(Dictionary<RenderState, Material>.ValueCollection.Enumerator);
				UnityEngine.Object obj = default(UnityEngine.Object);
				while (enumerator.MoveNext())
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
					if (obj != null)
					{
						ShapesExtensions.DestroyBranched(obj);
					}
				}
				enumerator.Dispose();
				if (pool != null)
				{
					pool.Clear();
					return;
				}
			}
		}
		throw new NullReferenceException();
	}
}

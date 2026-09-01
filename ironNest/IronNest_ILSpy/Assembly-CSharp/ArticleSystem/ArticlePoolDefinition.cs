using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ArticleSystem;

public class ArticlePoolDefinition : ScriptableObject
{
	public enum SelectionMode
	{
		Random,
		Sequential
	}

	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<GameObject, bool> _003C_003E9__5_0;

		public static Predicate<GameObject> _003C_003E9__9_0;

		static _003C_003Ec()
		{
			_003C_003Ec obj = new _003C_003Ec();
			_003C_003E9 = obj;
		}

		internal bool _003Cget_UniqueArticlePrefabs_003Eb__5_0(GameObject p)
		{
			return p != null;
		}

		internal bool _003COnValidate_003Eb__9_0(GameObject p)
		{
			return p == null;
		}
	}

	public string ID;

	private SelectionMode selection;

	private List<GameObject> articlePrefabs;

	private List<GameObject> articlePrefefsDistinctCache;

	public IReadOnlyList<GameObject> UniqueArticlePrefabs
	{
		get
		{
			IReadOnlyList<GameObject> result = articlePrefefsDistinctCache;
			if (articlePrefefsDistinctCache == null)
			{
				Func<GameObject, bool> predicate = _003C_003Ec._003C_003E9__5_0;
				if (_003C_003Ec._003C_003E9__5_0 == null)
				{
					predicate = (_003C_003Ec._003C_003E9__5_0 = (GameObject p) => p != null);
				}
				IEnumerable<GameObject> source = Enumerable.Where(articlePrefabs, predicate);
				IEnumerable<GameObject> source2 = Enumerable.Distinct(source);
				result = (articlePrefefsDistinctCache = Enumerable.ToList(source2));
			}
			return result;
		}
	}

	public SelectionMode Mode => selection;

	private void OnValidate()
	{
		if (articlePrefabs == null)
		{
			List<GameObject> list = new List<GameObject>();
			articlePrefabs = list;
		}
		Predicate<GameObject> match = _003C_003Ec._003C_003E9__9_0;
		if (_003C_003Ec._003C_003E9__9_0 == null)
		{
			match = (_003C_003Ec._003C_003E9__9_0 = (GameObject p) => p == null);
		}
		int num = articlePrefabs.RemoveAll(match);
		articlePrefefsDistinctCache = null;
	}

	public ArticlePoolDefinition()
	{
		List<GameObject> list = new List<GameObject>();
		articlePrefabs = list;
		base._002Ector();
	}
}

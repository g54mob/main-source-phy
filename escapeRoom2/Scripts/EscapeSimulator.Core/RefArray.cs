using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class RefArray<T1> : IEnumerable<Ref<T1>>, IEnumerable where T1 : UnityEngine.Object
{
	[SerializeField]
	private List<Ref<T1>> refs;

	public int Length => refs.Count;

	public Ref<T1> this[int i] => refs[i];

	public T1 Get<T>(int index, int dummy = 0) where T : T1
	{
		return refs[index].Get<T1>();
	}

	public bool Contains<T>(T item, int dummy = 0) where T : T1
	{
		return refs.Any((Ref<T1> r) => r.Get<T1>() == item);
	}

	public int IndexOf<T>(T item, int dummy = 0) where T : T1
	{
		return refs.FindIndex((Ref<T1> r) => r.Get<T1>() == item);
	}

	public int FindIndex<T>(Predicate<T1> match, int dummy = 0) where T : T1
	{
		return refs.FindIndex((Ref<T1> r) => match(r.Get<T1>()));
	}

	public T1[] ToArray<T>(int dummy = 0) where T : T1
	{
		return RefArrayUtils.ToArray(Length, (int index) => refs[index].Get<T1>());
	}

	public IEnumerable<T1> Iterate<T>(int dummy = 0) where T : T1
	{
		for (int i = 0; i < Length; i++)
		{
			yield return refs[i].Get<T1>();
		}
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return GetEnumerator();
	}

	public IEnumerator<Ref<T1>> GetEnumerator()
	{
		return refs.GetEnumerator();
	}
}
[Serializable]
public class RefArray<T1, T2> : IEnumerable<Ref<T1, T2>>, IEnumerable where T1 : UnityEngine.Object where T2 : UnityEngine.Object
{
	[SerializeField]
	private List<Ref<T1, T2>> refs;

	public int Length => refs.Count;

	public Ref<T1, T2> this[int i] => refs[i];

	public T1 Get<T>(int index, int dummy = 0) where T : T1
	{
		return refs[index].Get<T1>(0);
	}

	public T2 Get<T>(int index, float dummy = 0f) where T : T2
	{
		return refs[index].Get<T2>(0f);
	}

	public bool Contains<T>(T item, int dummy = 0) where T : T1
	{
		return refs.Any((Ref<T1, T2> r) => r.Get<T1>(0) == item);
	}

	public bool Contains<T>(T item, float dummy = 0f) where T : T2
	{
		return refs.Any((Ref<T1, T2> r) => r.Get<T2>(0f) == item);
	}

	public int IndexOf<T>(T item, int dummy = 0) where T : T1
	{
		return refs.FindIndex((Ref<T1, T2> r) => r.Get<T1>(0) == item);
	}

	public int IndexOf<T>(T item, float dummy = 0f) where T : T2
	{
		return refs.FindIndex((Ref<T1, T2> r) => r.Get<T2>(0f) == item);
	}

	public int FindIndex<T>(Predicate<T1> match, int dummy = 0) where T : T1
	{
		return refs.FindIndex((Ref<T1, T2> r) => match(r.Get<T1>(0)));
	}

	public int FindIndex<T>(Predicate<T2> match, float dummy = 0f) where T : T2
	{
		return refs.FindIndex((Ref<T1, T2> r) => match(r.Get<T2>(0f)));
	}

	public T1[] ToArray<T>(int dummy = 0) where T : T1
	{
		return RefArrayUtils.ToArray(Length, (int index) => refs[index].Get<T1>(0));
	}

	public T2[] ToArray<T>(float dummy = 0f) where T : T2
	{
		return RefArrayUtils.ToArray(Length, (int index) => refs[index].Get<T2>(0f));
	}

	public IEnumerable<T1> Iterate<T>(int dummy = 0) where T : T1
	{
		for (int i = 0; i < Length; i++)
		{
			yield return refs[i].Get<T1>(0);
		}
	}

	public IEnumerable<T2> Iterate<T>(float dummy = 0f) where T : T2
	{
		for (int i = 0; i < Length; i++)
		{
			yield return refs[i].Get<T2>(0f);
		}
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return GetEnumerator();
	}

	public IEnumerator<Ref<T1, T2>> GetEnumerator()
	{
		return refs.GetEnumerator();
	}
}
[Serializable]
public class RefArray<T1, T2, T3> : IEnumerable<Ref<T1, T2, T3>>, IEnumerable where T1 : UnityEngine.Object where T2 : UnityEngine.Object where T3 : UnityEngine.Object
{
	[SerializeField]
	private List<Ref<T1, T2, T3>> refs;

	public int Length => refs.Count;

	public Ref<T1, T2, T3> this[int i] => refs[i];

	public T1 Get<T>(int index, int dummy = 0) where T : T1
	{
		return refs[index].Get<T1>(0);
	}

	public T2 Get<T>(int index, float dummy = 0f) where T : T2
	{
		return refs[index].Get<T2>(0f);
	}

	public T3 Get<T>(int index, uint dummy = 0u) where T : T3
	{
		return refs[index].Get<T3>(0u);
	}

	public bool Contains<T>(T item, int dummy = 0) where T : T1
	{
		return refs.Any((Ref<T1, T2, T3> r) => r.Get<T1>(0) == item);
	}

	public bool Contains<T>(T item, float dummy = 0f) where T : T2
	{
		return refs.Any((Ref<T1, T2, T3> r) => r.Get<T2>(0f) == item);
	}

	public bool Contains<T>(T item, uint dummy = 0u) where T : T3
	{
		return refs.Any((Ref<T1, T2, T3> r) => r.Get<T3>(0u) == item);
	}

	public int IndexOf<T>(T item, int dummy = 0) where T : T1
	{
		return refs.FindIndex((Ref<T1, T2, T3> r) => r.Get<T1>(0) == item);
	}

	public int IndexOf<T>(T item, float dummy = 0f) where T : T2
	{
		return refs.FindIndex((Ref<T1, T2, T3> r) => r.Get<T2>(0f) == item);
	}

	public int IndexOf<T>(T item, uint dummy = 0u) where T : T3
	{
		return refs.FindIndex((Ref<T1, T2, T3> r) => r.Get<T3>(0u) == item);
	}

	public int FindIndex<T>(Predicate<T1> match, int dummy = 0) where T : T1
	{
		return refs.FindIndex((Ref<T1, T2, T3> r) => match(r.Get<T1>(0)));
	}

	public int FindIndex<T>(Predicate<T2> match, float dummy = 0f) where T : T2
	{
		return refs.FindIndex((Ref<T1, T2, T3> r) => match(r.Get<T2>(0f)));
	}

	public int FindIndex<T>(Predicate<T3> match, uint dummy = 0u) where T : T3
	{
		return refs.FindIndex((Ref<T1, T2, T3> r) => match(r.Get<T3>(0u)));
	}

	public T1[] ToArray<T>(int dummy = 0) where T : T1
	{
		return RefArrayUtils.ToArray(Length, (int index) => refs[index].Get<T1>(0));
	}

	public T2[] ToArray<T>(float dummy = 0f) where T : T2
	{
		return RefArrayUtils.ToArray(Length, (int index) => refs[index].Get<T2>(0f));
	}

	public T3[] ToArray<T>(uint dummy = 0u) where T : T3
	{
		return RefArrayUtils.ToArray(Length, (int index) => refs[index].Get<T3>(0u));
	}

	public IEnumerable<T1> Iterate<T>(int dummy = 0) where T : T1
	{
		for (int i = 0; i < Length; i++)
		{
			yield return refs[i].Get<T1>(0);
		}
	}

	public IEnumerable<T2> Iterate<T>(float dummy = 0f) where T : T2
	{
		for (int i = 0; i < Length; i++)
		{
			yield return refs[i].Get<T2>(0f);
		}
	}

	public IEnumerable<T3> Iterate<T>(uint dummy = 0u) where T : T3
	{
		for (int i = 0; i < Length; i++)
		{
			yield return refs[i].Get<T3>(0u);
		}
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return GetEnumerator();
	}

	public IEnumerator<Ref<T1, T2, T3>> GetEnumerator()
	{
		return refs.GetEnumerator();
	}
}
[Serializable]
public class RefArray<T1, T2, T3, T4> : IEnumerable<Ref<T1, T2, T3, T4>>, IEnumerable where T1 : UnityEngine.Object where T2 : UnityEngine.Object where T3 : UnityEngine.Object where T4 : UnityEngine.Object
{
	[SerializeField]
	private List<Ref<T1, T2, T3, T4>> refs;

	public int Length => refs.Count;

	public Ref<T1, T2, T3, T4> this[int i] => refs[i];

	public T1 Get<T>(int index, int dummy = 0) where T : T1
	{
		return refs[index].Get<T1>(0);
	}

	public T2 Get<T>(int index, float dummy = 0f) where T : T2
	{
		return refs[index].Get<T2>(0f);
	}

	public T3 Get<T>(int index, uint dummy = 0u) where T : T3
	{
		return refs[index].Get<T3>(0u);
	}

	public T4 Get<T>(int index, short dummy = 0) where T : T4
	{
		return refs[index].Get<T4>((short)0);
	}

	public bool Contains<T>(T item, int dummy = 0) where T : T1
	{
		return refs.Any((Ref<T1, T2, T3, T4> r) => r.Get<T1>(0) == item);
	}

	public bool Contains<T>(T item, float dummy = 0f) where T : T2
	{
		return refs.Any((Ref<T1, T2, T3, T4> r) => r.Get<T2>(0f) == item);
	}

	public bool Contains<T>(T item, uint dummy = 0u) where T : T3
	{
		return refs.Any((Ref<T1, T2, T3, T4> r) => r.Get<T3>(0u) == item);
	}

	public bool Contains<T>(T item, short dummy = 0) where T : T4
	{
		return refs.Any((Ref<T1, T2, T3, T4> r) => r.Get<T4>((short)0) == item);
	}

	public int IndexOf<T>(T item, int dummy = 0) where T : T1
	{
		return refs.FindIndex((Ref<T1, T2, T3, T4> r) => r.Get<T1>(0) == item);
	}

	public int IndexOf<T>(T item, float dummy = 0f) where T : T2
	{
		return refs.FindIndex((Ref<T1, T2, T3, T4> r) => r.Get<T2>(0f) == item);
	}

	public int IndexOf<T>(T item, uint dummy = 0u) where T : T3
	{
		return refs.FindIndex((Ref<T1, T2, T3, T4> r) => r.Get<T3>(0u) == item);
	}

	public int IndexOf<T>(T item, short dummy = 0) where T : T4
	{
		return refs.FindIndex((Ref<T1, T2, T3, T4> r) => r.Get<T4>((short)0) == item);
	}

	public int FindIndex<T>(Predicate<T1> match, int dummy = 0) where T : T1
	{
		return refs.FindIndex((Ref<T1, T2, T3, T4> r) => match(r.Get<T1>(0)));
	}

	public int FindIndex<T>(Predicate<T2> match, float dummy = 0f) where T : T2
	{
		return refs.FindIndex((Ref<T1, T2, T3, T4> r) => match(r.Get<T2>(0f)));
	}

	public int FindIndex<T>(Predicate<T3> match, uint dummy = 0u) where T : T3
	{
		return refs.FindIndex((Ref<T1, T2, T3, T4> r) => match(r.Get<T3>(0u)));
	}

	public int FindIndex<T>(Predicate<T4> match, short dummy = 0) where T : T4
	{
		return refs.FindIndex((Ref<T1, T2, T3, T4> r) => match(r.Get<T4>((short)0)));
	}

	public T1[] ToArray<T>(int dummy = 0) where T : T1
	{
		return RefArrayUtils.ToArray(Length, (int index) => refs[index].Get<T1>(0));
	}

	public T2[] ToArray<T>(float dummy = 0f) where T : T2
	{
		return RefArrayUtils.ToArray(Length, (int index) => refs[index].Get<T2>(0f));
	}

	public T3[] ToArray<T>(uint dummy = 0u) where T : T3
	{
		return RefArrayUtils.ToArray(Length, (int index) => refs[index].Get<T3>(0u));
	}

	public T4[] ToArray<T>(short dummy = 0) where T : T4
	{
		return RefArrayUtils.ToArray(Length, (int index) => refs[index].Get<T4>((short)0));
	}

	public IEnumerable<T1> Iterate<T>(int dummy = 0) where T : T1
	{
		for (int i = 0; i < Length; i++)
		{
			yield return refs[i].Get<T1>(0);
		}
	}

	public IEnumerable<T2> Iterate<T>(float dummy = 0f) where T : T2
	{
		for (int i = 0; i < Length; i++)
		{
			yield return refs[i].Get<T2>(0f);
		}
	}

	public IEnumerable<T3> Iterate<T>(uint dummy = 0u) where T : T3
	{
		for (int i = 0; i < Length; i++)
		{
			yield return refs[i].Get<T3>(0u);
		}
	}

	public IEnumerable<T4> Iterate<T>(short dummy = 0) where T : T4
	{
		for (int i = 0; i < Length; i++)
		{
			yield return refs[i].Get<T4>((short)0);
		}
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return GetEnumerator();
	}

	public IEnumerator<Ref<T1, T2, T3, T4>> GetEnumerator()
	{
		return refs.GetEnumerator();
	}
}
[Serializable]
public class RefArray<T1, T2, T3, T4, T5> : IEnumerable<Ref<T1, T2, T3, T4, T5>>, IEnumerable where T1 : UnityEngine.Object where T2 : UnityEngine.Object where T3 : UnityEngine.Object where T4 : UnityEngine.Object where T5 : UnityEngine.Object
{
	[SerializeField]
	private List<Ref<T1, T2, T3, T4, T5>> refs;

	public int Length => refs.Count;

	public Ref<T1, T2, T3, T4, T5> this[int i] => refs[i];

	public T1 Get<T>(int index, int dummy = 0) where T : T1
	{
		return refs[index].Get<T1>(0);
	}

	public T2 Get<T>(int index, float dummy = 0f) where T : T2
	{
		return refs[index].Get<T2>(0f);
	}

	public T3 Get<T>(int index, uint dummy = 0u) where T : T3
	{
		return refs[index].Get<T3>(0u);
	}

	public T4 Get<T>(int index, short dummy = 0) where T : T4
	{
		return refs[index].Get<T4>((short)0);
	}

	public T5 Get<T>(int index, byte dummy = 0) where T : T5
	{
		return refs[index].Get<T5>((byte)0);
	}

	public bool Contains<T>(T item, int dummy = 0) where T : T1
	{
		return refs.Any((Ref<T1, T2, T3, T4, T5> r) => r.Get<T1>(0) == item);
	}

	public bool Contains<T>(T item, float dummy = 0f) where T : T2
	{
		return refs.Any((Ref<T1, T2, T3, T4, T5> r) => r.Get<T2>(0f) == item);
	}

	public bool Contains<T>(T item, uint dummy = 0u) where T : T3
	{
		return refs.Any((Ref<T1, T2, T3, T4, T5> r) => r.Get<T3>(0u) == item);
	}

	public bool Contains<T>(T item, short dummy = 0) where T : T4
	{
		return refs.Any((Ref<T1, T2, T3, T4, T5> r) => r.Get<T4>((short)0) == item);
	}

	public bool Contains<T>(T item, byte dummy = 0) where T : T5
	{
		return refs.Any((Ref<T1, T2, T3, T4, T5> r) => r.Get<T5>((byte)0) == item);
	}

	public int IndexOf<T>(T item, int dummy = 0) where T : T1
	{
		return refs.FindIndex((Ref<T1, T2, T3, T4, T5> r) => r.Get<T1>(0) == item);
	}

	public int IndexOf<T>(T item, float dummy = 0f) where T : T2
	{
		return refs.FindIndex((Ref<T1, T2, T3, T4, T5> r) => r.Get<T2>(0f) == item);
	}

	public int IndexOf<T>(T item, uint dummy = 0u) where T : T3
	{
		return refs.FindIndex((Ref<T1, T2, T3, T4, T5> r) => r.Get<T3>(0u) == item);
	}

	public int IndexOf<T>(T item, short dummy = 0) where T : T4
	{
		return refs.FindIndex((Ref<T1, T2, T3, T4, T5> r) => r.Get<T4>((short)0) == item);
	}

	public int IndexOf<T>(T item, byte dummy = 0) where T : T5
	{
		return refs.FindIndex((Ref<T1, T2, T3, T4, T5> r) => r.Get<T5>((byte)0) == item);
	}

	public int FindIndex<T>(Predicate<T1> match, int dummy = 0) where T : T1
	{
		return refs.FindIndex((Ref<T1, T2, T3, T4, T5> r) => match(r.Get<T1>(0)));
	}

	public int FindIndex<T>(Predicate<T2> match, float dummy = 0f) where T : T2
	{
		return refs.FindIndex((Ref<T1, T2, T3, T4, T5> r) => match(r.Get<T2>(0f)));
	}

	public int FindIndex<T>(Predicate<T3> match, uint dummy = 0u) where T : T3
	{
		return refs.FindIndex((Ref<T1, T2, T3, T4, T5> r) => match(r.Get<T3>(0u)));
	}

	public int FindIndex<T>(Predicate<T4> match, short dummy = 0) where T : T4
	{
		return refs.FindIndex((Ref<T1, T2, T3, T4, T5> r) => match(r.Get<T4>((short)0)));
	}

	public int FindIndex<T>(Predicate<T5> match, byte dummy = 0) where T : T5
	{
		return refs.FindIndex((Ref<T1, T2, T3, T4, T5> r) => match(r.Get<T5>((byte)0)));
	}

	public T1[] ToArray<T>(int dummy = 0) where T : T1
	{
		return RefArrayUtils.ToArray(Length, (int index) => refs[index].Get<T1>(0));
	}

	public T2[] ToArray<T>(float dummy = 0f) where T : T2
	{
		return RefArrayUtils.ToArray(Length, (int index) => refs[index].Get<T2>(0f));
	}

	public T3[] ToArray<T>(uint dummy = 0u) where T : T3
	{
		return RefArrayUtils.ToArray(Length, (int index) => refs[index].Get<T3>(0u));
	}

	public T4[] ToArray<T>(short dummy = 0) where T : T4
	{
		return RefArrayUtils.ToArray(Length, (int index) => refs[index].Get<T4>((short)0));
	}

	public T5[] ToArray<T>(byte dummy = 0) where T : T5
	{
		return RefArrayUtils.ToArray(Length, (int index) => refs[index].Get<T5>((byte)0));
	}

	public IEnumerable<T1> Iterate<T>(int dummy = 0) where T : T1
	{
		for (int i = 0; i < Length; i++)
		{
			yield return refs[i].Get<T1>(0);
		}
	}

	public IEnumerable<T2> Iterate<T>(float dummy = 0f) where T : T2
	{
		for (int i = 0; i < Length; i++)
		{
			yield return refs[i].Get<T2>(0f);
		}
	}

	public IEnumerable<T3> Iterate<T>(uint dummy = 0u) where T : T3
	{
		for (int i = 0; i < Length; i++)
		{
			yield return refs[i].Get<T3>(0u);
		}
	}

	public IEnumerable<T4> Iterate<T>(short dummy = 0) where T : T4
	{
		for (int i = 0; i < Length; i++)
		{
			yield return refs[i].Get<T4>((short)0);
		}
	}

	public IEnumerable<T5> Iterate<T>(byte dummy = 0) where T : T5
	{
		for (int i = 0; i < Length; i++)
		{
			yield return refs[i].Get<T5>((byte)0);
		}
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return GetEnumerator();
	}

	public IEnumerator<Ref<T1, T2, T3, T4, T5>> GetEnumerator()
	{
		return refs.GetEnumerator();
	}
}

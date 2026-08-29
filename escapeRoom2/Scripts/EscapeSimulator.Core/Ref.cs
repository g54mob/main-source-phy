using System;
using UnityEngine;

[Serializable]
public class Ref<T1> where T1 : UnityEngine.Object
{
	public const string refGameObjectFieldName = "refGameObject";

	[SerializeField]
	private GameObject refGameObject;

	[SerializeField]
	protected T1 ref1;

	public T1 Get<T>(int dummy = 0) where T : T1
	{
		return ref1;
	}

	public static implicit operator T1(Ref<T1> @ref)
	{
		return @ref.Get<T1>();
	}

	public void Deconstruct(out T1 r1)
	{
		r1 = ref1;
	}

	internal Ref(T1 ref1)
	{
		this.ref1 = ref1;
	}
}
[Serializable]
public class Ref<T1, T2> : Ref<T1> where T1 : UnityEngine.Object where T2 : UnityEngine.Object
{
	[SerializeField]
	protected T2 ref2;

	public new T1 Get<T>(int dummy = 0) where T : T1
	{
		return ref1;
	}

	public T2 Get<T>(float dummy = 0f) where T : T2
	{
		return ref2;
	}

	public static implicit operator T2(Ref<T1, T2> @ref)
	{
		return @ref.Get<T2>(0f);
	}

	public void Deconstruct(out T1 r1, out T2 r2)
	{
		T1 val = ref1;
		T2 val2 = ref2;
		r1 = val;
		r2 = val2;
	}

	internal Ref(T1 ref1, T2 ref2)
		: base(ref1)
	{
		this.ref2 = ref2;
	}
}
[Serializable]
public class Ref<T1, T2, T3> : Ref<T1, T2> where T1 : UnityEngine.Object where T2 : UnityEngine.Object where T3 : UnityEngine.Object
{
	[SerializeField]
	protected T3 ref3;

	public new T1 Get<T>(int dummy = 0) where T : T1
	{
		return ref1;
	}

	public new T2 Get<T>(float dummy = 0f) where T : T2
	{
		return ref2;
	}

	public T3 Get<T>(uint dummy = 0u) where T : T3
	{
		return ref3;
	}

	public static implicit operator T3(Ref<T1, T2, T3> @ref)
	{
		return @ref.Get<T3>(0u);
	}

	public void Deconstruct(out T1 r1, out T2 r2, out T3 r3)
	{
		T1 val = ref1;
		T2 val2 = ref2;
		T3 val3 = ref3;
		r1 = val;
		r2 = val2;
		r3 = val3;
	}

	internal Ref(T1 ref1, T2 ref2, T3 ref3)
		: base(ref1, ref2)
	{
		this.ref3 = ref3;
	}
}
[Serializable]
public class Ref<T1, T2, T3, T4> : Ref<T1, T2, T3> where T1 : UnityEngine.Object where T2 : UnityEngine.Object where T3 : UnityEngine.Object where T4 : UnityEngine.Object
{
	[SerializeField]
	protected T4 ref4;

	public new T1 Get<T>(int dummy = 0) where T : T1
	{
		return ref1;
	}

	public new T2 Get<T>(float dummy = 0f) where T : T2
	{
		return ref2;
	}

	public new T3 Get<T>(uint dummy = 0u) where T : T3
	{
		return ref3;
	}

	public T4 Get<T>(short dummy = 0) where T : T4
	{
		return ref4;
	}

	public static implicit operator T4(Ref<T1, T2, T3, T4> @ref)
	{
		return @ref.Get<T4>((short)0);
	}

	public void Deconstruct(out T1 r1, out T2 r2, out T3 r3, out T4 r4)
	{
		T1 val = ref1;
		T2 val2 = ref2;
		T3 val3 = ref3;
		T4 val4 = ref4;
		r1 = val;
		r2 = val2;
		r3 = val3;
		r4 = val4;
	}

	internal Ref(T1 ref1, T2 ref2, T3 ref3, T4 ref4)
		: base(ref1, ref2, ref3)
	{
		this.ref4 = ref4;
	}
}
[Serializable]
public class Ref<T1, T2, T3, T4, T5> : Ref<T1, T2, T3, T4> where T1 : UnityEngine.Object where T2 : UnityEngine.Object where T3 : UnityEngine.Object where T4 : UnityEngine.Object where T5 : UnityEngine.Object
{
	[SerializeField]
	protected T5 ref5;

	public new T1 Get<T>(int dummy = 0) where T : T1
	{
		return ref1;
	}

	public new T2 Get<T>(float dummy = 0f) where T : T2
	{
		return ref2;
	}

	public new T3 Get<T>(uint dummy = 0u) where T : T3
	{
		return ref3;
	}

	public new T4 Get<T>(short dummy = 0) where T : T4
	{
		return ref4;
	}

	public T5 Get<T>(byte dummy = 0) where T : T5
	{
		return ref5;
	}

	public static implicit operator T5(Ref<T1, T2, T3, T4, T5> @ref)
	{
		return @ref.Get<T5>((byte)0);
	}

	public void Deconstruct(out T1 r1, out T2 r2, out T3 r3, out T4 r4, out T5 r5)
	{
		T1 val = ref1;
		T2 val2 = ref2;
		T3 val3 = ref3;
		T4 val4 = ref4;
		T5 val5 = ref5;
		r1 = val;
		r2 = val2;
		r3 = val3;
		r4 = val4;
		r5 = val5;
	}

	internal Ref(T1 ref1, T2 ref2, T3 ref3, T4 ref4, T5 ref5)
		: base(ref1, ref2, ref3, ref4)
	{
		this.ref5 = ref5;
	}
}

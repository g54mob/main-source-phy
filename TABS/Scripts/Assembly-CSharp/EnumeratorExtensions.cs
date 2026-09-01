using System;
using System.Collections;

public static class EnumeratorExtensions
{
	public static IEnumerator[] Clone(this IEnumerator enumerator, int clones)
	{
		if (enumerator == null)
		{
			throw new ArgumentNullException("enumerator");
		}
		if (clones < 2)
		{
			throw new ArgumentOutOfRangeException("clones");
		}
		ClonedEnumerator.EnumeratorWrapper enumerator2 = new ClonedEnumerator.EnumeratorWrapper
		{
			Enumerator = enumerator,
			Clones = clones
		};
		ClonedEnumerator.Node firstNode = new ClonedEnumerator.Node
		{
			Value = enumerator.Current,
			Next = null
		};
		IEnumerator[] array = new IEnumerator[clones];
		for (int i = 0; i < clones; i++)
		{
			array[i] = new ClonedEnumerator(enumerator2, firstNode);
		}
		return array;
	}
}

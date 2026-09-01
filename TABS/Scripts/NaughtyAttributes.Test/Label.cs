using System;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.AI;

public class Label : MonoBehaviour
{
	[Serializable]
	public class MyClassExample
	{
		public int aInt;

		public string aString;
	}

	[Label("A Short Name")]
	public string aMoreSpecificName;

	[Label("RGB")]
	public Vector3 vectorXYZ;

	[Label("Agent")]
	public NavMeshAgent navMeshAgent;

	[Label("Ints")]
	public int[] arrayOfInts;

	[Label("Custom Class")]
	public MyClassExample myClass;
}

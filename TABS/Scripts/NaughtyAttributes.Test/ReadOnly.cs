using NaughtyAttributes;
using UnityEngine;

public class ReadOnly : MonoBehaviour
{
	[ReadOnly]
	public int readOnlyInt = 5;
}

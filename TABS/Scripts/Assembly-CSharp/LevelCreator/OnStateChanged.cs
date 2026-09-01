using System;
using UnityEngine.Events;

namespace LevelCreator
{
	[Serializable]
	public class OnStateChanged : UnityEvent<bool>
	{
	}
}

using InControl;
using UnityEngine;
using UnityEngine.Events;

namespace LevelCreator
{
	public struct InputKey
	{
		public KeyCode keyCode;

		public PlayerAction playerAction;

		public UnityEvent onKeyDown;

		public UnityEvent onKeyUp;

		public string description;

		public Sprite contextIcon;
	}
}

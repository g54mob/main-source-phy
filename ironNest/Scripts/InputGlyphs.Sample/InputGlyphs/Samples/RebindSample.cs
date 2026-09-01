using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace InputGlyphs.Samples
{
	public class RebindSample : MonoBehaviour
	{
		public PlayerInput PlayerInput;

		public InputActionReference ActionReference;

		public UnityEvent OnComplete;

		private InputActionRebindingExtensions.RebindingOperation _rebindOp;

		private bool _enableActionAfterRebind;

		private int _rebindingIndex;

		private static readonly List<int> _bindingIndexBuffer;

		private void OnDisable()
		{
		}

		public void Rebind()
		{
		}

		private void OnCompleteBinding(InputActionRebindingExtensions.RebindingOperation op)
		{
		}
	}
}

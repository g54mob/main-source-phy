using System;
using System.Collections.Generic;
using InputGlyphs.Display;
using UnityEngine;
using UnityEngine.InputSystem;

namespace InputGlyphs.Samples
{
	[AddComponentMenu(null)]
	public class InputCheckSample : MonoBehaviour
	{
		[SerializeField]
		private SpriteRenderer _spriteRenderer;

		private IDisposable _callOnce;

		private Texture2D _texture;

		private List<InputControl> _controlBuffer;

		private List<InputDevice> _deviceBuffer;

		private List<string> _pathBuffer;

		private readonly GlyphsLayoutData _layoutData;

		private void Start()
		{
		}

		private void DrawGlyphs(IReadOnlyList<InputControl> controls)
		{
		}

		private void OnDestroy()
		{
		}
	}
}

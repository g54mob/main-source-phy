using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace InputGlyphs.Display
{
	public class InputGlyphSprite : MonoBehaviour
	{
		[SerializeField]
		public SpriteRenderer SpriteRenderer;

		[SerializeField]
		public PlayerInput PlayerInput;

		[SerializeField]
		public InputActionReference InputActionReference;

		[SerializeField]
		public GlyphsLayoutData GlyphsLayoutData;

		private PlayerInput _lastPlayerInput;

		private List<string> _pathBuffer;

		private Texture2D _texture;

		private void Reset()
		{
		}

		private void Awake()
		{
		}

		private void Start()
		{
		}

		private void OnDisable()
		{
		}

		private void OnDestroy()
		{
		}

		private void Update()
		{
		}

		private void RegisterPlayerInputEvents(PlayerInput playerInput)
		{
		}

		private void UnregisterPlayerInputEvents(PlayerInput playerInput)
		{
		}

		private void OnControlsChanged(PlayerInput playerInput)
		{
		}

		public void UpdateGlyphs()
		{
		}

		private void UpdateGlyphs(PlayerInput playerInput)
		{
		}
	}
}

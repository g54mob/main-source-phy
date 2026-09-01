using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace InputGlyphs.Display
{
	public class InputGlyphImage : UIBehaviour, ILayoutElement
	{
		[SerializeField]
		public Image Image;

		[SerializeField]
		public PlayerInput PlayerInput;

		[SerializeField]
		public InputActionReference InputActionReference;

		[SerializeField]
		public GlyphsLayoutData GlyphsLayoutData;

		private Vector2 _defaultSizeDelta;

		private PlayerInput _lastPlayerInput;

		private List<string> _pathBuffer;

		private Texture2D _texture;

		private Sprite _createdSprite;

		[SerializeField]
		public bool EnableLayoutElement;

		[SerializeField]
		public int LayoutElementPriority;

		[SerializeField]
		public float LayoutElementSize;

		public virtual int layoutPriority => 0;

		public virtual float minWidth => 0f;

		public virtual float minHeight => 0f;

		public virtual float preferredWidth => 0f;

		public virtual float preferredHeight => 0f;

		public virtual float flexibleWidth => 0f;

		public virtual float flexibleHeight => 0f;

		protected override void Awake()
		{
		}

		protected override void Start()
		{
		}

		protected override void OnDisable()
		{
		}

		protected override void OnDestroy()
		{
		}

		protected virtual void Update()
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

		public virtual void CalculateLayoutInputHorizontal()
		{
		}

		public virtual void CalculateLayoutInputVertical()
		{
		}

		protected void SetDirty()
		{
		}
	}
}

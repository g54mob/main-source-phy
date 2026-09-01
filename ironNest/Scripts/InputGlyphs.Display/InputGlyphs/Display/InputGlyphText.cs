using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace InputGlyphs.Display
{
	public class InputGlyphText : MonoBehaviour
	{
		private class ActionTextureInfo
		{
			public readonly List<string> Paths;

			public Texture2D Texture;

			public int Index;
		}

		public static int PackedTextureSize;

		[SerializeField]
		public TMP_Text Text;

		[SerializeField]
		[HideInInspector]
		public Material Material;

		[SerializeField]
		public PlayerInput PlayerInput;

		[SerializeField]
		public InputActionReference[] InputActionReferences;

		[SerializeField]
		public GlyphsLayoutData GlyphsLayoutData;

		private readonly Dictionary<string, ActionTextureInfo> _actionTextureInfos;

		private static int MainTexProperty;

		private PlayerInput _lastPlayerInput;

		private Texture2D _packedTexture;

		private Material _sharedMaterial;

		private TMP_SpriteAsset _sharedSpriteAsset;

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

		private bool ShouldGenerateTexture(string actionName, List<string> bindingPaths, out ActionTextureInfo actionTextureInfo)
		{
			actionTextureInfo = null;
			return false;
		}

		private void SetGlyphsToSpriteAsset()
		{
		}

		private static TMP_SpriteAsset CreateEmptySpriteAsset()
		{
			return null;
		}

		private static void SetSpriteAssetVersion(TMP_SpriteAsset spriteAsset, string version)
		{
		}
	}
}

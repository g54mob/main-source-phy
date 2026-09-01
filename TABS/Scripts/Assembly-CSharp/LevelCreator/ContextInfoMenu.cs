using InControl;
using TFBGames;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LevelCreator
{
	public class ContextInfoMenu : MonoBehaviour
	{
		private struct InstancedContextKey
		{
			public GameObject m_instance;

			public Image m_iconImage;

			public DMActionGlyph m_actionGlyph;

			public void Activate(Sprite icon, PlayerAction action, DMActionGlyphPlatformSpecificOverride[] platformOverrides)
			{
				if (m_instance != null && m_instance.gameObject != null)
				{
					m_instance.gameObject.SetActive(value: true);
				}
				if (m_iconImage != null)
				{
					m_iconImage.sprite = icon;
				}
				if (m_actionGlyph != null)
				{
					m_actionGlyph.SetPlatformOverrides(platformOverrides);
					m_actionGlyph.SetAction(action.Name);
				}
			}

			public void Deactivate()
			{
				if (m_instance != null)
				{
					m_instance.gameObject.SetActive(value: false);
				}
				if (m_actionGlyph != null)
				{
					m_actionGlyph.SetPlatformOverrides(null);
				}
			}
		}

		private InstancedContextKey[] m_instancedKeys;

		private int m_instanceKeyIndex;

		private DMActionGlyphPlatformSpecificOverride[] platformOverrides;

		[SerializeField]
		private GameObject m_contextInfoKeyPrefab;

		private void Awake()
		{
			platformOverrides = GetComponents<DMActionGlyphPlatformSpecificOverride>();
		}

		private void InitKeyPool()
		{
			m_instancedKeys = new InstancedContextKey[50];
			for (int i = 0; i < m_instancedKeys.Length; i++)
			{
				GameObject gameObject = Object.Instantiate(m_contextInfoKeyPrefab, base.transform);
				DMActionGlyph actionGlyph = gameObject.GetComponentInChildren<TextMeshProUGUI>().gameObject.AddComponent<DMActionGlyph>();
				Image iconImage = gameObject.GetComponentsInChildren<Image>()[1];
				m_instancedKeys[i] = new InstancedContextKey
				{
					m_instance = gameObject,
					m_iconImage = iconImage,
					m_actionGlyph = actionGlyph
				};
				m_instancedKeys[i].Deactivate();
			}
		}

		private InstancedContextKey GetKeyInstance()
		{
			if (m_instancedKeys == null)
			{
				InitKeyPool();
			}
			InstancedContextKey result = m_instancedKeys[m_instanceKeyIndex];
			m_instanceKeyIndex = Mathf.Clamp(m_instanceKeyIndex + 1, 0, m_instancedKeys.Length - 1);
			return result;
		}

		private void ReturnKeyInstance(InstancedContextKey key)
		{
			key.Deactivate();
			m_instanceKeyIndex = Mathf.Clamp(m_instanceKeyIndex - 1, 0, m_instancedKeys.Length - 1);
		}

		private void ReturnAllKeyInstances()
		{
			int instanceKeyIndex = m_instanceKeyIndex;
			for (int i = 0; i < instanceKeyIndex; i++)
			{
				ReturnKeyInstance(m_instancedKeys[i]);
			}
		}

		public void ReplaceContextKeys(bool displayInputStateActions = true)
		{
			ReturnAllKeyInstances();
			if (displayInputStateActions)
			{
				InputState inputState = InputManager.PeekState();
				for (int i = 0; i < inputState.Keys.Count; i++)
				{
					InputKey inputKey = inputState.Keys[i];
					AddContextKey(inputKey.playerAction, inputKey.description, inputKey.contextIcon);
				}
			}
		}

		public void AddContextKey(PlayerAction playerAction, string description)
		{
			AddContextKey(playerAction, description, null);
		}

		public void AddContextKey(PlayerAction playerAction, Sprite contextIcon)
		{
			AddContextKey(playerAction, null, contextIcon);
		}

		public void AddContextKey(PlayerAction playerAction, string description, Sprite contextIcon)
		{
			if ((!string.IsNullOrEmpty(description) || !(contextIcon == null)) && !(playerAction.Bindings[0].Name == "None"))
			{
				GetKeyInstance().Activate(contextIcon, playerAction, platformOverrides);
			}
		}
	}
}

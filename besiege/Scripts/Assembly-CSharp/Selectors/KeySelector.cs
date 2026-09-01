using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Selectors
{
	public class KeySelector : Selector
	{
		public enum eKeyAlign
		{
			Center = 0,
			Left = 1,
			Right = 2
		}

		public static int MaxKeys = 3;

		[SerializeField]
		protected eKeyAlign keyAlign;

		[SerializeField]
		protected DynamicText title;

		[SerializeField]
		protected Transform hoverText;

		[SerializeField]
		protected KeySelectorExtender keyPrefab;

		[SerializeField]
		protected List<KeySelectorExtender> keys = new List<KeySelectorExtender>();

		[SerializeField]
		protected TagSelector variableSelector;

		[SerializeField]
		protected float spacer = 0.9f;

		public MeshRenderer BG;

		public UIButton addButton;

		public UIButton messageToggleOn;

		public Tooltip messageOnTooltip;

		public UIButton messageToggleOff;

		public Tooltip messageOffTooltip;

		public UIButtonExtended ignoreButton;

		public Tooltip ignoreTooltip;

		public MeshRenderer emulateIcon;

		public Texture ignoreIcon;

		public Texture unignoreIcon;

		public Transform container;

		public Transform addAndText;

		public bool isLogic;

		public bool moveHoverText = true;

		public float keyVerticalOffset = -0.15f;

		private bool updateCallback;

		private float top;

		private float distanceToTop;

		private bool inConflict;

		protected KeyCode lastKeyInput;

		protected int lastIndexChanged = -1;

		protected static List<BlockBehaviour> affected = new List<BlockBehaviour>();

		protected static SaveableDataHolder affectedHolder = null;

		private MKey _key;

		public List<KeySelectorExtender> Keys
		{
			get
			{
				return keys;
			}
		}

		public int KeyObjCount
		{
			get
			{
				return keys.Count;
			}
		}

		public MKey Key
		{
			get
			{
				return _key;
			}
			set
			{
				if (updateCallback)
				{
					if (_key != null)
					{
						_key.KeysChanged -= Rebuild;
					}
					updateCallback = false;
				}
				_key = value;
				MapperType = _key;
				if (_key != null)
				{
					_key.KeysChanged += Rebuild;
					updateCallback = true;
				}
			}
		}

		protected void Awake()
		{
			if (addButton != null)
			{
				addButton.Down += Add;
			}
			if (messageToggleOn != null)
			{
				messageToggleOn.Down += ToggleVar;
			}
			if (messageToggleOff != null)
			{
				messageToggleOff.Down += ToggleVar;
			}
			if (ignoreButton != null)
			{
				ignoreButton.Down += Ignore;
			}
			if (variableSelector != null)
			{
				variableSelector.FocusChange += OnFocusChange;
				variableSelector.TagsChanged += SetVariable;
				variableSelector.HeightChanged += OnVariableSelectorHeightChanged;
				variableSelector.GetAutocompleteItems = () => BlockMapper.CurrentInstance.AllVariables;
			}
			ReferenceMaster.onAdvancedBuildingToggled = (Action)Delegate.Combine(ReferenceMaster.onAdvancedBuildingToggled, new Action(UpdateMessageOption));
			for (int num = 0; num < keys.Count; num++)
			{
				keys[num].SetUp(this, hoverText, num, KeyCode.None);
				ChangeKey(num, KeyCode.None);
			}
			AlignKeys();
			if ((bool)variableSelector)
			{
				top = BG.transform.localPosition.y + BG.transform.localScale.y * 0.5f;
				distanceToTop = top - variableSelector.transform.localPosition.y;
				variableSelector.Hovered += OnVariableHovered;
			}
		}

		public void OnFocusChange(bool focused)
		{
			if (focused)
			{
				SaveableDataHolder current = BlockMapper.CurrentInstance.Current;
				if (current is BlockBehaviour)
				{
					affected = new List<BlockBehaviour>(AdvancedBlockEditor.Instance.selectionController.MachineSelection);
					affectedHolder = current;
				}
			}
		}

		public void TerminateVariable()
		{
			if (variableSelector != null && variableSelector.IsFocused)
			{
				variableSelector.Terminate();
				Key.message = variableSelector.GetTags().ToArray();
				if (affectedHolder != null && affected.Count != 0)
				{
					BlockMapper.EditField(affected, affectedHolder, MapperType);
				}
				else
				{
					Debug.Log("[Keyselector] Tried editing unassigned field");
				}
			}
		}

		public void TerminateCleanup()
		{
			affectedHolder = null;
			affected.Clear();
		}

		private void SetVariable(string[] s)
		{
			Key.message = s.ToArray();
			OnEdit();
		}

		protected virtual void Ignore()
		{
			if (Key != null)
			{
				Ignore(!Key.Ignored);
			}
		}

		public virtual void Ignore(bool toggle)
		{
			if (Key != null && Key.Ignored != toggle)
			{
				Key.SetIgnored(toggle);
				UpdateIgnoreVisual();
				OnEdit();
			}
		}

		public virtual void UpdateIgnoreVisual()
		{
			if (Key == null)
			{
				return;
			}
			if (Key.isEmulator)
			{
				if ((bool)emulateIcon)
				{
					emulateIcon.gameObject.SetActive(true);
				}
				if (ignoreButton != null)
				{
					ignoreButton.gameObject.SetActive(false);
				}
				return;
			}
			if (ignoreButton != null)
			{
				ignoreButton.gameObject.SetActive(!Key.useMessage);
			}
			for (int i = 0; i < keys.Count; i++)
			{
				keys[i].SetIgnored(Key.Ignored);
			}
			if (ignoreButton != null)
			{
				ignoreButton.icon.material.mainTexture = ((!Key.Ignored) ? ignoreIcon : unignoreIcon);
			}
		}

		public override void Init()
		{
			if (Key == null)
			{
				Debug.LogWarning("MKey has not been assigned to " + base.transform.name);
				return;
			}
			if (hoverText != null)
			{
				hoverText.gameObject.SetActive(false);
			}
			base.Init();
			if (hoverText != null)
			{
				hoverText.gameObject.SetActive(false);
			}
			Key.RemoveRedundant();
			Rebuild();
			if (title != null)
			{
				title.SetText(Key.DisplayName.ToUpper());
			}
			if (ignoreButton != null && OverviewBlockMapper.CurrentInstance == null)
			{
				float num = ignoreButton.transform.lossyScale.x * 0.75f;
				ignoreButton.transform.position = new Vector3(title.transform.TransformPoint(title.bounds.max).x + num, ignoreButton.transform.position.y, ignoreButton.transform.position.z);
				if ((bool)emulateIcon)
				{
					emulateIcon.transform.position = new Vector3(title.transform.TransformPoint(title.bounds.min).x - num * 1.2f, emulateIcon.transform.position.y, emulateIcon.transform.position.z);
				}
				if (messageToggleOn != null)
				{
					num = messageToggleOn.transform.lossyScale.x;
					messageToggleOn.transform.position = ignoreButton.transform.position + Vector3.right * num;
				}
				if (messageToggleOff != null)
				{
					messageToggleOff.transform.position = ignoreButton.transform.position;
				}
				CenterText(0.75f);
			}
			if (variableSelector != null)
			{
				variableSelector.Terminate();
				variableSelector.SetTags(Key.message);
				if (Key.VariableSelectorHeight.HasValue)
				{
					variableSelector.SetTags(Key.message, Key.VariableSelectorHeight.Value);
					ExpandBackgroundToMapperHeight();
				}
				else
				{
					variableSelector.SetTags(Key.message);
				}
				ToggleEmulationMessage(Key.useMessage);
			}
			UpdateMessageOption();
		}

		public void CenterText(float bias)
		{
			float x2;
			float x;
			float num;
			float num2;
			if (Key.isEmulator)
			{
				if (!emulateIcon)
				{
					title.transform.localPosition = new Vector3(0f, title.transform.localPosition.y, title.transform.localPosition.z);
					return;
				}
				x = title.transform.TransformPoint(title.bounds.max).x;
				x2 = emulateIcon.bounds.min.x;
				num = (x2 + x) / 2f;
				num2 = (title.transform.parent.position.x - num) * bias;
				title.transform.position += Vector3.right * num2;
				emulateIcon.transform.position += Vector3.right * num2;
				float num3 = (x - x2) / 2f;
				Vector3 position = title.transform.position;
				if (messageToggleOn != null)
				{
					position.z = messageToggleOn.transform.position.z;
					float num4 = messageToggleOn.transform.lossyScale.x + num3;
					messageToggleOn.transform.position = position + Vector3.right * num4 * 0.75f;
				}
				if (messageToggleOff != null)
				{
					position.z = messageToggleOn.transform.position.z;
					num3 = messageToggleOn.transform.lossyScale.x + num3;
					messageToggleOff.transform.position = position + Vector3.right * num3 * 0.75f;
				}
				return;
			}
			x2 = title.transform.TransformPoint(title.bounds.min).x;
			x = ignoreButton.icon.bounds.max.x;
			num = (x2 + x) / 2f;
			num2 = (title.transform.parent.position.x - num) * bias * 0.75f;
			title.transform.position += Vector3.right * num2;
			ignoreButton.transform.position += Vector3.right * num2;
			if ((bool)ignoreTooltip)
			{
				ignoreTooltip.tooltipParentStartPos = ignoreTooltip.tooltipParentStartPos.WithX(ignoreButton.transform.localPosition.x);
			}
			if (messageToggleOn != null)
			{
				float x3 = messageToggleOn.transform.lossyScale.x;
				messageToggleOn.transform.position = ignoreButton.transform.position + Vector3.right * x3;
				if ((bool)messageOnTooltip)
				{
					messageOnTooltip.tooltipParentStartPos = messageOnTooltip.tooltipParentStartPos.WithX(messageToggleOn.transform.localPosition.x);
				}
			}
			if (messageToggleOff != null)
			{
				messageToggleOff.transform.position = ignoreButton.transform.position;
				if ((bool)messageOffTooltip)
				{
					messageOffTooltip.tooltipParentStartPos = messageOffTooltip.tooltipParentStartPos.WithX(messageToggleOff.transform.localPosition.x);
				}
			}
		}

		protected virtual void Add()
		{
			Add(KeyCode.None);
		}

		protected KeySelectorExtender CreateKey(int index, KeyCode k)
		{
			KeySelectorExtender keySelectorExtender = UnityEngine.Object.Instantiate(keyPrefab, container) as KeySelectorExtender;
			keySelectorExtender.transform.localPosition = GetKeyOffset(index);
			return keySelectorExtender;
		}

		protected void Add(KeyCode k)
		{
			int count = keys.Count;
			KeySelectorExtender keySelectorExtender = CreateKey(count, k);
			keys.Add(keySelectorExtender);
			keySelectorExtender.SetUp(this, hoverText, count, k);
			ChangeKey(count, k);
			AlignKeys();
			keySelectorExtender.SetConflict(inConflict);
			keySelectorExtender.Hovered += OnKeysHovered;
		}

		protected virtual Vector3 GetKeyOffset(int index)
		{
			return new Vector3((float)index * (keyPrefab.transform.localScale.x + 0.1f) * (float)((keyAlign != eKeyAlign.Right) ? 1 : (-1)), keyVerticalOffset, 0f);
		}

		protected virtual Vector3 GetAddOffset()
		{
			return GetKeyOffset(keys.Count - 1) + container.transform.localPosition;
		}

		protected virtual void OnModifyKey(int index, KeyCode keycode)
		{
		}

		protected override void UpdateVisual()
		{
		}

		private void Rebuild()
		{
			Clear();
			inConflict = InConflict();
			bool flag = Key.KeysCount <= 0;
			for (int i = 0; i < Key.KeysCount; i++)
			{
				Add(Key.GetKey(i));
			}
			if (flag)
			{
				Add(KeyCode.None);
			}
			if ((bool)variableSelector)
			{
				variableSelector.SetConflict(inConflict);
			}
			UpdateIgnoreVisual();
			ToggleEmulationMessage(Key.useMessage);
			if (Key.isEmulator && BG != null)
			{
				Color color = BG.sharedMaterial.GetColor("_TintColor");
				BG.material.SetColor("_TintColor", new Color(color.r, color.g, color.b, 0.141f));
			}
		}

		protected void AlignKeys()
		{
			switch (keyAlign)
			{
			case eKeyAlign.Center:
				CenterKeys();
				break;
			case eKeyAlign.Left:
				LeftAlignKeys();
				break;
			}
		}

		protected void CenterKeys()
		{
			if (keyAlign != eKeyAlign.Center)
			{
				return;
			}
			container.transform.localPosition = new Vector3(((float)keys.Count - 1f) * (0f - spacer) * 0.5f, 0f, container.transform.localPosition.z);
			bool flag = keys.Count >= MaxKeys;
			for (int num = keys.Count - 1; num >= 0; num--)
			{
				if (keys[num].myKey == KeyCode.None)
				{
					flag = true;
					break;
				}
			}
			if (addButton != null)
			{
				addButton.gameObject.SetActive(!flag);
			}
			if (moveHoverText)
			{
				hoverText.transform.localPosition = new Vector3((!flag) ? 1.175f : 0.837f, hoverText.transform.localPosition.y, hoverText.transform.localPosition.z);
			}
			if (addAndText != null)
			{
				addAndText.transform.localPosition = new Vector3(((float)keys.Count - 1f) * spacer * 0.5f, keyVerticalOffset, 0f);
			}
		}

		protected virtual void LeftAlignKeys()
		{
			bool flag = keys.Count >= MaxKeys;
			for (int num = keys.Count - 1; num >= 0; num--)
			{
				if (keys[num].myKey == KeyCode.None)
				{
					flag = true;
					break;
				}
			}
			if (addButton != null)
			{
				addButton.gameObject.SetActive(!flag);
			}
			if (moveHoverText)
			{
				hoverText.transform.localPosition = new Vector3((!flag) ? 1.175f : 0.837f, hoverText.transform.localPosition.y, hoverText.transform.localPosition.z);
			}
			if (addAndText != null)
			{
				addAndText.transform.localPosition = GetAddOffset();
			}
		}

		protected virtual void Clear()
		{
			foreach (KeySelectorExtender key in keys)
			{
				UnityEngine.Object.Destroy(key.gameObject);
			}
			keys.Clear();
		}

		public virtual int ChangeKey(int index, KeyCode keyCode)
		{
			return ChangeKey(index, keyCode, false);
		}

		public virtual int ChangeKey(int index, KeyCode keyCode, bool allowSameKey)
		{
			int result = -1;
			if (Key != null && (Key.GetKey(index) != keyCode || allowSameKey))
			{
				result = Key.AddOrReplaceKey(index, keyCode);
				UpdateVisual();
				lastKeyInput = keyCode;
				lastIndexChanged = index;
				OnEdit();
			}
			return result;
		}

		public virtual void RemoveKey(int index)
		{
			if (Key != null && index < keys.Count)
			{
				if (keys.Count <= 1)
				{
					Key.AddOrReplaceKey(index, KeyCode.None);
				}
				else
				{
					Key.RemoveKey(index);
				}
				Rebuild();
				OnEdit();
			}
		}

		public virtual void RemoveAllKeys()
		{
			if (Key == null)
			{
				return;
			}
			for (int num = Key.KeysCount - 1; num >= 0; num--)
			{
				if (num == 0)
				{
					Key.AddOrReplaceKey(0, KeyCode.None);
				}
				else
				{
					Key.RemoveKey(num);
				}
				OnEdit();
			}
			Rebuild();
		}

		public virtual void UpdateAll()
		{
			Rebuild();
			OnEdit();
		}

		protected void OnDisable()
		{
			if (variableSelector != null)
			{
				variableSelector.Terminate();
			}
			if (updateCallback)
			{
				if (Key != null)
				{
					Key.KeysChanged -= Rebuild;
				}
				updateCallback = false;
			}
		}

		public static string[] KeyCodeToDisplay(KeyCode key)
		{
			string text = key.ToString();
			if (text.StartsWith("F") && text.Length <= 3)
			{
				return new string[1] { text };
			}
			if (text.StartsWith("Alpha") && text.Length == 6)
			{
				return new string[1] { text.Remove(0, 5) };
			}
			Match match = Regex.Match(text, "Joystick([0-9])?Button([0-9]+)");
			if (match.Success)
			{
				if (!match.Groups[1].Success)
				{
					return new string[2]
					{
						"Joystick",
						match.Groups[2].Value
					};
				}
				return new string[2]
				{
					string.Format("Joystick{0}", match.Groups[1].Value),
					match.Groups[2].Value
				};
			}
			if (text == "Backslash")
			{
				return new string[2] { "Back", "slash" };
			}
			List<char> list = new List<char>();
			List<char> list2 = new List<char>();
			bool flag = true;
			bool flag2 = false;
			for (int i = 0; i < text.Length; i++)
			{
				char c = text[i];
				if (i > 0)
				{
					if (char.IsUpper(c))
					{
						if (!flag)
						{
							break;
						}
						flag = false;
					}
					if (char.IsDigit(c))
					{
						if (!flag2)
						{
							if (!flag)
							{
								break;
							}
							flag = false;
						}
						flag2 = true;
					}
				}
				if (char.IsDigit(c))
				{
					flag2 = true;
				}
				((!flag) ? list2 : list).Add(c);
			}
			return (list2.Count == 0) ? new string[1]
			{
				new string(list.ToArray())
			} : new string[2]
			{
				new string(list.ToArray()),
				new string(list2.ToArray())
			};
		}

		protected virtual void ToggleVar()
		{
			ToggleEmulationMessage(!Key.useMessage);
		}

		private void ToggleVar(bool b, bool runOnEdit = true)
		{
			if ((bool)variableSelector && variableSelector.gameObject.activeSelf != b)
			{
				variableSelector.gameObject.SetActive(b);
			}
			if ((bool)messageToggleOff)
			{
				messageToggleOff.gameObject.SetActive(b);
			}
			if ((bool)addButton)
			{
				addButton.gameObject.SetActive(!b);
			}
			if ((bool)messageToggleOn)
			{
				messageToggleOn.gameObject.SetActive(!b && OptionsMaster.BesiegeConfig.AdvancedBuilding);
			}
			for (int i = 0; i < keys.Count; i++)
			{
				keys[i].Display(!b);
			}
			if (Key.useMessage != b)
			{
				Key.useMessage = b;
				if (runOnEdit)
				{
					OnEdit();
				}
			}
			if (variableSelector != null)
			{
				variableSelector.SetTags(Key.message);
			}
		}

		private void ToggleEmulationMessage(bool b)
		{
			if (!isLogic)
			{
				ToggleVar(b);
			}
		}

		private void UpdateMessageOption()
		{
			if (!(this == null) && !(base.gameObject == null) && base.gameObject.activeInHierarchy)
			{
				ToggleVar(Key.useMessage, false);
				UpdateIgnoreVisual();
			}
		}

		private void ExpandBackgroundToMapperHeight()
		{
			if (Key.useMessage && Key.VariableMapperHeight.HasValue)
			{
				BG.transform.localScale = BG.transform.localScale.WithY(Key.VariableMapperHeight.Value);
				float y = top - BG.transform.localScale.y * 0.5f;
				BG.transform.localPosition = BG.transform.localPosition.WithY(y);
			}
		}

		private void OnVariableSelectorHeightChanged(float variableSelectorHeight)
		{
			float num = variableSelectorHeight + distanceToTop;
			if (!Mathf.Approximately(BG.transform.localScale.y, num))
			{
				Key.VariableSelectorHeight = variableSelectorHeight;
				Key.VariableMapperHeight = num;
				BlockMapper.CurrentInstance.Rebuild();
			}
		}

		private void OnVariableHovered(bool hover)
		{
			variableSelector.SetConflict(!hover && inConflict);
		}

		private void OnKeysHovered(bool hover)
		{
			bool noneHovered = true;
			for (int i = 0; i < keys.Count; i++)
			{
				KeySelectorExtender keySelectorExtender = keys[i];
				if (keySelectorExtender.isHovered)
				{
					noneHovered = false;
					break;
				}
			}
			keys.ForEach(delegate(KeySelectorExtender x)
			{
				x.SetConflict(noneHovered && inConflict);
			});
		}
	}
}

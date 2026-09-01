using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SRF;
using Selectors.Effects;
using UnityEngine;

namespace Selectors
{
	public class TextHolderAutocomplete : TextHolder
	{
		private class Option
		{
			public GameObject gameObject;

			public MeshRenderer background;

			public DynamicText text;

			public FitDynamicTextToBackground textFitter;

			public UIHoverArea hoverArea;
		}

		public Func<IEnumerable<string>> GetItems;

		public GameObject optionContainer;

		public float minOptionsWidth = 2f;

		public Material optionNormalMaterial;

		public Material optionHoverMaterial;

		private BoxCollider collider;

		private UIHoverArea hoverArea;

		private UIHoverArea clearButtonHoverArea;

		private List<string> items;

		private int scrollPosition;

		private int? highlightedOption;

		private bool selectHighlightedOnExit;

		private bool isEmpty = true;

		private bool wasEmpty = true;

		private float initialWidth;

		private List<Option> options = new List<Option>();

		public event Action<int> Cleared;

		private void Awake()
		{
			CharLimit = StatMaster.KeyMapper.VariableCharLimit;
			collider = GetComponent<BoxCollider>();
			hoverArea = GetComponent<UIHoverArea>();
			initialWidth = BG.transform.localScale.x;
			foreach (Transform child in optionContainer.transform.GetChildren())
			{
				SimpleUIButton optionButton = child.GetComponentInChildren<SimpleUIButton>(true);
				Option option = new Option
				{
					gameObject = child.gameObject,
					background = optionButton.GetComponent<MeshRenderer>(),
					text = child.GetComponentInChildren<DynamicText>(true),
					textFitter = child.GetComponentInChildren<FitDynamicTextToBackground>(true),
					hoverArea = optionButton.GetComponent<UIHoverArea>()
				};
				optionButton.Click += delegate
				{
					OnItemSelected(option.gameObject.transform.GetSiblingIndex());
				};
				SimpleUIButton simpleUIButton = optionButton;
				simpleUIButton.HoverChanged = (Action)Delegate.Combine(simpleUIButton.HoverChanged, (Action)delegate
				{
					OptionHoverChanged(optionButton, option);
				});
				options.Add(option);
				option.gameObject.SetActive(false);
			}
			optionContainer.SetActive(false);
			base.TextInput += OnTextInput;
			base.TextChangedExternal += OnTextInput;
			base.FocusChange += OnFocusChange;
			base.ArrowKeyPressed += delegate(Direction d)
			{
				HighlightAndScrollOptions(d, true);
			};
		}

		private void OnCleared()
		{
			if (this.Cleared != null)
			{
				this.Cleared(base.transform.GetSiblingIndex());
			}
		}

		protected override void Update()
		{
			base.Update();
			if (base.IsFocused)
			{
				float num = InputManager.ActualScrollValue();
				if (num > 0f)
				{
					HighlightAndScrollOptions(Direction.Up, false);
				}
				else if (num < 0f)
				{
					HighlightAndScrollOptions(Direction.Down, false);
				}
				if (isEmpty && wasEmpty && InputManager.GetKeyDown(KeyCode.Backspace) && this.Cleared != null)
				{
					this.Cleared(base.transform.GetSiblingIndex());
				}
				wasEmpty = isEmpty;
			}
		}

		protected override void OnDisable()
		{
			base.OnDisable();
			optionContainer.SetActive(false);
		}

		protected override bool StayFocusedOnClickOutside()
		{
			return options.Any((Option x) => x.hoverArea.isMouseOver);
		}

		private void OnItemSelected(int index)
		{
			base.ValueText = items[scrollPosition + index];
			base.IsFocused = false;
			AdaptWidth();
			OnTextChanged();
		}

		private void OptionHoverChanged(SimpleUIButton optionButton, Option option)
		{
			if (optionButton.IsHovered)
			{
				highlightedOption = option.gameObject.transform.GetSiblingIndex();
				selectHighlightedOnExit = false;
				RenderItems();
			}
		}

		private void OnTextInput(string s)
		{
			isEmpty = string.IsNullOrEmpty(s);
			highlightedOption = null;
			AdaptWidth();
			UpdateItems(s);
		}

		private void HighlightAndScrollOptions(Direction direction, bool arrowKey)
		{
			if (items == null)
			{
				return;
			}
			int num = Mathf.Min(items.Count, options.Count);
			switch (direction)
			{
			case Direction.Up:
				if (!highlightedOption.HasValue)
				{
					highlightedOption = 0;
					break;
				}
				if (arrowKey)
				{
					int? num2 = highlightedOption;
					if (num2.GetValueOrDefault() != 0 || !num2.HasValue)
					{
						highlightedOption = Mathf.Clamp(highlightedOption.Value - 1, 0, num - 1);
						goto IL_00cb;
					}
				}
				scrollPosition = Mathf.Clamp(scrollPosition - 1, 0, items.Count - num);
				goto IL_00cb;
			case Direction.Down:
				if (!highlightedOption.HasValue)
				{
					highlightedOption = 0;
					break;
				}
				if (!arrowKey || highlightedOption == num - 1 || scrollPosition + highlightedOption.Value >= items.Count - 1)
				{
					scrollPosition = Mathf.Clamp(scrollPosition + 1, 0, items.Count - num);
				}
				else
				{
					highlightedOption = Mathf.Clamp(highlightedOption.Value + 1, 0, num - 1);
				}
				selectHighlightedOnExit = arrowKey;
				break;
			default:
				{
					highlightedOption = null;
					break;
				}
				IL_00cb:
				selectHighlightedOnExit = arrowKey;
				break;
			}
			RenderItems();
		}

		private void OnFocusChange(bool focused)
		{
			if (focused)
			{
				optionContainer.SetActive(true);
				AdaptWidth();
				UpdateItems(base.ValueText);
				return;
			}
			if (selectHighlightedOnExit && highlightedOption.HasValue)
			{
				OnItemSelected(highlightedOption.Value);
			}
			if ((bool)this && base.isActiveAndEnabled)
			{
				StartCoroutine(IEDisableOptionContainer());
			}
		}

		private IEnumerator IEDisableOptionContainer()
		{
			yield return new WaitForEndOfFrame();
			if ((bool)optionContainer)
			{
				optionContainer.SetActive(false);
			}
		}

		private void UpdateItems(string text)
		{
			if (!optionContainer.activeSelf || !optionContainer.activeInHierarchy || GetItems == null)
			{
				return;
			}
			items = GetItems().ToList();
			if (items.Count == 0)
			{
				return;
			}
			if (string.IsNullOrEmpty(text))
			{
				items.Sort();
			}
			else
			{
				items.Sort(delegate(string a, string b)
				{
					int length = new string(text.TakeWhile((char ch, int i) => i < b.Length && b[i] == ch).ToArray()).Length;
					int length2 = new string(text.TakeWhile((char ch, int i) => i < a.Length && a[i] == ch).ToArray()).Length;
					return (length != length2) ? length.CompareTo(length2) : DamerauLevenshteinDistance(text, a, 10).CompareTo(DamerauLevenshteinDistance(text, b, 10));
				});
			}
			scrollPosition = 0;
			highlightedOption = null;
			RenderItems();
		}

		private void RenderItems()
		{
			int count = Mathf.Min(options.Count, items.Count - scrollPosition);
			List<string> range = items.GetRange(scrollPosition, count);
			for (int i = 0; i < options.Count; i++)
			{
				Option option = options[i];
				if (i < range.Count)
				{
					option.gameObject.SetActive(true);
					option.background.sharedMaterial = ((!highlightedOption.HasValue || highlightedOption.Value != i) ? optionNormalMaterial : optionHoverMaterial);
					string text = range[i].Replace('\n', ' ');
					ReferenceMaster.SetDynamicText(option.text, (!onlyUpperCase) ? text : text.ToUpper());
					option.textFitter.FitText();
				}
				else
				{
					option.gameObject.SetActive(false);
					option.background.sharedMaterial = optionNormalMaterial;
				}
			}
		}

		private void AdaptWidth()
		{
			float a = text.bounds.size.x + margin * 2f;
			float num = Mathf.Max(a, initialWidth);
			Vector3 vector;
			if (text.anchor == DynamicTextAnchor.BaselineLeft)
			{
				BoxCollider boxCollider = collider;
				vector = BG.transform.localPosition.WithX(num * 0.5f);
				BG.transform.localPosition = vector;
				boxCollider.center = vector;
				optionContainer.transform.localPosition = optionContainer.transform.localPosition.WithX(num * 0.5f);
				text.transform.localPosition = text.transform.localPosition.WithX(margin);
			}
			BoxCollider boxCollider2 = collider;
			vector = BG.transform.localScale.WithX(num).WithZ(0.05f);
			BG.transform.localScale = vector;
			boxCollider2.size = vector;
			float x = Mathf.Max(minOptionsWidth, num - margin * 2f);
			foreach (Option option in options)
			{
				option.background.transform.localScale = option.background.transform.localScale.WithX(x);
				option.textFitter.FitText();
			}
		}

		public static int DamerauLevenshteinDistance(string source, string target, int threshold)
		{
			int arg = source.Length;
			int arg2 = target.Length;
			if (Math.Abs(arg - arg2) > threshold)
			{
				return int.MaxValue;
			}
			if (arg > arg2)
			{
				Swap(ref target, ref source);
				Swap(ref arg, ref arg2);
			}
			int num = arg;
			int num2 = arg2;
			int[] array = new int[num + 1];
			int[] array2 = new int[num + 1];
			int[] array3 = new int[num + 1];
			for (int i = 0; i <= num; i++)
			{
				array[i] = i;
			}
			int num3 = 0;
			int num4 = 0;
			int num5 = -1;
			for (int j = 1; j <= num2; j++)
			{
				int[] array4 = array3;
				array3 = array2;
				array2 = array;
				array = array4;
				int num6 = int.MaxValue;
				array[0] = j;
				num4 = 0;
				num5 = -1;
				for (int k = 1; k <= num; k++)
				{
					int num7 = ((source[num4] != target[num3]) ? 1 : 0);
					int num8 = array[num4] + 1;
					int num9 = array2[k] + 1;
					int num10 = array2[num4] + num7;
					int num11 = ((num8 > num9) ? ((num9 <= num10) ? num9 : num10) : ((num8 <= num10) ? num8 : num10));
					if (k > 1 && j > 1 && source[num5] == target[num3] && source[num4] == target[j - 2])
					{
						num11 = Math.Min(num11, array3[num5] + num7);
					}
					array[k] = num11;
					if (num11 < num6)
					{
						num6 = num11;
					}
					num4++;
					num5++;
				}
				num3++;
				if (num6 > threshold)
				{
					return int.MaxValue;
				}
			}
			int num12 = array[num];
			return (num12 <= threshold) ? num12 : int.MaxValue;
		}

		private static void Swap<T>(ref T arg1, ref T arg2)
		{
			T val = arg1;
			arg1 = arg2;
			arg2 = val;
		}
	}
}

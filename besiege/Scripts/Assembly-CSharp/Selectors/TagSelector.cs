using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SRF;
using UnityEngine;

namespace Selectors
{
	public class TagSelector : MonoBehaviour
	{
		private static readonly char[] SEPARATOR_CHARS = new char[2] { ';', ',' };

		public GameObject tagPrefab;

		public Transform background;

		public float rowHeight = 0.35f;

		public float gap = 0.05f;

		public float margin = 0.15f;

		public Func<IEnumerable<string>> GetAutocompleteItems;

		private Coroutine repositionCoroutine;

		private bool _isFocused;

		private float width;

		private float minHeight;

		private int rowCount = 1;

		private bool rowCountChanged;

		private bool splittingTags;

		private bool tagsChanging;

		private bool inConflict;

		public bool IsFocused
		{
			get
			{
				return _isFocused;
			}
		}

		public float Height
		{
			get
			{
				return background.localScale.y;
			}
		}

		private TextHolderAutocomplete[] tags
		{
			get
			{
				return GetComponentsInChildren<TextHolderAutocomplete>();
			}
		}

		public event Action<bool> Hovered;

		public event Action<bool> FocusChange;

		public event Action<string[]> TagsChanged;

		public event Action<float> HeightChanged;

		private void Awake()
		{
			width = background.localScale.x - margin * 2f;
			minHeight = Height;
			tagPrefab.transform.localPosition = Vector3.zero.WithX(10f);
			if (tags.Length == 0)
			{
				CreateVariableInstance(string.Empty);
			}
		}

		public void SetConflict(bool conflict)
		{
			inConflict = conflict;
			TextHolderAutocomplete[] array = tags;
			foreach (TextHolderAutocomplete textHolderAutocomplete in array)
			{
				textHolderAutocomplete.SetConflict(conflict);
			}
			if (array.Length > 1 && array.Length < StatMaster.KeyMapper.MaxDisplayedTags)
			{
				array[array.Length - 1].SetConflict(false);
			}
		}

		public void SetTags(IEnumerable<string> tags, float? selectorHeight = null)
		{
			if (selectorHeight.HasValue)
			{
				background.localScale = background.localScale.WithY(selectorHeight.Value / base.transform.localScale.y);
			}
			if (tags.SequenceEqual(GetTags()))
			{
				return;
			}
			foreach (Transform item in base.transform)
			{
				if (!(item == background))
				{
					UnityEngine.Object.Destroy(item.gameObject);
				}
			}
			int num = 0;
			foreach (string tag in tags)
			{
				if (!string.IsNullOrEmpty(tag))
				{
					CreateVariableInstance(tag);
					num++;
					if (num >= StatMaster.KeyMapper.MaxDisplayedTags)
					{
						break;
					}
				}
			}
			if (tags.Count() < StatMaster.KeyMapper.MaxDisplayedTags && !splittingTags)
			{
				TextHolderAutocomplete textHolderAutocomplete = CreateVariableInstance(string.Empty);
				textHolderAutocomplete.SetConflict(false);
			}
			RepositionElements(true);
		}

		public string[] GetTags()
		{
			return (from x in tags
				where (bool)x && !string.IsNullOrEmpty(x.ValueText)
				select x.ValueText).ToArray();
		}

		public void Terminate()
		{
			TextHolderAutocomplete[] array = tags;
			foreach (TextHolderAutocomplete textHolderAutocomplete in array)
			{
				textHolderAutocomplete.Terminate();
			}
		}

		private TextHolderAutocomplete CreateVariableInstance(string value = "")
		{
			TextHolderAutocomplete tag = (UnityEngine.Object.Instantiate(tagPrefab, base.transform, false) as GameObject).GetComponent<TextHolderAutocomplete>();
			tag.ValueText = value;
			tag.FocusChange += OnFocusChanged;
			tag.TextChanged += delegate
			{
				OnTagsChanged();
			};
			tag.TextInput += delegate(string t)
			{
				OnTextInput(t, tag);
			};
			tag.Cleared += OnTagCleared;
			tag.GetItems = GetAutocompleteItems;
			tag.SetConflict(inConflict);
			SimpleUIButton simpleUIButton = tag.gameObject.AddComponent<SimpleUIButton>();
			simpleUIButton.HoverChanged = (Action)Delegate.Combine(simpleUIButton.HoverChanged, (Action)delegate
			{
				this.Hovered(IsAnyHovered());
			});
			return tag;
		}

		private bool IsAnyHovered()
		{
			SimpleUIButton[] componentsInChildren = GetComponentsInChildren<SimpleUIButton>();
			bool result = false;
			foreach (SimpleUIButton simpleUIButton in componentsInChildren)
			{
				if (simpleUIButton.IsHovered)
				{
					result = true;
					break;
				}
			}
			return result;
		}

		private void OnTagCleared(int siblingIndex)
		{
			if (siblingIndex > 1)
			{
				if (siblingIndex > 2)
				{
					UnityEngine.Object.Destroy(base.transform.GetChild(siblingIndex).gameObject);
				}
				else
				{
					base.transform.GetChild(siblingIndex).GetComponent<TextHolderAutocomplete>().IsFocused = false;
				}
				base.transform.GetChild(siblingIndex - 1).GetComponent<TextHolderAutocomplete>().IsFocused = true;
				RepositionElements(false);
			}
		}

		private void OnTextInput(string input, TextHolderAutocomplete tag)
		{
			if (!SEPARATOR_CHARS.Any(((IEnumerable<char>)input).Contains<char>))
			{
				RepositionElements(false);
				return;
			}
			string[] array = input.Split(SEPARATOR_CHARS, 2);
			if (base.transform.childCount > StatMaster.KeyMapper.MaxDisplayedTags)
			{
				tag.ValueText = string.Join(string.Empty, array);
				return;
			}
			if (string.IsNullOrEmpty(array.First()))
			{
				tag.ValueText = array.Last();
				return;
			}
			splittingTags = true;
			tag.IsFocused = false;
			tag.ValueText = array.First();
			string value = ((array.Length <= 1) ? string.Empty : array.Last());
			TextHolderAutocomplete textHolderAutocomplete = CreateVariableInstance(value);
			splittingTags = false;
			if ((bool)this && base.isActiveAndEnabled)
			{
				StartCoroutine(IEFocusDelayed(textHolderAutocomplete));
			}
		}

		private IEnumerator IEFocusDelayed(TextHolderAutocomplete tag)
		{
			yield return new WaitForEndOfFrame();
			if ((bool)tag)
			{
				tag.IsFocused = true;
			}
		}

		private void OnFocusChanged(bool focused)
		{
			_isFocused = focused;
			if (this.FocusChange != null)
			{
				this.FocusChange(focused);
			}
		}

		private void OnTagsChanged()
		{
			if (!tagsChanging && this.TagsChanged != null)
			{
				tagsChanging = true;
				this.TagsChanged(GetTags());
				tagsChanging = false;
			}
			TextHolderAutocomplete[] array = tags;
			foreach (TextHolderAutocomplete textHolderAutocomplete in array)
			{
				if (string.IsNullOrEmpty(textHolderAutocomplete.ValueText))
				{
					UnityEngine.Object.Destroy(textHolderAutocomplete.gameObject);
				}
			}
			if (base.transform.childCount <= StatMaster.KeyMapper.MaxDisplayedTags && !splittingTags)
			{
				TextHolderAutocomplete textHolderAutocomplete2 = CreateVariableInstance(string.Empty);
				textHolderAutocomplete2.SetConflict(false);
			}
			RepositionElements(true);
		}

		private void RepositionElements(bool submit)
		{
			if ((bool)this && base.isActiveAndEnabled)
			{
				if (repositionCoroutine != null)
				{
					StopCoroutine(repositionCoroutine);
				}
				repositionCoroutine = StartCoroutine(IERepositionElements(submit));
			}
		}

		private IEnumerator IERepositionElements(bool submit)
		{
			yield return new WaitForEndOfFrame();
			IEnumerable<Transform> elements = base.transform.GetChildren();
			float x = 0f;
			float y = (0f - rowHeight) / 2f - gap;
			int newRowCount = 0;
			List<Transform> rowItems = new List<Transform>();
			foreach (Transform t in elements)
			{
				if (t == background)
				{
					continue;
				}
				TextHolderAutocomplete autocomplete = t.GetComponent<TextHolderAutocomplete>();
				float tagWidth = autocomplete.BG.transform.localScale.x;
				if (x + tagWidth > width)
				{
					Vector3 centerDistance = Vector3.zero.WithX(x / 2f);
					foreach (Transform item in rowItems)
					{
						item.localPosition -= centerDistance;
					}
					rowItems.Clear();
					x = 0f;
					y -= rowHeight + gap;
					newRowCount++;
				}
				float gapIfNotFirst = ((rowItems.Count <= 0) ? 0f : gap);
				t.localPosition = new Vector3(x + gapIfNotFirst, y);
				x += tagWidth + gapIfNotFirst;
				rowItems.Add(t);
			}
			Vector3 lastCenterDistance = Vector3.zero.WithX(x / 2f);
			foreach (Transform item2 in rowItems)
			{
				item2.localPosition -= lastCenterDistance;
			}
			float newHeight = Mathf.Max(minHeight, (float)(newRowCount + 2) * (rowHeight + gap) + margin);
			if (submit && !Mathf.Approximately(newHeight, Height) && this.HeightChanged != null)
			{
				this.HeightChanged(newHeight * base.transform.localScale.y);
			}
			repositionCoroutine = null;
		}
	}
}

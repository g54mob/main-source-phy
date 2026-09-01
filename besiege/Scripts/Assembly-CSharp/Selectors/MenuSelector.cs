using System;
using UnityEngine;

namespace Selectors
{
	public class MenuSelector : Selector
	{
		[SerializeField]
		private Material activeMaterial;

		[SerializeField]
		private Transform content;

		[SerializeField]
		private Renderer background;

		[SerializeField]
		private TextHolderAutocomplete text;

		[SerializeField]
		private UIButton previousButton;

		[SerializeField]
		private UIButton nextButton;

		private bool updateCallback;

		private UIButton uiButton;

		public override MapperType MapperType
		{
			get
			{
				return Menu;
			}
			set
			{
				if (updateCallback)
				{
					if (Menu != null)
					{
						Menu.ValueChanged -= OnMenuChanged;
					}
					updateCallback = false;
				}
				Menu = (MMenu)value;
				if (Menu != null)
				{
					Menu.ValueChanged += OnMenuChanged;
					updateCallback = true;
				}
			}
		}

		public MMenu Menu { get; set; }

		private void Awake()
		{
			previousButton.Click += Previous;
			nextButton.Click += Next;
			text.GetItems = () => Menu.Items;
			text.TextChanged += OnOptionChanged;
		}

		private void OnMenuChanged(int newValue)
		{
			UpdateVisual();
		}

		protected void OnDisable()
		{
			if (updateCallback)
			{
				if (Menu != null)
				{
					Menu.ValueChanged -= OnMenuChanged;
				}
				updateCallback = false;
			}
		}

		protected override void UpdateVisual()
		{
			if (Menu != null && !(text == null))
			{
				if (!string.Equals(Menu.Selection, text.ValueText, StringComparison.OrdinalIgnoreCase))
				{
					text.SetText(Menu.Selection.ToUpper());
				}
				text.SetConflict(InConflict());
			}
		}

		public override void Init()
		{
			base.Init();
			UpdateVisual();
		}

		private void OnOptionChanged(string t)
		{
			int num = Menu.Items.IndexOf(t);
			if (num < 0)
			{
				UpdateVisual();
				return;
			}
			Menu.SetValue(num);
			OnEdit();
		}

		public void Previous()
		{
			if (Menu == null)
			{
				Debug.LogWarning("Trying to execute Previous, but Menu is null!");
				return;
			}
			int num = Menu.Value - 1;
			if (num < 0)
			{
				num = Menu.Items.Count - 1;
			}
			Menu.SetValue(num);
			OnEdit();
		}

		public void Next()
		{
			if (Menu == null)
			{
				Debug.LogWarning("Trying to execute Next, but Menu is null!");
				return;
			}
			int num = Menu.Value + 1;
			if (num >= Menu.Items.Count)
			{
				num = 0;
			}
			Menu.SetValue(num);
			OnEdit();
		}

		private void OnClick()
		{
			if (Menu != null)
			{
				int num = Menu.Value + 1;
				if (num >= Menu.Items.Count)
				{
					num = 0;
				}
				Menu.SetValue(num);
				OnEdit();
			}
		}
	}
}

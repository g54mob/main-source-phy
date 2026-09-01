using UnityEngine;

namespace Selectors
{
	[RequireComponent(typeof(UIButton))]
	public class ToggleSelector : Selector
	{
		[SerializeField]
		protected Material activeMaterial;

		[SerializeField]
		protected Transform content;

		[SerializeField]
		protected Renderer background;

		[SerializeField]
		protected Renderer conflictBg;

		[SerializeField]
		protected DynamicText text;

		private bool updateCallback;

		private UIButton uiButton;

		private Material normalMaterial;

		private bool inConflict;

		public override MapperType MapperType
		{
			get
			{
				return Toggle;
			}
			set
			{
				if (updateCallback)
				{
					if (Toggle != null)
					{
						Toggle.Toggled -= OnToggle;
					}
					updateCallback = false;
				}
				Toggle = (MToggle)value;
				if (Toggle != null)
				{
					Toggle.Toggled += OnToggle;
					updateCallback = true;
				}
			}
		}

		public MToggle Toggle { get; set; }

		private void Awake()
		{
			GetComponent<UIButton>().Click += OnClick;
			normalMaterial = background.material;
		}

		private void OnToggle(bool toggle)
		{
			UpdateVisual();
		}

		protected void OnDisable()
		{
			if (updateCallback)
			{
				if (Toggle != null)
				{
					Toggle.Toggled -= OnToggle;
				}
				updateCallback = false;
			}
		}

		public override void Init()
		{
			if (Toggle == null)
			{
				Debug.LogWarning("MToggle has not been assigned to " + base.transform.name);
			}
			else
			{
				string displayName = Toggle.DisplayName;
				text.SetText(displayName.ToUpper());
				int num = 0;
				string[] array = displayName.Split('\n');
				for (int i = 0; i < array.Length; i++)
				{
					int length = array[i].Length;
					if (length > num)
					{
						num = length;
					}
				}
				text.letterSpacing = Mathf.Clamp(0.6f - (float)num / 30f, -0.05f, 0.2f);
			}
			base.Init();
			UpdateVisual();
		}

		private void OnClick()
		{
			if (Toggle != null)
			{
				if ((bool)conflictBg && inConflict)
				{
					OnEdit();
					return;
				}
				Toggle.SetValue(!Toggle.IsActive);
				OnEdit();
			}
		}

		protected override void UpdateVisual()
		{
			if (Toggle != null)
			{
				background.material = ((!Toggle.IsActive) ? normalMaterial : activeMaterial);
				if ((bool)conflictBg)
				{
					inConflict = InConflict();
					background.gameObject.SetActive(!inConflict);
					conflictBg.gameObject.SetActive(inConflict);
				}
			}
		}
	}
}

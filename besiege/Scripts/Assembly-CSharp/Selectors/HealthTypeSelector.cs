using UnityEngine;

namespace Selectors
{
	public class HealthTypeSelector : Selector
	{
		[SerializeField]
		private UIButtonExtended totalHealth;

		[SerializeField]
		private UIButtonExtended minHealth;

		[SerializeField]
		private UIButtonExtended maxHealth;

		private bool updateCallback;

		public override MapperType MapperType
		{
			get
			{
				return Range;
			}
			set
			{
				if (updateCallback)
				{
					if (Range != null)
					{
						Range.HealthRangeChanged -= OnRangeChanged;
					}
					updateCallback = false;
				}
				Range = (MHealthType)value;
				if (Range != null)
				{
					Range.HealthRangeChanged += OnRangeChanged;
					updateCallback = true;
				}
			}
		}

		public MHealthType Range { get; set; }

		private void Awake()
		{
			totalHealth.Down += TotalHealth;
			minHealth.Down += MinHealth;
			maxHealth.Down += MaxHealth;
		}

		private void OnRangeChanged(HealthRange range)
		{
			UpdateVisual();
		}

		protected void OnDisable()
		{
			if (updateCallback)
			{
				if (Range != null)
				{
					Range.HealthRangeChanged -= OnRangeChanged;
				}
				updateCallback = false;
			}
		}

		private void TotalHealth()
		{
			OnRangeClicked(HealthRange.TotalHealth);
		}

		private void MinHealth()
		{
			OnRangeClicked(HealthRange.MinHealth);
		}

		private void MaxHealth()
		{
			OnRangeClicked(HealthRange.MaxHealth);
		}

		private void OnRangeClicked(HealthRange range)
		{
			Range.SetValue(range);
			OnEdit();
		}

		public override void Init()
		{
			if (Range == null)
			{
				Debug.LogWarning("MHealthType has not been assigned to " + base.transform.name);
			}
			base.Init();
			UpdateVisual();
		}

		protected override void UpdateVisual()
		{
			switch (Range.HealthRange)
			{
			case HealthRange.TotalHealth:
				totalHealth.ToggleBG(true);
				minHealth.ToggleBG(false);
				maxHealth.ToggleBG(false);
				break;
			case HealthRange.MinHealth:
				totalHealth.ToggleBG(false);
				minHealth.ToggleBG(true);
				maxHealth.ToggleBG(false);
				break;
			case HealthRange.MaxHealth:
				totalHealth.ToggleBG(false);
				minHealth.ToggleBG(false);
				maxHealth.ToggleBG(true);
				break;
			}
		}
	}
}

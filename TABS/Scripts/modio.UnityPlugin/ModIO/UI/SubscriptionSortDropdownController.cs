using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ModIO.UI
{
	[RequireComponent(typeof(Dropdown))]
	public class SubscriptionSortDropdownController : MonoBehaviour, ISubscriptionsViewElement
	{
		public class FieldSelectAttribute : PropertyAttribute
		{
		}

		[Serializable]
		public class OptionData
		{
			public string displayText = string.Empty;

			[FieldSelect]
			public string fieldName = string.Empty;

			public bool isAscending = true;
		}

		public static readonly Dictionary<string, Comparison<ModProfile>> subscriptionSortOptions = new Dictionary<string, Comparison<ModProfile>>
		{
			{
				"Name",
				delegate(ModProfile a, ModProfile b)
				{
					if (a == null || b == null)
					{
						if (a == null && b == null)
						{
							return 0;
						}
						if (a == null)
						{
							return 1;
						}
						return -1;
					}
					int num = string.Compare(a.name, b.name);
					if (num == 0)
					{
						num = a.id - b.id;
					}
					return num;
				}
			},
			{
				"File Size",
				delegate(ModProfile a, ModProfile b)
				{
					bool flag = a == null || a.currentBuild == null;
					bool flag2 = b == null || b.currentBuild == null;
					if (flag || flag2)
					{
						if (flag && flag2)
						{
							return 0;
						}
						if (flag)
						{
							return 1;
						}
						return -1;
					}
					int num = (int)(a.currentBuild.fileSize - b.currentBuild.fileSize);
					if (num == 0)
					{
						num = string.Compare(a.name, b.name);
						if (num == 0)
						{
							num = a.id - b.id;
						}
					}
					return num;
				}
			},
			{
				"Date Updated",
				delegate(ModProfile a, ModProfile b)
				{
					if (a == null || b == null)
					{
						if (a == null && b == null)
						{
							return 0;
						}
						if (a == null)
						{
							return 1;
						}
						return -1;
					}
					int num = a.dateUpdated - b.dateUpdated;
					if (num == 0)
					{
						num = string.Compare(a.name, b.name);
						if (num == 0)
						{
							num = a.id - b.id;
						}
					}
					return num;
				}
			},
			{
				"Enabled",
				delegate(ModProfile a, ModProfile b)
				{
					if (a == null || b == null)
					{
						if (a == null && b == null)
						{
							return 0;
						}
						if (a == null)
						{
							return 1;
						}
						return -1;
					}
					int num = 0;
					num += (LocalUser.EnabledModIds.Contains(a.id) ? (-1) : 0);
					num += (LocalUser.EnabledModIds.Contains(b.id) ? 1 : 0);
					if (num == 0)
					{
						num = string.Compare(a.name, b.name);
						if (num == 0)
						{
							num = a.id - b.id;
						}
					}
					return num;
				}
			}
		};

		public OptionData[] options = new OptionData[1]
		{
			new OptionData
			{
				displayText = "A-Z",
				fieldName = "Name",
				isAscending = true
			}
		};

		private SubscriptionsView m_view;

		public Dropdown dropdown => base.gameObject.GetComponent<Dropdown>();

		private void Start()
		{
			dropdown.onValueChanged.AddListener(delegate
			{
				SetSubscriptionsViewSortMethod();
			});
			SetSubscriptionsViewSortMethod();
		}

		public void SetSubscriptionsView(SubscriptionsView view)
		{
			if (!(m_view == view))
			{
				m_view = view;
				SetSubscriptionsViewSortMethod();
			}
		}

		public void SetSubscriptionsViewSortMethod()
		{
			if (!(m_view == null))
			{
				Comparison<ModProfile> selectedSortFunction = GetSelectedSortFunction();
				if (selectedSortFunction != null)
				{
					m_view.SetSortDelegate(selectedSortFunction);
				}
			}
		}

		public Comparison<ModProfile> GetSelectedSortFunction()
		{
			if (options != null && options.Length != 0 && dropdown.options != null && dropdown.value < dropdown.options.Count)
			{
				Dropdown.OptionData optionData = dropdown.options[dropdown.value];
				OptionData[] array = options;
				foreach (OptionData option in array)
				{
					if (option.displayText == optionData.text)
					{
						if (option.isAscending)
						{
							return subscriptionSortOptions[option.fieldName];
						}
						return (ModProfile a, ModProfile b) => subscriptionSortOptions[option.fieldName](b, a);
					}
				}
			}
			return null;
		}
	}
}

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Kamgam.SettingsGenerator
{
	public class GetSetConnectionWithOptions<T> : ConnectionWithOptions<T>
	{
		protected int _selectedIndex;

		protected List<T> _optionLabels;

		public event Func<int> Getter
		{
			[CompilerGenerated]
			add
			{
			}
			[CompilerGenerated]
			remove
			{
			}
		}

		public event Action<int> Setter
		{
			[CompilerGenerated]
			add
			{
			}
			[CompilerGenerated]
			remove
			{
			}
		}

		public event Func<List<T>> OptionLabelGetter
		{
			[CompilerGenerated]
			add
			{
			}
			[CompilerGenerated]
			remove
			{
			}
		}

		public event Action<List<T>> OptionLabelSetter
		{
			[CompilerGenerated]
			add
			{
			}
			[CompilerGenerated]
			remove
			{
			}
		}

		public GetSetConnectionWithOptions(Func<int> getter, Action<int> setter, Func<List<T>> optionLabelGetter = null, Action<List<T>> optionLabelSetter = null)
		{
		}

		public override int Get()
		{
			return 0;
		}

		public override void Set(int selectedIndex)
		{
		}

		public int GetLastKnownValue()
		{
			return 0;
		}

		public override List<T> GetOptionLabels()
		{
			return null;
		}

		public override void SetOptionLabels(List<T> optionLabels)
		{
		}

		public override void RefreshOptionLabels()
		{
		}

		public int GetLastSelectedIndex()
		{
			return 0;
		}

		public void SetLastSelectedIndex(int index)
		{
		}
	}
}

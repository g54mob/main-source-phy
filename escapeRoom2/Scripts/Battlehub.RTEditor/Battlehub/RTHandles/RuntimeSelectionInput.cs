using UnityEngine;

namespace Battlehub.RTHandles
{
	public class RuntimeSelectionInput : RuntimeSelectionInputBase
	{
		public KeyCode m_modifierKey = KeyCode.LeftControl;

		public KeyCode SelectAllKey = KeyCode.A;

		protected KeyCode ModifierKey => m_modifierKey;

		protected virtual bool MultiselectAction()
		{
			return m_component.Editor.Input.GetKey(ModifierKey);
		}

		protected virtual bool SelectAllAction()
		{
			if (Input.GetKeyDown(SelectAllKey))
			{
				return Input.GetKey(ModifierKey);
			}
			return false;
		}

		protected override void LateUpdate()
		{
			base.LateUpdate();
			if (SelectAllAction())
			{
				SelectAll();
			}
		}

		protected override void OnSelectGO()
		{
			m_component.SelectGO(MultiselectAction(), allowUnselect: true);
		}

		protected override void OnBoxSelection(object sender, BoxSelectionArgs e)
		{
			m_component.BoxSelect(e.GameObjects, MultiselectAction());
		}
	}
}

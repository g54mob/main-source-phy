using UnityEngine;

namespace Battlehub.RTHandles
{
	[DefaultExecutionOrder(-65)]
	public class PositionHandleInput : BaseHandleInput
	{
		public KeyCode VertexSnappingKey = KeyCode.V;

		public KeyCode VertexSnappingToggleKey = KeyCode.LeftShift;

		public KeyCode SnapToGroundKey;

		protected PositionHandle PositionHandle => (PositionHandle)m_handle;

		protected override void Update()
		{
			base.Update();
			if (m_handle == null || !m_handle.enabled || m_editor.Tools.IsViewing || !m_handle.IsWindowActive || !m_handle.Window.IsPointerOver)
			{
				return;
			}
			PositionHandle.SnapToGround = SnapToGroundAction();
			if (BeginVertexSnappingAction())
			{
				PositionHandle.IsInVertexSnappingMode = true;
				if (VertexSnappingToggleAction())
				{
					m_editor.Tools.IsSnapping = !m_editor.Tools.IsSnapping;
				}
			}
			else if (EndVertexSnappingAction())
			{
				PositionHandle.IsInVertexSnappingMode = false;
			}
		}

		protected virtual bool BeginVertexSnappingAction()
		{
			return m_editor.Input.GetKeyDown(VertexSnappingKey);
		}

		protected virtual bool EndVertexSnappingAction()
		{
			return m_editor.Input.GetKeyUp(VertexSnappingKey);
		}

		protected virtual bool VertexSnappingToggleAction()
		{
			return m_editor.Input.GetKey(VertexSnappingToggleKey);
		}

		protected virtual bool SnapToGroundAction()
		{
			return m_editor.Input.GetKey(SnapToGroundKey);
		}
	}
}

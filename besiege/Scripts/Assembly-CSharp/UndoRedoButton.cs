public class UndoRedoButton : ClickBehaviour
{
	public bool undo;

	public override void OnClicked()
	{
		Machine machine = Machine.Active();
		if ((bool)machine && !machine.isSimulating && machine.CanModify)
		{
			if (undo)
			{
				machine.UndoSystem.Undo();
			}
			else
			{
				machine.UndoSystem.Redo();
			}
		}
	}
}

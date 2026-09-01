public class ReplaceMachineUndoAction : UndoAction
{
	private MachineInfo previousMachineInfo;

	private MachineInfo newMachineInfo;

	public ReplaceMachineUndoAction(Machine m, MachineInfo newMachineInfo)
	{
		machine = m;
		previousMachineInfo = m.CreateMachineInfo();
		this.newMachineInfo = newMachineInfo;
	}

	public override bool Redo()
	{
		machine.LoadMachineInfo(newMachineInfo);
		return true;
	}

	public override bool Undo()
	{
		machine.LoadMachineInfo(previousMachineInfo);
		return true;
	}
}

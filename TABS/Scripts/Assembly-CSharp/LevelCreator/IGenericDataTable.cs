using System;

namespace LevelCreator
{
	public interface IGenericDataTable
	{
		DataTableRow AddRow(string key);

		void RemoveRow(string key);

		void RemoveRow(int index);

		int Nudge(string key, NudgeDirection direction);

		int Nudge(int index, NudgeDirection direction);

		int Size();

		DataTableRow GetRowValue(string key);

		DataTableRow[] GetRowValues();

		string[] GetKeys();

		void SetKey(string key, string newKey);

		Type GetTableType();
	}
}

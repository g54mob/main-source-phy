using Mono.Collections.Generic;

namespace Mono.Cecil.Cil
{
	internal class InstructionCollection : Collection<Instruction>
	{
		private struct InstructionOffsetCache
		{
			public int Offset;

			public int Index;

			public Instruction Instruction;
		}

		private readonly MethodDefinition method;

		internal InstructionCollection(MethodDefinition method)
		{
			this.method = method;
		}

		internal InstructionCollection(MethodDefinition method, int capacity)
			: base(capacity)
		{
			this.method = method;
		}

		protected override void OnAdd(Instruction item, int index)
		{
			if (index != 0)
			{
				Instruction instruction = items[index - 1];
				instruction.next = item;
				item.previous = instruction;
			}
		}

		protected override void OnInsert(Instruction item, int index)
		{
			if (size != 0)
			{
				Instruction instruction = items[index];
				if (instruction == null)
				{
					Instruction instruction2 = items[index - 1];
					instruction2.next = item;
					item.previous = instruction2;
					return;
				}
				_ = instruction.Offset;
				Instruction previous = instruction.previous;
				if (previous != null)
				{
					previous.next = item;
					item.previous = previous;
				}
				instruction.previous = item;
				item.next = instruction;
			}
			UpdateLocalScopes(null, null);
		}

		protected override void OnSet(Instruction item, int index)
		{
			Instruction instruction = items[index];
			item.previous = instruction.previous;
			item.next = instruction.next;
			instruction.previous = null;
			instruction.next = null;
			UpdateLocalScopes(item, instruction);
		}

		protected override void OnRemove(Instruction item, int index)
		{
			Instruction previous = item.previous;
			if (previous != null)
			{
				previous.next = item.next;
			}
			Instruction next = item.next;
			if (next != null)
			{
				next.previous = item.previous;
			}
			RemoveSequencePoint(item);
			UpdateLocalScopes(item, next ?? previous);
			item.previous = null;
			item.next = null;
		}

		private void RemoveSequencePoint(Instruction instruction)
		{
			MethodDebugInformation debug_info = method.debug_info;
			if (debug_info == null || !debug_info.HasSequencePoints)
			{
				return;
			}
			Collection<SequencePoint> sequence_points = debug_info.sequence_points;
			for (int i = 0; i < sequence_points.Count; i++)
			{
				if (sequence_points[i].Offset == instruction.offset)
				{
					sequence_points.RemoveAt(i);
					break;
				}
			}
		}

		private void UpdateLocalScopes(Instruction removedInstruction, Instruction existingInstruction)
		{
			MethodDebugInformation debug_info = method.debug_info;
			if (debug_info != null)
			{
				InstructionOffsetCache cache = new InstructionOffsetCache
				{
					Offset = 0,
					Index = 0,
					Instruction = items[0]
				};
				UpdateLocalScope(debug_info.Scope, removedInstruction, existingInstruction, ref cache);
			}
		}

		private void UpdateLocalScope(ScopeDebugInformation scope, Instruction removedInstruction, Instruction existingInstruction, ref InstructionOffsetCache cache)
		{
			if (scope == null)
			{
				return;
			}
			if (!scope.Start.IsResolved)
			{
				scope.Start = ResolveInstructionOffset(scope.Start, ref cache);
			}
			if (!scope.Start.IsEndOfMethod && scope.Start.ResolvedInstruction == removedInstruction)
			{
				scope.Start = new InstructionOffset(existingInstruction);
			}
			if (scope.HasScopes)
			{
				foreach (ScopeDebugInformation scope2 in scope.Scopes)
				{
					UpdateLocalScope(scope2, removedInstruction, existingInstruction, ref cache);
				}
			}
			if (!scope.End.IsResolved)
			{
				scope.End = ResolveInstructionOffset(scope.End, ref cache);
			}
			if (!scope.End.IsEndOfMethod && scope.End.ResolvedInstruction == removedInstruction)
			{
				scope.End = new InstructionOffset(existingInstruction);
			}
		}

		private InstructionOffset ResolveInstructionOffset(InstructionOffset inputOffset, ref InstructionOffsetCache cache)
		{
			if (inputOffset.IsResolved)
			{
				return inputOffset;
			}
			int offset = inputOffset.Offset;
			if (cache.Offset == offset)
			{
				return new InstructionOffset(cache.Instruction);
			}
			if (cache.Offset > offset)
			{
				int num = 0;
				for (int i = 0; i < items.Length; i++)
				{
					if (num == offset)
					{
						return new InstructionOffset(items[i]);
					}
					if (num > offset)
					{
						return new InstructionOffset(items[i - 1]);
					}
					num += items[i].GetSize();
				}
				return default(InstructionOffset);
			}
			int num2 = cache.Offset;
			for (int j = cache.Index; j < items.Length; j++)
			{
				cache.Index = j;
				cache.Offset = num2;
				Instruction instruction = items[j];
				if (instruction == null)
				{
					break;
				}
				cache.Instruction = instruction;
				if (cache.Offset == offset)
				{
					return new InstructionOffset(cache.Instruction);
				}
				if (cache.Offset > offset)
				{
					return new InstructionOffset(items[j - 1]);
				}
				num2 += instruction.GetSize();
			}
			return default(InstructionOffset);
		}
	}
}

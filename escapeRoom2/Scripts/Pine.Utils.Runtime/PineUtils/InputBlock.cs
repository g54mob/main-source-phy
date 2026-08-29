using System.Collections.Generic;

namespace PineUtils
{
	public class InputBlock
	{
		public uint block;

		public string source;

		public static void blockInput(List<InputBlock> inputBlocks, uint block, string source)
		{
			inputBlocks.Add(new InputBlock
			{
				block = block,
				source = source
			});
		}

		public static void unblockInput(List<InputBlock> inputBlocks, uint block, string source)
		{
			bool flag = false;
			int num = inputBlocks.Count - 1;
			while (num >= 0 && !flag)
			{
				flag = (inputBlocks[num].block & block) == block && inputBlocks[num].source == source;
				if (flag)
				{
					inputBlocks.RemoveAt(num);
				}
				num--;
			}
		}

		public static bool isInputBlocked(List<InputBlock> inputBlocks, uint block)
		{
			return inputBlocks.Exists((InputBlock x) => (x.block & block) == block);
		}
	}
}

using System.Collections.Generic;

namespace func.brainfuck
{
	public class BrainfuckLoopCommands
	{
		private static readonly Stack<int> stack = new Stack<int>();
		private static readonly Dictionary<int, int> openClose = new Dictionary<int, int>();
		private static readonly Dictionary<int, int> closeOpen = new Dictionary<int, int>();

		private static void BodyLoop(IVirtualMachine vm)
		{
			for (var i = 0; i < vm.Instructions.Length; i++)
			{
				if (vm.Instructions[i] == '[') stack.Push(i);
				if (vm.Instructions[i] == ']')
				{
					var index = stack.Pop();
					openClose[index] = i;
					closeOpen[i] = index;
				}
			}

		}
		public static void RegisterTo(IVirtualMachine virtualMachine)
		{
			BodyLoop(virtualMachine);
			virtualMachine.RegisterCommand('[', b => {
				if (b.Memory[b.MemoryPointer] == 0)
					b.InstructionPointer = openClose[b.InstructionPointer];
			});
			virtualMachine.RegisterCommand(']', b => {
				if (b.Memory[b.MemoryPointer] != 0)
					b.InstructionPointer = closeOpen[b.InstructionPointer];
			});

		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace func.brainfuck
{
    public class Constant
    {
        static readonly char[] symbols = "QWERTYUIOPASDFGHJKLZXCVBNMqwertyuiopasdfghjklzxcvbnm1234567890".ToCharArray();

        public static void SaveValues(IVirtualMachine vm)
        {
            foreach (var chars in symbols)
                vm.RegisterCommand(chars, machine => machine.Memory[machine.MemoryPointer] = (byte)chars);
        }
    }

    public class BrainfuckBasicCommands
    {
        public static void RightOrLeftShift(IVirtualMachine virtualMachine)
        {
            virtualMachine.RegisterCommand('>', machine =>
               machine.MemoryPointer = Calculate(machine.MemoryPointer, 1, machine.Memory.Length));

            virtualMachine.RegisterCommand('<', machine =>
                machine.MemoryPointer = Calculate(machine.MemoryPointer, -1, machine.Memory.Length));
        }

        public static int Calculate(int a, int b, int modulus)
        {
            return (a + modulus + b % modulus) % modulus;
        }

        public static void IncreaseOrDecreaseByte(IVirtualMachine virtualMachine)
        {
            virtualMachine.RegisterCommand('-', machine =>
            {
                var length = machine.Memory.Length;
                var bytes = machine.Memory[machine.MemoryPointer];
                machine.Memory[machine.MemoryPointer] = bytes == 0
                    ? machine.Memory[machine.MemoryPointer] = 255
                    : (byte)Calculate(bytes, -1, length);
            });

            virtualMachine.RegisterCommand('+', machine =>
            {
                var length = machine.Memory.Length;
                var bytes = machine.Memory[machine.MemoryPointer];
                machine.Memory[machine.MemoryPointer] = bytes == 255
                    ? machine.Memory[machine.MemoryPointer] = 0
                    : (byte)Calculate(bytes, 1, length);
            });
        }

        public static void RegisterTo(IVirtualMachine visualMachine, Func<int> read, Action<char> write)
        {
            Constant.SaveValues(visualMachine);
            RightOrLeftShift(visualMachine);
            IncreaseOrDecreaseByte(visualMachine);
            visualMachine.RegisterCommand('.', machine => write((char)machine.Memory[machine.MemoryPointer]));
            visualMachine.RegisterCommand(',', machine => machine.Memory[machine.MemoryPointer] = (byte)read());
        }
    }
}
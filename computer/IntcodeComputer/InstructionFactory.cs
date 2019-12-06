using System;
using System.Collections.Generic;

namespace IntcodeComputer
{
  public static class InstructionFactory
  {
    public static IInstruction ParseInstruction(List<int> memory, int instructionPointer)
    {
      var opCode = memory[instructionPointer];
      var opCodeString = opCode.ToString().PadLeft(2); // 2-digit op codes
      var baseOpCode = int.Parse(opCodeString.Replace(' ', '0').Substring(opCodeString.Length - 2));

      if (baseOpCode == 1)
        return new AddInstruction(
          opCode,
          memory[instructionPointer + 1],
          memory[instructionPointer + 2],
          memory[instructionPointer + 3]);

      else if (baseOpCode == 2)
        return new MultiplyInstruction(
          opCode,
          memory[instructionPointer + 1],
          memory[instructionPointer + 2],
          memory[instructionPointer + 3]);

      else if (baseOpCode == 3)
        return new InputInstruction(
          opCode,
          memory[instructionPointer + 1]);

      else if (baseOpCode == 4)
        return new OutputInstruction(
          opCode,
          memory[instructionPointer + 1]);

      else if (baseOpCode == 5)
        return new JumpIfTrueInstruction(
          opCode,
          memory[instructionPointer + 1],
          memory[instructionPointer + 2]);

      else if (baseOpCode == 6)
        return new JumpIfFalseInstruction(
          opCode,
          memory[instructionPointer + 1],
          memory[instructionPointer + 2]);

      else if (baseOpCode == 7)
        return new LessThanInstruction(
          opCode,
          memory[instructionPointer + 1],
          memory[instructionPointer + 2],
          memory[instructionPointer + 3]);

      else if (baseOpCode == 8)
        return new EqualsInstruction(
          opCode,
          memory[instructionPointer + 1],
          memory[instructionPointer + 2],
          memory[instructionPointer + 3]);

      else if (baseOpCode == 99) return new HaltInstruction();

      throw new Exception($"Invalid OpCode detected: {opCode}");
    }
  }
}

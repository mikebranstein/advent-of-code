using System;
using System.Collections.Generic;

namespace IntcodeComputer
{
  public static class InstructionFactory
  {
    public static IInstruction ParseInstruction(List<int> memory, int instructionPointer)
    {
      var opCode = memory[instructionPointer];

      if (opCode == 1)
        return new AddInstruction(
          opCode,
          memory[instructionPointer + 1],
          memory[instructionPointer + 2],
          memory[instructionPointer + 3]);

      else if (opCode == 2)
        return new MultiplyInstruction(
          opCode,
          memory[instructionPointer + 1],
          memory[instructionPointer + 2],
          memory[instructionPointer + 3]);

      else if (opCode == 3)
        return new InputInstruction(
          opCode,
          memory[instructionPointer + 1]);

      else if (opCode == 4)
        return new OutputInstruction(
          opCode,
          memory[instructionPointer + 1]);

      else if (opCode == 99) return new HaltInstruction();

      throw new Exception($"Invalid OpCode detected: {opCode}");
    }
  }
}

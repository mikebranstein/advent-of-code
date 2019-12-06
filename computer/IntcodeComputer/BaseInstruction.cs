using System;
using System.Collections.Generic;

namespace IntcodeComputer
{
  public class BaseInstruction
  {
    public int OpCode { get; set; }
    public int PointerAdvancement { get; set; }
    public Parameter[] Parameters { get; set; }
  }
}

using System;
using System.Collections.Generic;

namespace TT_Games_Explorer.Formats.ExtractHelper.LDraw
{
    public class OptionalLines : List<OptionalLine>
    {
        public new void Add(OptionalLine newLine)
        {
            foreach (var optionalLine in this)
            {
                if (optionalLine.X.Equals(newLine.X) && optionalLine.Y.Equals(newLine.Y))
                {
                    if (!optionalLine.Nx.Equals(newLine.Nx) || !optionalLine.Ny.Equals(newLine.Ny) || optionalLine.B != null)
                        return;
                    if (optionalLine.A != newLine.A)
                        optionalLine.B = newLine.A;
                    else
                        Console.WriteLine("Same Triangles");
                    return;
                }
                if (optionalLine.X.Equals(newLine.Y) && optionalLine.Y.Equals(newLine.X))
                {
                    if (!optionalLine.Nx.Equals(newLine.Ny) || !optionalLine.Ny.Equals(newLine.Nx) || optionalLine.B != null)
                        return;
                    if (optionalLine.A != newLine.A)
                        optionalLine.B = newLine.A;
                    else
                        Console.WriteLine("Same Triangles");
                    return;
                }
            }
            base.Add(newLine);
        }
    }
}
//http://msdn2.microsoft.com/en-us/library/system.iformattable.aspx
using System;

class Point : IFormattable
{
    public int x, y;

    public Point(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public override String ToString() { return ToString(null, null); }

    public String ToString(String format, IFormatProvider fp)
    {
        // If no format is passed, display like this: (x, y).
        if (format == null) return String.Format("({0}, {1})", x, y);

        // For "x" formatting, return just the x value as a string
        if (format == "x") return x.ToString();

        // For "y" formatting, return just the y value as a string
        if (format == "y") return y.ToString();

        // For any unrecognized format, throw an exception.
        throw new FormatException(String.Format("Invalid format string: '{0}'.", format));
    }
}


public sealed class App
{
    static void Main()
    {
        // Create the object.
        Point p = new Point(5, 98);

        // Test ToString with no formatting.
        Console.WriteLine("This is my point: " + p.ToString());

        // Use custom formatting style "x"
        Console.WriteLine("The point's x value is {0:x}", p);

        // Use custom formatting style "y"
        Console.WriteLine("The point's y value is {0:y}", p);

        try 
        {
            // Use an invalid format; FormatException should be thrown here.
            Console.WriteLine("Invalid way to format a point: {0:XYZ}", p);
        }
        catch (FormatException e)
        {
            Console.WriteLine("The last line could not be displayed: {0}", e.Message);
        }
    }
}

// This code produces the following output.
// 
//  This is my point: (5, 98)
//  The point's x value is 5
//  The point's y value is 98
//  The last line could not be displayed: Invalid format string: 'XYZ'.
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CerasConsoleApp
{
    static class Util
    {
        public static void VisualizePrint(this byte[] bytes, string title)
        {
            Console.WriteLine();
            Console.WriteLine();

            // Pseudo ASCII
            var charArray = Encoding.ASCII.GetString(bytes).Replace("\0", " ").Select(c => char.IsControl(c) ? '_' : c).ToArray();
            var pseudoAscii = new string(charArray);


            // Information
            Console.WriteLine($"{title} ({bytes.Length} bytes)");

            // Hex
            Console.WriteLine(string.Join("", bytes.Select(b => b.ToString("x2"))));

            // Print the pseudo ascii but align the symbols so they are directly below the hex-bytes
            Console.WriteLine(string.Join(" ", pseudoAscii.ToCharArray()));

            Console.WriteLine();
        }
    }
}

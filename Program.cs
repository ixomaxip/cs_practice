using System;
using System.IO;
using System.Collections.Generic;


public class utils
{
    static public Dictionary<string, string> argparse(string [] args)
    {
        var files = new Dictionary<string, string>();

        if (args.Length == 4)
        {
            files.Add(args[0], args[1]);
            files.Add(args[2], args[3]);           
        }
        else
        {
            Console.WriteLine("There is no input and output!");
            return null;
        }

        return files;
    }
    static public string [] reader(string filename)
    {
        var lines = new string[] {};
        try
        {
            lines = File.ReadAllLines(filename, System.Text.Encoding.UTF8);
            foreach(string l in lines)
            {
                Console.WriteLine(l);
            }
            if (lines.Length < 2)
            {
                throw new System.Exception("Input file has less than two lines!");
            }
        }
        catch(Exception err)
        {
            Console.WriteLine(err.Message);
        }

        return lines;
    }
}

class Program
{
    static int Main(string[] args)
    {
        var files = utils.argparse(args);

        try
        {
            Console.WriteLine("Input: {0}", files["-i"]);
            Console.WriteLine("Output: {0}", files["-o"]);

        }
        catch(Exception err)
        {
            Console.WriteLine("Error: {0}", err.Message);
            return 1;
        }

        utils.reader(files["-i"]);
        
        

        return 0;
    }

}


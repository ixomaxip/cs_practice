using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public struct Point
{
    public double x {get;set;}
    public double y {get;set;}

}

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
static public class test_data
    {
        static public int size = 3;
        static public double[] xVals = { 0.4, 0.65, 0.73};
        static public double[] yVals =  {1.5, 2.35, 2.2};
        static public double[] fVals =  {1.7, 2.2, 3.7};

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

        var lines = utils.reader(files["-i"]);
        
        var l_p = new List<string>();

        var fst = lines[0];
        while (fst.Contains("("))
        {
            var p = fst.Split('(', ')')[1];
            fst = fst.Replace("(" + p + ")", "");
            l_p.Add(p);
        }

        var points = new List<Point>();
        foreach ( string p in l_p)
        {
            var pp = p.Split(',');
            // points.Add(new Point().x);
            
            Console.WriteLine(p);
        }
        Console.WriteLine('\n');

        


        foreach(double v in test_data.fVals)
        {
            Console.WriteLine(InterpLagr(v, test_data.yVals, test_data.xVals, test_data.size));
        }

        return 0;
    }

    static double InterpLagr(double f, double[] xs, double[] ys, int n)
    {
        double lp = 0.0;

        for (int i = 0; i < n; i++)
        {
            double a0 = 1;
            for (int j = 0; j < n; j++)
            {
                if (j != i)
                {
                    a0 *= (f - xs[j])/(xs[i] - xs[j]);
                }
            }
            lp += a0 * ys[i];
        }

        return lp;
    }

}


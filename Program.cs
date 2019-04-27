using System;
using System.IO;
using System.Collections.Generic;

class Program
{
    static int Main(string[] args)
    {
        var files = argparse(args);
        var lines = reader(files["-i"]);
        var points = points_parse(lines[0]);
        var fVals = fVals_parse(lines[1]);

        foreach(var f in fVals)
        {
            Console.WriteLine(f);
        }

        foreach(var p in points["xs"].ToArray())
            Console.WriteLine(p);

        Console.WriteLine('\n');

        foreach(double v in test_data.fVals)
        {
            Console.WriteLine(InterpLagr(v, test_data.yVals, test_data.xVals, test_data.size));
        }

        return 0;
    }

    static List<double> fVals_parse(string line)
    {
        var fVals = new List<double>();
        
        foreach(string f in line.Split())
        {
            try
            {
                fVals.Add(double.Parse(f));
            }
            catch(Exception err)
            {
                Console.WriteLine("Error: {0}", err.Message);
            }
        }

        return fVals;
    }

    static Dictionary<string, List<double>> points_parse(string line)
    {
        var points = new Dictionary<string, List<double>>();

        var xs = new List<double>();
        var ys = new List<double>();

        // var fst = lines[0];
        while (line.Contains("("))
        {
            var p = line.Split('(', ')')[1];
            line = line.Replace("(" + p + ")", "");
            var p_splitted = p.Split(',');
            try
            {
                xs.Add(double.Parse(p_splitted[0]));
                ys.Add(double.Parse(p_splitted[1]));
            }
            catch(Exception err)
            {
                Console.WriteLine("Error: {0}", err.Message);
            }
        }
        points.Add("xs", xs);
        points.Add("ys", ys);
        return points;
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
                    a0 *= (f - xs[j])/(xs[i] - xs[j]);
            }
            lp += a0 * ys[i];
        }

        return lp;
    }
    static public Dictionary<string, string> argparse(string [] args)
    {
        var files = new Dictionary<string, string>();

        if (args.Length == 4)
        {
            files.Add(args[0], args[1]);
            files.Add(args[2], args[3]);           
        }
        else
            throw new System.Exception("There is no input and output!");

        try
        {
            Console.WriteLine("Input: {0}", files["-i"]);
            Console.WriteLine("Output: {0}", files["-o"]);
        }
        catch(Exception err)
        {
            Console.WriteLine("Error: {0}", err.Message);
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
            if (lines.Length < 2)
            {
                throw new System.Exception("Not enough lines in input file!");
            }
        }
        catch(Exception err)
        {
            Console.WriteLine(err.Message);
        }

        return lines;
    }

    static public class test_data
    {
        static public int size = 3;
        static public double[] xVals = { 0.4, 0.65, 0.73};
        static public double[] yVals =  {1.5, 2.35, 2.2};
        static public double[] fVals =  {1.7, 2.2, 3.7};

    }
}

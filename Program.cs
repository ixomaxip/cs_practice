using System;
using System.IO;
using System.Collections.Generic;

class Program
{
    static int Main(string[] args)
    {
        var files = argparse(args);
        
        Console.WriteLine("Read input data.");
        var lines = reader(files["-i"]);
        var points = points_parse(lines[0]);
        var fVals = fVals_parse(lines[1]);

        //inverse interpolation
        Interp.xs = points["ys"].ToArray();
        Interp.ys = points["xs"].ToArray();

        using (System.IO.StreamWriter fs = 
            new System.IO.StreamWriter(files["-o"]))
        {
            Console.WriteLine("Start interpolation");
            foreach(var v in fVals)
            {
                fs.WriteLine(Interp.Lagrange(v));
            }

        }
        Console.WriteLine("All done.");

        return 0;
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

        if (!files.ContainsKey("-i") || !files.ContainsKey("-o"))
            throw new System.Exception("There is no keys required. Example: -i input_file -o output_file");

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

    static Dictionary<string, List<double>> points_parse(string line)
    {
        var points = new Dictionary<string, List<double>>();

        var xs = new List<double>();
        var ys = new List<double>();

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
    static public class Interp
    {
        static public double[] xs;
        static public double[] ys;
        static public double Lagrange(double f)
        {
            if (xs.Length != ys.Length)
                throw new System.Exception("There is different lengths for x and y arrays.");

            double lp = 0.0;
            for (int i = 0; i < ys.Length; i++)
            {
                double a0 = 1;
                for (int j = 0; j < xs.Length; j++)
                {
                    if (j != i)
                        a0 *= (f - xs[j])/(xs[i] - xs[j]);
                }
                lp += a0 * ys[i];
            }

            return lp;
        }
    }

    static public class test_data
    {
        static public int size = 3;
        static public double[] xVals = { 0.4, 0.65, 0.73};
        static public double[] yVals =  {1.5, 2.35, 2.2};
        static public double[] fVals =  {1.7, 2.2, 3.7};

    }
}

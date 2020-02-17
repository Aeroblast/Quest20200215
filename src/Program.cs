using System;

namespace Quest20200215
{
    class Program
    {
        const string usage = "Usage: <csv path> --problem<1-4> [arg] ";
        static void Main(string[] args)
        {
            if (args.Length < 2) { Console.WriteLine(usage); return; }
            if (!System.IO.File.Exists(args[0]))
            {
                Console.WriteLine("Data file cannot be found! path:" + args[0]);
                return;
            }
            Node tree = DataProc.ToTree(args[0]);
            switch (args[1])
            {
                case "--problem1":
                    {
                        Console.WriteLine(tree.ToJSON());
                    }
                    break;
                case "--problem2":
                    if (args.Length < 3) { Console.WriteLine("--problem2 <country name or region name>"); return; }
                    tree.ForEach((n) =>
                    {
                        if (n.children.Count == 0)
                            if (n.name == args[2])
                            {
                                Node t = n.parent;
                                string r = n.name;
                                while (t != null)
                                {
                                    r = t + " / " + r;
                                    t = t.parent;
                                }
                                Console.WriteLine(r);
                                Environment.Exit(0);
                            }
                    });
                    break;
                case "--problem3":
                if (args.Length < 3) { Console.WriteLine("--problem3 <region name>"); return; }
                    tree.ForEach((n) =>
                    {
                        if (n.name == args[2])
                        {
                            n.ForEach((m) =>
                            {
                                if (m.children.Count == 0)
                                    Console.WriteLine(m);
                            });
                            Environment.Exit(0);
                        }
                    });
                    break;
                case "--problem4":
                    {
                        DataProc.PrintNode(tree, "", "");
                    }

                    break;
                default: Console.WriteLine("Input command cannot be recognized \n" + usage); return;
            }
        }
    }
}

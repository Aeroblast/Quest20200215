using System.Collections.Generic;
using System.IO;
using System;
using System.Text;
namespace Quest20200215
{
    class DataProc
    {
        //static Dictionary<string, string> code = new Dictionary<string, string>();
        public static Node ToTree(string path)
        {
            Node root = new Node("World");
            string[] data = File.ReadAllLines(path);
            for (int i = 1; i < data.Length; i++)
            {
                string[] s = data[i].Replace("\"", "").Split(',');
                Node a = root;
                int j = 2;
                for (; j <= 6; j += 2)
                {
                    if (s[j] == "") continue;//check code exist
                    string name = s[j + 1];
                    a = a.AddOrGetChild(name);
                }
                string endname = s[j];
                if (!isNumber(s[j + 1]))
                {
                    j++;
                    endname += "," + s[j];
                }
                Info info = new Info();
                info.m49Code = s[j]; j++;
                info.isoCode = s[j]; j++;
                //...
                Node endnode = a.AddOrGetChild(endname);
                endnode.info = info;
            }
            return root;
        }
        const string fm1 = " --+-- ";
        const string fm2 = "   +-- ";
        const string fm3 = "   |   ";
        const string fm4 = "       ";
        public static void PrintNode(Node node, string leadFirstLine, string leadLine)
        {
            if (node.children.Count == 0)
            {
                Console.WriteLine(leadFirstLine + node);
                return;
            }
            PrintNode(node.children[0], leadFirstLine + node + fm1, leadLine + BlankString(node) + fm3);
            if (node.children.Count == 1) return;

            for (int i = 1; i < node.children.Count - 1; i++)
            {
                PrintNode(node.children[i], leadLine + BlankString(node) + fm2, leadLine + BlankString(node) + fm3);
            }
            PrintNode(node.children[node.children.Count - 1], leadLine + BlankString(node) + fm2, leadLine + BlankString(node) + fm4);
        }
        static string BlankString(Node n)
        {
            int length = n.name.Length;
            StringBuilder s = new StringBuilder();
            while (length > 0) { s.Append(' '); length--; }
            return s.ToString();
        }

        static bool isNumber(string s)
        {
            foreach (var i in s)
                if (i < '0' || i > '9') return false;
            return true;
        }
    }
}
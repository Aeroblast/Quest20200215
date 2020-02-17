using System.Collections.Generic;
using System;
namespace Quest20200215
{
    class Node
    {
        public Node parent;
        public List<Node> children = new List<Node>();
        public string name;
        public Info info;
        public Node(string name) { this.name = name; }
        public Node AddOrGetChild(string name)
        {
            foreach (var c in children)
            { if (c.name == name) return c; }
            Node n = new Node(name);
            n.parent = this;
            children.Add(n);
            return n;
        }
        public void ForEach(Action<Node> func)
        {
            func(this);
            foreach (var c in children)
                c.ForEach(func);
        }
        public override string ToString() { return name; }
        public string ToJSON()
        {

            if (children.Count > 0)
            {
                string s = children[0].ToJSON();
                for (int i = 1; i < children.Count; i++)
                    s += "," + children[i].ToJSON();

                return "{" + $"\"{name}\":[{s}]" + "}";
            }
            else return "\"" + name + "\"";
        }

    }
    class Info
    {

        public string m49Code, isoCode;
        //public string countryType;
        //public bool ldc, lldc, sids;
    }
}
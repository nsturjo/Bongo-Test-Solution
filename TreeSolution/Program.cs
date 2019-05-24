using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tree
{
    class Program
    {
        // global variable for storing all nodes of the tree
        public static IDictionary<int, int?> allNodes = new Dictionary<int, int?>();
        // to store all anchestors of a node
        public static List<int> anchestors = new List<int>();

        // give class
        public class Node
        {
            public int value;
            public int? parent;

            // constructor to initialize data
            public Node(int _value, int? _parent)
            {
                value = _value;
                parent = _parent;
            }
        }


        public class Tree
        {
            // method to insert a node in tree
            public void InsertNode(Node _node)
            {
                allNodes.Add(_node.value, _node.parent);
            }
        }



        static void Main(string[] args)
        {
            Tree t1 = new Tree();

            //inserting all the nodes in 'allNodes' dictionary
            t1.InsertNode(new Node(1, 0));
            t1.InsertNode(new Node(2, 1));
            t1.InsertNode(new Node(5, 2));
            t1.InsertNode(new Node(4, 2));
            t1.InsertNode(new Node(8, 4));
            t1.InsertNode(new Node(9, 4));
            t1.InsertNode(new Node(3, 1));
            t1.InsertNode(new Node(6, 3));
            t1.InsertNode(new Node(7, 3));

            // loop to use re iterate the test
            var yesNo = "y";
            do
            {
                Solution3();

                Console.WriteLine("\n\nPress 'y' to perform another test\n");
                yesNo = Console.ReadLine().ToLower();
            } while (yesNo == "y");


            Console.WriteLine("\n\nPress enter to close...");
            Console.ReadLine();
        }

        public static void Solution3()
        {
            int a = 0;
            int b = 0;

            try
            {
                Console.WriteLine("Please enter Node 1");
                a = int.Parse(Console.ReadLine());
                Console.WriteLine("Please enter Node 2");
                b = int.Parse(Console.ReadLine());
            }
            catch
            {
                Console.WriteLine("Wrong input!\n");
                Solution3();
            }

            var result = lca(a, b);

            //output display message
            if (result == -1) Console.WriteLine("Wrong input!");
            else if (result == -2) Console.WriteLine("Root nodes doesn't have anchestors!");
            else if (result == 0) Console.WriteLine("{0} and {1} doesn't have common anchestor!", a, b);
            else
            {
                Console.WriteLine("Least common anchestor of {0} and {1} is: " + result, a, b);
            }
        }

        //method to find least common anchestor
        private static int lca(int node1, int node2)
        {
            //finding first parent
            var node1Parents = allNodes.Where(a => a.Key == node1).Select(a => a.Value).FirstOrDefault();
            var node2Parents = allNodes.Where(a => a.Key == node2).Select(a => a.Value).FirstOrDefault();

            // conditions to find least common anchestor from first parent
            if (node1Parents == 0 && node2Parents == 0) return -2;
            if (node1Parents == null || node2Parents == null) return -1;
            if (node1Parents == node2Parents) return (int)node1Parents;
            if (node1Parents.Value == node2) return node2;
            if (node2Parents.Value == node1) return node1;

            // conditions to find least common anchestor from all anchestors
            var anchestor1 = GetAnchestors(node1);
            anchestors.Clear();
            var anchestor2 = GetAnchestors(node2);
            var commonAnchestors = anchestor1.Intersect(anchestor2);
            if (commonAnchestors != null)
            {
                return commonAnchestors.Min();
            }

            else return 0;
        }

        // recursive method to get all anchestors of a given node
        private static List<int> GetAnchestors(int node)
        {
            int parent;

            if (node == 1) return anchestors;
            else
                parent = (int)allNodes.Where(a => a.Key == node).Select(a => a.Value).FirstOrDefault();
            anchestors.Add(parent);
            GetAnchestors(parent);
            return anchestors;
        }
    }
}

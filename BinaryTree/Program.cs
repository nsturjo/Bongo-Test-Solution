using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryTree
{
    class Program
    {
        public class Node
        {
            public int Data;
            public Node LeftChild;
            public Node RightChild;
        }

        public class BinaryTree
        {
            public Node root;

            public BinaryTree()
            {
                root = null;
            }

            public void Add(int data)
            {
                Node newNode = new Node();
                newNode.Data = data;

                if (root == null)
                {
                    root = newNode;
                }
                else
                {
                    Node current = root;
                    Node parent;

                    while (true)
                    {
                        parent = current;
                        if (data < current.Data)
                        {
                            current = current.LeftChild;
                            if (current == null)
                            {
                                parent.LeftChild = newNode;
                                break;
                            }
                        }
                        else
                        {
                            current = current.RightChild;
                            if (current == null)
                            {
                                parent.RightChild = newNode;
                                break;
                            }
                        }
                    }
                }
            }
        }

        static void Main(string[] args)
        {
            BinaryTree t1 = new BinaryTree();

            t1.Add(50);
            t1.Add(40);
            t1.Add(10);
            t1.Add(200);


            Console.WriteLine(t1.ToString());
            Console.ReadLine();
        }
    }
}

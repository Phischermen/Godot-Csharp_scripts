using Godot;
using Godot.Collections;
using System.Collections.Generic;

namespace NodeUtility
{
    public static class Extensions
    {
        //<summary>
        //Find the first child node of type T
        //</summary>
        public static T Find<T>(this Node root, bool recursive = false) where T : Node
        {
            for (int i = 0; i < root.GetChildCount(); ++i)
            {
                Node child = root.GetChild(i);
                if (child is T) return (T)child;
                if (recursive && child.GetChildCount() > 0)
                {
                    T grandChild = Find<T>(child, true);
                    if (grandChild != null) return grandChild;
                }
            }
            return null;
        }
        //<summary>
        //Find all nodes of type T in children
        //</summary>
        public static Array<T> FindAll<T>(this Node root, bool recursive = false) where T : Node
        {
            var nodes = new Array<T>();
            for (int i = 0; i < root.GetChildCount(); ++i)
            {
                Node child = root.GetChild(i);
                if (child is T) nodes.Add((T)(object)child);
                if (recursive && child.GetChildCount() > 0)
                {
                    var grandChildren = FindAll<T>(child, true);
                    if (grandChildren.Count != 0)
                    {
                        foreach (var grandChild in grandChildren)
                        {
                            nodes.Add(grandChild);
                        }
                    }
                }
            }
            return nodes;
        }
        //<summary>
        //Find first parent node of type T
        //</summary>
        public static T FindParent<T>(this Node root) where T : Node
        {
            root = root.GetParentOrNull<Node>();
            while (root != null)
            {
                if (root is T) break;
                root = root.GetParentOrNull<Node>();
            }
            return (T)root;
        }
        //<summary>
        //Find all parent nodes of type T
        //</summary>
        public static Array<T> FindAllParent<T>(this Node root) where T : Node
        {
            Array<T> nodes = new Array<T>();
            root = root.GetParentOrNull<Node>();
            while (root != null)
            {
                if (root is T) nodes.Add((T)root);
                root = root.GetParentOrNull<Node>();
            }
            return nodes;
        }
        //<summary>
        //Find node whose name matches its type
        //</summary>
        public static T GetNodeWithNameType<T>(this Node root) where T : Node
        {
            string name = typeof(T).ToString();
            int pos = name.FindLast(".");
            return root.GetNode<T>(name.Right(pos + 1));
        }
        //<summary>
        //Check if node is a child of root
        //</summary>
        public static bool IsUnderRoot(this Node node)
        {
            return node.GetTree().Root == node.GetParent();
        }
    }
}




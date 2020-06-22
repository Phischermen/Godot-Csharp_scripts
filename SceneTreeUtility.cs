using Godot;
using System;

namespace SceneTreeUtility
{
    public static class Extensions
    {
        public static void ProcessNodesInGroup<T>(this SceneTree tree, string group, Action<T> action) where T : Node
        {
            foreach (var obj in tree.GetNodesInGroup(group))
            {
                try
                {
                    var node = (T)obj;
                    action.Invoke(node);
                }
                catch (System.InvalidCastException)
                {
                    GD.PushWarning(obj.ToString() + " was found in group: " + group);
                }
            }
        }
    }
}

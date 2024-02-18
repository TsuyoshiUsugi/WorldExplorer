using GraphProcessor;
using System;
using UnityEngine;

namespace TsuyoshiBehaviorTree
{
    /// <summary>
    /// ビヘイビアツリーのルートノード
    /// </summary>
    [Serializable, NodeMenuItem("Root/Root")]
    public class BehaviorTreeRootNode : Node
    {
        [Output(name = "Child")]
        public Node ChildNode;
        public override NodeState OnUpdate()
        {
            if (ChildNode == null)
            {
                return NodeState.Failure;
            }
            Debug.Log(ChildNode is Log);
            return ChildNode.OnUpdate();
        }
    }
}

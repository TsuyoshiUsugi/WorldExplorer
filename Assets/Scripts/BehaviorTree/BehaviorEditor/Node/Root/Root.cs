using GraphProcessor;
using System;
using UnityEngine;

namespace TsuyoshiBehaviorTree
{
    /// <summary>
    /// ビヘイビアツリーのルートノード
    /// </summary>
    [Serializable, NodeMenuItem("Root/Root")]
    public class Root : Node
    {
        [Output(name = "Child")]
        public Node ChildNode;
        
        // Override OnStart method for the root node
        public override void OnStart()
        {
            base.OnStart();
        }

        // Override OnUpdate method for the root node
        public override NodeState OnUpdate()
        {
            if (ChildNode == null)
            {
                return NodeState.Failure;
            }
            // Call the base OnUpdate method
            base.OnUpdate();

            _state = ChildNode.OnUpdate();
            return _state;
        }

        // Override OnEnd method for the root node
        public override void OnEnd()
        {
            base.OnEnd();
        }
    }
}

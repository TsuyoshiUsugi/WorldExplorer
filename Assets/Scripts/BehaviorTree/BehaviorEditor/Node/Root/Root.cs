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
        //Todo: 親ノードを追加できないようにする
        
        [Input(name = "Child")]
        public Node ChildNode;

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
            Debug.Log("ビヘイビアツリーの全ノード実行終了");
        }
    }
}

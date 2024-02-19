using GraphProcessor;
using System;
using System.Linq;
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
        
        [Output(name = "Child"), Vertical]
        public Node ChildNode;
        
        protected override void Process()
        {
            base.Process();
            ChildNode = this.GetOutputNodes().FirstOrDefault() as Node;
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
            Debug.Log("ビヘイビアツリーの全ノード実行終了");
        }
    }
}

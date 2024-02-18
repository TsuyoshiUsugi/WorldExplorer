using System;
using GraphProcessor;
using UnityEngine;

namespace TsuyoshiBehaviorTree
{
    /// <summary>
    /// ログ出力を行うアクションノード
    /// </summary>
    [Serializable, NodeMenuItem("ActionNode/Log")]
    public class Log : ActionNode
    {
        [Output(name = "Parent")]
        public Node Output;
        
        [SerializeField, TextArea(1, 1)] private string _message;
        
        protected override void Process()
        {
            Output = this;
        }

        protected override NodeState OnUpdateMethod()
        {
            Debug.Log(_message);
            return NodeState.Success;
        }
    }
}

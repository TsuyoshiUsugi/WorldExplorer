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

        
        [SerializeField, TextArea(1, 1)] private string _message;


        protected override NodeState OnUpdateMethod()
        {
            Debug.Log(_message);
            return NodeState.Success;
        }
    }
}

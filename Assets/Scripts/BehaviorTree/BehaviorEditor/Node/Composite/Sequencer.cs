using System;
using UnityEngine;
using GraphProcessor;

namespace  TsuyoshiBehaviorTree
{
    /// <summary>
    /// シーケンサーノード
    /// 子が一つでも失敗したら失敗を返す。全て成功したら成功を返す
    /// </summary>
    [Serializable, NodeMenuItem("Composite/Sequencer")]
    public class Sequencer : Branch
    {
        public override NodeState OnUpdate()
        {
            base.OnUpdate();
            if (_childIndex >= _childNode.Count)
            {
                Debug.LogError("子ノードがありません");
                return NodeState.Failure;
            }
            var childState = _childNode[_childIndex].OnUpdate();
            if (childState == NodeState.Success)
            {   //現在見ている子が成功したらOnEndを呼び出して次の子を評価する
                _childNode[_childIndex].OnEnd();
                _childIndex++;
            }

            _state = EvaluateChild();
            return _state;
        }
        
        /// <summary>
        /// 子のステータスを評価する
        /// </summary>
        /// <returns></returns>
        protected override NodeState EvaluateChild()
        {
            var result = NodeState.Waiting;
            foreach (var child in _childNode)
            {
                if (child.State == NodeState.Failure)
                {
                    result = NodeState.Failure;
                    break;
                }
                else if (child.State == NodeState.Running || child.State == NodeState.Waiting)
                {
                    result = NodeState.Running;
                    break;
                }

                result = NodeState.Success;
            }

            return result;
        }
    }
}

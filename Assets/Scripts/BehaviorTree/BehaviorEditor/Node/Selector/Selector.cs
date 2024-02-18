using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TsuyoshiBehaviorTree
{
    /// <summary>
    /// 子が一つでも成功したら成功を返す。全て失敗したら失敗を返す
    /// </summary>
    public class Selector : Branch
    {
       public override NodeState OnUpdate()
        {
            base.OnUpdate();
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
            int failureCount = 0;
            foreach (var child in _childNode)
            {
                if (child.State == NodeState.Success || child.State == NodeState.Failure)
                {   //既に実行結果のでているものはスキップ
                    if (child.State == NodeState.Failure)
                    {
                        failureCount++;
                        _childIndex++;
                    }
                    continue;
                }

                child.OnUpdate();
                if (child.State != NodeState.Success)
                {
                    _childIndex++;
                    if (child.State == NodeState.Failure)
                    {
                        failureCount++;
                        child.OnEnd();
                    }
                    continue;
                }
                
                //一つでも成功したらほかをWaitingにして終了
                child.OnEnd();
                result = child.State;
                for (int i = 0; i < _childNode.Count; i++)
                {
                    if (i == _childIndex)
                    {
                        continue;
                    }

                    _childNode[i].State = NodeState.Waiting;
                }
                return result;
            }
            
            //全て失敗したら失敗を返す
            if (failureCount >= _childNode.Count)
            {
                result = NodeState.Failure;
            }
            else
            {
                result = NodeState.Running;
            }

            return result;
        }
    }
}

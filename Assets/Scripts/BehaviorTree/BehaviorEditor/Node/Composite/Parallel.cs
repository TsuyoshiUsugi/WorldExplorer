using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GraphProcessor;
using System;

namespace  TsuyoshiBehaviorTree
{
    /// <summary>
    /// 子を同時に実行し、すべての子が成功したら成功を返す。一つでも失敗したら失敗を返す
    /// </summary>
    [Serializable, NodeMenuItem("Composite/Parallel")]
    public class Parallel : Branch
    {
        public override NodeState OnUpdate()
        {
            base.OnUpdate();
            _state = EvaluateChild();
            return _state;
        }

        protected override NodeState EvaluateChild()
        {
            var result = NodeState.Waiting;
            int successCount = 0;
            int runningCount = 0;
            foreach (var child in _childNode)
            {
                if (child.State == NodeState.Success)
                {   //既に実行結果のでているものはスキップ
                    successCount++;
                    continue;
                }

                child.OnUpdate();
                if (child.State != NodeState.Failure)
                {
                    if (child.State == NodeState.Running)
                    {
                        runningCount++;
                    }
                    else if (child.State == NodeState.Success)
                    {
                        successCount++;
                        child.OnEnd();
                    }
                    _childIndex++;
                    continue;
                }
                
                //一つでも失敗したらほかをWaitingにして終了
                result = NodeState.Failure;
                child.OnEnd();
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
            if (runningCount > 0)
            {   //実行中の子があるならRunningを返す
                result = NodeState.Running;
            }
            else if (successCount == _childNode.Count)
            {   //全て成功したら成功を返す
                result = NodeState.Success;
            }

            return result;
        }
    }
}

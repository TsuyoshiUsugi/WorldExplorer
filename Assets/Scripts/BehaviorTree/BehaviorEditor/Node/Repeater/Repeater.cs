using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TsuyoshiBehaviorTree
{
    /// <summary>
    /// 規定の回数を子ノードが返すまでRunningを返す。条件を満たしたらSuccess
    /// </summary>
    public class Repeater : Branch
    {
        [SerializeField] private int _repeatCount = 1;
        private int _currentCount = 0;

        public override NodeState OnUpdate()
        {
            base.OnUpdate();
            if (_childNode.Count == null || _childNode.Count == 0)
            {
                Debug.LogError("子ノードがありません");
                return NodeState.Failure;
            }

            if (_repeatCount == 0)
            {
                Debug.LogError("繰り返し回数が0です");
                return NodeState.Failure;
            }

            _state = EvaluateChild();
            if (_state == NodeState.Running)
            {
                OnUpdate();
            }

            return _state;
        }
        
        protected override NodeState EvaluateChild()
        {
            var result = NodeState.Waiting;
            int finishCount = 0;
            foreach (var child in _childNode)
            {
                if (child.State == NodeState.Failure || child.State == NodeState.Success)
                {   //既に実行結果のでているものはスキップ
                    finishCount++;
                    continue;
                }

                child.OnUpdate();
                if (child.State == NodeState.Failure || child.State == NodeState.Success)
                {
                    child.OnEnd();
                    finishCount++;
                }
            }
            if (finishCount < _childNode.Count)
            {   //まだ子が実行中ならRunningを返す
                return NodeState.Running;
            }

            _currentCount++;
            if (_currentCount >= _repeatCount)
            {
                result = NodeState.Success;
                return result;
            }

            result = NodeState.Running;
            SetChildWaiting(_childNode);
            return result;
        }
        
        /// <summary>
        /// 子ノードをWaitingにする
        /// </summary>
        /// <param name="childNode"></param>
        private void SetChildWaiting(List<Node> childNode)
        {
            foreach (var child in childNode)
            {
                child.State = NodeState.Waiting;
                var branch = child as Branch;
                if (branch == null)
                {
                    continue;
                }
                SetChildWaiting(branch.GetChildren());
            }
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using GraphProcessor;
using UnityEngine;

namespace TsuyoshiBehaviorTree
{
    [Serializable, NodeMenuItem("Composite/RandomSelector")]
    public class RandomSelector : Branch
    {
        private int _childIndex;
        public override void OnStart()
        {
            base.OnStart();
            if (_childNode == null || _childNode.Count == 0)
            {
                Debug.LogError("子ノードがありません");
                _state = NodeState.Failure;
                return; 
            }
            var random = new System.Random();
            _childIndex = random.Next(0, _childNode.Count);
        }
        
        public override NodeState OnUpdate()
        {
            base.OnUpdate();
            _state = EvaluateChild();
            return _state;
        }
        
        /// <summary>
        /// ランダムに選択した子のステータスを評価する
        /// </summary>
        /// <returns></returns>
        protected override NodeState EvaluateChild()
        {
            var result = NodeState.Waiting;
            
            var child = _childNode[_childIndex];
            result = child.OnUpdate();
            if (result == NodeState.Running)
            {
                return result;
            }
            if (result == NodeState.Success || result == NodeState.Failure)
            {
                child.OnEnd();
                return result;
            }
            return result;
        }
    }
}

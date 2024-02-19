using System;
using System.Collections;
using System.Collections.Generic;
using GraphProcessor;
using UnityEngine;

namespace  TsuyoshiBehaviorTree
{
    /// <summary>
    /// 条件を満たしているなら子のStateを返す。そうでないならFailureを返す
    /// 子を一つだけ持つ
    /// </summary>
    [Serializable, NodeMenuItem("Decorator/Decorator")]
    public class Decorator : Branch
    {
        [SerializeReference, SubclassSelector]
        public DecoratorBase _decorator;
        
        public override void OnStart()
        {
            base.OnStart();
            if (_decorator != null)
            {
                _decorator.SetOwner(_owner);
            }
        }
        
        public override NodeState OnUpdate()
        {
            base.OnUpdate();
            if (_childNode.Count == null || _childNode.Count == 0)
            {
                Debug.LogError("子ノードがありません");
                return NodeState.Failure;
            }
            if (_childNode.Count > 1)
            {   //TODO: そもそもDecoratorノードが複数の子を持てないようにすべき
                Debug.LogError("子ノードが複数あります");
                return NodeState.Failure;
            }
            if (_decorator == null)
            {
                Debug.LogError("評価条件がありません");
                return NodeState.Failure;
            }

            _state = EvaluateChild();
            if (_state == NodeState.Running)
            {   //実行中なら子のステータスを評価するために自身を再呼び出し
                OnUpdate();
            }

            return _state;
        }
        
        /// <summary>
        /// 子のステータスを評価する
        /// </summary>
        /// <returns></returns>
        protected override NodeState EvaluateChild()
        {
            var result = NodeState.Waiting;
            _state = _decorator.Evaluate();
            if (_state == NodeState.Failure)
            {   //判定が通らないなら強制的にFailureを返す
                return NodeState.Failure;
            }
            else if (_state == NodeState.Success)
            {   //判定が通ったら子のステータスを返す
                result = _childNode[0].OnUpdate();
            }
            if (result == NodeState.Success || result == NodeState.Failure)
            {
                _childNode[0].OnEnd();
            }

            return result;
        }
    }
    
}

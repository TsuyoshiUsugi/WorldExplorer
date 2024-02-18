using System.Collections;
using System.Collections.Generic;
using GraphProcessor;
using TsuyoshiBehaviorTree;
using UnityEngine;

namespace TsuyoshiBehaviorTree
{
    /// <summary>
    /// 子を持つことのできるノードのベースクラス
    /// </summary>
    public class Branch : Node
    {
        [Output("ChildNode")]
        public List<Node> _childNode;
        protected int _childIndex = 0;

        public override void OnStart()
        {
            base.OnStart();
            _childIndex = 0;
            if (_childNode.Count == 0)
            {
                Debug.LogError("子ノードがありません");
                return;
            }
        }
        
        /// <summary>
        /// 子のステータスを評価する
        /// </summary>
        /// <returns></returns>
        protected virtual NodeState EvaluateChild()
        {
            return NodeState.Waiting;
        }

        public virtual List<Node> GetChildren()
        {
            return _childNode;
        }
    }
}

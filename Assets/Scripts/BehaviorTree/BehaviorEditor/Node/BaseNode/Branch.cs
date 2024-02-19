using System;
using System.Collections;
using System.Collections.Generic;
using GraphProcessor;
using UnityEngine;

namespace TsuyoshiBehaviorTree
{
    /// <summary>
    /// 子を持つことのできるノードのベースクラス
    /// </summary>
    public class Branch : Node
    {
        [Output(name = "Parent")]
        public Node Output;

        [Input(name = "Child", allowMultiple = true)]
        public Node Child;
        
        [NonSerialized]
        public List<Node> _childNode = new List<Node>();
        protected int _childIndex = 0;

        /// <summary>
        /// Childに繋がっているノードを取得してリストに追加する
        /// </summary>
        protected override void Process()
        {
            var edges = Child.GetAllEdges();
            foreach (var edge in edges)
            {
                if (edge.inputNode is Node)
                {
                    _childNode.Add(edge.inputNode as Node);
                }
            }
        }

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

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GraphProcessor;
using UnityEngine;

namespace TsuyoshiBehaviorTree
{
    /// <summary>
    /// 子を持つことのできるノードのベースクラス
    /// ここでは子の設定処理や評価処理の基底クラスを実装する
    /// </summary>
    public class Branch : Node
    {
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
            base.Process();
            _childIndex = 0;
            var nodes = this.GetInputNodes();
            //エディターで上に表示されているものから順番に実行したいのでY座標でソート
            //TODO: ここでソートするのはよくないかもしれない
            nodes = nodes.OrderBy(x => x.position.y);
            foreach (var node in nodes)
            {
                if (node is Node)
                {
                    _childNode.Add(node as Node);
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

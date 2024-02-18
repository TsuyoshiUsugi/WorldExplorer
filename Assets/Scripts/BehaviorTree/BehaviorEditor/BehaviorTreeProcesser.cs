using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using GraphProcessor;

namespace  TsuyoshiBehaviorTree
{
    /// <summary>
    /// ツリーを実行するクラス
    /// </summary>
    public class BehaviorTreeProcesser : BaseGraphProcessor
    {
        //現在実行中のノード
        private Node _runningNode;
        //最終的な実行結果
        private NodeState _resultState = NodeState.Waiting;
        
        /// <summary>
        /// 継承したコンストラクタ
        /// ノード取得とグラフの取得を行う
        /// </summary>
        /// <param name="graph"></param>
        public BehaviorTreeProcesser(BaseGraph graph) : base(graph)
        {
        }

        /// <summary>
        /// ここでスクリプタブルオブジェクトから最初のノードを取得する
        /// </summary>
        public override void UpdateComputeOrder()
        {
            _runningNode = graph.nodes.FirstOrDefault(n => n is Root) as Node;
        }

        public override void Run()
        {
            Debug.Log("ビヘイビアツリー起動");
            if (_resultState != NodeState.Waiting)
            {
                Debug.LogError("既に実行中です");
                return;
            }
            _resultState = NodeState.Running;
            _runningNode.OnStart();
        }

        /// <summary>
        /// ビヘイビアツリーの更新処理
        /// </summary>
        public void OnUpdate()
        {
            if (_resultState == NodeState.Success || _resultState == NodeState.Failure)
            {
                return;
            }

            if (_resultState == NodeState.Waiting)
            {
                Debug.LogError("実行していません");
                return;
            }
            _resultState = _runningNode.OnUpdate();
            if (_resultState == NodeState.Success || _resultState == NodeState.Failure)
            {
                _runningNode.OnEnd();
                OnEnd();
                Debug.Log($"ビヘイビアツリー終了:結果{_resultState}");
            }
        }

        public void OnEnd()
        {
            if (_resultState != NodeState.Success && _resultState != NodeState.Failure)
            {
                Debug.LogError("結果が出ていないのに終了が呼ばれました");
                return;
            }
            return;
        }
    }
    
}

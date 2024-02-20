using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using GraphProcessor;
using Unity.Jobs;

namespace  TsuyoshiBehaviorTree
{
    /// <summary>
    /// ツリーを実行するクラス
    /// 利用方法としては、初期化とRunを呼び出し、その後Updateをメインループで呼び出す
    /// </summary>
    public class BehaviorTreeProcesser : BaseGraphProcessor
    {
        //現在実行中のノード
        private Node _runningNode;
        //最終的な実行結果
        private NodeState _resultState = NodeState.Waiting;
        private List<BaseNode> _process;
        private GameObject _owner;
        
        /// <summary>
        /// 継承したコンストラクタ
        /// ノード取得とグラフの取得を行う
        /// </summary>
        /// <param name="graph"></param>
        public BehaviorTreeProcesser(BaseGraph graph, GameObject owner) : base(graph)
        {
            _owner = owner;
        }

        /// <summary>
        /// ここでスクリプタブルオブジェクトから最初のノードを取得する
        /// </summary>
        public override void UpdateComputeOrder()
        {
            _runningNode = graph.nodes.FirstOrDefault(n => n is Root) as Node;
            _process = graph.nodes.OrderBy(n => n.computeOrder).ToList();
        }

        public override void Run()
        {   //ToDo: これ不要かも
            Initialize();
        }

        /// <summary>
        /// ビヘイビアツリーの初期化処理を行う
        /// 一度は実行する必要がある
        /// </summary>
        private void Initialize()
        {
            Debug.Log("ビヘイビアツリー起動");
            _resultState = NodeState.Waiting;

            for (int i = 0; i < _process.Count; i++)
            {
                _process[i].OnProcess();
                var node = (Node)_process[i];
                node?.SetOwnerObject(_owner);
            }
            JobHandle.ScheduleBatchedJobs();
            
            if (_resultState != NodeState.Waiting)
            {
                Debug.LogError("既に実行中です");
                return;
            }
            _resultState = NodeState.Running;
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

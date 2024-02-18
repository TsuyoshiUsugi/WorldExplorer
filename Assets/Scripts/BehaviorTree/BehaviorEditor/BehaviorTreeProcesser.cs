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
        private Node _rootNode;
        public bool IsRunning { get; private set; }
        
        public BehaviorTreeProcesser(BaseGraph graph) : base(graph)
        {
        }

        public override void UpdateComputeOrder()
        {
            _rootNode = graph.nodes.FirstOrDefault(n => n is BehaviorTreeRootNode) as Node;
        }

        public override void Run()
        {
            _rootNode.OnUpdate();
        }
    }
    
}

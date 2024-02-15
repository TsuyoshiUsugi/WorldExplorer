using GraphProcessor;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Jobs;

namespace TsuyoshiBehaviorTree
{
    public class BehavioreTreeGraphProcesser : BaseGraphProcessor
    {
        private List<BaseNode> _processList = new List<BaseNode>();

        public BehavioreTreeGraphProcesser(BaseGraph graph) : base(graph)
        {
        }

        public override void Run()
        {
            var count = _processList.Count;
            for (int i = 0; i < count; i++)
            {
                _processList[i].OnProcess();
            }

            JobHandle.ScheduleBatchedJobs();
        }

        public override void UpdateComputeOrder()
        {
            _processList = graph.nodes.OrderBy(x => x.computeOrder).ToList();
        }
    }
}

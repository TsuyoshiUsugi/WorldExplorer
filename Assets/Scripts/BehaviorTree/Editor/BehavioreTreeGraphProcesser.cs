using GraphProcessor;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Jobs;

namespace TsuyoshiBehaviorTree
{
    public class BehavioreTreeGraphProcesser : BaseGraphProcessor
    {
        private List<BaseNode> _processList;

        public BehavioreTreeGraphProcesser(BaseGraph graph) : base(graph)
        {
        }

        public override void Run()
        {
            _processList = graph.nodes.OrderBy(x => x.computeOrder).ToList();
        }

        public override void UpdateComputeOrder()
        {
            var count = _processList.Count;
            for (int i = 0; i < count; i++)
            {
                _processList[i].OnProcess();
            }

            JobHandle.ScheduleBatchedJobs();
        }
    }
}

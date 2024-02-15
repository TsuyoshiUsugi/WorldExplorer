using GraphProcessor;
using System;

namespace TsuyoshiBehaviorTree
{
    [Serializable, NodeMenuItem("Root/Root")]
    public class BehaviorTreeRootNode : BaseNode
    {
        [Output(name = "Child")]
        public IBehaviorNode ChildNode;

        public void AddChildNode(IBehaviorNode node)
        {
            ChildNode = node;
        }

        protected override void Process()
        {

        }
    }
}

using UnityEditor.Experimental.GraphView;

namespace TsuyoshiBehaviorTree
{
    public class ExampleNode : Node
    {
        public ExampleNode()
        {
            title = "ExampleNode";
            var inputoirt = Port.Create<Edge>(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(bool));
            inputoirt.portName = "Input";
            inputContainer.Add(inputoirt);

            var outputPort = Port.Create<Edge>(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(bool));
            outputPort.portName = "Output";
            outputContainer.Add(outputPort);
        }
    }
}


using System;
using GraphProcessor;

[Serializable]
[NodeMenuItem("Custom/Add")]
public class AddNode : BaseNode
{
    [Input(name = "A")]
    public float _inputl1;
    [Input(name = "B")]
    public float _inputl2;

    [Output(name = "Out", allowMultiple = false)]
    public float _output;

    public override string name => "Add";

    protected override void Process()
    {
        _output = _inputl1 + _inputl2;
    }
}

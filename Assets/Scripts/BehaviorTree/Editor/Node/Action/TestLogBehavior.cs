using UnityEngine;
using TsuyoshiBehaviorTree;
using GraphProcessor;

[System.Serializable]
[NodeMenuItem("Action/TestLog")]
public class TestLogBehavior : ActionNode
{
    [Input(name = "Root")]
    public IBehaviorNode Root;

    public override bool Execute()
    {
        int i = 0;
        int max = 100;
        while (i < max)
        {
            Debug.Log("TestLogBehavior");
            i++;
        }
        return true;
    }
}

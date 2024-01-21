using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TsuyoshiBehaviorTree;

[System.Serializable]
public class TestLogBehavior : ActionNode
{
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

using System;
using System.Collections;
using System.Collections.Generic;
using GraphProcessor;
using TsuyoshiBehaviorTree;
using UnityEngine;

[Serializable, NodeMenuItem("Decorator/HasRequiredMoney")]
public class HasRequiredMoney : DecoratorBase
{
    [SerializeField]
    private int _requiredMoney = 200;
    public override NodeState Evaluate()
    {
        if (_owner.GetComponent<Money>().money >= _requiredMoney)
        {
            return NodeState.Success;
        }
        else
        {
            return NodeState.Failure;
        }
    }
}
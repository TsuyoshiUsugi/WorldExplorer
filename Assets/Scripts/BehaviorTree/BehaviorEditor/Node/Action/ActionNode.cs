using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace  TsuyoshiBehaviorTree
{
    /// <summary>
    /// アクションノードの基底クラス
    /// </summary>
    public abstract class ActionNode : Node
    {
        protected abstract NodeState OnUpdateMethod();

        public override NodeState OnUpdate()
        {
            base.OnUpdate();
            return OnUpdateMethod();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TsuyoshiBehaviorTree
{
    /// <summary>
    /// ActionNodeで指定できるインターフェース
    /// </summary>
    public interface IAnyAction
    {
        public NodeState OnUpdate();
        public IAnyAction Clone();
    }
}

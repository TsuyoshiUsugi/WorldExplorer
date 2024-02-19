using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace  TsuyoshiBehaviorTree
{
    /// <summary>
    /// 条件ノードの基底クラス
    /// </summary>
    [Serializable]
    public abstract class Condition : Node
    {
        /// <summary>
        /// 継承した条件ノードで実行される処理を書く
        /// </summary>
        /// <returns></returns>
        protected abstract NodeState OnUpdateMethod();
        
        public override NodeState OnUpdate()
        {
            base.OnUpdate();
            return OnUpdateMethod();
        }
    }
}

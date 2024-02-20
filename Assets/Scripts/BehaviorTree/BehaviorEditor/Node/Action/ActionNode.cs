using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GraphProcessor;

namespace  TsuyoshiBehaviorTree
{
    /// <summary>
    /// アクションノードの基底クラス
    /// </summary>
    public abstract class ActionNode : Node
    {
        /// <summary>
        /// 継承したアクションノードで実行される処理を書く
        /// </summary>
        /// <returns></returns>
        protected abstract NodeState OnUpdateMethod();

        /// <summary>
        /// 継承されたアクションノードの処理を実行し、結果を返す
        /// </summary>
        /// <returns></returns>
        public override NodeState OnUpdate()
        {
            base.OnUpdate();
            return OnUpdateMethod();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TsuyoshiBehaviorTree
{
    /// <summary>
    /// 行動ノードの基底クラス
    /// UpdataはWhile文で回す
    /// 成功の可否はboolで返す
    /// </summary>
    [System.Serializable]
    public abstract class ActionNode : IBehaviorNode
    {
        public abstract bool Execute();
    }
}

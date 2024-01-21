using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TsuyoshiBehaviorTree
{
    /// <summary>
    /// ビヘイビアツリーのノードのインターフェース
    /// </summary>
    public interface IBehaviorNode
    {
        bool Execute();
    }
}

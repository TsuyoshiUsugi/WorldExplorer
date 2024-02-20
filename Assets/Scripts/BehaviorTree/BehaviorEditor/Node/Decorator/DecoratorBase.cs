using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace  TsuyoshiBehaviorTree
{
    /// <summary>
    /// decoratorノードの基底クラス
    /// </summary>
    public abstract class DecoratorBase
    {
        protected GameObject _owner;
        
        public void SetOwner(GameObject owner)
        {
            _owner = owner;
        }
        
        public abstract NodeState Evaluate();
    }
}

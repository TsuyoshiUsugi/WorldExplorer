using System;
using GraphProcessor;
using UnityEngine;

namespace  TsuyoshiBehaviorTree
{
    [Serializable, NodeMenuItem("ActionNode/AnyAction")]
    public class AnyAction : ActionNode
    {
        [SerializeReference, SubclassSelector] public IAnyAction _action;

        public override void OnStart()
        {
            base.OnStart();
            if (_action == null)
            {
                Debug.LogError("Action is null");
                _state = NodeState.Failure;
                return;
            }
            
            _action = _action.Clone();
        }
        
        protected override NodeState OnUpdateMethod()
        {
            _state = _action.OnUpdate();
            return _state;
        }
    }
}

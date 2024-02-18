using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GraphProcessor;

namespace TsuyoshiBehaviorTree
{
    public enum NodeState
    {
        Running,
        Success,
        Failure,
        Inactive,
        Completed,
    }
    
    /// <summary>
    /// ビヘイビアツリーのノードの基底クラス
    /// </summary>
    [Serializable]
    public class Node : BaseNode
    {
        /// <summary>
        /// 名前
        /// </summary>
        private string _name;
        
        /// <summary>
        /// 説明
        /// </summary>
        [SerializeField] private string _description;
        
        private int _index = -1;
        public int Index => _index;
        
        /// <summary>
        /// 親ノード
        /// </summary>
        private Node _parent;
        
        /// <summary>
        /// 状態
        /// </summary>
        protected NodeState _state = NodeState.Inactive;
        public NodeState State => _state;

        public Node()
        {
            _name = GetType().ToString();
        }

        /// <summary>
        /// ツリー起動時に一度だけ呼ばれる
        /// </summary>
        public virtual void OnAwake()
        {
        }

        /// <summary>
        /// ノード実行時に呼ばれる
        /// </summary>
        public virtual void OnStart()
        {
            _state = NodeState.Running;
        }
        
        public virtual NodeState OnUpdate()
        {
            if (_state == NodeState.Completed)
            {
                Debug.Log("This task already has been completed.");
                return _state;
            }

            if (_state == NodeState.Inactive)
            {
                OnStart();
            }

            return _state;
        }

        /// <summary>
        /// ノードが終了したら呼ばれる
        /// </summary>
        public virtual void OnEnd()
        {
            if (_state == NodeState.Completed)
            {
                Debug.Log("This task already has been completed.");
                return;
            }

            _state = NodeState.Inactive;
        }
        
        /// <summary>
        /// ノードが中断した時に呼ばれる
        /// </summary>
        public virtual void OnAbort()
        {
            OnEnd();
        }
    }
}

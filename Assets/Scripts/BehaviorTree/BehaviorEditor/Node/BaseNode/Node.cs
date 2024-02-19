using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using GraphProcessor;

namespace TsuyoshiBehaviorTree
{
    /// <summary>
    /// ノードの状態を表す列挙型
    /// </summary>
    public enum NodeState
    {
        Waiting,
        Success,
        Failure,
        Running,
    }
    
    /// <summary>
    /// ビヘイビアツリーのノードの基底クラス
    /// NodeGraphProcessorのBaseNodeを継承してUIで使えるようにする
    /// </summary>
    [Serializable]
    public class Node : BaseNode
    {
        [Input(name = "Parent"), Vertical]
        protected Node Parent;
        [SerializeField] 
        private string _description;
        private string _name;
        protected GameObject _owner;
        /// <summary>状態</summary>
        protected NodeState _state = NodeState.Waiting;
        public NodeState State
        {
            get => _state;
            set => _state = value;
        } 

        public Node()
        {
            _name = GetType().ToString();
        }
        
        /// <summary>
        /// OutPutに自身を格納することで、次のノードに自身を渡す
        /// </summary>
        protected override void Process()
        {
            Parent = this.GetInputNodes().FirstOrDefault() as Node;
            _state = NodeState.Waiting;
        }
        
        /// <summary>
        /// ノードを実行するオブジェクトを設定する
        /// </summary>
        /// <param name="owner"></param>
        public void SetOwnerObject(GameObject owner)
        {
            _owner = owner;
        }

        /// <summary>
        /// ノード実行時に呼ばれる
        /// </summary>
        public virtual void OnStart()
        {
            if (_state != NodeState.Waiting)
            {
                Debug.LogError("Statusが待機状態ではありません。");
                return;
            }
            _state = NodeState.Running;
            Debug.Log($"{_name}ノード実行開始");
        }
        
        /// <summary>
        /// ノードの更新処理
        /// </summary>
        /// <returns></returns>
        public virtual NodeState OnUpdate()
        {
            //待機状態なら開始処理を呼ぶ
            if (_state == NodeState.Waiting)
            {
                OnStart();
            }

            //成功または失敗したら終了処理を呼んで結果を返す
            if (_state == NodeState.Success || _state == NodeState.Failure)
            {
                OnEnd();
                return _state;
            }
            return _state;
        }

        /// <summary>
        /// ノードが終了したら呼ばれる
        /// </summary>
        public virtual void OnEnd()
        {
            //結果が出ていないのに終了処理を呼ぼうとしたらエラーを出す
            if (_state != NodeState.Success && _state != NodeState.Failure)
            {
                Debug.LogError("タスクの結果がでていないのに終了処理を呼び出そうとしました。");
                return;
            }
        }
        
        //TODO: 以下のメソッドは使うかどうか検討する
        // /// <summary>
        // /// ノードが中断した時に呼ばれる
        // /// </summary>
        // public virtual void OnAbort()
        // {
        //     OnEnd();
        // }
    }
}

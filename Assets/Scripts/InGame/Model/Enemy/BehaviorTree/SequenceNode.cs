using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TsuyoshiBehaviorTree
{
    /// <summary>
    /// コンポジットノードの一つ
    /// シーケンサーノードの基底クラス
    /// </summary>
    [System.Serializable]
    public class SequenceNode : IBehaviorNode
    {
        [SerializeReference, SubclassSelector] private List<IBehaviorNode> _nodes = new List<IBehaviorNode>();

        public void AddNode(IBehaviorNode node)
        {
            _nodes.Add(node);
        }

        public bool Execute()
        {
            foreach (var node in _nodes)
            {
                if (!node.Execute())
                {
                    return false;
                }
            }

            return true;
        }
    }
}

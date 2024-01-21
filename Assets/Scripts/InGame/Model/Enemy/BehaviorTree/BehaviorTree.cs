using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TsuyoshiBehaviorTree
{
    public class BehaviorTree : MonoBehaviour
    {
        [SerializeReference, SubclassSelector] private IBehaviorNode _rootNode;

        public void AddRootNode(IBehaviorNode node)
        {
            _rootNode = node;
        }

        public void Start()
        {
            _rootNode.Execute();
        }
    }
}

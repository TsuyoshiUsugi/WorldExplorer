using GraphProcessor;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

namespace TsuyoshiBehaviorTree
{
    [CreateAssetMenu(fileName = "ExampleGraph", menuName = "TsuyoshiBehaviorTree/ExampleGraph")]
    public class ExampleGraph : BaseGraph
    {
#if UNITY_EDITOR
        [OnOpenAsset(0)]
        public static bool OnBaseGraphOpened(int instanceID, int line)
        {
            var asset = EditorUtility.InstanceIDToObject(instanceID) as ExampleGraph;
            if (asset == null) return false;
            var window = EditorWindow.GetWindow<BehaviorTreeEditorWindow>();
            window.InitializeGraph(asset);
            return true;
        }
#endif
    }
}

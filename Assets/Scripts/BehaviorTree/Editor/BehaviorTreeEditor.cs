using GraphProcessor;
using UnityEditor;
using UnityEngine;

namespace TsuyoshiBehaviorTree
{
    [CustomEditor(typeof(BehaviorTreeGraph))]
    public class BehaviorTreeEditor : Editor
    { 
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Process"))
            {
                var graph = target as BehaviorTreeGraph;
                var processor = new BehavioreTreeGraphProcesser(graph);
                processor.Run();
            }
        }
    }
}

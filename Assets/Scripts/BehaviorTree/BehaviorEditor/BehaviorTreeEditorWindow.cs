using System.IO;
using UnityEngine;
using UnityEditor;
using GraphProcessor;
using UnityEngine.Assertions;

namespace TsuyoshiBehaviorTree
{
    /// <summary>
    /// ビヘイビアツリーを表示するウィンドウクラス
    /// </summary>
    public class BehaviorTreeEditorWindow : BaseGraphWindow
    {
        protected override void InitializeWindow(BaseGraph graph)
        {
            Assert.IsNotNull(graph);
            var fileName = Path.GetFileNameWithoutExtension(AssetDatabase.GetAssetPath(graph));
            titleContent = new GUIContent(ObjectNames.NicifyVariableName(fileName));
            if (graphView == null)
            {
                graphView = new DefaultBehaviorTreeGraphView(this);
            }
            rootView.Add(graphView);
        }
    }
}

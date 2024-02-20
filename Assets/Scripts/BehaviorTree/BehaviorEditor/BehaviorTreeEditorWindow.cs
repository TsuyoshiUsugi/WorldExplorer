using System.IO;
using UnityEngine;
using GraphProcessor;

#if UNITY_EDITOR


using UnityEditor;
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
                graphView = new BaseGraphView(this);
            }
            rootView.Add(graphView);
        }
    }
}

#endif

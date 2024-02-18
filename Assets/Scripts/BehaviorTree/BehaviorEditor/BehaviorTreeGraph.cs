using GraphProcessor;
using UnityEditor.Callbacks;
using UnityEngine;
using System.Linq;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace TsuyoshiBehaviorTree
{
    /// <summary>
    /// ビヘイビアツリーのグラフを表すクラス
    /// </summary>
    [CreateAssetMenu(menuName = "BehaviorTreeEditorWindow")]
    public class BehaviorTreeGraph : BaseGraph
    {
#if UNITY_EDITOR
        [OnOpenAsset(0)]
        public static bool OnBaseGraphOpened(int instanceID, int line)
        {
            var asset = EditorUtility.InstanceIDToObject(instanceID) as BehaviorTreeGraph;

            if (asset == null) return false;

            var window = EditorWindow.GetWindow<BehaviorTreeEditorWindow>();
            window.InitializeGraph(asset);
            return true;
        }

        /// <summary>
        /// グラフ作成時にビヘイビアツリーのルートノードを追加
        /// </summary>
        protected override void OnEnable()
        {
            base.OnEnable();

            if (!nodes.Any(x => x is BehaviorTreeRootNode))
            {
                AddNode(BaseNode.CreateFromType<BehaviorTreeRootNode>(Vector2.zero));
            }
        }
    }
#endif
}

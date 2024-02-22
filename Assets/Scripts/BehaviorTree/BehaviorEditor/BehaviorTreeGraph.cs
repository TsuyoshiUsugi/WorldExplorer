using GraphProcessor;
using UnityEngine;
using System.Linq;
using UnityEditor.Callbacks;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace TsuyoshiBehaviorTree
{
    /// <summary>
    /// ビヘイビアツリーのグラフを表すクラス
    /// </summary>
    [CreateAssetMenu(menuName = "BehaviorTreeEditorGraph")]
    public class BehaviorTreeGraph : BaseGraph
    {
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

            if (!nodes.Any(x => x is Root))
            {
                AddNode(BaseNode.CreateFromType<Root>(Vector2.zero));
            }
        }
    }
}

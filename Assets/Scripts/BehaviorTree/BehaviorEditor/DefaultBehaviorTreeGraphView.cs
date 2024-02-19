using System;
using System.Collections.Generic;
using System.Linq;
using GraphProcessor;
using TsuyoshiBehaviorTree;
#if UNITY_EDITOR
using UnityEditor;

/// <summary>
/// デフォルトで使用するビヘイビアツリーグラフビュー
/// </summary>
public class DefaultBehaviorTreeGraphView : BaseGraphView
{
    public DefaultBehaviorTreeGraphView(EditorWindow window) : base(window)
    {
    }

    public override IEnumerable<(string, Type)> FilterCreateNodeMenuEntries()
    {
        foreach (var nodeMenuItem in NodeProvider.GetNodeMenuEntries())
        {
            // ResultNodeを追加できないように
            if (nodeMenuItem.type == typeof(Root))
            {
                continue;
            }
            yield return nodeMenuItem;
        }
    }

    protected override bool canDeleteSelection { get { return !selection.Any(e => e is Root); } }
}

#endif

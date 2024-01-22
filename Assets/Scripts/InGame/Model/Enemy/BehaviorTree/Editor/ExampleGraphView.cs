using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.Experimental.GraphView;
using TsuyoshiBehaviorTree;

public class ExampleGraphView : GraphView
{
    public ExampleGraphView(EditorWindow editorWindow)
    {
        AddElement(new ExampleNode());
        this.StretchToParentSize();
        SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);
        this.AddManipulator(new ContentDragger());
        this.AddManipulator(new SelectionDragger());
        this.AddManipulator(new RectangleSelector());
    }
}

using System.Collections;
using System.Collections.Generic;
using GraphProcessor;
using TsuyoshiBehaviorTree;
using UnityEngine;

public class TestProcesser : MonoBehaviour
{
    [SerializeField] BaseGraph _graph;
    private BehaviorTreeProcesser _processer;
    
    // Start is called before the first frame update
    void Start()
    {
        _processer = new(_graph);
        
    }

    // Update is called once per frame
    void Update()
    {
        _processer.Run();
    }
}

using System.Collections;
using System.Collections.Generic;
using GraphProcessor;
using TsuyoshiBehaviorTree;
using UnityEngine;

public class TestAI : MonoBehaviour
{
    [SerializeField] BaseGraph _graph;
    private BehaviorTreeProcesser _processer;
    
    // Start is called before the first frame update
    void Start()
    {
        _processer = new(_graph, this.gameObject);
        _processer.Run();
    }

    // Update is called once per frame
    void Update()
    {
        _processer.OnUpdate();
    }
}

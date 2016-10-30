using UnityEngine;
using System;
using System.Collections;
using TreeSharpPlus;

public class B3BehaviorTree : MonoBehaviour {

    public GameObject fighter1;
    public GameObject fighter2;

    private BehaviorAgent behaviorAgent;

	void Start () {
        behaviorAgent = new BehaviorAgent(this.BuildTreeRoot());
        BehaviorManager.Instance.Register(behaviorAgent);
        behaviorAgent.StartBehavior();
	}
	
	
	void Update () {
	
	}

    protected Node BuildTreeRoot()
    {
        Node fight = new Sequence(
            fighter2.GetComponent<BehaviorMecanim>().Node_GoTo(fighter1.transform.position),
            fighter1.GetComponent<BehaviorMecanim>().Node_BodyAnimation("Breakdance", true));
        return fight;
    }
}

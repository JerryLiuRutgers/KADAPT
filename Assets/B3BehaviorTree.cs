using UnityEngine;
using System;
using System.Collections;
using TreeSharpPlus;

public class B3BehaviorTree : MonoBehaviour {

    public GameObject protag;
    public GameObject judge, judge2, judge3, judge4;
    public GameObject door1;
    public GameObject door2;
    public Transform exit;

    private BehaviorAgent behaviorAgent;
    private Boolean doors;

	void Start () {
        doors = false;
        behaviorAgent = new BehaviorAgent(this.BuildTreeRoot());
        BehaviorManager.Instance.Register(behaviorAgent);
        behaviorAgent.StartBehavior();
	}
	
	
	void Update () {
	    if (Input.GetKeyDown(KeyCode.Space) && doors == false)
        {
            OpenDoors();
        }
	}

    void OpenDoors()
    {
        door1.transform.Translate(new Vector3(-3, 0, 0));
        door2.transform.Translate(new Vector3(3, 0, 0));
        doors = true;
    }

    protected Node ApproachOrient(GameObject approachee, GameObject approacher)
    {
        return new Sequence(
            approacher.GetComponent<BehaviorMecanim>().Node_GoToUpToRadius(approachee.transform.position, 3.0f),
            approacher.GetComponent<BehaviorMecanim>().Node_OrientTowards(approachee.transform.position),
            approachee.GetComponent<BehaviorMecanim>().Node_OrientTowards(approacher.transform.position));        
    }

    protected Node RandomDance(GameObject dancer, GameObject judge)
    {
        return new Sequence(
            new SequenceParallel(
                new RandomNode(
                    SatNightDance(dancer),
                    ApplePick(dancer),
                    BreakDance(dancer)),
                judge.GetComponent<BehaviorMecanim>().Node_FaceAnimation("HeadShake", true)),
            judge.GetComponent<BehaviorMecanim>().Node_BodyAnimation("Breakdance", true));
    }

    protected Node BreakDance(GameObject dancer)
    {
        return new Sequence(
            dancer.GetComponent<BehaviorMecanim>().Node_BodyAnimation("Breakdance", true),
            new LeafWait(3000),
            dancer.GetComponent<BehaviorMecanim>().Node_BodyAnimation("Breakdance", false));
    }

    protected Node SatNightDance(GameObject dancer)
    {
        return new Sequence(
            dancer.GetComponent<BehaviorMecanim>().Node_HandAnimation("SatNightFever", true),
            new LeafWait(3000),
            dancer.GetComponent<BehaviorMecanim>().Node_HandAnimation("SatNightFever", false));
    }

    protected Node ApplePick(GameObject dancer)
    {
        return new Sequence(
            dancer.GetComponent<BehaviorMecanim>().Node_HandAnimation("ApplePick", true),
            new LeafWait(4000),
            dancer.GetComponent<BehaviorMecanim>().Node_HandAnimation("ApplePick", false));
    }

    protected Node DanceOff()
    {
        return new Sequence(
            ApproachOrient(protag, judge),
            RandomDance(protag, judge),
            ApproachOrient(protag, judge2),
            RandomDance(protag, judge2),
            ApproachOrient(protag, judge3),
            RandomDance(protag, judge3),
            ApproachOrient(protag, judge4),
            RandomDance(protag, judge4));
    }

    protected Node BuildTreeRoot()
    {
        Node story = new Sequence(
            DanceOff(),
            protag.GetComponent<BehaviorMecanim>().Node_GoTo(exit.position));
        return story;
    }
}

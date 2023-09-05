using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearBT : BTBase
{
    private SelectorNode rootNode;
    private List<Node> rootChildren = new List<Node>();

    private SelectorNode hitNode;
    private List<Node> hitChildren = new List<Node>();
    private BearHitAction hitAction = new BearHitAction();
    private BearUnconsciousAction unconsciousAction = new BearUnconsciousAction();

    private SelectorNode idleNode;
    private List<Node> idleChildren = new List<Node>();
    private BearIdleAction idleAction = new BearIdleAction();
    private BearDigAction digAction = new BearDigAction();
    private BearSmellAction smellAction = new BearSmellAction();
    private BearLookAction lookAction = new BearLookAction();
    private BearWalkAction walkAction = new BearWalkAction();

    private SequenceNode trackingNode;
    private List<Node> trackingChildren= new List<Node>();
    private ActionNode runAction;   

    private SelectorNode attackNode;
    private List<Node> attackChildren = new List<Node>();
    private ActionNode attackAction;

    //public BearBT() : base()
    //{
    //    this.data = base.data;

    //}

    //public BearBT()
    //{
    //    rootNode.rootChildren = new List<Node>();

    //    rootNode.rootChildren.Add(hitNode);
    //    hitNode.rootChildren.Add(hitAction);
    //    hitNode.rootChildren.Add(unconsciousAction);

    //    rootNode.rootChildren.Add(idleNode);
    //    idleNode.rootChildren.Add(idleAction);
    //    idleNode.rootChildren.Add(digAction);
    //    idleNode.rootChildren.Add(smellAction);
    //    idleNode.rootChildren.Add(lookAction);
    //    idleNode.rootChildren.Add(walkAction);

    //    rootNode.rootChildren.Add(trackingNode);
    //    trackingNode.rootChildren.Add(runAction);

    //    trackingNode.rootChildren.Add(attackNode);
    //    attackNode.rootChildren.Add(attackAction);
    //}

    //public BearBT(SelectorNode rootNode, SelectorNode hitNode, SelectorNode idleNode, SequenceNode trackingNode, SelectorNode attackNode) : base(rootNode, hitNode, idleNode, trackingNode, attackNode)
    //{
    //    this.hitNode = hitNode;
    //    this.hitNode.rootChildren.Add(hitAction);
    //    this.hitNode.rootChildren.Add(unconsciousAction);

    //    this.idleNode = idleNode;
    //    this.idleNode.rootChildren.Add(idleAction);
    //    this.idleNode.rootChildren.Add(digAction);
    //    this.idleNode.rootChildren.Add(smellAction);
    //    this.idleNode.rootChildren.Add(lookAction);
    //    this.idleNode.rootChildren.Add(walkAction);

    //    this.trackingNode = trackingNode;
    //    this.trackingNode.rootChildren.Add(runAction);

    //    this.attackNode = attackNode;
    //    this.attackNode.rootChildren.Add(attackAction);
    //}

    private void Awake()
    {
        rootNode = new SelectorNode(rootChildren);
        hitNode = new SelectorNode(hitChildren);
        idleNode = new SelectorNode(idleChildren);
        trackingNode = new SequenceNode(trackingChildren);
        attackNode = new SelectorNode(attackChildren);
        //rootNode.rootChildren = new List<Node>()
        //{
        //    hitNode,
        //    idleNode,
        //    trackingNode
        //};

        rootNode.childrenNode.Add(hitNode);
        hitNode.childrenNode.Add(hitAction);
        hitNode.childrenNode.Add(unconsciousAction);

        rootNode.childrenNode.Add(idleNode);
        idleNode.childrenNode.Add(idleAction);
        idleNode.childrenNode.Add(digAction);
        idleNode.childrenNode.Add(smellAction);
        idleNode.childrenNode.Add(lookAction);
        idleNode.childrenNode.Add(walkAction);

        //rootNode.childrenNode.Add(trackingNode);
        //trackingNode.childrenNode.Add(runAction);

        //trackingNode.childrenNode.Add(attackNode);
        //attackNode.childrenNode.Add(attackAction);
    }

    private void Update()
    {
        rootNode.Evaluate();
    }
}

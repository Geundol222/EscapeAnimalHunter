using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTBase
{
    public SelectorNode rootNode = new SelectorNode();

    public BTBase(SelectorNode rootNode)
    {
        this.rootNode = rootNode;
    }

    public void Update()
    {
        rootNode.Evaluate();
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeFactory {
    public NodeFactory() {

    }
    public Node CreateNode(int id) {
        switch (id) {
            case 0: return null;
            case 1: return new Block(1);
            default: return null;
        }
    }
}
public class NodeMemberFactory
{
    public NodeMemberFactory()
    {

    }
    public NodeMember CreateNodeMember(int id)
    {
        switch(id)
        {
            case 0: return null;
            case 1: return new Player(1);
            case 2: return new Enemy(2);
            case 3: return new LaserN(3);
            case 4: return new LaserN(4);

            default: return null;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeFactory {
    public NodeFactory() {

    }
    public Node CreateNode(int id) {
        switch (id) {
            case 0: return null;
            case 1: return new Block(1);
            case 2: return new LaserN(2);
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
            case 2: return new EnemyN(2);
            default: return null;
        }
    }
}

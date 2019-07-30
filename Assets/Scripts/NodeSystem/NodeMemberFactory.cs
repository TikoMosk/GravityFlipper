using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeMemberFactory
{
    public NodeMemberFactory()
    {

    }
    public NodeMember CreateNodeMember(int id)
    {
        Debug.Log(id);
        switch(id)
        {
            case 0: return null;
            case 1: return new Player(1);
            case 2: return new Enemy(2);
            default: return null;
        }
    }
}

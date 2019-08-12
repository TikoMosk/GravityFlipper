using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface GameState 
{
    void OnNodeClick(Node n, Node.Direction dir);
    
}

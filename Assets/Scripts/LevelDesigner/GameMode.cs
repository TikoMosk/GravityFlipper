﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface GameMode 
{
    void OnNodeClick(Node n, Node.Direction dir);
    
}

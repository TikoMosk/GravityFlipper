using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveableObject
{
    private int id;
    public int Id { get => id; set => id = value; }

    public MoveableObject(int id)
    {
        this.id = id;
    }
    //FACTORY PATTERN
    public static MoveableObject CreateMoveableObject(int id)
    {
        if(id == 1)
        {
            return new MoveableObject(1);
        }
        return null;
    }
}

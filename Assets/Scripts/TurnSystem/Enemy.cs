using UnityEngine;

public class Enemy : NodeMember
{
    public Enemy(int id) : base(id) {
        
    }

   /* private void OnDestroy()
    {
        EventController.currentInstance.Remove(GetComponent<PatrolScript>().Check);
    }*/
}

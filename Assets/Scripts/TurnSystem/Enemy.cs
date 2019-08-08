using UnityEngine;

public class Enemy : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			Destroy(collision.gameObject);
		}
	}

    private void OnDestroy()
    {
        EventController.currentInstance.Remove(GetComponent<PatrolScript>().Check);
    }
}

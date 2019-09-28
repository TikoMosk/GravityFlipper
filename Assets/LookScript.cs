using UnityEngine;

public class LookScript : MonoBehaviour {
    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        transform.rotation = Quaternion.LookRotation(Camera.main.transform.forward, GameController.Game.CameraController.UpVector);
    }
}

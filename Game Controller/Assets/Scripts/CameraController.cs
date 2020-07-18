using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public GameObject player;

    void LateUpdate()
    {
        transform.position = new Vector3(0f, 10f, player.transform.position.z - 10f);
    }
}

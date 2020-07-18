using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightGenerator : MonoBehaviour {

    public float LightDistance = 50f;
    public int NumberOfLights = 5;
    public Queue<GameObject> GeneratedLights;
    public GameObject LightModel;
    public GameObject Ball;

    float lastRecordedBallDistance;
    private float DistanceSinceLastLight = 0;
	// Use this for initialization
	void Start () {
        GeneratedLights = new Queue<GameObject>();
        for (int i = 0; i < NumberOfLights; i++) {
            GeneratedLights.Enqueue(Instantiate(LightModel, new Vector3(Ball.transform.position.x, Ball.transform.position.y, Ball.transform.position.z), Quaternion.identity));
        }
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + Ball.transform.position.z - lastRecordedBallDistance);
        DistanceSinceLastLight += Ball.transform.position.z - lastRecordedBallDistance;
        if (DistanceSinceLastLight >= LightDistance) {
            GameObject oldestLight = GeneratedLights.Dequeue();
            if (oldestLight.transform.position.z > Ball.transform.position.z) {
                Debug.Log("Not enough lights available! Add NumberOfLights");
            } else {
                oldestLight.transform.position = new Vector3(oldestLight.transform.position.x, oldestLight.transform.position.y, transform.position.z);
                GeneratedLights.Enqueue(oldestLight);
                DistanceSinceLastLight = 0;
            }
        }
        lastRecordedBallDistance = Ball.transform.position.z;
    }
}

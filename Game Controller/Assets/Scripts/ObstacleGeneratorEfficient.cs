using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGeneratorEfficient : MonoBehaviour {
    public AudioSource gruntingSound;
    public GameObject Ball;
    public Queue<GameObject>[] obstacles;
    public int obstacleNumber = 3;
    public int storedObstacleCount = 15;
    public GameObject[] obstacleTypes;
    public Vector3 InitialGeneratingSite;

    // Lane Variables
    public float LanesLength = 12f;
    public float LaneNum = 3;


    public float ObjectGeneratingTime = 10f;
    public float ObjectHeight = 1f;
    public int behindBallEPS = 5;

    float lastRecordedBallDistance;
    float distanceSinceLastObstacle = 0;
    private int LastObjectGeneratedLane;
    // Use this for initialization
    void Start()
    {
        LastObjectGeneratedLane = Mathf.Max(1, Mathf.CeilToInt(Random.value * LaneNum));
        obstacles = new Queue<GameObject>[5];
        for (int i = 0; i < obstacleNumber; i++) {
            obstacles[i] = new Queue<GameObject>();
            for (int j = 0; j < storedObstacleCount; j++) {
                GameObject obstacle = Instantiate(obstacleTypes[i], InitialGeneratingSite, Quaternion.identity);
                obstacle.AddComponent<obstacle>();
                obstacle.GetComponent<obstacle>().grunting = gruntingSound;
                obstacle.AddComponent<BoxCollider>();
                obstacle.GetComponent<Collider>().isTrigger = true;
                obstacles[i].Enqueue(obstacle);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + Ball.transform.position.z - lastRecordedBallDistance);
        if (distanceSinceLastObstacle > ObjectGeneratingTime * 2 * Random.value)
        {
            int setObstacleLane = GenerateObstacleLane();
            int selectedObstacleType = ((int)(Random.value * obstacleNumber)) % obstacleNumber;

            if (obstacles[selectedObstacleType].Peek().transform.position.z + behindBallEPS <= Ball.transform.position.z)
            {
                GameObject newObstacle = obstacles[selectedObstacleType].Dequeue();
                newObstacle.transform.position = new Vector3(transform.position.x + (setObstacleLane - Mathf.Ceil(LaneNum / 2)) * LanesLength / (LaneNum + 1), transform.position.y + ObjectHeight, transform.position.z);
                distanceSinceLastObstacle = 0;
                obstacles[selectedObstacleType].Enqueue(newObstacle);
            }
        }
        else
        {
            distanceSinceLastObstacle += (Ball.transform.position.z - lastRecordedBallDistance);
        }
        lastRecordedBallDistance = Ball.transform.position.z;
    }

    int GenerateObstacleLane() {
        int ChosenLane = Mathf.Max(1, Mathf.CeilToInt(Random.value * LaneNum));
        while (ChosenLane == LastObjectGeneratedLane) {
            ChosenLane = Mathf.Max(1, Mathf.CeilToInt(Random.value * LaneNum));
        }
        LastObjectGeneratedLane = ChosenLane;
        return ChosenLane;
    }
}

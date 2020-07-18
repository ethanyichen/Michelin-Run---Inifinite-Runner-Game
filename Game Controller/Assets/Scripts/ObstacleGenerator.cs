using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour {
    public GameObject Ball;
    public AudioSource pickupSound;
    public Queue<GameObject> spawnedObjects;
    string[] ingredients = { "tomato", "lettuce", "onion", "potato",
        "baguette", "ketchup", "olive oil", "mayo", //end of 50 pt ingredients
        "eggplant", "pumpkin", "sausage",
        "egg", "noodle", // end of 100pt ing
        "chicken", "shrimp", "bun", "cheese", // end of 150pt ing
        "steak", "patty", "flour" }; // end of 200pt ing
    int FiftyPointRange = 8;
    int HundredPointRange = 13;
    int HundredFiftyRange = 17;
    int TwoHundredRange = 20;

    // Lane Variables
    public float LanesLength = 12f;
    public float LaneNum = 3;


    public float ObjectAverageDistance = 100f;
    public float ObjectHeight = 1f;
    public int destroyEPS = 5;

    float lastRecordedBallDistance;
    float distanceSinceLastObstacle = 0;
    // Use this for initialization

    string ingredientGenerate()
    {
        int ing;
        int i;
        // change the range to adjust probability/weight
        i = Random.Range(0, 10);
        // change the i conditions to adust the probability/weight
        if (i > 5) ing = Random.Range(0, FiftyPointRange);
        else if (i > 2) ing = Random.Range(FiftyPointRange, HundredPointRange);
        else if (i > 1) ing = Random.Range(HundredPointRange, HundredFiftyRange);
        else ing = Random.Range(HundredFiftyRange, TwoHundredRange);
        return ingredients[ing];
    }

    void Start () {
		spawnedObjects = new Queue<GameObject>();
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + Ball.transform.position.z - lastRecordedBallDistance);
        if (distanceSinceLastObstacle > ObjectAverageDistance * 2 * Random.value) {
            int setObstacleLane = Mathf.Max(1, Mathf.CeilToInt(Random.value * LaneNum));
            //generatedObject.transform.position = new Vector3(transform.position.x + (setObstacleLane - Mathf.Ceil(LaneNum / 2)) * LanesLength / (LaneNum + 1), transform.position.y + ObjectHeight, transform.position.z);
            string ingredientGenerated = ingredientGenerate();
            GameObject newObject = Instantiate(Resources.Load("Models/" + ingredientGenerated), new Vector3(transform.position.x + (setObstacleLane - Mathf.Ceil(LaneNum / 2)) * LanesLength / (LaneNum + 1), transform.position.y + ObjectHeight + 1, transform.position.z), Quaternion.identity) as GameObject;
            if (newObject == null) {
                Debug.Log(ingredientGenerated);
            }
            newObject.AddComponent<CoinScript>();
            newObject.AddComponent<BoxCollider>();
            newObject.GetComponent<Collider>().isTrigger = true;
            newObject.GetComponent<CoinScript>().ingredient = ingredientGenerated;
            newObject.GetComponent<CoinScript>().pickup = pickupSound;
            spawnedObjects.Enqueue(newObject);
            distanceSinceLastObstacle = 0;
        } else {
            distanceSinceLastObstacle += (Ball.transform.position.z - lastRecordedBallDistance);
        }
        while (!(spawnedObjects.Count == 0) && (spawnedObjects.Peek() == null || spawnedObjects.Peek().transform.position.z + destroyEPS < Ball.transform.position.z)) {
            Destroy(spawnedObjects.Dequeue());
        }
        lastRecordedBallDistance = Ball.transform.position.z;
	}
}

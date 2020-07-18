using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    public AudioSource pickup;
    public string ingredient;
    public static float speedDecreaseRatioToWeight = 0.01f;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Rotate(0,0, 90 * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Ball")
        {
            pickup.Play();
            int weightGained;
            other.GetComponent<ItemCollection>().ingredientList.TryGetValue(ingredient, out weightGained);
            other.GetComponent<PlayerController>().Speed -= speedDecreaseRatioToWeight * weightGained;
            AddToCollected(other);

            // Coin gets removed
            Destroy(gameObject);
        }
    }


    private void AddToCollected(Collider other)
    {
        if (other.GetComponent<ItemCollection>().collectedItems.ContainsKey(ingredient))
        {
            int num;
            other.GetComponent<ItemCollection>().collectedItems.TryGetValue(ingredient, out num);
            other.GetComponent<ItemCollection>().collectedItems.Remove(ingredient);
            other.GetComponent<ItemCollection>().collectedItems.Add(ingredient, num + 1);
        }
        else
        {
            other.GetComponent<ItemCollection>().collectedItems.Add(ingredient, 1);
        }
    }
}
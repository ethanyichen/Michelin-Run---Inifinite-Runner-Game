using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class UIScript : MonoBehaviour {

    public TextMeshProUGUI inventory;
    public GameObject player;
    // Use this for initialization
    void Start()
    {
        inventory.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        DisplayInventory();
    }

    // Update the inventory being displayed
    void DisplayInventory()
    {
        inventory.text = "";
        foreach (string key in player.GetComponent<ItemCollection>().collectedItems.Keys)
        {
            int num = player.GetComponent<ItemCollection>().collectedItems[key];
            inventory.text += "<sprite name=" + key + ">\u00A0\u00A0\u00A0\u00A0\u00A0 x" + num + "\n\n";
        }
    }
}

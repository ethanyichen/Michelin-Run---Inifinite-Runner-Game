using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class obstacle : MonoBehaviour {
    public AudioSource grunting;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Ball")
        {
            grunting.PlayDelayed(0.2f);
            other.GetComponent<PlayerController>().Speed -= 1f;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMover : MonoBehaviour {
    public GameObject Ball;
    public Transform otherGround;
    public GameObject rightWall;
    public GameObject leftWall;
    public Texture[] wallSkins;
    public int wallSkinNumber;
    public int EPS = 5;
	// Use this for initialization
	void Start () {
        float rand = Random.Range(0, wallSkinNumber - 0.001f);
        rightWall.GetComponent<Renderer>().material.SetTexture("_MainTex", wallSkins[(int)Mathf.Floor(rand)]);
        rand = Random.Range(0, wallSkinNumber - 0.001f);
        leftWall.GetComponent<Renderer>().material.SetTexture("_MainTex", wallSkins[(int)Mathf.Floor(rand)]);
    }
	
	// Update is called once per frame
	void Update () {
        if (Ball.transform.position.z > transform.position.z + transform.lossyScale.z / 2 + EPS) {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + transform.lossyScale.z + otherGround.lossyScale.z * 2);
            float rand = Random.Range(0, wallSkinNumber - 0.001f);
            rightWall.GetComponent<Renderer>().material.SetTexture("_MainTex", wallSkins[(int)Mathf.Floor(rand)]);
            rand = Random.Range(0, wallSkinNumber - 0.001f);
            leftWall.GetComponent<Renderer>().material.SetTexture("_MainTex", wallSkins[(int)Mathf.Floor(rand)]);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour {
    private float speed = 8f;
    private float endX=135f;
   
	// Use this for initialization
    void Start()
    {
     
    }
	// Update is called once per frame
	void Update () {
        if(transform.position.x<135f)
        {
            transform.Translate(Vector3.forward*speed*Time.deltaTime);
        }
          
	}
}

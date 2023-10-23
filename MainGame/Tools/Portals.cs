using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portals : MonoBehaviour {
    private Transform pointA;
    private Transform pointB;
	// Use this for initialization
	void Start () {
        pointA = GameObject.Find("PointA").transform;
        pointB = GameObject.Find("PointB").transform;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnControllerColliderHit(ControllerColliderHit other)
    {
        if ((other.gameObject.name).Equals("PointA"))
        {
            Vector3 target = pointB.position;
            target.x += 2;
            target.z += 2;
            transform.position = target;
        }
        if ((other.gameObject.name).Equals("PointB"))
        {
            Vector3 target = pointA.position;
            target.x += 2;
            target.z += 2;
            transform.position = target;
        }
        
    }
}

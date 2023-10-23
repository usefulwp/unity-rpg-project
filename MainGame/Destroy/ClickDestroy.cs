using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickDestroy : MonoBehaviour {
    public float destroySelfTime=1f;
	// Use this for initialization
	void Start () {
		 Destroy(this.gameObject, destroySelfTime);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
 
}

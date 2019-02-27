using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotation : MonoBehaviour {
    public Transform target;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 realitivePos = target.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(realitivePos);
        transform.rotation = rotation;  

    }
}

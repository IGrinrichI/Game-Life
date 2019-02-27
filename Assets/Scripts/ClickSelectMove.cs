using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickSelectMove : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
    private bool cliked = false;
    void OnMouseUp()
    {
        if (cliked == false)
            cliked = true;
        else
            cliked = false;
        print(cliked);
    }
    // Update is called once per frame
    void Update ()
    {
        if (cliked)
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
            transform.position += new Vector3(h, 0f, v);
        }
    }
}

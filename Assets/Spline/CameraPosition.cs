using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPosition : MonoBehaviour {

    public Vector3 topPosition;
    
    public Vector3 sidePosition;

    private bool istop = true;

	public void Goo()
    {
        if(istop)
        {
            transform.localPosition = sidePosition;
            transform.localRotation = Quaternion.Euler(15, 270, 0);
            istop ^= true;
        }
        else
        {
            transform.localPosition = topPosition;
            transform.localRotation = Quaternion.Euler(60, 0, 0);
            istop ^= true;
        }
    }
}

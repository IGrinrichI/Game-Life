using UnityEngine;
using System.Collections;

public class RECT : MonoBehaviour
{
    public Transform target;
    [SerializeField]
    private int hit = 0;
    /*private bool cliked = false;
    void OnMouseUpAsButton ()
    {
        if (cliked == false)
             cliked = true;
        else
             cliked = false;
        print(cliked);
    }*/
    private void OnTriggerEnter(Collider other)
    {
        hit++;
        if (hit < 100)
            //Instantiate(other.gameObject ,other.transform.position +new Vector3 (0f,0f,3f), other.transform.rotation);
            //Instantiate(other.gameObject, target);
            Instantiate(other, other.transform);
     
    }
    private void OnTriggerExit(Collider other)
    {
        Destroy(other.gameObject);
        hit--;
    }
}

/*using UnityEngine;
using System.Collections;

public class RECT : MonoBehaviour
{
    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.relativeVelocity.magnitude > 2)
            Debug.Log("World");
    }
}*/

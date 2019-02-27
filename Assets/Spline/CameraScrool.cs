using UnityEngine;
using System.Collections;

public class CameraScrool : MonoBehaviour
{
    public bool zooming;
    public float zoomSpeed;
    public Camera camera;
    public Transform seek;
    private Vector3 mousePreviousPosition;
    private float r;

    private void Start()
    {
        r = Mathf.Pow(camera.transform.localPosition.x * camera.transform.localPosition.x + camera.transform.localPosition.z * camera.transform.localPosition.z + camera.transform.localPosition.y * camera.transform.localPosition.y, .5f);
    }

    void Update()
    {
        if (zooming)
        {
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            float zoomDistance = zoomSpeed * Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime;
            camera.transform.Translate(ray.direction * zoomDistance, Space.World);
            r = Mathf.Pow(camera.transform.localPosition.x * camera.transform.localPosition.x + camera.transform.localPosition.z * camera.transform.localPosition.z + camera.transform.localPosition.y * camera.transform.localPosition.y, .5f);
        }
        if (Input.GetButtonDown("Fire1"))
        {
            mousePreviousPosition = Input.mousePosition;
        }
        if (Input.GetButton("Fire1"))
        {
            float arcsin = Mathf.Asin(camera.transform.localPosition.y / r);
            float arccos = camera.transform.localPosition.z < 0 ? -Mathf.Acos(((camera.transform.localPosition.x / Mathf.Abs(Mathf.Cos(arcsin))) % r) / r) : Mathf.Acos(((camera.transform.localPosition.x / Mathf.Abs(Mathf.Cos(arcsin))) % r) / r);
            float dy = ((mousePreviousPosition.y - Input.mousePosition.y) / 100) + arcsin;
            float dx = ((mousePreviousPosition.x - Input.mousePosition.x) / 100) + arccos;
            camera.transform.localPosition = new Vector3(
                Mathf.Cos(dy) * Mathf.Cos(dx) * r,
                Mathf.Sin(dy) * r,
                Mathf.Cos(dy) * Mathf.Sin(dx) * r);
            camera.transform.LookAt(seek);
            mousePreviousPosition = Input.mousePosition;
        }
    }
}


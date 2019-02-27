using UnityEngine;

public class Cell : MonoBehaviour {
    private Renderer renderer;
    public int isAlive = 0;

    void Start () {
        renderer = gameObject.GetComponent<Renderer>();
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButton(0))
        {
            IsAlive();
        }
        else if (Input.GetMouseButton(1))
        {
            IsDead();
        }
    }

    public void IsAlive()
    {
        renderer.material.color = Color.black;
        isAlive = 1;
    }

    public void IsDead()
    {
        renderer.material.color = Color.white;
        isAlive = 0;
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}

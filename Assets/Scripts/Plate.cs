using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Plate : MonoBehaviour {

    public GameObject cell;
    public List<GameObject> plat;
    public int n;
    private bool run = false;
    private List<bool> platbool;
    private bool[] a = { false };
    public Text pause;
    private int generation = 1;
    public Text textGeneration;
    public float pertime = 1;
    public Button randomize;
    public int resize = 1;

    void Start () {
        platbool = new List<bool>(a);
        for (float i = 0; i < n; i++)
        {
            for (float j = 0; j < n; j++)
            {
                plat.Add(Instantiate(cell, new Vector3(j, i, 0f), Quaternion.identity));
                platbool.AddRange(a);
            }
        }
        platbool.RemoveAt(n * n);
        float N = n;
        Camera.main.transform.position = new Vector3((N-1)/2, (N-1)/2, -(N * Mathf.Cos(3.14159265f / 6) + .5f));
	}

    public void Clear()
    {
        foreach(GameObject block in plat)
        {
            block.GetComponent<Cell>().IsDead();
        }
        pause.GetComponent<Text>().text = "Старт";
        textGeneration.text = "Поколение: 1";
        generation = 1;
        randomize.interactable = true;
    }

    public void Run()
    {
        if (run)
        {
            int around = 0;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    around = plat[((i + n-1) % n) * n + (j + n-1) % n].GetComponent<Cell>().isAlive + plat[((i + n-1) % n) * n + (j + n) % n].GetComponent<Cell>().isAlive + plat[((i + n-1) % n) * n + (j + n+1) % n].GetComponent<Cell>().isAlive + plat[((i + n) % n) * n + (j + n-1) % n].GetComponent<Cell>().isAlive + plat[((i + n) % n) * n + (j + n+1) % n].GetComponent<Cell>().isAlive + plat[((i + n+1) % n) * n + (j + n-1) % n].GetComponent<Cell>().isAlive + plat[((i + n+1) % n) * n + (j + n) % n].GetComponent<Cell>().isAlive + plat[((i + n+1) % n) * n + (j + n+1) % n].GetComponent<Cell>().isAlive;
                    if (around == 3)
                        platbool[i * n + j] = true;
                    if (around < 2 || around > 3)
                        platbool[i * n + j] = false;
                }
            }
            if (!NotEqual())
            {
                run = false;
                pause.GetComponent<Text>().text = "Старт";
                generation = 1;
                return;
            }
            generation++;
            textGeneration.text = "Поколение: " + generation;
            Interpritate();
            Invoke("Run", (n < 100 ? pertime * (1 - n / 100) : 0));
        }
    }

    public void Stop()
    {
        if (run)
        {
            run = false;
            pause.GetComponent<Text>().text = "Продолжить";
        }
        else
        {
            randomize.interactable = false;
            for (int i = 0; i < n * n; i++)
            {
                if (plat[i].GetComponent<Cell>().isAlive == 1)
                    platbool[i] = true;
                else
                    platbool[i] = false;
            }
            run = true;
            pause.GetComponent<Text>().text = "Пауза";
            Run();
        }
    }

    private void Interpritate()
    {
        for (int i = 0; i < n * n; i++)
        {
                if (platbool[i])
                    plat[i].GetComponent<Cell>().IsAlive();
                else
                    plat[i].GetComponent<Cell>().IsDead();
        }
    }

    private bool NotEqual()
    {
        bool notEqual = false;
        for (int i = 0; i < n * n; i++)
        {
            if (plat[i].GetComponent<Cell>().isAlive == 1 && platbool[i] == false || plat[i].GetComponent<Cell>().isAlive == 0 && platbool[i] == true)
            {
                notEqual = true;
                break;
            }
        }
        return notEqual;
    }

    public void Randomize()
    {
        for (int i = 0; i < n * n; i++)
        {
            if (Random.Range(0, 2) == 1)
                plat[i].GetComponent<Cell>().IsAlive();
            else
                plat[i].GetComponent<Cell>().IsDead();
        }
    }

    public void Resizeresize(float r)
    {
        resize = (int)r;
    }

    public void Resize()
    {
        for (int i = 0; i < n * n; i++)
        {
            plat[n * n - (i + 1)].GetComponent<Cell>().Die();
            plat.RemoveAt(n * n - (i + 1));
        }
        n = resize;
        Start();
    }

    public void Polsha()
    {
        int center = n / 2;
        int range = 11;
        plat[center * n + center].GetComponent<Cell>().IsAlive();
        for (int i = 0; i < range / 2 + 1; i++)
        {
            plat[(center+i) * n + center].GetComponent<Cell>().IsAlive();
            plat[center * n + (center+i)].GetComponent<Cell>().IsAlive();
            plat[center * n + (center-i)].GetComponent<Cell>().IsAlive();
            plat[(center-i) * n + center].GetComponent<Cell>().IsAlive();
            plat[(center+range/2) * n + (center+i)].GetComponent<Cell>().IsAlive();
            plat[(center-range/2) * n + (center-i)].GetComponent<Cell>().IsAlive();
            plat[(center-i) * n + (center+range/2)].GetComponent<Cell>().IsAlive();
            plat[(center+i) * n + (center-range/2)].GetComponent<Cell>().IsAlive();
        }
    }
}

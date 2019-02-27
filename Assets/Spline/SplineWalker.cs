using UnityEngine;
using System.Collections.Generic;

public class SplineWalker : MonoBehaviour {

    public struct RailWay
    {
        public struct Ways
        {
            public List<int> input;
            public List<int> output;
        }

        public BezierSpline[] splines;
        public Ways[] ways;
        public int currentLine;
        public int nextLine;
        public int prevLine;
    }

    public SplineWalker target;
    public Train[] train;
    private float traindis;

    public BezierSpline spline;
    public RailWay railWay;
    [SerializeField]
    private float duration;

    public bool lookForward;

    public SplineWalkerMode mode;
    [SerializeField]
    private float distance;
    [SerializeField]
    private float speed;
    private float maxSpeed;
    private float a;
    private bool goingForward = true;

    public void SetSpeed(float s)
    {
        maxSpeed = s;
    }
    public void SetFriction(float fr)
    {
        a = fr;
    }
    private void Start()
    {
        railWay.splines = GameObject.FindObjectsOfType<BezierSpline>();
        railWay.ways = new RailWay.Ways[railWay.splines.Length];
        train = new Train[transform.childCount];
        for (int i = 0; i < train.Length; i++)
        {
            train[i] = transform.GetChild(i).GetComponent<Train>();
        }
        for (int i = 0; i < railWay.ways.Length; i++)
        {
            railWay.ways[i].input = new List<int> { };
            railWay.ways[i].output = new List<int> { };
        }
        Debug.Log("ways = " + railWay.ways.Length + "      inputs = " + railWay.ways[0].input.Capacity);
        int gi = 0;
        foreach(BezierSpline line in railWay.splines)
        {
            line.Distance();
            for (int i = gi; i < railWay.splines.Length; i++)
            {
                if (railWay.splines[gi].GetPoint(0) == railWay.splines[i].GetPoint(1))
                {
                    //railWay.ways[gi].input = new int[] { (int)railWay.ways[gi].input.GetValue(0, railWay.ways[gi].input.Length - 1), i };
                    railWay.ways[gi].input.Add(i);
                    //railWay.ways[i].output = new int[] { (int)railWay.ways[i].output.GetValue(0, railWay.ways[i].output.Length - 1), gi };
                    railWay.ways[i].output.Add(gi);
                }
                if (railWay.splines[gi].GetPoint(1) == railWay.splines[i].GetPoint(0))
                {
                    //railWay.ways[gi].output = new int[] { (int)railWay.ways[gi].output.GetValue(0, railWay.ways[gi].output.Length - 1), i };
                    railWay.ways[gi].output.Add(i);
                    //railWay.ways[i].input = new int[] { (int)railWay.ways[i].input.GetValue(0, railWay.ways[i].input.Length - 1), gi };
                    railWay.ways[i].input.Add(gi);
                }
            }
            gi++;
        }
        railWay.currentLine = 0;
        railWay.nextLine = railWay.ways[railWay.currentLine].output[0];
        railWay.prevLine = railWay.ways[railWay.currentLine].input[0];
        spline = railWay.splines[railWay.currentLine];
        //spline.Distance();
        a = 1;
        traindis = 20f;
        for (int i = 0; i < train.Length; i++)
        {
            train[i].distance = -i * 1.5f;
            train[i].prevPos = SearchDot(1, train[i].distance);
            train[i].progress = spline.distances[train[i].prevPos - 1].progress + ((train[i].distance - spline.distances[train[i].prevPos - 1].distance) / (spline.distances[train[i].prevPos].distance - spline.distances[train[i].prevPos - 1].distance)) / (spline.distances.Length - 1);
        }
    }
    private void Update()
    {
        
        if (!target)
        {
            float summarySpeed = 0;
            foreach (Train box in train)
            {
                box.angle = Vector3.Angle(spline.GetDirection(box.progress), Vector3.up) * 3.14159265f / 180;
                box.angleSpeed = -Mathf.Cos(box.angle) * 9.8f;
                summarySpeed += box.angleSpeed;
            }
            speed += (maxSpeed + summarySpeed) * Time.deltaTime / 1000;
            speed *= a;
            SetColor();
            int i = 0;
            foreach (Train box in train)
            {
                if (goingForward)
                {
                    if (summarySpeed != 0f)
                    {
                        box.distance = (box.distance + speed + spline.distances[spline.distances.Length - 1].distance) % spline.distances[spline.distances.Length - 1].distance;
                        box.prevPos = SearchDot(box.prevPos, box.distance);
                        box.progress = spline.distances[box.prevPos - 1].progress + ((box.distance - spline.distances[box.prevPos - 1].distance) / (spline.distances[box.prevPos].distance - spline.distances[box.prevPos - 1].distance)) / (spline.distances.Length - 1);
                    }
                }
                else
                {
                    box.progress -= Time.deltaTime / duration;
                    if (box.progress < 0f)
                    {
                        box.progress = -box.progress;
                        goingForward = true;
                    }
                }
                Vector3 position = spline.GetPoint(box.progress);
                box.transform.localPosition = position;
                if (lookForward)
                {
                    box.transform.LookAt(position + spline.GetDirection(box.progress));
                }
                i++;
            }
            i = 0;
        }
        else
        {

        }
    }

    void SetColor()
    {
        float rnd = 10 * speed;
        for (int i = 0; i < train.Length; i++)
        {
            train[i].transform.GetChild(0).GetComponent<MeshRenderer>().material.color = new Color(
                Mathf.Abs(Mathf.Cos(rnd * i)),
                Mathf.Abs(Mathf.Cos(rnd * i + Mathf.PI / 3)),
                Mathf.Abs(Mathf.Cos(rnd * i + (Mathf.PI * 2) / 3)));
        }
    }

    private int SearchDot(int prevPos, float priraschenie)
    {
        if (spline.distances[prevPos].distance < priraschenie)
        {
            for (int i = prevPos + 1; i < spline.distances.Length; i++)
            {
                if (spline.distances[i].distance > priraschenie)
                {
                    return i;
                }
            }
            return prevPos;
        }
        else
        {
            for (int i = prevPos - 1; i >= 0; i--)
            {
                if (spline.distances[i].distance < priraschenie)
                {
                    return i + 1;
                }
            }
            return prevPos;
        }
    }
}
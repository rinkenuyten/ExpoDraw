using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LineDrawer : MonoBehaviour
{
    //reference to LineRenderer component
    private LineRenderer line;
    //car to store mouse position on the screen
    private Vector3 mousePos;
    //assign a material to the Line Renderer in the Inspector
    public Material material;

    //number of lines drawn
    private List<LineRenderer> lines = new List<LineRenderer>();



    private Vector2 fingerStart;
    private Vector2 fingerEnd;

    void Update()
    {
        //////////////////////////////////////////////////////////////////
        // Android swipe input
            foreach (Touch touch in Input.touches)
            {
                if (touch.phase == TouchPhase.Began)
                {
                    fingerStart = touch.position;
                    fingerEnd = touch.position;
                }

                if (touch.phase == TouchPhase.Moved)
                {
                    fingerEnd = touch.position;
                    if (Vector2.Distance(fingerStart, fingerEnd) > 0.01f)
                    {
                        if (line == null)
                        {
                            createLine();

                            line.SetPosition(0, fingerStart);
                            line.SetPosition(1, fingerEnd);

                            fingerStart = fingerEnd;
                            fingerEnd = Vector2.zero;
                        }
                    }
                }

                if (touch.phase == TouchPhase.Ended)
                {
                    fingerStart = Vector2.zero;
                    fingerEnd = Vector2.zero;
                }
            }
    }

    //method to create line
    private void createLine()
    {
        //create a new empty gameobject and line renderer component
        line = new GameObject("Line").AddComponent<LineRenderer>();
        //assign the material to the line
        line.material = material;
        //set the number of points to the line
        line.SetVertexCount(2);
        //set the width
        line.SetWidth(0.15f, 0.15f);
        //render line to the world origin and not to the object's position
        line.useWorldSpace = true;
        lines.Add(line);
    }
}
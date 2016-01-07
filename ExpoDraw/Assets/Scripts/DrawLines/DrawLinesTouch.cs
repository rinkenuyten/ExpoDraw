using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Vectrosity;

public class DrawLinesTouch : MonoBehaviour {
// For touchscreen devices -- draw a line with your finger

	public Texture lineTex;
	public float maxPoints = 5000;
	public float lineWidth = 4.0f;
	public int minPixelMove = 5;	// Must move at least this many pixels per sample for a new segment to be recorded
	public bool useEndCap = false;
	public Texture2D capLineTex;
	public Texture2D capTex;
	public float capLineWidth = 20.0f;

	private VectorLine line;
	private Vector2 previousPosition;
	private int sqrMinPixelMove;
	private bool canDraw = false;
	private Touch touch;

	private Texture tex;
	private float useLineWidth;
	private Vector3 pointCount;

    private List<VectorLine> Lines = new List<VectorLine>();
    private VectorLine activeLine;
    private int LineNr;

	public void Start ()
    {
        LineNr = 0;
        useLineWidth = lineWidth;
		
		
        /// Instantiate Vectorlines here

		
		
		sqrMinPixelMove = minPixelMove*minPixelMove;
	}

	public void Update (){
        if (GameObject.Find("VectorCanvas"))
        {
            Canvas DrawCanvas = GameObject.Find("VectorCanvas").GetComponent<Canvas>();
            DrawCanvas.renderMode = RenderMode.ScreenSpaceCamera;
            DrawCanvas.worldCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
            DrawCanvas.pixelPerfect = true;
        }

		if (Input.touchCount > 0) {
			touch = Input.GetTouch(0);
			if (touch.phase == TouchPhase.Began) 
            {
				line.Draw();
				previousPosition = touch.position;
				line.points2.Add (touch.position);
				canDraw = true;
			}
			else if (touch.phase == TouchPhase.Moved && (touch.position - previousPosition).sqrMagnitude > sqrMinPixelMove && canDraw) 
            {
				previousPosition = touch.position;
				line.points2.Add (touch.position);
				if (line.points2.Count >= maxPoints) {
					canDraw = false;
				}
				line.Draw();
			}
		}
	}

    public void StartOpdracht(Opdracht opdracht)
    {
        foreach (int size in opdracht.Sizes)
        {
            foreach (Texture brush in opdracht.Brush)
            {
                foreach (Color color in opdracht.Colors)
                {
                    VectorLine tempLine = new VectorLine(size + "-" + color.GetHashCode() + "-" + brush.name, new List<Vector2>(), brush, size, 
                        LineType.Continuous, Joins.Weld);
                    tempLine.color = color;
                    tempLine.endPointsUpdate = 1;
                    Lines.Add(tempLine);
                }
            }
        }
        foreach(VectorLine linetest in Lines)
        {
            linetest.Draw();
        }
        line = Lines[0];
    }

    public void SetBrush(string brushName)
    {
        Debug.Log(brushName);
        foreach (VectorLine line in Lines)
        {
            if (line.name.Contains(brushName))
            {
                Debug.Log(line);
            }   
        }
    }

    public void setColor(string colorname)
    {
        foreach (VectorLine line in Lines)
        {
            Debug.Log("line color" + line.color.ToString());
            Debug.Log("line color" + line.color);
            Debug.Log("colorname" + colorname);

            if (line.color.GetHashCode().ToString() == colorname)
            {
 
            }

            if (line.name.Contains(colorname))
            {
                //if(line.name.Contains(activeLine.name.))
            }
        }
    }

    public void ToggleCanDraw(bool Boel)
    {
        canDraw = Boel;
    }


}
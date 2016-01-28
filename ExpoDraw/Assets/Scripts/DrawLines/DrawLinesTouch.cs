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

	public GameObject canvas;

	private Texture tex;
	private float useLineWidth;
	private Vector3 pointCount;

    private List<VectorLine> Lines = new List<VectorLine>();
	private int LineNr;
	
	private int activeSize;
	private string activeColor;
	private string activeBrush;

	public void Start ()
    {
        LineNr = 0;
        useLineWidth = lineWidth;
		
		sqrMinPixelMove = minPixelMove*minPixelMove;

        if (GameObject.Find("VectorCanvas"))
        {
            Canvas DrawCanvas = GameObject.Find("VectorCanvas").GetComponent<Canvas>();
            DrawCanvas.renderMode = RenderMode.ScreenSpaceCamera;
            DrawCanvas.worldCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
            DrawCanvas.pixelPerfect = true;
        }
	}

    public void Update()
    {

        if (canDraw)
        {
            if (Input.touchCount > 0)
            {
                touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began)
                {
                    line.Draw();
                    previousPosition = touch.position;
                    line.points2.Add(touch.position);
                    canDraw = true;
                }
                else if (touch.phase == TouchPhase.Moved && (touch.position - previousPosition).sqrMagnitude > sqrMinPixelMove && canDraw)
                {
                    previousPosition = touch.position;
                    line.points2.Add(touch.position);
                    if (line.points2.Count >= maxPoints)
                    {
                        canDraw = false;
                    }
                    line.Draw();
                }
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
                    VectorLine tempLine = new VectorLine(size + "," + color.ToString() + "," + brush.name, new List<Vector2>(), brush, size, 
                        LineType.Continuous, Joins.Weld);


					//tempLine.SetCanvas (canvas);
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

	    activeSize = System.Int32.Parse(Lines [LineNr].name.Split (',') [0]);
		activeBrush = Lines [LineNr].texture.name;
	    activeColor = Lines [LineNr].color.ToString();
        line = Lines[LineNr];
        Debug.Log("Current Active Line:" + line.name);
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

    public void Undo()
    {
        Debug.Log("Undid line" + line.name);
        line.points2.Clear();
	    line.Draw();
	    
        line = Lines[--LineNr % (Lines.Count - 1)];
    }

    public void ClearLines()
    {
        foreach (VectorLine line in Lines)
        {
            line.points2.Clear();
            line.Draw();
        }
    }

    public void setColor(string colorname)
	{
		activeColor = colorname;
		setLine(activeSize, activeColor, activeBrush);
        Debug.Log("Current Active Line:" + line.name);
	}
	
	public void setSize(int size){
		activeSize = size;
		setLine(activeSize, activeColor, activeBrush);
	}
	
	public void setBrush(string brush){
		activeBrush = brush;
		setLine(activeSize, activeColor, activeBrush);
	}
	
	public void setLine(int size, string color, string brush){
		String newLineName = size +","+ color + "," + brush;

		foreach (VectorLine line in Lines)
		{
			if (line.name == newLineName) {
				this.line = line;
			}
		}
	}
	
    public void ToggleCanDraw(bool Boel)
    {
        canDraw = Boel;
    }
}
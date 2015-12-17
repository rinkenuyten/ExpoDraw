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
    private int colorID = 0;

    private List<VectorLine> lineList = new List<VectorLine>();

	public List<Texture> brushes;
	private int selectedBrush = 0;

	public void Start (){
        //VectorLine.SetCanvasCamera(this.gameObject.GetComponent<Camera>());
		if (useEndCap) {
			VectorLine.SetEndCap ("RoundCap", EndCap.Mirror, capLineTex, capTex);
			 tex= capLineTex;
			 useLineWidth= capLineWidth;
		}
		else {
			setBrush (selectedBrush);
			useLineWidth = lineWidth;
		}
		
		lineList.Add( new VectorLine("RedLine", new List<Vector2>(), tex, useLineWidth, LineType.Continuous, Joins.Weld));
        lineList[0].color = Color.red;
        lineList[0].endPointsUpdate = 1;
        lineList.Add( new VectorLine("GreenLine", new List<Vector2>(), tex, useLineWidth, LineType.Continuous, Joins.Weld));
        lineList[1].color = Color.green;
        lineList[1].endPointsUpdate = 1;
        lineList.Add( new VectorLine("BlueLine", new List<Vector2>(), tex, useLineWidth, LineType.Continuous, Joins.Weld));
        lineList[2].color = Color.blue;
        lineList[2].endPointsUpdate = 1;

        line = lineList[colorID];
		//line.endPointsUpdate = 1;	// Optimization for updating only the last point of the line, and the rest is not re-computed
		if (useEndCap) {
			line.endCap = "RoundCap";
		}
		// Used for .sqrMagnitude, which is faster than .magnitude
		sqrMinPixelMove = minPixelMove*minPixelMove;
	}

	public void Update (){
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

    public void ClearLines()
    {
        lineList[0].points2.Clear();
        lineList[1].points2.Clear();
        lineList[2].points2.Clear();
        lineList[0].Draw();
        lineList[1].Draw();
        lineList[2].Draw();
    }

    public void ToggleCanDraw(bool state)
    {
        lineList[0].Draw();
        lineList[1].Draw();
        lineList[2].Draw();
        canDraw = state;
    }

    public void ToggleColor()
    {
        line = lineList[++colorID % 3];
        lineList[0].Draw();
        lineList[1].Draw();
        lineList[2].Draw();
    }

	public void setBrush(int sBrush){
		tex = brushes[sBrush];
		line = new VectorLine("DrawnLine", new List<Vector2>(), tex, useLineWidth, LineType.Continuous, Joins.Weld);
	}

	public void setBrushes(List<Texture> brushes){
		this.brushes = brushes;
	}

	public List<Texture> getBrushes(){
		return brushes;
	}
}
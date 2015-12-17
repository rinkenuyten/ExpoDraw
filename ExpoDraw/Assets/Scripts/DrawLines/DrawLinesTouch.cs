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
        //VectorLine.SetCanvasCamera(this.gameObject.GetComponent<Camera>());
		if (useEndCap) {
			VectorLine.SetEndCap ("RoundCap", EndCap.Mirror, capLineTex, capTex);
			 tex= capLineTex;
			 useLineWidth= capLineWidth;
		}
		else {
            //setBrush (selectedBrush);
			useLineWidth = lineWidth;
		}
		
        /// Instantiate Vectorlines here

		//line.endPointsUpdate = 1;	// Optimization for updating only the last point of the line, and the rest is not re-computed
		if (useEndCap) {
			line.endCap = "RoundCap";
		}
		// Used for .sqrMagnitude, which is faster than .magnitude
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
            foreach (Texture texture in opdracht.Textures)
            {
                foreach (Color color in opdracht.Colors)
                {
                    Lines.Add(new VectorLine(LineNr.ToString() + color.ToString() + texture.ToString(), new List<Vector2>(), texture, size, 
                        LineType.Continuous, Joins.Weld));
                }
            }
        }
        activeLine = Lines[0];
    }

    public void SetBrush(string brushID)
    {
       
    }

}
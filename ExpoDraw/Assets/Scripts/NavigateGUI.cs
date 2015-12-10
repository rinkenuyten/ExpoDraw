using UnityEngine;
using System.Collections;

public class NavigateGUI : MonoBehaviour {

    //fields
    public Canvas[] Canvases;
    private Canvas[] lastScreen;
    private int currentLayer = 1;

	// Use this for initialization
	void Start () {
        lastScreen = new Canvas[10];
	}

    /// <summary>
    /// Moves to the next canvas.
    /// </summary>
    /// <param name="canvas">The canvas you will move to.</param>
    public void MoveTo(Canvas canvas)
    {
        foreach (Canvas c in Canvases)
        {
            //Save as last screen when enabled is true
            //This is the current screen (before moving)
            if (c.enabled == true)
            {
                lastScreen[currentLayer] = c;    
                currentLayer++;              
            }

            //Enable and disable visibility for all canvases
            if (c.name == canvas.name)
            {
                c.enabled = true;
                
            }
            else
            {
                c.enabled = false;
            }
        }
    }

    /// <summary>
    /// Moves back to the last screen used
    /// </summary>
    public void MoveBack()
    {
        foreach (Canvas c in Canvases)
        {
            if (c == lastScreen[currentLayer - 1])
            {
                c.enabled = true;
                currentLayer--;
            }
            else
            {
                c.enabled = false;
            }
        }
    }

}

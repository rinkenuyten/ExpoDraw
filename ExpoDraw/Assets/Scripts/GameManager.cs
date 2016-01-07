using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {
    public List<Opdracht> Opdrachten;
    public Opdracht ActieveOpdracht;
    public Camera cam;
    public Texture texture1;
    public Texture texture2;

	void Start () 
    {
        Opdrachten = new List<Opdracht>();
        Screen.orientation = ScreenOrientation.Portrait;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        /////////////////////////////////
        ////// ADD OPDRACHTEN HERE///////
        /////////////////////////////////
        

        List<int> newInts = new List<int>();
        newInts.Add(2);
	    newInts.Add(4);
        newInts.Add(6);
        newInts.Add(8);
	    

        List<Texture> newText = new List<Texture>();
        newText.Add(texture1);

        List<Color> newColor = new List<Color>();
        newColor.Add(Color.red);
        newColor.Add(Color.blue);

        Opdrachten.Add(new Opdracht("yolo", newInts, newText, newColor));
        SetOpdracht(Opdrachten[0]);
	}
	
	void Update () 
    {
	    
	}

    public void SetOpdracht(Opdracht opdracht)
    {
        this.ActieveOpdracht = opdracht;
        cam.SendMessage("StartOpdracht", ActieveOpdracht);
    }
}

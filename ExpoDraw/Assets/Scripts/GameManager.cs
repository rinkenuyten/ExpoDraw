using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {
    public List<Opdracht> Opdrachten;
    public Opdracht ActieveOpdracht;
    public Camera cam;

	void Start () 
    {
        Opdrachten = new List<Opdracht>();
        Screen.orientation = ScreenOrientation.Portrait;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        /////////////////////////////////
        ////// ADD OPDRACHTEN HERE///////
        /////////////////////////////////
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

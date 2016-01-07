using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using AssemblyCSharp;

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
        
		//--------------------

        List<int> newInts = new List<int>();
	    for (int i = 5; i < 8; i++)
	    {
	        newInts.Add(i);
	    }

        List<Texture> newText = new List<Texture>();
        newText.Add(texture1);

        List<Color> newColor = new List<Color>();
        newColor.Add(Color.red);
        newColor.Add(Color.blue);

        Opdrachten.Add(new Opdracht("yolo", "Goede beschrijving", newInts, newText, newColor));

		Painting nachtwacht = new Painting ("Nachtwacht", Opdrachten);
		//--------------------



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

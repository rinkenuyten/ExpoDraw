using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using AssemblyCSharp;

public class GameManager : MonoBehaviour {
    public List<Opdracht> Opdrachten;
	public List<Painting> paintings;
    public Opdracht ActieveOpdracht;
    public Camera cam;
    public Texture texture1;
    public Texture texture2;
	public Texture texture3;

	void Start () 
    {
        Opdrachten = new List<Opdracht>();
        Screen.orientation = ScreenOrientation.Portrait;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;



		List<int> newInts = new List<int>();
		List<Texture> newBrush = new List<Texture>();
		List<Color> newColor = new List<Color>();



        /////////////////////////////////
        ////// ADD OPDRACHTEN HERE///////
        /////////////////////////////////
        
		//--------------------

	    for (int i = 5; i < 10; i++)
	    {
	        newInts.Add(i);
	    }
        
		newBrush.Add(texture1);

        
        newColor.Add(Color.red);
        newColor.Add(Color.blue);
		newColor.Add(Color.yellow);
		newColor.Add (Color.green);

		Opdrachten.Add(new Opdracht("Maak van Donker Licht", "Rembrandt heeft dit schilderij erg donker getekend om het realistischer over te laten komen. Teken dit schilderij met lichte kleuren.", newInts, newBrush, newColor));




		newInts = new List<int>();
		newBrush = new List<Texture> ();
		newColor = new List<Color> ();

		for (int i = 4; i < 9; i++)
		{
			newInts.Add(i);
		}

		newBrush.Add(texture1);

		newColor.Add(Color.red);
		newColor.Add(Color.black);
		newColor.Add(Color.white);
		newColor.Add (Color.blue);
		newColor.Add (Color.green);

		Opdrachten.Add(new Opdracht("Teken symbolieken", "Volgens de geleerde heeft Rembrandt een aantal symbolieken toegevoegd aan dit schilderij. Een hiervan is “De uitgetrokken handschoen van de kapitein”.", newInts, newBrush, newColor));


		Painting nachtwacht = new Painting ("Nachtwacht", Opdrachten);
		paintings.Add (nachtwacht);

		//--------------------
		// new painting


		Opdrachten.Clear ();

		newInts = new List<int>();
		newBrush = new List<Texture> ();
		newColor = new List<Color> ();

		newInts.Add(6);
		newInts.Add(12);

		newBrush.Add(texture1);
		newBrush.Add(texture2);

		newColor.Add(Color.blue);
		newColor.Add(Color.red);
		newColor.Add(Color.yellow);
		newColor.Add(Color.green);

		Opdrachten.Add(new Opdracht("Emoties", "In dit kunstwerk worden de reacties van de apostelen weergegeven toen Jezus vertelde dat een van hen hem verraden zou hebben. De apostelen zijn in 4 groepjes verdeeld. Elke groep apostelen heeft een eigen emotie centraal staan. Neem 1 groepje en teken jouw interpretatie van hun reactie (emotie).", newInts, newBrush, newColor));

		newInts = new List<int>();
		newBrush = new List<Texture> ();
		newColor = new List<Color> ();

		newInts.Add(4);

		newBrush.Add(texture1);
		newBrush.Add(texture2);

		newColor.Add(Color.blue);
		newColor.Add(Color.red);
		newColor.Add(Color.yellow);
		newColor.Add(Color.green);

		Opdrachten.Add(new Opdracht("Emoties", "In dit kunstwerk worden de reacties van de apostelen weergegeven toen Jezus vertelde dat een van hen hem verraden zou hebben. De apostelen zijn in 4 groepjes verdeeld. Elke groep apostelen heeft een eigen emotie centraal staan. Neem 1 groepje en teken jouw interpretatie van hun reactie (emotie).", newInts, newBrush, newColor));

		Painting avondmaal = new Painting ("Het Laatste Avondmaal", Opdrachten);
		paintings.Add (avondmaal);


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

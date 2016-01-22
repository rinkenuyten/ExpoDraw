using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using AssemblyCSharp;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    public List<Opdracht> Opdrachten;
	public List<Painting> paintings;
    public Opdracht ActieveOpdracht;
    public Camera cam;
    public Texture texture1;
    public Texture texture2;
	public Texture texture3;
	public Texture texture4;
	public Texture texture5;

    [SerializeField]
    List<int> newInts = new List<int>();
    [SerializeField]
    List<Texture> newBrush = new List<Texture>();
    [SerializeField]
    List<Color> newColor = new List<Color>();

	void Start () 
    {
        //Adding eventhandler to the 'ready' button.
        //Game manager
        if (Application.loadedLevelName == "Workspace_Yamil")
        {
            GameObject GUIManager = GameObject.Find("UIManager").gameObject;
            NavigateGUI navGUI = GUIManager.GetComponent<NavigateGUI>();
            Canvas rightCanvas = null;
            foreach (Canvas c in navGUI.Canvases) {
                if (c.name == "ResultCanvas")
                {
                    rightCanvas = c;
                }
            }
            //Screenshotmanager
            GameObject screenshotManger = GameObject.Find("ScreenShotManager").gameObject;
            ScreenshotOnly ssManager = screenshotManger.GetComponent<ScreenshotOnly>();
            //CurrentCanvas
            GameObject canvas = GameObject.Find("Canvas").gameObject;
            //The done button
            GameObject buttonObj = GameObject.Find("Canvas/canvas options/canvas done/done").gameObject;
            Button button = buttonObj.GetComponent<Button>();
            button.onClick.AddListener(() => ssManager.MakeScreenshot());
            /*button.onClick.AddListener(() => canvas.SetActive(false));
            button.onClick.AddListener(() => navGUI.MoveTo(rightCanvas));*/
            //The back button
            buttonObj = GameObject.Find("Canvas/canvas options/canvas done/back").gameObject;
            button = buttonObj.GetComponent<Button>();
            button.onClick.AddListener(() => canvas.SetActive(false));
            button.onClick.AddListener(() => navGUI.MoveBack());
            //The no button form the 'areyousurepopup'
            buttonObj = GameObject.Find("AreYouSurePopup/btnNo").gameObject;
            button = buttonObj.GetComponent<Button>();
            button.onClick.AddListener(() => canvas.SetActive(true));
        }
        if (Application.loadedLevelName == "Workspace_Laura")
        {
            GameObject GUIManager = GameObject.Find("UIManager").gameObject;
            NavigateGUI navGUI = GUIManager.GetComponent<NavigateGUI>();
            //
            GameObject canvas = GameObject.Find("Gallery").gameObject;
            //The back button
            GameObject buttonObj = GameObject.Find("Gallery/Done").gameObject;
            Button button = buttonObj.GetComponent<Button>();
            button.onClick.AddListener(() => canvas.SetActive(false));
            button.onClick.AddListener(() => navGUI.ReturnMainMenu());
        }

        Opdrachten = new List<Opdracht>();
        paintings = new List<Painting>();

        Screen.orientation = ScreenOrientation.Portrait;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;


        /////////////////////////////////
        ////// ADD OPDRACHTEN HERE///////
        /////////////////////////////////

        //------------------------------------------------------------------------------------------------------------------------

        newInts = new List<int>();
        newInts.Add(6 + 5);
        newInts.Add(8 + 5);
        newInts.Add(10 + 5);
        newInts.Add(12 + 5);
	    
        
        newColor.Add(Color.red);
        newColor.Add(Color.blue);
		newColor.Add(Color.yellow);
		newColor.Add (Color.green);

		Opdrachten.Add(new Opdracht("Maak van Donker Licht", "Rembrandt heeft dit schilderij erg donker getekend om het realistischer over te laten komen. Teken dit schilderij met lichte kleuren.", newInts, newBrush, newColor));

        //------------------------------------------------------------------------------------------------------------------------

        newInts.Clear();
		newInts = new List<int>();
		newBrush = new List<Texture> ();
		newColor = new List<Color> ();

        newInts.Add(1 + 5);
        newInts.Add(4 + 5);
        newInts.Add(6 + 5);
        newInts.Add(9 + 5);
        newInts.Add(12 + 5);

		newBrush.Add(texture1);

		newColor.Add(Color.red);
		newColor.Add(Color.black);
		newColor.Add(Color.white);
		newColor.Add (Color.blue);
		newColor.Add (Color.green);

		Opdrachten.Add(new Opdracht("Teken symbolieken", "Volgens de geleerde heeft Rembrandt een aantal symbolieken toegevoegd aan dit schilderij. Een hiervan is “De uitgetrokken handschoen van de kapitein”.", newInts, newBrush, newColor));


		Painting nachtwacht = new Painting ("Nachtwacht", Opdrachten);
		paintings.Add (nachtwacht);

        //--------------------------------------------------------------------------------------------------------------------------------------------
		// new painting


		Opdrachten.Clear ();

		newInts = new List<int>();
		newBrush = new List<Texture> ();
		newColor = new List<Color> ();

        newInts.Add(6 + 5);
        newInts.Add(9 + 5);

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

        newInts.Add(4 + 5);

		newBrush.Add(texture1);
		newBrush.Add(texture2);
		newBrush.Add(texture3);

		newColor.Add(Color.black);
		newColor.Add(Color.white);
		newColor.Add(Color.gray);

		Opdrachten.Add(new Opdracht("Tekentechniek: Clair-Obscur", "Da Vinci heeft 2 verschillende technieken ontworpen. Een hiervan heet: “Clair-Obscur”\nDit is een techniek waarbij de vorm gesuggereerd wordt door het gebruik van licht en donker. Deze contrasten tussen licht en donker worden sterker weergegeven dan ze in werkelijkheid zijn. Hierbij worden de objecten op de achtergrond donker te kleuren en objecten op de voorgrond licht.  Dit is een hulpmiddel om werk realistisch te laten lijken.\nProbeer met behulp van deze techniek dit schilderij na te maken.", newInts, newBrush, newColor));

		newInts = new List<int>();
		newBrush = new List<Texture> ();
		newColor = new List<Color> ();

        newInts.Add(4 + 5);
        newInts.Add(6 + 5);

		newBrush.Add(texture1);

		newColor.Add(Color.red);
		newColor.Add(Color.blue);
		newColor.Add(Color.yellow);

		Opdrachten.Add(new Opdracht("Portret", "De meeste schilderijen rond deze tijd waren portret schilderijen. Doordat Da Vinci hier geen portret heeft geschilderd, maar juist een groep mensen, maakt dit zo bijzonder voor deze tijd. Neem één persoon uit dit schilderij en teken van deze persoon een portret.\n", newInts, newBrush, newColor));


		Painting avondmaal = new Painting ("Het Laatste Avondmaal", Opdrachten);
		paintings.Add(avondmaal);

        //--------------------------------------------------------------------------------------------------------------------------------------------
		// new painting


		Opdrachten.Clear ();

		newInts = new List<int>();
		newBrush = new List<Texture> ();
		newColor = new List<Color> ();

        newInts.Add(1 + 5);
        newInts.Add(4 + 5);
        newInts.Add(6 + 5);
        newInts.Add(9 + 5);

		newBrush.Add(texture1);
		newBrush.Add(texture2);

		newColor.Add(Color.yellow);
		newColor.Add(Color.blue);
		newColor.Add(Color.red);

		Opdrachten.Add(new Opdracht("Ochtend naar middag", "Het schilderij is geschilderd net voordat de zon op komt. Maar hoe zou dit schilderij er uit zien als het in de middag werd geschilderd? Misschien is alles wel scherper aangezien het niet meer schemer", newInts, newBrush, newColor));

		newInts = new List<int>();
		newBrush = new List<Texture> ();
		newColor = new List<Color> ();

        newInts.Add(4 + 5);
        newInts.Add(6 + 5);
        newInts.Add(9 + 5);

		newBrush.Add(texture1);

		newColor.Add(Color.blue);
		newColor.Add(Color.yellow);
		newColor.Add(Color.black);
		newColor.Add(Color.white);
		newColor.Add(Color.cyan);

		Opdrachten.Add(new Opdracht("Van verf naar potlood", "Van Gogh maakte dit schilderij met alleen olie verf.\nzorg er voor dat je die schilderij zo goed mogelijk natekenend met alleen potlood.", newInts, newBrush, newColor));

		newInts = new List<int>();
		newBrush = new List<Texture> ();
		newColor = new List<Color> ();

        newInts.Add(4 + 5);
        newInts.Add(6 + 5);
        newInts.Add(9 + 5);

		newBrush.Add(texture1);

		newColor.Add(Color.yellow);
		newColor.Add(Color.red);
		newColor.Add(Color.blue);
		newColor.Add(Color.cyan);
		newColor.Add(Color.black);
		newColor.Add(Color.white);

		Opdrachten.Add(new Opdracht("Tekenen met bogen", "In dit schilderij is het gebruik van bogen erg belangrijk. Probeer nu zelf dit schilderij na te tekenen zonder het gebruik van rechte lijnen.", newInts, newBrush, newColor));

		Painting sterrennacht = new Painting ("Sterrennacht", Opdrachten);
		paintings.Add(sterrennacht);

        //--------------------------------------------------------------------------------------------------------------------------------------------
        // new painting


        Opdrachten.Clear();

        newInts = new List<int>();
        newBrush = new List<Texture>();
        newColor = new List<Color>();

        newInts.Add(6 + 5);
        newInts.Add(9 + 5);

        newBrush.Add(texture1);
        newBrush.Add(texture2);

        newColor.Add(Color.yellow);
        newColor.Add(Color.blue);
        newColor.Add(Color.red);
        newColor.Add(Color.white);

        Opdrachten.Add(new Opdracht("Tekenen op inspiratie", "De inspiratie van dit schilderij kwam van het stratenpatroon in New York en van de swingende jazzmuziek van die tijd. Teken jouw stratenpatroon waar je woont met de verkregen inspiratie.", newInts, newBrush, newColor));

        newInts = new List<int>();
        newBrush = new List<Texture>();
        newColor = new List<Color>();

        newInts.Add(4 + 5);
        newInts.Add(6 + 5);
        newInts.Add(9 + 5);

        newBrush.Add(texture1);
        newBrush.Add(texture2);
        newBrush.Add(texture3);
        newBrush.Add(texture4);
        newBrush.Add(texture5);

        newColor.Add(Color.yellow);

        Opdrachten.Add(new Opdracht("Tekenen met één kleur", "Mondriaan tekende met primaire kleuren en abstracte vormen. Maak een eigen sfeerimpressie waarbij iedere kleur een andere richting weergeeft.(Horizontaal/Verticaal/Diagonaal)", newInts, newBrush, newColor));

        newInts = new List<int>();
        newBrush = new List<Texture>();
        newColor = new List<Color>();

        newInts.Add(4 + 5);
        newInts.Add(6 + 5);
        newInts.Add(9 + 5);

        newBrush.Add(texture1);

        newColor.Add(Color.yellow);
        newColor.Add(Color.red);
        newColor.Add(Color.blue);

        Opdrachten.Add(new Opdracht("Wat zie jij?", "Mondriaan is een schilder die vooral abstracte kunstwerken maakt. Ook dit is weer een abstract kunstwerk. Teken je hobby met de kleuren die ook in dit kunstwerk verwerkt zijn.", newInts, newBrush, newColor));

        Painting boogie = new Painting("Broadway Boogie Woogie", Opdrachten);
        paintings.Add(boogie);

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

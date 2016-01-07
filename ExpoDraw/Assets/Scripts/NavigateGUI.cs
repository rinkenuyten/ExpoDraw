using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using AssemblyCSharp;

public class NavigateGUI : MonoBehaviour {

    //fields
    public GameObject taskButton;
    private List<Button> cashingObjects;
    private GameManager gameManager;
    public Canvas[] Canvases;
    private Canvas[] lastScreen;
    private int currentLayer = 1;

    //status variables
    public string gameMode { get; set; } //Eiteher "SinglePlayer" or "MultiPlayer"
    public string hostOrJoin { get; set; } //Either "Host" or "Join"
    public Text roomName { get; set; }
    public Text userName { get; set; }
    public string paintingName { get; set; } //Name of the scanned painting


	// Use this for initialization
	void Start () {
        //Array length is based on the maximum layouts you can travel.
        //For example: if you navigate "MainMenu" -> "GameMode" -> "Draw" the minimum size needed will be 3.
        lastScreen = new Canvas[10];
        gameManager = this.GetComponent<GameManager>();
        //These objects will be removed if they aren`t in use anymore.
        //Currently in use for storing the right tasks after loading form the GameManager.
        cashingObjects = new List<Button>();
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
                //If layer is a pop-up layer, dont set them as lastScreen
                if(!c.name.Contains("Popup")){
                    lastScreen[currentLayer] = c;
                    currentLayer++;   
                }                         
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
                if (c.name == "ScanCanvas")
                {
                    clearCahse();
                }
            }
            else
            {
                c.enabled = false;
            }
        }
        currentLayer--;
    }

    /// <summary>
    /// Returns back to the mainmenu-screen
    /// </summary>
    public void ReturnMainMenu()
    {
        lastScreen = new Canvas[10];
        currentLayer = 1;
        foreach (Canvas c in Canvases){
            if (c.name == "MainMenuCanvas")
            {
                c.enabled = true;
            }
            else
            {
                c.enabled = false;
            }
        }
        clearCahse();
    }

    /// <summary>
    /// Depending on variables, different properties will be initialized.
    /// For examle: in the waitingroom, the host needs to see a "GO" button but the other people dont.
    /// So after the refresh, the code checks what you are and sets the right items active.
    /// </summary>
    /// <param name="currentCanvas">The Canvas that needs to be refreshed.</param>
    public void SpecifyCanvas(Canvas currentCanvas)
    {
        if (currentCanvas.name == "WaitingRoomCanvas")
        {
            //Host need to see the "GO" button, other players dont.
            GameObject buttonObj = currentCanvas.transform.Find("btnGo").gameObject;
            Button button = buttonObj.GetComponent<Button>();
            if (hostOrJoin == "Host")
            {               
                button.gameObject.SetActive(true);
            }
            else
            {
                button.gameObject.SetActive(false);
            }
            //Room name needs to be displayed
            GameObject textObj = currentCanvas.transform.Find("txtRoomName").gameObject;
            Text textfield = textObj.GetComponent<Text>();
            textfield.text = this.roomName.text;
        }
        else if (currentCanvas.name == "TaskCanvas")
        {
            Debug.Log(paintingName);

            if (paintingName == null || paintingName == "")
            {
                return;
            }

            GameObject PaintingObj = lastScreen[currentLayer - 1].transform.Find(paintingName).gameObject;
            Button paintingButton = PaintingObj.GetComponent<Button>();
            
            //Searching for the right painting
            List<Opdracht> tasks = new List<Opdracht>();
            foreach (Painting p in gameManager.paintings)
	        {
                if (p.Name == paintingButton.name)
                {
                    tasks = p.Opdrachten;
                }
	        }

            Canvas toMoveAndRefresh = new Canvas();
            //Displaying all found tasks
            foreach(Canvas c in Canvases) 
            {
                if (c.name == "TaskCanvas")
                {
                    toMoveAndRefresh = c;
                }
            }
            int yPos = 300;
            foreach (Opdracht o in tasks)
            {
                GameObject objTask = (GameObject)Instantiate(taskButton);
                objTask.transform.SetParent(currentCanvas.transform, false);
                objTask.name = o.Name;
                Vector3 newVector3 = new Vector3(objTask.transform.position.x, yPos);
                objTask.transform.position = newVector3;

                Button btnTask = objTask.GetComponent<Button>();
                //btnTask.GetComponent<Text>().text = o.Name;
                //btnTask.onClick.AddListener(() => MoveTo(toMoveAndRefresh));
                //btnTask.onClick.AddListener(() => SpecifyCanvas(toMoveAndRefresh));

                cashingObjects.Add(btnTask);

                yPos -= 40;
            }
        }
        else if (currentCanvas.name == "DescriptionCanvas")
        {
            //When playing singleplayer, the "singleplayerChoose" button needs to be activated.
            //When playing multiplayer, the "multiplayerChoose" button needs to be activated.
            GameObject SPbuttonObj = currentCanvas.transform.Find("btnChooseSP").gameObject;
            Button singleplayerButton = SPbuttonObj.GetComponent<Button>();
            GameObject MPbuttonObj = currentCanvas.transform.Find("btnChooseMP").gameObject;
            Button multiplayerButton = MPbuttonObj.GetComponent<Button>();
            if (gameMode == "SinglePlayer")
            {
                singleplayerButton.gameObject.SetActive(true);
                multiplayerButton.gameObject.SetActive(false);
            }
            else
            {
                singleplayerButton.gameObject.SetActive(false);
                multiplayerButton.gameObject.SetActive(true);
            }
        }
        else if (currentCanvas.name == "DrawingCanvas")
        {
            //When playing singleplayer, the "back" button needs to be active.
            //When playing multiplayer, the "back" button needs to be inactive.
            GameObject buttonObj = currentCanvas.transform.Find("btnBack").gameObject;
            Button button = buttonObj.GetComponent<Button>();
            if (gameMode == "SinglePlayer")
            {
                button.gameObject.SetActive(true);
            }
            else
            {
                button.gameObject.SetActive(false);
            }
        }       
    }

    /// <summary>
    /// Clears the found tasks and descriptions.
    /// </summary>
    public void clearCahse()
    {
        foreach (Button b in cashingObjects)
        {
            GameObject obj = GameObject.Find(b.name);
            Destroy(obj);
        }
        cashingObjects.Clear();
    }

    /// <summary>
    /// Quits the application.
    /// </summary>
    public void QuitApp()
    {
        Application.Quit();
    }

}

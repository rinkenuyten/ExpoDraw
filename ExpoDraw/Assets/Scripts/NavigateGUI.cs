using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class NavigateGUI : MonoBehaviour {

    //fields
    public Canvas[] Canvases;
    private Canvas[] lastScreen;
    private TouchScreenKeyboard keyboard;
    private int currentLayer = 1;

    //status variables
    public string gameMode { get; set; } //Eiteher "SinglePlayer" or "MultiPlayer"
    public string hostOrJoin { get; set; } //Either "Host" or "Join"
    public Text roomName { get; set; }
    public Text userName { get; set; } 


	// Use this for initialization
	void Start () {
        //Array length is based on the maximum layouts you can travel.
        //For example: if you navigate "MainMenu" -> "GameMode" -> "Draw" the minimum size needed will be 3.
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
                //If layer is an pop-up layer, dont set them as lastScreen
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
        else if (currentCanvas.name == "DrawingCanvas")
        {
            //If you play singleplayer, the "back" button needs to be active.
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
    /// Toggels the status of the keyboard
    /// </summary>
    /// <param name="openOrClose">"Open" if it needs to be displayed, "Close" if it needs to be disposed</param>
    public void ToggleKeyBoard(string openOrClose)
    {
        if (openOrClose == "Open")
        {
            keyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default, false, false, false, false, "");
        }
        else
        {
            //Dispose?
        }
    }

    /// <summary>
    /// Quits the application.
    /// </summary>
    public void QuitApp()
    {
        Application.Quit();
    }

}

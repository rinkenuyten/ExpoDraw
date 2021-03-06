﻿using UnityEngine;
using System.Collections;
using Vectrosity;

public class UIScript : MonoBehaviour {

    public Camera AccessCamera;
	public GameObject MenuButton1;
	public GameObject MenuButton2;
	private bool expandedMenu = false;
    private bool expandedDrawing = false;
	

	private Color selectedColor;

    public void setColor()
    {
        AccessCamera.SendMessage("setColor", "270532608");
    }
    public void SetBrush()
    {

        AccessCamera.SendMessage("SetBrush", "Thick");
    }

    public void OnUndoButtonClick()
    {
        AccessCamera.SendMessage("Undo");
    }

    public void OnClearButtonClick()
    {
        AccessCamera.SendMessage("ClearLines");
    }

    public void OnPointerEnter()
    {
        AccessCamera.SendMessage("ToggleCanDraw", false);
    }

    public void OnPointerExit()
    {
        AccessCamera.SendMessage("ToggleCanDraw", true);
    }
	
	public void SetActiveColor(string color){
		
		Color activeColor = new Color();
		switch (color)
		{
		case "red":
			activeColor = Color.red;
			break;
		case "blue":
			activeColor = Color.blue;
			break;
		case "yellow":
			activeColor = Color.yellow;
			break;
		case "green":
			activeColor = Color.green;
			break;
		case "black":
			activeColor = Color.black;
			break;
		default:
			activeColor = Color.black;
			break;
		}
			
		
		AccessCamera.SendMessage("setColor", activeColor.ToString());
	}

	public void SetActiveBrush (string brush){
		AccessCamera.SendMessage("setBrush", brush);
	}

	public void SetActiveSize (int size){
		AccessCamera.SendMessage("setSize", size);
	}

    public void ShowInformationTask()
    {

    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void MenuButton1Click(){
		if (!expandedMenu) {
            expandedMenu = true;
			//MenuButton2.SetActive (false);

			MenuButton1.transform.FindChild ("canvas tools").gameObject.SetActive (true);
			MenuButton1.transform.FindChild ("canvas done").gameObject.SetActive (true);
            MenuButton2.transform.FindChild("painterboard tools").gameObject.SetActive(false);
            MenuButton2.transform.FindChild("painterboard colors").gameObject.SetActive(false);
            expandedDrawing = false;
		} else {
            expandedMenu = false;
			//MenuButton2.SetActive (true);

			MenuButton1.transform.FindChild ("canvas tools").gameObject.SetActive (false);
			MenuButton1.transform.FindChild ("canvas done").gameObject.SetActive (false);
		}
	}

	public void MenuButton2Click(){
		if (!expandedDrawing) {
            expandedDrawing = true;
			//MenuButton1.SetActive (false);

			MenuButton2.transform.FindChild ("painterboard tools").gameObject.SetActive (true);
			MenuButton2.transform.FindChild ("painterboard colors").gameObject.SetActive (true);
            MenuButton1.transform.FindChild("canvas tools").gameObject.SetActive(false);
            MenuButton1.transform.FindChild("canvas done").gameObject.SetActive(false);
            expandedMenu = false;
		} else {
            expandedDrawing = false;
			//MenuButton1.SetActive (true);

			MenuButton2.transform.FindChild ("painterboard tools").gameObject.SetActive (false);
			MenuButton2.transform.FindChild ("painterboard colors").gameObject.SetActive (false);
		}
	}
	
}

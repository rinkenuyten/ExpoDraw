using UnityEngine;
using System.Collections;
using Vectrosity;

public class UIScript : MonoBehaviour {

    public Camera AccessCamera;

    public void SetBrush()
    {

        AccessCamera.SendMessage("SetBrush", "Thick");
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
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

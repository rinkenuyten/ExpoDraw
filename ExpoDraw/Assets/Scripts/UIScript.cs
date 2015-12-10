using UnityEngine;
using System.Collections;
using Vectrosity;

public class UIScript : MonoBehaviour {

    public Camera AccessCamera;

    public void OnClearButtonClick()
    {
        AccessCamera.SendMessage("ClearLines");
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

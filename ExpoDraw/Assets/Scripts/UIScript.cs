using UnityEngine;
using System.Collections;
using Vectrosity;

public class UIScript : MonoBehaviour {

    public Camera AccessCamera;
	public GameObject MenuButton1;
	public GameObject MenuButton2;
	private bool expanded = false;

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
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void MenuButton1Click(){
		if (!expanded) {
			expanded = true;
			MenuButton2.SetActive (false);

			MenuButton1.transform.FindChild ("canvas tools").gameObject.SetActive (true);
			MenuButton1.transform.FindChild ("canvas done").gameObject.SetActive (true);
		} else {
			expanded = false;
			MenuButton2.SetActive (true);

			MenuButton1.transform.FindChild ("canvas tools").gameObject.SetActive (false);
			MenuButton1.transform.FindChild ("canvas done").gameObject.SetActive (false);
		}
	}

	public void MenuButton2Click(){
		if (!expanded) {
			expanded = true;
			MenuButton1.SetActive (false);

			MenuButton2.transform.FindChild ("painterboard tools").gameObject.SetActive (true);
			MenuButton2.transform.FindChild ("painterboard colors").gameObject.SetActive (true);
		} else {
			expanded = false;
			MenuButton1.SetActive (true);

			MenuButton2.transform.FindChild ("painterboard tools").gameObject.SetActive (false);
			MenuButton2.transform.FindChild ("painterboard colors").gameObject.SetActive (false);
		}
	}
}

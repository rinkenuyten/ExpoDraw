using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PainterBoard : MonoBehaviour {

	private List<Texture> brushes;

	public GameObject prefabButton;
	public RectTransform ParentPanel;
	private DrawLinesTouch dlt;
	private TouchScreenKeyboard keyboard;

	// Use this for initialization
	void Start () {
		
		//RectTransform.rect.y = 134;

		GameObject drawLineObject = GameObject.FindGameObjectWithTag ("DrawLine");

		if (drawLineObject != null) {
			dlt = drawLineObject.GetComponent <DrawLinesTouch>();
		}
		else {
			Debug.Log ("Cannot find GameControler");
		}

		//brushes = dlt.getBrushes ();

		for(int i = 0; i < brushes.Count; i++)
		{
			GameObject boardButton = (GameObject)Instantiate(prefabButton);
			boardButton.transform.SetParent(ParentPanel, false);
			int newY = -((1 * i)-4);
			Debug.Log (newY);
			Vector3 newVector3 = new Vector3 (boardButton.transform.position.x, newY);
			boardButton.transform.position = newVector3;

			Texture brush = brushes [i];
			boardButton.GetComponentInChildren<Text> ().text = brush.name;

			Button tempButton = boardButton.GetComponent<Button>();
			int tempInt = i;
			tempButton.onClick.AddListener(() => ButtonClicked(tempInt));
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void ButtonClicked(int buttonNo)
	{
		Debug.Log ("Button clicked = " + buttonNo);
		keyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default);
		keyboard.active = true;
		//dlt.setBrush (buttonNo);
	}
}

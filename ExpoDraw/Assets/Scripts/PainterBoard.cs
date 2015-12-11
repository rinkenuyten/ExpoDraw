using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PainterBoard : MonoBehaviour {

	public List<Texture> Brushes;

	public GameObject prefabButton;
	public RectTransform ParentPanel;

	public GameObject sendTo;

	// Use this for initialization
	void Start () {
		for(int i = 0; i < Brushes.Count; i++)
		{
			GameObject boardButton = (GameObject)Instantiate(prefabButton);
			boardButton.transform.SetParent(ParentPanel, false);
			int newY = (-(28 + 5) * i) + 300;
			Vector3 newVector3 = new Vector3 (boardButton.transform.position.x, newY);
			boardButton.transform.position = newVector3;

			Texture brush = Brushes [i];
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
		sendTo.SendMessage ("setBrush", buttonNo);
//		dlt.test ();
//		dlt.setBrush(buttonNo);

	}
}

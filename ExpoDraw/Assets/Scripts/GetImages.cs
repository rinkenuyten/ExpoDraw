using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class GetImages : MonoBehaviour {
		public GameObject scriptObject;

	// Use this for initialization
	void Start () {
	}

    void Update()
    {
        if (scriptObject.GetComponent<Screenshot>().loading)
        {
            int allImagesIndex = 1;
            scriptObject.GetComponent<Screenshot>().indexToGet = 1;
            while (scriptObject.GetComponent<Screenshot>().indexToGet < scriptObject.GetComponent<Screenshot>().index)
            {
                scriptObject.GetComponent<Screenshot>().texture = LoadPNG("/data/data/com.ExposureGames.expodraw/files/" + scriptObject.GetComponent<Screenshot>().imageName + scriptObject.GetComponent<Screenshot>().indexToGet + ".png");

                if (scriptObject.GetComponent<Screenshot>().texture != null)
                {
                    Rect rect = new Rect(0, 0, scriptObject.GetComponent<Screenshot>().texture.width, scriptObject.GetComponent<Screenshot>().texture.height);
                    Debug.Log(rect);
                    Vector2 pivot = new Vector2(scriptObject.GetComponent<Screenshot>().texture.width / 2, scriptObject.GetComponent<Screenshot>().texture.height / 2);
                    scriptObject.GetComponent<Screenshot>().viewImages.transform.GetChild(allImagesIndex).GetComponent<Image>().sprite = Sprite.Create(scriptObject.GetComponent<Screenshot>().texture, rect, pivot);
                    scriptObject.GetComponent<Screenshot>().viewImages.transform.GetChild(allImagesIndex).GetChild(0).GetComponent<Text>().text = "";
                }
                scriptObject.GetComponent<Screenshot>().indexToGet++;
                if (allImagesIndex < scriptObject.GetComponent<Screenshot>().viewImages.transform.childCount)
                {
                    allImagesIndex++;
                }
            }

            scriptObject.GetComponent<Screenshot>().loading = false;
            scriptObject.GetComponent<Screenshot>().loadingCanvas.SetActive(false);
            scriptObject.GetComponent<Screenshot>().viewImages.SetActive(true);
        }
    }
	
	public static Texture2D LoadPNG(string filePath)
	{
		
		Texture2D tex = null;
		byte[] fileData;
		
		Debug.Log("loading");
		
		if (File.Exists(filePath))
		{
			fileData = File.ReadAllBytes(filePath);
			tex = new Texture2D(2, 2);
			tex.LoadImage(fileData); //..this will auto-resize the texture dimensions.
			Debug.Log(tex);
		}
		return tex;
	}
}

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class GetImages : MonoBehaviour {
	public GameObject scriptObject;
    int allImagesIndex = 1;
	int indexToGet = 1;
	public Texture2D texture;

    // Use this for initialization
    void Start () {
	}

    void Update()
    {
        if (scriptObject.GetComponent<Screenshot>().loading)
        {
	        while (indexToGet < scriptObject.GetComponent<Screenshot>().viewImages.transform.childCount)
	        {
		        getImage();
		        indexToGet++;
		        if (allImagesIndex < scriptObject.GetComponent<Screenshot>().viewImages.transform.childCount)
		        {
			        allImagesIndex++;
		        }
	        }

            if (indexToGet == scriptObject.GetComponent<Screenshot>().viewImages.transform.childCount)
            {
                indexToGet = 1;
                allImagesIndex = 1;
            }

            scriptObject.GetComponent<Screenshot>().loading = false;
            scriptObject.GetComponent<Screenshot>().loadingCanvas.SetActive(false);
            scriptObject.GetComponent<Screenshot>().viewImages.SetActive(true);
        }
    }
	
	public void getImage()
	{
		texture = LoadPNG("Screenshot_" + indexToGet + ".png");
		
		if (texture != null)
		{
			Rect rect = new Rect(0, 0, texture.width, texture.height);
			Vector2 pivot = new Vector2(texture.width / 2, texture.height / 2);
			scriptObject.GetComponent<Screenshot>().viewImages.transform.GetChild(allImagesIndex).GetComponent<Image>().sprite = Sprite.Create(texture, rect, pivot);
		}
	}
	
	public Texture2D LoadPNG(string fileName)
	{
		Texture2D tex = null;
		byte[] fileData;
		
		DirectoryInfo dir = new DirectoryInfo(Application.persistentDataPath);
		FileInfo[] images = dir.GetFiles(fileName);
		if (images.Length > 0)
		{
			fileData = File.ReadAllBytes(images[0].FullName);
			tex = new Texture2D(2, 2);
			tex.LoadImage(fileData);
		}
		return tex;
	}
}

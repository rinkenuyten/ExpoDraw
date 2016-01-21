using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class GetImages : MonoBehaviour {
		public GameObject scriptObject;
        int allImagesIndex = 1;
        int indexToGet = 1;

    // Use this for initialization
    void Start () {
	}

    void Update()
    {
        if (scriptObject.GetComponent<Screenshot>().loading)
        {
            while (indexToGet < scriptObject.GetComponent<Screenshot>().viewImages.transform.childCount)
            {
	            scriptObject.GetComponent<Screenshot>().texture = scriptObject.GetComponent<Screenshot>().LoadPNG(scriptObject.GetComponent<Screenshot>().customPath + scriptObject.GetComponent<Screenshot>().imageName + indexToGet + ".png");

                if (scriptObject.GetComponent<Screenshot>().texture != null)
                {
                    Rect rect = new Rect(0, 0, scriptObject.GetComponent<Screenshot>().texture.width, scriptObject.GetComponent<Screenshot>().texture.height);
                    Vector2 pivot = new Vector2(scriptObject.GetComponent<Screenshot>().texture.width / 2, scriptObject.GetComponent<Screenshot>().texture.height / 2);
                    scriptObject.GetComponent<Screenshot>().viewImages.transform.GetChild(allImagesIndex).GetComponent<Image>().sprite = Sprite.Create(scriptObject.GetComponent<Screenshot>().texture, rect, pivot);
                }
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
}

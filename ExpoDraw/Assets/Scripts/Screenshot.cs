using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class Screenshot : MonoBehaviour
{

    public float moveSpeed = 1;
    public int resolution = 3; // 1= default, 2= 2x default, etc.
    public string imageName = "Screenshot_";
    public string customPath = ""; // leave blank for project file location
    public bool resetIndex = false;
    public GameObject gallery;
    public GameObject viewImages;

    private int index = 1;
    private int indexToGet = 1;
    private int pressedButton;
    Texture2D text;

    void Start()
    {
        viewImages.SetActive(false);
        text = new Texture2D(10, 10);
    }

    void Awake()
    {
        if (resetIndex) PlayerPrefs.SetInt("ScreenshotIndex", 0);
        if (customPath != "")
        {
            if (!System.IO.Directory.Exists(customPath))
            {
                System.IO.Directory.CreateDirectory(customPath);
            }
        }
        index = PlayerPrefs.GetInt("ScreenshotIndex") != 0 ? PlayerPrefs.GetInt("ScreenshotIndex") : 1;
    }

    public void MakeScreenshot()
    {
        string path = customPath + imageName + index + ".png";
        Application.CaptureScreenshot(path, resolution);
        index++;
        Debug.Log("Take Screenshot" + index);
        Debug.LogWarning("Screenshot saved: " + customPath + " --- " + imageName + index);
    }


    public void GetPictures(int button)
    {
        pressedButton = button;
        int allImagesIndex = 0;
        indexToGet = 1;
        while (indexToGet < index)
        {
            text = LoadPNG(customPath + imageName + indexToGet + ".png");

            if (text != null)
            {
                Rect rect = new Rect(0, 0, text.width, text.height);
                Debug.Log(rect);
                Vector2 pivot = new Vector2(text.width / 2, text.height / 2);
                viewImages.transform.GetChild(allImagesIndex).GetComponent<Image>().sprite = Sprite.Create(text, rect, pivot);
            }
            indexToGet++;
            if (allImagesIndex < viewImages.transform.GetChildCount())
            {
                allImagesIndex++;
            }
        }

        gallery.SetActive(false);
        viewImages.SetActive(true);
    }

    public void AddToGallery(int childIndex)
    {

        gallery.transform.GetChild(pressedButton).GetComponent<Image>().sprite = viewImages.transform.GetChild(childIndex).GetComponent<Image>().sprite;
        gallery.transform.GetChild(pressedButton).GetChild(0).GetComponent<Text>().text = "";

        gallery.SetActive(true);
        viewImages.SetActive(false);
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


    void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("ScreenshotIndex", (index));
    }
}

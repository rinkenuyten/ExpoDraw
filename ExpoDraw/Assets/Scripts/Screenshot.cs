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
	public GameObject loadingCanvas;
    public RectTransform ParentCanvas;

    public int index = 1;
    public int indexToGet = 1;
    private int pressedButton;
    public Texture2D texture;
    public bool loading = false;

    void Start()
    {
	    viewImages.SetActive(false);
	    loadingCanvas.SetActive(false);
        texture = new Texture2D(10, 10);
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
        gallery.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = customPath;
        Debug.Log("Take Screenshot" + index);
		Debug.LogWarning("Screenshot saved: " + customPath + " --- " + imageName + index);
    }


    public void GetPictures(int button)
	{
		pressedButton = button;
        loading = true;
        Debug.Log(loading);
        loadingCanvas.SetActive(true);
		gallery.SetActive(false);
    }

    public void AddToGallery(int childIndex)
    {

        gallery.transform.GetChild(pressedButton).GetComponent<Image>().sprite = viewImages.transform.GetChild(childIndex).GetComponent<Image>().sprite;
        if (viewImages.transform.GetChild(childIndex).GetChild(0).GetComponent<Text>().text != "No art available")
        {
            gallery.transform.GetChild(pressedButton).GetChild(0).GetComponent<Text>().text = "";
        }

        gallery.SetActive(true);
        viewImages.SetActive(false);
    }

    public void Back()
    {
        gallery.SetActive(true);
        viewImages.SetActive(false);
    }


    void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("ScreenshotIndex", (index));
    }
}

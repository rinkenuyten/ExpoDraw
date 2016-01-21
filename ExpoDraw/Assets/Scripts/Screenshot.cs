using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;

public class Screenshot : MonoBehaviour
{

    public float moveSpeed = 1;
    public int resolution = 3; // 1= default, 2= 2x default, etc.
    public string imageName = "Screenshot_";
	public string customPath = ""; // leave blank for project file location
	public string mapName = "Buttons";
    public GameObject gallery;
	public GameObject viewImages;
	public GameObject loadingCanvas;
    public RectTransform ParentCanvas;

    static int index = 1;
    private int pressedButton;
    public Texture2D texture;
    public bool loading = false;
    bool firstScreenshot = true;

    void Start()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            customPath = "/data/data/com.ExposureGames.expodraw/files";
        }
        else
        {
            customPath = "";
        }
        int buttonIndex = 1;

        while (buttonIndex <= gallery.transform.childCount - 2)
        {
            texture = LoadPNG(customPath + "/Gallery/button" + buttonIndex + ".png");

            Debug.Log(texture);

            if (texture != null)
            {
                Rect rect = new Rect(0, 0, texture.width, texture.height);
                Vector2 pivot = new Vector2(texture.width / 2, texture.height / 2);
                gallery.transform.GetChild(buttonIndex).GetComponent<Image>().sprite = Sprite.Create(texture, rect, pivot);
                gallery.transform.GetChild(buttonIndex).GetChild(0).GetComponent<Text>().text = "";
            }

            buttonIndex++;
        }
        DontDestroyOnLoad(this);
        texture = new Texture2D(10, 10);
    }

    void Awake()
    {
        if (customPath != "")
        {
            if (!System.IO.Directory.Exists(customPath))
            {
                System.IO.Directory.CreateDirectory(customPath);
            }
        }
        else if (mapName != "")
        {
        	if (!System.IO.Directory.Exists(customPath + mapName))
        	{
	        	System.IO.Directory.CreateDirectory(customPath + mapName);
        	}
        }
    }

    public void MakeScreenshot()
	{
        if (firstScreenshot)
        {
            DirectoryInfo di = new DirectoryInfo(Environment.CurrentDirectory);
            FileInfo[] fi = di.GetFiles();
            foreach (FileInfo fiTemp in fi)
            {
                if (Path.GetExtension(customPath + fiTemp) == ".png")
                {
                    index++;
                }
            }
            firstScreenshot = false;
        }

        string path = customPath + imageName + index + ".png";
        Application.CaptureScreenshot(path, resolution);
        index++;
        Debug.Log("Take Screenshot" + index);
		Debug.LogWarning("Screenshot saved: " + customPath + " --- " + imageName + index);
    }


    public void GetPictures(int button)
	{
		pressedButton = button;
        loading = true;
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

    public Texture2D LoadPNG(string filePath)
    {
        Texture2D tex = null;
        byte[] fileData;

        Debug.Log(filePath);

        if (File.Exists(filePath))
        {
            fileData = File.ReadAllBytes(filePath);
            tex = new Texture2D(2, 2);
            tex.LoadImage(fileData); //..this will auto-resize the texture dimensions.
            Debug.Log(tex);
        }
        return tex;
    }

    public void Back()
    {
        gallery.SetActive(true);
	    viewImages.SetActive(false);
    }

    public void SaveGallery()
    {
        int buttonIndex = 1;
        Debug.Log(customPath + "/Gallery/button" + buttonIndex + ".png");
        while (buttonIndex <= gallery.transform.childCount - 2)
        {
            Sprite s = gallery.transform.GetChild(buttonIndex).GetComponent<Image>().sprite;
            Texture2D croppedTexture = new Texture2D((int)s.rect.width, (int)s.rect.height);
            Color[] pixels = s.texture.GetPixels((int)s.textureRect.x,
                                                    (int)s.textureRect.y,
                                                    (int)s.textureRect.width,
                                                    (int)s.textureRect.height);
            croppedTexture.SetPixels(pixels);
            croppedTexture.Apply();

            Byte[] t = croppedTexture.EncodeToPNG();
            File.WriteAllBytes(customPath + "/Gallery/button" + buttonIndex + ".png", t);
            buttonIndex++;
        }
    }

    void OnApplicationQuit()
	{
        PlayerPrefs.SetInt("ScreenshotIndex", (index));
    }
}

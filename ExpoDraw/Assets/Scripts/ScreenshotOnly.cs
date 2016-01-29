using UnityEngine;
using System.Collections;
using System;
using System.IO;

public class ScreenshotOnly : MonoBehaviour {
    public float moveSpeed = 1;
    public int resolution = 3; // 1= default, 2= 2x default, etc.
    public string imageName = "Screenshot_";
    public string customPath = ""; // leave blank for project file location

    static int index = 1;
    bool firstScreenshot = true;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void MakeScreenshot()
    {
        if (firstScreenshot)
        {
            DirectoryInfo di = new DirectoryInfo(Application.persistentDataPath);
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
        Debug.Log("Screenshot saved: " + customPath + "-- - " + imageName + index);

        
        GameObject canvas = GameObject.Find("Canvas").gameObject;
        canvas.SetActive(false);
        GameObject GUIManager = GameObject.Find("UIManager").gameObject;
        NavigateGUI navGUI = GUIManager.GetComponent<NavigateGUI>();
        Canvas rightCanvas = null;
        foreach (Canvas c in navGUI.Canvases)
        {
            if (c.name == "ResultCanvas")
            {
                rightCanvas = c;
            }
        }
        navGUI.MoveTo(rightCanvas);

        GameObject UIScriptManager = GameObject.Find("UIScripts").gameObject;
        UIScript uiScript = UIScriptManager.GetComponent<UIScript>();
        uiScript.OnClearButtonClick();

        GameObject MainCamera = GameObject.Find("Main Camera Yamil").gameObject;
        DrawLinesTouch drawLinesTouch = MainCamera.GetComponent<DrawLinesTouch>();
        drawLinesTouch.ToggleCanDraw(false);
    }
}

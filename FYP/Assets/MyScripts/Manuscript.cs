using UnityEngine;
using UnityEngine.UI;
using Vuforia;

public class Manuscript : MonoBehaviour
{

    public Canvas startCanvas;
    public Canvas vaCanvas;
    public Canvas oilCanvas;
    public Canvas operatingCanvas;
    public Canvas systemStatusCanvas;
    public Canvas maintenanceCanvas;
    public Canvas dashboardCanvas;
	public Canvas homeCanvas;
    public Text systemStatusButtonText;

    private MyImageTrackableEventHandler trackableHandler;
    private string currentCanvasName;

    // Use this for initialization
    void Start()
    {
        trackableHandler = GameObject.Find("ImageTarget").gameObject.GetComponent<MyImageTrackableEventHandler>();

        startCanvas = startCanvas.GetComponent<Canvas>();
        operatingCanvas = operatingCanvas.GetComponent<Canvas>();
        vaCanvas = vaCanvas.GetComponent<Canvas>();
        oilCanvas = oilCanvas.GetComponent<Canvas>();
        maintenanceCanvas = maintenanceCanvas.GetComponent<Canvas>();
        systemStatusCanvas = systemStatusCanvas.GetComponent<Canvas>();
        dashboardCanvas = dashboardCanvas.GetComponent<Canvas>();
		homeCanvas = homeCanvas.GetComponent<Canvas>();

		homeCanvas.GetComponent<Canvas> ().enabled = true;
		startCanvas.GetComponent<Canvas>().enabled = false;
        vaCanvas.GetComponent<Canvas>().enabled = false;
        oilCanvas.GetComponent<Canvas>().enabled = false;
        operatingCanvas.GetComponent<Canvas>().enabled = false;
        systemStatusCanvas.GetComponent<Canvas>().enabled = false;
        maintenanceCanvas.GetComponent<Canvas>().enabled = false;
        dashboardCanvas.GetComponent<Canvas>().enabled = false;

        currentCanvasName = startCanvas.name;

    }

    public void ExitButtonPress()
    {
        //UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }

    public void vaButtonPress()
    {
        currentCanvasName = vaCanvas.name;
        vaCanvas.GetComponent<Canvas>().enabled = true;
        startCanvas.GetComponent<Canvas>().enabled = false;
    }

    public void maintainenanceButtonPress()
    {
        currentCanvasName = maintenanceCanvas.name;
        maintenanceCanvas.GetComponent<Canvas>().enabled = true;
        startCanvas.GetComponent<Canvas>().enabled = false;
    }

    public void resolveButtonPress()
    {
        currentCanvasName = maintenanceCanvas.name;
        maintenanceCanvas.GetComponent<Canvas>().enabled = true;
        systemStatusCanvas.GetComponent<Canvas>().enabled = false;
    }

    public void ocButtonPress()
    {
        currentCanvasName = operatingCanvas.name;
        operatingCanvas.GetComponent<Canvas>().enabled = true;
        startCanvas.GetComponent<Canvas>().enabled = false;
    }

    public void systemButtonPress()
    {
        if (! (systemStatusButtonText.text == "System Healthy"))
        {
            currentCanvasName = systemStatusCanvas.name;
            systemStatusCanvas.GetComponent<Canvas>().enabled = true;
            startCanvas.GetComponent<Canvas>().enabled = false;
            dashboardCanvas.GetComponent<Canvas>().enabled = false;
        }
    }

    public void oilButtonPress()
    {
        currentCanvasName = oilCanvas.name;
        oilCanvas.GetComponent<Canvas>().enabled = true;
        startCanvas.GetComponent<Canvas>().enabled = false;
    }

    public void dashboardButtonPress()
    {
        currentCanvasName = dashboardCanvas.name;
        dashboardCanvas.GetComponent<Canvas>().enabled = true;
        startCanvas.GetComponent<Canvas>().enabled = false;
    }

    public void returnButtonPress()
    {
        currentCanvasName = startCanvas.name;
        vaCanvas.GetComponent<Canvas>().enabled = false;
        operatingCanvas.GetComponent<Canvas>().enabled = false;
        oilCanvas.GetComponent<Canvas>().enabled = false;
        systemStatusCanvas.GetComponent<Canvas>().enabled = false;
        maintenanceCanvas.GetComponent<Canvas>().enabled = false;
        dashboardCanvas.GetComponent<Canvas>().enabled = false;
        startCanvas.GetComponent<Canvas>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (trackableHandler.trackableVisible())
        {
            homeCanvas.GetComponent<Canvas>().enabled = false;
            restoreLastLoadedCanvas();
        }
        else
        {
			homeCanvas.GetComponent<Canvas> ().enabled = true;
			startCanvas.GetComponent<Canvas>().enabled = false;
            vaCanvas.GetComponent<Canvas>().enabled = false;
            oilCanvas.GetComponent<Canvas>().enabled = false;
            operatingCanvas.GetComponent<Canvas>().enabled = false;
            systemStatusCanvas.GetComponent<Canvas>().enabled = false;
            maintenanceCanvas.GetComponent<Canvas>().enabled = false;
            dashboardCanvas.GetComponent<Canvas>().enabled = false;
        }
    }

    private void restoreLastLoadedCanvas()
    {
        if (currentCanvasName == startCanvas.name)
        {
            startCanvas.GetComponent<Canvas>().enabled = true;
        }
        else if (currentCanvasName == vaCanvas.name)
        {
            vaCanvas.GetComponent<Canvas>().enabled = true;
        }
        else if (currentCanvasName == oilCanvas.name)
        {
            oilCanvas.GetComponent<Canvas>().enabled = true;
        }
        else if (currentCanvasName == operatingCanvas.name)
        {
            operatingCanvas.GetComponent<Canvas>().enabled = true;
        }
        else if (currentCanvasName == systemStatusCanvas.name)
        {
            systemStatusCanvas.GetComponent<Canvas>().enabled = true;
        }
        else if (currentCanvasName == maintenanceCanvas.name)
        {
            maintenanceCanvas.GetComponent<Canvas>().enabled = true;
        }
        else if (currentCanvasName == dashboardCanvas.name)
        {
            dashboardCanvas.GetComponent<Canvas>().enabled = true;
        }
    }

}


using UnityEngine;
using UnityEngine.UI;
using EventsAndAnalysers;
using FailureType;
using System;
using System.Collections.Generic;


public class FusionManager : MonoBehaviour {

    public Text displayText;
    public Text systemStatusButtonText;
    public Button gearWearButton;
    public Button gearPittingButton;
    public Button gearFatigueButton;
    public Button bearingWearButton;
    public Button resolveButton;
    public Image healthySystemStatusImage;
    public Image alertSystemStatusImage;

    public Text dashsystemStatusButtonText;
    public Image dashhealthySystemStatusImage;
    public Image dashalertSystemStatusImage;

    //public GameObject gearboxDefaultCADModel;
    public GameObject gearboxGearFailureCADModel;
    public GameObject gearboxBearingFailureCADModel;

    public Text gearWearPercentage;
    public Text gearPittingPercentage;
    public Text gearFatiguePercentage;
    public Text bearingWearPercentage;

    private GearWearFailure gearWear;
    private GearFatigueFailure gearFatigue;
    private GearPittingFailure gearPitting;
    private BearingWearFailure bearingWear;
    private string finalDisplayString;
    private bool allowCADToBeActivated;

    private int curSeconds;
    private List<string> conditionIndicatorList;

    // Use this for initialization
    void Start ()
    {
        gearFatigue = new GearFatigueFailure();
        gearWear = new GearWearFailure();
        gearPitting = new GearPittingFailure();
        bearingWear = new BearingWearFailure();
        conditionIndicatorList = new List<string>();
        curSeconds = DateTime.Now.Second;
        allowCADToBeActivated = true;

        finalDisplayString = "";

        alertSystemStatusImage.enabled = false;
        dashalertSystemStatusImage.enabled = false;
        setAllButtonsToFalse();
        setCADModelInactive();
    }

    public void setCADModelInactive()
    {
        gearboxGearFailureCADModel.gameObject.SetActive(false);
        gearboxBearingFailureCADModel.gameObject.SetActive(false);
    }

    public void setAllButtonsToFalse()
    {
        gearWearButton.gameObject.SetActive(false);
        gearFatigueButton.gameObject.SetActive(false);
        gearPittingButton.gameObject.SetActive(false);
        bearingWearButton.gameObject.SetActive(false);
        resolveButton.gameObject.SetActive(false);
    }

    public void resetAll()
    {
        gearFatigue = new GearFatigueFailure();
        gearWear = new GearWearFailure();
        gearPitting = new GearPittingFailure();
        bearingWear = new BearingWearFailure();
        conditionIndicatorList = new List<string>();
    }

    // Update is called once per frame
    void Update ()
    {
	    if ((curSeconds + 1)%60 == DateTime.Now.Second)
        {
            curSeconds = DateTime.Now.Second;
            setAllButtonsToFalse(); 
            setCADModelInactive();
            if (allowCADToBeActivated && checkForFailure())
            {
                loadConditionIndicatorText();
                setAlarmFontStyle(systemStatusButtonText);
                setAlarmFontStyle(dashsystemStatusButtonText);
                systemStatusFailure();                
                resolveButton.gameObject.SetActive(true);
                displayText.text = finalDisplayString;
            }
        }
	}

    public void systemStatusFailure()
    {
        systemStatusButtonText.text = "Failure detected";
        alertSystemStatusImage.enabled = true;
        healthySystemStatusImage.enabled = false;

        dashsystemStatusButtonText.text = "Failure detected";
        dashalertSystemStatusImage.enabled = true;
        dashhealthySystemStatusImage.enabled = false;
    }

    public void toggleCADActivationStatus (bool value)
    {
        allowCADToBeActivated = value;
    }

    public void loadConditionIndicatorText()
    {
        finalDisplayString = "";
        for (int i = 0; i < conditionIndicatorList.Count; i++)
        {
            finalDisplayString += (" -- " + conditionIndicatorList[i] + "\n");
        }
    }

    public void LoadPublisher(OilAnalyser oilPub, VibrationAnalyser vibPub)
    {
        oilPub.RaiseFerrousDebrisCountEvent += HandleFerrousDebrisCountEvent;
        oilPub.RaiseNonFerrousDebrisCountEvent += HandleNonFerrousDebrisCountEvent;
        oilPub.RaiseFerrousDebrisSizeEvent += HandleFerrousDebrisSizeEvent;
        oilPub.RaiseNonFerrousDebrisSizeEvent += HandleNonFerrousDebrisSizeEvent;

        vibPub.RaisePeakValueEvent += HandlePeakValueEvent;
        vibPub.RaiseRMSValueEvent += HandleRMSValueEvent;
        vibPub.RaiseCFValueEvent += HandleCFValueEvent;
        vibPub.RaiseKurtosisValueEvent += HandleKurtosisValueEvent;
        vibPub.RaiseFM4ValueEvent += HandleFM4ValueEvent;
        vibPub.RaiseNA4ValueEvent += HandleNA4ValueEvent;
    }

    private bool checkForFailure()
    { 
        bool failureDetected = false;

        if (gearWear.checkFailure())
        {
            //finalDisplayString += ("\n   Gear Wear Failure\n"); // + gearWear.getDisplayString());
            failureDetected = true;
            gearWearButton.gameObject.SetActive(true);
            gearWearPercentage.text = gearWear.getFailurePercentage() + " %";
            gearboxGearFailureCADModel.gameObject.SetActive(true);
        }
        if (gearFatigue.checkFailure())
        {
            //finalDisplayString += ("\n   Gear Fatigue Failure\n"); // + gearWear.getDisplayString());
            failureDetected = true;
            gearFatigueButton.gameObject.SetActive(true); ;
            gearFatiguePercentage.text = gearFatigue.getFailurePercentage() + " %";
            gearboxGearFailureCADModel.gameObject.SetActive(true);
        }
        if (gearPitting.checkFailure())
        {
            //finalDisplayString += ("\n   Gear Pitting Failure\n"); // + gearWear.getDisplayString());
            failureDetected = true;
            gearPittingButton.gameObject.SetActive(true);
            gearPittingPercentage.text = gearPitting.getFailurePercentage() + " %";
            gearboxGearFailureCADModel.gameObject.SetActive(true);
        }
        if (bearingWear.checkFailure())
        {
            //finalDisplayString += ("\n   Bearing Wear Failure\n"); // + gearWear.getDisplayString());
            failureDetected = true;
            bearingWearButton.gameObject.SetActive(true);
            bearingWearPercentage.text = bearingWear.getFailurePercentage() + " %";
            gearboxBearingFailureCADModel.gameObject.SetActive(true);
        }
        return failureDetected;
    }

    private void HandleFerrousDebrisCountEvent(object sender, CustomEventArgs e)
    {
        setFeature(FullFeatureList.ferrousCount, e.datetime);
        if (!conditionIndicatorList.Contains(e.message))
        {
            conditionIndicatorList.Add(e.message);
        }
    }

    private void HandleNonFerrousDebrisCountEvent(object sender, CustomEventArgs e)
    {
        setFeature(FullFeatureList.nonFerrousCount, e.datetime);
        if (!conditionIndicatorList.Contains(e.message))
        {
            conditionIndicatorList.Add(e.message);
        }
    }

    private void HandleFerrousDebrisSizeEvent(object sender, CustomEventArgs e)
    {
        setFeature(FullFeatureList.ferrousSize, e.datetime);
        if (!conditionIndicatorList.Contains(e.message))
        {
            conditionIndicatorList.Add(e.message);
        }
    }

    private void HandleNonFerrousDebrisSizeEvent(object sender, CustomEventArgs e)
    {
        setFeature(FullFeatureList.nonFerrousSize, e.datetime);
        if (!conditionIndicatorList.Contains(e.message))
        {
            conditionIndicatorList.Add(e.message);
        }
    }

    private void HandlePeakValueEvent(object sender, CustomEventArgs e)
    {
        setFeature(FullFeatureList.peak, e.datetime);
        if (!conditionIndicatorList.Contains(e.message))
        {
            conditionIndicatorList.Add(e.message);
        }
    }

    private void HandleRMSValueEvent(object sender, CustomEventArgs e)
    {
        setFeature(FullFeatureList.rms, e.datetime);
        if (!conditionIndicatorList.Contains(e.message))
        {
            conditionIndicatorList.Add(e.message);
        }
    }

    private void HandleCFValueEvent(object sender, CustomEventArgs e)
    {
        setFeature(FullFeatureList.cf, e.datetime);
        if (!conditionIndicatorList.Contains(e.message))
        {
            conditionIndicatorList.Add(e.message);
        }
    }

    private void HandleKurtosisValueEvent(object sender, CustomEventArgs e)
    {
        setFeature(FullFeatureList.kurtosis, e.datetime);
        if (!conditionIndicatorList.Contains(e.message))
        {
            conditionIndicatorList.Add(e.message);
        }
    }

    private void HandleFM4ValueEvent(object sender, CustomEventArgs e)
    {
        setFeature(FullFeatureList.fm4, e.datetime);
        if (!conditionIndicatorList.Contains(e.message))
        {
            conditionIndicatorList.Add(e.message);
        }
    }

    private void HandleNA4ValueEvent(object sender, CustomEventArgs e)
    {
        setFeature(FullFeatureList.na4, e.datetime);
        if (!conditionIndicatorList.Contains(e.message))
        {
            conditionIndicatorList.Add(e.message);
        }
    }

    private void setFeature(string key, DateTime occurence)
    {
        if (gearFatigue.featureList.ContainsKey(key))
        {
            gearFatigue.featureList[key]++;
            gearFatigue.totalOccurence++;
            if (!(gearFatigue.firstOccurenceList.ContainsKey(key)))
            {
                gearFatigue.firstOccurenceList[key] = occurence;
                gearFatigue.failedFeatureCount++;
            }
            
        }
        if (gearPitting.featureList.ContainsKey(key))
        {
            gearPitting.featureList[key] ++;
            gearPitting.totalOccurence++;
            if (!(gearPitting.firstOccurenceList.ContainsKey(key)))
            {
                gearPitting.firstOccurenceList[key] = occurence;
                gearPitting.failedFeatureCount++;
            }
        }
        if (gearWear.featureList.ContainsKey(key))
        {
            gearWear.featureList[key] ++;
            gearWear.totalOccurence++;
            if (!(gearWear.firstOccurenceList.ContainsKey(key)))
            {
                gearWear.firstOccurenceList[key] = occurence;
                gearWear.failedFeatureCount++;
            }
        }
        if (bearingWear.featureList.ContainsKey(key))
        {
            bearingWear.featureList[key] ++;
            bearingWear.totalOccurence++;
            if (!(bearingWear.firstOccurenceList.ContainsKey(key)))
            {
                bearingWear.firstOccurenceList[key] = occurence;
                bearingWear.failedFeatureCount++;
            }
        }
    }

    private void setAlarmFontStyle(Text targetText)
    {
        targetText.fontStyle = FontStyle.BoldAndItalic;
        targetText.color = Color.red;
    }

}

using UnityEngine;
using UnityEngine.UI;
using System;
using EventsAndAnalysers;

public class DataManager : MonoBehaviour {

    public TextAsset file;
    public TextAsset gearWearFailureFile;
    public TextAsset gearPittingFailureFile;
    public TextAsset gearFatigueFailureFile;
    public TextAsset bearingWearFailureFile;

    public Text ferrousText;
    public Text nonferrousText;
    public Text ferrousSizeText;
    public Text nonferrousSizeText;
    public Text dashFerrousText;
    public Text dashNonferrousText;
    public Text dashFerrousSizeText;
    public Text dashNonferrousSizeText;

    public Text peakValueText;
    public Text rmsText;
    public Text cfText;
    public Text kurtosisText;
    public Text fm4Text;
    public Text na4Text;
    public Text peakValueTitleText;
    public Text rmsTitleText;
    public Text cfTitleText;
    public Text kurtosisTitleText;
    public Text fm4TitleText;
    public Text na4TitleText;

    public Text dashPeakValueText;
    public Text dashRmsText;
    public Text dashCfText;
    public Text dashKurtosisText;
    public Text dashFm4Text;
    public Text dashNa4Text;
    public Text dashPeakValueTitleText;
    public Text dashRmsTitleText;
    public Text dashCfTitleText;
    public Text dashKurtosisTitleText;
    public Text dashFm4TitleText;
    public Text dashNa4TitleText;

    public Button BearingWearButton;
    public Button GearFatigueButton;
    public Button GearWearButton;
    public Button GearPittingButton;

    public Text spectrographyTestDate;
    public Button resetTestDateButton;
    public Text systemStatusDisplayText;
    public Text systemStatusButtonText;
    public Image healthySystemStatusImage;
    public Image alertSystemStatusImage;
    public Text dashsystemStatusButtonText;
    public Image dashhealthySystemStatusImage;
    public Image dashalertSystemStatusImage;

    public OilAnalysisScript oilAnalysisHandler;
    public VibrationAnalysisScript vibrationAnalysisHandler;
    public FusionManager fusionManager;

    private string ferrousValue;
    private string nonferrousValue;
    private string ferrousSizeValue;
    private string nonferrousSizeValue;
    private string peakValue;
    private string rmsValue;
    private string cfValue;
    private string kurtosisValue;
    private string fm4Value;
    private string na4Value;

    private OilAnalyser oilAnalyser;
    private VibrationAnalyser vibrationAnalyser;   
    private DataTable dataTable;

    private int curSeconds;
    private int prevSeconds;
    private static string defaultWearText = "Particle Count: ";
    private static string defaultSizeText = "Particle Size: ";
    private static string defaultTestDate = "1/10/16";
    private string curTestDate;
    private bool isFailureLoaded = false;
    private string defaultDisplayString = "Gearbox is healthy";
    private string defaultStatusString = "System Healthy";

    public string GetTestDate()
    {
        return curTestDate == null ? defaultTestDate : curTestDate;
    }

    // Use this for initialization
    void Start()
    {
        oilAnalyser = new OilAnalyser();
        vibrationAnalyser = new VibrationAnalyser();
        oilAnalysisHandler.loadPublisher(oilAnalyser);
        vibrationAnalysisHandler.loadPublisher(vibrationAnalyser);
        fusionManager.LoadPublisher(oilAnalyser, vibrationAnalyser);
        dataTable = new DataTable();
        dataTable.Load(file);
        LoadTestDate();

        InitSpectrographyTestDate();
    }

    private void InitSpectrographyTestDate()
    {
        if (curTestDate == null || curTestDate == "")
        {
            spectrographyTestDate.text = defaultTestDate;
        }
        else
        {
            spectrographyTestDate.text = curTestDate;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isFailureLoaded && curSeconds != DateTime.Now.Second)
        {
            curSeconds = DateTime.Now.Second;
            SetNormalFontStyle();

            UpdateOilValues();
            UpdateOilText();
            UpdateVibrationValues();
            UpdateVibrationText();

            oilAnalyser.MonitorOilReading(ferrousValue, ferrousSizeValue, nonferrousValue, nonferrousSizeValue);
            vibrationAnalyser.MonitorVibrationReading(peakValue, rmsValue, cfValue, kurtosisValue, fm4Value, na4Value);
        }
        else if (isFailureLoaded && prevSeconds != DateTime.Now.Second)
        {
            prevSeconds = DateTime.Now.Second;
            SetNormalFontStyle();

            curSeconds = (curSeconds == 299) ? 0 : (curSeconds + 1);
            UpdateOilValues();
            UpdateOilText();
            UpdateVibrationValues();
            UpdateVibrationText();

            oilAnalyser.MonitorOilReading(ferrousValue, ferrousSizeValue, nonferrousValue, nonferrousSizeValue);
            vibrationAnalyser.MonitorVibrationReading(peakValue, rmsValue, cfValue, kurtosisValue, fm4Value, na4Value);
        }
    }

    private void SetNormalFontStyle()
    {
        setNormalFontStyle(ferrousText);
        setNormalFontStyle(nonferrousText);
        setNormalFontStyle(ferrousSizeText);
        setNormalFontStyle(nonferrousSizeText);

        setNormalFontStyle(dashFerrousText);
        setNormalFontStyle(dashNonferrousText);
        setNormalFontStyle(dashFerrousSizeText);
        setNormalFontStyle(dashNonferrousSizeText);

        setNormalFontStyle(peakValueText, peakValueTitleText);
        setNormalFontStyle(rmsText, rmsTitleText);
        setNormalFontStyle(cfText,cfTitleText);
        setNormalFontStyle(kurtosisText, kurtosisTitleText);
        setNormalFontStyle(fm4Text, fm4TitleText);
        setNormalFontStyle(na4Text,na4TitleText);

        setNormalFontStyle(dashPeakValueText, dashPeakValueTitleText);
        setNormalFontStyle(dashRmsText, dashRmsTitleText);
        setNormalFontStyle(dashCfText, dashCfTitleText);
        setNormalFontStyle(dashKurtosisText, dashKurtosisTitleText);
        setNormalFontStyle(dashFm4Text, dashFm4TitleText);
        setNormalFontStyle(dashNa4Text, dashNa4TitleText);
    }

    private void setNormalFontStyle(Text targetText)
    {
        targetText.fontStyle = FontStyle.Normal;
        targetText.color = Color.black;
    }

    private void setButtonPressedStyle(Button button)
    {
        resetAllFailureButton();
        button.image.color = Color.white;
    }

    private void resetAllFailureButton()
    {
        BearingWearButton.image.color = Color.red;
        GearFatigueButton.image.color = Color.red;
        GearWearButton.image.color = Color.red;
        GearPittingButton.image.color = Color.red;
    }

    private void setBoldFontStyle(Text targetText)
    {
        targetText.fontStyle = FontStyle.Bold;
        targetText.color = Color.black;
    }

    private void setNormalFontStyle(Text targetText, Text targetTitle)
    {
        targetText.fontStyle = FontStyle.Normal;
        targetText.color = Color.black;
        targetTitle.fontStyle = FontStyle.Normal;
        targetTitle.color = Color.black;
    }

    private void UpdateOilValues()
    {
        ferrousValue = dataTable.Find_id(curSeconds.ToString()).ferrouscount;
        ferrousSizeValue = dataTable.Find_id(curSeconds.ToString()).ferroussize;
        nonferrousValue = dataTable.Find_id(curSeconds.ToString()).nonferrouscount;
        nonferrousSizeValue = dataTable.Find_id(curSeconds.ToString()).nonferroussize;
    }

    private void UpdateOilText()
    {
        ferrousText.text = defaultWearText + ferrousValue;
        ferrousSizeText.text = defaultSizeText + ferrousSizeValue + " μm";
        nonferrousText.text = defaultWearText + nonferrousValue;
        nonferrousSizeText.text = defaultSizeText + nonferrousSizeValue + " μm";

        dashFerrousText.text = "Count: " + ferrousValue;
        dashFerrousSizeText.text = "Size: " + ferrousSizeValue + " μm";
        dashNonferrousText.text = "Count: " + nonferrousValue;
        dashNonferrousSizeText.text = "Size: " + nonferrousSizeValue + " μm";
    }

    private void UpdateVibrationValues()
    {
        peakValue = dataTable.Find_id(curSeconds.ToString()).peakvalue;
        rmsValue = dataTable.Find_id(curSeconds.ToString()).rms;
        cfValue = dataTable.Find_id(curSeconds.ToString()).cf;
        kurtosisValue = dataTable.Find_id(curSeconds.ToString()).kurtosis;
        fm4Value = dataTable.Find_id(curSeconds.ToString()).fm4;
        na4Value = dataTable.Find_id(curSeconds.ToString()).na4;
    }

    private void UpdateVibrationText()
    {
        peakValueText.text = peakValue;
        rmsText.text =  rmsValue;
        cfText.text = cfValue;
        kurtosisText.text = kurtosisValue;
        fm4Text.text = fm4Value;
        na4Text.text = na4Value;

        dashPeakValueText.text = peakValue;
        dashRmsText.text = rmsValue;
        dashCfText.text = cfValue;
        dashKurtosisText.text = kurtosisValue;
        dashFm4Text.text = fm4Value;
        dashNa4Text.text = na4Value;
    }

    private void LoadTestDate()
    {
        string testDate = PlayerPrefs.GetString("TestDate");
        Debug.Log("Loading Test Date");
        if (testDate == null)
        {
            curTestDate = defaultTestDate;
        }
        else
        {
            curTestDate = testDate;
        }
    }

    public void bearingWearFailureButtonPress()
    {
        dataTable.Load(bearingWearFailureFile);
        isFailureLoaded = true;
        resetStatus();
        prevSeconds = DateTime.Now.Second;
        curSeconds = 0;
        setButtonPressedStyle(BearingWearButton);
    }

    public void gearWearFailureButtonPress()
    {
        dataTable.Load(gearWearFailureFile);
        isFailureLoaded = true;
        resetStatus();
        prevSeconds = DateTime.Now.Second;
        curSeconds = 0;
        setButtonPressedStyle(GearWearButton);
    }

    public void gearPittingFailureButtonPress()
    {
        dataTable.Load(gearPittingFailureFile);
        isFailureLoaded = true;
        resetStatus();
        prevSeconds = DateTime.Now.Second;
        curSeconds = 0;
        setButtonPressedStyle(GearPittingButton);
    }

    public void gearFatigueFailureButtonPress()
    {
        dataTable.Load(gearFatigueFailureFile);
        isFailureLoaded = true;
        resetStatus();
        prevSeconds = DateTime.Now.Second;
        curSeconds = 0;
        setButtonPressedStyle(GearFatigueButton);
    }

    public void ResetTestDateButtonPress()
    {
        Debug.Log("Before: " + curTestDate);
        curTestDate = DateTime.Now.Date.ToShortDateString();
        spectrographyTestDate.text = curTestDate;
        Debug.Log("After: " + curTestDate);
        PlayerPrefs.SetString("TestDate", this.GetTestDate());
    }

    public void resetStatus()
    {
        systemStatusButtonText.text = defaultStatusString;
        healthySystemStatusImage.enabled = true;
        alertSystemStatusImage.enabled = false;
        dashsystemStatusButtonText.text = defaultStatusString;
        dashhealthySystemStatusImage.enabled = true;
        dashalertSystemStatusImage.enabled = false;

        setBoldFontStyle(systemStatusButtonText);
        setBoldFontStyle(dashsystemStatusButtonText);
        systemStatusDisplayText.text = defaultDisplayString;
        fusionManager.setAllButtonsToFalse();
        fusionManager.resetAll();
    }

}

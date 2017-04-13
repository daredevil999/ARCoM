using EventsAndAnalysers;
using UnityEngine;
using UnityEngine.UI;

public class VibrationAnalysisScript : MonoBehaviour
{

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

    public Text dashpeakValueText;
    public Text dashrmsText;
    public Text dashcfText;
    public Text dashkurtosisText;
    public Text dashfm4Text;
    public Text dashna4Text;

    public Text dashpeakValueTitleText;
    public Text dashrmsTitleText;
    public Text dashcfTitleText;
    public Text dashkurtosisTitleText;
    public Text dashfm4TitleText;
    public Text dashna4TitleText;

    void Start()
    {

    }

    void Update()
    {

    }

    public void loadPublisher(VibrationAnalyser pub)
    {
        pub.RaisePeakValueEvent += HandlePeakValueEvent;
        pub.RaiseRMSValueEvent += HandleRMSValueEvent;
        pub.RaiseCFValueEvent += HandleCFValueEvent;
        pub.RaiseKurtosisValueEvent += HandleKurtosisValueEvent;
        pub.RaiseFM4ValueEvent += HandleFM4ValueEvent;
        pub.RaiseNA4ValueEvent += HandleNA4ValueEvent;
    }

    private void HandlePeakValueEvent(object sender, CustomEventArgs e)
    {
        Debug.Log("Peak Value Exceeded");
        setAlarmFontStyle(peakValueText, peakValueTitleText);
        setAlarmFontStyle(dashpeakValueText, dashpeakValueTitleText);
    }

    private void HandleRMSValueEvent(object sender, CustomEventArgs e)
    {
        Debug.Log("RMS Value Exceeded");
        setAlarmFontStyle(rmsText, rmsTitleText);
        setAlarmFontStyle(dashrmsText, dashrmsTitleText);
    }

    private void HandleCFValueEvent(object sender, CustomEventArgs e)
    {
        Debug.Log("Crest Factor Exceeded");
        setAlarmFontStyle(cfText, cfTitleText);
        setAlarmFontStyle(dashcfText, dashcfTitleText);
    }

    private void HandleKurtosisValueEvent(object sender, CustomEventArgs e)
    {
        Debug.Log("Kurtosis value Exceeded");
        setAlarmFontStyle(kurtosisText, kurtosisTitleText);
        setAlarmFontStyle(dashkurtosisText, dashkurtosisTitleText);
    }

    private void HandleFM4ValueEvent(object sender, CustomEventArgs e)
    {
        Debug.Log("FM4 Value Exceeded");
        setAlarmFontStyle(fm4Text,fm4TitleText);
        setAlarmFontStyle(dashfm4Text, dashfm4TitleText);
    }

    private void HandleNA4ValueEvent(object sender, CustomEventArgs e)
    {
        Debug.Log("NA4 Value Exceeded");
        setAlarmFontStyle(na4Text, na4TitleText);
        setAlarmFontStyle(dashna4Text, dashna4TitleText);
    }

    private void setAlarmFontStyle(Text targetText, Text targetTitleText)
    {
        targetText.fontStyle = FontStyle.BoldAndItalic;
        targetText.color = Color.red;

        targetTitleText.fontStyle = FontStyle.BoldAndItalic;
        targetTitleText.color = Color.red;

    }

}


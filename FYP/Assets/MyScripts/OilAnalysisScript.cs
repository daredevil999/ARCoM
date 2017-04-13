using EventsAndAnalysers;
using UnityEngine;
using UnityEngine.UI;

public class OilAnalysisScript : MonoBehaviour {

    public Text ferrousText;
    public Text nonferrousText;
    public Text ferrousSizeText;
    public Text nonferrousSizeText;

    public Text dashferrousText;
    public Text dashnonferrousText;
    public Text dashferrousSizeText;
    public Text dashnonferrousSizeText;

    void Start()
    {

    }

    void Update()
    {

    }

    public void loadPublisher(OilAnalyser pub)
    {
        pub.RaiseFerrousDebrisCountEvent += HandleFerrousDebrisCountEvent;
        pub.RaiseNonFerrousDebrisCountEvent += HandleNonFerrousDebrisCountEvent;
        pub.RaiseFerrousDebrisSizeEvent += HandleFerrousDebrisSizeEvent;
        pub.RaiseNonFerrousDebrisSizeEvent += HandleNonFerrousDebrisSizeEvent;
    }

    private void HandleFerrousDebrisCountEvent (object sender, CustomEventArgs e)
    {
        Debug.Log("Ferrous Debris Count Exceeded");
        setAlarmFontStyle(ferrousText);
        setAlarmFontStyle(dashferrousText);
    }

    private void HandleNonFerrousDebrisCountEvent(object sender, CustomEventArgs e)
    {
        Debug.Log("Non-Ferrous Debris Count Exceeded");
        setAlarmFontStyle(nonferrousText);
        setAlarmFontStyle(dashnonferrousText);
    }

    private void HandleFerrousDebrisSizeEvent(object sender, CustomEventArgs e)
    {
        Debug.Log("Ferrous Debris Size Exceeded");
        setAlarmFontStyle(ferrousSizeText);
        setAlarmFontStyle(dashferrousSizeText);
    }

    private void HandleNonFerrousDebrisSizeEvent(object sender, CustomEventArgs e)
    {
        Debug.Log("Non-Ferrous Debris Size Exceeded");
        setAlarmFontStyle(nonferrousSizeText);
        setAlarmFontStyle(dashnonferrousSizeText);
    }

    private void setAlarmFontStyle (Text targetText)
    {
        targetText.fontStyle = FontStyle.BoldAndItalic;
        targetText.color = Color.red;
    }

}

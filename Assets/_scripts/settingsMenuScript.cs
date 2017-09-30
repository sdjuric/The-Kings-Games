using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class settingsMenuScript : MonoBehaviour {

    public GameObject qualityText;
    public GameObject resText;

    private int currentQuality = 0;
    private string[] qualities = { "Fastest", "Fast", "Simple", "Good", "Beautiful", "Fantastic" };

    private int currentResolution = 0;
	private int[] possibleW = { 800, 1024, 1280,  1280, 1600, 1920 };
	private int[] possibleH = { 600, 768,  720, 1024, 900, 1080 };

    void Start () {
        currentQuality = QualitySettings.GetQualityLevel();
    }

    public void resolutionUp() {
        if (currentResolution < possibleH.Length-1)
            currentResolution++;
        else
            currentResolution = 0;

    }

    public void resolutionDown() {
        if (currentResolution > 0)
            currentResolution--;
        else
            currentResolution = possibleH.Length-1;
    }

    public void qualityUp()
    {
        if (currentQuality < qualities.Length - 1)
            currentQuality++;
        else
            currentQuality = 0;

    }

    public void qualityDown()
    {
        if (currentQuality > 0)
            currentQuality--;
        else
            currentQuality = qualities.Length - 1;
    }

    public void apply() {
        QualitySettings.SetQualityLevel(currentQuality, true);
        Screen.SetResolution(possibleW[currentResolution], possibleH[currentResolution], false);
    }

    void Update () {
        resText.GetComponent<Text>().text = possibleW[currentResolution] +" x "+possibleH[currentResolution];
        qualityText.GetComponent<Text>().text = qualities[currentQuality];
    }
    
}

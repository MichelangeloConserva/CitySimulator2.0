using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityManagementTime : MonoBehaviour
{
    public HUGeneralManager huGeneralManager;



    public System.DateTime realTime;
    public Utils.DayTime dayTime;


    private Dictionary<Utils.DayTime, int> dayTimeDuration;

    private bool changeDayTime;


    void Start()
    {
        changeDayTime = false;


        dayTimeDuration = new Dictionary<Utils.DayTime, int>()
        {
            {Utils.DayTime.earlyMorning, 6},    // 0-6
            {Utils.DayTime.breakfast,    2},    // 6-8
            {Utils.DayTime.morning,      5},    // 8-12    
            {Utils.DayTime.launch,         2},    // 12-14
            {Utils.DayTime.afternoon,    5},    // 14-19
            {Utils.DayTime.dinner,       2},    // 19-21
            {Utils.DayTime.evening,      3},    // 21-24
        };

        int initHours = 6;
        dayTime = Utils.DayTime.breakfast;


        realTime = new System.DateTime(System.DateTime.Now.Year,
                                       System.DateTime.Now.Month,
                                       System.DateTime.Now.Day,
                                       initHours, // hours
                                       0, // minutes
                                       0);// seconds




        StartCoroutine(ChangeDayTime(dayTime));
    }

    void Update()
    {
        realTime = realTime.AddSeconds(Time.deltaTime * Settings.timeMultiplyer);

        if (changeDayTime)
        {
            ChangeDayTime((Utils.DayTime)((((int)dayTime + 1)) % 8));
            changeDayTime = false;
        }


        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
    }



    private IEnumerator ChangeDayTime(Utils.DayTime dayTime)
    {
        // TODO : remove
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();


        this.dayTime = dayTime;


        // Updating the costs for the hus
        huGeneralManager.DayTimeChanged(dayTime);

        yield return new WaitForSeconds(dayTimeDuration[dayTime] * 3600 / Settings.timeMultiplyer);

        changeDayTime = true;
    }

    float deltaTime = 0.0f;
    void OnGUI()
    {
        var style1 = new GUIStyle();
        style1.normal.textColor = Color.black;
        GUI.Label(new Rect(10, 30, 400, 100), "Time: " + realTime.ToString("d/M/yyyy HH:mm:ss ") + dayTime.ToString(), style1);

        int w = Screen.width, h = Screen.height;
        GUIStyle style = new GUIStyle();
        Rect rect = new Rect(10, 80, w, h * 2 / 30);
        style.alignment = TextAnchor.UpperLeft;
        style.fontSize = h * 2 / 30;
        style.normal.textColor = new Color(0.0f, 0.0f, 0.5f, 1.0f);
        float msec = deltaTime * 1000.0f;
        float fps = 1.0f / deltaTime;
        string text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
        GUI.Label(rect, text, style);
    }


}

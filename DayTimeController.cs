using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Experimental.Rendering.Universal;
public enum DayOfWeek

{  
Pazartesi,
Salı,
Çarşamba,
Perşembe,
Cuma,
Cumartesi,
Pazar
}

public enum Season
{
    Kış,
    İlkbahar,
    Yaz,
    Sonbahar
    
}
public class DayTimeController : MonoBehaviour
{
    const float secondsInDay = 86400f; //Oyun içi hızın gerçek dünyaya oranı
    const float phaseLength = 900f; // 15 dakikalık zaman 
    const float phasesInDay = 96f ; // secondsInDay ın phaseLengthe bölünmesi

    [SerializeField] Color nightLightColor; //Gecenin karanlık rengi
    [SerializeField] AnimationCurve nightTimeCurve; //Havanın kararma animasyonu
    [SerializeField] Color dayLightColor = Color.white; //Gündüz beyazlığı

    float time;

    [SerializeField] float timeScale = 60f;
    [SerializeField] float startAtTime = 28800f; // saniye olarak başlama saati
    [SerializeField] float morningTime = 28800f;
    DayOfWeek dayOfWeek;

    [SerializeField] TMPro.TextMeshProUGUI text; //Saati göstermek için yazı
    [SerializeField] TMPro.TextMeshProUGUI dayOfTheWeekText; //Gün gösterimi
    [SerializeField] TMPro.TextMeshProUGUI seasonText; // Mevsim gösterimi
    [SerializeField] Light2D globalLight; //Unity'nin 2D aydıklandırma teknolojisi
    public int days;

    Season currentSeason;
    const int seasonLength = 30;

    List<TimeAgent> agents;

    private void Awake()
    {
        agents = new List<TimeAgent>();
    }

    private void Start()
    {
        time = startAtTime;
        UpdateDayText();
        UpdateSeasonText();
    }

    public void Subscribe(TimeAgent timeAgent)
    {
        agents.Add(timeAgent);
    }

    public void Unsubscribe(TimeAgent timeAgent)
    {
        agents.Remove(timeAgent);
    }

    float Hours
    {
        get { return time / 3600f; }  //Saat ayarı
    }
    float Minutes
    { 
        get { return time % 3600f / 60f; } //Dakika ayarı
    }

    private void Update()
    {


        time += Time.deltaTime * timeScale;
        TimeValueCalculation(); //Saat ve dakikayi 2 haneli olarak gösterme
        DayLight();
        if (time > secondsInDay)
        {
            NextDay();
        }

        TimeAgents();

        if (Input.GetKeyDown(KeyCode.T))
        {
            SkipTime(hours: 4);
        }
    }


    private void TimeValueCalculation()
    {
        int hh = (int)Hours;
        int mm = (int)Minutes;
        text.text = hh.ToString("00") + ":" + mm.ToString("00");
    }

    private void DayLight()
    {
        float v = nightTimeCurve.Evaluate(Hours);

        Color c = Color.Lerp(dayLightColor, nightLightColor, v);  //Saate göre dünyanın aydınlık ve karanlık olması
        globalLight.color = c;
    }

    int oldPhase = -1;
    private void TimeAgents()
    {
        if (oldPhase == -1)
        {
            oldPhase = CalculatePhase();
        }
        int currentPhase = CalculatePhase();
        
        while (oldPhase < currentPhase)
        {
            oldPhase += 1;
            for(int i = 0; i < agents.Count; i++)
            {
                agents[i].Invoke(this);
            }
        }
        

       
    }

    private int CalculatePhase()
    {
        return (int)(time / phaseLength) + (int) (days * phasesInDay);
    }

    private void NextDay()
    {
        time -= secondsInDay; //Saat sıfırlanýnca sonraki güne geçilir
        days += 1;

        int dayNum = (int)dayOfWeek;
        dayNum += 1;
        if (dayNum >= 7)
        {
            dayNum = 0;
        }
        dayOfWeek = (DayOfWeek)dayNum;
        UpdateDayText();

        if (days >= seasonLength)
        {
            NextSeason();
        }
    }

    private void NextSeason()
    {
        days = 0;
        int seasonNum = (int)currentSeason;
        seasonNum += 1;

        if (seasonNum >= 4)
        {
            seasonNum = 0;
        }

        currentSeason = (Season)seasonNum;
        UpdateSeasonText();
    }

    private void UpdateSeasonText()
    {
        seasonText.text = currentSeason.ToString();
    }

    private void UpdateDayText()
    {
        dayOfTheWeekText.text = dayOfWeek.ToString();
    }

    public void SkipTime(float seconds = 0, float minute = 0, float hours = 0)
    {
        float timeToSkip = seconds;
        timeToSkip += minute * 60f;
        timeToSkip += hours * 3600f;

        time += timeToSkip;
    }
    internal void SkipToMorning()
    {
        float secondsToSkip = 0f;

        if(time > morningTime)
        {
            secondsToSkip += secondsInDay - time + morningTime;
        }
        else
        {
            secondsToSkip += morningTime - time;
        }
        SkipTime(secondsToSkip);

    }
}

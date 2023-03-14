using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Experimental.Rendering.Universal;

public class DayTimeController : MonoBehaviour
{
    const float secondsInDay = 86400f; //Oyun içi hızın gerçek dünyaya oranı
    const float phaseLength = 900f; // 15 dakikalık zaman 

    [SerializeField] Color nightLightColor; //Gecenin karanlık rengi
    [SerializeField] AnimationCurve nightTimeCurve; //Havanın kararma animasyonu
    [SerializeField] Color dayLightColor = Color.white; //Gündüz beyazlığı

    float time;

    [SerializeField] float timeScale = 60f;
    [SerializeField] float startAtTime = 28800f; // saniye olarak başlama saati

    [SerializeField] Text text; //Saati göstermek için yazı
    [SerializeField] Light2D globalLight; //Unity'nin 2D aydıklandırma teknolojisi
    private int days;

    List<TimeAgent> agents;

    private void Awake()
    {
        agents = new List<TimeAgent>();
    }

    private void Start()
    {
        time = startAtTime;
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

    int oldPhase = 0;
    private void TimeAgents()
    {
        int currentPhase = (int)(time / phaseLength);
        
        if (oldPhase != currentPhase)
        {
            oldPhase = currentPhase;
            for (int i = 0; i < agents.Count; i++)
            {
                agents[i].Invoke();
            }
            
        }

       
    }

    private void NextDay()
    {
        time = 0; //Saat sıfırlanýnca sonraki güne geçilir
        days += 1;
    }
}

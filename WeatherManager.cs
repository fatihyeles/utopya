using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeatherStates
{
    Clear,
    Rain,
    HeavyRain,
    RainAndThunder
}
public class WeatherManager : TimeAgent
{
    [Range(0f, 1f)] [SerializeField] float chanceToChangeWeather = 0.02f;

    WeatherStates currentWeatherState = WeatherStates.Clear;

    [SerializeField] ParticleSystem rainObject;
    [SerializeField] ParticleSystem heavyRainObject;
    [SerializeField] ParticleSystem rainAndThunder;

    private void Start()
    {
        Init();
        onTimeTick += RandomWeatherChangeCheck;
        UpdateWeather();
    }

    public void RandomWeatherChangeCheck(DayTimeController dayTimeController)
    {
        if (UnityEngine.Random.value < chanceToChangeWeather)
        {
            RandomWeatherChange();
        }
    }

    private void RandomWeatherChange()
    {
        WeatherStates newWeatherState = (WeatherStates)UnityEngine.Random.Range(0, Enum.GetNames(typeof(WeatherStates)).Length);
        ChangeWeather(newWeatherState);
    }

    private void ChangeWeather(WeatherStates newWeatherState)
    {
        currentWeatherState = newWeatherState;
        Debug.Log(currentWeatherState);
        UpdateWeather();
    }

    private void UpdateWeather()
    {
        switch (currentWeatherState)
        {
            case WeatherStates.Clear:
                rainObject.gameObject.SetActive(false);
                heavyRainObject.gameObject.SetActive(false);
                rainAndThunder.gameObject.SetActive(false);
                break;
            case WeatherStates.Rain:
                rainObject.gameObject.SetActive(true);
                heavyRainObject.gameObject.SetActive(false);
                rainAndThunder.gameObject.SetActive(false);
                break;
            case WeatherStates.HeavyRain:
                rainObject.gameObject.SetActive(false);
                heavyRainObject.gameObject.SetActive(true);
                rainAndThunder.gameObject.SetActive(false);
                break;
            case WeatherStates.RainAndThunder:
                rainObject.gameObject.SetActive(false);
                heavyRainObject.gameObject.SetActive(false);
                rainAndThunder.gameObject.SetActive(true);
                break;
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StateManager : MonoBehaviour
{
    public WeatherData weatherData;
    public string currentWeather;
    private bool rain;
    private bool snow;
    private bool cloudy;
    private bool sunny;
    public GameObject rainObject;
    public GameObject snowObject;
    public GameObject cloudyObject;
    public GameObject sunnyObject;
	List<string> rainie = new List<string>();
	List<string> snowie = new List<string>();
	List<string> cloudie = new List<string>();
	List<string> sunnie = new List<string>();


    void Start()
    {
        string[] rainy_list = { "Rain", "Drizzle", "Thunderstorm" };
        string[] snow_list = { "Snow", "Sleet" };
        string[] cloud_list = { "Clouds", "Mist", "Smoke", "Dust", "Haze", "Fog", "Sand", "Dust", "Ash", "Sqail", "Tornado" };
        string[] sunny_list = { "Clear" };

        rainie.AddRange(rainy_list);
        snowie.AddRange(snow_list);
        cloudie.AddRange(cloud_list);
        sunnie.AddRange(sunny_list);
    }
    void Update()
    {
		try{
        	currentWeather = weatherData.Info.weather[0].main;
		} catch(Exception e) {
            currentWeather = "none";
        }
		
        Debug.Log("Weather: " + currentWeather);

        if (rainie.Contains(currentWeather))
        {
            SpawnRain();
        }
        else if (snowie.Contains(currentWeather))
        {
            SpawnSnow();
        }
        else if (cloudie.Contains(currentWeather))
        {
            SpawnCloudy();
        }
        else if (sunnie.Contains(currentWeather))
        {
            SpawnSunny();
        }
        else
        {
            None();
        }
    }

    void SpawnRain()
    {
        rain = true;
        rainObject.SetActive(true);
        if (snow)
        {
            StartCoroutine(DisableSnow());
        }
        else if (cloudy)
        {
            StartCoroutine(DisableCloudy());
        }
        else if (sunny)
        {
            StartCoroutine(DisableSunny());
        }
    }
    void SpawnSnow()
    {
        snow = true;
        snowObject.SetActive(true);
        if (rain)
        {
            StartCoroutine(DisableRain());
        }
        else if (cloudy)
        {
            StartCoroutine(DisableCloudy());
        }
        else if (sunny)
        {
            StartCoroutine(DisableSunny());
        }
    }
    void SpawnCloudy()
    {
        cloudy = true;
        cloudyObject.SetActive(true);
        if (snow)
        {
            StartCoroutine(DisableSnow());
        }
        else if (rain)
        {
            StartCoroutine(DisableRain());
        }
        else if (sunny)
        {
            StartCoroutine(DisableSunny());
        }
    }
    void SpawnSunny()
    {
        sunny = true;
        sunnyObject.SetActive(true);
        if (snow)
        {
            StartCoroutine(DisableSnow());
        }
        else if (cloudy)
        {
            StartCoroutine(DisableCloudy());
        }
        else if (rain)
        {
            StartCoroutine(DisableRain());
        }
    }
    void None()
    {
        if (rain)
        {
            StartCoroutine(DisableRain());
        }
        else if (cloudy)
        {
            StartCoroutine(DisableCloudy());
        }
        else if (sunny)
        {
            StartCoroutine(DisableSunny());
        }
        else if (snow)
        {
            StartCoroutine(DisableSnow());
        }
    }

    IEnumerator DisableRain()
    {
        rain = false;
        rainObject.GetComponent<ParticleSystem>().Stop();
        rainObject.GetComponent<Animator>().Play("rain_exit");
        yield return new WaitForSeconds(5);
        rainObject.SetActive(false);
    }

    IEnumerator DisableSnow()
    {
        snow = false;
        snowObject.GetComponent<ParticleSystem>().Stop();
        snowObject.GetComponent<Animator>().Play("snow_exit");
        yield return new WaitForSeconds(5);
        snowObject.SetActive(false);
    }

    IEnumerator DisableCloudy()
    {
        cloudy = false;
        cloudyObject.GetComponent<Animator>().Play("cloudy_exit");
        yield return new WaitForSeconds(5);
        cloudyObject.SetActive(false);
    }

    IEnumerator DisableSunny()
    {
        sunny = false;
        sunnyObject.GetComponent<Animator>().Play("sunny_exit");
        yield return new WaitForSeconds(5);
        sunnyObject.SetActive(false);
    }
}

using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class WeatherData : MonoBehaviour {
	private float timer;
	public float minutesBetweenUpdate;
	// public WeatherInfo Info;
	public OpenWeather Info;
	public string API_key;
	private float latitude;
	private float longitude;
	private bool locationInitialized;
	public Text currentWeatherText;
	public GetLocation getLocation;

	public void Begin() {
		latitude = getLocation.latitude;
		longitude = getLocation.longitude;
		locationInitialized = true;
	}

	void Update() {
		if (locationInitialized) {
			if (timer <= 0) {
				StartCoroutine (GetWeatherInfo ());
				timer = minutesBetweenUpdate * 60;
			} else {
				timer -= Time.deltaTime;
			}
		}
	}

	private IEnumerator GetWeatherInfo(){
		var www = new UnityWebRequest("https://api.openweathermap.org/data/2.5/weather?id=1791121&appid=a5243c4f8aac51d476f00e8c7ae2c5d1"){
			downloadHandler = new DownloadHandlerBuffer()
		};

		yield return www.SendWebRequest();

		if (www.isNetworkError || www.isHttpError){
			Debug.Log("error");
			//error 
			yield break;
		}

		Info = JsonUtility.FromJson<OpenWeather>(www.downloadHandler.text);
		// currentWeatherText.text = "Current weather: " + Info.currently.summary;
	}
}

[Serializable]
public class WeatherInfo{
	public float latitude;
	public float longitude;
	public string timezone;
	public Currently currently;
	public int offset;
}

[Serializable]
public class Currently{
	public int time;
	public string summary;
	public string icon;
	public int nearestStormDistance;
	public int nearestStormBearing;
	public int precipIntensity;
	public int precipProbability;
	public double temperature;
	public double apparentTemperature;
	public double dewPoint;
	public double humidity;
	public double pressure;
	public double windSpeed;
	public double windGust;
	public int windBearing;
	public int cloudCover;
	public int uvIndex;
	public double visibility;
	public double ozone;
}

[Serializable]
public class OpenWeather{
	public Coord coord;
	public List<Weather> weather = new List<Weather>();
	// public string base;
	public Wind wind;
	public Main main;
	public Clouds clouds;
	public Sys sys;	
	public int timezone;
	public int id;
	public string name;
	public int cod;
	public int visibility;
	public int dt;

	public string weatherType(){
        Debug.Log(weather.Count);
        return weather.ElementAt(0).main;
    }
}

[Serializable]
public class Coord{
	public float lon;
	public float lat;
}

[Serializable]
public class Weather{
    public int id;
    public string main;
    public string description;
    public string icon;
	
}

[Serializable]
public class Main{
	public float temp;
    public float feels_like;
	public float temp_min;
	public float temp_max;
	public float pressure;
	public float humidity;
	
}

[Serializable]
public class Wind{
	public float speed;
	public float deg;
}

[Serializable]
public class Clouds{
	public int all;
}

[Serializable]
public class Sys{
	public int type;
	public int id;
	public string country;
	public int sunrise;
	public int sunset;	
}


using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TestApi : MonoBehaviour
{

    public void ButtonOnClick()
    {
        ApiController.GetInstance()
            .SetServerUrl(Servers.kAPI_COUNTRY_SERVER, EndPoints.kGET_ALL_COUNTRIES)
            .SetHttpMethod(HTTPMethods.kGET)
            .SendData<string>("fields=name", HandleRequest);
    }

    private void HandleRequest(UnityWebRequest webRequest)
    {
        if(webRequest.responseCode == 200)
        {
            var countries = JsonConvert.DeserializeObject<ResponseCountries.Root[]>(webRequest.downloadHandler.text);
            foreach(var country in countries)
            {
                Debug.Log(country.name);
            }
        }
    }
}

[System.Serializable]
public class ResponseCountries
{
    [System.Serializable]
    public class Root
    {
        public string name { get; set; }
    }
}

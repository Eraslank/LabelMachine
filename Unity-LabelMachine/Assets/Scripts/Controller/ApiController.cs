using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
using Newtonsoft.Json;
using System;

public class ApiController : MonoBehaviour
{

    [SerializeField] private string server=null;
    private string httpMethod=null;
    private UnityWebRequest webRequest;

    private HashSet<string> allowedMethods;

    private Action<UnityWebRequest> OnFinish;

    private ApiController()
    {
        allowedMethods = new HashSet<string>();
        allowedMethods.Add("GET");
        allowedMethods.Add("POST");
    }

    public static ApiController GetInstance()
    {
        return new GameObject("ApiController").AddComponent<ApiController>();
    }

    public void Error(string error)
    {
        Debug.Log(error);
        Dispose();
    }

    public ApiController SetServerUrl(string url,string endPoint)
    {
        server=url+endPoint;
        return this;
    }

    public ApiController SetHttpMethod(string method)
    {
        httpMethod = method.ToUpper();
        return this;
    }

    public void SendData<T>(T dataToSend, Action<UnityWebRequest> callback = null)
    {
        if(string.IsNullOrWhiteSpace(server))
        {
            Error("Use SetServerUrl() Before Sending Data!");
            return;
        }
        if(string.IsNullOrWhiteSpace(httpMethod))
        {
            Error("Use SetHttpMethod() Before Sending Data!");
            return;
        }
        else if(!allowedMethods.Contains(httpMethod))
        {
            Error($"The Method : {httpMethod} Is Not Recognized!");
            return;
        }
        OnFinish = callback;
        if(dataToSend == null)
            StartCoroutine("PostData","");
        else
            StartCoroutine("PostData",JsonConvert.SerializeObject(dataToSend));
    }
    private IEnumerator PostData(string jsonData)
    {
        UnityWebRequest request = new UnityWebRequest();
        request.method = httpMethod;
		request.SetRequestHeader("Content-Type", "application/json");

        if(!string.IsNullOrWhiteSpace(PlayerPrefs.GetString("UserToken")))
            request.SetRequestHeader("Authorization", "Bearer " + PlayerPrefs.GetString("UserToken"));
            
        request.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();
        UriBuilder uriBuilder;
        jsonData=jsonData.Trim('"');
        if(httpMethod.Equals("GET"))
        {
            if(!string.IsNullOrWhiteSpace(jsonData))
                uriBuilder = new UriBuilder(server+"?"+jsonData);
            else
                uriBuilder = new UriBuilder(server);
        }
        else
        {
            uriBuilder = new UriBuilder(server);
            if(!string.IsNullOrWhiteSpace(jsonData))
		        request.uploadHandler = (UploadHandler) new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(jsonData));
        }
        request.uri = uriBuilder.Uri;

        yield return request.SendWebRequest();
        webRequest = request;
        Finish();
    }
    private void Finish()
    {
        OnFinish?.Invoke(webRequest);
        Dispose();
    }
    private void Dispose()
    {
        Destroy(gameObject,.5f);
    }
}

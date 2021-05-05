using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using System;

public class SendPictureToServer : MonoBehaviour
{
    [SerializeField] TMP_InputField labelField;

    public void SendDataToServer()
    {
        List<PostImage.Label> labels = new List<PostImage.Label>();
        foreach(var text in labelField.text.Split(' '))
        {
            labels.Add(new PostImage.Label() { labelName = text});
        }

        PostImage.Root dataToSend = new PostImage.Root();
        var fileName = GameManager.Instance.latestScreenShotPath.Replace('\\','/').Split('/');
        dataToSend.fileName = fileName[fileName.Length - 1];
        dataToSend.labels = labels;
        dataToSend.base64Image = GameManager.Instance.GetBase64LatestPicture();

        ApiController.GetInstance()
            .SetHttpMethod(HTTPMethods.kPOST)
            .SetServerUrl(Servers.kAPI_SERVER, EndPoints.kPOST_IMAGE)
            .SendData<PostImage.Root>(dataToSend, HandleRequest);
    }

    private void HandleRequest(UnityWebRequest webRequest)
    {
        Debug.Log(webRequest.responseCode);
        GameManager.Instance.ContinueCamStream();
    }
}

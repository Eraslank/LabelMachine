using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WebcamController : MonoBehaviour
{
    [SerializeField] private RawImage rawImage;
    [SerializeField] private int webCamIndex = 0;

    private WebCamTexture webCamTexture;

    private void Start()
    {
        PlayWebCamera();
    }

    private void PlayWebCamera()
    {
        WebCamDevice[] devices = WebCamTexture.devices;
        if (devices.Length <= webCamIndex)
        {
            webCamIndex = 0;
        }
        webCamTexture = new WebCamTexture(devices[webCamIndex].name, Screen.width / 2, Screen.height / 2);

        if (rawImage)
            rawImage.texture = webCamTexture;

        webCamTexture.Play();

        float aspect = (float)webCamTexture.width / (float)webCamTexture.height;
        rawImage.GetComponent<AspectRatioFitter>().aspectRatio = aspect;
    }
}

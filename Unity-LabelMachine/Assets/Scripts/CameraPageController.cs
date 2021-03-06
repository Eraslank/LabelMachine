using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraPageController : Page, IConfigurablePage
{
    [SerializeField] private RawImage rawImage;
    [SerializeField] private int webCamIndex = 0;

    private WebCamTexture webCamTexture;
    public void PauseRecording()
    {
        webCamTexture.Pause();
    }
    public void StartRecording()
    {
        webCamTexture.Play();
    }
    public void ConfigurePage()
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

        float aspect = (float)webCamTexture.height / (float)webCamTexture.width;
        rawImage.GetComponent<AspectRatioFitter>().aspectRatio = aspect;
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(1))     //DEVELOPER TOOLS
        {
            PageManager.Instance.ChangePage(EPageName.Launch);
        }
    }

    private void OnDisable()
    {
        webCamTexture?.Stop();
    }
}

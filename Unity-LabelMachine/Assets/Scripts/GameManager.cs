using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviourSingleton<GameManager>
{
    public string latestScreenShotPath;
    public Camera mainCamera;

    [SerializeField] ScreenRecorder sR;
    [SerializeField] CameraPageController wC;

    [SerializeField] GameObject takePictureButton;
    [SerializeField] GameObject labelPanel;

    public void TakePicture()
    {
        takePictureButton.SetActive(false);
        sR.TakeScreenShot();
        wC.PauseRecording();
        labelPanel.SetActive(true);
    }
    public void ContinueCamStream()
    {
        labelPanel.SetActive(false);
        takePictureButton.SetActive(true);
        wC.StartRecording();
    }
    public string GetBase64LatestPicture() =>
        Convert.ToBase64String(File.ReadAllBytes(latestScreenShotPath));
}


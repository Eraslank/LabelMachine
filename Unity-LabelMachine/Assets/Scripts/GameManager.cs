using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviourSingleton<GameManager>
{
    public string latestScreenShotPath;
    public Camera mainCamera;

    [SerializeField] ScreenRecorder sR;
    [SerializeField] CameraPageController wC;

    [SerializeField] GameObject takePictureButton;

    public void TakePicture()
    {
        sR.TakeScreenShot();
        wC.PauseRecording();
        takePictureButton.SetActive(false);
    }
}

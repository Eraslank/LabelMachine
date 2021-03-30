using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Image))]
public class ColorChangerController : MonoBehaviour
{
    [SerializeField] private bool isForeGround = true;
    Image img;
    void Start()
    {
        img = GetComponent<Image>();
        img.color = (isForeGround) ? LabelMachineColors.ForegroundColor : LabelMachineColors.BackgroundColor;
    }
}

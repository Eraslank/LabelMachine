using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public struct Servers
{
    public const string kAPI_SERVER = "https://localhost:44372/api";
}
public struct EndPoints
{
    public const string kPOST_IMAGE = "/Image";
}
public struct HTTPMethods
{
    public const string kGET = "GET";
    public const string kPOST = "POST";
}
public struct LabelMachineColors
{
    public static Color32 BackgroundColor { get { return new Color32(67, 67, 67, 255); } }//#434343
    public static Color32 ForegroundColor { get { return new Color32(212, 158, 39, 255); } }//#D49E27
}

public enum EPageName : int
{
    Launch = 0,
    CameraDisplay,
    Count,
    None
}
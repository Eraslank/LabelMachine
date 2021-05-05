using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public struct Servers
{
    public const string kAPI_SERVER = "";
    public const string kAPI_COUNTRY_SERVER = "https://restcountries.eu/rest/v2";
}
public struct EndPoints
{
    public const string kLOGIN_ENDPOINT = "";
    public const string kGET_ALL_COUNTRIES = "/all";
    //https://restcountries.eu/rest/v2/all?fields=name
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
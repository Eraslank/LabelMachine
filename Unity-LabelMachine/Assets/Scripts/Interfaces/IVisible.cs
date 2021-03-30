using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IVisible
{
    bool IsVisible { get; set; }
    void SetVisible(bool state);
}

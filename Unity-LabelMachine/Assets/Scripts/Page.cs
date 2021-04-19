using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Page : MonoBehaviour, IVisible
{
	public EPageName pageName;

    public bool IsVisible { get; set; }

    public void SetVisible(bool state)
	{
		gameObject.SetActive(IsVisible = state);
	}
}
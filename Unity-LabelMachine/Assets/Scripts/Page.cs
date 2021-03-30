using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Page : MonoBehaviour 
{
	public EPageName pageName;
	public void isVisible(bool state)
	{
		gameObject.SetActive(state);
	}
}

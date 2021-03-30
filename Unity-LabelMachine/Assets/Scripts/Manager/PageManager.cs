using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using System.Threading;
using UnityEngine.UI;

public class PageManager : MonoBehaviourSingleton<PageManager>
{
    public List<Page> pages;
    Page currentPage;

    // Use this for initialization
    void Start()
    {
        Application.targetFrameRate = 60;
        SetPage(GetPage(EPageName.Launch), false);
        SetPage(GetPage(EPageName.None), true);
    }

    public void OpenUrl(string url)
    {
        Application.OpenURL(url);
    }

    public void ChangePage(Page page)
    {
        if (currentPage != null && currentPage == page)
            return;
        if (currentPage != null)
            SetPage(currentPage, false);
        currentPage = page;
        SetPage(currentPage, true);
    }
    public void ChangePage(EPageName pageName, bool forcePageChange = false)
    {
        if (currentPage != null && currentPage.pageName == pageName && !forcePageChange)
            return;
        Page page = GetPage(pageName);
        if (currentPage != null)
            SetPage(currentPage, false);
        currentPage = page;
        SetPage(currentPage, true);
    }
    void SetPage(Page page, bool showPage)
    {
        if (showPage)
        {
            IConfigurablePage iCP;
            if ((iCP = page.GetComponent<IConfigurablePage>()) != null)
                iCP.ConfigurePage();
        }
        page.SetVisible(showPage);
    }
    public Page GetPage(EPageName page)
    {
        return pages.Select(p => p).Where(p => p.pageName == page).First();
    }
}

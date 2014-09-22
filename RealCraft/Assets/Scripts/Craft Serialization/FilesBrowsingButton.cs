using UnityEngine;
using System.Collections;

public class FilesBrowsingButton : AnimatedGUITextureButton
{

    public SavedFilesBrowser fileBrowser;
    public bool reverseBrowsing;
    public FilesBrowsingButton otherBrowsingButton;

    void Start()
    {
        if ((reverseBrowsing && !fileBrowser.PreviousPageAvailable) || (!reverseBrowsing && !fileBrowser.NextPageAvailable))
            EnableButton(false);

    }

    protected override void OnButtonUp()
    {
        if (reverseBrowsing)
        {
            fileBrowser.MoveToPreviousPage();
            if (!fileBrowser.PreviousPageAvailable)
                EnableButton(false);
        }
        else
        {
            fileBrowser.MoveToNextPage();
            if (!fileBrowser.NextPageAvailable)
                EnableButton(false);
        }
        otherBrowsingButton.EnableButton(true);
    }

}

using UnityEngine;
using System.Collections;

public class TextureBrowsingButtonScript : AnimatedGUITextureButton {

    public TextureBrowser textureBrowser;
    public bool reverseBrowsing;
    public TextureBrowsingButtonScript otherBrowsingButton;

    void Start()
    {
        if ((reverseBrowsing && !textureBrowser.PreviousPageAvailable) || (!reverseBrowsing && !textureBrowser.NextPageAvailable))
            EnableButton(false);

    }

    protected override void OnButtonUp()
    {
        if (reverseBrowsing)
        {
            textureBrowser.MoveToPreviousPage();
            if (!textureBrowser.PreviousPageAvailable)
                EnableButton(false);
        }
        else
        {
            textureBrowser.MoveToNextPage();
            if (!textureBrowser.NextPageAvailable)
                EnableButton(false);
        }
        otherBrowsingButton.EnableButton(true);
    }

}

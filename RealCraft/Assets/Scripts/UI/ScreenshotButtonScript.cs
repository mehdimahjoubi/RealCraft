using UnityEngine;
using System.Collections;
using System.IO;
using System;

public class ScreenshotButtonScript : AnimatedGUITextureButton {

    public GuiContainerScript guiContainer;
    public NotificationScript notification;
    public float notificationStartTimer = 2;

    protected override void OnButtonUp()
    {
        guiContainer.DisableGUI(1.5f);
        string screenshotsFolderRelativePath = "Screenshots";
        string screenshotsDirectory = Application.persistentDataPath + "/" + screenshotsFolderRelativePath;
        bool screenFolderExists = Directory.Exists(screenshotsDirectory);
        if (!screenFolderExists)
            Directory.CreateDirectory(screenshotsDirectory);
        string fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + ".png";
        var fileRelativePath = screenshotsFolderRelativePath + "/" + fileName;
        Application.CaptureScreenshot(fileRelativePath);
        notification.ShowDelayedNotification("\n\n\nSaved screenshot to : \n " + screenshotsDirectory + "/" + fileName, notificationStartTimer, 0.5f);
        transform.localScale = initialScale;
        guiTexture.color = currentButtonColor;
    }

}

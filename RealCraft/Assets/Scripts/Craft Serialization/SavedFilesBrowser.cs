using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class SavedFilesBrowser : MonoBehaviour
{

    public GameObject browserGuiRoot;
    public FilesBrowsingButton nextButton;
    public GameObject loadButton;
    public GUIText[] filesDisplay;
    public bool NextPageAvailable { get; private set; }
    public bool PreviousPageAvailable { get; private set; }
    string[] filePaths;
    string[] fileNames;
    int lastDisplayedFile = 0;
    string savedFilesRelativePath = "/SavedCrafts";
    public static string SavedFilesPath { get; private set; }
    public static string FileExtension { get; private set; }

    bool LoadFileNames()
    {
        SavedFilesPath = Application.persistentDataPath + savedFilesRelativePath;
        if (!Directory.Exists(SavedFilesPath))
        {
            Directory.CreateDirectory(SavedFilesPath);
            return false;
        }
        filePaths = Directory.GetFiles(SavedFilesPath, "*." + FileExtension);
        fileNames = new string[filePaths.Length];
        for (int i = 0; i < filePaths.Length; i++)
        {
            fileNames[i] = Path.GetFileNameWithoutExtension(filePaths[i]);
        }
        return true;
    }

    public void ReInitializeFilesList()
    {
        FileExtension = "rcdata";
        if (LoadFileNames())
        {
            for (int i = 0; i < filesDisplay.Length; i++)
            {
                if (!filesDisplay[i].gameObject.activeSelf)
                {
                    filesDisplay[i].gameObject.SetActive(true);
                    filesDisplay[i].transform.parent.gameObject.SetActive(true);
                }
            }
            if (fileNames.Length <= filesDisplay.Length)
            {
                for (int i = fileNames.Length; i < filesDisplay.Length; i++)
                {
                    filesDisplay[i].gameObject.SetActive(false);
                    filesDisplay[i].transform.parent.gameObject.SetActive(false);
                }
                for (int i = 0; i < fileNames.Length; i++)
                {
                    filesDisplay[i].text = fileNames[i];
                }
                lastDisplayedFile = fileNames.Length - 1;
                PreviousPageAvailable = false;
                NextPageAvailable = false;
            }
            else
            {
                int i = 0;
                while (i < filesDisplay.Length)
                {
                    filesDisplay[i].text = fileNames[i];
                    i++;
                }
                lastDisplayedFile = i - 1;
                PreviousPageAvailable = false;
                NextPageAvailable = true;
            }
            if (fileNames.Length == 0)
                loadButton.SetActive(false);
        }
        else
            loadButton.SetActive(false);
        if (NextPageAvailable && !nextButton.ButtonEnabled)
            nextButton.EnableButton(true);
    }

    void Awake()
    {
        browserGuiRoot.SetActive(true); // to allow next button correct initialization
        browserGuiRoot.SetActive(false);
        ReInitializeFilesList();
    }

    public void MoveToNextPage()
    {
        if (NextPageAvailable)
        {
            PreviousPageAvailable = true;
            int remainingTexturesCount = fileNames.Length - lastDisplayedFile - 1;
            if (remainingTexturesCount < filesDisplay.Length)
            {
                for (int i = remainingTexturesCount; i < filesDisplay.Length; i++)
                {
                    filesDisplay[i].gameObject.SetActive(false);
                    filesDisplay[i].transform.parent.gameObject.SetActive(false);
                }
                for (int i = 0; i < remainingTexturesCount; i++)
                {
                    filesDisplay[i].text = fileNames[lastDisplayedFile + i + 1];
                }
                lastDisplayedFile = fileNames.Length - 1;
                NextPageAvailable = false;
            }
            else
            {
                int i = 0;
                while (i < filesDisplay.Length)
                {
                    filesDisplay[i].text = fileNames[lastDisplayedFile + i + 1];
                    i++;
                }
                lastDisplayedFile = lastDisplayedFile + i;
            }
        }
    }

    public void MoveToPreviousPage()
    {
        if (PreviousPageAvailable)
        {
            NextPageAvailable = true;
            int activeDisplays = 0;
            while (activeDisplays < filesDisplay.Length && filesDisplay[activeDisplays].gameObject.activeSelf)
                activeDisplays++;
            int previousPageLastTextureIndex = lastDisplayedFile - activeDisplays;
            for (int i = 0; i < filesDisplay.Length; i++)
            {
                if (!filesDisplay[i].gameObject.activeSelf)
                {
                    filesDisplay[i].transform.parent.gameObject.SetActive(true);
                    filesDisplay[i].gameObject.SetActive(true);
                }
                filesDisplay[i].text = fileNames[previousPageLastTextureIndex - filesDisplay.Length + 1 + i];
            }
            lastDisplayedFile = previousPageLastTextureIndex;
            if (lastDisplayedFile == filesDisplay.Length - 1)
                PreviousPageAvailable = false;
        }
    }

}

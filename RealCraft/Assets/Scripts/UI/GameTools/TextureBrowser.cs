using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class TextureBrowser : MonoBehaviour
{

    public Texture[] defaultTextures;
    public GUITexture[] texturesDisplay;
    public Transform textureButton;
    public bool NextPageAvailable { get; private set; }
    public bool PreviousPageAvailable { get; private set; }
    List<Texture> allTextures = new List<Texture>();
    int lastDisplayedTexture = 0;
    static TextureBrowser firstInstance;

    void LoadExternalTextures()
    {
        //TODO: Load external textures from a specific directory...
    }

    public static TextureBrowser GetFirstInstance()
    {
        return firstInstance;
    }

    void Awake()
    {
        if (firstInstance == null)
            firstInstance = this;
        for (int i = 0; i < defaultTextures.Length; i++)
        {
            allTextures.Add(defaultTextures[i]);
        }
        LoadExternalTextures();
        if (allTextures.Count < texturesDisplay.Length)
        {
            for (int i = allTextures.Count; i < texturesDisplay.Length; i++)
            {
                texturesDisplay[i].gameObject.SetActive(false);
            }
            for (int i = 0; i < allTextures.Count; i++)
            {
                texturesDisplay[i].texture = allTextures.ElementAt(i);
            }
            lastDisplayedTexture = allTextures.Count - 1;
            PreviousPageAvailable = false;
            NextPageAvailable = false;
        }
        else
        {
            int i = 0;
            while (i < texturesDisplay.Length)
            {
                texturesDisplay[i].texture = allTextures.ElementAt(i);
                i++;
            }
            lastDisplayedTexture = i - 1;
            PreviousPageAvailable = false;
            NextPageAvailable = true;
        }
        transform.parent = textureButton;
        gameObject.SetActive(false);
    }

    public void MoveToNextPage()
    {
        if (NextPageAvailable)
        {
            PreviousPageAvailable = true;
            int remainingTexturesCount = allTextures.Count - lastDisplayedTexture - 1;
            if (remainingTexturesCount < texturesDisplay.Length)
            {
                for (int i = remainingTexturesCount; i < texturesDisplay.Length; i++)
                {
                    texturesDisplay[i].gameObject.SetActive(false);
                }
                for (int i = 0; i < remainingTexturesCount; i++)
                {
                    texturesDisplay[i].texture = allTextures.ElementAt(lastDisplayedTexture + i + 1);
                }
                lastDisplayedTexture = allTextures.Count - 1;
                NextPageAvailable = false;
            }
            else
            {
                int i = 0;
                while (i < texturesDisplay.Length)
                {
                    texturesDisplay[i].texture = allTextures.ElementAt(lastDisplayedTexture + i + 1);
                    i++;
                }
                lastDisplayedTexture = lastDisplayedTexture + i;
            }
        }
    }

    public void MoveToPreviousPage()
    {
        if (PreviousPageAvailable)
        {
            NextPageAvailable = true;
            int activeDisplays = 0;
            while (activeDisplays < texturesDisplay.Length && texturesDisplay[activeDisplays].gameObject.activeSelf)
                activeDisplays++;
            int previousPageLastTextureIndex = lastDisplayedTexture - activeDisplays;
            for (int i = 0; i < texturesDisplay.Length; i++)
            {
                if (!texturesDisplay[i].gameObject.activeSelf)
                    texturesDisplay[i].gameObject.SetActive(true);
                texturesDisplay[i].texture = allTextures.ElementAt(previousPageLastTextureIndex - texturesDisplay.Length + 1 + i);
            }
            lastDisplayedTexture = previousPageLastTextureIndex;
            if (lastDisplayedTexture == texturesDisplay.Length - 1)
                PreviousPageAvailable = false;
        }
    }

    public int GetTextureIndex(Texture tex)
    {
        if (tex == null || !allTextures.Contains(tex))
            return 0;
        return allTextures.FindIndex(a => a == tex);
    }

    public Texture GetTextureByIndex(int index)
    {
        return allTextures.ElementAt(index);
    }

    public int GetTexturesCount()
    {
        return allTextures.Count;
    }

}

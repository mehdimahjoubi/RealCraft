using UnityEngine;
using System.Collections;

public class TextureSelectionButtonScript : AnimatedGUITextureButton {

    public CubeTextureButtonScript textureToolButton;
    public TextureBrowser textureBrowser;
    public static Texture SelectedTexture { get; private set; }

    protected override void OnButtonUp()
    {
        if (textureBrowser.GetTextureIndex(guiTexture.texture) == textureBrowser.GetTexturesCount() - 1)
        {
            //TODO: more textures button...
            return;
        }
        if (textureBrowser.GetTextureIndex(guiTexture.texture) == 0)
            SelectedTexture = null;
        else
            SelectedTexture = guiTexture.texture;
        ResetButtonAnims();
        textureToolButton.CloseTexturesGui();
    }

}

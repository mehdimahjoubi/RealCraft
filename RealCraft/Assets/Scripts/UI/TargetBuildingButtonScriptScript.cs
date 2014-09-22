using UnityEngine;
using System.Collections;

public class TargetBuildingButtonScriptScript : AnimatedGUITextureButton
{

    public UserDefinedTargetEventHandler_preview targetBuilder;
    public NotificationScript notification;
    public GameObject menuButton;
    public GameObject toolsButton;
    public GameObject defaultQuitButton;
    public GameObject initialCubePrefab;

    protected override void OnButtonUp()
    {
        if (targetBuilder.CurrentFrameQuality == ImageTargetBuilder.FrameQuality.FRAME_QUALITY_HIGH)
        {
            StartBuildingTarget();
        }
        else if (targetBuilder.CurrentFrameQuality == ImageTargetBuilder.FrameQuality.FRAME_QUALITY_MEDIUM)
        {
            notification.ShowNotification("The visible object in the scene is medium quality. \nDo you wish to use it as reference anyway? \nWe recommend the use of an object \nwith higher contrast as reference \n(such as a detailed magazine cover).", true);
        }
        else
        {
            notification.ShowNotification("The visible object in the scene is low quality. \nPlease make sure your camera's field of view contains \na highly contrasted object \n(such as a detailed magazine cover).", false);
        }
    }

    public void StartBuildingTarget()
    {
        StartCoroutine(TargetBuildingCoroutine());
    }

    IEnumerator TargetBuildingCoroutine()
    {
        //if (ModelParamsScript.ModelLocation == "external")
        //{
        //    Mesh importedMesh = new ObjImporter().ImportFile(ModelParamsScript.ModelPath);
        //    //Mesh importedMesh = new ObjImporter().ImportFile("/storage/emulated/0/chair.obj");
        //    WWW textureLoader = new WWW(ModelParamsScript.ModelTexturePath);
        //    //WWW textureLoader = new WWW("file:///storage/emulated/0/chairTex.jpg");
        //    yield return textureLoader;
        //    UserDefinedTargetEventHandler_preview.model = (Instantiate(Resources.Load("EmptyMeshRenderer")) as GameObject).transform;
        //    UserDefinedTargetEventHandler_preview.model.gameObject.GetComponent<MeshFilter>().mesh = importedMesh;
        //    UserDefinedTargetEventHandler_preview.model.renderer.material.mainTexture = textureLoader.texture;
        //}
        //else if (ModelParamsScript.ModelLocation == "internal")
        //{
        //    UserDefinedTargetEventHandler_preview.model = (Instantiate(Resources.Load(ModelParamsScript.ModelPath)) as GameObject).transform;
        //}
        //else
        //{
        //    //notification.ShowNotification("Unable to load the 3D model. \nThe model might not be in its correct location", false);

        //}
        if (initialCubePrefab != null)
            UserDefinedTargetEventHandler_preview.model = (Instantiate(initialCubePrefab) as GameObject).transform;
        else
            UserDefinedTargetEventHandler_preview.model = (Instantiate(Resources.Load("initial cube")) as GameObject).transform;
        yield return new WaitForSeconds(0.5f);
        targetBuilder.BuildTarget();
        gameObject.SetActive(false);
        menuButton.SetActive(true);
        toolsButton.SetActive(true);
        defaultQuitButton.SetActive(false);
        //CameraDevice.Instance.SetFocusMode(CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO);
    }
}

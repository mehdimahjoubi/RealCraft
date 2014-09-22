using UnityEngine;
using System.Collections;

public class InteractionScript : MonoBehaviour
{
    public Material sourceMat;
    public GameObject cubePrefab;
    int cubesCount = 1;

    void Update()
    {
        RaycastHit hit;
        if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began &&
            Physics.Raycast(Camera.main.ScreenPointToRay(Input.GetTouch(0).position), out hit))
            || (Input.GetMouseButtonDown(0) && Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit)))
        {
            if (hit.collider.gameObject.tag == "Cube")
            {
                if (ToolButtonScript.SelectedTool == ToolButtonScript.Tool.ADD_CUBE)
                {
                    var newCubeRot = hit.collider.transform.rotation;
                    GameObject newCube;
                    if (cubePrefab != null)
                        newCube = Instantiate(cubePrefab, hit.collider.transform.position, newCubeRot) as GameObject;
                    else
                        newCube = Instantiate(Resources.Load("Cube"), hit.collider.transform.position, newCubeRot) as GameObject;
                    newCube.renderer.material = new Material(sourceMat);
                    newCube.transform.parent = hit.collider.transform;
                    newCube.transform.localScale = new Vector3(1, 1, 1);
                    var deltaPos = hit.collider.transform.localScale.x * hit.collider.transform.parent.localScale.x;
                    var superParent = hit.collider.transform.parent.parent;
                    while (superParent != null)
                    {
                        deltaPos *= superParent.localScale.x;
                        superParent = superParent.parent;
                    }
                    newCube.transform.position = hit.collider.transform.position + hit.normal * deltaPos;
                    cubesCount++;
                    newCube.transform.parent = UserDefinedTargetEventHandler_preview.model;
                    if(ColorPresetsManager.SelectedColorHasBeenInitialized)
                        newCube.renderer.material.color = ColorPresetsManager.SelectedPresetColor;
                    if (TextureSelectionButtonScript.SelectedTexture != null)
                        newCube.renderer.material.mainTexture = TextureSelectionButtonScript.SelectedTexture;
                }
                else if (cubesCount > 1 && ToolButtonScript.SelectedTool == ToolButtonScript.Tool.DELETE_CUBE)
                {
                    if (hit.collider.gameObject != UserDefinedTargetEventHandler_preview.model.gameObject)
                    {
                        Destroy(hit.collider.gameObject);
                        cubesCount--;
                    }
                }
                else if (ToolButtonScript.SelectedTool == ToolButtonScript.Tool.APPLY_COLOR)
                {
                    hit.collider.renderer.material.color = ColorPresetsManager.SelectedPresetColor;
                }
                else if (ToolButtonScript.SelectedTool == ToolButtonScript.Tool.APPLY_TEXTURE)
                {
                    hit.collider.renderer.material.mainTexture = TextureSelectionButtonScript.SelectedTexture;
                }
            }
        }
    }

}

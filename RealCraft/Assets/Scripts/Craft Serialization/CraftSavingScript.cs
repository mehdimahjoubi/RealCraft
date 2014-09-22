using UnityEngine;
using System.Collections;

public class CraftSavingScript : MonoBehaviour
{

    public Material sourceMaterial;
    public ParticleSystem spawnEffect;
    public string childCubePrefabName;
    NotificationScript notification;
    public string FilePath { get; private set; }

    void Awake()
    {
        notification = GameObject.Find("Notification").GetComponent<NotificationScript>();
    }

    public void SaveCraft(string newFilePath = null)
    {
        var textureManager = TextureBrowser.GetFirstInstance();
        if (newFilePath != null)
            FilePath = newFilePath;
        var craft = new CraftState()
        {
            CraftScale = transform.localScale.x,
            RootPositionX = transform.position.x,
            RootPositionY = transform.position.y,
            RootPositionZ = transform.position.z,
            RootColor = ColorHexConverter.RGB_To_Hex(renderer.material.color),
            RootTexture = textureManager.GetTextureIndex(renderer.material.mainTexture),
            ChildrenLocalPosX = new float[transform.childCount],
            ChildrenLocalPosY = new float[transform.childCount],
            ChildrenLocalPosZ = new float[transform.childCount],
            ChildrenColors = new string[transform.childCount],
            ChildrensTextures = new int[transform.childCount]
        };
        for (int i = 0; i < transform.childCount; i++)
        {
            var child = transform.GetChild(i);
            craft.ChildrenLocalPosX[i] = child.localPosition.x;
            craft.ChildrenLocalPosY[i] = child.localPosition.y;
            craft.ChildrenLocalPosZ[i] = child.localPosition.z;
            craft.ChildrenColors[i] = ColorHexConverter.RGB_To_Hex(child.renderer.material.color);
            craft.ChildrensTextures[i] = textureManager.GetTextureIndex(child.renderer.material.mainTexture);
        }
        CraftState.SerializeState(craft, FilePath);
    }

    public bool LoadCraft(string newFilePath)
    {
        var textureManager = TextureBrowser.GetFirstInstance();
        if (newFilePath != FilePath)
            FilePath = newFilePath;
        Debug.Log(FilePath);
        CraftState craft = null;
        try
        {
            craft = CraftState.DeserializeState(FilePath);
        }
        catch
        {
            notification.ShowNotification("Unable to load the geometry. \nThe file might be corrupted.");
            return false;
        }
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
        transform.localScale = new Vector3(craft.CraftScale, craft.CraftScale, craft.CraftScale);
        renderer.material.color = ColorHexConverter.HexToRGB(craft.RootColor);
        if (craft.RootTexture == 0)
            renderer.material.mainTexture = null;
        else
            renderer.material.mainTexture = textureManager.GetTextureByIndex(craft.RootTexture);
        AddSpawnEffect(gameObject);
        for (int i = 0; i < craft.ChildrenColors.Length; i++)
        {
            var child = Instantiate(Resources.Load(childCubePrefabName), transform.position, transform.rotation) as GameObject;
            child.renderer.material = new Material(sourceMaterial);
            child.transform.parent = transform;
            child.transform.localScale = new Vector3(1, 1, 1);
            child.transform.localPosition = new Vector3(craft.ChildrenLocalPosX[i], craft.ChildrenLocalPosY[i], craft.ChildrenLocalPosZ[i]);
            child.renderer.material.color = ColorHexConverter.HexToRGB(craft.ChildrenColors[i]);
            if (craft.ChildrensTextures[i] == 0)
                child.renderer.material.mainTexture = null;
            else
                child.renderer.material.mainTexture = textureManager.GetTextureByIndex(craft.ChildrensTextures[i]);
            AddSpawnEffect(child);
        }
        return true;
    }

    private void AddSpawnEffect(GameObject spawnedCube)
    {
        if (spawnEffect != null)
        {
            spawnedCube.renderer.enabled = false;
            var effect = Instantiate(spawnEffect, spawnedCube.transform.position, spawnedCube.transform.rotation) as ParticleSystem;
            effect.transform.parent = spawnedCube.transform;
            var size = UserDefinedTargetEventHandler_preview.model.localScale.x * UserDefinedTargetEventHandler_preview.model.parent.localScale.x;
            effect.startSize = size * 2;
            effect.transform.localScale = new Vector3(1, 1, 1);
            effect.transform.localPosition = Vector3.zero;
            effect.transform.rotation = UserDefinedTargetEventHandler_preview.model.rotation * Quaternion.Euler(-90, 0, 0);
            StartCoroutine(ChangeSpawnEffectVelocity(effect, 1, 10 * size, spawnedCube));
        }
    }

    IEnumerator ChangeSpawnEffectVelocity(ParticleSystem effect, float timer, float newSpeed, GameObject newCube)
    {
        yield return new WaitForSeconds(timer);
        newCube.renderer.enabled = true;
        effect.startSpeed = newSpeed;
        yield return new WaitForSeconds(timer);
        effect.emissionRate = 0;
    }

}

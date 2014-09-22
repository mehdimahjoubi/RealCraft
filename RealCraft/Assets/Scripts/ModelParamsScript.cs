using UnityEngine;
using System.Collections;

public class ModelParamsScript : MonoBehaviour
{

    public static string ModelLocation { get; private set; }
    public static string ModelPath { get; private set; }
    public static string ModelTexturePath { get; private set; }

    void Awake()
    {
        //AndroidJavaClass jc = new AndroidJavaClass("org.iac.fitmyplace.UnityParams");
        //ModelLocation = jc.CallStatic<string>("getModelLocationType");
        //ModelPath = jc.CallStatic<string>("getModelPath");
        //if (ModelLocation == "external")
        //    ModelTexturePath = jc.CallStatic<string>("getModelTexturePath");
    }

}

using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[Serializable]
public class CraftState {

    public float CraftScale { get; set; }
    public float RootPositionX { get; set; }
    public float RootPositionY { get; set; }
    public float RootPositionZ { get; set; }
    public string RootColor { get; set; }
    public int RootTexture { get; set; }
    public float[] ChildrenLocalPosX { get; set; }
    public float[] ChildrenLocalPosY { get; set; }
    public float[] ChildrenLocalPosZ { get; set; }
    public string[] ChildrenColors { get; set; }
    public int[] ChildrensTextures { get; set; }

    public static void SerializeState(CraftState state, string filePath)
    {
        IFormatter formatter = new BinaryFormatter();
        Stream stream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None);
        formatter.Serialize(stream, state);
        stream.Close();
    }

    public static CraftState DeserializeState(string filePath)
    {
        try
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            CraftState craft = (CraftState)formatter.Deserialize(stream);
            stream.Close();
            return craft;
        }
        catch (SerializationException se)
        {
            throw se;
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PrefabToLua : MonoBehaviour {

    private static Dictionary<string, string> objsDic = new Dictionary<string, string>();
    private static string pathPrefix = "";

    [MenuItem("Assets/Tools/AutoGenerateLuaCode")]
    public static void AutoGenerateLuaCode()
    {
        //Debug.Log(Selection.activeGameObject.name);
        objsDic.Clear();
        pathPrefix = Selection.activeGameObject.name + "\\";
        Debug.Log("pathPrefix:" + pathPrefix);
        Recursion(Selection.activeGameObject.transform,"");
        foreach(KeyValuePair <string,string> pair in objsDic)
        {
            Debug.Log(pair.Key + " : " +  pair.Value);
        }
    }

    public static void Recursion(Transform root,string parentPath)
    {
        if (!string.IsNullOrEmpty(parentPath))
        {
            string path = parentPath + "\\" + root.name;
            path = path.Replace(pathPrefix, "");
            objsDic.Add(path, root.name);
        }
        if (root.childCount == 0)
        {
            return;
        }
        //Debug.Log(parentPath + " : " + parentPath);
        if (!string.IsNullOrEmpty(parentPath))
        {
            parentPath = parentPath + "\\" + root.name;
        }
        else
        {
            parentPath = root.name;
        }
        foreach (Transform child in root)
        {
            Recursion(child, parentPath);
        }
    }
}

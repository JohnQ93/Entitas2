using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;

public class AutoAddNamespace : UnityEditor.AssetModificationProcessor {

	private static void OnWillCreateAsset(string path)
    {
        path = path.Replace(".meta", "");
        if(path.EndsWith(".cs"))
        {
            string text = File.ReadAllText(path);
            Debug.Log(getClassName(text));
        }

    }

    private static string getClassName(string text)
    {
        string pattern = @"public class ([a-zA-Z0-9_]+)\s*:\s* MonoBehaviour";
        Regex regex = new Regex(pattern);
        var match = regex.Match(text);
        if(match.Success)
        {
            return match.Groups[1].Value;  //Groups[0]为匹配的整体，Groups[1]为匹配到的子项
        }
        return "";
    }
}

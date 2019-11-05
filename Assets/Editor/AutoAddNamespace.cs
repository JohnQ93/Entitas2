using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;
using System.Text;
using UnityEditor;

namespace CustomTool
{
    public class AutoAddNamespace : UnityEditor.AssetModificationProcessor
    {

        private static void OnWillCreateAsset(string path)
        {
            path = path.Replace(".meta", ""); //path是资源的meta文件路径
            if (path.EndsWith(".cs"))  //判断是否是创建的脚本文件.cs
            {
                StringBuilder stringBuilder = new StringBuilder();
                using (StreamReader sr = new StreamReader(path))
                {
                    string text = sr.ReadToEnd();
                    stringBuilder.Append(text);
                }
                
                using (StreamWriter sw = new StreamWriter(path))
                {
                    var newText = GetScriptContext(GetClassName(stringBuilder.ToString()));
                    sw.Write(newText);
                }
            }
            AssetDatabase.Refresh();
        }

        //更新脚本内容为新的类名 
        private static string GetScriptContext(string className)
        {
            var script = new ScriptBuildHelper();
            script.WriteUsing("UnityEngine");
            script.WriteLine("");
            script.WriteNamespace("UIFrame");
            script.WriteClass(className, 1);
            script.WriteFunction("Start", 2);
            //script.WriteFunction("Update",2, "float delta", "bool isUpdate");
            return script.toString();
        }

        //获取类名
        private static string GetClassName(string text)
        {
            string pattern = @"public class (\w+)\s*:\s* MonoBehaviour";
            var match = Regex.Match(text, pattern);
            if (match.Success)
            {
                return match.Groups[1].Value;  //Groups[0]为匹配的整体，Groups[1]为匹配到的子项,即小括号内的内容
            }
            return "";
        }
    }
}

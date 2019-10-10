using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;
using System.Text;

namespace CustomTool
{
    public class AutoAddNamespace : UnityEditor.AssetModificationProcessor
    {

        private static void OnWillCreateAsset(string path)
        {
            path = path.Replace(".meta", "");
            if (path.EndsWith(".cs"))
            {
                StringBuilder stringBuilder = new StringBuilder();
                using (StreamReader sr = new StreamReader(path))
                {
                    while(true)
                    {
                        string line = sr.ReadLine();
                        if(line == null)
                        {
                            break;
                        }
                        stringBuilder.AppendLine(line);
                    }
                    sr.Close();
                    sr.Dispose();
                }
                var newText = GetScriptContext(getClassName(stringBuilder.ToString()));
                
                using (StreamWriter sw = new StreamWriter(path))
                {
                    sw.WriteLine(stringBuilder.ToString());
                    sw.Close();
                    sw.Dispose();
                }
            }
        }

        private static string GetScriptContext(string className)
        {
            var script = new ScriptBuildHelper();
            script.indentTimes = 0;
            script.WriteUsing("UnityEngine");
            script.WriteLine("", false);
            script.WriteNamespace("UIFrame");
            script.indentTimes ++;
            script.WriteClass(className);
            script.indentTimes ++;
            script.WriteFunction("Start");
            //script.WriteFunction("Update");
            return script.toString();
        }

        private static string getClassName(string text)
        {
            string pattern = @"public class (\w+)\s*:\s* MonoBehaviour";
            Regex regex = new Regex(pattern);
            var match = regex.Match(text);
            if (match.Success)
            {
                return match.Groups[1].Value;  //Groups[0]为匹配的整体，Groups[1]为匹配到的子项
            }
            return "";
        }
    }
}

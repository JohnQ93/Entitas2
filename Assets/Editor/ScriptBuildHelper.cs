using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace CustomTool
{
    public class ScriptBuildHelper
    {
        private StringBuilder _stringBuilder;
        public int indentTimes { get; set; }
        private int currentIndex = 0;
        public ScriptBuildHelper()
        {
            _stringBuilder = new StringBuilder();
        }

        private void Write(string context, bool needIndent = false)
        {
            if (needIndent)
            {
                context = GetIndent() + context;
            }
            if (currentIndex == _stringBuilder.Length)
            {
                _stringBuilder.Append(context);
            }
            else
            {
                _stringBuilder.Insert(currentIndex, context);
            }
            currentIndex += context.Length;
        }

        public void WriteLine(string context, bool needIndent = false)
        {
            Write(context + "\n", needIndent);
        }

        private string  GetIndent()
        {
            string indent = "";
            for (int i = 0; i < indentTimes; i++)
            {
                indent += "    ";
            }
            return indent;
        }

        private int WriteCurlyBrackets()
        {
            var start = "\n" + GetIndent() + "{" + "\n";
            var end = GetIndent() + "}" + "\n";
            Write(start + end, false);
            return end.Length;
        }

        public void WriteUsing(string name)
        {
            WriteLine("using " + name + ";", false);
        }

        public void WriteNamespace(string name)
        {
            Write("namespace " + name);
            int length = WriteCurlyBrackets();
            currentIndex -= length;
        }

        public void WriteClass(string name)
        {
            Write("public class " + name + " : MonoBehaviour", true);
            int length = WriteCurlyBrackets();
            currentIndex -= length;
        }

        public void WriteFunction(string name, params string[] paraNames)
        {
            StringBuilder temp = new StringBuilder();
            temp.Append("void " + name + "()");
            if (paraNames.Length > 0)
            {
                foreach (var item in paraNames)
                {
                    temp.Insert(temp.Length - 1, item + ", ");
                }
                temp.Remove(temp.Length - 3, 2);
            }
            Write(temp.ToString(), true);
            WriteCurlyBrackets();
            WriteLine("");
        }

        public string toString()
        {
            return _stringBuilder.ToString();
        }
    }
}

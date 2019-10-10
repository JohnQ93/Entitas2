using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace CustomTool
{
    public class ScriptBuildHelper
    {
        private StringBuilder _stringBuilder;
        private int currentIndex = 0;
        public ScriptBuildHelper()
        {
            _stringBuilder = new StringBuilder();
        }

        /// <summary>
        /// 写入新的字符串内容
        /// </summary>
        /// <param name="context">要写入的文本内容</param>
        /// <param name="indentTimes">需要缩进的次数，1次为一个tab</param>
        private void Write(string context, int indentTimes = 0)
        {
            if (indentTimes > 0)
            {
                //如果需要缩进，则进行首行缩进
                context = GetIndent(indentTimes) + context;
            }

            if (currentIndex == _stringBuilder.Length)
            {
                //光标在末尾的时候添加字符串
                _stringBuilder.Append(context);
            }
            else
            {
                //光标在文本中间的时候插入字符串
                _stringBuilder.Insert(currentIndex, context);
            }
            //对当前光标位置进行累加
            currentIndex += context.Length;
        }

        public void WriteLine(string context, int indentTimes = 0)
        {
            Write(context + "\r\n", indentTimes);
        }

        //添加缩进的方法
        private string  GetIndent(int indentTimes)
        {
            string indent = "";
            for (int i = 0; i < indentTimes; i++)
            {
                indent += "    ";
            }
            return indent;
        }

        //添加大括号的方法
        private int WriteCurlyBrackets(int indentTimes, bool needWriteLine = false)
        {
            var start = "\r\n" + GetIndent(indentTimes) + "{" + "\r\n";
            var end = GetIndent(indentTimes) + "}" + "\r\n";
            if (needWriteLine)
            {
                Write(start + GetIndent(++indentTimes) + "\r\n" + end);
            }
            else
            {
                Write(start + end);
            }
            return end.Length;
        }

        public void WriteUsing(string name)
        {
            WriteLine("using " + name + ";");
        }

        public void WriteNamespace(string name, int indentTimes = 0)
        {
            Write("namespace " + name);
            int length = WriteCurlyBrackets(indentTimes);
            currentIndex -= length;
        }

        public void WriteClass(string name, int indentTimes = 0)
        {
            Write("public class " + name + " : MonoBehaviour", indentTimes);
            int length = WriteCurlyBrackets(indentTimes);
            currentIndex -= length;
        }

        public void WriteFunction(string name, int indentTimes = 0, params string[] paraNames)
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
            Write(temp.ToString(), indentTimes);
            WriteCurlyBrackets(indentTimes, true);
        }

        public string toString()
        {
            return _stringBuilder.ToString();
        }
    }
}

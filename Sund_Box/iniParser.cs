using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace Sund_Box
{
    class iniParser
    {
        string PATH;
        string extension;

        public iniParser()
        {
            while (PATH != "" && extension != ".ini")
            {
                Console.WriteLine("Enter a file path for his a parse");
                PATH = @Console.ReadLine();
                extension = Path.GetExtension(PATH);

                if (PATH == "")
                {
                    Console.WriteLine(@"C:\Users\sheam\Desktop\IniFileForTheTest.ini");
                    PATH = @"C:\Users\sheam\Desktop\IniFileForTheTest.ini";
                    extension = Path.GetExtension(PATH);
                }
                else if (extension != ".ini")
                {
                    Console.WriteLine("Format is not \".ini\"");
                    
                }
            }
        }

        Dictionary<string, Dictionary<string, string>> DictIniFile()
        {
            ArrayList arrayList = new ArrayList();
            string[] fileINI = File.ReadAllLines(PATH);
            arrayList.AddRange(fileINI);
            Dictionary<string, Dictionary<string, string>> dict = new Dictionary<string, Dictionary<string, string>>();

            foreach (string str in fileINI)
            {
                if (str.StartsWith('[') && str.EndsWith(']'))
                {
                    Dictionary<string, string> underDict = new Dictionary<string, string>();
                    string keyDict = str.Trim(new char[] { '[', ']' });
                    
                    for (int i = arrayList.IndexOf(str) + 1; i < fileINI.Length; i++)
                    {
                        if (fileINI[i].Contains("="))
                        {
                            string strMod = fileINI[i].Contains(';') ? fileINI[i].Remove(fileINI[i].IndexOf(';')) : fileINI[i];

                            string keyUnderDict = strMod.Trim().Substring(0, strMod.IndexOf('=') - 1);
                            string valueUnderDict = strMod.Trim().Substring(strMod.IndexOf('=') + 1);

                            underDict.Add(keyUnderDict, valueUnderDict);
                        }
                        else if (fileINI[i].StartsWith('[') && fileINI[i].EndsWith(']'))
                        {
                            break;
                        }
                    }

                    dict.Add(keyDict, underDict);
                }
            }
            
            return dict;
        }

        public async void fileInfo()
        {            
            FileInfo file = new FileInfo(PATH);

            if (file.Exists)
            {
                Console.WriteLine($"\nFile name: {file.Name}");
                Console.WriteLine($"Time of creation: {file.CreationTime}");
                Console.WriteLine($"File size: {file.Length}\n");
            }
            else
            {
                Console.WriteLine("\nFile does is not exist\n");
            }

            using (StreamReader st = new StreamReader(PATH))
            {

                Console.WriteLine("--------------------------------\n");
                string line = await st.ReadToEndAsync();
                Console.WriteLine(line + "\n\n---------------------------------\n");

            }
   
        }
        public void Read()
        {
            Console.WriteLine("What value do you want to find?");
            string section = "";
            string value = "";

            while (section == "")
            {
                Console.WriteLine("Enter a section");
                section = Console.ReadLine();
                if (!DictIniFile().ContainsKey(section.ToUpper()))
                {
                    Console.WriteLine("Section is does not exist");
                    section = "";
                }
            }
            while (value == "")
            {
                Console.WriteLine("Enter a value");
                value = Console.ReadLine();
            }

            foreach (KeyValuePair<string, Dictionary<string, string>> keyValue in DictIniFile())
            {
                if (section.ToLower() == keyValue.Key.ToLower())
                {
                    foreach (KeyValuePair<string, string> underKeyValue in keyValue.Value)
                    {
                        if (value.ToLower() == underKeyValue.Key.ToLower())
                        {
                            Console.WriteLine($"\nSection : {keyValue.Key}\nValue: {underKeyValue.Key} = {underKeyValue.Value}");
                        }
                        
                    }
                }
            }
        }
    }
}

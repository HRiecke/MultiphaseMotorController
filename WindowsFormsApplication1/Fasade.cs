using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    public class Fasade
    {
        private Dictionary<string, object> stepMode = new Dictionary<string, object>();
        private object loadedStepMode;

        public Fasade()
        {
            Init();
        }

        private void Init()
        {
            Assembly assembly;

            FileStream fs = new FileStream("interfaceLog.txt", FileMode.Create);
            StreamWriter w = new StreamWriter(fs, Encoding.UTF8);

            string[] filePaths = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.exe");

            foreach (string path in filePaths)
            {
                try
                {
                    assembly = Assembly.LoadFile(path);
                    //System.Console.WriteLine(Path.GetFileName(path));
                }
                catch
                {
                    continue;
                }

                try
                {
                    Type[] typeList = null;
                    try
                    {
                        typeList = assembly.GetTypes();
                    }
                    catch (ReflectionTypeLoadException lex)
                    {
                        string le = "\nCalling assembly: " + Assembly.GetCallingAssembly().Location +
                            "\nLoaded assembly: " + assembly.Location +
                            "\n" + lex.Message + "\n";

                        foreach (Exception item in lex.LoaderExceptions)
                        {
                            le = le + "\n" + item.Message;
                        }
                        System.Console.WriteLine("ReflectionTypeLoadException: " + le);
                        continue;
                    }

                    if (typeList == null || typeList.Length < 1)
                        continue;

                    System.Console.WriteLine(Path.GetFileName(path));
                    w.WriteLine(Path.GetFileName(path));

                    foreach (Type type in typeList)
                    {
                        // alle typen durchgehen und prüfen ober der Type das
                        // Interface IStepMode implementiert hat
                        Type interfaceList = type.GetInterface("IStepMode");
                        if (interfaceList == null) continue;

                        w.WriteLine("Interface Name = " + type.Name);

                        try
                        {
                            object obj = Activator.CreateInstance(type);
                            if (obj == null) continue;
                                           
                            string st;
                            try
                            {
                                st = GetPropValue(obj, "StepName") as string;
                                w.WriteLine("StepName = " + st);

                                stepMode.Add(st, obj);
                            }
                            catch (Exception ex)
                            {
                                w.WriteLine("Exception by StepName = " + ex.Message);
                            }

                            try
                            {
                                bool isCW = (bool)GetPropValue(obj, "IsCW");
                                w.WriteLine("IsCW = " + isCW);
                            }
                            catch (Exception ex)
                            {
                                w.WriteLine("Exception by IsCW = " + ex.Message);
                            }

                            try
                            {
                                int sleep = (int)GetPropValue(obj, "Sleep");
                                w.WriteLine("Init Sleep Value = " + sleep);
                            }
                            catch (Exception ex)
                            {
                                w.WriteLine("Exception by Sleep = " + ex.Message);
                            }

                            try
                            {
                                int newVal = 400;
                                SetPropValue(obj, "Sleep",newVal);
                                w.WriteLine("Set a new Sllep Value of "+ newVal);
                            }
                            catch (Exception ex)
                            {
                                w.WriteLine("Exception by Set Sleep Value..." + ex.Message);
                            }

                            try
                            {
                                int sleep = (int)GetPropValue(obj, "Sleep");
                                w.WriteLine("REad new Sleep Value = " + sleep);
                            }
                            catch (Exception ex)
                            {
                                w.WriteLine("Exception by Sleep = " + ex.Message);
                            }

                        }
                        catch (Exception ex)
                        {
                            System.Diagnostics.Trace.TraceError(
                                "Factory Activator.CreateInstance Type: " + type +
                                " " + ex.Message);
                            while ((ex = ex.InnerException) != null)
                            {
                                System.Diagnostics.Trace.TraceError("InnerException " + ex.Message);
                            }
                            continue;
                        }
                    }
                }
                catch (ReflectionTypeLoadException ex)
                {
                    System.Diagnostics.Trace.TraceError("Factory.RegisterAssemblyByIFactory " +
                        "ReflectionTypeLoadException " + ex.Message);
                }
            }
            w.Close();
            fs.Close();
        }

        public static object GetPropValue(object src, string propName)
        {
            PropertyInfo pi = src.GetType().GetProperty(propName);
            return pi.GetValue(src, null);
        }
        public static void SetPropValue(object src, string propName,object value)
        {
            PropertyInfo pi = src.GetType().GetProperty(propName);
            pi.SetValue(src, value);
        }

        public static object GetMethodValue(object src, string propName, object[] parameters)
        {
            return src.GetType().InvokeMember(propName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.InvokeMethod, null, src, parameters);
        }


        public string[] StepModeName
        {
            get
            {
                List<string> names = new List<string>();
                foreach (var item in stepMode)
                {
                    names.Add(item.Key);
                }
                return names.ToArray();
            }
        }

        public string LoadStepMode { 
            set {
                loadedStepMode = null;
                if (stepMode.ContainsKey(value) == true)
                {
                    loadedStepMode = stepMode[value];
                }
            } 
        }

        public int Sleep { set { int sleep = value; } }

        public void Start()
        {
            try
            {
                object o = GetMethodValue(loadedStepMode, "Start", null);
                //w.WriteLine("Call Start...");

            }
            catch (Exception ex)
            {
                //w.WriteLine("Exception by Start = " + ex.Message);
            }
        }
        public void Stop()
        {
            try
            {
                object o = GetMethodValue(loadedStepMode, "Stop", null);
                //w.WriteLine("call Stop");

            }
            catch (Exception ex)
            {
                //w.WriteLine("Exception by Stop = " + ex.Message);
            }
        }
    }
}

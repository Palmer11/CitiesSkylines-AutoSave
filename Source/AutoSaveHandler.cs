/// Created By: Palmer11
/// Steam ID: oPalmer
using System;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using System.Collections.Generic;
using ColossalFramework.IO;
using Handler.Save;

namespace Mod.AutoSave
{
    public class AutoSaveHandler
    {
        #region Variables

        public const short CONST_MAX_FILE_COUNT = 999;
        private static List<string> m_ListOfSavedNames = null;
        public static ConfigFile m_ConfigFile = null;

        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        public AutoSaveHandler()
        {
            m_ListOfSavedNames = new List<string>(m_ConfigFile.SaveCount);
            FindCurrentSaves();   
        }

        /// <summary>
        /// Deconstructor
        /// </summary>
        ~AutoSaveHandler()
        {
            if (m_ListOfSavedNames != null)
            {
                m_ListOfSavedNames = null;
            }

            if (m_ConfigFile != null)
            {
                m_ConfigFile = null;
            }
        }

        /// <summary>
        /// Returns the name for the file.
        /// </summary>
        /// <returns></returns>
        public static string GetFileName()
        {
            string n = m_ConfigFile.SavePrefix;

            short c = (short)m_ListOfSavedNames.Count;
            short cap = (short)m_ListOfSavedNames.Capacity;

            if (c < cap)
            {
                n += DigitCounter(c);
            }
            else if (c >= cap)
            {
                n = m_ListOfSavedNames.First();
                m_ListOfSavedNames.RemoveAt(0);
            }

            m_ListOfSavedNames.Add(n);
            return n;
        }

        /// <summary>
        /// Check if current autosaves exists and
        /// iterate through them. If numaric count
        /// doesn't match the number of saves,
        /// files will be renamed.
        /// </summary>
        private void FindCurrentSaves()
        {
            string p = DataLocation.saveLocation;
            string[] fs = Directory.GetFiles(p);

            if (fs.Length > 0)
            {
                List<string> afList = new List<string>();

                try
                {
                    foreach (string f in fs)
                    {
                        string fwe = Path.GetFileName(f);
                        string fn = fwe.Remove(fwe.Length - 4);

                        if (fn.StartsWith(m_ConfigFile.SavePrefix))
                        {
                            afList.Add(fn);
                        }
                    }

                    if (afList.Count > 0)
                    {
                        afList.Sort();

                        for (short i = 0; i < afList.Count; i++)
                        {
                            string f = afList[i];
                            string n = DigitCounter(i);

                            if (f.EndsWith(n) == false)
                            {
                                string nfn = f.Remove(f.Length - 3) + n;
                                string ofp = p + @"\" + f + ".crp";
                                string nfp = p + @"\" + nfn + ".crp";

                                File.Move(ofp, nfp);
                                f = nfp;
                            }

                            m_ListOfSavedNames.Add(f);
                        }
                    }
                }
                catch (Exception /*e*/)
                {
                    // Error occured
                }
            }
        }

        /// <summary>
        /// Count the number of digits and
        /// place the required fillers.
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        private static string DigitCounter(short n)
        {
            string s = string.Empty;
            short d = (short)Math.Floor(Math.Log10(n) + 1);

            switch (d)
            {
                case 3:
                    break;
                case 2:
                    s = "0";
                    break;
                default:
                    s = "00";
                    break;
            }

            return s + n.ToString();
        }
    }

    [Serializable]
    public class ConfigFile : XmlFile
    {
        public ConfigFile()
        {
            Title = "AutoSave Configuration";
            SavePrefix = "autosave_";
            TimeDelay = 600;   
            SaveCount = 50;
        }

        [XmlElement("SavePrefix")]
        public string SavePrefix;

        [XmlElement("SaveDelay")]
        public short TimeDelay;

        [XmlElement("SaveCount")]
        public short SaveCount;
    }
}

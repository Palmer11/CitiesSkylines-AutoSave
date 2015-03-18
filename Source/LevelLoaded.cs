/// Created By: Palmer11
/// Steam ID: oPalmer
using System.Collections;
using ICities;
using UnityEngine;
using Handler.Save;
using ColossalFramework.IO;
using ColossalFramework;


namespace Mod.AutoSave
{
    public class LevelLoaded : LoadingExtensionBase
    {
        private const string CONST_FILE_CONFIG = @"\..\autosave_config.xml";
        private static AutoSaveHandler m_AutoSaveHandler = null;
        public static SaveHandler m_SaveHandler = null;

        
        public override void OnLevelLoaded(LoadMode mode)
        {
            m_SaveHandler = new SaveHandler();

            string sl = DataLocation.saveLocation + CONST_FILE_CONFIG;
            m_SaveHandler.CreateFile<ConfigFile>(sl, new ConfigFile(), false);
            AutoSaveHandler.m_ConfigFile = m_SaveHandler.GetFile<ConfigFile>(sl);

            short td = AutoSaveHandler.m_ConfigFile.TimeDelay;
            if (SAutoSave.m_Delay != td)
            {
                if (td > AutoSaveHandler.CONST_MAX_FILE_COUNT)
                {
                    td = AutoSaveHandler.CONST_MAX_FILE_COUNT;
                }

                SAutoSave.m_Delay = td;
            }

            m_AutoSaveHandler = new AutoSaveHandler();         
        }

        public override void OnLevelUnloading()
        {
            if (m_AutoSaveHandler != null)
            {
                m_AutoSaveHandler = null;
            }

            if (m_SaveHandler != null)
            {
                m_SaveHandler = null;
            }
        }
    }
}

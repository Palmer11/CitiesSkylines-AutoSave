/// Created By: Palmer11
/// Steam ID: oPalmer
using System;
using System.Timers;
using ICities;

namespace Mod.AutoSave
{
    public class SAutoSave : SerializableDataExtensionBase
    {
        private static Timer m_Timer = null;
        public static float m_Delay = 600.0f;

        public override void OnLoadData()
        {
            m_Timer = new Timer();
            m_Timer.Elapsed += new ElapsedEventHandler((sender, e) => SaveGame(sender, e, this.serializableDataManager));
            m_Timer.Interval = (m_Delay * 1000);
            m_Timer.Start();
        }

        public override void OnReleased()
        {
            if (m_Timer != null)
            {
                m_Timer.Stop();
                m_Timer = null;
            }
        }

        private static void SaveGame(object sender, ElapsedEventArgs e, ISerializableData sd)
        {
            sd.SaveGame(AutoSaveHandler.GetFileName());
            m_Timer.Start();
        }

        public static void DebugMsg(string msg)
        {
            DebugOutputPanel.AddMessage(ColossalFramework.Plugins.PluginManager.MessageType.Message, msg);
        }
    }
}
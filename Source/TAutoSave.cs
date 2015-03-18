/// Created By: Palmer11
/// Steam ID: oPalmer
using System;
using ICities;

namespace Mod.AutoSave
{
    public class TAutoSave : ThreadingExtensionBase
    {
       
        private static float m_Time = 0.0f;
        public static float m_Delay = 600.0f;

        public override void OnUpdate(float realTimeDelta, float simulationTimeDelta)
        {
            if (m_Time >= m_Delay)
            {
                managers.serializableData.SaveGame(AutoSaveHandler.GetFileName());
                m_Time = 0.0f;
            }
            else
            {
                m_Time += realTimeDelta;
            }
        }

        //public static void DebugMsg(string msg)
        //{
        //    DebugOutputPanel.AddMessage(ColossalFramework.Plugins.PluginManager.MessageType.Message, msg);
        //}
    }
}
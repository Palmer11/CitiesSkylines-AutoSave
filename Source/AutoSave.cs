using System;
using ICities;
using UnityEngine;


namespace Mod.AutoSave
{

    public class AutoSave : IUserMod 
    {

        public string Name 
        {
            get { return "AutoSave"; }
        }

        public string Description 
        {
            get { return "Auto Saves your current game every 10 minutes."; }
        }
    }

    public class ThreadingAutoSave : ThreadingExtensionBase
    {
        float time = 0.0f;
        float delay = 600.0f;

        public override void OnUpdate(float realTimeDelta, float simulationTimeDelta)
        {
            if (time >= delay)
            {
                managers.serializableData.SaveGame("autosave_" + DateTime.Now);
                time = 0.0f;
            }
            else
            {
                time += realTimeDelta;
            }
        }
    }
}
/// Created By: Palmer11
/// Steam ID: oPalmer
using ICities;

namespace Mod.AutoSave
{
    public class IAutoSave : IUserMod
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
}

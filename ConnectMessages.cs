

using System;

namespace Oxide.Plugins
{
    [Info("ConnectMessages", "CatMeat", "0.2.0", ResourceId = 00000)]
    [Description("Announce connects and disconnects in chat")]
    public class ConnectMessages : RustPlugin
    {
        public bool ignoreAdmin = true;

        public string msgConnect = "has connected.";
        public string msgDisconnect = "has disconnected.";

        void OnPlayerInit(BasePlayer player)
        {
            string msg = ($"{player.displayName} ") + msgConnect;
            if (player?.net?.connection != null)
                if (player.net.connection.authLevel < 1 || !ignoreAdmin)
                    PrintToChat(msg);
        }

        void OnPlayerDisconnected(BasePlayer player, string reason)
        {
            string msg = ($"{player.displayName} ") + msgDisconnect;
            if (player?.net?.connection != null)
                if (player.net.connection.authLevel < 1 || !ignoreAdmin)
                    PrintToChat(msg);
        }

        void Init()
        {
            LoadDefaultConfig();
        }

        T GetConfig<T>(string name, T value) => Config[name] == null ? value : (T)Convert.ChangeType(Config[name], typeof(T));

        protected override void LoadDefaultConfig()
        {
            Config["Ignore Admins"] = ignoreAdmin = GetConfig("Ignore Admins", true);
            Config["Connect Message"] = msgConnect = GetConfig("Connect Message", "has connected.");
            Config["Disconnect Message"] = msgDisconnect = GetConfig("Disconnect Message", "has disconnected.");

            SaveConfig();
        }
    }
}
using System;
using JetBrains.Annotations;
using Supabase.Gotrue;
using TSS.Core;
using UnityEngine;
using Client = Supabase.Client;

namespace TSS.Supabase
{
    [PublicAPI]
    public static class SupabaseManager
    {
        public static bool IsInitialized => _isInitialized;
        public static Client Client => _client;
        public static string CurrentUserId => SystemInfo.deviceUniqueIdentifier;
        
        private static bool _isInitialized;
        private static NetworkStatus _networkStatus;
        private static Client _client;
        private static IDisposable _quitDisposable;
        
        internal static void Initialize(
            NetworkStatus networkStatus,
            Client client)
        {
            _networkStatus = networkStatus;
            _client = client;
            _isInitialized = true;
            _quitDisposable = Runtime.SubscribeQuit(() =>
            {
                _client?.Auth.Shutdown();
                _client = null;
                _isInitialized = false;
            });
        }

        internal static void Dispose()
        {
            _quitDisposable?.Dispose();
            _client = null;
            _isInitialized = false;
        }
    }
}
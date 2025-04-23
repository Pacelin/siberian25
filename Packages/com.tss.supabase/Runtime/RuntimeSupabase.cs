using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using Supabase;
using Supabase.Gotrue;
using Supabase.Gotrue.Interfaces;
using TSS.Core;
using TSS.Supabase.Data;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Client = Supabase.Client;
using User = Supabase.Gotrue.User;

namespace TSS.Supabase
{
    [UsedImplicitly]
    [RuntimeOrder(ERuntimeOrder.SystemRegistration)]
    public class RuntimeSupabase : IRuntimeLoader
    {
        private IDisposable _quitDisposable;
        
        public async UniTask Initialize(CancellationToken cancellationToken)
        {
            var networkStatus = new NetworkStatus();
            var settings = await Addressables.LoadAssetAsync<SupabaseSettings>("Supabase Settings");
            
            var options = new SupabaseOptions();
            options.AutoRefreshToken = true;
            options.AutoConnectRealtime = true;
            options.Schema = "public";

            var client = new Client(settings.SupabaseURL, settings.SupabaseAnonKey, options);
            client.Auth.AddDebugListener((msg, e) =>
            {
                Debug.Log(msg);
                if (e != null)
                    Debug.LogException(e);
            });

            networkStatus.Client = client.Auth;
            client.Auth.SetPersistence(new SupabaseUnitySession());
            client.Auth.AddStateChangedListener(UnityAuthListener);
            client.Auth.LoadSession();

            string url = $"{settings.SupabaseURL}/auth/v1/settings?apikey={settings.SupabaseAnonKey}";
            try
            {
                client.Auth.Online = await networkStatus.StartAsync(url);
            }
            catch (NotSupportedException _)
            {
                client.Auth.Online = true;
            }
            catch (Exception e)
            {
                Debug.LogException(e);
                client.Auth.Online = false;
            }

            if (client.Auth.Online)
            {
                try
                {
                    await client.InitializeAsync();
                    await client.SureCurrentUserExists(cancellationToken);
                    SupabaseManager.Initialize(networkStatus, client);
                }
                catch (Exception e)
                {
                    Debug.LogException(e);
                }
            }
        }

        public void Dispose()
        {
            SupabaseManager.Dispose();
        }
        
        private void UnityAuthListener(IGotrueClient<User, Session> sender, Constants.AuthState newState)
        {
            switch (newState)
            {
                case Constants.AuthState.SignedIn:
                    Debug.Log("Signed In: " + sender.CurrentUser?.Id);
                    break;
                case Constants.AuthState.SignedOut:
                    Debug.Log("Signed Out" + sender.CurrentUser?.Id);
                    break;
                case Constants.AuthState.UserUpdated:
                    Debug.Log("Signed In: " + sender.CurrentUser?.Id);
                    break;
                case Constants.AuthState.PasswordRecovery:
                    Debug.Log("Password Recovery");
                    break;
                case Constants.AuthState.TokenRefreshed:
                    Debug.Log("Token Refreshed");
                    break;
                case Constants.AuthState.Shutdown:
                    Debug.Log("Shutdown");
                    break;
                default:
                    Debug.Log("Unknown Auth State Update");
                    break;
            }
        }
    }
}
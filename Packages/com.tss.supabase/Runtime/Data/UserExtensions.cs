using System.Threading;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using Postgrest;
using Client = Supabase.Client;

namespace TSS.Supabase.Data
{
    [PublicAPI]
    public static class UserExtensions
    {
        internal static async UniTask SureCurrentUserExists(this Client client, CancellationToken cancellationToken)
        {
            if (client.Auth.Online)
            {
                var count = await client.From<User>()
                    .Filter("id", Constants.Operator.Equals, SupabaseManager.CurrentUserId)
                    .Count(Constants.CountType.Exact, cancellationToken);
                if (count == 0)
                    await client.From<User>()
                        .Insert(new User(), cancellationToken: cancellationToken);
            }
        }

        public static async UniTask<int> GetUsersCount(this Client client, CancellationToken cancellationToken)
        {
            if (client.Auth.Online)
                return await client.From<User>().Count(Constants.CountType.Exact, cancellationToken);
            return 1;
        }
    }
}
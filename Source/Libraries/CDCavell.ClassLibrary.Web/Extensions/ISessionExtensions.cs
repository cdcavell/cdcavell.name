using CDCavell.ClassLibrary.Web.Security;
using System.Text.Json;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Http
{
    /// <summary>
    /// Extension methods for existing ISession types.
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.3.1 | 02/07/2021 | Utilize Redis Cache - Not implemented |~ 
    /// </revision>
    public static class ISessionExtensions
    {
        /// <summary>
        /// Method to return decrypted object from Http Session
        /// </summary>
        /// <param name="session">ISession</param>
        /// <param name="key">string</param>
        /// <returns>&lt;T&gt;</returns>
        /// <method>Decrypt&lt;T&gt;(this ISession session, string key)</method>
        public static async Task<T> Decrypt<T>(this ISession session, string key)
        {
            await session.LoadAsync();

            var value = session.GetString(key);
            return value == null ? default : JsonSerializer.Deserialize<T>(AESGCM.Decrypt(value));
        }

        /// <summary>
        /// Method to store encrypted object in Http Session
        /// </summary>
        /// <param name="session">ISession</param>
        /// <param name="key">string</param>
        /// <param name="value">T</param>
        /// <method>Encrypt&lt;T&gt;(this ISession session, string key, T value)</method>
        public static async void Encrypt<T>(this ISession session, string key, T value)
        {
            session.SetString(key, AESGCM.Encrypt(JsonSerializer.Serialize(value)));
            await session.CommitAsync();
        }
    }
}
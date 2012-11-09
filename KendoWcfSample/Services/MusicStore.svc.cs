using System.Data.Services;
using System.Data.Services.Common;
using System.Web;
using KendoWcfSample.Models;

namespace KendoWcfSample.Services
{
    public class MusicStore : DataService<MusicStoreEntities>
    {
        private const string ADMIN_ROLE = "Administrator";

        public static void InitializeService(DataServiceConfiguration config)
        {
            config.SetEntitySetAccessRule("Albums", EntitySetRights.All);
            config.SetEntitySetAccessRule("Artists", EntitySetRights.AllRead);
            config.SetEntitySetAccessRule("Genres", EntitySetRights.AllRead);
            config.DataServiceBehavior.MaxProtocolVersion = DataServiceProtocolVersion.V3;
        }

        /// <summary>
        /// Only admin users can modify Albums.
        /// </summary>
        [ChangeInterceptor("Albums")]
        public void OnChangeAlbum(Album album, UpdateOperations operations)
        {
            if (!HttpContext.Current.Request.IsAuthenticated || !HttpContext.Current.User.IsInRole(ADMIN_ROLE))
                throw new DataServiceException(400, "Albums can only be modified by an administrator.");
        } 
    }
}

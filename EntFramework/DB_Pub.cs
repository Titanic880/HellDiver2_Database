using HellDiver2_API2DB.EntFramework;
using HellDiver2_API2DB.V1_Objects;

namespace HD2_EFDatabase.EntFramework {
    public static class DB_Pub {
        #region assignmentData
        public static assignmentData? GetAssignmentData(long PrimaryKey) {
            using DB_Context context = new();
            if (!context.Database.CanConnect()) {
                return null;
            }
            return context.assignmentDatas.Where(x=> x.PK_id == PrimaryKey)
                .OrderBy(x=>x.PK_id)
                .LastOrDefault();
        }
        /// <summary>
        /// Title is not case sensitive
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        public static assignmentData? GetAssignmentData(string title) {
            using DB_Context context = new();
            if (!context.Database.CanConnect()) {
                return null;
            }
            return context.assignmentDatas.Where(
                x => x.title.Equals(title, StringComparison.CurrentCultureIgnoreCase)
               ).OrderBy(x => x.PK_id)
                .LastOrDefault();
        }
        public static assignmentData? GetAssignmentData(DateTime expiration) {
            using DB_Context context = new();
            if (!context.Database.CanConnect()) {
                return null;
            }
            return context.assignmentDatas.Where(
                x => x.expiration.ToString("g") == expiration.ToString("g")
               ).OrderBy(x => x.PK_id)
                .LastOrDefault();
        }

        /// <summary>
        /// Unsure if i want this to be part of the public API
        /// </summary>
        /// <returns></returns>
        internal static assignmentData[] GetAllAssignmentData() {
            using DB_Context context = new();
            if (!context.Database.CanConnect()) {
                return [];
            }
            return [.. context.assignmentDatas];
        }
        #endregion assignmentData
        #region Campaign2
        public static Campaign2? GetCampaign(long PrimaryKey) {
            using DB_Context context = new();
            if (!context.Database.CanConnect()) {
                return null;
            }
            return context.campaign2s.Where(x => x.PK_id == PrimaryKey)
                .OrderBy(x => x.PK_id)
                .LastOrDefault();
        }

        public static Campaign2? GetCampaign(int Planet_Index) {

        }
        public static Campaign2? GetCampaign()string Planet_Name {

        }

        /// <summary>
        /// Unsure if i want this to be part of the public API
        /// </summary>
        /// <returns></returns>
        internal static Campaign2[] GetAllCampaigns() {
            using DB_Context context = new();
            if (!context.Database.CanConnect()) {
                return [];
            }
            return [.. context.campaign2s];
        }
        #endregion Campaign2
        #region Dispatch

        #endregion Dispatch
        #region eventData

        #endregion eventData
        #region Planet

        #endregion Planet
        #region Statistics

        #endregion Statistics
        #region steamData

        #endregion steamData
        #region WarInfo

        #endregion WarInfo
    }
}

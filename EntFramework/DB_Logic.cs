using HellDiver2_API2DB.raw_Objects;
using HellDiver2_API2DB.V1_Objects;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace HellDiver2_API2DB.EntFramework {
    internal static class DB_Logic {
        #region GetData
        public static T[] GetDatabaseItems<T>(int[] indexes) {
            using DB_Context dB_Context = new();
            object? result = dB_Context.Find(typeof(T),indexes);
            if (result is null) {
                return [];
            } else if (result is T[] v) {
                return v;
            }
            throw new Exception($"Type Fail: Expected: {typeof(T).Name}, recieved: {result.GetType().Name}");
        }
        public static PlanetStatus[] GetStatuses(int[] indexes) {
            return GetDatabaseItems<PlanetStatus>(indexes);
        }
        public static planetAttacks[] GetplanetAttacks(int[] indexes) {
            return GetDatabaseItems<planetAttacks>(indexes);
        }
        public static Campaign[] GetCampaigns(int[] indexes) {
            return GetDatabaseItems<Campaign>(indexes);
        }
        public static JointOperation[] GetJointOperations(int[] indexes) {
            return GetDatabaseItems<JointOperation>(indexes);
        }
        public static eventData[] GetEvents(int[] indexes) {
            return GetDatabaseItems<eventData>(indexes);
        }
        #endregion GetData
        #region AddToDatabase
        public static bool AddAssignmentData(assignmentData[] Data) {
            using DB_Context cont = new();
            long[] keys = [
                GetNextassignmentKey(),
                GetNextTaskDataKey(),
                GetNextRewardKey()
                ];

            bool ret = false;
            for(int i = 0; i < Data.Length; i++) {
               assignmentData? data = cont.assignmentDatas.Where(x => !x.Equals(Data[i])).FirstOrDefault();
                if(data == null) {
                    Data[i].PK_id = keys[0];
                    taskData[] vals = [.. Data[i].tasks];
                    for(int x = 0; x < vals.Length; x++) {
                        vals[x].PK_id = keys[1];
                        keys[1]++;
                    }
                    Data[i].tasks = vals;
                    Data[i].reward.PK_id = keys[2];
                    cont.assignmentDatas.Add(Data[i]);
                    ret = true;
                    keys[0]++;
                    //keys[1] is updated during assignment
                    keys[2]++;
                }
            }
            cont.SaveChanges();
            return ret;
        }
        public static bool AddCampaign2(Campaign2[] Data) {
            using DB_Context cont = new();
            bool ret = false;
            for (int i = 0; i < Data.Length; i++) {
                Campaign2? data = cont.campaign2s.Where(x => !x.Equals(Data[i])).FirstOrDefault();
                if (data == null) {
                    cont.campaign2s.Add(Data[i]);
                    ret = true;
                }
            }
            cont.SaveChanges();
            return ret;
        }
        public static bool AddDispatch(Dispatch[] Data) {
            using DB_Context cont = new();
            bool ret = false;
            for (int i = 0; i < Data.Length; i++) {
                Dispatch? data = cont.dispatches.Where(x => !x.Equals(Data[i])).FirstOrDefault();
                if (data == null) {
                    cont.dispatches.Add(Data[i]);
                    ret = true;
                }
            }
            cont.SaveChanges();
            return ret;
        }
        public static bool AddEventData(eventData[] Data) {
            using DB_Context cont = new();
            bool ret = false;
            for (int i = 0; i < Data.Length; i++) {
                eventData? data = cont.eventDatas.Where(x => !x.Equals(Data[i])).FirstOrDefault();
                if (data == null) {
                    cont.eventDatas.Add(Data[i]);
                    ret = true;
                }
            }
            cont.SaveChanges();
            return ret;
        }
        public static bool AddPlanets(Planet[] Data){
            using DB_Context cont = new();
            long[] keys = [
                GetNextPlanetKey(),
                GetNextpositionKey(),
                GetNexteventDataKey(),
                GetNextStatsKey()
                ];

            bool ret = false;
            for (int i = 0; i < Data.Length; i++) {
                Planet? data = cont.planets.Where(x => !x.Equals(Data[i])).FirstOrDefault();
                if (data == null) {
                    Data[i].PK_id = keys[0];
                    Data[i].position.PK_id = keys[1];
                    if (Data[i].events != null) {
                        Data[i].events!.PK_id = keys[2];
                        keys[2]++;
                    }else {
                        Data[i].FK_Events_ID = null;
                    }
                    if (Data[i].statistics != null) {
                        Data[i].statistics!.PK_id = keys[3];
                        keys[3]++;
                    } else {
                        Data[i].FK_Stats_ID = null;
                    }
                    cont.planets.Add(Data[i]);

                    keys[0]++;
                    keys[1]++;
                    //keys[2] is updated during assignment
                    //keys[3] is updated during assignment
                    ret = true;
                }
            }
            cont.SaveChanges();
            return ret;
        }



        public static bool AddsteamData(steamData[] Data) {
            using DB_Context cont = new();
            bool ret = false;
            for (int i = 0; i < Data.Length; i++) {
                steamData? data = cont.steamDatas.Where(x => !x.Equals(Data[i])).FirstOrDefault();
                if (data == null) {
                    cont.steamDatas.Add(Data[i]);
                    ret = true;
                }
            }
            cont.SaveChanges();
            return ret;
        }
        public static bool AddWarInfo(WarInfo[] Data) {
            using DB_Context cont = new();
            bool ret = false;
            for (int i = 0; i < Data.Length; i++) {
                WarInfo? data = cont.warInfos.Where(x => !x.Equals(Data[i])).FirstOrDefault();
                if (data == null) {
                    cont.warInfos.Add(Data[i]);
                    ret = true;
                }
            }
            cont.SaveChanges();
            return ret;
        }
        #endregion AddToDatabase
        #region KeyValues
        private static long GetNextassignmentKey() {
            using DB_Context cont = new();
            if (!cont.assignmentDatas.Any()) {
                return 0;
            }
            return cont.assignmentDatas.OrderBy(x => x.PK_id).Last().PK_id + 1;
        }
        private static long GetNextTaskDataKey() {
            using DB_Context cont = new();
            if (!cont.taskDatas.Any()) {
                return 0;
            }
            return cont.taskDatas.OrderBy(x => x.PK_id).Last().PK_id + 1;
        }
        private static long GetNextRewardKey() {
            using DB_Context cont = new();
            if (!cont.rewards.Any()) {
                return 0;
            }
            return cont.rewards.OrderBy(x => x.PK_id).Last().PK_id + 1;
        }
        private static long GetNextCampaign2Key() {
            using DB_Context cont = new();
            if (!cont.campaign2s.Any()) {
                return 0;
            }
            return cont.campaign2s.OrderBy(x => x.PK_id).Last().PK_id + 1;
        }
        private static long GetNextDispatchKey() {
            using DB_Context cont = new();
            if (!cont.dispatches.Any()) {
                return 0;
            }
            return cont.dispatches.OrderBy(x => x.PK_id).Last().PK_id + 1;
        }
        private static long GetNexteventDataKey() {
            using DB_Context cont = new();
            if (!cont.eventDatas.Any()) {
                return 0;
            }
            return cont.eventDatas.OrderBy(x => x.PK_id).Last().PK_id + 1;
        }
        private static long GetNextStatsKey() {
            using DB_Context cont = new();
            if (!cont.statistics.Any()) {
                return 0;
            }
            return cont.statistics.OrderBy(x => x.PK_id).Last().PK_id + 1;
        }
        private static long GetNextPlanetKey() {
            using DB_Context cont = new();
            if(!cont.planets.Any()) {
                return 0;
            }
            return cont.planets.OrderBy(x=>x.PK_id).Last().PK_id + 1;
        }
        private static long GetNextpositionKey() {
            using DB_Context cont = new();
            if (!cont.xyPositions.Any()) {
                return 0;
            }
            return cont.xyPositions.OrderBy(x => x.PK_id).Last().PK_id + 1;
        }
        #endregion KeyValues

    }
}

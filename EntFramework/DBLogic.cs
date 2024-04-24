using HD2_EFDatabase.V1_Objects;
using Microsoft.EntityFrameworkCore;

namespace HD2_EFDatabase.EntFramework {
    internal static class DbLogic {
        /// <summary>
        /// Checks to see if migration has been applied and datatables exist
        /// </summary>
        /// <returns></returns>
        internal static bool DataTableCheck() {
            try {
                using DbContext cont = new();
                //Attempt to access each tables in someway without breaking due to no data
                _ = cont.assignmentDatas.Count();
                _ = cont.taskDatas.Count();
                _ = cont.rewards.Count();
                _ = cont.campaign2s.Count();
                _ = cont.dispatches.Count();
                _ = cont.eventDatas.Count();
                _ = cont.planets.Count();
                _ = cont.xyPositions.Count();
                _ = cont.statistics.Count();
                _ = cont.steamDatas.Count();
                _ = cont.xyPositions.Count();
                _ = cont.warInfos.Count();
            } catch {
                return false;
            }
            return true;
        }

        internal static bool GenerateDatabase() {
            try {
                using DbContext cont = new();
                cont.Database.Migrate();
            } catch {
                return false;
            }
            return true;
        }

        #region AddToDatabase

        internal static bool AddAssignmentData(assignmentData[] data) {
            using DbContext cont = new();
            if(cont.Database.CanConnect() is false) {
                return false;
            }

            long[] keys = [
                GetNextassignmentKey(),
                GetNextTaskDataKey(),
                GetNextRewardKey()
            ];

            bool ret = false;
            for (int i = 0; i < data.Length; i++) {
                assignmentData? databit = cont.assignmentDatas
                                           .Where(x => x.id == data[i].id)
                                           .OrderBy(x => x.PK_id)
                                           .LastOrDefault();
                if (databit != null) {
                    if (databit.Equals(data[i])) {
                        continue;
                    }
                }

                data[i].PK_id      = keys[0];
                data[i].FK_Task_ID = keys[1];

                taskData[] vals     = [.. data[i].tasks];
                long[]     keyVals = new long[data[i].tasks.Count];

                for (int x = 0; x < vals.Length; x++) {
                    vals[x].PK_id      = keys[1];
                    vals[x].FK_Task_ID = keys[0]; //Set the PK that this object originates from
                    keyVals[x]        = keys[1]; //Attempt at resolving many-many link break
                    keys[1]++;
                }

                data[i].tasks        = vals;
                data[i].reward.PK_id = keys[2];

                // Data[i].FK_Task_ID = key_vals;
                cont.assignmentDatas.Add(data[i]);
                ret = true;
                keys[0]++;
                //keys[1] is updated during assignment
                keys[2]++;
            }

            cont.SaveChanges();
            return ret;
        }

        internal static bool AddCampaign2(Campaign2[] data) {
            using DbContext cont = new();
            if(cont.Database.CanConnect() is false) {
                return false;
            }
            long[] keys = [
                GetNextCampaign2Key(),
                GetNextPlanetKey(),
                GetNextpositionKey(),
                GetNexteventDataKey(),
                GetNextStatsKey()
                ];

            bool ret = false;
            for (int i = 0; i < data.Length; i++) {
                Campaign2? databit = cont.campaign2s
                                         .Where(x => x.id==data[i].id)
                                         .OrderBy(x=>x.PK_id)
                                         .LastOrDefault();
                if (databit != null) {
                    if (databit.Equals(data[i])) {
                        continue;
                    }
                }
                data[i].PK_id = keys[0];
                data[i].planet.PK_id = keys[1];
                data[i].planet.position.PK_id= keys[2];

                if (data[i].planet.events != null) {
                    data[i].planet.events!.PK_id = keys[3];
                    keys[3]++;
                } else {
                    data[i].planet.FK_Events_ID = null;
                }
                if (data[i].planet.statistics != null) {
                    data[i].planet.statistics!.PK_id = keys[4];
                    keys[4]++;
                } else {
                    data[i].planet.FK_Stats_ID = null;
                }
                cont.campaign2s.Add(data[i]);
                ret = true;

                keys[0]++;
                keys[1]++;
                keys[2]++;
            }
            cont.SaveChanges();
            return ret;
        }
        internal static bool AddDispatch(Dispatch[] data) {
            using DbContext cont = new();
            if(cont.Database.CanConnect() is false) {
                return false;
            }
            long key = GetNextDispatchKey();
            bool ret = false;
            for (int i = 0; i < data.Length; i++) {
                Dispatch? databit = cont.dispatches
                                        .Where(x => x.id==data[i].id)
                                        .OrderBy(x=>x.PK_id)
                                        .LastOrDefault();
                if (databit != null) {
                    if (databit.Equals(data[i])) {
                        continue;
                    }
                }
                data[i].PK_id = key;
                cont.dispatches.Add(data[i]);
                ret = true;
                key++;
            }
            cont.SaveChanges();
            return ret;
        }
        internal static bool AddEventData(EventData[] data) {
            using DbContext cont = new();
            if(cont.Database.CanConnect() is false) {
                return false;
            }
            long key = GetNexteventDataKey();
            bool ret = false;
            for (int i = 0; i < data.Length; i++) {
                EventData? databit = cont.eventDatas
                                         .Where(x => x.id == data[i].id)
                                         .OrderBy(x=>x.PK_id)
                                         .LastOrDefault();
                if (databit != null) {
                    if (databit.Equals(data[i])) {
                        continue;
                    }
                }
                data[i].PK_id = key;
                cont.eventDatas.Add(data[i]);
                ret = true;
                key++;
            }
            cont.SaveChanges();
            return ret;
        }
        internal static bool AddPlanets(Planet[] data){
            using DbContext cont = new();
            if(cont.Database.CanConnect() is false) {
                return false;
            }
            long[] keys = [
                GetNextPlanetKey(),
                GetNextpositionKey(),
                GetNexteventDataKey(),
                GetNextStatsKey()
                ];

            bool ret = false;
            for (int i = 0; i < data.Length; i++) {
                //Grab the most upto date version of the planet
                Planet? databit = cont.planets
                                      .Where(x => x.index == data[i].index)
                                      .OrderBy(x=>x.PK_id)
                                      .LastOrDefault();
                if (databit != null) {
                    if (databit.Equals(data[i])) {
                        continue;
                    }
                }
                data[i].PK_id = keys[0];
                data[i].position.PK_id = keys[1];
                if (data[i].events != null) {
                    data[i].events!.PK_id = keys[2];
                    keys[2]++;
                } else {
                    data[i].FK_Events_ID = null;
                }
                if (data[i].statistics != null) {
                    data[i].statistics!.PK_id = keys[3];
                    keys[3]++;
                } else {
                    data[i].FK_Stats_ID = null;
                }
                cont.planets.Add(data[i]);

                keys[0]++;
                keys[1]++;
                //keys[2] is updated during assignment
                //keys[3] is updated during assignment
                ret = true;
            }
            cont.SaveChanges();
            return ret;
        }
        internal static bool AddsteamData(SteamData[] data) {
            using DbContext cont = new();
            if(cont.Database.CanConnect() is false) {
                return false;
            }
            long key = GetNextsteamDataKey();
            bool ret = false;
            for (int i = 0; i < data.Length; i++) {
                SteamData? databit = cont.steamDatas
                                         .Where(x => x.id == data[i].id)
                                         .OrderBy(x=>x.PK_id)
                                         .LastOrDefault();
                if (databit != null) {
                    if (data.Equals(data[i])) {
                        continue;
                    }
                }
                data[i].PK_id = key;
                cont.steamDatas.Add(data[i]);
                ret = true;
                key++;
            }
            cont.SaveChanges();
            return ret;
        }
        internal static bool AddWarInfo(WarInfo data) {
            using DbContext cont = new();
            if(cont.Database.CanConnect() is false) {
                return false;
            }
            long[] keys = [
                GetNextWarInfoKey(),
                GetNextStatsKey()
                ];

            data.PK_id = keys[0];
            
            if (data.statistics != null) {
                data.statistics.PK_id = keys[1];
                keys[1]++;
            } else {
                data.FK_Stats_ID = null;
            }

            cont.warInfos.Add(data);
            keys[0]++;

            cont.SaveChanges();
            return true;
        }
        #endregion AddToDatabase
        #region KeyValues
        private static long GetNextassignmentKey() {
            using DbContext cont = new();
            if(cont.Database.CanConnect() is false) {
                return -1;
            }
            if (!cont.assignmentDatas.Any()) {
                return 0;
            }
            return cont.assignmentDatas
                       .OrderBy(x => x.PK_id)
                       .Last()
                       .PK_id + 1;
        }
        private static long GetNextTaskDataKey() {
            using DbContext cont = new();
            if(cont.Database.CanConnect() is false) {
                return -1;
            }
            if (!cont.taskDatas.Any()) {
                return 0;
            }
            return cont.taskDatas
                       .OrderBy(x => x.PK_id)
                       .Last()
                       .PK_id + 1;
        }
        private static long GetNextRewardKey() {
            using DbContext cont = new();
            if(cont.Database.CanConnect() is false) {
                return -1;
            }
            if (!cont.rewards.Any()) {
                return 0;
            }
            return cont.rewards
                       .OrderBy(x => x.PK_id)
                       .Last()
                       .PK_id + 1;
        }
        private static long GetNextCampaign2Key() {
            using DbContext cont = new();
            if(cont.Database.CanConnect() is false) {
                return -1;
            }
            if (!cont.campaign2s.Any()) {
                return 0;
            }
            return cont.campaign2s
                       .OrderBy(x => x.PK_id)
                       .Last()
                       .PK_id + 1;
        }
        private static long GetNextDispatchKey() {
            using DbContext cont = new();
            if(cont.Database.CanConnect() is false) {
                return -1;
            }
            if (!cont.dispatches.Any()) {
                return 0;
            }
            return cont.dispatches
                       .OrderBy(x => x.PK_id)
                       .Last()
                       .PK_id + 1;
        }
        private static long GetNextsteamDataKey() {
            using DbContext cont = new();
            if(cont.Database.CanConnect() is false) {
                return -1;
            }
            if (!cont.steamDatas.Any()) {
                return 0;
            }
            return cont.steamDatas
                       .OrderBy(x => x.PK_id)
                       .Last()
                       .PK_id + 1;
        }
        private static long GetNexteventDataKey() {
            using DbContext cont = new();
            if(cont.Database.CanConnect() is false) {
                return -1;
            }
            if (!cont.eventDatas.Any()) {
                return 0;
            }
            return cont.eventDatas
                       .OrderBy(x => x.PK_id)
                       .Last()
                       .PK_id + 1;
        }
        private static long GetNextStatsKey() {
            using DbContext cont = new();
            if(cont.Database.CanConnect() is false) {
                return -1;
            }
            if (!cont.statistics.Any()) {
                return 0;
            }
            return cont.statistics
                       .OrderBy(x => x.PK_id)
                       .Last()
                       .PK_id + 1;
        }
        private static long GetNextPlanetKey() {
            using DbContext cont = new();
            if(cont.Database.CanConnect() is false) {
                return -1;
            }
            if (!cont.planets.Any()) {
                return 0;
            }
            return cont.planets
                       .OrderBy(x=>x.PK_id)
                       .Last()
                       .PK_id + 1;
        }
        private static long GetNextpositionKey() {
            using DbContext cont = new();
            if (cont.Database.CanConnect() is false) {
                return -1;
            }
            if (!cont.xyPositions.Any()) {
                return 0;
            }
            return cont.xyPositions
                       .OrderBy(x => x.PK_id)
                       .Last()
                       .PK_id + 1;
        }
        private static long GetNextWarInfoKey() {
            using DbContext cont = new();
            if(cont.Database.CanConnect() is false) {
                return -1;
            }
            if (!cont.warInfos.Any()) {
                return 0;
            }
            return cont.warInfos
                       .OrderBy(x => x.PK_id)
                       .Last()
                       .PK_id + 1;
        }
        #endregion KeyValues

        #region GetValues
        public static EventData? GetEvent(long pkId) {
            using DbContext cont = new();
            if(cont.Database.CanConnect() is false) {

            }
            return cont.eventDatas
                       .Where(x => x.PK_id == pkId)
                       .OrderBy(x => x.PK_id)
                       .LastOrDefault();
        }
        public static Planet? GetPlanet(long pkId) {
            using DbContext cont = new();
            if(cont.Database.CanConnect() is false) {

            }
            return cont.planets
                       .Where(x => x.PK_id == pkId)
                       .OrderBy(x => x.PK_id)
                       .LastOrDefault();
        }

        #endregion GetValues
    }
}

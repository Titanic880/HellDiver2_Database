using HellDiver2_API2DB.V1_Objects;

namespace HellDiver2_API2DB.EntFramework {
    internal static class DB_Logic {
        #region AddToDatabase
        public static bool AddAssignmentData(assignmentData[] Data) {
            using DB_Context cont = new();
            if (!cont.Database.CanConnect()) {
                return false;
            }
            long[] keys = [
                GetNextassignmentKey(),
                GetNextTaskDataKey(),
                GetNextRewardKey()
                ];

            bool ret = false;
            for (int i = 0; i < Data.Length; i++) {
                assignmentData? data = cont.assignmentDatas.Where(x => x.Equals(Data[i])).OrderBy(x=>x.PK_id).Last();
                if (data != null) {
                    if (data.Equals(Data[i])) {
                        continue;
                    }
                }

                Data[i].PK_id = keys[0];
                taskData[] vals = [.. Data[i].tasks];
                for (int x = 0; x < vals.Length; x++) {
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
            cont.SaveChanges();
            return ret;
        }
        public static bool AddCampaign2(Campaign2[] Data) {
            using DB_Context cont = new();
            if (!cont.Database.CanConnect()) {
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
            for (int i = 0; i < Data.Length; i++) {
                Campaign2? data = cont.campaign2s.Where(x => x.id==Data[i].id).FirstOrDefault();
                if(data != null) {
                    continue;
                }
                Data[i].PK_id = keys[0];
                Data[i].planet.PK_id = keys[1];
                Data[i].planet.position.PK_id= keys[2];

                if (Data[i].planet.events != null) {
                    Data[i].planet.events!.PK_id = keys[3];
                    keys[3]++;
                } else {
                    Data[i].planet.FK_Events_ID = null;
                }
                if (Data[i].planet.statistics != null) {
                    Data[i].planet.statistics!.PK_id = keys[4];
                    keys[4]++;
                } else {
                    Data[i].planet.FK_Stats_ID = null;
                }
                cont.campaign2s.Add(Data[i]);
                ret = true;

                keys[0]++;
                keys[1]++;
                keys[2]++;
            }
            cont.SaveChanges();
            return ret;
        }
        public static bool AddDispatch(Dispatch[] Data) {
            using DB_Context cont = new();
            if (!cont.Database.CanConnect()) {
                return false;
            }
            long Key = GetNextDispatchKey();
            bool ret = false;
            for (int i = 0; i < Data.Length; i++) {
                Dispatch? data = cont.dispatches.Where(x => x.id==Data[i].id).OrderBy(x=>x.PK_id).Last();
                if (data != null) {
                    continue;
                }
                Data[i].PK_id = Key;
                cont.dispatches.Add(Data[i]);
                ret = true;
                Key++;
            }
            cont.SaveChanges();
            return ret;
        }
        public static bool AddEventData(eventData[] Data) {
            using DB_Context cont = new();
            if (!cont.Database.CanConnect()) {
                return false;
            }
            long Key = GetNexteventDataKey();
            bool ret = false;
            for (int i = 0; i < Data.Length; i++) {
                eventData? data = cont.eventDatas.Where(x => x.id == Data[i].id).OrderBy(x=>x.PK_id).Last();
                if (data != null) {
                    if (data.Equals(Data[i])) {
                        continue;
                    }
                }
                Data[i].PK_id = Key;
                cont.eventDatas.Add(Data[i]);
                ret = true;
                Key++;
            }
            cont.SaveChanges();
            return ret;
        }
        public static bool AddPlanets(Planet[] Data){
            using DB_Context cont = new();
            if (!cont.Database.CanConnect()) {
                return false;
            }
            long[] keys = [
                GetNextPlanetKey(),
                GetNextpositionKey(),
                GetNexteventDataKey(),
                GetNextStatsKey()
                ];

            bool ret = false;
            for (int i = 0; i < Data.Length; i++) {
                //Grab the most upto date version of the planet
                Planet? data = cont.planets.Where(x => x.index == Data[i].index).OrderBy(x=>x.PK_id).Last();
                if (data != null) {
                    if (data.Equals(Data[i])) {
                        continue;
                    }
                }
                Data[i].PK_id = keys[0];
                Data[i].position.PK_id = keys[1];
                if (Data[i].events != null) {
                    Data[i].events!.PK_id = keys[2];
                    keys[2]++;
                } else {
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
            cont.SaveChanges();
            return ret;
        }
        public static bool AddsteamData(steamData[] Data) {
            using DB_Context cont = new();
            if (!cont.Database.CanConnect()) {
                return false;
            }
            long Key = GetNextsteamDataKey();
            bool ret = false;
            for (int i = 0; i < Data.Length; i++) {
                steamData? data = cont.steamDatas.Where(x => x.id == Data[i].id).OrderBy(x=>x.PK_id).Last();
                if (data != null) {
                    if (data.Equals(Data[i])) {
                        continue;
                    }
                }
                Data[i].PK_id = Key;
                cont.steamDatas.Add(Data[i]);
                ret = true;
                Key++;
            }
            cont.SaveChanges();
            return ret;
        }
        public static bool AddWarInfo(WarInfo Data) {
            using DB_Context cont = new();
            if (!cont.Database.CanConnect()) {
                return false;
            }
            long[] Keys = [
                GetNextWarInfoKey(),
                GetNextStatsKey()
                ];

            bool ret = false;
            Data.PK_id = Keys[0];
            if (Data.statistics != null) {
                Data.statistics!.PK_id = Keys[1];
                Keys[1]++;
            } else {
                Data.FK_Stats_ID = null;
            }

            cont.warInfos.Add(Data);
            ret = true;
            Keys[0]++;

            cont.SaveChanges();
            return ret;
        }
        #endregion AddToDatabase
        #region UpdateDatabase
        public static void UpdatePlanet(long toUpdateID,Planet NewPlanet) {
            using DB_Context cont = new();
            if (!cont.Database.CanConnect()) {
                return;
            }
            NewPlanet.PK_id = toUpdateID;
            Planet update = cont.planets.Where(x=>x.PK_id == toUpdateID).First();
            update = NewPlanet;
            cont.SaveChanges();
        }
        #endregion UpdateDatabase
        #region KeyValues
        private static long GetNextassignmentKey() {
            using DB_Context cont = new();
            if (!cont.Database.CanConnect()) {
                return -1;
            }
            if (!cont.assignmentDatas.Any()) {
                return 0;
            }
            return cont.assignmentDatas.OrderBy(x => x.PK_id).Last().PK_id + 1;
        }
        private static long GetNextTaskDataKey() {
            using DB_Context cont = new();
            if (!cont.Database.CanConnect()) {
                return -1;
            }
            if (!cont.taskDatas.Any()) {
                return 0;
            }
            return cont.taskDatas.OrderBy(x => x.PK_id).Last().PK_id + 1;
        }
        private static long GetNextRewardKey() {
            using DB_Context cont = new();
            if (!cont.Database.CanConnect()) {
                return -1;
            }
            if (!cont.rewards.Any()) {
                return 0;
            }
            return cont.rewards.OrderBy(x => x.PK_id).Last().PK_id + 1;
        }
        private static long GetNextCampaign2Key() {
            using DB_Context cont = new();
            if (!cont.Database.CanConnect()) {
                return -1;
            }
            if (!cont.campaign2s.Any()) {
                return 0;
            }
            return cont.campaign2s.OrderBy(x => x.PK_id).Last().PK_id + 1;
        }
        private static long GetNextDispatchKey() {
            using DB_Context cont = new();
            if (!cont.Database.CanConnect()) {
                return -1;
            }
            if (!cont.dispatches.Any()) {
                return 0;
            }
            return cont.dispatches.OrderBy(x => x.PK_id).Last().PK_id + 1;
        }
        private static long GetNextsteamDataKey() {
            using DB_Context cont = new();
            if (!cont.Database.CanConnect()) {
                return -1;
            }
            if (!cont.steamDatas.Any()) {
                return 0;
            }
            return cont.steamDatas.OrderBy(x => x.PK_id).Last().PK_id + 1;
        }
        private static long GetNexteventDataKey() {
            using DB_Context cont = new();
            if (!cont.Database.CanConnect()) {
                return -1;
            }
            if (!cont.eventDatas.Any()) {
                return 0;
            }
            return cont.eventDatas.OrderBy(x => x.PK_id).Last().PK_id + 1;
        }
        private static long GetNextStatsKey() {
            using DB_Context cont = new();
            if (!cont.Database.CanConnect()) {
                return -1;
            }
            if (!cont.statistics.Any()) {
                return 0;
            }
            return cont.statistics.OrderBy(x => x.PK_id).Last().PK_id + 1;
        }
        private static long GetNextPlanetKey() {
            using DB_Context cont = new();
            if (!cont.Database.CanConnect()) {
                return -1;
            }
            if (!cont.planets.Any()) {
                return 0;
            }
            return cont.planets.OrderBy(x=>x.PK_id).Last().PK_id + 1;
        }
        private static long GetNextpositionKey() {
            using DB_Context cont = new();
            if (!cont.Database.CanConnect()) {
                return -1;
            }
            if (!cont.xyPositions.Any()) {
                return 0;
            }
            return cont.xyPositions.OrderBy(x => x.PK_id).Last().PK_id + 1;
        }
        private static long GetNextWarInfoKey() {
            using DB_Context cont = new();
            if (!cont.Database.CanConnect()) {
                return -1;
            }
            if (!cont.warInfos.Any()) {
                return 0;
            }
            return cont.warInfos.OrderBy(x => x.PK_id).Last().PK_id + 1;
        }
        #endregion KeyValues

    }
}

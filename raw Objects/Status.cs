using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HellDiver2_API2DB.raw_Objects {
    internal class Status {
        public int warId { get; set; }
        public Int64 time { get; set; }
        public double impactMultiplier { get; set; }
        public Int64 storyBeatId32 { get; set; }
        //TODO: implement previously used conversions
        [NotMapped]
        public required PlanetStatus[] planetStatus { get; set; }
        [NotMapped]
        public required planetAttacks[] planetAttacks { get; set; }
        [NotMapped]
        public required Campaign[] campaigns { get; set; }
        [NotMapped]
        public required JointOperation[] jointOperations { get; set; }
        [NotMapped]
        public required V1_Objects.eventData[] planetEvents { get; set; }



        private string MakeDBValue<T>(T[] values) {
            if(values == null) {
                return "";
            }else if(values.Length == 0) {
                return "";
            }
            string ret = "";

            for(int i = 0; i < values.Length; i++) {
                switch (values[i]) {
                case PlanetStatus val:
                    ret += val.index;
                    break;
                case planetAttacks val:
                    ret += val.id;
                    break;
                case Campaign val:
                    ret += val.id;
                    break;
                case JointOperation val:
                    ret += val.id;
                    break;
                case V1_Objects.eventData val:
                    ret += val.id;
                    break;
                default:
                    continue;
                }
                ret += ",";
            }
            return ret.TrimEnd(',');
        }
        private T[] GetDBValues<T>(string input) {
            string[] vals = [input];
            if (input == "") {
                return [];
            } else if (input.Contains(',')) {
                vals = input.Split(',');
            }
            int[] valint = new int[vals.Length];
            for (int i = 0; i < vals.Length; i++) {
                valint[i] = int.Parse(vals[i]);
            }
            return EntFramework.DB_Logic.GetDatabaseItems<T>(valint);
        }


        public const bool CanIndex = false;
        public const string apiEndpoint = "/raw/api/WarSeason/{WARID}/Status";
    }

    internal class PlanetStatus {
        [Key]
        public required int index { get; set; }
        public required int owner { get; set; }
        public required Int64 health { get; set; }
        public required double regenPerSecond { get; set; }
        public required Int64 players { get; set; }
    }
    internal class planetAttacks {
        [Key]
        public int id { get; set; }
    }
    internal class JointOperation {
        [Key]
        public required int id { get; set; }
        public required int planetIndex { get; set; }
        public required int hqNodeIndex { get; set; }
    }
}

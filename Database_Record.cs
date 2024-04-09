using HellDiver2_API2DB.raw_Objects;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HellDiver2_API2DB {
    public abstract class Database_Record {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long PK_id { get; set; }
        public DateTime DataEntryTimeUTC { get; set; } = DateTime.UtcNow;
        /// <summary>
        /// Convert item array into Database friendly comma seperated list of ids
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="values"></param>
        /// <returns></returns>
        protected string MakeDBValue<T>(T[] values) {
            if (values == null) {
                return "";
            } else if (values.Length == 0) {
                return "";
            }
            string ret = "";

            for (int i = 0; i < values.Length; i++) {
                switch (values[i]) {
                case Database_Record val:
                    ret += val.PK_id;
                    break;
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
                default:
                    continue;
                }
                ret += ",";
            }
            return ret.TrimEnd(',');
        }
        /// <summary>
        /// returns database result of provided id array
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input"></param>
        /// <returns></returns>
        protected T[] GetDBValues<T>(string input) {
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
    }
}

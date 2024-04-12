using HellDiver2_API2DB.EntFramework;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations.Schema;

namespace HellDiver2_API2DB.V1_Objects {
    internal class Campaign2 : Database_Record {
        public required int id { get; set; }                    //Provided by API
        [ForeignKey("FK_Planet_ID")]
        public required Planet planet { get; set; }
        public long FK_Planet_ID { get; set; }
        public required int type { get; set; }
        public required int count { get; set; }

        public const bool CanIndex = true;
        public const string apiEndpoint = "/api/v1/campaigns";

        public override bool Equals(object? obj) {
            if(obj is not Campaign2 data) {
                return false;
            }
            //Ensures we do not change the state of the objects
            Planet? e1 = planet;
            Planet? e2 = data.planet;
            if (e1 == null && FK_Planet_ID != default) {
                e1 = DB_Logic.GetPlanet(FK_Planet_ID);
            }
            if(e2 == null && data.FK_Planet_ID != default) {
                e2 = DB_Logic.GetPlanet(data.FK_Planet_ID);
            }
            if(
               id == data.id
            && e1!.index == e2!.index
            && type == data.type
            && count == data.count
            ) {
                return true;
            }
            return false;
        }
        public override int GetHashCode() {
            return base.GetHashCode();
        }
    }
}

using System.ComponentModel.DataAnnotations.Schema;

namespace HellDiver2_API2DB.V1_Objects {
    internal class Campaign2 : Database_Record {
        public required int id { get; set; }                    //Provided by API
        [ForeignKey("FK_Planet_ID")]
        public required Planet campaignPlanet { get; set; }
        public long FK_Planet_ID { get; set; }
        public required int type { get; set; }
        public required int count { get; set; }

        public const bool CanIndex = true;
        public const string apiEndpoint = "/api/v1/campaigns";

        public override bool Equals(object? obj) {
            if(obj is not Campaign2 data) {
                return false;
            }
            if(
                id == data.id
            &&  campaignPlanet == data.campaignPlanet
            &&  FK_Planet_ID == data.FK_Planet_ID
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

using System.ComponentModel.DataAnnotations.Schema;
using HD2_EFDatabase.EntFramework;

namespace HD2_EFDatabase.V1_Objects {
    public class Campaign2 : DatabaseRecord {
        public required int id { get; set; }                    //Provided by API
        [ForeignKey("FK_Planet_ID")]
        public required Planet planet { get; set; }
        public long FK_Planet_ID { get; set; }
        public required int type { get; set; }
        public required int count { get; set; }

        public const bool CanIndex = true;
        public const string ApiEndpoint = "/api/v1/campaigns";

        public override bool Equals(object? obj) {
            if(obj is not Campaign2 data) {
                return false;
            }
            //Ensures we do not change the state of the objects
            Planet? e1 = planet;
            Planet? e2 = data.planet;
            if (e1 == null && FK_Planet_ID != default) {
                e1 = DbLogic.GetPlanet(FK_Planet_ID);
            }
            if(e2 == null && data.FK_Planet_ID != default) {
                e2 = DbLogic.GetPlanet(data.FK_Planet_ID);
            }
            return id        == data.id
                && e1!.index == e2!.index
                && type      == data.type
                && count     == data.count;
        }
        public override int GetHashCode() {
            return base.GetHashCode();
        }
    }
}

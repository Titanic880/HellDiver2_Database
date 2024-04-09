using HellDiver2_API2DB.EntFramework;
using System.ComponentModel.DataAnnotations.Schema;

namespace HellDiver2_API2DB.V1_Objects {
    internal class assignmentData : Database_Record {
        public required Int64 id { get; set; }            //Provided by API
        public required string title { get; set; }
        public required string briefing { get; set; }
        public required string description { get; set; }
        [ForeignKey("FK_Task_ID")]
        public required ICollection<taskData> tasks { get; set; }
        public long FK_Task_ID { get; set; }

        [ForeignKey("FK_Reward_ID")]
        public required Reward reward { get; set; }
        public long FK_Reward_ID { get; set; }

        public const bool CanIndex = true;
        public const string apiEndpoint = "/api/v1/assignments";

        public override bool Equals(object? obj) {
            if(obj is not assignmentData data) {
                return false;
            }
            if (
               id == data.id
            && title == data.title
            && briefing == data.briefing
            && description == data.description
            ) {
                return true;
            }
            return false;
        }

        public override int GetHashCode() {
            return base.GetHashCode();
        }
    }

    internal class taskData : Database_Record {
        public required int type {  get; set; }
        public required int[] values {  get; set; }
        public required int[] valueTypes {  get; set; }
    }
    internal class Reward : Database_Record {
        public required int type { get; set; }          //1 => Medals
        public required int amount { get; set; }
    }
}

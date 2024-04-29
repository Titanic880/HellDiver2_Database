using System.ComponentModel.DataAnnotations.Schema;

namespace HD2_EFDatabase.V1_Objects {
    public class assignmentData : DatabaseRecord {
        public required long id { get; set; }            //Provided by API
        public int[] progress { get; set; } = [];
        public required string title { get; set; }
        public required string briefing { get; set; }
        public required string description { get; set; }
        [ForeignKey("FK_Task_ID")]
        public required ICollection<taskData> tasks { get; set; }
        public long FK_Task_ID { get; set; }

        [ForeignKey("FK_Reward_ID")]
        public required Reward reward { get; set; }
        public long FK_Reward_ID { get; set; }
        public DateTime expiration { get; set; }

        public const bool CanIndex = true;
        public const string apiEndpoint = "/api/v1/assignments";

        public override bool Equals(object? obj) {
            if(obj is not assignmentData data) {
                return false;
            }
            return id    == data.id
                && title == data.title
                && progress.SequenceEqual(data.progress);
        }

        public override int GetHashCode() {
            return base.GetHashCode();
        }
    }


    public class taskData : DatabaseRecord {
        /// <summary>
        /// PK of the parent object
        /// </summary>
        public long FK_Task_ID { get; set; }
        
        public required int type {  get; set; }
        public required int[] values {  get; set; }
        public required int[] valueTypes {  get; set; }

        /// <summary>
        /// Example method of how to access values properly
        /// </summary>
        /// <param name="valueType"></param>
        /// <returns></returns>
        private int GetValueByType(int valueType) {
            for (int i = 0; i < valueTypes.Length; i++) {
                if (valueTypes[i] == valueType) {
                    return values[i];
                }
            }
            return -1;
        }
        //Still learning about what the values actually are
        private enum TaskValueEn {
            Race = 1,
            IDFK1 = 2,
            GoalValue = 3,
            IDFK2 = 4,
            IDFK3 = 5,
            IDFK4 = 8,
            IDFK5 = 9,
            MajorOrder = 11,
            PlanetIndex = 12
        }
        
        /// <summary>
        /// type values
        /// </summary>
        private enum MajorOrderTypes {
            Eradicate = 3,
            Liberation = 11,
            Defense = 12,
            Control = 13
        }
}
    public class Reward : DatabaseRecord {
        public required int type { get; set; }

        private enum RewardType {
            Medals = 1
        }
        public required int amount { get; set; }
    }
}

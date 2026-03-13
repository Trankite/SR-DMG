using System.Collections.Immutable;
using System.Text.Json.Serialization;

namespace StarRailDamage.Source.Web.Hoyolab.Takumi.Note
{
    public class NoteResponseWrapper
    {
        [JsonPropertyName("current_stamina")]
        public int CurrentStamina { get; set; }

        [JsonPropertyName("max_stamina")]
        public int MaxStamina { get; set; }

        [JsonPropertyName("stamina_recover_time")]
        public int StaminaRecoverTime { get; set; }

        [JsonPropertyName("stamina_full_ts")]
        public int StaminaFullTs { get; set; }

        [JsonPropertyName("accepted_epedition_num")]
        public int AcceptedEpeditionNum { get; set; }

        [JsonPropertyName("total_expedition_num")]
        public int TotalExpeditionNum { get; set; }

        [JsonPropertyName("expeditions")]
        public ImmutableArray<NoteResponseExpedition> Expeditions { get; set; }

        [JsonPropertyName("current_train_score")]
        public int CurrentTrainScore { get; set; }

        [JsonPropertyName("max_train_score")]
        public int MaxTrainScore { get; set; }

        [JsonPropertyName("current_rogue_score")]
        public int CurrentRogueScore { get; set; }

        [JsonPropertyName("max_rogue_score")]
        public int MaxRogueScore { get; set; }

        [JsonPropertyName("weekly_cocoon_cnt")]
        public int WeeklyCocoonCnt { get; set; }

        [JsonPropertyName("weekly_cocoon_limit")]
        public int WeeklyCocoonLimit { get; set; }

        [JsonPropertyName("current_reserve_stamina")]
        public int CurrentReserveStamina { get; set; }

        [JsonPropertyName("is_reserve_stamina_full")]
        public bool IsReserveStaminaFull { get; set; }

        [JsonPropertyName("rogue_tourn_weekly_unlocked")]
        public bool RogueTournWeeklyUnlocked { get; set; }

        [JsonPropertyName("rogue_tourn_weekly_max")]
        public int RogueTournWeeklyMax { get; set; }

        [JsonPropertyName("rogue_tourn_weekly_cur")]
        public int RogueTournWeeklyCur { get; set; }

        [JsonPropertyName("current_ts")]
        public int CurrentTs { get; set; }

        [JsonPropertyName("rogue_tourn_exp_is_full")]
        public bool RogueTournExpIsFull { get; set; }

        [JsonPropertyName("grid_fight_weekly_cur")]
        public int GridFightWeeklyCur { get; set; }

        [JsonPropertyName("grid_fight_weekly_max")]
        public int GridFightWeeklyMax { get; set; }
    }
}
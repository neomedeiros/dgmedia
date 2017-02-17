using dgmedia.Util.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dgmedia.Entities.Enums
{
    public enum ActionType
    {
        [Description("Watch Video")]
        WatchVideo = 1,

        [Description("Play Game")]
        PlayGame = 2,

        [Description("Facebook Redemption Bonus")]
        FacebookRedemptionBonus = 3,

        [Description("Complete Profile")]
        CompleteProfile = 4,

        [Description("Complete Tutorial")]
        CompleteTutorial = 5,

        [Description("Fill Out Survey")]
        FillOutSurvey = 6,

        [Description("Registration Bonus")]
        RegistrationBonus = 7,

        [Description("Survey Bonus")]
        SurveyBonus = 8,

        [Description("Survey Consolation")]
        SurveyConsolation = 9,

        [Description("Admin Ajustment")]
        AdminAdjustment = 10,

        [Description("Cygnus Watch Video")]
        CygnusWatchVideo = 11,

        [Description("Outbound Traffic")]
        OutboundTraffic = 12,

        [Description("Daily Login Bonus")]
        DailyLoginBonus = 13,

        [Description("Complete Badge")]
        CompleteBadge = 14,

        [Description("Referral")]
        Referral = 15,

        [Description("Redeem")]
        Redeem = 16,

        [Description("Honey Code")]
        HoneyCode = 17,

        [Description("Special Offer")]
        SpecialOffer = 18,

        [Description("Promotional Bonus")]
        PromotionalBonus = 19,

        [Description("Testimony")]
        Testimony = 20,

        [Description("EHC Mobile App")]
        EHCMobileApp = 21,

        [Description("TVG Roku App")]
        TVGRokuApp = 22,

        [Description("SS2 Mobile App")]
        SS2MobileApp = 23
    }
}

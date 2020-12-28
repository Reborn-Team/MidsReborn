using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using mrbBase.Base.Master_Classes;

namespace mrbBase.Base.Data_Classes
{
    public class Effect : IEffect, IComparable, ICloneable
    {
        private static readonly Regex UidClassRegex =
            new Regex("arch source(.owner)?> (Class_[^ ]*)", RegexOptions.IgnoreCase);

        private IPower power;

        public Effect()

        {
            BaseProbability = 1f;
            MagnitudeExpression = string.Empty;
            Reward = string.Empty;
            EffectClass = Enums.eEffectClass.Primary;
            EffectType = Enums.eEffectType.None;
            DisplayPercentageOverride = Enums.eOverrideBoolean.NoOverride;
            DamageType = Enums.eDamage.None;
            MezType = Enums.eMez.None;
            ETModifies = Enums.eEffectType.None;
            PowerAttribs = Enums.ePowerAttribs.None;
            Summon = string.Empty;
            Stacking = Enums.eStacking.No;
            Suppression = Enums.eSuppress.None;
            Buffable = true;
            Resistible = true;
            SpecialCase = Enums.eSpecialCase.None;
            UIDClassName = string.Empty;
            nIDClassName = -1;
            PvMode = Enums.ePvX.Any;
            ToWho = Enums.eToWho.Unspecified;
            AttribType = Enums.eAttribType.Magnitude;
            Aspect = Enums.eAspect.Str;
            ModifierTable = "Melee_Ones";
            PowerFullName = string.Empty;
            Absorbed_PowerType = Enums.ePowerType.Auto_;
            Absorbed_Power_nID = -1;
            Absorbed_Class_nID = -1;
            Absorbed_EffectID = -1;
            Override = string.Empty;
            buffMode = Enums.eBuffMode.Normal;
            Special = string.Empty;
            EffectId = "Ones";
            PowerAttribs = Enums.ePowerAttribs.None;
            AtrOrigAccuracy = -1;
            AtrOrigActivatePeriod = -1;
            AtrOrigArc = -1;
            AtrOrigCastTime = -1;
            AtrOrigEffectArea = Enums.eEffectArea.None;
            AtrOrigEnduranceCost = -1;
            AtrOrigInterruptTime = -1;
            AtrOrigMaxTargets = -1;
            AtrOrigRadius = -1;
            AtrOrigRange = -1;
            AtrOrigRechargeTime = -1;
            AtrOrigSecondaryRange = -1;
            ActiveConditionals = new List<KeyValue<string, string>>();
            AtrModAccuracy = -1;
            AtrModActivatePeriod = -1;
            AtrModArc = -1;
            AtrModCastTime = -1;
            AtrModEffectArea = Enums.eEffectArea.None;
            AtrModEnduranceCost = -1;
            AtrModInterruptTime = -1;
            AtrModMaxTargets = -1;
            AtrModRadius = -1;
            AtrModRange = -1;
            AtrModRechargeTime = -1;
            AtrModSecondaryRange = -1;
        }

        public Effect(IPower power)
            : this()
        {
            this.power = power;
        }

        public Effect(BinaryReader reader)
            : this()
        {
            PowerFullName = reader.ReadString();
            UniqueID = reader.ReadInt32();
            EffectClass = (Enums.eEffectClass)reader.ReadInt32();
            EffectType = (Enums.eEffectType)reader.ReadInt32();
            DamageType = (Enums.eDamage)reader.ReadInt32();
            MezType = (Enums.eMez)reader.ReadInt32();
            ETModifies = (Enums.eEffectType)reader.ReadInt32();
            Summon = reader.ReadString();
            DelayedTime = reader.ReadSingle();
            Ticks = reader.ReadInt32();
            Stacking = (Enums.eStacking)reader.ReadInt32();
            BaseProbability = reader.ReadSingle();
            Suppression = (Enums.eSuppress)reader.ReadInt32();
            Buffable = reader.ReadBoolean();
            Resistible = reader.ReadBoolean();
            SpecialCase = (Enums.eSpecialCase)reader.ReadInt32();
            VariableModifiedOverride = reader.ReadBoolean();
            PvMode = (Enums.ePvX)reader.ReadInt32();
            ToWho = (Enums.eToWho)reader.ReadInt32();
            DisplayPercentageOverride = (Enums.eOverrideBoolean)reader.ReadInt32();
            Scale = reader.ReadSingle();
            nMagnitude = reader.ReadSingle();
            nDuration = reader.ReadSingle();
            AttribType = (Enums.eAttribType)reader.ReadInt32();
            Aspect = (Enums.eAspect)reader.ReadInt32();
            ModifierTable = reader.ReadString();
            nModifierTable = DatabaseAPI.NidFromUidAttribMod(ModifierTable);
            NearGround = reader.ReadBoolean();
            CancelOnMiss = reader.ReadBoolean();
            RequiresToHitCheck = reader.ReadBoolean();
            UIDClassName = reader.ReadString();
            nIDClassName = reader.ReadInt32();
            MagnitudeExpression = reader.ReadString();
            Reward = reader.ReadString();
            EffectId = reader.ReadString();
            IgnoreED = reader.ReadBoolean();
            Override = reader.ReadString();
            ProcsPerMinute = reader.ReadSingle();

            PowerAttribs = (Enums.ePowerAttribs)reader.ReadInt32();
            AtrOrigAccuracy = reader.ReadSingle();
            AtrOrigActivatePeriod = reader.ReadSingle();
            AtrOrigArc = reader.ReadInt32();
            AtrOrigCastTime = reader.ReadSingle();
            AtrOrigEffectArea = (Enums.eEffectArea)reader.ReadInt32();
            AtrOrigEnduranceCost = reader.ReadSingle();
            AtrOrigInterruptTime = reader.ReadSingle();
            AtrOrigMaxTargets = reader.ReadInt32();
            AtrOrigRadius = reader.ReadSingle();
            AtrOrigRange = reader.ReadSingle();
            AtrOrigRechargeTime = reader.ReadSingle();
            AtrOrigSecondaryRange = reader.ReadSingle();

            AtrModAccuracy = reader.ReadSingle();
            AtrModActivatePeriod = reader.ReadSingle();
            AtrModArc = reader.ReadInt32();
            AtrModCastTime = reader.ReadSingle();
            AtrModEffectArea = (Enums.eEffectArea)reader.ReadInt32();
            AtrModEnduranceCost = reader.ReadSingle();
            AtrModInterruptTime = reader.ReadSingle();
            AtrModMaxTargets = reader.ReadInt32();
            AtrModRadius = reader.ReadSingle();
            AtrModRange = reader.ReadSingle();
            AtrModRechargeTime = reader.ReadSingle();
            AtrModSecondaryRange = reader.ReadSingle();

            var conditionalCount = reader.ReadInt32();
            for (var cIndex = 0; cIndex < conditionalCount; cIndex++)
            {
                var cKey = reader.ReadString();
                var cValue = reader.ReadString();
                ActiveConditionals.Add(new KeyValue<string, string>(cKey, cValue));
            }

            /*if (DatabaseAPI.Database.EffectIds.Contains(EffectId))
                return;
            DatabaseAPI.Database.EffectIds.Add(EffectId)*/
            ;
        }

        private Effect(IEffect template)
            : this()
        {
            PowerFullName = template.PowerFullName;
            power = template.GetPower();
            Enhancement = template.Enhancement;
            UniqueID = template.UniqueID;
            EffectClass = template.EffectClass;
            EffectType = template.EffectType;
            DisplayPercentageOverride = template.DisplayPercentageOverride;
            DamageType = template.DamageType;
            MezType = template.MezType;
            ETModifies = template.ETModifies;
            Summon = template.Summon;
            Ticks = template.Ticks;
            DelayedTime = template.DelayedTime;
            Stacking = template.Stacking;
            BaseProbability = template.BaseProbability;
            Suppression = template.Suppression;
            Buffable = template.Buffable;
            Resistible = template.Resistible;
            SpecialCase = template.SpecialCase;
            VariableModifiedOverride = template.VariableModifiedOverride;
            isEnhancementEffect = template.isEnhancementEffect;
            PvMode = template.PvMode;
            ToWho = template.ToWho;
            Scale = template.Scale;
            nMagnitude = template.nMagnitude;
            nDuration = template.nDuration;
            AttribType = template.AttribType;
            Aspect = template.Aspect;
            ModifierTable = template.ModifierTable;
            nModifierTable = template.nModifierTable;
            NearGround = template.NearGround;
            CancelOnMiss = template.CancelOnMiss;
            ProcsPerMinute = template.ProcsPerMinute;
            Absorbed_Duration = template.Absorbed_Duration;
            Absorbed_Effect = template.Absorbed_Effect;
            Absorbed_PowerType = template.Absorbed_PowerType;
            Absorbed_Class_nID = template.Absorbed_Class_nID;
            Absorbed_Interval = template.Absorbed_Interval;
            Absorbed_EffectID = template.Absorbed_EffectID;
            buffMode = template.buffMode;
            Math_Duration = template.Math_Duration;
            Math_Mag = template.Math_Mag;
            RequiresToHitCheck = template.RequiresToHitCheck;
            UIDClassName = template.UIDClassName;
            nIDClassName = template.nIDClassName;
            MagnitudeExpression = template.MagnitudeExpression;
            Reward = template.Reward;
            EffectId = template.EffectId;
            IgnoreED = template.IgnoreED;
            Override = template.Override;
            PowerAttribs = template.PowerAttribs;
            AtrOrigAccuracy = template.AtrOrigAccuracy;
            AtrOrigActivatePeriod = template.AtrOrigActivatePeriod;
            AtrOrigArc = template.AtrOrigArc;
            AtrOrigCastTime = template.AtrOrigCastTime;
            AtrOrigEffectArea = template.AtrOrigEffectArea;
            AtrOrigEnduranceCost = template.AtrOrigEnduranceCost;
            AtrOrigInterruptTime = template.AtrOrigInterruptTime;
            AtrOrigMaxTargets = template.AtrOrigMaxTargets;
            AtrOrigRadius = template.AtrOrigRadius;
            AtrOrigRange = template.AtrOrigRange;
            AtrOrigRechargeTime = template.AtrOrigRechargeTime;
            AtrOrigSecondaryRange = template.AtrOrigSecondaryRange;

            AtrModAccuracy = template.AtrModAccuracy;
            AtrModActivatePeriod = template.AtrModActivatePeriod;
            AtrModArc = template.AtrModArc;
            AtrModCastTime = template.AtrModCastTime;
            AtrModEffectArea = template.AtrModEffectArea;
            AtrModEnduranceCost = template.AtrModEnduranceCost;
            AtrModInterruptTime = template.AtrModInterruptTime;
            AtrModMaxTargets = template.AtrModMaxTargets;
            AtrModRadius = template.AtrModRadius;
            AtrModRange = template.AtrModRange;
            AtrModRechargeTime = template.AtrModRechargeTime;
            AtrModSecondaryRange = template.AtrModSecondaryRange;
            ActiveConditionals = template.ActiveConditionals;
        }

        private int? SummonId { get; set; }

        private int? OverrideId { get; set; }

        public string MagnitudeExpression { get; set; }

        public float ProcsPerMinute { get; set; }

        public bool CancelOnMiss { get; set; }

        public float Probability
        {
            get
            {
                var num1 = BaseProbability;
                if (ProcsPerMinute > 0.0 && num1 < 0.01 && power != null)
                {
                    var num2 = (float)(power.AoEModifier * 0.75 + 0.25);
                    var procsPerMinute = ProcsPerMinute;
                    var Global_Recharge = (MidsContext.Character.DisplayStats.BuffHaste(false) - 100) / 100;
                    var rechargeval = power.BaseRechargeTime /
                                      (power.BaseRechargeTime / power.RechargeTime - Global_Recharge);
                    if (power.PowerType == Enums.ePowerType.Click)
                        num1 = Math.Min(
                            Math.Max(procsPerMinute * (rechargeval + power.CastTimeReal) / (60f * num2),
                                (float)(0.0500000007450581 + 0.0149999996647239 * ProcsPerMinute)), 0.9f);
                    else
                        num1 = Math.Min(
                            Math.Max(procsPerMinute * 10 / (60f * num2),
                                (float)(0.0500000007450581 + 0.0149999996647239 * ProcsPerMinute)), 0.9f);
                }

                //num1 = Math.Min(Math.Max((power.PowerType != Enums.ePowerType.Click ? procsPerMinute * 10 : procsPerMinute * (rechargeval + power.CastTimeReal)) / (60f * num2), (float)(0.0500000007450581 + 0.0149999996647239 * ProcsPerMinute)), 0.9f);
                if (MidsContext.Character != null && !string.IsNullOrEmpty(EffectId) &&
                    MidsContext.Character.ModifyEffects.ContainsKey(EffectId))
                    num1 += MidsContext.Character.ModifyEffects[EffectId];
                if (num1 > 1.0)
                    num1 = 1f;
                if (num1 < 0.0)
                    num1 = 0.0f;
                return num1;
            }
            set => BaseProbability = value;
        }

        public float Mag
        {
            get
            {
                var num1 = 0.0f;
                switch (AttribType)
                {
                    case Enums.eAttribType.Magnitude:
                        if (Math.Abs(Math_Mag - 0.0f) > 0.01)
                            return Math_Mag;
                        var num2 = nMagnitude;
                        if (EffectType == Enums.eEffectType.Damage)
                            num2 = -num2;
                        num1 = Scale * num2 * DatabaseAPI.GetModifier(this);
                        break;
                    case Enums.eAttribType.Duration:
                        if (Math.Abs(Math_Mag - 0.0f) > 0.01)
                            return Math_Mag;
                        num1 = nMagnitude;
                        if (EffectType == Enums.eEffectType.Damage) num1 = -num1;
                        break;
                    case Enums.eAttribType.Expression:
                        num1 = ParseMagnitudeExpression() * DatabaseAPI.GetModifier(this);
                        if (EffectType == Enums.eEffectType.Damage) num1 = -num1;
                        break;
                }

                return num1;
            }
        }

        public float MagPercent => !DisplayPercentage ? Mag : Mag * 100f;

        public float Duration
        {
            get
            {
                var num = AttribType switch
                {
                    Enums.eAttribType.Magnitude => Math.Abs(Math_Duration - 0.0f) > 0.01
                        ? Math_Duration
                        : nDuration,
                    Enums.eAttribType.Duration => Math.Abs(Math_Duration - 0.0f) <= 0.01
                        ? Scale * DatabaseAPI.GetModifier(this)
                        : Math_Duration,
                    _ => 0.0f
                };
                return num;
            }
        }

        public bool DisplayPercentage
        {
            get
            {
                bool flag;
                switch (DisplayPercentageOverride)
                {
                    case Enums.eOverrideBoolean.TrueOverride:
                        flag = true;
                        break;
                    case Enums.eOverrideBoolean.FalseOverride:
                        flag = false;
                        break;
                    default:
                        if (EffectType == Enums.eEffectType.SilentKill)
                        {
                            flag = false;
                            break;
                        }

                        switch (Aspect)
                        {
                            case Enums.eAspect.Max:
                                if (EffectType == Enums.eEffectType.HitPoints ||
                                    EffectType == Enums.eEffectType.Endurance ||
                                    EffectType == Enums.eEffectType.SpeedRunning ||
                                    EffectType == Enums.eEffectType.SpeedJumping ||
                                    EffectType == Enums.eEffectType.SpeedFlying)
                                    return false;
                                break;
                            case Enums.eAspect.Abs:
                                return false;
                            case Enums.eAspect.Cur:
                                if (EffectType == Enums.eEffectType.Mez ||
                                    EffectType == Enums.eEffectType.StealthRadius ||
                                    EffectType == Enums.eEffectType.StealthRadiusPlayer)
                                    return false;
                                break;
                        }

                        flag = true;
                        break;
                }

                return flag;
            }
        }

        public bool VariableModified //
        {
            get
            {
                bool flag;
                if (VariableModifiedOverride)
                {
                    flag = true;
                }
                else
                {
                    if (power != null)
                    {
                        var ps = power.GetPowerSet();
                        if (ps == null)
                            return false;
                        if (ps.nArchetype > -1)
                        {
                            if (!DatabaseAPI.Database.Classes[ps.nArchetype].Playable)
                                return false;
                        }
                        else if (ps.SetType == Enums.ePowerSetType.None
                                 || ps.SetType == Enums.ePowerSetType.Accolade
                                 //|| ps.SetType == Enums.ePowerSetType.Pet
                                 || ps.SetType == Enums.ePowerSetType.SetBonus
                                 || ps.SetType == Enums.ePowerSetType.Temp)
                        {
                            return false;
                        }
                    }

                    if ((EffectType == Enums.eEffectType.EntCreate) & (ToWho == Enums.eToWho.Target) & (Stacking == Enums.eStacking.Yes))
                    {
                        flag = true;
                    }
                    else if ((EffectType == Enums.eEffectType.DamageBuff) & (ToWho == Enums.eToWho.Target) & (Stacking == Enums.eStacking.Yes))
                    {
                        flag = true;
                    }
                    else
                    {
                        if (power != null)
                        {
                            for (var index = 0; index <= power.Effects.Length - 1; ++index)
                            {
                                if ((power.Effects[index].EffectType == Enums.eEffectType.EntCreate) & (power.Effects[index].ToWho == Enums.eToWho.Target) & (power.Effects[index].Stacking == Enums.eStacking.Yes))
                                {
                                    return false;
                                }
                            }
                        }

                        flag = ToWho == Enums.eToWho.Self && Stacking == Enums.eStacking.Yes;
                    }
                }

                return flag;
            }
            set { }
        }

        public bool InherentSpecial => SpecialCase == Enums.eSpecialCase.Assassination ||
                                       SpecialCase == Enums.eSpecialCase.Hidden ||
                                       SpecialCase == Enums.eSpecialCase.Containment ||
                                       SpecialCase == Enums.eSpecialCase.CriticalHit ||
                                       SpecialCase == Enums.eSpecialCase.Domination ||
                                       SpecialCase == Enums.eSpecialCase.Scourge ||
                                       SpecialCase == Enums.eSpecialCase.Supremacy;

        public bool InherentSpecial2 => ValidateConditional("active", "Assassination") ||
                                        ValidateConditional("active", "Containment") ||
                                        ValidateConditional("active", "CriticalHit") ||
                                        ValidateConditional("active", "Domination") ||
                                        ValidateConditional("active", "Scourge") ||
                                        ValidateConditional("active", "Supremacy");


        public float BaseProbability { get; set; }

        public bool IgnoreED { get; set; }

        public string Reward { get; set; }

        public string EffectId { get; set; }

        public string Special { get; set; }

        public IPower GetPower()
        {
            return power;
        }

        public void SetPower(IPower power)
        {
            this.power = power;
        }

        public IEnhancement Enhancement { get; set; }

        public int nID { get; set; }

        public Enums.eEffectClass EffectClass { get; set; }

        public Enums.eEffectType EffectType { get; set; }

        public Enums.ePowerAttribs PowerAttribs { get; set; }

        public Enums.eOverrideBoolean DisplayPercentageOverride { get; set; }

        public Enums.eDamage DamageType { get; set; }

        public Enums.eMez MezType { get; set; }

        public Enums.eEffectType ETModifies { get; set; }

        public string Summon { get; set; }

        public int nSummon
        {
            get
            {
                if (!SummonId.HasValue)
                    SummonId = EffectType == Enums.eEffectType.EntCreate
                        ? DatabaseAPI.NidFromUidEntity(Summon)
                        : DatabaseAPI.NidFromUidPower(Summon);
                return SummonId.Value;
            }
            set => SummonId = value;
        }


        public int Ticks { get; set; }

        public float DelayedTime { get; set; }

        public Enums.eStacking Stacking { get; set; }

        public Enums.eSuppress Suppression { get; set; }

        public bool Buffable { get; set; }

        public bool Resistible { get; set; }

        public Enums.eSpecialCase SpecialCase { get; set; }

        public string UIDClassName { get; set; }

        public int nIDClassName { get; set; }

        public bool VariableModifiedOverride { get; set; }

        public bool isEnhancementEffect { get; set; }

        public Enums.ePvX PvMode { get; set; }

        public Enums.eToWho ToWho { get; set; }

        public float Scale { get; set; }

        public float nMagnitude { get; set; }

        public float nDuration { get; set; }

        public Enums.eAttribType AttribType { get; set; }

        public Enums.eAspect Aspect { get; set; }

        public string ModifierTable { get; set; }

        public int nModifierTable { get; set; }

        public string PowerFullName { get; set; }

        public bool NearGround { get; set; }

        public bool RequiresToHitCheck { get; set; }

        public float Math_Mag { get; set; }

        public float Math_Duration { get; set; }

        public bool Absorbed_Effect { get; set; }

        public Enums.ePowerType Absorbed_PowerType { get; set; }

        public int Absorbed_Power_nID { get; set; }

        public float Absorbed_Duration { get; set; }

        public int Absorbed_Class_nID { get; set; }

        public float Absorbed_Interval { get; set; }

        public int Absorbed_EffectID { get; set; }

        public Enums.eBuffMode buffMode { get; set; }

        public int UniqueID { get; set; }

        public string Override { get; set; }

        public float AtrOrigAccuracy { get; set; }
        public float AtrOrigActivatePeriod { get; set; }
        public int AtrOrigArc { get; set; }
        public float AtrOrigCastTime { get; set; }
        public Enums.eEffectArea AtrOrigEffectArea { get; set; }
        public float AtrOrigEnduranceCost { get; set; }
        public float AtrOrigInterruptTime { get; set; }
        public int AtrOrigMaxTargets { get; set; }
        public float AtrOrigRadius { get; set; }
        public float AtrOrigRange { get; set; }
        public float AtrOrigRechargeTime { get; set; }
        public float AtrOrigSecondaryRange { get; set; }

        public float AtrModAccuracy { get; set; }
        public float AtrModActivatePeriod { get; set; }
        public int AtrModArc { get; set; }
        public float AtrModCastTime { get; set; }
        public Enums.eEffectArea AtrModEffectArea { get; set; }
        public float AtrModEnduranceCost { get; set; }
        public float AtrModInterruptTime { get; set; }
        public int AtrModMaxTargets { get; set; }
        public float AtrModRadius { get; set; }
        public float AtrModRange { get; set; }
        public float AtrModRechargeTime { get; set; }
        public float AtrModSecondaryRange { get; set; }

        public List<KeyValue<string, string>> ActiveConditionals { get; set; }
        public bool Validated { get; set; }

        public int nOverride
        {
            get
            {
                if (!OverrideId.HasValue)
                    OverrideId = DatabaseAPI.NidFromUidPower(Override);
                return OverrideId.Value;
            }
            set => OverrideId = value;
        }


        public bool isDamage()
        {
            return EffectType == Enums.eEffectType.Defense || EffectType == Enums.eEffectType.DamageBuff ||
                   EffectType == Enums.eEffectType.Resistance || EffectType == Enums.eEffectType.Damage ||
                   EffectType == Enums.eEffectType.Elusivity;
        }

        public string BuildEffectStringShort(bool noMag = false, bool simple = false, bool useBaseProbability = false)
        {
            var str1 = string.Empty;
            var str2 = string.Empty;
            var iValue = string.Empty;
            var str3 = string.Empty;
            var str4 = string.Empty;
            var effectNameShort1 = Enums.GetEffectNameShort(EffectType);
            if (power != null && power.VariableEnabled && VariableModified)
                str4 = " (V)";
            if (!simple)
                str3 = ToWho switch
                {
                    Enums.eToWho.Target => " to Tgt",
                    Enums.eToWho.Self => " to Slf",
                    _ => str3
                };
            if (useBaseProbability)
            {
                if (BaseProbability < 1.0)
                    iValue = (BaseProbability * 100f).ToString("#0") + "% chance";
            }
            else if (Probability < 1.0)
            {
                iValue = (Probability * 100f).ToString("#0") + "% chance";
            }

            if (!noMag)
            {
                str1 = Utilities.FixDP(MagPercent);
                if (DisplayPercentage)
                    str1 += "%";
            }

            string str5;
            switch (EffectType)
            {
                case Enums.eEffectType.None:
                    str5 = Special;
                    if (Special == "Debt Protection" && !noMag) str5 = str1 + "% " + str5;
                    break;
                case Enums.eEffectType.Damage:
                case Enums.eEffectType.DamageBuff:
                case Enums.eEffectType.Defense:
                case Enums.eEffectType.Resistance:
                case Enums.eEffectType.Elusivity:
                    var name1 = Enum.GetName(typeof(Enums.eDamageShort), (Enums.eDamageShort)DamageType);
                    if (EffectType == Enums.eEffectType.Damage)
                    {
                        if (Ticks > 0)
                        {
                            str1 = Ticks + " * " + str1;
                            if (Duration > 0.0)
                                str2 = " over " + Utilities.FixDP(Duration) + " seconds";
                            else if (Absorbed_Duration > 0.0)
                                str2 = " over " + Utilities.FixDP(Absorbed_Duration) + " seconds";
                        }

                        str5 = str1 + " " + name1 + " " + effectNameShort1 + str3 + str2;
                        break;
                    }

                    var str6 = "(" + name1 + ")";
                    if (DamageType == Enums.eDamage.None)
                        str6 = string.Empty;
                    str5 = str1 + " " + effectNameShort1 + str6 + str3 + str2;
                    break;
                case Enums.eEffectType.Endurance:
                    if (noMag)
                    {
                        str5 = "+Max End";
                        break;
                    }

                    str5 = str1 + " " + effectNameShort1 + str3 + str2;
                    break;
                case Enums.eEffectType.Enhancement:
                    var str7 = ETModifies != Enums.eEffectType.Mez ? !((ETModifies == Enums.eEffectType.Defense) | (ETModifies == Enums.eEffectType.Resistance)) ? Enums.GetEffectNameShort(ETModifies) : Enums.GetDamageNameShort(DamageType) + " " + Enums.GetEffectNameShort(ETModifies) : Enums.GetMezNameShort((Enums.eMezShort)MezType);
                    str5 = str1 + " " + effectNameShort1 + "(" + str7 + ")" + str3 + str2;
                    break;
                case Enums.eEffectType.GrantPower:
                    var powerByName = DatabaseAPI.GetPowerByFullName(Summon);
                    var str8 = powerByName == null ? " " + Summon : " " + powerByName.DisplayName;
                    str5 = effectNameShort1 + str8 + str3;
                    break;
                case Enums.eEffectType.Heal:
                case Enums.eEffectType.HitPoints:
                    if (noMag)
                    {
                        str5 = "+Max HP";
                        break;
                    }

                    if (Aspect == Enums.eAspect.Cur)
                    {
                        str5 = Utilities.FixDP(Mag * 100f) + "% " + effectNameShort1 + str3 + str2;
                        break;
                    }

                    if (!DisplayPercentage)
                    {
                        str5 = str1 + " (" +
                               Utilities.FixDP((float)(Mag / (double)MidsContext.Archetype.Hitpoints * 100.0)) +
                               "%)" + effectNameShort1 + str3 + str2;
                        break;
                    }

                    str5 = Utilities.FixDP(Mag / 100f * MidsContext.Archetype.Hitpoints) + " (" + str1 + ") " +
                           effectNameShort1 + str3 + str2;
                    break;
                case Enums.eEffectType.Mez:
                    var name2 = Enum.GetName(MezType.GetType(), MezType);
                    if (Duration > 0.0 && (!simple || MezType != Enums.eMez.None && MezType != Enums.eMez.Knockback &&
                        MezType != Enums.eMez.Knockup))
                        str2 = Utilities.FixDP(Duration) + " second ";
                    var str9 = " (Mag " + str1 + ")";
                    str5 = str2 + name2 + str9 + str3;
                    break;
                case Enums.eEffectType.MezResist:
                    var name3 = Enum.GetName(MezType.GetType(), MezType);
                    if (!noMag)
                        str1 = " " + str1;
                    str5 = effectNameShort1 + "(" + name3 + ")" + str1 + str3 + str2;
                    break;
                case Enums.eEffectType.Recovery:
                    if (noMag)
                    {
                        str5 = "+Recovery";
                        break;
                    }

                    if (DisplayPercentage)
                    {
                        str5 = str1 + " (" +
                               Utilities.FixDP(Mag * (MidsContext.Archetype.BaseRecovery * Statistics.BaseMagic)) +
                               " /s) " + effectNameShort1 + str3 + str2;
                        break;
                    }

                    str5 = str1 + " " + effectNameShort1 + str3 + str2;
                    break;
                case Enums.eEffectType.Regeneration:
                    if (noMag)
                    {
                        str5 = "+Regeneration";
                        break;
                    }

                    if (DisplayPercentage)
                    {
                        str5 = str1 + " (" +
                               Utilities.FixDP((float)(MidsContext.Archetype.Hitpoints / 100.0 *
                                                        (Mag * (double)MidsContext.Archetype.BaseRegen *
                                                         1.66666662693024))) + " HP/s) " + effectNameShort1 + str3 +
                               str2;
                        break;
                    }

                    str5 = str1 + " " + effectNameShort1 + str3 + str2;
                    break;
                case Enums.eEffectType.ResEffect:
                    var effectNameShort2 = Enums.GetEffectNameShort(ETModifies);
                    str5 = str1 + " " + effectNameShort1 + "(" + effectNameShort2 + ")" + str3 + str2;
                    break;
                case Enums.eEffectType.StealthRadius:
                case Enums.eEffectType.StealthRadiusPlayer:
                    str5 = str1 + "ft " + effectNameShort1 + str3 + str2;
                    break;
                case Enums.eEffectType.EntCreate:
                    var index = DatabaseAPI.NidFromUidEntity(Summon);
                    var str10 = index <= -1 ? " " + Summon : " " + DatabaseAPI.Database.Entities[index].DisplayName;
                    str5 = Duration <= 9999.0
                        ? effectNameShort1 + str10 + str3 + str2
                        : effectNameShort1 + str10 + str3;
                    break;
                case Enums.eEffectType.GlobalChanceMod:
                    str5 = str1 + " " + effectNameShort1 + " " + Reward + str3 + str2;
                    break;
                default:
                    str5 = str1 + " " + effectNameShort1 + str3 + str2;
                    break;
            }

            var iStr = string.Empty;
            if (!string.IsNullOrEmpty(iValue))
                iStr = " (" + BuildCs(iValue, iStr) + ")";
            return str5.Trim() + iStr + str4;
        }


        public string BuildEffectString(bool simple = false, string specialCat = "", bool noMag = false, bool grouped = false, bool useBaseProbability = false)
        {
            string sBuild = string.Empty;

            string sSubEffect = string.Empty;
            string sSubSubEffect = string.Empty;
            string sMag = string.Empty;
            string sDuration = string.Empty;
            string sChance = string.Empty;
            string sTarget = string.Empty;
            string sPvx = string.Empty;
            string sStack = string.Empty;
            string sBuff = string.Empty;
            string sDelay = string.Empty;
            string sResist = string.Empty;
            string sSpecial = string.Empty;
            string sSuppress = string.Empty;
            string sVariable = string.Empty;
            string sToHit = string.Empty;
            string sEnh = string.Empty;
            string sSuppressShort = string.Empty;
            string sConditional = string.Empty;

            if (power != null && power.VariableEnabled && VariableModified)
            {
                sVariable = " (Variable)";
            }

            if (isEnhancementEffect)
            {
                sEnh = "(From Enh) ";
            }
            string sEffect = Enums.GetEffectName(EffectType);

            if (!simple)
            {
                if (ToWho == Enums.eToWho.Target)
                {
                    sTarget = " to Target";
                }
                else if (ToWho == Enums.eToWho.Self)
                {
                    sTarget = " to Self";
                }
                if (RequiresToHitCheck)
                {
                    sToHit = " requires ToHit check";
                }
            }

            if (ProcsPerMinute > 0 && Probability < 0.01)
            {
                sChance = ProcsPerMinute + "PPM";
            }
            else if (useBaseProbability)
            {
                if (BaseProbability < 1)
                {
                    if (BaseProbability >= 0.975f)
                    {
                        sChance = (BaseProbability * 100).ToString("#0" + NumberFormatInfo.CurrentInfo.NumberDecimalSeparator + "0") + "% chance";
                    }
                    else
                    {
                        sChance = (BaseProbability * 100).ToString("#0") + "% chance";
                    }
                }
            }
            else
            {
                if (Probability < 1)
                {
                    if (Probability >= 0.975f)
                    {
                        sChance = (Probability * 100).ToString("#0" + NumberFormatInfo.CurrentInfo.NumberDecimalSeparator + "0") + "% chance";
                    }
                    else
                    {
                        sChance = (Probability * 100).ToString("#0") + "% chance";
                    }

                    if (CancelOnMiss)
                    {
                        sChance += ", CancelOnMiss";
                    }
                }
            }

            bool resistPresent = false;
            if (Resistible == false)
            {
                if ((!simple & ToWho != Enums.eToWho.Self) | EffectType == Enums.eEffectType.Damage)
                {
                    sResist = "Non-resistible";
                    resistPresent = true;
                }
            }

            switch (PvMode)
            {
                case Enums.ePvX.PvE:
                    sPvx = resistPresent ? "by Mobs" : "to Mobs";
                    if (EffectType == Enums.eEffectType.Heal & Aspect == Enums.eAspect.Abs & Mag > 0 &
                        PvMode == Enums.ePvX.PvE)
                    {
                        sPvx = "in PvE";
                    }
                    if (ToWho == Enums.eToWho.Self)
                    {
                        sPvx = "in PvE";
                    }
                    break;
                case Enums.ePvX.PvP:
                    sPvx = resistPresent ? "by Players" : "to Players";
                    if (ToWho == Enums.eToWho.Self)
                    {
                        sPvx = "in PvP";
                    }
                    break;
            }
            if (!simple)
            {
                if (Buffable == false & EffectType != Enums.eEffectType.DamageBuff)
                {
                    sBuff = " [Ignores Enhancements & Buffs]";
                }
                if (Stacking == Enums.eStacking.No)
                {
                    sStack = "\n  Effect does not stack from same caster";
                }

                if (DelayedTime > 0)
                {
                    sDelay = "after " + Utilities.FixDP(DelayedTime) + " seconds";
                }
            }

            if (SpecialCase != Enums.eSpecialCase.None & SpecialCase != Enums.eSpecialCase.Defiance)
            {
                sSpecial = Enum.GetName(SpecialCase.GetType(), SpecialCase);
            }

            if (ActiveConditionals.Count > 0)
            {
                var getCondition = new Regex("(:.*)");
                var getConditionItem = new Regex("(.*:)");
                var conList = new List<string>();
                foreach (var cVp in ActiveConditionals)
                {
                    var condition = getCondition.Replace(cVp.Key, "").Replace(":", "");
                    var conditionItemName = getConditionItem.Replace(cVp.Key, "").Replace(":", "");
                    var conditionPower = DatabaseAPI.GetPowerByFullName(conditionItemName);
                    string conditionOperator;
                    if (cVp.Value.Equals("True")) { conditionOperator = "is "; }
                    else if (cVp.Value.Equals("False")) { conditionOperator = "not "; }
                    else { conditionOperator = ""; }

                    if (!condition.Equals("Stacks") && !condition.Equals("Team"))
                    {
                        conList.Add($"{conditionPower} {conditionOperator}{condition}");
                    }
                    else if (condition.Equals("Stacks"))
                    {
                        conList.Add($"{conditionPower} {condition} {cVp.Value}");
                    }
                    else if (condition.Equals("Team"))
                    {
                        conList.Add($"{conditionItemName}s on {condition} {cVp.Value}");
                    }
                    /*conList.Add(!condition.Equals("Stacks")
                        ? $"{conditionPower} {conditionOperator}{condition}"
                        : $"{conditionPower} {condition} {cVp.Value}");*/
                    sConditional = string.Join(" AND ", conList);
                }
            }

            if (!simple || Scale > 0 && EffectType == Enums.eEffectType.Mez)
            {
                sDuration = string.Empty;
                string sForOver = " for ";
                switch (EffectType)
                {
                    case Enums.eEffectType.Damage:
                        sForOver = " over ";
                        break;
                    case Enums.eEffectType.SilentKill:
                        sForOver = " in ";
                        break;
                }

                if (Duration > 0 & (EffectType != Enums.eEffectType.Damage | Ticks > 0))
                {
                    sDuration += sForOver + Utilities.FixDP(Duration) + " seconds";
                }
                else if (Absorbed_Duration > 0 & (EffectType != Enums.eEffectType.Damage | Ticks > 0))
                {
                    sDuration += sForOver + Utilities.FixDP(Absorbed_Duration) + " seconds";
                }
                else
                {
                    sDuration += " ";
                }
                // If .Absorbed_Interval > 0 Then
                if (Absorbed_Interval > 0 & Absorbed_Interval < 900)
                {
                    sDuration += " every " + Utilities.FixDP(Absorbed_Interval) + " seconds";
                }
            }

            if (noMag == false & EffectType != Enums.eEffectType.SilentKill)
            {
                if (DisplayPercentage)
                {
                    sMag = Utilities.FixDP(Mag * 100);
                    sMag += "%";
                }
                else
                {
                    sMag = Utilities.FixDP(Mag);
                }
            }

            if (!simple)
            {
                sSuppress = string.Empty;
                if ((Suppression & Enums.eSuppress.ActivateAttackClick) == Enums.eSuppress.ActivateAttackClick)
                {
                    sSuppress += "\n  Suppressed when Attacking.";
                }
                if ((Suppression & Enums.eSuppress.Attacked) == Enums.eSuppress.Attacked)
                {
                    sSuppress += "\n  Suppressed when Attacked.";
                }
                if ((Suppression & Enums.eSuppress.HitByFoe) == Enums.eSuppress.HitByFoe)
                {
                    sSuppress += "\n  Suppressed when Hit.";
                }
                if ((Suppression & Enums.eSuppress.MissionObjectClick) == Enums.eSuppress.MissionObjectClick)
                {
                    sSuppress += "\n  Suppressed when MissionObjectClick.";
                }

                if ((Suppression & Enums.eSuppress.Held) == Enums.eSuppress.Held ||
                    (Suppression & Enums.eSuppress.Immobilized) == Enums.eSuppress.Immobilized ||
                    (Suppression & Enums.eSuppress.Sleep) == Enums.eSuppress.Sleep ||
                    (Suppression & Enums.eSuppress.Stunned) == Enums.eSuppress.Stunned ||
                    (Suppression & Enums.eSuppress.Terrorized) == Enums.eSuppress.Terrorized)
                {
                    sSuppress += "\n  Suppressed when Mezzed.";
                }
                if ((Suppression & Enums.eSuppress.Knocked) == Enums.eSuppress.Knocked)
                {
                    sSuppress += "\n  Suppressed when Knocked.";
                }
            }
            else
            {
                if ((Suppression & Enums.eSuppress.ActivateAttackClick) == Enums.eSuppress.ActivateAttackClick ||
                    (Suppression & Enums.eSuppress.Attacked) == Enums.eSuppress.Attacked ||
                    (Suppression & Enums.eSuppress.HitByFoe) == Enums.eSuppress.HitByFoe)
                {
                    sSuppressShort = "Combat Suppression";
                }
            }


            switch (EffectType)
            {
                case Enums.eEffectType.Elusivity:
                case Enums.eEffectType.Damage:
                case Enums.eEffectType.Resistance:
                case Enums.eEffectType.DamageBuff:
                case Enums.eEffectType.Defense:
                    if (string.IsNullOrEmpty(specialCat))
                    {
                        sSubEffect = grouped ? "%VALUE%" : Enum.GetName(DamageType.GetType(), DamageType);
                        if (EffectType == Enums.eEffectType.Damage)
                        {
                            if (Ticks > 0)
                            {
                                sMag = Ticks + " x " + sMag;
                            }
                            sBuild = sMag + " " + sSubEffect + " " + sEffect + sTarget + sDuration;
                        }
                        else
                        {
                            sSubEffect = "(" + sSubEffect + ")";
                            if (DamageType == Enums.eDamage.None)
                                sSubEffect = string.Empty;
                            sBuild = sMag + " " + sEffect + sSubEffect + sTarget + sDuration;
                        }
                    }
                    else
                    {
                        sBuild = sMag + " " + specialCat + " " + sTarget + sDuration;
                    }
                    break;
                case Enums.eEffectType.StealthRadius:
                case Enums.eEffectType.StealthRadiusPlayer:
                    sBuild = sMag + "ft " + sEffect + sTarget + sDuration;
                    break;
                case Enums.eEffectType.Mez:
                    sSubEffect = Enum.GetName(MezType.GetType(), MezType);
                    if (Duration > 0 & (simple == false | (MezType != Enums.eMez.None & MezType != Enums.eMez.Knockback & MezType != Enums.eMez.Knockup)))
                        sDuration = Utilities.FixDP(Duration) + " second ";
                    if (noMag == false)
                        sMag = " (Mag " + sMag + ")";
                    sBuild = sDuration + sSubEffect + sMag + sTarget;
                    break;
                case Enums.eEffectType.MezResist:
                    sSubEffect = Enum.GetName(MezType.GetType(), MezType);
                    if (noMag == false)
                        sMag = " " + sMag;
                    sBuild = sEffect + "(" + sSubEffect + ")" + sMag + sTarget + sDuration;
                    break;

                case Enums.eEffectType.ResEffect:
                    sSubEffect = Enum.GetName(ETModifies.GetType(), ETModifies);
                    if (sSubEffect == "Mez")
                    {
                        sSubSubEffect = Enum.GetName(MezType.GetType(), MezType);
                        sBuild = $"{sMag} {sEffect}({sSubSubEffect}){sTarget}{sDuration}";
                    }
                    else
                    {
                        sBuild = $"{sMag} {sEffect}({sSubEffect}){sTarget}{sDuration}";
                    }
                    break;

                case Enums.eEffectType.Enhancement:
                    string tSpStr;
                    if (ETModifies == Enums.eEffectType.Mez)
                    {
                        tSpStr = Enums.GetMezName((Enums.eMezShort)MezType);
                    }
                    else if (ETModifies == Enums.eEffectType.Defense | ETModifies == Enums.eEffectType.Resistance | ETModifies == Enums.eEffectType.Damage)
                    {
                        tSpStr = Enums.GetDamageName(DamageType) + " " + Enums.GetEffectName(ETModifies);
                    }
                    else
                    {
                        tSpStr = Enums.GetEffectName(ETModifies);
                    }

                    sBuild = sMag + " " + sEffect + "(" + tSpStr + ")" + sTarget + sDuration;
                    break;
                case Enums.eEffectType.None:
                    sBuild = Special;
                    if (Special == "Debt Protection")
                    {
                        sBuild = sMag + "% " + sBuild;
                    }
                    break;
                case Enums.eEffectType.Heal:
                case Enums.eEffectType.HitPoints:
                    if (noMag == false)
                    {
                        if (Ticks > 0)
                        {
                            sMag = Ticks + " x " + sMag;
                        }
                        if (Aspect == Enums.eAspect.Cur)
                        {
                            sBuild = Utilities.FixDP(Mag * 100) + "% " + sEffect + sTarget + sDuration;
                        }
                        else
                        {
                            if (DisplayPercentage == false)
                            {
                                sBuild = sMag + " HP (" + Utilities.FixDP(Mag / MidsContext.Archetype.Hitpoints * 100) + "%) " + sEffect + sTarget + sDuration;
                            }
                            else
                            {
                                sBuild = Utilities.FixDP((Mag / 100) * MidsContext.Archetype.Hitpoints) + " HP (" + sMag + ") " + sEffect + sTarget + sDuration;
                            }
                        }
                    }
                    else
                    {
                        sBuild = "+Max HP";
                    }
                    break;
                case Enums.eEffectType.Regeneration:
                    if (noMag == false)
                    {
                        if (DisplayPercentage)
                        {
                            sBuild = sMag + " (" + Utilities.FixDP((MidsContext.Archetype.Hitpoints / 100f) * (Mag * MidsContext.Archetype.BaseRegen * Statistics.BaseMagic)) + " HP/sec) " + sEffect + sTarget + sDuration;
                        }
                        else
                        {
                            sBuild = sMag + " " + sEffect + sTarget + sDuration;
                        }
                    }
                    else
                    {
                        sBuild = "+Regeneration";
                    }
                    break;
                case Enums.eEffectType.Recovery:
                    if (noMag == false)
                    {
                        if (DisplayPercentage)
                        {
                            sBuild = sMag + " (" + Utilities.FixDP(Mag * (MidsContext.Archetype.BaseRecovery * Statistics.BaseMagic)) + " End/sec) " + sEffect + sTarget + sDuration;
                        }
                        else
                        {
                            sBuild = sMag + " " + sEffect + sTarget + sDuration;
                        }
                    }
                    else
                    {
                        sBuild = "+Recovery";
                    }
                    break;
                case Enums.eEffectType.EntCreate:
                    sResist = string.Empty;
                    var summon = DatabaseAPI.NidFromUidEntity(Summon);
                    string tSummon;
                    if (summon > -1)
                    {
                        tSummon = " " + DatabaseAPI.Database.Entities[summon].DisplayName;
                    }
                    else
                    {
                        tSummon = " " + Summon;
                    }
                    if (Duration > 9999)
                    {
                        sBuild = sEffect + tSummon + sTarget;
                    }
                    else
                    {
                        sBuild = sEffect + tSummon + sTarget + sDuration;
                    }
                    break;
                case Enums.eEffectType.Endurance:
                    if (noMag) sBuild = "+Max End";
                    else if (Aspect == Enums.eAspect.Max)
                    {
                        sBuild = sMag + "% Max End" + sTarget + sDuration;
                    }
                    else
                    {
                        sBuild = sMag + " " + sEffect + sTarget + sDuration;
                    }
                    break;
                case Enums.eEffectType.GrantPower:
                    sResist = string.Empty;
                    string tGrant;
                    var pID = DatabaseAPI.GetPowerByFullName(Summon);
                    if (pID != null)
                    {
                        tGrant = " " + pID.DisplayName;
                    }
                    else
                    {
                        tGrant = " " + Summon;
                    }
                    sBuild = sEffect + tGrant + sTarget;
                    break;
                case Enums.eEffectType.GlobalChanceMod:
                    sBuild = sMag + " " + sEffect + " " + Reward + sTarget + sDuration;
                    break;
                case Enums.eEffectType.ModifyAttrib:
                    sSubEffect = Enum.GetName(PowerAttribs.GetType(), PowerAttribs);
                    if (sSubEffect == "Accuracy")
                    {
                        sBuild = $"{sEffect}({sSubEffect}) to {AtrModAccuracy} ({Convert.ToDecimal(AtrModAccuracy * MidsContext.Config.BaseAcc * 100f):0.##}%)";
                    }
                    else if (sSubEffect == "ActivateInterval")
                    {
                        sBuild = $"{sEffect}({sSubEffect}) to {AtrModActivatePeriod} second(s)";
                    }
                    else if (sSubEffect == "Arc")
                    {
                        sBuild = $"{sEffect}({sSubEffect}) to {AtrModArc} degrees";
                    }
                    else if (sSubEffect == "CastTime")
                    {
                        sBuild = $"{sEffect}({sSubEffect}) to {AtrModCastTime} second(s)";
                    }
                    else if (sSubEffect == "EffectArea")
                    {
                        sBuild = $"{sEffect}({sSubEffect}) to {Enum.GetName(typeof(Enums.eEffectArea), AtrModEffectArea)}";
                    }
                    else if (sSubEffect == "EnduranceCost")
                    {
                        sBuild = $"{sEffect}({sSubEffect}) to {AtrModEnduranceCost}";
                    }
                    else if (sSubEffect == "InterruptTime")
                    {
                        sBuild = $"{sEffect}({sSubEffect}) to {AtrModInterruptTime} second(s)";
                    }
                    else if (sSubEffect == "MaxTargets")
                    {
                        sBuild = $"{sEffect}({sSubEffect}) to {AtrModMaxTargets} target(s)";
                    }
                    else if (sSubEffect == "Radius")
                    {
                        sBuild = $"{sEffect}({sSubEffect}) to {AtrModRadius} feet";
                    }
                    else if (sSubEffect == "Range")
                    {
                        sBuild = $"{sEffect}({sSubEffect}) to {AtrModRange} feet";
                    }
                    else if (sSubEffect == "RechargeTime")
                    {
                        sBuild = $"{sEffect}({sSubEffect}) to {AtrModRechargeTime} second(s)";
                    }
                    else if (sSubEffect == "SecondaryRange")
                    {
                        sBuild = $"{sEffect}({sSubEffect}) to {AtrModSecondaryRange} feet";
                    }
                    break;
                default:
                    sBuild = sMag + " " + sEffect + sTarget + sDuration;
                    break;
            }


            var sExtra = string.Empty;
            var sExtra2 = string.Empty;
            //(20% chance, non-resistible if target = player)
            if (!string.IsNullOrEmpty(sChance + sResist + sPvx + sDelay + sSpecial + sConditional + sToHit + sSuppressShort))
            {
                sExtra = BuildCs(sChance, sExtra);
                sExtra = BuildCs(sDelay, sExtra);
                sExtra = BuildCs(sSuppressShort, sExtra);
                sExtra = BuildCs(sResist, sExtra);
                sExtra2 = BuildCs(sChance, sExtra2);
                sExtra2 = BuildCs(sDelay, sExtra2);
                sExtra2 = BuildCs(sSuppressShort, sExtra2);
                sExtra2 = BuildCs(sResist, sExtra2);
                if (!string.IsNullOrEmpty(sPvx))
                {
                    sExtra = !string.IsNullOrEmpty(sSpecial) ? BuildCs(sPvx + ", if " + sSpecial, sExtra, resistPresent) : BuildCs(sPvx, sExtra, resistPresent);
                    sExtra2 = !string.IsNullOrEmpty(sConditional) ? BuildCs(sPvx + ", if " + sConditional, sExtra2, resistPresent) : BuildCs(sPvx, sExtra2, resistPresent);
                }
                else
                {
                    if (!string.IsNullOrEmpty(sSpecial))
                        sExtra = BuildCs("if " + sSpecial, sExtra);
                    if (!string.IsNullOrEmpty(sConditional))
                        sExtra2 = BuildCs("if " + sConditional, sExtra2);
                }
                sExtra = BuildCs(sToHit, sExtra);
                sExtra = " (" + sExtra + ")";
                sExtra2 = BuildCs(sToHit, sExtra2);
                sExtra2 = " (" + sExtra2 + ")";
            }

            if (sExtra.Equals(" ()")) { sExtra = ""; }

            if (sConditional != "")
            {
                return sEnh + sBuild + sExtra2 + sBuff + sVariable + sStack + sSuppress;
            }
            else
            {
                return sEnh + sBuild + sExtra + sBuff + sVariable + sStack + sSuppress;
            }
        }


        public void StoreTo(ref BinaryWriter writer)
        {
            writer.Write(PowerFullName);
            writer.Write(UniqueID);
            writer.Write((int)EffectClass);
            writer.Write((int)EffectType);
            writer.Write((int)DamageType);
            writer.Write((int)MezType);
            writer.Write((int)ETModifies);
            writer.Write(Summon);
            writer.Write(DelayedTime);
            writer.Write(Ticks);
            writer.Write((int)Stacking);
            writer.Write(BaseProbability);
            writer.Write((int)Suppression);
            writer.Write(Buffable);
            writer.Write(Resistible);
            writer.Write((int)SpecialCase);
            writer.Write(VariableModifiedOverride);
            writer.Write((int)PvMode);
            writer.Write((int)ToWho);
            writer.Write((int)DisplayPercentageOverride);
            writer.Write(Scale);
            writer.Write(nMagnitude);
            writer.Write(nDuration);
            writer.Write((int)AttribType);
            writer.Write((int)Aspect);
            /*DatabaseAPI.Database.EffectIds.RemoveRange(DatabaseAPI.Database.EffectIds.Count - NewEffects.Count,
                NewEffects.Count);
            DatabaseAPI.Database.EffectIds.AddRange(NewEffects.Except(DatabaseAPI.Database.EffectIds));*/
            writer.Write(ModifierTable);
            writer.Write(NearGround);
            writer.Write(CancelOnMiss);
            writer.Write(RequiresToHitCheck);
            writer.Write(UIDClassName);
            writer.Write(nIDClassName);
            writer.Write(MagnitudeExpression);
            writer.Write(Reward);
            writer.Write(EffectId);
            writer.Write(IgnoreED);
            writer.Write(Override);
            writer.Write(ProcsPerMinute);

            writer.Write((int)PowerAttribs);
            writer.Write(AtrOrigAccuracy);
            writer.Write(AtrOrigActivatePeriod);
            writer.Write(AtrOrigArc);
            writer.Write(AtrOrigCastTime);
            writer.Write((int)AtrOrigEffectArea);
            writer.Write(AtrOrigEnduranceCost);
            writer.Write(AtrOrigInterruptTime);
            writer.Write(AtrOrigMaxTargets);
            writer.Write(AtrOrigRadius);
            writer.Write(AtrOrigRange);
            writer.Write(AtrOrigRechargeTime);
            writer.Write(AtrOrigSecondaryRange);

            writer.Write(AtrModAccuracy);
            writer.Write(AtrModActivatePeriod);
            writer.Write(AtrModArc);
            writer.Write(AtrModCastTime);
            writer.Write((int)AtrModEffectArea);
            writer.Write(AtrModEnduranceCost);
            writer.Write(AtrModInterruptTime);
            writer.Write(AtrModMaxTargets);
            writer.Write(AtrModRadius);
            writer.Write(AtrModRange);
            writer.Write(AtrModRechargeTime);
            writer.Write(AtrModSecondaryRange);

            writer.Write(ActiveConditionals.Count);
            foreach (var cVp in ActiveConditionals)
            {
                writer.Write(cVp.Key);
                writer.Write(cVp.Value);
            }
        }

        public bool ImportFromCSV(string iCSV)
        {
            bool flag;
            if (iCSV == null)
            {
                flag = false;
            }
            else if (string.IsNullOrEmpty(iCSV))
            {
                flag = false;
            }
            else
            {
                var array = CSV.ToArray(iCSV);
                if (array.Length < 34)
                {
                    flag = false;
                }
                else
                {
                    if (UniqueID < 1)
                        UniqueID = int.Parse(array[34]);
                    PowerFullName = array[0];
                    Aspect = (Enums.eAspect)Enums.StringToFlaggedEnum(array[2], Aspect, true);
                    AttribType = (Enums.eAttribType)Enums.StringToFlaggedEnum(array[6], AttribType, true);
                    EffectId = array[37];
                    Reward = array[29];
                    MagnitudeExpression = array[27];
                    IgnoreED = int.Parse(array[38]) > 0;
                    EffectType = Enums.eEffectType.None;
                    ETModifies = Enums.eEffectType.None;
                    MezType = Enums.eMez.None;
                    DamageType = Enums.eDamage.None;
                    Special = string.Empty;
                    Summon = string.Empty;
                    if (Enums.IsEnumValue(array[3], Enums.eEffectType.None))
                    {
                        EffectType =
                            (Enums.eEffectType)Enums.StringToFlaggedEnum(array[3], Enums.eEffectType.None, true);
                        switch (Aspect)
                        {
                            case Enums.eAspect.Res:
                                ETModifies = EffectType;
                                EffectType = Enums.eEffectType.ResEffect;
                                break;
                            case Enums.eAspect.Str:
                                ETModifies = EffectType;
                                EffectType = Enums.eEffectType.Enhancement;
                                break;
                        }
                    }
                    else if (Enums.IsEnumValue(array[3], Enums.eCSVImport_Damage.None))
                    {
                        DamageType =
                            (Enums.eDamage)Enums.StringToFlaggedEnum(array[3], Enums.eCSVImport_Damage.None, true);
                        switch (Aspect)
                        {
                            case Enums.eAspect.Res:
                                EffectType = Enums.eEffectType.Resistance;
                                break;
                            case Enums.eAspect.Abs:
                                EffectType = Enums.eEffectType.Damage;
                                break;
                            case Enums.eAspect.Str:
                                EffectType = Enums.eEffectType.DamageBuff;
                                break;
                            case Enums.eAspect.Cur:
                                EffectType = Enums.eEffectType.Damage;
                                break;
                            default:
                                var num = (int)MessageBox.Show(
                                    "Unable to interpret Damage-based attribute:\n" + array[0],
                                    "Interpretation Failed");
                                break;
                        }
                    }
                    else if (Enums.IsEnumValue(array[3], Enums.eCSVImport_Damage_Def.None))
                    {
                        DamageType =
                            (Enums.eDamage)Enums.StringToFlaggedEnum(array[3], Enums.eCSVImport_Damage_Def.None, true);
                        switch (Aspect)
                        {
                            case Enums.eAspect.Str:
                                ETModifies = Enums.eEffectType.Defense;
                                EffectType = Enums.eEffectType.Enhancement;
                                break;
                            case Enums.eAspect.Cur:
                                EffectType = Enums.eEffectType.Defense;
                                break;
                        }
                    }
                    else if (Enums.IsEnumValue(array[3], Enums.eCSVImport_Damage_Elusivity.None))
                    {
                        DamageType = (Enums.eDamage)Enums.StringToFlaggedEnum(array[3],
                            Enums.eCSVImport_Damage_Elusivity.None, true);
                        if (Aspect == Enums.eAspect.Str)
                        {
                            EffectType = Enums.eEffectType.Elusivity;
                        }
                        else
                        {
                            var num = (int)MessageBox.Show(
                                "Unable to interpret Elusivity field - not STR based:\n" + array[0],
                                "Interpretation Failed");
                        }
                    }
                    else if (Enums.IsEnumValue(array[3], MezType))
                    {
                        MezType = (Enums.eMez)Enums.StringToFlaggedEnum(array[3], MezType, true);
                        switch (Aspect)
                        {
                            case Enums.eAspect.Res:
                                EffectType = Enums.eEffectType.MezResist;
                                break;
                            case Enums.eAspect.Abs:
                                EffectType = Enums.eEffectType.Mez;
                                break;
                            case Enums.eAspect.Str:
                                ETModifies = Enums.eEffectType.Mez;
                                EffectType = Enums.eEffectType.Enhancement;
                                break;
                            case Enums.eAspect.Cur:
                                EffectType = Enums.eEffectType.Mez;
                                break;
                            default:
                                var num = (int)MessageBox.Show("Unable to interpret Mez-based attribute:\n" + array[0],
                                    "Interpretation Failed");
                                break;
                        }
                    }

                    Summon = EffectType switch
                    {
                        Enums.eEffectType.GrantPower => Reward,
                        Enums.eEffectType.EntCreate => array[13],
                        _ => Summon
                    };
                    nDuration = float.Parse(array[16]);
                    ModifierTable = array[1];
                    nMagnitude = float.Parse(array[17]);
                    Scale = float.Parse(array[5]);
                    NearGround = int.Parse(array[21]) > 0;
                    CancelOnMiss = int.Parse(array[22]) > 0;
                    Override = array[40];
                    ProcsPerMinute = float.Parse(array[59]);
                    Ticks = 0;
                    if (float.Parse(array[19]) > 0.0)
                        Ticks = (int)(1.0 + Math.Floor(nDuration / (double)float.Parse(array[19])));
                    DelayedTime = float.Parse(array[15]);
                    Stacking = array[18].ToLower() == "stack" ? Enums.eStacking.Yes : Enums.eStacking.No;
                    BaseProbability = float.Parse(array[20]);
                    Suppression = (Enums.eSuppress)Enums.StringToFlaggedEnum(array[9].Replace(" ", ","), Suppression);
                    if (Suppression == Enums.eSuppress.None)
                        Suppression =
                            (Enums.eSuppress)Enums.StringToFlaggedEnum(array[10].Replace(" ", ","), Suppression);
                    Buffable = int.Parse(array[7]) > 0;
                    Resistible = int.Parse(array[8]) > 0;
                    var lower = array[26].ToLower();
                    if (power != null)
                    {
                        var ps = power.GetPowerSet();
                        if (ps != null && ps.nArchetype > -1)
                        {
                            if (lower.Contains("kDD".ToLower()))
                                SpecialCase = Enums.eSpecialCase.Combo;
                            else
                                switch (DatabaseAPI.Database.Classes[ps.nArchetype].PrimaryGroup.ToLower())
                                {
                                    case "scrapper_melee":
                                        var str = lower;
                                        if (EffectType == Enums.eEffectType.Damage && !string.IsNullOrEmpty(lower) &&
                                            Probability <= 0.1)
                                        {
                                            switch (str)
                                            {
                                                case
                                                    "arch target> class_minion_grunt eq arch target> class_minion_small eq || arch target> class_minion_pets eq || arch target> class_minion_swarm eq || enttype target> player eq || ! arch source> class_scrapper == &&"
                                                    :
                                                case
                                                    "arch target> class_minion_grunt eq arch target> class_minion_small eq || arch target> class_minion_pets eq || arch target> class_minion_swarm eq || enttype target> player eq || !"
                                                    :
                                                    SpecialCase = Enums.eSpecialCase.CriticalHit;
                                                    goto label_77;
                                                case
                                                    "arch target> class_minion_grunt eq arch target> class_minion_small eq || arch target> class_minion_pets eq || arch target> class_minion_swarm eq || arch source> class_scrapper == &&"
                                                    :
                                                case
                                                    "arch target> class_minion_grunt eq arch target> class_minion_small eq || arch target> class_minion_pets eq || arch target> class_minion_swarm eq ||"
                                                    :
                                                    SpecialCase = Enums.eSpecialCase.CriticalMinion;
                                                    goto label_77;
                                            }

                                            SpecialCase = Enums.eSpecialCase.CriticalHit;
                                            goto label_77;
                                        }
                                        else
                                        {
                                            goto label_77;
                                        }
                                    case "controller_control":
                                        if (EffectType == Enums.eEffectType.Damage && lower.Contains("kheld"))
                                        {
                                            SpecialCase = Enums.eSpecialCase.Containment;
                                            goto label_77;
                                        }
                                        else
                                        {
                                            goto label_77;
                                        }
                                    case "dominator_control":
                                        if (lower.Contains("kStealth source".ToLower()))
                                        {
                                            SpecialCase = Enums.eSpecialCase.Domination;
                                            goto label_77;
                                        }
                                        else
                                        {
                                            goto label_77;
                                        }
                                    case "stalker_melee":
                                        if (lower.Contains("kMeter source> 0".ToLower()))
                                            SpecialCase = Enums.eSpecialCase.Assassination;
                                        if (lower.Contains("kHeld target> 0".ToLower()))
                                            SpecialCase = Enums.eSpecialCase.Mezzed;
                                        if (MagnitudeExpression.IndexOf("TeamSize",
                                            StringComparison.OrdinalIgnoreCase) > -1)
                                        {
                                            SpecialCase = Enums.eSpecialCase.None;
                                            BaseProbability = 0.1f;
                                            AttribType = Enums.eAttribType.Magnitude;
                                            goto label_77;
                                        }
                                        else
                                        {
                                            goto label_77;
                                        }
                                    case "blaster_ranged":
                                        if (lower.Contains("kRange_Finder_Mode".ToLower()))
                                        {
                                            SpecialCase = Enums.eSpecialCase.TargetDroneActive;
                                            goto label_77;
                                        }
                                        else
                                        {
                                            goto label_77;
                                        }
                                    case "corruptor_ranged":
                                        if (lower.Contains("kHitPoints%".ToLower()))
                                        {
                                            SpecialCase = Enums.eSpecialCase.Scourge;
                                            goto label_77;
                                        }
                                        else
                                        {
                                            goto label_77;
                                        }
                                    case "arachnos_soldiers":
                                        if (lower.Contains("kMeter source> 0".ToLower()))
                                        {
                                            SpecialCase = Enums.eSpecialCase.Hidden;
                                            goto label_77;
                                        }
                                        else
                                        {
                                            goto label_77;
                                        }
                                    case "widow_training":
                                        if (lower.Contains("kMeter source> 0".ToLower()))
                                        {
                                            SpecialCase = Enums.eSpecialCase.Hidden;
                                            goto label_77;
                                        }
                                        else
                                        {
                                            goto label_77;
                                        }
                                    default:
                                        switch (lower)
                                        {
                                            case
                                                "arch target> class_minion_grunt eq arch target> class_minion_small eq || arch target> class_minion_pets eq || arch target> class_minion_swarm eq || enttype target> player eq || ! arch source> class_scrapper == &&"
                                                :
                                                SpecialCase = Enums.eSpecialCase.CriticalHit;
                                                goto default;
                                            case
                                                "arch target> class_minion_grunt eq arch target> class_minion_small eq || arch target> class_minion_pets eq || arch target> class_minion_swarm eq || arch source> class_scrapper == &&"
                                                :
                                                SpecialCase = Enums.eSpecialCase.CriticalMinion;
                                                goto default;
                                            case null:
                                                break;
                                            default:
                                                goto case null;
                                        }

                                        break;
                                }
                        }

                    label_77:
                        if (SpecialCase == Enums.eSpecialCase.None)
                        {
                            var str = lower;
                            if ((lower.Contains("arch source> class_scrapper eq") ||
                                 lower.Contains("arch source> class_scrapper ==")) && Probability < 0.9)
                            {
                                switch (str)
                                {
                                    case
                                        "arch target> class_minion_grunt eq arch target> class_minion_small eq || arch target> class_minion_pets eq || arch target> class_minion_swarm eq || enttype target> player eq || ! arch source> class_scrapper == &&"
                                        :
                                    case
                                        "arch target> class_minion_grunt eq arch target> class_minion_small eq || arch target> class_minion_pets eq || arch target> class_minion_swarm eq || enttype target> player eq || !"
                                        :
                                        SpecialCase = Enums.eSpecialCase.CriticalHit;
                                        goto label_101;
                                    case
                                        "arch target> class_minion_grunt eq arch target> class_minion_small eq || arch target> class_minion_pets eq || arch target> class_minion_swarm eq || arch source> class_scrapper == &&"
                                        :
                                    case
                                        "arch target> class_minion_grunt eq arch target> class_minion_small eq || arch target> class_minion_pets eq || arch target> class_minion_swarm eq ||"
                                        :
                                        SpecialCase = Enums.eSpecialCase.CriticalMinion;
                                        goto label_101;
                                }

                                SpecialCase = Enums.eSpecialCase.CriticalHit;
                            }
                            else if ((lower.Contains("arch source> class_controller eq") ||
                                      lower.Contains("arch source> class_controller ==".ToLower())) &&
                                     lower.Contains("kheld") && EffectType == Enums.eEffectType.Damage)
                            {
                                SpecialCase = Enums.eSpecialCase.Containment;
                            }
                            else if (lower.Contains("kmeter source> .9") && lower.Contains("kheld"))
                            {
                                SpecialCase = Enums.eSpecialCase.Mezzed;
                            }
                            else if (lower.Contains("kmeter source> 0"))
                            {
                                SpecialCase = Enums.eSpecialCase.Assassination;
                            }
                            else if (lower.Contains("arch source> class_corruptor eq") && lower.Contains("khitpoints%"))
                            {
                                SpecialCase = Enums.eSpecialCase.Scourge;
                            }
                            else if (lower.Contains("arch source> class_dominator") &&
                                     !lower.Contains("arch source> class_dominator eq !") &&
                                     lower.Contains("kstealth source>"))
                            {
                                SpecialCase = Enums.eSpecialCase.Domination;
                            }
                            else if (lower.Contains("khitpoints%"))
                            {
                                SpecialCase = Enums.eSpecialCase.Scourge;
                            }
                            else if (lower.Contains("kdd"))
                            {
                                SpecialCase = Enums.eSpecialCase.Combo;
                            }
                        }
                    }

                label_101:
                    if (lower.Contains("Electronic target.HasTag?".ToLower()))
                        SpecialCase = Enums.eSpecialCase.Robot;
                    if (lower.IndexOf("source.TeamSize> 1", StringComparison.OrdinalIgnoreCase) > -1)
                        SpecialCase = Enums.eSpecialCase.TeamSize1;
                    else if (lower.IndexOf("source.TeamSize> 2", StringComparison.OrdinalIgnoreCase) > -1)
                        SpecialCase = Enums.eSpecialCase.TeamSize2;
                    else if (lower.IndexOf("source.TeamSize> 3", StringComparison.OrdinalIgnoreCase) > -1)
                        SpecialCase = Enums.eSpecialCase.TeamSize3;
                    else if (lower.IndexOf(
                        "Temporary_Powers.Temporary_Powers.Combo_Level_1 source.ownPower? ! Temporary_Powers.Temporary_Powers.Combo_Level_2 source.ownPower? ! && Temporary_Powers.Temporary_Powers.Combo_Level_3 source.ownPower? !",
                        StringComparison.OrdinalIgnoreCase) > -1)
                        SpecialCase = Enums.eSpecialCase.ComboLevel0;
                    else if (lower.IndexOf("Temporary_Powers.Temporary_Powers.Combo_Level_1 source.ownPower?",
                        StringComparison.OrdinalIgnoreCase) > -1)
                        SpecialCase = Enums.eSpecialCase.ComboLevel1;
                    else if (lower.IndexOf("Temporary_Powers.Temporary_Powers.Combo_Level_2 source.ownPower?",
                        StringComparison.OrdinalIgnoreCase) > -1)
                        SpecialCase = Enums.eSpecialCase.ComboLevel2;
                    else if (lower.IndexOf("Temporary_Powers.Temporary_Powers.Combo_Level_3 source.ownPower?",
                        StringComparison.OrdinalIgnoreCase) > -1)
                        SpecialCase = Enums.eSpecialCase.ComboLevel3;
                    else if (lower.IndexOf(
                        "Temporary_Powers.Temporary_Powers.Perfection_of_Body_Level_1 source.ownPower? ! Temporary_Powers.Temporary_Powers.Perfection_of_Body_Level_2 source.ownPower? ! && Temporary_Powers.Temporary_Powers.Perfection_of_Body_Level_3 source.ownPower? !",
                        StringComparison.OrdinalIgnoreCase) > -1)
                        SpecialCase = Enums.eSpecialCase.PerfectionOfBody0;
                    else if (lower.IndexOf(
                        "Temporary_Powers.Temporary_Powers.Perfection_of_Body_Level_1 source.ownPower?",
                        StringComparison.OrdinalIgnoreCase) > -1)
                        SpecialCase = Enums.eSpecialCase.PerfectionOfBody1;
                    else if (lower.IndexOf(
                        "Temporary_Powers.Temporary_Powers.Perfection_of_Body_Level_2 source.ownPower?",
                        StringComparison.OrdinalIgnoreCase) > -1)
                        SpecialCase = Enums.eSpecialCase.PerfectionOfBody2;
                    else if (lower.IndexOf(
                        "Temporary_Powers.Temporary_Powers.Perfection_of_Body_Level_3 source.ownPower?",
                        StringComparison.OrdinalIgnoreCase) > -1)
                        SpecialCase = Enums.eSpecialCase.PerfectionOfBody3;
                    else if (lower.IndexOf(
                        "Temporary_Powers.Temporary_Powers.Perfection_of_Mind_Level_1 source.ownPower? ! Temporary_Powers.Temporary_Powers.Perfection_of_Mind_Level_2 source.ownPower? ! && Temporary_Powers.Temporary_Powers.Perfection_of_Mind_Level_3 source.ownPower? !",
                        StringComparison.OrdinalIgnoreCase) > -1)
                        SpecialCase = Enums.eSpecialCase.PerfectionOfMind0;
                    else if (lower.IndexOf(
                        "Temporary_Powers.Temporary_Powers.Perfection_of_Mind_Level_1 source.ownPower?",
                        StringComparison.OrdinalIgnoreCase) > -1)
                        SpecialCase = Enums.eSpecialCase.PerfectionOfMind1;
                    else if (lower.IndexOf(
                        "Temporary_Powers.Temporary_Powers.Perfection_of_Mind_Level_2 source.ownPower?",
                        StringComparison.OrdinalIgnoreCase) > -1)
                        SpecialCase = Enums.eSpecialCase.PerfectionOfMind2;
                    else if (lower.IndexOf(
                        "Temporary_Powers.Temporary_Powers.Perfection_of_Mind_Level_3 source.ownPower?",
                        StringComparison.OrdinalIgnoreCase) > -1)
                        SpecialCase = Enums.eSpecialCase.PerfectionOfMind3;
                    else if (lower.IndexOf(
                        "Temporary_Powers.Temporary_Powers.Perfection_of_Soul_Level_1 source.ownPower? ! Temporary_Powers.Temporary_Powers.Perfection_of_Soul_Level_2 source.ownPower? ! && Temporary_Powers.Temporary_Powers.Perfection_of_Soul_Level_3 source.ownPower? !",
                        StringComparison.OrdinalIgnoreCase) > -1)
                        SpecialCase = Enums.eSpecialCase.PerfectionOfSoul0;
                    else if (lower.IndexOf(
                        "Temporary_Powers.Temporary_Powers.Perfection_of_Soul_Level_1 source.ownPower?",
                        StringComparison.OrdinalIgnoreCase) > -1)
                        SpecialCase = Enums.eSpecialCase.PerfectionOfSoul1;
                    else if (lower.IndexOf(
                        "Temporary_Powers.Temporary_Powers.Perfection_of_Soul_Level_2 source.ownPower?",
                        StringComparison.OrdinalIgnoreCase) > -1)
                        SpecialCase = Enums.eSpecialCase.PerfectionOfSoul2;
                    else if (lower.IndexOf(
                        "Temporary_Powers.Temporary_Powers.Perfection_of_Soul_Level_3 source.ownPower?",
                        StringComparison.OrdinalIgnoreCase) > -1)
                        SpecialCase = Enums.eSpecialCase.PerfectionOfSoul3;
                    else if (lower.IndexOf("temporary_powers.temporary_powers.tidal_power source.ownPowerNum? 0 ==",
                        StringComparison.OrdinalIgnoreCase) > -1)
                        SpecialCase = Enums.eSpecialCase.ComboLevel0;
                    else if (lower.IndexOf("temporary_powers.temporary_powers.tidal_power source.ownPowerNum? 1 ==",
                        StringComparison.OrdinalIgnoreCase) > -1)
                        SpecialCase = Enums.eSpecialCase.ComboLevel1;
                    else if (lower.IndexOf("temporary_powers.temporary_powers.tidal_power source.ownPowerNum? 2 ==",
                        StringComparison.OrdinalIgnoreCase) > -1)
                        SpecialCase = Enums.eSpecialCase.ComboLevel2;
                    else if (lower.IndexOf("temporary_powers.temporary_powers.tidal_power source.ownPowerNum? 3 ==",
                        StringComparison.OrdinalIgnoreCase) > -1)
                        SpecialCase = Enums.eSpecialCase.ComboLevel3;
                    else if (lower.IndexOf("temporary_powers.temporary_powers.tidal_power source.ownPowerNum? 2 <=",
                        StringComparison.OrdinalIgnoreCase) > -1)
                        SpecialCase = Enums.eSpecialCase.NotComboLevel3;
                    else if (lower.IndexOf("cur.kToHit source> .97 >=", StringComparison.OrdinalIgnoreCase) > -1)
                        SpecialCase = Enums.eSpecialCase.ToHit97;
                    else if (lower.Contains("temporary_powers.temporary_powers.time_crawl_debuff target.ownpower? !"))
                        SpecialCase = Enums.eSpecialCase.NotDelayed;
                    else if (lower.Contains("temporary_powers.temporary_powers.time_crawl_debuff target.ownpower?"))
                        SpecialCase = Enums.eSpecialCase.Delayed;
                    else if (lower.Contains(
                        "temporary_powers.temporary_powers.temporal_selection_buff target.ownpower? !"))
                        SpecialCase = Enums.eSpecialCase.NotAccelerated;
                    else if (lower.Contains(
                        "temporary_powers.temporary_powers.temporal_selection_buff target.ownpower?"))
                        SpecialCase = Enums.eSpecialCase.Accelerated;
                    else if (lower.Contains("temporary_powers.temporary_powers.beam_rifle_debuff target.ownpower? !"))
                        SpecialCase = Enums.eSpecialCase.NotDisintegrated;
                    else if (lower.Contains("temporary_powers.temporary_powers.beam_rifle_debuff target.ownpower?"))
                        SpecialCase = Enums.eSpecialCase.Disintegrated;
                    else if (lower.Contains("kfastmode source.mode?"))
                        SpecialCase = Enums.eSpecialCase.FastMode;
                    else if (lower.IndexOf("kOffensiveAdaptation source.Mode? ! kDefensiveAdaptation source.Mode? ! &&",
                        StringComparison.OrdinalIgnoreCase) > -1)
                        SpecialCase = Enums.eSpecialCase.NotDefensiveNorOffensiveAdaptation;
                    else if (lower.IndexOf("kDefensiveAdaptation source.Mode? !", StringComparison.OrdinalIgnoreCase) >
                             -1)
                        SpecialCase = Enums.eSpecialCase.NotDefensiveAdaptation;
                    else if (lower.IndexOf("kDefensiveAdaptation source.Mode?", StringComparison.OrdinalIgnoreCase) >
                             -1)
                        SpecialCase = Enums.eSpecialCase.DefensiveAdaptation;
                    else if (lower.IndexOf("kRestedAdaptation source.Mode?", StringComparison.OrdinalIgnoreCase) > -1)
                        SpecialCase = Enums.eSpecialCase.EfficientAdaptation;
                    else if (lower.IndexOf("kOffensiveAdaptation source.Mode?", StringComparison.OrdinalIgnoreCase) >
                             -1)
                        SpecialCase = Enums.eSpecialCase.OffensiveAdaptation;
                    if (!string.IsNullOrEmpty(lower) &&
                        (!lower.Contains("!") || lower.Contains("Raid target.HasTag?".ToLower())) &&
                        UidClassRegex.IsMatch(array[26]))
                    {
                        UIDClassName = UidClassRegex.Matches(array[26])[0].Groups[2].Value;
                        nIDClassName = DatabaseAPI.NidFromUidClass(UIDClassName);
                    }

                    PvMode =
                        !lower.Contains("entref target> entref source> eq ! enttype target> player eq &&".ToLower())
                            ? !(lower.Contains("enttype target> player eq || !".ToLower()) |
                                (SpecialCase == Enums.eSpecialCase.CriticalMinion))
                                ? !lower.Contains("enttype target> critter eq".ToLower())
                                    ? !lower.Contains("enttype target> player eq".ToLower())
                                        ? !lower.Contains("isPVPMap? !".ToLower())
                                            ? !lower.Contains("isPVPMap?".ToLower()) ? Enums.ePvX.Any : Enums.ePvX.PvP
                                            : Enums.ePvX.PvE
                                        : Enums.ePvX.PvP
                                    : Enums.ePvX.PvE
                                : Enums.ePvX.PvE
                            : Enums.ePvX.Any;
                    if (lower.Contains("@ToHitRoll".ToLower()))
                    {
                        RequiresToHitCheck = true;
                        if (lower.Contains("Raid target.HasTag? @ToHitRoll".ToLower()))
                            SpecialCase = Enums.eSpecialCase.VersusSpecial;
                    }

                    switch (array[4].ToLower())
                    {
                        case "self":
                            ToWho = Enums.eToWho.Self;
                            goto default;
                        case "target":
                            ToWho = power == null ? Enums.eToWho.Target :
                                (power.EntitiesAutoHit & Enums.eEntity.Caster) != Enums.eEntity.Caster ||
                                lower == "entref target> entref source> eq !" ? Enums.eToWho.Target : Enums.eToWho.All;
                            goto default;
                        case null:
                            var ps = power?.GetPowerSet();
                            if (power != null && ps != null)
                            {
                                if (string.Equals(ps.ATClass, "CLASS_BLASTER", StringComparison.OrdinalIgnoreCase))
                                {
                                    nModifierTable = DatabaseAPI.NidFromUidAttribMod(ModifierTable);
                                    if ((EffectType == Enums.eEffectType.DamageBuff) & (Scale < 1.0) && Scale > 0.0 &&
                                        ToWho == Enums.eToWho.Self && SpecialCase == Enums.eSpecialCase.None)
                                        SpecialCase = Enums.eSpecialCase.Defiance;
                                }
                                else if (ps.SetType == Enums.ePowerSetType.Inherent &&
                                         EffectType == Enums.eEffectType.DamageBuff &&
                                         (AttribType == Enums.eAttribType.Expression) &
                                         (Math.Abs(Scale - 0.0f) < 0.01) && power.Requires.ClassName.Length > 0 &&
                                         string.Equals(power.Requires.ClassName[0], "CLASS_BRUTE",
                                             StringComparison.OrdinalIgnoreCase))
                                {
                                    Stacking = Enums.eStacking.Yes;
                                    AttribType = Enums.eAttribType.Magnitude;
                                    Scale = 0.02f;
                                }
                            }

                            flag = true;
                            break;
                        default:
                            goto case null;
                    }
                }
            }

            return flag;
        }

        public int SetTicks(float iDuration, float iInterval)
        {
            Ticks = 0;
            if (iInterval > 0.0)
                Ticks = (int)(1.0 + Math.Floor(iDuration / (double)iInterval));
            return Ticks;
        }

        public bool ValidateConditional(string cPowername)
        {
            if (ActiveConditionals.Count <= 0)
            {
                return false;
            }
            var getCondition = new Regex("(:.*)");
            var getConditionItem = new Regex("(.*:)");
            foreach (var cVp in ActiveConditionals)
            {
                var condition = getCondition.Replace(cVp.Key, "");
                var conditionItemName = getConditionItem.Replace(cVp.Key, "").Replace(":", "");
                var conditionPower = DatabaseAPI.GetPowerByFullName(conditionItemName);
                var buildPowers = MidsContext.Character.CurrentBuild.Powers;
                var cVal = cVp.Value.Split(' ');
                var powerDisplayName = conditionPower?.DisplayName;
                if (powerDisplayName == null || !powerDisplayName.Contains(cPowername))
                {
                    return false;
                }
                switch (condition)
                {
                    case "Active":
                        if (conditionPower != null)
                        {
                            cVp.Validated = conditionPower.Active.Equals(Convert.ToBoolean(cVp.Value));
                        }

                        break;
                    case "Taken":
                        if (conditionPower != null)
                        {
                            cVp.Validated = MidsContext.Character.CurrentBuild.PowerUsed(conditionPower)
                                .Equals(Convert.ToBoolean(cVp.Value));
                        }

                        break;
                    case "Stacks":
                        if (conditionPower != null)
                        {
                            var stacks = buildPowers.Where(x => x.Power == conditionPower).Select(x => x.Power.Stacks)
                                .ToList();
                            switch (cVal[0])
                            {
                                case "=":

                                    cVp.Validated = stacks[0].Equals(Convert.ToInt32(cVal[1]));

                                    break;
                                case ">":
                                    cVp.Validated = stacks[0] > Convert.ToInt32(cVal[1]);

                                    break;
                                case "<":
                                    cVp.Validated = stacks[0] < Convert.ToInt32(cVal[1]);

                                    break;
                            }
                        }

                        break;
                    case "Team":
                        switch (cVal[0])
                        {
                            case "=":
                                if (MidsContext.Config.TeamMembers.ContainsKey(conditionItemName) && MidsContext
                                    .Config.TeamMembers[conditionItemName].Equals(Convert.ToInt32(cVal[1])))
                                {
                                    cVp.Validated = true;
                                }
                                else
                                {
                                    cVp.Validated = false;
                                }

                                break;
                            case ">":
                                if (MidsContext.Config.TeamMembers.ContainsKey(conditionItemName) &&
                                    MidsContext.Config.TeamMembers[conditionItemName] >
                                    Convert.ToInt32(cVal[1]))
                                {
                                    cVp.Validated = true;
                                }
                                else
                                {
                                    cVp.Validated = false;
                                }

                                break;
                            case "<":
                                if (MidsContext.Config.TeamMembers.ContainsKey(conditionItemName) &&
                                    MidsContext.Config.TeamMembers[conditionItemName] <
                                    Convert.ToInt32(cVal[1]))
                                {
                                    cVp.Validated = true;
                                }
                                else
                                {
                                    cVp.Validated = false;
                                }

                                break;
                        }

                        break;
                }
            }

            var validCount = ActiveConditionals.Count(b => b.Validated);
            var invalidCount = ActiveConditionals.Count(b => !b.Validated);
            if (ActiveConditionals.Count > 0)
            {
                Validated = validCount == ActiveConditionals.Count;
            }

            return Validated;
        }

        public bool ValidateConditional(string cType, string cPowername)
        {
            var getCondition = new Regex("(:.*)");
            var getConditionItem = new Regex("(.*:)");
            foreach (var cVp in ActiveConditionals)
            {
                var condition = getCondition.Replace(cVp.Key, "");
                var conditionItemName = getConditionItem.Replace(cVp.Key, "").Replace(":", "");
                var conditionPower = DatabaseAPI.GetPowerByFullName(conditionItemName);
                var cVal = cVp.Value.Split(' ');
                var powerDisplayName = conditionPower?.DisplayName;
                if (powerDisplayName == null || !powerDisplayName.Contains(cPowername))
                {
                    return false;
                }

                if (string.Equals(cType, condition, StringComparison.CurrentCultureIgnoreCase) && condition == "Active")
                {
                    cVp.Validated = conditionPower.Active.Equals(Convert.ToBoolean(cVp.Value));
                }
                else if (string.Equals(cType, condition, StringComparison.CurrentCultureIgnoreCase) &&
                         condition == "Taken")
                {
                    cVp.Validated = MidsContext.Character.CurrentBuild.PowerUsed(conditionPower)
                        .Equals(Convert.ToBoolean(cVp.Value));
                }
                else if (string.Equals(cType, condition, StringComparison.CurrentCultureIgnoreCase) &&
                         condition == "Stacks")
                {
                    switch (cVal[0])
                    {
                        case "=":

                            cVp.Validated = conditionPower.Stacks.Equals(Convert.ToInt32(cVal[1]));

                            break;
                        case ">":
                            cVp.Validated = conditionPower.Stacks > Convert.ToInt32(cVal[1]);

                            break;
                        case "<":
                            cVp.Validated = conditionPower.Stacks < Convert.ToInt32(cVal[1]);

                            break;
                    }
                }
                else if (string.Equals(cType, condition, StringComparison.CurrentCultureIgnoreCase) &&
                         condition == "Team")
                {
                    switch (cVal[0])
                    {
                        case "=":
                            if (MidsContext.Config.TeamMembers.ContainsKey(conditionItemName) && MidsContext
                                .Config.TeamMembers[conditionItemName].Equals(Convert.ToInt32(cVal[1])))
                            {
                                cVp.Validated = true;
                            }
                            else
                            {
                                cVp.Validated = false;
                            }

                            break;
                        case ">":
                            if (MidsContext.Config.TeamMembers.ContainsKey(conditionItemName) &&
                                MidsContext.Config.TeamMembers[conditionItemName] >
                                Convert.ToInt32(cVal[1]))
                            {
                                cVp.Validated = true;
                            }
                            else
                            {
                                cVp.Validated = false;
                            }

                            break;
                        case "<":
                            if (MidsContext.Config.TeamMembers.ContainsKey(conditionItemName) &&
                                MidsContext.Config.TeamMembers[conditionItemName] <
                                Convert.ToInt32(cVal[1]))
                            {
                                cVp.Validated = true;
                            }
                            else
                            {
                                cVp.Validated = false;
                            }

                            break;
                    }
                }
            }

            var validCount = ActiveConditionals.Count(b => b.Validated);
            if (ActiveConditionals.Count > 0)
            {
                Validated = validCount == ActiveConditionals.Count;
            }

            return Validated;
        }

        public bool ValidateConditional()
        {
            var getCondition = new Regex("(:.*)");
            var getConditionItem = new Regex("(.*:)");
            foreach (var cVp in ActiveConditionals)
            {
                var condition = getCondition.Replace(cVp.Key, "");
                var conditionItemName = getConditionItem.Replace(cVp.Key, "").Replace(":", "");
                var conditionPower = DatabaseAPI.GetPowerByFullName(conditionItemName);
                var cVal = cVp.Value.Split(' ');
                switch (condition)
                {
                    case "Active":
                        if (conditionPower != null)
                        {
                            cVp.Validated = conditionPower.Active.Equals(Convert.ToBoolean(cVp.Value));
                        }

                        break;
                    case "Taken":
                        if (conditionPower != null)
                        {
                            cVp.Validated = MidsContext.Character.CurrentBuild.PowerUsed(conditionPower)
                                .Equals(Convert.ToBoolean(cVp.Value));
                        }

                        break;
                    case "Stacks":
                        if (conditionPower != null)
                        {
                            switch (cVal[0])
                            {
                                case "=":

                                    cVp.Validated = conditionPower.Stacks.Equals(Convert.ToInt32(cVal[1]));

                                    break;
                                case ">":
                                    cVp.Validated = conditionPower.Stacks > Convert.ToInt32(cVal[1]);

                                    break;
                                case "<":
                                    cVp.Validated = conditionPower.Stacks < Convert.ToInt32(cVal[1]);

                                    break;
                            }
                        }

                        break;
                    case "Team":
                        switch (cVal[0])
                        {
                            case "=":
                                if (MidsContext.Config.TeamMembers.ContainsKey(conditionItemName) && MidsContext
                                    .Config.TeamMembers[conditionItemName].Equals(Convert.ToInt32(cVal[1])))
                                {
                                    cVp.Validated = true;
                                }
                                else
                                {
                                    cVp.Validated = false;
                                }

                                break;
                            case ">":
                                if (MidsContext.Config.TeamMembers.ContainsKey(conditionItemName) &&
                                    MidsContext.Config.TeamMembers[conditionItemName] >
                                    Convert.ToInt32(cVal[1]))
                                {
                                    cVp.Validated = true;
                                }
                                else
                                {
                                    cVp.Validated = false;
                                }

                                break;
                            case "<":
                                if (MidsContext.Config.TeamMembers.ContainsKey(conditionItemName) &&
                                    MidsContext.Config.TeamMembers[conditionItemName] <
                                    Convert.ToInt32(cVal[1]))
                                {
                                    cVp.Validated = true;
                                }
                                else
                                {
                                    cVp.Validated = false;
                                }

                                break;
                        }

                        break;
                }
            }

            var validCount = ActiveConditionals.Count(b => b.Validated);
            var invalidCount = ActiveConditionals.Count(b => !b.Validated);
            if (ActiveConditionals.Count > 0)
            {
                Validated = validCount == ActiveConditionals.Count;
                if (Validated)
                {
                    return true;
                }
            }

            return false;
        }
        public void UpdateAttrib()
        {
            switch (PowerAttribs)
            {
                case Enums.ePowerAttribs.Accuracy:
                    var conditionsMet = ValidateConditional();
                    power.Accuracy = conditionsMet ? AtrModAccuracy : AtrOrigAccuracy;
                    break;
                case Enums.ePowerAttribs.ActivateInterval:
                    conditionsMet = ValidateConditional();
                    power.ActivatePeriod =
                        conditionsMet ? AtrModActivatePeriod : AtrOrigActivatePeriod;
                    break;
                case Enums.ePowerAttribs.Arc:
                    conditionsMet = ValidateConditional();
                    power.Arc = conditionsMet ? AtrModArc : AtrOrigArc;
                    break;
                case Enums.ePowerAttribs.CastTime:
                    conditionsMet = ValidateConditional();
                    power.CastTime = conditionsMet ? AtrModCastTime : AtrOrigCastTime;
                    break;
                case Enums.ePowerAttribs.EffectArea:
                    conditionsMet = ValidateConditional();
                    power.EffectArea = conditionsMet ? AtrModEffectArea : AtrOrigEffectArea;
                    break;
                case Enums.ePowerAttribs.EnduranceCost:
                    conditionsMet = ValidateConditional();
                    power.EndCost = conditionsMet ? AtrModEnduranceCost : AtrOrigEnduranceCost;
                    break;
                case Enums.ePowerAttribs.InterruptTime:
                    conditionsMet = ValidateConditional();
                    power.InterruptTime = conditionsMet ? AtrModInterruptTime : AtrOrigInterruptTime;
                    break;
                case Enums.ePowerAttribs.MaxTargets:
                    conditionsMet = ValidateConditional();
                    power.MaxTargets = conditionsMet ? AtrModMaxTargets : AtrOrigMaxTargets;
                    break;
                case Enums.ePowerAttribs.Radius:
                    conditionsMet = ValidateConditional();
                    power.Radius = conditionsMet ? AtrModRadius : AtrOrigRadius;
                    break;
                case Enums.ePowerAttribs.Range:
                    conditionsMet = ValidateConditional();
                    power.Range = conditionsMet ? AtrModRange : AtrOrigRange;
                    break;
                case Enums.ePowerAttribs.RechargeTime:
                    conditionsMet = ValidateConditional();
                    power.RechargeTime = conditionsMet ? AtrModRechargeTime : AtrOrigRechargeTime;
                    break;
                case Enums.ePowerAttribs.SecondaryRange:
                    conditionsMet = ValidateConditional();
                    power.RangeSecondary = conditionsMet ? AtrModSecondaryRange : AtrOrigSecondaryRange;
                    break;
            }

        }
        public bool CanInclude()
        {
            if (MidsContext.Character == null | ActiveConditionals == null | ActiveConditionals?.Count == 0)
                return true;
            #region SpecialCase Processing
            switch (SpecialCase)
            {
                case Enums.eSpecialCase.Hidden:
                    if (MidsContext.Character.IsStalker || MidsContext.Character.IsArachnos)
                        return true;
                    break;
                case Enums.eSpecialCase.Domination:
                    if (MidsContext.Character.Domination)
                        return true;
                    break;
                case Enums.eSpecialCase.Scourge:
                    if (MidsContext.Character.Scourge)
                        return true;
                    break;
                case Enums.eSpecialCase.CriticalHit:
                    if (MidsContext.Character.CriticalHits || MidsContext.Character.IsStalker)
                        return true;
                    break;
                case Enums.eSpecialCase.CriticalBoss:
                    if (MidsContext.Character.CriticalHits)
                        return true;
                    break;
                case Enums.eSpecialCase.Assassination:
                    if (MidsContext.Character.IsStalker && MidsContext.Character.Assassination)
                        return true;
                    break;
                case Enums.eSpecialCase.Containment:
                    if (MidsContext.Character.Containment)
                        return true;
                    break;
                case Enums.eSpecialCase.Defiance:
                    if (MidsContext.Character.Defiance)
                        return true;
                    break;
                case Enums.eSpecialCase.TargetDroneActive:
                    if (MidsContext.Character.IsBlaster && MidsContext.Character.TargetDroneActive)
                        return true;
                    break;
                case Enums.eSpecialCase.NotDisintegrated:
                    if (!MidsContext.Character.DisintegrateActive)
                        return true;
                    break;
                case Enums.eSpecialCase.Disintegrated:
                    if (MidsContext.Character.DisintegrateActive)
                        return true;
                    break;
                case Enums.eSpecialCase.NotAccelerated:
                    if (!MidsContext.Character.AcceleratedActive)
                        return true;
                    break;
                case Enums.eSpecialCase.Accelerated:
                    if (MidsContext.Character.AcceleratedActive)
                        return true;
                    break;
                case Enums.eSpecialCase.NotDelayed:
                    if (!MidsContext.Character.DelayedActive)
                        return true;
                    break;
                case Enums.eSpecialCase.Delayed:
                    if (MidsContext.Character.DelayedActive)
                        return true;
                    break;
                case Enums.eSpecialCase.ComboLevel0:
                    if (MidsContext.Character.ActiveComboLevel == 0)
                        return true;
                    break;
                case Enums.eSpecialCase.ComboLevel1:
                    if (MidsContext.Character.ActiveComboLevel == 1)
                        return true;
                    break;
                case Enums.eSpecialCase.ComboLevel2:
                    if (MidsContext.Character.ActiveComboLevel == 2)
                        return true;
                    break;
                case Enums.eSpecialCase.ComboLevel3:
                    if (MidsContext.Character.ActiveComboLevel == 3)
                        return true;
                    break;
                case Enums.eSpecialCase.FastMode:
                    if (MidsContext.Character.FastModeActive)
                        return true;
                    break;
                case Enums.eSpecialCase.NotAssassination:
                    if (!MidsContext.Character.Assassination)
                        return true;
                    break;
                case Enums.eSpecialCase.PerfectionOfBody0:
                    if (MidsContext.Character.PerfectionOfBodyLevel == 0)
                        return true;
                    break;
                case Enums.eSpecialCase.PerfectionOfBody1:
                    if (MidsContext.Character.PerfectionOfBodyLevel == 1)
                        return true;
                    break;
                case Enums.eSpecialCase.PerfectionOfBody2:
                    if (MidsContext.Character.PerfectionOfBodyLevel == 2)
                        return true;
                    break;
                case Enums.eSpecialCase.PerfectionOfBody3:
                    if (MidsContext.Character.PerfectionOfBodyLevel == 3)
                        return true;
                    break;
                case Enums.eSpecialCase.PerfectionOfMind0:
                    if (MidsContext.Character.PerfectionOfMindLevel == 0)
                        return true;
                    break;
                case Enums.eSpecialCase.PerfectionOfMind1:
                    if (MidsContext.Character.PerfectionOfMindLevel == 1)
                        return true;
                    break;
                case Enums.eSpecialCase.PerfectionOfMind2:
                    if (MidsContext.Character.PerfectionOfMindLevel == 2)
                        return true;
                    break;
                case Enums.eSpecialCase.PerfectionOfMind3:
                    if (MidsContext.Character.PerfectionOfMindLevel == 3)
                        return true;
                    break;
                case Enums.eSpecialCase.PerfectionOfSoul0:
                    if (MidsContext.Character.PerfectionOfSoulLevel == 0)
                        return true;
                    break;
                case Enums.eSpecialCase.PerfectionOfSoul1:
                    if (MidsContext.Character.PerfectionOfSoulLevel == 1)
                        return true;
                    break;
                case Enums.eSpecialCase.PerfectionOfSoul2:
                    if (MidsContext.Character.PerfectionOfSoulLevel == 2)
                        return true;
                    break;
                case Enums.eSpecialCase.PerfectionOfSoul3:
                    if (MidsContext.Character.PerfectionOfSoulLevel == 3)
                        return true;
                    break;
                case Enums.eSpecialCase.TeamSize1:
                    if (MidsContext.Config.TeamSize > 1)
                        return true;
                    break;
                case Enums.eSpecialCase.TeamSize2:
                    if (MidsContext.Config.TeamSize > 2)
                        return true;
                    break;
                case Enums.eSpecialCase.TeamSize3:
                    if (MidsContext.Config.TeamSize > 3)
                        return true;
                    break;
                case Enums.eSpecialCase.NotComboLevel3:
                    if (MidsContext.Character.ActiveComboLevel != 3)
                        return true;
                    break;
                case Enums.eSpecialCase.ToHit97:
                    if (MidsContext.Character.DisplayStats.BuffToHit >= 22.0)
                        return true;
                    break;
                case Enums.eSpecialCase.DefensiveAdaptation:
                    if (MidsContext.Character.DefensiveAdaptation)
                        return true;
                    break;
                case Enums.eSpecialCase.EfficientAdaptation:
                    if (MidsContext.Character.EfficientAdaptation)
                        return true;
                    break;
                case Enums.eSpecialCase.OffensiveAdaptation:
                    if (MidsContext.Character.OffensiveAdaptation)
                        return true;
                    break;
                case Enums.eSpecialCase.NotDefensiveAdaptation:
                    if (!MidsContext.Character.DefensiveAdaptation)
                        return true;
                    break;
                case Enums.eSpecialCase.NotDefensiveNorOffensiveAdaptation:
                    if (!MidsContext.Character.OffensiveAdaptation && !MidsContext.Character.DefensiveAdaptation)
                        return true;
                    break;
                case Enums.eSpecialCase.BoxingBuff:
                    if (MidsContext.Character.BoxingBuff)
                        return true;
                    break;
                case Enums.eSpecialCase.NotBoxingBuff:
                    if (MidsContext.Character.NotBoxingBuff)
                        return true;
                    break;
                case Enums.eSpecialCase.KickBuff:
                    if (MidsContext.Character.KickBuff)
                        return true;
                    break;
                case Enums.eSpecialCase.NotKickBuff:
                    if (MidsContext.Character.NotKickBuff)
                        return true;
                    break;
                case Enums.eSpecialCase.CrossPunchBuff:
                    if (MidsContext.Character.CrossPunchBuff)
                        return true;
                    break;
                case Enums.eSpecialCase.NotCrossPunchBuff:
                    if (MidsContext.Character.NotCrossPunchBuff)
                        return true;
                    break;
                case Enums.eSpecialCase.Supremacy:
                    if (MidsContext.Character.Supremacy && !MidsContext.Character.PackMentality)
                        return true;
                    break;
                case Enums.eSpecialCase.SupremacyAndBuffPwr:
                    if (MidsContext.Character.Supremacy && MidsContext.Character.PackMentality)
                        return true;
                    break;
                case Enums.eSpecialCase.PetTier2:
                    if (MidsContext.Character.PetTier2)
                        return true;
                    break;
                case Enums.eSpecialCase.PetTier3:
                    if (MidsContext.Character.PetTier3)
                        return true;
                    break;
                case Enums.eSpecialCase.PackMentality:
                    if (MidsContext.Character.PackMentality)
                        return true;
                    break;
                case Enums.eSpecialCase.NotPackMentality:
                    if (!MidsContext.Character.PackMentality)
                        return true;
                    break;
                case Enums.eSpecialCase.FastSnipe:
                    if (MidsContext.Character.FastSnipe)
                        return true;
                    break;
                case Enums.eSpecialCase.NotFastSnipe:
                    if (!MidsContext.Character.FastSnipe)
                        return true;
                    break;
            }
            #endregion

            #region Conditional Processing
            var getCondition = new Regex("(:.*)");
            var getConditionItem = new Regex("(.*:)");
            foreach (var cVp in ActiveConditionals)
            {
                var condition = getCondition.Replace(cVp.Key, "");
                var conditionItemName = getConditionItem.Replace(cVp.Key, "").Replace(":", "");
                var conditionPower = DatabaseAPI.GetPowerByFullName(conditionItemName);
                var cVal = cVp.Value.Split(' ');
                switch (condition)
                {
                    case "Active":
                        if (conditionPower != null)
                        {
                            cVp.Validated = conditionPower.Active.Equals(Convert.ToBoolean(cVp.Value));
                        }

                        break;
                    case "Taken":
                        if (conditionPower != null)
                        {
                            cVp.Validated = MidsContext.Character.CurrentBuild.PowerUsed(conditionPower)
                                .Equals(Convert.ToBoolean(cVp.Value));
                        }

                        break;
                    case "Stacks":
                        if (conditionPower != null)
                        {
                            switch (cVal[0])
                            {
                                case "=":

                                    cVp.Validated = conditionPower.Stacks.Equals(Convert.ToInt32(cVal[1]));

                                    break;
                                case ">":
                                    cVp.Validated = conditionPower.Stacks > Convert.ToInt32(cVal[1]);

                                    break;
                                case "<":
                                    cVp.Validated = conditionPower.Stacks < Convert.ToInt32(cVal[1]);

                                    break;
                            }
                        }

                        break;
                    case "Team":
                        switch (cVal[0])
                        {
                            case "=":
                                if (MidsContext.Config.TeamMembers.ContainsKey(conditionItemName) && MidsContext
                                    .Config.TeamMembers[conditionItemName].Equals(Convert.ToInt32(cVal[1])))
                                {
                                    cVp.Validated = true;
                                }
                                else
                                {
                                    cVp.Validated = false;
                                }

                                break;
                            case ">":
                                if (MidsContext.Config.TeamMembers.ContainsKey(conditionItemName) &&
                                    MidsContext.Config.TeamMembers[conditionItemName] >
                                    Convert.ToInt32(cVal[1]))
                                {
                                    cVp.Validated = true;
                                }
                                else
                                {
                                    cVp.Validated = false;
                                }

                                break;
                            case "<":
                                if (MidsContext.Config.TeamMembers.ContainsKey(conditionItemName) &&
                                    MidsContext.Config.TeamMembers[conditionItemName] <
                                    Convert.ToInt32(cVal[1]))
                                {
                                    cVp.Validated = true;
                                }
                                else
                                {
                                    cVp.Validated = false;
                                }

                                break;
                        }

                        break;
                }
            }

            var validCount = ActiveConditionals.Count(b => b.Validated);
            var invalidCount = ActiveConditionals.Count(b => !b.Validated);
            if (ActiveConditionals.Count > 0)
            {
                Validated = validCount == ActiveConditionals.Count;
                if (Validated)
                {
                    return true;
                }

                if (!Validated)
                {
                    return false;
                }
            }
            #endregion

            return false;
        }

        public bool CanGrantPower()
        {
            if (MidsContext.Character == null | ActiveConditionals == null | ActiveConditionals?.Count == 0)
                return true;
            #region SpecialCase Processing
            switch (SpecialCase)
            {
                case Enums.eSpecialCase.Hidden:
                    if (MidsContext.Character.IsStalker || MidsContext.Character.IsArachnos)
                        return true;
                    break;
                case Enums.eSpecialCase.Domination:
                    if (MidsContext.Character.Domination)
                        return true;
                    break;
                case Enums.eSpecialCase.Scourge:
                    if (MidsContext.Character.Scourge)
                        return true;
                    break;
                case Enums.eSpecialCase.CriticalHit:
                    if (MidsContext.Character.CriticalHits || MidsContext.Character.IsStalker)
                        return true;
                    break;
                case Enums.eSpecialCase.CriticalBoss:
                    if (MidsContext.Character.CriticalHits)
                        return true;
                    break;
                case Enums.eSpecialCase.Assassination:
                    if (MidsContext.Character.IsStalker && MidsContext.Character.Assassination)
                        return true;
                    break;
                case Enums.eSpecialCase.Containment:
                    if (MidsContext.Character.Containment)
                        return true;
                    break;
                case Enums.eSpecialCase.Defiance:
                    if (MidsContext.Character.Defiance)
                        return true;
                    break;
                case Enums.eSpecialCase.TargetDroneActive:
                    if (MidsContext.Character.IsBlaster && MidsContext.Character.TargetDroneActive)
                        return true;
                    break;
                case Enums.eSpecialCase.NotDisintegrated:
                    if (!MidsContext.Character.DisintegrateActive)
                        return true;
                    break;
                case Enums.eSpecialCase.Disintegrated:
                    if (MidsContext.Character.DisintegrateActive)
                        return true;
                    break;
                case Enums.eSpecialCase.NotAccelerated:
                    if (!MidsContext.Character.AcceleratedActive)
                        return true;
                    break;
                case Enums.eSpecialCase.Accelerated:
                    if (MidsContext.Character.AcceleratedActive)
                        return true;
                    break;
                case Enums.eSpecialCase.NotDelayed:
                    if (!MidsContext.Character.DelayedActive)
                        return true;
                    break;
                case Enums.eSpecialCase.Delayed:
                    if (MidsContext.Character.DelayedActive)
                        return true;
                    break;
                case Enums.eSpecialCase.ComboLevel0:
                    if (MidsContext.Character.ActiveComboLevel == 0)
                        return true;
                    break;
                case Enums.eSpecialCase.ComboLevel1:
                    if (MidsContext.Character.ActiveComboLevel == 1)
                        return true;
                    break;
                case Enums.eSpecialCase.ComboLevel2:
                    if (MidsContext.Character.ActiveComboLevel == 2)
                        return true;
                    break;
                case Enums.eSpecialCase.ComboLevel3:
                    if (MidsContext.Character.ActiveComboLevel == 3)
                        return true;
                    break;
                case Enums.eSpecialCase.FastMode:
                    if (MidsContext.Character.FastModeActive)
                        return true;
                    break;
                case Enums.eSpecialCase.NotAssassination:
                    if (!MidsContext.Character.Assassination)
                        return true;
                    break;
                case Enums.eSpecialCase.PerfectionOfBody0:
                    if (MidsContext.Character.PerfectionOfBodyLevel == 0)
                        return true;
                    break;
                case Enums.eSpecialCase.PerfectionOfBody1:
                    if (MidsContext.Character.PerfectionOfBodyLevel == 1)
                        return true;
                    break;
                case Enums.eSpecialCase.PerfectionOfBody2:
                    if (MidsContext.Character.PerfectionOfBodyLevel == 2)
                        return true;
                    break;
                case Enums.eSpecialCase.PerfectionOfBody3:
                    if (MidsContext.Character.PerfectionOfBodyLevel == 3)
                        return true;
                    break;
                case Enums.eSpecialCase.PerfectionOfMind0:
                    if (MidsContext.Character.PerfectionOfMindLevel == 0)
                        return true;
                    break;
                case Enums.eSpecialCase.PerfectionOfMind1:
                    if (MidsContext.Character.PerfectionOfMindLevel == 1)
                        return true;
                    break;
                case Enums.eSpecialCase.PerfectionOfMind2:
                    if (MidsContext.Character.PerfectionOfMindLevel == 2)
                        return true;
                    break;
                case Enums.eSpecialCase.PerfectionOfMind3:
                    if (MidsContext.Character.PerfectionOfMindLevel == 3)
                        return true;
                    break;
                case Enums.eSpecialCase.PerfectionOfSoul0:
                    if (MidsContext.Character.PerfectionOfSoulLevel == 0)
                        return true;
                    break;
                case Enums.eSpecialCase.PerfectionOfSoul1:
                    if (MidsContext.Character.PerfectionOfSoulLevel == 1)
                        return true;
                    break;
                case Enums.eSpecialCase.PerfectionOfSoul2:
                    if (MidsContext.Character.PerfectionOfSoulLevel == 2)
                        return true;
                    break;
                case Enums.eSpecialCase.PerfectionOfSoul3:
                    if (MidsContext.Character.PerfectionOfSoulLevel == 3)
                        return true;
                    break;
                case Enums.eSpecialCase.TeamSize1:
                    if (MidsContext.Config.TeamSize > 1)
                        return true;
                    break;
                case Enums.eSpecialCase.TeamSize2:
                    if (MidsContext.Config.TeamSize > 2)
                        return true;
                    break;
                case Enums.eSpecialCase.TeamSize3:
                    if (MidsContext.Config.TeamSize > 3)
                        return true;
                    break;
                case Enums.eSpecialCase.NotComboLevel3:
                    if (MidsContext.Character.ActiveComboLevel != 3)
                        return true;
                    break;
                case Enums.eSpecialCase.ToHit97:
                    if (MidsContext.Character.DisplayStats.BuffToHit >= 22.0)
                        return true;
                    break;
                case Enums.eSpecialCase.DefensiveAdaptation:
                    if (MidsContext.Character.DefensiveAdaptation)
                        return true;
                    break;
                case Enums.eSpecialCase.EfficientAdaptation:
                    if (MidsContext.Character.EfficientAdaptation)
                        return true;
                    break;
                case Enums.eSpecialCase.OffensiveAdaptation:
                    if (MidsContext.Character.OffensiveAdaptation)
                        return true;
                    break;
                case Enums.eSpecialCase.NotDefensiveAdaptation:
                    if (!MidsContext.Character.DefensiveAdaptation)
                        return true;
                    break;
                case Enums.eSpecialCase.NotDefensiveNorOffensiveAdaptation:
                    if (!MidsContext.Character.OffensiveAdaptation && !MidsContext.Character.DefensiveAdaptation)
                        return true;
                    break;
                case Enums.eSpecialCase.BoxingBuff:
                    if (MidsContext.Character.BoxingBuff)
                        return true;
                    break;
                case Enums.eSpecialCase.NotBoxingBuff:
                    if (MidsContext.Character.NotBoxingBuff)
                        return true;
                    break;
                case Enums.eSpecialCase.KickBuff:
                    if (MidsContext.Character.KickBuff)
                        return true;
                    break;
                case Enums.eSpecialCase.NotKickBuff:
                    if (MidsContext.Character.NotKickBuff)
                        return true;
                    break;
                case Enums.eSpecialCase.CrossPunchBuff:
                    if (MidsContext.Character.CrossPunchBuff)
                        return true;
                    break;
                case Enums.eSpecialCase.NotCrossPunchBuff:
                    if (MidsContext.Character.NotCrossPunchBuff)
                        return true;
                    break;
                case Enums.eSpecialCase.Supremacy:
                    if (MidsContext.Character.Supremacy && !MidsContext.Character.PackMentality)
                        return true;
                    break;
                case Enums.eSpecialCase.SupremacyAndBuffPwr:
                    if (MidsContext.Character.Supremacy && MidsContext.Character.PackMentality)
                        return true;
                    break;
                case Enums.eSpecialCase.PetTier2:
                    if (MidsContext.Character.PetTier2)
                        return true;
                    break;
                case Enums.eSpecialCase.PetTier3:
                    if (MidsContext.Character.PetTier3)
                        return true;
                    break;
                case Enums.eSpecialCase.PackMentality:
                    if (MidsContext.Character.PackMentality)
                        return true;
                    break;
                case Enums.eSpecialCase.NotPackMentality:
                    if (!MidsContext.Character.PackMentality)
                        return true;
                    break;
                case Enums.eSpecialCase.FastSnipe:
                    if (MidsContext.Character.FastSnipe)
                        return true;
                    break;
                case Enums.eSpecialCase.NotFastSnipe:
                    if (!MidsContext.Character.FastSnipe)
                        return true;
                    break;
            }
            #endregion

            #region Conditional Processing
            var getCondition = new Regex("(:.*)");
            var getConditionItem = new Regex("(.*:)");
            foreach (var cVp in ActiveConditionals)
            {
                var condition = getCondition.Replace(cVp.Key, "");
                var conditionItemName = getConditionItem.Replace(cVp.Key, "").Replace(":", "");
                var conditionPower = DatabaseAPI.GetPowerByFullName(conditionItemName);
                var cVal = cVp.Value.Split(' ');
                switch (condition)
                {
                    case "Active":
                        if (conditionPower != null)
                        {
                            cVp.Validated = conditionPower.Active.Equals(Convert.ToBoolean(cVp.Value));
                        }

                        break;
                    case "Taken":
                        if (conditionPower != null)
                        {
                            cVp.Validated = MidsContext.Character.CurrentBuild.PowerUsed(conditionPower)
                                .Equals(Convert.ToBoolean(cVp.Value));
                        }

                        break;
                    case "Stacks":
                        if (conditionPower != null)
                        {
                            switch (cVal[0])
                            {
                                case "=":

                                    cVp.Validated = conditionPower.Stacks.Equals(Convert.ToInt32(cVal[1]));

                                    break;
                                case ">":
                                    cVp.Validated = conditionPower.Stacks > Convert.ToInt32(cVal[1]);

                                    break;
                                case "<":
                                    cVp.Validated = conditionPower.Stacks < Convert.ToInt32(cVal[1]);

                                    break;
                            }
                        }

                        break;
                    case "Team":
                        switch (cVal[0])
                        {
                            case "=":
                                if (MidsContext.Config.TeamMembers.ContainsKey(conditionItemName) && MidsContext
                                    .Config.TeamMembers[conditionItemName].Equals(Convert.ToInt32(cVal[1])))
                                {
                                    cVp.Validated = true;
                                }
                                else
                                {
                                    cVp.Validated = false;
                                }

                                break;
                            case ">":
                                if (MidsContext.Config.TeamMembers.ContainsKey(conditionItemName) &&
                                    MidsContext.Config.TeamMembers[conditionItemName] >
                                    Convert.ToInt32(cVal[1]))
                                {
                                    cVp.Validated = true;
                                }
                                else
                                {
                                    cVp.Validated = false;
                                }

                                break;
                            case "<":
                                if (MidsContext.Config.TeamMembers.ContainsKey(conditionItemName) &&
                                    MidsContext.Config.TeamMembers[conditionItemName] <
                                    Convert.ToInt32(cVal[1]))
                                {
                                    cVp.Validated = true;
                                }
                                else
                                {
                                    cVp.Validated = false;
                                }

                                break;
                        }

                        break;
                }
            }

            var validCount = ActiveConditionals.Count(b => b.Validated);
            var invalidCount = ActiveConditionals.Count(b => !b.Validated);
            if (ActiveConditionals.Count > 0)
            {
                Validated = validCount == ActiveConditionals.Count;
                if (Validated)
                {
                    return true;
                }

                if (!Validated)
                {
                    return false;
                }
            }
            #endregion

            return false;
        }

        public bool PvXInclude()
        {
            return MidsContext.Archetype == null ||
                   (PvMode != Enums.ePvX.PvP && !MidsContext.Config.Inc.DisablePvE ||
                    PvMode != Enums.ePvX.PvE && MidsContext.Config.Inc.DisablePvE) &&
                   (nIDClassName == -1 || nIDClassName == MidsContext.Archetype.Idx);
        }

        public int CompareTo(object obj)
        {
            //Less than zero This instance is less than obj. 
            //Zero This instance is equal to obj. 
            //Greater than zero This instance is greater than obj. 
            //
            //A.CompareTo(A) is required to return zero.
            //
            //If A.CompareTo(B) returns zero then B.CompareTo(A) is required to return zero.
            //
            //If A.CompareTo(B) returns zero and B.CompareTo(C) returns zero then A.CompareTo(C) is required to return zero.
            //
            //If A.CompareTo(B) returns a value other than zero then B.CompareTo(A) is required to return a value of the opposite sign.
            //
            //If A.CompareTo(B) returns a value x not equal to zero, and B.CompareTo(C) returns a value y of the same sign as x, then A.CompareTo(C) is required to return a value of the same sign as x and y.

            if (obj == null)
                return 1;

            if (obj is Effect effect)
            {
                var nVariableFlag = 0;
                if (VariableModified & effect.VariableModified == false)
                {
                    nVariableFlag = 1;
                }
                else if (VariableModified == false & effect.VariableModified)
                {
                    nVariableFlag = -1;
                }

                if (nVariableFlag == 0)
                {
                    if (Suppression < effect.Suppression)
                    {
                        nVariableFlag = 1;
                    }
                    else if (Suppression > effect.Suppression)
                    {
                        nVariableFlag = -1;
                    }
                }

                if (effect.EffectType == Enums.eEffectType.None & EffectType != Enums.eEffectType.None)
                    return -1;
                if (effect.EffectType != Enums.eEffectType.None & EffectType == Enums.eEffectType.None)
                    return 1;

                if (EffectType > effect.EffectType)
                    return 1;
                if (EffectType < effect.EffectType)
                    return -1;

                if (IgnoreED && !effect.IgnoreED)
                    return 1;
                if (!IgnoreED && effect.IgnoreED)
                    return -1;

                if (EffectId != effect.EffectId)
                    return string.CompareOrdinal(EffectId, effect.EffectId);
                if (Reward != effect.Reward)
                    return string.CompareOrdinal(Reward, effect.Reward);
                if (MagnitudeExpression != effect.MagnitudeExpression)
                    return string.CompareOrdinal(MagnitudeExpression, effect.MagnitudeExpression);

                //EffectType is the same, go more detailed.
                if (effect.isDamage())
                {
                    if (DamageType > effect.DamageType)
                        return 1;
                    if (DamageType < effect.DamageType)
                        return -1;
                    if (Mag > effect.Mag)
                        return 1;
                    if (Mag < effect.Mag)
                        return -1;
                    return nVariableFlag;
                }
                if (effect.EffectType == Enums.eEffectType.ResEffect)
                {
                    if (ETModifies > effect.ETModifies)
                        return 1;
                    if (ETModifies < effect.ETModifies)
                        return -1;
                    if (Mag > effect.Mag)
                        return 1;
                    if (Mag < effect.Mag)
                        return -1;
                    return nVariableFlag;
                }
                if (effect.EffectType == Enums.eEffectType.Mez || effect.EffectType == Enums.eEffectType.MezResist)
                {
                    if (MezType > effect.MezType)
                        return 1;
                    if (MezType < effect.MezType)
                        return -1;
                    if (Mag > effect.Mag)
                        return 1;
                    if (Mag < effect.Mag)
                        return -1;
                    if (Duration > effect.Duration)
                        return 1;
                    if (Duration < effect.Duration)
                        return -1;
                    return nVariableFlag;
                }
                if (effect.EffectType == Enums.eEffectType.Enhancement)
                {
                    if (ETModifies > effect.ETModifies)
                        return 1;
                    if (ETModifies < effect.ETModifies)
                        return 1;
                    if (Mag > effect.Mag)
                        return 1;
                    if (Mag < effect.Mag)
                        return -1;
                    if (Duration > effect.Duration)
                        return 1;
                    if (Duration < effect.Duration)
                        return -1;
                    return nVariableFlag;
                }
                if (effect.EffectType == Enums.eEffectType.None)
                {
                    return string.CompareOrdinal(Special, effect.Special);
                }
                return nVariableFlag;
            }

            throw new ArgumentException("Compare failed, object is not a Power Effect class");
        }


        public object Clone()
        {
            return new Effect(this);
        }

        private static string BuildCs(string iValue, string iStr, bool noComma = false)

        {
            if (string.IsNullOrEmpty(iValue))
                return iStr;
            var str = ", ";
            if (noComma)
                str = " ";
            if (!string.IsNullOrEmpty(iStr))
                iStr += str;
            iStr += iValue;
            return iStr;
        }

        private float ParseMagnitudeExpression()

        {
            float num1;
            if (MagnitudeExpression.IndexOf(".8 rechargetime power.base> 1 30 minmax * 1.8 + 2 * @StdResult * 10 / areafactor power.base> /", StringComparison.OrdinalIgnoreCase) > -1)
            {
                var num2 = (float)((Math.Max(Math.Min(power.RechargeTime, 30f), 0.0f) * 0.800000011920929 + 1.79999995231628) / 5.0) / power.AoEModifier * Scale;
                if (MagnitudeExpression.Length > ".8 rechargetime power.base> 1 30 minmax * 1.8 + 2 * @StdResult * 10 / areafactor power.base> /".Length + 2)
                {
                    num2 *= float.Parse(MagnitudeExpression.Substring(".8 rechargetime power.base> 1 30 minmax * 1.8 + 2 * @StdResult * 10 / areafactor power.base> /".Length + 1).Substring(0, 2));
                }

                num1 = num2;
            }
            else
            {
                num1 = 0.0f;
            }

            return num1;
        }
    }
    public class KeyValue<TKey, TValue>
    {
        public TKey Key { get; set; }
        public TValue Value { get; set; }

        public KeyValue() { }

        public KeyValue(TKey key, TValue value)
        {
            Key = key;
            Value = value;
        }
        public bool Validated { get; set; }
    }
}
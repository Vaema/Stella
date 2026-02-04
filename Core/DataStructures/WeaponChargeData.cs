
//using System.IO;

//namespace Stella.Core.DataStructures;

//public class AbilityChargeData
//{
//    public int MaxCharges;
//    public int CurrentCharges;
//    public int CooldownTicks;
//    public int CooldownTimer;

//    public AbilityChargeData() { }

//    public AbilityChargeData(int maxCharges, int cooldownTicks)
//    {
//        MaxCharges = maxCharges;
//        CurrentCharges = maxCharges;
//        CooldownTicks = cooldownTicks;
//    }

//    public bool CanUse => CurrentCharges > 0;

//    public void Consume()
//    {
//        if (!CanUse)
//            return;

//        CurrentCharges--;
//        if (CooldownTimer <= 0)
//            CooldownTimer = CooldownTicks;
//    }

//    public void Update()
//    {
//        if (CurrentCharges >= MaxCharges)
//            return;

//        if (CooldownTimer > 0)
//            CooldownTimer--;
//        else
//        {
//            CurrentCharges++;
//            if (CurrentCharges < MaxCharges)
//                CooldownTimer = CooldownTicks;
//        }
//    }

//    public void NetSend(BinaryWriter writer)
//    {
//        writer.Write(MaxCharges);
//        writer.Write(CurrentCharges);
//        writer.Write(CooldownTicks);
//        writer.Write(CooldownTimer);
//    }

//    public void NetReceive(BinaryReader reader)
//    {
//        MaxCharges = reader.ReadInt32();
//        CurrentCharges = reader.ReadInt32();
//        CooldownTicks = reader.ReadInt32();
//        CooldownTimer = reader.ReadInt32();
//    }
//}

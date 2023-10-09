using Terraria;
using Terraria.ModLoader;
using NaturalRiceFirstMod.Items.Mounts.InfiniteFlight;

namespace NaturalRiceFirstMod.Buffs.Mounts
{
    public class NeroMountBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoTimeDisplay[Type] = true;
            Main.buffNoSave[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.mount.SetMount(ModContent.MountType<NeroMount>(), player);
            player.buffTime[buffIndex] = 10;
        }
    }
}
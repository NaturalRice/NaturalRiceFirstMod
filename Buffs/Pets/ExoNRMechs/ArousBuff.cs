﻿using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NaturalRiceFirstMod.Buffs.Pets.ExoNRMechs
{
    public class ArousBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoTimeDisplay[Type] = true;
            Main.vanityPet[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.buffTime[buffIndex] = 18000;
            player.GetModPlayer<NaturalRiceFirstModPlayer>().arous = true;
            bool petProjectileNotSpawned = player.ownedProjectileCounts[ModContent.ProjectileType<Projectiles.Pets.ExoNRMechs.ArousBody>()] <= 0;
            if (petProjectileNotSpawned && player.whoAmI == Main.myPlayer)
            {
                Projectile.NewProjectile(player.GetSource_Buff(buffIndex), player.position.X + (float)(player.width / 2), player.position.Y + (float)(player.height / 2), 0f, 0f, ModContent.ProjectileType<Projectiles.Pets.ExoNRMechs.ArousBody>(), 0, 0f, player.whoAmI, 0f, 0f);
            }
        }
    }
}
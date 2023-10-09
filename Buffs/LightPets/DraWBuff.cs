using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NaturalRiceFirstMod.Buffs.LightPets
{
    public class DraWBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoTimeDisplay[Type] = true;
            Main.lightPet[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            /*if (NaturalRiceFirstMod.CalamityActive)
            {
                Mod clamMod = ModLoader.GetMod("CalamityMod");
                clamMod.Call("AddAbyssLightStrength", Main.player[Main.myPlayer], 3);
            }*/
            player.buffTime[buffIndex] = 18000;
            player.GetModPlayer<NaturalRiceFirstModPlayer>().DraWGuard1 = true;
            player.GetModPlayer<NaturalRiceFirstModPlayer>().DraWGuard2 = true;
            player.GetModPlayer<NaturalRiceFirstModPlayer>().DraWGuard3 = true;
            player.GetModPlayer<NaturalRiceFirstModPlayer>().DraWPet = true;
            bool petProjectileNotSpawned = player.ownedProjectileCounts[ModContent.ProjectileType<Projectiles.Pets.LightPets.DraWGuard1>()] <= 0 &&
                                           player.ownedProjectileCounts[ModContent.ProjectileType<Projectiles.Pets.LightPets.DraWGuard2>()] <= 0 &&
                                           player.ownedProjectileCounts[ModContent.ProjectileType<Projectiles.Pets.LightPets.DraWGuard3>()] <= 0 &&
                                           player.ownedProjectileCounts[ModContent.ProjectileType<Projectiles.Pets.LightPets.DraWPet>()] <= 0;
            if (petProjectileNotSpawned && player.whoAmI == Main.myPlayer)
            {
                Projectile.NewProjectile(player.GetSource_Buff(buffIndex), player.position.X + player.width / 2, player.position.Y + player.height / 2,
                    0f, 0f, ModContent.ProjectileType<Projectiles.Pets.LightPets.DraWGuard1>(), 0, 0f, player.whoAmI);
                Projectile.NewProjectile(player.GetSource_Buff(buffIndex), player.position.X + player.width / 2, player.position.Y + player.height / 2,
                    0f, 0f, ModContent.ProjectileType<Projectiles.Pets.LightPets.DraWGuard2>(), 0, 0f, player.whoAmI);
                Projectile.NewProjectile(player.GetSource_Buff(buffIndex), player.position.X + player.width / 2, player.position.Y + player.height / 2,
                    0f, 0f, ModContent.ProjectileType<Projectiles.Pets.LightPets.DraWGuard3>(), 0, 0f, player.whoAmI);
                Projectile.NewProjectile(player.GetSource_Buff(buffIndex), player.position.X + player.width / 2, player.position.Y + player.height / 2,
                    0f, 0f, ModContent.ProjectileType<Projectiles.Pets.LightPets.DraWPet>(), 0, 0f, player.whoAmI);
            }
        }
    }
}
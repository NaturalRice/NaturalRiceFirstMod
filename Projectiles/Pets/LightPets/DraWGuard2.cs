﻿using Microsoft.Xna.Framework;
using Terraria;

namespace NaturalRiceFirstMod.Projectiles.Pets.LightPets
{
    public class DraWGuard2 : ModFlyingPet
    {
        public override float TeleportThreshold => 1440f;

        public override Vector2 FlyingOffset => new Vector2(148f * -Main.player[Projectile.owner].direction, -50f);

        public override void SetStaticDefaults()
        {
            PetSetStaticDefaults(lightPet: true);
            // DisplayName.SetDefault("Nice Small Potato");
            Main.projFrames[Projectile.type] = 6;
        }

        public override void SetDefaults()
        {
            PetSetDefaults();
            Projectile.width = 46;
            Projectile.height = 32;
            Projectile.ignoreWater = true;
        }

        public override void PostDraw(Color lightColor)
        {
            SimpleGlowmask(Main.spriteBatch);
        }

        public override void Animation(int state)
        {
            SimpleAnimation(speed: 4);
        }

        public override void PetFunctionality(Player player)
        {
            NaturalRiceFirstModPlayer modPlayer = player.GetModPlayer<NaturalRiceFirstModPlayer>();

            if (player.dead)
                modPlayer.DraWGuard2 = false;

            if (modPlayer.DraWGuard2)
                Projectile.timeLeft = 2;
        }
    }
}
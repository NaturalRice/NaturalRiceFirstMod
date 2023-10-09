﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using NaturalRiceFirstMod.Projectiles.Ammo;
using NaturalRiceFirstMod.Projectiles;
using Mono.Cecil;

namespace NaturalRiceFirstMod.Items.Equips.Shirts.ArousChestplate
{
    public class PlasmaArm : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Plasma arm");
        }

        public override void SetDefaults()
        {
            Projectile.width = 78;
            Projectile.height = 103;
            Projectile.friendly = true;
            Projectile.timeLeft = 18000;
            Projectile.aiStyle = -1;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            NaturalRiceFirstModPlayer modPlayer = player.GetModPlayer<NaturalRiceFirstModPlayer>();

            Projectile.position.X = player.position.X + 270;
            Projectile.position.Y = player.position.Y + 240;

            if (modPlayer.arousarms)
            {
                Projectile.timeLeft = 18000;
            }
            else
            {
                Projectile.active = false;
            }
            Projectile.rotation = Projectile.AngleTo(Main.MouseWorld);
            //Projectile.spriteDirection = player.direction;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Player player = Main.player[Projectile.owner];
            Vector2 distToProj = Projectile.Center;
            float projRotation = Projectile.AngleTo(player.MountedCenter) - 1.57f;
            bool doIDraw = true;
            Texture2D texture = ModContent.Request<Texture2D>("NaturalRiceFirstMod/Items/Equips/Shirts/ArousChestplate/ArousTube").Value; //change this accordingly to your chain texture

            while (doIDraw)
            {
                float distance = (player.MountedCenter - distToProj).Length();
                if (distance < (texture.Height + 1))
                {
                    doIDraw = false;
                }
                else if (!float.IsNaN(distance))
                {
                    Color drawColor = Lighting.GetColor((int)distToProj.X / 16, (int)(distToProj.Y / 16f));
                    distToProj += Projectile.DirectionTo(player.MountedCenter) * texture.Height;
                    Main.EntitySpriteDraw(texture, distToProj - Main.screenPosition,
                        new Rectangle(0, 0, texture.Width, texture.Height), drawColor, projRotation,
                        Utils.Size(texture) / 2f, 1f, SpriteEffects.None, 0);
                }
            }
            return true;
        }
    }
}
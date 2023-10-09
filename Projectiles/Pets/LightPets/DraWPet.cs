using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NaturalRiceFirstMod.Projectiles.Pets.LightPets
{
    public class DraWPet : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("large Potato");
            Main.projFrames[Projectile.type] = 12;//原先6帧
            Main.projPet[Projectile.type] = true;
            ProjectileID.Sets.LightPet[Projectile.type] = true;
        }

        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.DD2PetGhost);
            DrawOriginOffsetY = -300;//-300
            DrawOffsetX = -125;//-125
        }

        public override void PostDraw(Color lightColor)
        {
            Texture2D glowMask = ModContent.Request<Texture2D>("NaturalRiceFirstMod/Projectiles/Pets/LightPets/DraWPet_Glow").Value;
            Rectangle frame = glowMask.Frame(1, Main.projFrames[Projectile.type], 0, Projectile.frame);
            frame.Height -= 1;
            float originOffsetX = (glowMask.Width - Projectile.width) * 0.5f + Projectile.width * 0.5f + DrawOriginOffsetX;
            Main.EntitySpriteDraw
            (
                glowMask,
                Projectile.position - Main.screenPosition + new Vector2(originOffsetX + DrawOffsetX, Projectile.height / 2 + Projectile.gfxOffY),
                frame,
                Color.White,
                Projectile.rotation = 0f,
                //这样宠物确实不旋转了，但是光亮跟本体会不重合；我目前的方法是将本体图片变成空白的，光亮图片即原先的本体图片，这样只显示光亮
                new Vector2(originOffsetX, Projectile.height / 2 - DrawOriginOffsetY),
                Projectile.scale,
                Projectile.spriteDirection == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None,
                0
            );
        }

        public void AnimateProjectile() // Call this every frame, for example in the AI method.
        {
            Projectile.frameCounter++;
            if (Projectile.frameCounter >= 9) // This will change the sprite every 8 frames (0.13 seconds).
            {
                Projectile.frame++;
                Projectile.frame %= 1; // Will reset to the first frame if you've gone through them all.
                Projectile.frameCounter = 4;
            }
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            NaturalRiceFirstModPlayer modPlayer = player.GetModPlayer<NaturalRiceFirstModPlayer>();
            if (player.dead)
            {
                modPlayer.DraWPet = false;
            }
            if (modPlayer.DraWPet)
            {
                Projectile.timeLeft = 2;
            }

            Lighting.AddLight(Projectile.position, new Vector3(1.61568627f, 0.901960784f, 0.462745098f));

            Vector2 vectorToOwner = player.Center - Projectile.Center;
            float distanceToOwner = vectorToOwner.Length();

            if (distanceToOwner > 3000f)
            {
                Projectile.position = player.Center;
                Projectile.velocity *= 0.1f;
                Projectile.netUpdate = true;
            }
        }
    }

}
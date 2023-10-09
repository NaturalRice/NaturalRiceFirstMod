using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using NaturalRiceFirstMod.Projectiles.Ammo;
using NaturalRiceFirstMod.Projectiles;
using Mono.Cecil;

namespace NaturalRiceFirstMod.Items.Equips.Shirts.ArousChestplate
{

    public class GaussArm : ModProjectile
    {
        public int owner; // 自定义字段，用于存储投射物的源

        private const float TargetDistance = 800f; // 设置寻找敌人的最大距离
        private int target = -1; // 记录当前目标敌人的索引

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Nuke arm");
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

        private void FindTarget()
        {
            target = -1; // 默认值，表示没有找到目标
            float maxDistance = TargetDistance; // 设置最大寻找距离

            for (int i = 0; i < Main.npc.Length; i++)
            {
                NPC npc = Main.npc[i];

                // 检查敌人是否活着、活跃，并且在最大寻找范围内
                if (npc.active && !npc.friendly && Vector2.Distance(npc.Center, Projectile.Center) < maxDistance)
                {
                    maxDistance = Vector2.Distance(npc.Center, Projectile.Center);
                    target = i; // 更新目标索引
                }
            }
        }


        public override void AI()
        {
            // 在AI方法中使用 owner 字段来表示投射物的源
            Player player = Main.player[owner];

            NaturalRiceFirstModPlayer modPlayer = player.GetModPlayer<NaturalRiceFirstModPlayer>();

            Projectile.position.X = player.position.X + 260;
            Projectile.position.Y = player.position.Y - 310;

            if (modPlayer.arousarms)
            {
                Projectile.timeLeft = 18000;

                // 寻找附近的敌人
                FindTarget(); // 调用FindTarget方法来更新target值

                if (target != -1)
                {
                    NPC targetNPC = Main.npc[target];

                    // 瞄准敌人
                    Vector2 direction = targetNPC.Center - Projectile.Center;
                    direction.Normalize();
                    Projectile.velocity = direction * 8f;

                    // 设置投射物的旋转角度
                    Projectile.rotation = direction.ToRotation();

                    // 发射子弹
                    if (Projectile.localAI[0] == 0f)
                    {
                        Projectile.localAI[0] = 1f;

                        // 计算发射角度，这里示例为向下发射
                        float shootAngle = MathHelper.PiOver2;

                        // 发射弹药
                        for (int i = 0; i < 100000; i++) // 这里可以根据需要发射多个弹药(好像不能设无限，否则会无限循环，卡死）
                        {
                            Vector2 shotVelocity = Vector2.UnitX.RotatedBy(shootAngle) * 8f; // 这里示例为向右发射
                            int newProjectile = Projectile.NewProjectile(null, Projectile.position, shotVelocity, ModContent.ProjectileType<NeroBullet>(), (int)(Projectile.damage * 0.5f), 0, Main.myPlayer);
                            Main.projectile[newProjectile].timeLeft = 300;
                            Main.projectile[newProjectile].netUpdate = true;

                            //shootAngle += MathHelper.PiOver4; // 增加弹药之间的间隔角度
                        }
                    }
                }
                else
                {
                    // 当没有目标时，保持静止
                    Projectile.velocity = Vector2.Zero;
                }
            }
            else
            {
                Projectile.active = false;
            }
            Projectile.rotation = Projectile.AngleTo(Main.MouseWorld);//朝向鼠标的地方
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Player player = Main.player[owner];
            Vector2 distToProj = new Vector2(Projectile.Center.X, Projectile.Center.Y);
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
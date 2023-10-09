﻿using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using System;

namespace NaturalRiceFirstMod.Items.Pets.ExoNRMechs
{
    public class NeroSummon : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Exo Gemstone");
            // Tooltip.SetDefault("Summons the full miniaturized exo ensemble");
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.ZephyrFish);
            Item.UseSound = SoundID.NPCHit4;
            Item.shoot = ModContent.ProjectileType<Projectiles.Pets.ExoNRMechs.NREyeBallPet>();
            Item.value = Item.sellPrice(0, 3, 0, 0);
            Item.rare = 11;
            Item.buffType = ModContent.BuffType<Buffs.Pets.ExoNRMechs.NeroBuff>();
        }

        public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            if (player.whoAmI == Main.myPlayer && player.itemTime == 0)
            {
                player.AddBuff(Item.buffType, 3600, true);
            }
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            //rarity 12 (Turquoise) = new Color(0, 255, 200)
            //rarity 13 (Pure Green) = new Color(0, 255, 0)
            //rarity 14 (Dark Blue) = new Color(43, 96, 222)
            //rarity 15 (Violet) = new Color(108, 45, 199)
            //rarity 16 (Hot Pink/Developer) = new Color(255, 0, 255)
            //rarity rainbow (no expert tag on item) = new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB)
            //rarity rare variant = new Color(255, 140, 0)
            //rarity dedicated(patron items) = new Color(139, 0, 0)
            //look at https://calamitymod.gamepedia.com/Rarity to know where to use the colors
            foreach (TooltipLine tooltipLine in tooltips)
            {
                if (tooltipLine.Mod == "Terraria" && tooltipLine.Name == "ItemName")
                {
                    tooltipLine.OverrideColor = new Color(108, 45, 199); //change the color accordingly to above
                }
            }
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(source, player.Center, Vector2.Zero, ModContent.ProjectileType<Projectiles.Pets.ExoNRMechs.NurexPet>(), 0, 0, player.whoAmI);
            Projectile.NewProjectile(source, player.Center, Vector2.Zero, ModContent.ProjectileType<Projectiles.Pets.ExoNRMechs.ArousBody>(), 0, 0, player.whoAmI);
            Projectile.NewProjectile(source, player.Center, Vector2.Zero, ModContent.ProjectileType<Projectiles.Pets.ExoNRMechs.NREyeBallPet>(), 0, 0, player.whoAmI);

            return false;
        }


        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();//创建一个配方
            recipe.AddIngredient(ItemID.Torch, 10);//加入材料（10火把）
            recipe.AddIngredient(ItemID.Wood, 10);//添加第二种材料（10木材）
            recipe.AddTile(TileID.Campfire);//加入合成站（这里为了有趣我改成了篝火）
            recipe.Register();
        }
    }
}
﻿using Terraria;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria.ID;
using System;
/*using NaturalRiceFirstMod.Tiles.AstralBlocks;
using NaturalRiceFirstMod.Walls.AstralUnsafe;
using NaturalRiceFirstMod.Tiles.AstralMisc;
using NaturalRiceFirstMod.Walls.AstralSafe;
using NaturalRiceFirstMod.Dusts;*/

namespace NaturalRiceFirstMod
{
    public class NaturalRiceFirstModGlobalProjectile : GlobalProjectile
    {
        public override bool InstancePerEntity
        {
            get
            {
                return true;
            }
        }
        public bool isCalValPet;
        public override bool PreDraw(Projectile projectile, ref Color drawColor)
        {
            /*if (NaturalRiceFirstModWorld.RockshrinEX && NaturalRiceFirstMod.CalamityActive)
            {
                if (projectile.type == NaturalRiceFirstMod.CalamityProjectile("BrimstoneMonster"))
                {
                    Texture2D deusheadsprite = ModContent.Request<Texture2D>("CalValEX/Projectiles/BrimstoneMonster").Value;

                    Rectangle deusheadsquare = new Rectangle(0, 0, deusheadsprite.Width, deusheadsprite.Height);
                    Color deusheadalpha = projectile.GetAlpha(drawColor);
                    Main.EntitySpriteDraw(deusheadsprite, projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY), deusheadsquare, deusheadalpha, projectile.rotation, Utils.Size(deusheadsquare) / 2f, projectile.scale, SpriteEffects.None, 0);
                    return false;
                }
            }*/
            return true;
        }

        public override void AI(Projectile proj)
        {
            if (isCalValPet)
            {
                for (int k = 0; k < Main.maxProjectiles; k++)
                {
                    Projectile otherProj = Main.projectile[k];
                    if (!otherProj.active || otherProj.owner != proj.owner || k == proj.whoAmI)
                        continue;

                    bool bothPets = otherProj.GetGlobalProjectile<NaturalRiceFirstModGlobalProjectile>().isCalValPet;
                    float dist = Math.Abs(proj.position.X - otherProj.position.X) + Math.Abs(proj.position.Y - otherProj.position.Y);
                    if (bothPets && dist < proj.width)
                    {
                        if (proj.position.X < otherProj.position.X)
                            proj.velocity.X -= 0.4f;
                        else
                            proj.velocity.X += 0.4f;

                        if (proj.position.Y < otherProj.position.Y)
                            proj.velocity.Y -= 0.4f;
                        else
                            proj.velocity.Y += 0.4f;
                    }
                }
            }

            if (proj.owner == Main.myPlayer && proj.type == Terraria.ID.ProjectileID.PureSpray)
                PureConvert((int)(proj.position.X + proj.width / 2) / 16, (int)(proj.position.Y + proj.height / 2) / 16, 2);
            /*if (NaturalRiceFirstMod.CalamityActive && proj.owner == Main.myPlayer && proj.type == NaturalRiceFirstMod.CalamityProjectile("AstralSpray"))
                InfectionConvert((int)(proj.position.X + proj.width / 2) / 16, (int)(proj.position.Y + proj.height / 2) / 16, 2);*/
            if (proj.owner == Main.myPlayer && (proj.type == ProjectileID.CorruptSpray || proj.type == ProjectileID.CrimsonSpray || proj.type == ProjectileID.HallowSpray))
                VoidConvert((int)(proj.position.X + proj.width / 2) / 16, (int)(proj.position.Y + proj.height / 2) / 16, 2);

        }

        public void PureConvert(int i, int j, int size = 4)
        {
            for (int k = i - size; k <= i + size; k++)
            {
                for (int l = j - size; l <= j + size; l++)
                {
                    if (WorldGen.InWorld(k, l, 1) && Math.Abs(k - i) + Math.Abs(l - j) < Math.Sqrt(size * size + size * size))
                    {
                        int type = Main.tile[k, l].TileType;
                        int typemed = Main.tile[k - 1, l].TileType;
                        int wall = Main.tile[k, l].WallType;

                        //Stone
                        /*if (type == ModContent.TileType<AstralHardenedSandPlaced>())
                        {
                            Main.tile[k, l].TileType = TileID.HardenedSand;
                            WorldGen.SquareTileFrame(k, l, true);
                            NetMessage.SendTileSquare(-1, k, l, 1);
                            break;
                        }
                        else if (type == ModContent.TileType<AstralSandstonePlaced>())
                        {
                            Main.tile[k, l].TileType = TileID.Sandstone;
                            WorldGen.SquareTileFrame(k, l, true);
                            NetMessage.SendTileSquare(-1, k, l, 1);
                            break;
                        }
                        else if (type == ModContent.TileType<AstralTreeWoodPlaced>())
                        {
                            Main.tile[k, l].TileType = TileID.LivingWood;
                            WorldGen.SquareTileFrame(k, l, true);
                            NetMessage.SendTileSquare(-1, k, l, 1);
                            break;
                        }
                        else if (type == ModContent.TileType<AstralGrassPlaced>())
                        {
                            Main.tile[k, l].TileType = TileID.Grass;
                            WorldGen.SquareTileFrame(k, l, true);
                            NetMessage.SendTileSquare(-1, k, l, 1);
                            break;
                        }
                        else if (type == ModContent.TileType<AstralDirtPlaced>())
                        {
                            Main.tile[k, l].TileType = TileID.Dirt;
                            WorldGen.SquareTileFrame(k, l, true);
                            NetMessage.SendTileSquare(-1, k, l, 1);
                            break;
                        }
                        else if (type == ModContent.TileType<AstralSandPlaced>())
                        {
                            Main.tile[k, l].TileType = TileID.Sand;
                            WorldGen.SquareTileFrame(k, l, true);
                            NetMessage.SendTileSquare(-1, k, l, 1);
                            break;
                        }
                        else if (type == ModContent.TileType<AstralClayPlaced>())
                        {
                            Main.tile[k, l].TileType = TileID.ClayBlock;
                            WorldGen.SquareTileFrame(k, l, true);
                            NetMessage.SendTileSquare(-1, k, l, 1);
                            break;
                        }
                        else if (type == ModContent.TileType<XenostonePlaced>())
                        {
                            Main.tile[k, l].TileType = TileID.Stone;
                            WorldGen.SquareTileFrame(k, l, true);
                            NetMessage.SendTileSquare(-1, k, l, 1);
                            break;
                        }
                        else if (type == ModContent.TileType<AstralIcePlaced>())
                        {
                            Main.tile[k, l].TileType = TileID.IceBlock;
                            WorldGen.SquareTileFrame(k, l, true);
                            NetMessage.SendTileSquare(-1, k, l, 1);
                            break;
                        }
                        else if (type == ModContent.TileType<AstralSnowPlaced>())
                        {
                            Main.tile[k, l].TileType = TileID.SnowBlock;
                            WorldGen.SquareTileFrame(k, l, true);
                            NetMessage.SendTileSquare(-1, k, l, 1);
                            break;
                        }
                        else if (type == ModContent.TileType<AstralShortGrass>())
                        {
                            Main.tile[k, l].TileType = TileID.Plants;
                            WorldGen.SquareTileFrame(k, l, true);
                            NetMessage.SendTileSquare(-1, k, l, 1);
                            break;
                        }
                        else if (type == ModContent.TileType<AstralTallGrass>())
                        {
                            Main.tile[k, l].TileType = TileID.Plants2;
                            WorldGen.SquareTileFrame(k, l, true);
                            NetMessage.SendTileSquare(-1, k, l, 1);
                            break;
                        }*/



                        /*else if (type == ModContent.TileType<AstralPilesBig>())
						{
							//Left top
							Main.tile[k, l].TileType = TileID.LargePiles;
							//Middle top
							Main.tile[k - 1, l].TileType = TileID.LargePiles;
							//Right top
							Main.tile[k - 2, l].TileType = TileID.LargePiles;
							//Left bottom
							Main.tile[k, l - 1].TileType = TileID.LargePiles;
							//Middle bottom
							Main.tile[k - 1, l - 1].TileType = TileID.LargePiles;
							//Right bottom
							Main.tile[k - 2, l - 1].TileType = TileID.LargePiles;
							WorldGen.SquareTileFrame(k, l, true);
							NetMessage.SendTileSquare(-1, k, l, 1);
							break;
						}
						else if (type == ModContent.TileType<AstralPilesMedium>())
						{
							Main.tile[k, l].TileType = TileID.SmallPiles;
							Main.tile[k - 1, l].TileType = TileID.SmallPiles;
							WorldGen.SquareTileFrame(k, l, true);
							NetMessage.SendTileSquare(-1, k, l, 1);
							break;
						}
						else if (type == ModContent.TileType<AstralPilesSmall>())
						{
							Main.tile[k, l].TileType = TileID.SmallPiles;
							WorldGen.SquareTileFrame(k, l, true);
							NetMessage.SendTileSquare(-1, k, l, 1);
							break;
						}
						else if (type == ModContent.TileType<AstralStalactites>())
						{
							Main.tile[k, l].TileType = TileID.Stalactite;
							Main.tile[k, l - 1].TileType = TileID.Stalactite;
							WorldGen.SquareTileFrame(k, l, true);
							NetMessage.SendTileSquare(-1, k, l, 1);
							break;
						}*/


                        /*if (wall == ModContent.WallType<AstralDirtWallPlaced>())
                        {
                            Main.tile[k, l].WallType = WallID.DirtUnsafe;
                            WorldGen.SquareWallFrame(k, l, true);
                            NetMessage.SendTileSquare(-1, k, l, 1);
                            break;
                        }
                        else if (wall == ModContent.WallType<XenostoneWallPlaced>())
                        {
                            Main.tile[k, l].WallType = WallID.Stone;
                            WorldGen.SquareWallFrame(k, l, true);
                            NetMessage.SendTileSquare(-1, k, l, 1);
                            break;
                        }
                        else if (wall == ModContent.WallType<AstralHardenedSandWallPlaced>())
                        {
                            Main.tile[k, l].WallType = WallID.HardenedSand;
                            WorldGen.SquareWallFrame(k, l, true);
                            NetMessage.SendTileSquare(-1, k, l, 1);
                            break;
                        }
                        else if (wall == ModContent.WallType<AstralSandstoneWallPlaced>())
                        {
                            Main.tile[k, l].WallType = WallID.Sandstone;
                            WorldGen.SquareWallFrame(k, l, true);
                            NetMessage.SendTileSquare(-1, k, l, 1);
                            break;
                        }
                        else if (wall == ModContent.WallType<AstralIceWallPlaced>())
                        {
                            Main.tile[k, l].WallType = WallID.IceUnsafe;
                            WorldGen.SquareWallFrame(k, l, true);
                            NetMessage.SendTileSquare(-1, k, l, 1);
                            break;
                        }
                        else if (wall == ModContent.WallType<AstralGrassWallPlaced>())
                        {
                            Main.tile[k, l].WallType = WallID.Grass;
                            WorldGen.SquareWallFrame(k, l, true);
                            NetMessage.SendTileSquare(-1, k, l, 1);
                            break;
                        }*/
                    }
                }
            }
        }



        /// <summary>
        /// Allows you to determine whether the vanilla code for Kill and the Kill hook will be called. Return false to stop them from being called. Returns true by default. Note that this does not stop the projectile from dying.
        /// </summary>
        /// <param name="projectile"></param>
        /// <param name="timeLeft"></param>
        /// <returns></returns>
        public virtual bool PreKill(Projectile projectile, int timeLeft)
        {
            return true;
        }

        /// <summary>
        /// Allows you to control what happens when a projectile is killed (for example, creating dust or making sounds).
        /// </summary>
        /// <param name="projectile"></param>
        /// <param name="timeLeft"></param>
        public virtual void OnKill(Projectile projectile, int timeLeft)
        {
        }

        [Obsolete("Renamed to OnKill", error: true)] // Remove in 2023_10
        public virtual void Kill(Projectile projectile, int timeLeft)
        {
        }


        /*public void InfectionConvert(int i, int j, int size = 4)
        {
            if (NaturalRiceFirstMod.CalamityActive)
            {
                for (int k = i - size; k <= i + size; k++)
                {
                    for (int l = j - size; l <= j + size; l++)
                    {
                        if (WorldGen.InWorld(k, l, 1) && Math.Abs(k - i) + Math.Abs(l - j) < Math.Sqrt(size * size + size * size))
                        {
                            int type = Main.tile[k, l].TileType;
                            int typemed = Main.tile[k - 1, l].TileType;
                            int wall = Main.tile[k, l].WallType;

                            if (type == ModContent.TileType<AstralTreeWoodPlaced>())
                            {
                                Main.tile[k, l].TileType = (ushort)CalValEX.CalamityTile("AstralMonolith");
                                WorldGen.SquareTileFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                                break;
                            }
                            else if (type == ModContent.TileType<AstralGrassPlaced>())
                            {
                                Main.tile[k, l].TileType = (ushort)CalValEX.CalamityTile("AstralGrass");
                                WorldGen.SquareTileFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                                break;
                            }
                            else if (type == ModContent.TileType<AstralDirtPlaced>())
                            {
                                Main.tile[k, l].TileType = (ushort)CalValEX.CalamityTile("AstralDirt");
                                WorldGen.SquareTileFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                                break;
                            }
                            else if (type == ModContent.TileType<AstralClayPlaced>())
                            {
                                Main.tile[k, l].TileType = (ushort)CalValEX.CalamityTile("AstralClay");
                                WorldGen.SquareTileFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                                break;
                            }
                            else if (type == ModContent.TileType<AstralSnowPlaced>())
                            {
                                Main.tile[k, l].TileType = (ushort)CalValEX.CalamityTile("AstralSnow");
                                WorldGen.SquareTileFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                                break;
                            }
                            if (wall == ModContent.WallType<AstralDirtWallPlaced>())
                            {
                                Main.tile[k, l].WallType = (ushort)CalValEX.CalamityWall("AstralDirtWall");
                                WorldGen.SquareWallFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                                break;
                            }
                        }
                    }
                }
            }
        }*/
        public void VoidConvert(int i, int j, int size = 4)
        {
            for (int k = i - size; k <= i + size; k++)
            {
                for (int l = j - size; l <= j + size; l++)
                {
                    if (WorldGen.InWorld(k, l, 1) && Math.Abs(k - i) + Math.Abs(l - j) < Math.Sqrt(size * size + size * size))
                    {
                        int type = Main.tile[k, l].TileType;
                        int typemed = Main.tile[k - 1, l].TileType;
                        int wall = Main.tile[k, l].WallType;

                        /*if (type == ModContent.TileType<AstralTreeWoodPlaced>())
                        {
                            Main.tile[k, l].TileType = TileID.LivingWood;
                            WorldGen.SquareTileFrame(k, l, true);
                            NetMessage.SendTileSquare(-1, k, l, 1);
                            break;
                        }
                        else if (type == ModContent.TileType<AstralDirtPlaced>())
                        {
                            Main.tile[k, l].TileType = TileID.Dirt;
                            WorldGen.SquareTileFrame(k, l, true);
                            NetMessage.SendTileSquare(-1, k, l, 1);
                            break;
                        }
                        else if (type == ModContent.TileType<AstralClayPlaced>())
                        {
                            Main.tile[k, l].TileType = TileID.ClayBlock;
                            WorldGen.SquareTileFrame(k, l, true);
                            NetMessage.SendTileSquare(-1, k, l, 1);
                            break;
                        }
                        else if (type == ModContent.TileType<AstralSnowPlaced>())
                        {
                            Main.tile[k, l].TileType = TileID.SnowBlock;
                            WorldGen.SquareTileFrame(k, l, true);
                            NetMessage.SendTileSquare(-1, k, l, 1);
                            break;
                        }
                        if (wall == ModContent.WallType<AstralDirtWallPlaced>())
                        {
                            Main.tile[k, l].WallType = WallID.DirtUnsafe;
                            WorldGen.SquareWallFrame(k, l, true);
                            NetMessage.SendTileSquare(-1, k, l, 1);
                            break;
                        }*/
                    }
                }
            }
        }

        public virtual bool NeroBullet(Projectile projectile, int timeLeft)
        {
            return true;
        }


        /*public override bool? CanHitNPC(Projectile projectile, NPC target)
        {
            if (NaturalRiceFirstMod.CalamityActive)
            {
                if (projectile.type == NaturalRiceFirstMod.CalamityProjectile("CosmicFire") && target.type == ModContent.NPCType<AprilFools.Jharim.Jharim>())
                {
                    return true;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                if (projectile.type == ProjectileID.VortexBeaterRocket && target.type == ModContent.NPCType<AprilFools.Jharim.Jharim>())
                {
                    return true;
                }
                else
                {
                    return null;
                }
            }
        }*/
    }
}
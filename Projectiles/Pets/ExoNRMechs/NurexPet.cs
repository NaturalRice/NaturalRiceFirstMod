﻿using NaturalRiceFirstMod.Items.Pets.ExoNRMechs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NaturalRiceFirstMod.Projectiles.Pets;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.Animations;
using Terraria.ModLoader;
using static Humanizer.In;

namespace NaturalRiceFirstMod.Projectiles.Pets.ExoNRMechs
{
    public class NurexPet : BaseWormPet
    {
        public override string Texture => "NaturalRiceFirstMod/Projectiles/Pets/ExoNRMechs/NurexHead";
        public override WormPetVisualSegment HeadSegment() => new WormPetVisualSegment("NaturalRiceFirstMod/Projectiles/Pets/ExoNRMechs/NurexHead", true, 1, 5);
        public override WormPetVisualSegment BodySegment() => new WormPetVisualSegment("NaturalRiceFirstMod/Projectiles/Pets/ExoNRMechs/NurexBody", true, 2, 5);
        public override WormPetVisualSegment TailSegment() => new WormPetVisualSegment("NaturalRiceFirstMod/Projectiles/Pets/ExoNRMechs/NurexTail", true, 1, 5);

        public override int SegmentSize() => 28;

        public override int SegmentCount() => 20;

        public override bool ExistenceCondition() => ModOwner.nurex;

        public override float GetSpeed => MathHelper.Lerp(17, 40, MathHelper.Clamp(Projectile.Distance(IdealPosition) / (WanderDistance * 2.2f) - 1f, 0, 1));

        public override int BodyVariants => 2;
        public override float BashHeadIn => 5;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Toy Nurex");
            Main.projFrames[Projectile.type] = 5;
            Main.projPet[Projectile.type] = true;
        }

        [JITWhenModsEnabled("CalamityMod")]
        public override void MoveTowardsIdealPosition()
        {
            //THIS CODE NEEDS CALAMITY 1.5.1.001 STUFF TO WORK PROPERLY!

            //If the owner is holding right click, shift its goal from the worms ideal position tothe mouse cursor
            /*if (NaturalRiceFirstMod.CalamityActive)
            {
                if ((bool)NaturalRiceFirstMod.Calamity.Call("GetRightClick", Owner) && Owner.HeldItem.type == ModContent.ItemType<GunmetalRemote>())
                    RelativeIdealPosition = (Vector2)NaturalRiceFirstMod.Calamity.Call("GetMouseWorld", Owner) - Owner.Center;
            }*/

            //Rotate towards its ideal position
            Projectile.rotation = Projectile.rotation.AngleTowards((IdealPosition - Projectile.Center).ToRotation(), MathHelper.Lerp(MaximumSteerAngle, MinimumSteerAngle, MathHelper.Clamp(Projectile.Distance(IdealPosition) / 80f, 0, 1)));
            Projectile.velocity = Projectile.rotation.ToRotationVector2() * GetSpeed;

            //Update its segment
            Segments[0].oldPosition = Segments[0].position;
            Segments[0].position = Projectile.Center;
        }

        public override void Animate()
        {
            foreach (WormPetSegment segment in Segments)
            {
                if (Owner.statLife / (float)Owner.statLifeMax > 0.5f && segment.visual.Frame != 0)
                {
                    segment.visual.FrameCounter++;
                    if (segment.visual.FrameCounter > segment.visual.FrameDuration)
                    {
                        segment.visual.Frame--;
                        segment.visual.FrameCounter = 0;
                    }
                }
                else if (Owner.statLife / (float)Owner.statLifeMax <= 0.5f && segment.visual.Frame != segment.visual.FrameCount - 1)
                {
                    segment.visual.FrameCounter++;
                    if (segment.visual.FrameCounter > segment.visual.FrameDuration)
                    {
                        segment.visual.Frame++;
                        segment.visual.FrameCounter = 0;
                    }
                }
            }
        }
    }
}
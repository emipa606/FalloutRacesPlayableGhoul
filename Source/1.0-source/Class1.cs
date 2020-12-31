using System;
using RimWorld;
using Verse;

namespace Verse
{
    // Token: 0x0200024E RID: 590
    public class CompProperties_GlowerGhoul : CompProperties_Glower
    {
        // Token: 0x06000AB1 RID: 2737 RVA: 0x0005598C File Offset: 0x00053D8C
        public CompProperties_GlowerGhoul()
        {
            this.compClass = typeof(CompGlowerGhoul);
        }

    }
    // Token: 0x02000C69 RID: 3177
    public class CompGlowerGhoul : CompGlower
    {
        public new CompProperties_GlowerGhoul Props
        {
            get
            {
                return (CompProperties_GlowerGhoul)this.props;
            }
        }

        public IntVec3 vec3 = IntVec3.Invalid;

        public override void CompTick()
        {
            base.CompTick();
            Map map = this.parent.Map;
            if (map != null)
            {
                IntVec3 @int = this.parent.Position;
                if ((vec3 == IntVec3.Invalid || (vec3 != IntVec3.Invalid && vec3 != @int)) && Find.TickManager.TicksGame >= this.nextUpdateTick)
                {
                    this.nextUpdateTick = Find.TickManager.TicksGame + 50;
                    map.mapDrawer.MapMeshDirty(this.parent.Position, MapMeshFlag.Things);
                    map.glowGrid.DeRegisterGlower(this);
                    map.mapDrawer.MapMeshDirty(this.parent.Position, MapMeshFlag.Things);
                    map.glowGrid.RegisterGlower(this);
                }
            }
        }

        public override void PostSpawnSetup(bool respawningAfterLoad)
        {
            Log.Message("Test");
            //if (this.parent.Map != null)
            //{
            //    Log.Message("CompGlowerPawn : CompGlower - PostSpawnSetup - base.PostSpawnSetup(respawningAfterLoad); - 14", true);
            //    base.PostSpawnSetup(respawningAfterLoad);
            //    Log.Message("CompGlowerPawn : CompGlower - PostSpawnSetup - if (!respawningAfterLoad) - 15", true);
            //    if (!respawningAfterLoad)
            //    {
            //        this.nextUpdateTick = Find.TickManager.TicksGame + Rand.Range(0, 100);
            //    }
            //}
            Log.Message("Test1");
        }
        // Token: 0x04000001 RID: 1
        public const int updatePeriodInTicks = 50;

        // Token: 0x04000002 RID: 2
        public int nextUpdateTick;
    }
}
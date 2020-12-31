using Verse;

namespace GlowingGhoul
{

    public class Comp_GlowingGhoul : CompGlower
    {

        public new CompProperties_GlowingGhoul Props => props as CompProperties_GlowingGhoul;

        public override void Initialize(CompProperties props)
        {
            base.Initialize(props);
        }

        public override void PostPostMake()
        {
            base.PostPostMake();
        }


        public IntVec3 vec3 = IntVec3.Invalid;

        public override void CompTick()
        {
            base.CompTick();
            Map map = parent.Map;
            if (map == null)
            {
                return;
            }
            IntVec3 position = parent.Position;
            if ((vec3 != IntVec3.Invalid && (vec3 == IntVec3.Invalid || vec3 == position)) || Find.TickManager.TicksGame < nextUpdateTick)
            {
                return;
            }
            nextUpdateTick = Find.TickManager.TicksGame + 50;
            map.mapDrawer.MapMeshDirty(parent.Position, MapMeshFlag.Things);
            map.glowGrid.DeRegisterGlower(this);
            map.mapDrawer.MapMeshDirty(parent.Position, MapMeshFlag.Things);
            map.glowGrid.RegisterGlower(this);
        }

        public override void PostSpawnSetup(bool respawningAfterLoad)
        {
            nextUpdateTick = Find.TickManager.TicksGame;
            //if (this.parent.Map != null)
            //{
            //    base.PostSpawnSetup(respawningAfterLoad);
            //    if (!respawningAfterLoad)
            //    {
            //        this.nextUpdateTick = Find.TickManager.TicksGame + Rand.Range(0, 100);
            //    }
            //}
        }

        public const int updatePeriodInTicks = 50;

        // Token: 0x04000002 RID: 2
        public int nextUpdateTick;
        public override void PostExposeData()
        {
            base.PostExposeData();
            Scribe_Values.Look(ref nextUpdateTick, "nextUpdateTick", 0);
        }
    }
}


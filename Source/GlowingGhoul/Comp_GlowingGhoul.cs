using Verse;

namespace GlowingGhoul;

public class Comp_GlowingGhoul : CompGlower
{
    public const int updatePeriodInTicks = 50;

    private readonly IntVec3 vec3 = IntVec3.Invalid;

    private Map lastMap;

    private int nextUpdateTick;

    public new CompProperties_GlowingGhoul Props => props as CompProperties_GlowingGhoul;

    public override void CompTick()
    {
        base.CompTick();
        var map = parent.Map;
        if (map == null)
        {
            lastMap?.glowGrid.DeRegisterGlower(this);

            return;
        }

        var position = parent.Position;
        if (vec3 != IntVec3.Invalid && (vec3 == IntVec3.Invalid || vec3 == position))
        {
            return;
        }

        if (Find.TickManager.TicksGame < nextUpdateTick)
        {
            return;
        }

        nextUpdateTick = Find.TickManager.TicksGame + 50;
        map.mapDrawer.MapMeshDirty(parent.Position, MapMeshFlag.Things);
        map.glowGrid.DeRegisterGlower(this);
        map.mapDrawer.MapMeshDirty(parent.Position, MapMeshFlag.Things);
        map.glowGrid.RegisterGlower(this);
        lastMap = map;
    }

    public override void PostExposeData()
    {
        base.PostExposeData();
        Scribe_Values.Look(ref nextUpdateTick, "nextUpdateTick");
        Scribe_References.Look(ref lastMap, "lastMap");
    }

    public override void PostSpawnSetup(bool respawningAfterLoad)
    {
        nextUpdateTick = Find.TickManager.TicksGame;

        // if (this.parent.Map != null)
        // {
        // base.PostSpawnSetup(respawningAfterLoad);
        // if (!respawningAfterLoad)
        // {
        // this.nextUpdateTick = Find.TickManager.TicksGame + Rand.Range(0, 100);
        // }
        // }
    }
}
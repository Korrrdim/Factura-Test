public class FixedHealthBar : HealthBar
{
    public override void Init(float startHealth)
    {
        base.Init(startHealth);
        BaseActive(true);
    }

    protected override void UpdatingStart()
    {
        BaseActive(true);
    }
    
    protected override void UpdatingEnd()
    {
        BaseActive(true);
    }
}

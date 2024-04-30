
public class DynamicHealthBar : HealthBar
{
    public override void Init(float startHealth)
    {
        base.Init(startHealth);
        BaseActive(false);
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

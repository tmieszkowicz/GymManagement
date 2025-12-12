namespace GymManagement.Domain.Subscriptions;

using GymManagement.SmartEnum;

public abstract class SubscriptionType : SmartEnum<SubscriptionType>
{

    public static readonly SubscriptionType Free = new FreeSubscriptionType();
    public static readonly SubscriptionType Starter = new StarterSubscriptionType();
    public static readonly SubscriptionType Pro = new ProSubscriptionType();

    private SubscriptionType(int value, string name) : base(value, name)
    {
    }

    private sealed class FreeSubscriptionType : SubscriptionType
    {
        public FreeSubscriptionType() : base(1, "Free")
        {
        }
    }

    private sealed class StarterSubscriptionType : SubscriptionType
    {
        public StarterSubscriptionType() : base(2, "Starter")
        {
        }
    }

    private sealed class ProSubscriptionType : SubscriptionType
    {
        public ProSubscriptionType() : base(3, "Pro")
        {
        }
    }
}

namespace MultitenantWebApp
{
    public interface ITenantStore
    {
        Tenant GetTenant(string tenandId);
    }

    public class TenantStore : ITenantStore
    {
        public Tenant GetTenant(string tenandId)
        {
            if (tenandId == "tenant1")
            {
                return new Tenant("tenant1");
            }

            if (tenandId == "tenant2")
            {
                return new Tenant("tenant2");
            }

            return null;
        }
    }
}
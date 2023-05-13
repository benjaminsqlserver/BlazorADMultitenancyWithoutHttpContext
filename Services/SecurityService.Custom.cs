using Microsoft.AspNetCore.Components;

namespace AzureADTenantPart2
{
    public partial class SecurityService
    {
        
        public async Task<ConDataService> CreateDataServiceForUser(NavigationManager navigationManager)
        {
            //to prevent data leak another tenant seeing other tenant's data
            
            var service = new ConDataService(navigationManager, _multitenancy);

            //get preferred username claim which is in email format

            var usernameClaim = Principal.Claims.Where(p => p.Type == "preferred_username").FirstOrDefault();
            bool userIsValid = false;
             if(usernameClaim != null)
            {
                userIsValid = await service.UserIsValid(usernameClaim.Value);
            }

            

            if (userIsValid)
            {
                return service;
            }
            else
            {
                return new ConDataService(_multitenancy.Tenants[2].ConnectionString);
            }


            
        }
    }
}

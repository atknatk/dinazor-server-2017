using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Dinazor.Core.Common.Model;
using Dinazor.Core.Database.Context;
using Dinazor.Core.Dto.Crm;
using Dinazor.Core.Interfaces.Licence; 

namespace Dinazor.Module.LicenceManagement.BusinessLayer
{
    public class LicenceOperation : ILicenceOperation
    {

        public async Task<DinazorResult<long>> GetClientByClientIdentifier(string clientIdentifier)
        {
            var result = new DinazorResult<long>();
            using (var ctx = new DinazorContext())
            {
                var model = ctx.Client.FirstOrDefault(l => !l.IsDeleted && l.ClientIdentifier == clientIdentifier);
                if (model != null && model.Id > 0)
                {
                    result.Data = model.Id;
                    result.Success();
                    return result;
                }
            }
            result.Data = -1;
            return result;
        }

        public async Task<DinazorResult<List<OrganizationLicenceDto>>> GetActiveLicence(long clientId)
        {
            var result = new DinazorResult<List<OrganizationLicenceDto>>();

            using (var ctx = new DinazorContext())
            {
                result.Data = ctx.OrganizationLicence
                    .Include(l => l.Client)
                    .Include(l => l.LicenceKey)
                    .Include(l => l.ModuleEnum)
                    .Include(l => l.Organization)
                    .Where(l => l.IdClient == clientId && l.ExpiresAt >= DateTime.Now && l.IsInUse)
                    .Select(new OrganizationLicenceDto().Select())
                    .ToList();

                result.Success();
            }
            return result;
        }
         
    }
}

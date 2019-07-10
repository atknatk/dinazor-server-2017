using System;
using System.Linq.Expressions;
using Dinazor.Core.Database.Entity.Crm;
using Dinazor.Core.Dto.Abstracts;
using LinqKit;


namespace Dinazor.Core.Dto.Crm
{
    public class OrganizationLicenceDto : UniqueDto<OrganizationLicence, OrganizationLicenceDto>
    {
        public int Duration { get; set; }
        public DateTime ExpiresAt { get; set; }
        public ClientDto Client { get; set; }
        public LicenceKeyDto LicenceKey { get; set; }
        public ModuleDto Module { get; set; }
        public OrganizationDto Organization { get; set; }
        public bool IsInUse { get; set; }

        public override Expression<Func<OrganizationLicence, OrganizationLicenceDto>> Select()
        {
            var clientSelect = new ClientDto().Select().Expand();
            var licenceKeySelect = new LicenceKeyDto().Select().Expand();
            var moduleSelect = new ModuleDto().Select().Expand();
            var organizationSelect = new OrganizationDto().Select().Expand();
            return l => new OrganizationLicenceDto()
            {
                Id = l.Id,
                Duration = l.Duration,
                ExpiresAt = l.ExpiresAt,
                Client = l.Client != null ? clientSelect.Invoke(l.Client) : null,
                LicenceKey = l.LicenceKey != null ? licenceKeySelect.Invoke(l.LicenceKey) : null,
                Module = l.ModuleEnum != null ? moduleSelect.Invoke(l.ModuleEnum) : null,
                Organization = l.Organization != null ? organizationSelect.Invoke(l.Organization) : null
            };
        }

        public override OrganizationLicence ToEntity()
        {
            throw new NotImplementedException();
        }

        public override Expression<Func<OrganizationLicence, bool>> IsExist()
        {
            throw new NotImplementedException();
        }
    }
}

using System;
using System.Collections;
using System.Data.Entity; 
using System.Data.Entity.Infrastructure; 
using System.Linq; 
using Dinazor.Core.Common.Enum;
using Dinazor.Core.Common.Extensions;
using Dinazor.Core.Common.Helper;
using Dinazor.Core.Database.Entity;
using Dinazor.Core.Database.Entity.Configuration;
using Dinazor.Core.Database.Entity.Crm;
using Dinazor.Core.Database.Entity.User;
using Dinazor.Core.Identity;
using Dinazor.Core.Interfaces.Databases;
using EntityFramework.DynamicFilters;
using log4net;
using Role = Dinazor.Core.Database.Entity.User.Role;

namespace Dinazor.Core.Database.Context
{
    public class DinazorContext : DbContext, IDinazorDbContext
    {
        private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private static string _connectionString;

        public static string ConnectionString
        {
            get => CheckConnectionString();

            set => _connectionString = value;
        }

        public DinazorContext() : base(nameOrConnectionString: CheckConnectionString())
        {
            Database.CreateIfNotExists();
            InitTenant();
        }

        private static string CheckConnectionString()
        {
            _log.Info("Connection String : " + _connectionString);

            if (string.IsNullOrEmpty(_connectionString))
            {
                _log.Info("Connection String : DinazorContext");
                return "DinazorContext";
            }
            return _connectionString;
        }

        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema("dinazor2017");
            modelBuilder.Filter("SoftDelete", (ISoftDelete d) => d.IsDeleted, false);

            modelBuilder.Filter("MustHaveTenant",
                (IMustHaveTenant t, long idTenant) => t.IdTenant == idTenant|| (long?)t.IdTenant== null,0);
        }

        #region CRM

        public DbSet<Client> Client { get; set; }
        public DbSet<Organization> Organization { get; set; }
        public DbSet<LicencePool> LicencePool { get; set; }
        public DbSet<LicenceKey> LicenceKey { get; set; }
        public DbSet<OrganizationLicence> OrganizationLicence { get; set; }
        public DbSet<MacAddress> MacAddress { get; set; }

        #endregion

        #region Enum

        public DbSet<ModuleEnum> ModuleEnum { get; set; }

        #endregion

        #region Authorization

        public DbSet<User> User { get; set; }
        public DbSet<UserGroup> UserGroup { get; set; }
        public DbSet<RelUserUserGroup> RelUserUserGroup { get; set; }

        public DbSet<Role> Role { get; set; }
        public DbSet<RoleGroup> RoleGroup { get; set; }
        public DbSet<RelRoleRoleGroup> RelRoleRoleGroup { get; set; }


        public DbSet<Authorization> Authorization { get; set; }

        #endregion

        #region Configuration

        public DbSet<Configuration> Configurations { get; set; }

        #endregion

        public DbSet<DinazorLog> DinazorLog { get; set; }

        public DbSet<City> City { get; set; }
        public DbSet<Town> Town { get; set; }

       
        public void GetDbEntityEntry<TEntity>(TEntity entity, DinazorEntityState state, out bool doRollback) where TEntity : class
        {
            doRollback = false;
            try
            {
                if (!(entity is IEntity)) return;

                if (state == DinazorEntityState.Save)
                {
                    IteratePropsSave(entity);

                    //handle the master
                    var entry = Entry<TEntity>(entity);
                    entry.State = ((IEntity)entity).Id <= 0 ? EntityState.Added : EntityState.Modified;
                }
                else if (state == DinazorEntityState.Update)
                {
                    IteratePropsUpdate(entity, out doRollback);

                    //handle the master
                    var entry = Entry<TEntity>(entity);
                    var _entity = (IEntity)entity;
                    if (_entity.Id > 0)
                    {
                        entry.State = EntityState.Modified;
                    }
                    else
                    {
                        doRollback = true;
                    }
                }
                else if (state == DinazorEntityState.SaveOrUpdate)
                {
                    IteratePropsSaveOrUpdate(entity);

                    var entry = Entry<TEntity>(entity);
                    entry.State = ((IEntity)entity).Id > 0 ? EntityState.Modified : EntityState.Added;
                }
            }
            catch (Exception e)
            {
                _log.Error("Get Db Entity Entry : " + e.GetAllMessages());
            }
        }
       
        private void IteratePropsSaveOrUpdate<TEntity>(TEntity entity) where TEntity : class
        {
            var props = entity.GetType().GetProperties();

            foreach (var property in props.Where(l => IsIEntity(l.PropertyType)))
            {
                //collection
                if (typeof(IEnumerable).IsAssignableFrom(property.PropertyType))
                {
                    var entityList = (ICollection)entity.GetValue(property.Name);

                    if (entityList == null)
                    {
                        return;
                    }

                    foreach (var entitiyItem in entityList)
                    {
                        IEntity subEntityOfList = (IEntity)entitiyItem;
                        if (subEntityOfList == null) continue;
                        IteratePropsSave(subEntityOfList);
                        var entry = Entry<IEntity>(subEntityOfList);
                        entry.State = ((IEntity)subEntityOfList).Id > 0 ? EntityState.Modified : EntityState.Added;
                    }
                }
                else
                {
                    IEntity subEntity = (IEntity)entity.GetValue(property.Name);
                    if (subEntity == null) continue;
                    DbEntityEntry entityEntry = Entry(subEntity);
                    IteratePropsSave(subEntity);
                    entityEntry.State = ((IEntity)subEntity).Id > 0 ? EntityState.Modified : EntityState.Added;
                }
            }
        }

        private void IteratePropsUpdate<TEntity>(TEntity entity, out bool doRollback) where TEntity : class
        {
            var props = entity.GetType().GetProperties();
            doRollback = false;

            foreach (var property in props.Where(l => IsIEntity(l.PropertyType)))
            {
                //collection
                if (typeof(IEnumerable).IsAssignableFrom(property.PropertyType))
                {
                    var entityList = (ICollection)entity.GetValue(property.Name);

                    if (entityList == null)
                    {
                        doRollback = false;
                        return;
                    }

                    foreach (var entitiyItem in entityList)
                    {
                        IEntity subEntityOfList = (IEntity)entitiyItem;
                        if (subEntityOfList == null) continue;
                        bool _doRollback = false;
                        IteratePropsUpdate(subEntityOfList, out _doRollback);
                        var entry = Entry<IEntity>(subEntityOfList);
                        var _entity = (IEntity)subEntityOfList;
                        if (_entity.Id > 0)
                        {
                            entry.State = EntityState.Modified;
                        }
                        else
                        {
                            doRollback = true;
                            return;
                        }
                    }
                }
                else
                {
                    IEntity subEntity = (IEntity)entity.GetValue(property.Name);
                    if (subEntity == null) continue;
                    DbEntityEntry entityEntry = Entry(subEntity);
                    bool _doRollback = false;
                    IteratePropsUpdate(subEntity, out _doRollback);
                    var _entity = (IEntity)subEntity;
                    if (_entity.Id > 0)
                    {
                        entityEntry.State = EntityState.Modified;
                    }
                    else
                    {
                        doRollback = true;
                        return;
                    }
                }
            }
        }

        private void IteratePropsSave<TEntity>(TEntity entity) where TEntity : class
        {
            var props = entity.GetType().GetProperties();

            foreach (var property in props.Where(l => IsIEntity(l.PropertyType)))
            {
                //collection
                if (typeof(IEnumerable).IsAssignableFrom(property.PropertyType))
                {
                    var entityList = (ICollection)entity.GetValue(property.Name);

                    if (entityList == null)
                    {
                        return;
                    }

                    foreach (var entitiyItem in entityList)
                    {
                        IEntity subEntityOfList = (IEntity)entitiyItem;
                        if (subEntityOfList == null) continue;
                        IteratePropsSave(subEntityOfList);
                        var entry = Entry<IEntity>(subEntityOfList);
                        entry.State = ((IEntity)subEntityOfList).Id <= 0 ? EntityState.Added : EntityState.Modified;
                    }
                }
                else
                {
                    IEntity subEntity = (IEntity)entity.GetValue(property.Name);
                    if (subEntity == null) continue;
                    DbEntityEntry entityEntry = Entry(subEntity);
                    IteratePropsSave(subEntity);
                    entityEntry.State = ((IEntity)subEntity).Id <= 0 ? EntityState.Added : EntityState.Modified;
                }
            }
        }

        private bool IsIEntity(Type type)
        {
            if (type.IsGenericType)
            {
                var genericList = type.GenericTypeArguments;
                foreach (var item in genericList)
                {
                    return item.GetInterfaces().Any(l => l == typeof(IEntity));
                }
            }
            return type.GetInterfaces().Any(l => l == typeof(IEntity));
        }

        private void InitTenant()
        {

            long idTenant = 0;
            try
            {
                var idTenantR = CustomAttributeHelper.GetCustomAttribute("idTenant");
                if (idTenantR!=null)
                {
                    idTenant = (long) idTenantR;
                } 
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            this.SetFilterScopedParameterValue("MustHaveTenant", "idTenant", idTenant);
        }
    }

}

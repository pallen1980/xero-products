using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XeroProducts.Types;

namespace XeroProducts.DAL.EntityFramework.Sql.Contexts
{
    public interface IXeroProductsContext
    {
        DbSet<Product> Products { get; set; }
        DbSet<ProductOption> ProductOptions { get; set; }
        DbSet<User> Users { get; set; }

        /// <summary>
        ///     Saves all changes made in this context to the database.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         This method will automatically call <see cref="Microsoft.EntityFrameworkCore.ChangeTracking.ChangeTracker.DetectChanges" />
        ///         to discover any changes to entity instances before saving to the underlying database. This can be disabled via
        ///         <see cref="Microsoft.EntityFrameworkCore.ChangeTracking.ChangeTracker.AutoDetectChangesEnabled" />.
        ///     </para>
        ///     <para>
        ///         Entity Framework Core does not support multiple parallel operations being run on the same DbContext instance. This
        ///         includes both parallel execution of async queries and any explicit concurrent use from multiple threads.
        ///         Therefore, always await async calls immediately, or use separate DbContext instances for operations that execute
        ///         in parallel. See <see href="https://aka.ms/efcore-docs-threading">Avoiding DbContext threading issues</see> for more information
        ///         and examples.
        ///     </para>
        ///     <para>
        ///         See <see href="https://aka.ms/efcore-docs-saving-data">Saving data in EF Core</see> for more information and examples.
        ///     </para>
        /// </remarks>
        /// <returns>
        ///     The number of state entries written to the database.
        /// </returns>
        /// <exception cref="DbUpdateException">
        ///     An error is encountered while saving to the database.
        /// </exception>
        /// <exception cref="DbUpdateConcurrencyException">
        ///     A concurrency violation is encountered while saving to the database.
        ///     A concurrency violation occurs when an unexpected number of rows are affected during save.
        ///     This is usually because the data in the database has been modified since it was loaded into memory.
        /// </exception>
        int SaveChanges();

        /// <summary>
        ///     Saves all changes made in this context to the database.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         This method will automatically call <see cref="Microsoft.EntityFrameworkCore.ChangeTracking.ChangeTracker.DetectChanges" />
        ///         to discover any changes to entity instances before saving to the underlying database. This can be disabled via
        ///         <see cref="Microsoft.EntityFrameworkCore.ChangeTracking.ChangeTracker.AutoDetectChangesEnabled" />.
        ///     </para>
        ///     <para>
        ///         Entity Framework Core does not support multiple parallel operations being run on the same DbContext instance. This
        ///         includes both parallel execution of async queries and any explicit concurrent use from multiple threads.
        ///         Therefore, always await async calls immediately, or use separate DbContext instances for operations that execute
        ///         in parallel. See <see href="https://aka.ms/efcore-docs-threading">Avoiding DbContext threading issues</see> for more information
        ///         and examples.
        ///     </para>
        ///     <para>
        ///         See <see href="https://aka.ms/efcore-docs-saving-data">Saving data in EF Core</see> for more information and examples.
        ///     </para>
        /// </remarks>
        /// <param name="acceptAllChangesOnSuccess">
        ///     Indicates whether <see cref="Microsoft.EntityFrameworkCore.ChangeTracking.ChangeTracker.AcceptAllChanges" />
        ///     is called after the changes have been sent successfully to the database.
        /// </param>
        /// <returns>
        ///     The number of state entries written to the database.
        /// </returns>
        /// <exception cref="DbUpdateException">
        ///     An error is encountered while saving to the database.
        /// </exception>
        /// <exception cref="DbUpdateConcurrencyException">
        ///     A concurrency violation is encountered while saving to the database.
        ///     A concurrency violation occurs when an unexpected number of rows are affected during save.
        ///     This is usually because the data in the database has been modified since it was loaded into memory.
        /// </exception>
        int SaveChanges(bool acceptAllChangesOnSuccess);

        /// <summary>
        ///     Saves all changes made in this context to the database.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         This method will automatically call <see cref="Microsoft.EntityFrameworkCore.ChangeTracking.ChangeTracker.DetectChanges" />
        ///         to discover any changes to entity instances before saving to the underlying database. This can be disabled via
        ///         <see cref="Microsoft.EntityFrameworkCore.ChangeTracking.ChangeTracker.AutoDetectChangesEnabled" />.
        ///     </para>
        ///     <para>
        ///         Entity Framework Core does not support multiple parallel operations being run on the same DbContext instance. This
        ///         includes both parallel execution of async queries and any explicit concurrent use from multiple threads.
        ///         Therefore, always await async calls immediately, or use separate DbContext instances for operations that execute
        ///         in parallel. See <see href="https://aka.ms/efcore-docs-threading">Avoiding DbContext threading issues</see> for more
        ///         information and examples.
        ///     </para>
        ///     <para>
        ///         See <see href="https://aka.ms/efcore-docs-saving-data">Saving data in EF Core</see> for more information and examples.
        ///     </para>
        /// </remarks>
        /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
        /// <returns>
        ///     A task that represents the asynchronous save operation. The task result contains the
        ///     number of state entries written to the database.
        /// </returns>
        /// <exception cref="DbUpdateException">
        ///     An error is encountered while saving to the database.
        /// </exception>
        /// <exception cref="DbUpdateConcurrencyException">
        ///     A concurrency violation is encountered while saving to the database.
        ///     A concurrency violation occurs when an unexpected number of rows are affected during save.
        ///     This is usually because the data in the database has been modified since it was loaded into memory.
        /// </exception>
        /// <exception cref="OperationCanceledException">If the <see cref="CancellationToken" /> is canceled.</exception>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        /// <summary>
        ///     Saves all changes made in this context to the database.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         This method will automatically call <see cref="Microsoft.EntityFrameworkCore.ChangeTracking.ChangeTracker.DetectChanges" />
        ///         to discover any changes to entity instances before saving to the underlying database. This can be disabled via
        ///         <see cref="Microsoft.EntityFrameworkCore.ChangeTracking.ChangeTracker.AutoDetectChangesEnabled" />.
        ///     </para>
        ///     <para>
        ///         Entity Framework Core does not support multiple parallel operations being run on the same DbContext instance. This
        ///         includes both parallel execution of async queries and any explicit concurrent use from multiple threads.
        ///         Therefore, always await async calls immediately, or use separate DbContext instances for operations that execute
        ///         in parallel. See <see href="https://aka.ms/efcore-docs-threading">Avoiding DbContext threading issues</see> for more
        ///         information and examples.
        ///     </para>
        ///     <para>
        ///         See <see href="https://aka.ms/efcore-docs-saving-data">Saving data in EF Core</see> for more information and examples.
        ///     </para>
        /// </remarks>
        /// <param name="acceptAllChangesOnSuccess">
        ///     Indicates whether <see cref="Microsoft.EntityFrameworkCore.ChangeTracking.ChangeTracker.AcceptAllChanges" /> is called after
        ///     the changes have been sent successfully to the database.
        /// </param>
        /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
        /// <returns>
        ///     A task that represents the asynchronous save operation. The task result contains the
        ///     number of state entries written to the database.
        /// </returns>
        /// <exception cref="DbUpdateException">
        ///     An error is encountered while saving to the database.
        /// </exception>
        /// <exception cref="DbUpdateConcurrencyException">
        ///     A concurrency violation is encountered while saving to the database.
        ///     A concurrency violation occurs when an unexpected number of rows are affected during save.
        ///     This is usually because the data in the database has been modified since it was loaded into memory.
        /// </exception>
        /// <exception cref="OperationCanceledException">If the <see cref="CancellationToken" /> is canceled.</exception>
        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);

        /// <summary>
        ///     Begins tracking the given entity in the <see cref="EntityState.Deleted" /> state such that it will
        ///     be removed from the database when <see cref="SaveChanges()" /> is called.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         If the entity is already tracked in the <see cref="EntityState.Added" /> state then the context will
        ///         stop tracking the entity (rather than marking it as <see cref="EntityState.Deleted" />) since the
        ///         entity was previously added to the context and does not exist in the database.
        ///     </para>
        ///     <para>
        ///         Any other reachable entities that are not already being tracked will be tracked in the same way that
        ///         they would be if <see cref="Attach(object)" /> was called before calling this method.
        ///         This allows any cascading actions to be applied when <see cref="SaveChanges()" /> is called.
        ///     </para>
        ///     <para>
        ///         Use <see cref="EntityEntry.State" /> to set the state of only a single entity.
        ///     </para>
        ///     <para>
        ///         See <see href="https://aka.ms/efcore-docs-change-tracking">EF Core change tracking</see> for more information and examples.
        ///     </para>
        /// </remarks>
        /// <param name="entity">The entity to remove.</param>
        /// <returns>
        ///     The <see cref="EntityEntry" /> for the entity. The entry provides
        ///     access to change tracking information and operations for the entity.
        /// </returns>
        EntityEntry Remove(object entity);
    }
}

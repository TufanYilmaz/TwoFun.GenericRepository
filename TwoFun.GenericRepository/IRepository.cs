using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwoFun.GenericRepository
{
    public interface IRepository
    {
        /// <summary>
        /// This method takes <typeparamref name="TEntity"/>, insert it into the database and returns <see cref="Task"/>.
        /// Bu method parametre olarak <typeparamref name="TEntity"/> alır , veri tabanına kaydeder ve  <see cref="Task"/>. dönüşü verir
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity. -- Entity tipi</typeparam>
        /// <param name="entities">The entities to be inserted. Eklenecek Entity'ler</param>
        /// <param name="cancellationToken"> A <see cref="CancellationToken"/> to observe while waiting for the task to complete. İptal etmek için Token gönderiliyor ve istek iptali tepeden en alta taşınıyor</param>
        /// <returns>Returns <see cref="Task"/>. Dönüş olarak Task verir</returns>
        Task InsertAsync<TEntity>(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
            where TEntity : class;


        /// <summary>
        /// This method takes <typeparamref name="TEntity"/>, insert it into database and returns <see cref="Task{TResult}"/>.
        /// Bu method <typeparamref name="TEntity"/> parametresi alır,veri tabanına kaydeder ve dönüş olarak <see cref="Task{TResult}"/> verir.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity. Entity tipi</typeparam>
        /// <param name="entity">The entity to be inserted. Eklencek Entity</param>
        /// <param name="cancellationToken"> A <see cref="CancellationToken"/> to observe while waiting for the task to complete. İptal etmek için Token gönderiliyor ve istek iptali tepeden en alta taşınıyor</param>
        /// <returns>Returns <see cref="Task"/>. Dönderir</returns>
        Task<object?[]> InsertAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default)
            where TEntity : class;

        /// <summary>
        /// Saves all changes made in this context to the database.
        /// Bu Contexte yapılan değişiklikleri Veri tabanına Uygular.
        /// </summary>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.  İptal etmek için Token gönderiliyor ve istek iptali tepeden en alta taşınıyor</param>
        /// <returns>
        /// A <see cref="Task"/> that represents the asynchronous save operation. The task result contains the number of state entries written to the database.
        ///  <see cref="Task"/> asnekron dönüş verir. Veritabanında yapılan değişiklikleri getirir.
        /// </returns>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// This method takes an <typeparamref name="TEntity"/> object, mark the object as <see cref="EntityState.Added"/> to the <see cref="ChangeTracker"/> of the <see cref="DbContext"/>.
        /// Bu Method <typeparamref name="TEntity"/> objesi alır, <see cref="EntityState.Added"/> olarak işaretler <see cref="ChangeTracker"/> görebilirsinz <see cref="DbContext"/>.
        /// <para>
        /// <see cref="SaveChangesAsync(CancellationToken)"/> methodunu çağırarak veritabanı değişikliklerini gerçekleştirir
        /// </para>
        /// </summary>
        /// <typeparam name="TEntity"> Eklenecek Tip <paramref name="entity"/> </typeparam>
        /// <param name="entity">The <typeparamref name="TEntity"/> object to be inserted to the database on <see cref="SaveChangesAsync(CancellationToken)"/>.
        /// veritabanındaki değişiklikleri görmek için. </param>
        void Add<TEntity>(TEntity entity)
            where TEntity : class;

        /// <summary>
        /// This method takes an object of <typeparamref name="TEntity"/>, adds it to the change tracker and will
        /// be inserted into the database when <see cref="IRepository.SaveChangesAsync(CancellationToken)" /> is called.
        ///  Bu Method<typeparamref name="TEntity"/>, Tipi alır Change Tracker  <see cref="IRepository.SaveChangesAsync(CancellationToken)" /> çağrıldığında tetklenir.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity. Entity Tipi</typeparam>
        /// <param name="entity">The entity to be inserted. Eklencek Entity</param>
        /// <param name="cancellationToken"> A <see cref="CancellationToken"/> to observe while waiting for the task to complete. İptal etmek için Token gönderiliyor ve istek iptali tepeden en alta taşınıyor</param>
        /// <returns>Returns <see cref="Task"/>.</returns>
        Task AddAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default)
            where TEntity : class;

        /// <summary>
        /// This method takes <see cref="IEnumerable{TEntity}"/> objects, mark the objects as <see cref="EntityState.Added"/> to the <see cref="ChangeTracker"/> of the <see cref="DbContext"/>.
        /// Bu Method  <see cref="IEnumerable{TEntity}"/> tipi alır, <see cref="EntityState.Added"/> olarak <see cref="ChangeTracker"/> da işaretler <see cref="DbContext"/>.
        /// <para>
        /// Call <see cref="SaveChangesAsync(CancellationToken)"/> to persist the changes to the database.
        /// Veritabanı değişiklikleri için Methodu çağrılmalıdır.
        /// </para>
        /// </summary>
        /// <typeparam name="TEntity">The type of the <paramref name="entities"/> to be added. Eklencek Entity'ler</typeparam>
        /// <param name="entities">The <typeparamref name="TEntity"/> objects to be inserted to the database on Eklenen Entityler <see cref="SaveChangesAsync(CancellationToken)"/>.</param>
        void Add<TEntity>(IEnumerable<TEntity> entities)
            where TEntity : class;
        /// <summary>
        /// This method takes a collection of <typeparamref name="TEntity"/> object, adds them to the change tracker and will
        /// be inserted into the database when <see cref="IRepository.SaveChangesAsync(CancellationToken)" /> is called.
        /// Bu Method <typeparamref name="TEntity"/> objesi alır ,  <see cref="IRepository.SaveChangesAsync(CancellationToken)" /> çağrıldığında
        /// Veritabanına eklenir ve Tracker ile takip edilebilir
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity. Entity Tipi</typeparam>
        /// <param name="entities">The entities to be inserted. Eklenecek Entity'ler</param>
        /// <param name="cancellationToken"> A <see cref="CancellationToken"/> to observe while waiting for the task to complete. İptal etmek için Token gönderiliyor ve istek iptali tepeden en alta taşınıyor</param>
        /// <returns>Returns <see cref="Task"/>.</returns>
        Task AddAsync<TEntity>(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
            where TEntity : class;
        /// <summary>
        /// This method takes an <typeparamref name="TEntity"/> object, mark the object as <see cref="EntityState.Deleted"/> to the <see cref="ChangeTracker"/> of the <see cref="DbContext"/>.
        /// Bu Method <typeparamref name="TEntity"/> objesi alır, objeyi <see cref="EntityState.Deleted"/>  olarak işaretler <see cref="ChangeTracker"/> da takip edilir <see cref="DbContext"/> de görülür.
        /// <para>
        /// Call <see cref="SaveChangesAsync(CancellationToken)"/> to persist the changes to the database.
        /// Değişiklikleri veritabanına kaydeder
        /// </para>
        /// </summary>
        /// <typeparam name="TEntity">The type of the <paramref name="entity"/> to be marked as deleted. Silinen Entity değeri silindi olarak işaretlenir</typeparam>
        /// <param name="entity">The <typeparamref name="TEntity"/> object to be deleted from the database on <see cref="SaveChangesAsync(CancellationToken)"/>.Databae değişiklikleri için çağırın</param>
        void Remove<TEntity>(TEntity entity)
            where TEntity : class;

        /// <summary>
        /// This method takes <see cref="IEnumerable{TEntity}"/> objects, mark the objects as <see cref="EntityState.Deleted"/> to the <see cref="ChangeTracker"/> of the <see cref="DbContext"/>.
        /// Bu method  <see cref="IEnumerable{TEntity}"/> objesi alır,  <see cref="EntityState.Deleted"/> olarak işaretler <see cref="ChangeTracker"/> <see cref="DbContext"/> de görünür.
        /// <para>
        /// Call <see cref="SaveChangesAsync(CancellationToken)"/> to persist the changes to the database.
        /// Değişiklikleri veratabnına uygulmak için 
        /// </para>
        /// </summary>
        /// <typeparam name="TEntity">The type of the <paramref name="entities"/> to be marked as deleted.TEntity Deleted olarak işaretlenir</typeparam>
        /// <param name="entities">The <typeparamref name="TEntity"/> objects to be deleted from the database on <see cref="SaveChangesAsync(CancellationToken)"/>.</param> Silinen objeyi veritabanından silme ikin çağrılır.
        void Remove<TEntity>(IEnumerable<TEntity> entities)
            where TEntity : class;

    }
}

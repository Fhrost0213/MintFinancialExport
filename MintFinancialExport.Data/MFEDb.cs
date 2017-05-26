﻿

// ------------------------------------------------------------------------------------------------
// This code was generated by EntityFramework Reverse POCO Generator (http://www.reversepoco.com/).
// Created by Simon Hughes (https://about.me/simon.hughes).
//
// Do not make changes directly to this file - edit the template instead.
//
// The following connection settings were used to generate this file:
//     Configuration file:     "MintFinancialExport\App.config"
//     Connection String Name: "MyDbContext"
//     Connection String:      "Data Source=DESKTOP-ACSKTG6;Initial Catalog=MintFinancialExport;Integrated Security=True;Application Name=Mint Financial Export"
// ------------------------------------------------------------------------------------------------
// Database Edition       : Standard Edition (64-bit)
// Database Engine Edition: Standard

// <auto-generated>
// ReSharper disable ConvertPropertyToExpressionBody
// ReSharper disable DoNotCallOverridableMethodsInConstructor
// ReSharper disable InconsistentNaming
// ReSharper disable PartialMethodWithSinglePart
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable RedundantNameQualifier
// ReSharper disable RedundantOverridenMember
// ReSharper disable UseNameofExpression
// TargetFrameworkVersion = 4.5
#pragma warning disable 1591    //  Ignore "Missing XML Comment" warning


namespace MintFinancialExport.Data
{
    using System.Linq;

    #region Unit of work

    public interface IMyDbContext : System.IDisposable
    {
        System.Data.Entity.DbSet<Account> Accounts { get; set; } // Account
        System.Data.Entity.DbSet<AccountHistory> AccountHistories { get; set; } // AccountHistory
        System.Data.Entity.DbSet<AccountMapping> AccountMappings { get; set; } // AccountMapping
        System.Data.Entity.DbSet<AccountType> AccountTypes { get; set; } // AccountType
        System.Data.Entity.DbSet<NetWorthHistory> NetWorthHistories { get; set; } // NetWorthHistory
        System.Data.Entity.DbSet<PreciousMetalsHistory> PreciousMetalsHistories { get; set; } // PreciousMetalsHistory
        System.Data.Entity.DbSet<User> Users { get; set; } // User

        int SaveChanges();
        System.Threading.Tasks.Task<int> SaveChangesAsync();
        System.Threading.Tasks.Task<int> SaveChangesAsync(System.Threading.CancellationToken cancellationToken);
        System.Data.Entity.Infrastructure.DbChangeTracker ChangeTracker { get; }
        System.Data.Entity.Infrastructure.DbContextConfiguration Configuration { get; }
        System.Data.Entity.Database Database { get; }
        System.Data.Entity.Infrastructure.DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
        System.Data.Entity.Infrastructure.DbEntityEntry Entry(object entity);
        System.Collections.Generic.IEnumerable<System.Data.Entity.Validation.DbEntityValidationResult> GetValidationErrors();
        System.Data.Entity.DbSet Set(System.Type entityType);
        System.Data.Entity.DbSet<TEntity> Set<TEntity>() where TEntity : class;
        string ToString();
    }

    #endregion

    #region Database context

    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.30.0.0")]
    public partial class MyDbContext : System.Data.Entity.DbContext, IMyDbContext
    {
        public System.Data.Entity.DbSet<Account> Accounts { get; set; } // Account
        public System.Data.Entity.DbSet<AccountHistory> AccountHistories { get; set; } // AccountHistory
        public System.Data.Entity.DbSet<AccountMapping> AccountMappings { get; set; } // AccountMapping
        public System.Data.Entity.DbSet<AccountType> AccountTypes { get; set; } // AccountType
        public System.Data.Entity.DbSet<NetWorthHistory> NetWorthHistories { get; set; } // NetWorthHistory
        public System.Data.Entity.DbSet<PreciousMetalsHistory> PreciousMetalsHistories { get; set; } // PreciousMetalsHistory
        public System.Data.Entity.DbSet<User> Users { get; set; } // User

        static MyDbContext()
        {
            System.Data.Entity.Database.SetInitializer<MyDbContext>(null);
            //MyDbContextStaticPartial(); // Create the following method in your partial class: private static void MyDbContextStaticPartial() { }
        }

        public MyDbContext()
            : base("Name=MyDbContext")
        {
            InitializePartial();
        }

        public MyDbContext(string connectionString)
            : base(connectionString)
        {
            InitializePartial();
        }

        public MyDbContext(string connectionString, System.Data.Entity.Infrastructure.DbCompiledModel model)
            : base(connectionString, model)
        {
            InitializePartial();
        }

        public MyDbContext(System.Data.Common.DbConnection existingConnection, bool contextOwnsConnection)
            : base(existingConnection, contextOwnsConnection)
        {
            InitializePartial();
        }

        public MyDbContext(System.Data.Common.DbConnection existingConnection, System.Data.Entity.Infrastructure.DbCompiledModel model, bool contextOwnsConnection)
            : base(existingConnection, model, contextOwnsConnection)
        {
            InitializePartial();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        public bool IsSqlParameterNull(System.Data.SqlClient.SqlParameter param)
        {
            var sqlValue = param.SqlValue;
            var nullableValue = sqlValue as System.Data.SqlTypes.INullable;
            if (nullableValue != null)
                return nullableValue.IsNull;
            return (sqlValue == null || sqlValue == System.DBNull.Value);
        }

        protected override void OnModelCreating(System.Data.Entity.DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new AccountConfiguration());
            modelBuilder.Configurations.Add(new AccountHistoryConfiguration());
            modelBuilder.Configurations.Add(new AccountMappingConfiguration());
            modelBuilder.Configurations.Add(new AccountTypeConfiguration());
            modelBuilder.Configurations.Add(new NetWorthHistoryConfiguration());
            modelBuilder.Configurations.Add(new PreciousMetalsHistoryConfiguration());
            modelBuilder.Configurations.Add(new UserConfiguration());

            OnModelCreatingPartial(modelBuilder);
        }

        public static System.Data.Entity.DbModelBuilder CreateModel(System.Data.Entity.DbModelBuilder modelBuilder, string schema)
        {
            modelBuilder.Configurations.Add(new AccountConfiguration(schema));
            modelBuilder.Configurations.Add(new AccountHistoryConfiguration(schema));
            modelBuilder.Configurations.Add(new AccountMappingConfiguration(schema));
            modelBuilder.Configurations.Add(new AccountTypeConfiguration(schema));
            modelBuilder.Configurations.Add(new NetWorthHistoryConfiguration(schema));
            modelBuilder.Configurations.Add(new PreciousMetalsHistoryConfiguration(schema));
            modelBuilder.Configurations.Add(new UserConfiguration(schema));
            return modelBuilder;
        }

        partial void InitializePartial();
        partial void OnModelCreatingPartial(System.Data.Entity.DbModelBuilder modelBuilder);
    }
    #endregion

    #region Fake Database context

    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.30.0.0")]
    public partial class FakeMyDbContext : IMyDbContext
    {
        public System.Data.Entity.DbSet<Account> Accounts { get; set; }
        public System.Data.Entity.DbSet<AccountHistory> AccountHistories { get; set; }
        public System.Data.Entity.DbSet<AccountMapping> AccountMappings { get; set; }
        public System.Data.Entity.DbSet<AccountType> AccountTypes { get; set; }
        public System.Data.Entity.DbSet<NetWorthHistory> NetWorthHistories { get; set; }
        public System.Data.Entity.DbSet<PreciousMetalsHistory> PreciousMetalsHistories { get; set; }
        public System.Data.Entity.DbSet<User> Users { get; set; }

        public FakeMyDbContext()
        {
            Accounts = new FakeDbSet<Account>("ObjectId");
            AccountHistories = new FakeDbSet<AccountHistory>("ObjectId");
            AccountMappings = new FakeDbSet<AccountMapping>("ObjectId");
            AccountTypes = new FakeDbSet<AccountType>("ObjectId");
            NetWorthHistories = new FakeDbSet<NetWorthHistory>("ObjectId");
            PreciousMetalsHistories = new FakeDbSet<PreciousMetalsHistory>("ObjectId");
            Users = new FakeDbSet<User>("ObjectId");

            InitializePartial();
        }

        public int SaveChangesCount { get; private set; }
        public int SaveChanges()
        {
            ++SaveChangesCount;
            return 1;
        }

        public System.Threading.Tasks.Task<int> SaveChangesAsync()
        {
            ++SaveChangesCount;
            return System.Threading.Tasks.Task<int>.Factory.StartNew(() => 1);
        }

        public System.Threading.Tasks.Task<int> SaveChangesAsync(System.Threading.CancellationToken cancellationToken)
        {
            ++SaveChangesCount;
            return System.Threading.Tasks.Task<int>.Factory.StartNew(() => 1, cancellationToken);
        }

        partial void InitializePartial();

        protected virtual void Dispose(bool disposing)
        {
        }

        public void Dispose()
        {
            Dispose(true);
        }

        public System.Data.Entity.Infrastructure.DbChangeTracker _changeTracker;
        public System.Data.Entity.Infrastructure.DbChangeTracker ChangeTracker { get { return _changeTracker; } }
        public System.Data.Entity.Infrastructure.DbContextConfiguration _configuration;
        public System.Data.Entity.Infrastructure.DbContextConfiguration Configuration { get { return _configuration; } }
        public System.Data.Entity.Database _database;
        public System.Data.Entity.Database Database { get { return _database; } }
        public System.Data.Entity.Infrastructure.DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class
        {
            throw new System.NotImplementedException();
        }
        public System.Data.Entity.Infrastructure.DbEntityEntry Entry(object entity)
        {
            throw new System.NotImplementedException();
        }
        public System.Collections.Generic.IEnumerable<System.Data.Entity.Validation.DbEntityValidationResult> GetValidationErrors()
        {
            throw new System.NotImplementedException();
        }
        public System.Data.Entity.DbSet Set(System.Type entityType)
        {
            throw new System.NotImplementedException();
        }
        public System.Data.Entity.DbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            throw new System.NotImplementedException();
        }
        public override string ToString()
        {
            throw new System.NotImplementedException();
        }

    }

    // ************************************************************************
    // Fake DbSet
    // Implementing Find:
    //      The Find method is difficult to implement in a generic fashion. If
    //      you need to test code that makes use of the Find method it is
    //      easiest to create a test DbSet for each of the entity types that
    //      need to support find. You can then write logic to find that
    //      particular type of entity, as shown below:
    //      public class FakeBlogDbSet : FakeDbSet<Blog>
    //      {
    //          public override Blog Find(params object[] keyValues)
    //          {
    //              var id = (int) keyValues.Single();
    //              return this.SingleOrDefault(b => b.BlogId == id);
    //          }
    //      }
    //      Read more about it here: https://msdn.microsoft.com/en-us/data/dn314431.aspx
    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.30.0.0")]
    public partial class FakeDbSet<TEntity> : System.Data.Entity.DbSet<TEntity>, IQueryable, System.Collections.Generic.IEnumerable<TEntity>, System.Data.Entity.Infrastructure.IDbAsyncEnumerable<TEntity> where TEntity : class
    {
        private readonly System.Reflection.PropertyInfo[] _primaryKeys;
        private readonly System.Collections.ObjectModel.ObservableCollection<TEntity> _data;
        private readonly IQueryable _query;

        public FakeDbSet()
        {
            _data = new System.Collections.ObjectModel.ObservableCollection<TEntity>();
            _query = _data.AsQueryable();

            InitializePartial();
        }

        public FakeDbSet(params string[] primaryKeys)
        {
            _primaryKeys = typeof(TEntity).GetProperties().Where(x => primaryKeys.Contains(x.Name)).ToArray();
            _data = new System.Collections.ObjectModel.ObservableCollection<TEntity>();
            _query = _data.AsQueryable();

            InitializePartial();
        }

        public override TEntity Find(params object[] keyValues)
        {
            if (_primaryKeys == null)
                throw new System.ArgumentException("No primary keys defined");
            if (keyValues.Length != _primaryKeys.Length)
                throw new System.ArgumentException("Incorrect number of keys passed to Find method");

            var keyQuery = this.AsQueryable();
            keyQuery = keyValues
                .Select((t, i) => i)
                .Aggregate(keyQuery,
                    (current, x) =>
                        current.Where(entity => _primaryKeys[x].GetValue(entity, null).Equals(keyValues[x])));

            return keyQuery.SingleOrDefault();
        }

        public override System.Threading.Tasks.Task<TEntity> FindAsync(System.Threading.CancellationToken cancellationToken, params object[] keyValues)
        {
            return System.Threading.Tasks.Task<TEntity>.Factory.StartNew(() => Find(keyValues), cancellationToken);
        }

        public override System.Threading.Tasks.Task<TEntity> FindAsync(params object[] keyValues)
        {
            return System.Threading.Tasks.Task<TEntity>.Factory.StartNew(() => Find(keyValues));
        }

        public override System.Collections.Generic.IEnumerable<TEntity> AddRange(System.Collections.Generic.IEnumerable<TEntity> entities)
        {
            if (entities == null) throw new System.ArgumentNullException("entities");
            var items = entities.ToList();
            foreach (var entity in items)
            {
                _data.Add(entity);
            }
            return items;
        }

        public override TEntity Add(TEntity item)
        {
            if (item == null) throw new System.ArgumentNullException("item");
            _data.Add(item);
            return item;
        }

        public override System.Collections.Generic.IEnumerable<TEntity> RemoveRange(System.Collections.Generic.IEnumerable<TEntity> entities)
        {
            if (entities == null) throw new System.ArgumentNullException("entities");
            var items = entities.ToList();
            foreach (var entity in items)
            {
                _data.Remove(entity);
            }
            return items;
        }

        public override TEntity Remove(TEntity item)
        {
            if (item == null) throw new System.ArgumentNullException("item");
            _data.Remove(item);
            return item;
        }

        public override TEntity Attach(TEntity item)
        {
            if (item == null) throw new System.ArgumentNullException("item");
            _data.Add(item);
            return item;
        }

        public override TEntity Create()
        {
            return System.Activator.CreateInstance<TEntity>();
        }

        public override TDerivedEntity Create<TDerivedEntity>()
        {
            return System.Activator.CreateInstance<TDerivedEntity>();
        }

        public override System.Collections.ObjectModel.ObservableCollection<TEntity> Local
        {
            get { return _data; }
        }

        System.Type IQueryable.ElementType
        {
            get { return _query.ElementType; }
        }

        System.Linq.Expressions.Expression IQueryable.Expression
        {
            get { return _query.Expression; }
        }

        IQueryProvider IQueryable.Provider
        {
            get { return new FakeDbAsyncQueryProvider<TEntity>(_query.Provider); }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _data.GetEnumerator();
        }

        System.Collections.Generic.IEnumerator<TEntity> System.Collections.Generic.IEnumerable<TEntity>.GetEnumerator()
        {
            return _data.GetEnumerator();
        }

        System.Data.Entity.Infrastructure.IDbAsyncEnumerator<TEntity> System.Data.Entity.Infrastructure.IDbAsyncEnumerable<TEntity>.GetAsyncEnumerator()
        {
            return new FakeDbAsyncEnumerator<TEntity>(_data.GetEnumerator());
        }

        partial void InitializePartial();
    }

    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.30.0.0")]
    public class FakeDbAsyncQueryProvider<TEntity> : System.Data.Entity.Infrastructure.IDbAsyncQueryProvider
    {
        private readonly IQueryProvider _inner;

        public FakeDbAsyncQueryProvider(IQueryProvider inner)
        {
            _inner = inner;
        }

        public IQueryable CreateQuery(System.Linq.Expressions.Expression expression)
        {
            return new FakeDbAsyncEnumerable<TEntity>(expression);
        }

        public IQueryable<TElement> CreateQuery<TElement>(System.Linq.Expressions.Expression expression)
        {
            return new FakeDbAsyncEnumerable<TElement>(expression);
        }

        public object Execute(System.Linq.Expressions.Expression expression)
        {
            return _inner.Execute(expression);
        }

        public TResult Execute<TResult>(System.Linq.Expressions.Expression expression)
        {
            return _inner.Execute<TResult>(expression);
        }

        public System.Threading.Tasks.Task<object> ExecuteAsync(System.Linq.Expressions.Expression expression, System.Threading.CancellationToken cancellationToken)
        {
            return System.Threading.Tasks.Task.FromResult(Execute(expression));
        }

        public System.Threading.Tasks.Task<TResult> ExecuteAsync<TResult>(System.Linq.Expressions.Expression expression, System.Threading.CancellationToken cancellationToken)
        {
            return System.Threading.Tasks.Task.FromResult(Execute<TResult>(expression));
        }
    }

    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.30.0.0")]
    public class FakeDbAsyncEnumerable<T> : EnumerableQuery<T>, System.Data.Entity.Infrastructure.IDbAsyncEnumerable<T>, IQueryable<T>
    {
        public FakeDbAsyncEnumerable(System.Collections.Generic.IEnumerable<T> enumerable)
            : base(enumerable)
        { }

        public FakeDbAsyncEnumerable(System.Linq.Expressions.Expression expression)
            : base(expression)
        { }

        public System.Data.Entity.Infrastructure.IDbAsyncEnumerator<T> GetAsyncEnumerator()
        {
            return new FakeDbAsyncEnumerator<T>(this.AsEnumerable().GetEnumerator());
        }

        System.Data.Entity.Infrastructure.IDbAsyncEnumerator System.Data.Entity.Infrastructure.IDbAsyncEnumerable.GetAsyncEnumerator()
        {
            return GetAsyncEnumerator();
        }

        IQueryProvider IQueryable.Provider
        {
            get { return new FakeDbAsyncQueryProvider<T>(this); }
        }
    }

    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.30.0.0")]
    public class FakeDbAsyncEnumerator<T> : System.Data.Entity.Infrastructure.IDbAsyncEnumerator<T>
    {
        private readonly System.Collections.Generic.IEnumerator<T> _inner;

        public FakeDbAsyncEnumerator(System.Collections.Generic.IEnumerator<T> inner)
        {
            _inner = inner;
        }

        public void Dispose()
        {
            _inner.Dispose();
        }

        public System.Threading.Tasks.Task<bool> MoveNextAsync(System.Threading.CancellationToken cancellationToken)
        {
            return System.Threading.Tasks.Task.FromResult(_inner.MoveNext());
        }

        public T Current
        {
            get { return _inner.Current; }
        }

        object System.Data.Entity.Infrastructure.IDbAsyncEnumerator.Current
        {
            get { return Current; }
        }
    }

    #endregion

    #region POCO classes

    // Account
    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.30.0.0")]
    public partial class Account
    {
        public int ObjectId { get; set; } // ObjectID (Primary key)
        public string AccountName { get; set; } // AccountName (length: 255)
        public bool? IsManual { get; set; } // IsManual

        // Reverse navigation

        /// <summary>
        /// Child AccountHistories where [AccountHistory].[AccountID] point to this entity (FK__AccountHi__Accou__3A81B327)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<AccountHistory> AccountHistories { get; set; } // AccountHistory.FK__AccountHi__Accou__3A81B327
        /// <summary>
        /// Child AccountMappings where [AccountMapping].[AccountID] point to this entity (FK__AccountMa__Accou__3F466844)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<AccountMapping> AccountMappings { get; set; } // AccountMapping.FK__AccountMa__Accou__3F466844
        /// <summary>
        /// Child PreciousMetalsHistories where [PreciousMetalsHistory].[AccountID] point to this entity (FK__PreciousM__Accou__49C3F6B7)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<PreciousMetalsHistory> PreciousMetalsHistories { get; set; } // PreciousMetalsHistory.FK__PreciousM__Accou__49C3F6B7

        public Account()
        {
            AccountHistories = new System.Collections.Generic.List<AccountHistory>();
            AccountMappings = new System.Collections.Generic.List<AccountMapping>();
            PreciousMetalsHistories = new System.Collections.Generic.List<PreciousMetalsHistory>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

    // AccountHistory
    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.30.0.0")]
    public partial class AccountHistory
    {
        public int ObjectId { get; set; } // ObjectID (Primary key)
        public int? AccountId { get; set; } // AccountID
        public decimal? Amount { get; set; } // Amount
        public System.DateTime? AsOfDate { get; set; } // AsOfDate
        public int? RunId { get; set; } // RunID

        // Foreign keys

        /// <summary>
        /// Parent Account pointed by [AccountHistory].([AccountId]) (FK__AccountHi__Accou__3A81B327)
        /// </summary>
        public virtual Account Account { get; set; } // FK__AccountHi__Accou__3A81B327

        public AccountHistory()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

    // AccountMapping
    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.30.0.0")]
    public partial class AccountMapping
    {
        public int ObjectId { get; set; } // ObjectID (Primary key)
        public int? AccountId { get; set; } // AccountID
        public int? AccountTypeId { get; set; } // AccountTypeID

        // Foreign keys

        /// <summary>
        /// Parent Account pointed by [AccountMapping].([AccountId]) (FK__AccountMa__Accou__3F466844)
        /// </summary>
        public virtual Account Account { get; set; } // FK__AccountMa__Accou__3F466844
        /// <summary>
        /// Parent AccountType pointed by [AccountMapping].([AccountTypeId]) (FK__AccountMa__Accou__403A8C7D)
        /// </summary>
        public virtual AccountType AccountType { get; set; } // FK__AccountMa__Accou__403A8C7D

        public AccountMapping()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

    // AccountType
    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.30.0.0")]
    public partial class AccountType
    {
        public int ObjectId { get; set; } // ObjectID (Primary key)
        public string AccountTypeName { get; set; } // AccountTypeName (length: 255)
        public string AccountTypeDesc { get; set; } // AccountTypeDesc (length: 255)
        public bool? IsAsset { get; set; } // IsAsset

        // Reverse navigation

        /// <summary>
        /// Child AccountMappings where [AccountMapping].[AccountTypeID] point to this entity (FK__AccountMa__Accou__403A8C7D)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<AccountMapping> AccountMappings { get; set; } // AccountMapping.FK__AccountMa__Accou__403A8C7D

        public AccountType()
        {
            AccountMappings = new System.Collections.Generic.List<AccountMapping>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

    // NetWorthHistory
    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.30.0.0")]
    public partial class NetWorthHistory
    {
        public int ObjectId { get; set; } // ObjectID (Primary key)
        public decimal? NetWorthAmount { get; set; } // NetWorthAmount
        public decimal? AssetAmount { get; set; } // AssetAmount
        public decimal? DebtAmount { get; set; } // DebtAmount
        public System.DateTime? AsOfDate { get; set; } // AsOfDate
        public int? RunId { get; set; } // RunID

        public NetWorthHistory()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

    // PreciousMetalsHistory
    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.30.0.0")]
    public partial class PreciousMetalsHistory
    {
        public int ObjectId { get; set; } // ObjectID (Primary key)
        public int? AccountId { get; set; } // AccountID
        public decimal? GoldSpotPrice { get; set; } // GoldSpotPrice
        public decimal? SilverSpotPrice { get; set; } // SilverSpotPrice
        public decimal? PlatinumSpotPrice { get; set; } // PlatinumSpotPrice
        public decimal? PalladiumSpotPrice { get; set; } // PalladiumSpotPrice
        public int? GoldOunces { get; set; } // GoldOunces
        public int? SilverOunces { get; set; } // SilverOunces
        public int? PlatinumOunces { get; set; } // PlatinumOunces
        public int? PalladiumOunces { get; set; } // PalladiumOunces
        public System.DateTime? AsOfDate { get; set; } // AsOfDate
        public int? RunId { get; set; } // RunID

        // Foreign keys

        /// <summary>
        /// Parent Account pointed by [PreciousMetalsHistory].([AccountId]) (FK__PreciousM__Accou__49C3F6B7)
        /// </summary>
        public virtual Account Account { get; set; } // FK__PreciousM__Accou__49C3F6B7

        public PreciousMetalsHistory()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

    // User
    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.30.0.0")]
    public partial class User
    {
        public int ObjectId { get; set; } // ObjectID (Primary key)
        public string UserName { get; set; } // UserName (length: 255)
        public System.DateTime? LastUsedDate { get; set; } // LastUsedDate

        public User()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

    #endregion

    #region POCO Configuration

    // Account
    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.30.0.0")]
    public partial class AccountConfiguration : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<Account>
    {
        public AccountConfiguration()
            : this("dbo")
        {
        }

        public AccountConfiguration(string schema)
        {
            ToTable("Account", schema);
            HasKey(x => x.ObjectId);

            Property(x => x.ObjectId).HasColumnName(@"ObjectID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            Property(x => x.AccountName).HasColumnName(@"AccountName").HasColumnType("varchar").IsRequired().IsUnicode(false).HasMaxLength(255);
            Property(x => x.IsManual).HasColumnName(@"IsManual").HasColumnType("bit").IsOptional();
            InitializePartial();
        }
        partial void InitializePartial();
    }

    // AccountHistory
    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.30.0.0")]
    public partial class AccountHistoryConfiguration : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<AccountHistory>
    {
        public AccountHistoryConfiguration()
            : this("dbo")
        {
        }

        public AccountHistoryConfiguration(string schema)
        {
            ToTable("AccountHistory", schema);
            HasKey(x => x.ObjectId);

            Property(x => x.ObjectId).HasColumnName(@"ObjectID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            Property(x => x.AccountId).HasColumnName(@"AccountID").HasColumnType("int").IsOptional();
            Property(x => x.Amount).HasColumnName(@"Amount").HasColumnType("money").IsOptional().HasPrecision(19,4);
            Property(x => x.AsOfDate).HasColumnName(@"AsOfDate").HasColumnType("datetime").IsOptional();
            Property(x => x.RunId).HasColumnName(@"RunID").HasColumnType("int").IsOptional();

            // Foreign keys
            HasOptional(a => a.Account).WithMany(b => b.AccountHistories).HasForeignKey(c => c.AccountId).WillCascadeOnDelete(false); // FK__AccountHi__Accou__3A81B327
            InitializePartial();
        }
        partial void InitializePartial();
    }

    // AccountMapping
    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.30.0.0")]
    public partial class AccountMappingConfiguration : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<AccountMapping>
    {
        public AccountMappingConfiguration()
            : this("dbo")
        {
        }

        public AccountMappingConfiguration(string schema)
        {
            ToTable("AccountMapping", schema);
            HasKey(x => x.ObjectId);

            Property(x => x.ObjectId).HasColumnName(@"ObjectID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            Property(x => x.AccountId).HasColumnName(@"AccountID").HasColumnType("int").IsOptional();
            Property(x => x.AccountTypeId).HasColumnName(@"AccountTypeID").HasColumnType("int").IsOptional();

            // Foreign keys
            HasOptional(a => a.Account).WithMany(b => b.AccountMappings).HasForeignKey(c => c.AccountId).WillCascadeOnDelete(false); // FK__AccountMa__Accou__3F466844
            HasOptional(a => a.AccountType).WithMany(b => b.AccountMappings).HasForeignKey(c => c.AccountTypeId).WillCascadeOnDelete(false); // FK__AccountMa__Accou__403A8C7D
            InitializePartial();
        }
        partial void InitializePartial();
    }

    // AccountType
    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.30.0.0")]
    public partial class AccountTypeConfiguration : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<AccountType>
    {
        public AccountTypeConfiguration()
            : this("dbo")
        {
        }

        public AccountTypeConfiguration(string schema)
        {
            ToTable("AccountType", schema);
            HasKey(x => x.ObjectId);

            Property(x => x.ObjectId).HasColumnName(@"ObjectID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None);
            Property(x => x.AccountTypeName).HasColumnName(@"AccountTypeName").HasColumnType("varchar").IsRequired().IsUnicode(false).HasMaxLength(255);
            Property(x => x.AccountTypeDesc).HasColumnName(@"AccountTypeDesc").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(255);
            Property(x => x.IsAsset).HasColumnName(@"IsAsset").HasColumnType("bit").IsOptional();
            InitializePartial();
        }
        partial void InitializePartial();
    }

    // NetWorthHistory
    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.30.0.0")]
    public partial class NetWorthHistoryConfiguration : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<NetWorthHistory>
    {
        public NetWorthHistoryConfiguration()
            : this("dbo")
        {
        }

        public NetWorthHistoryConfiguration(string schema)
        {
            ToTable("NetWorthHistory", schema);
            HasKey(x => x.ObjectId);

            Property(x => x.ObjectId).HasColumnName(@"ObjectID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            Property(x => x.NetWorthAmount).HasColumnName(@"NetWorthAmount").HasColumnType("money").IsOptional().HasPrecision(19,4);
            Property(x => x.AssetAmount).HasColumnName(@"AssetAmount").HasColumnType("money").IsOptional().HasPrecision(19,4);
            Property(x => x.DebtAmount).HasColumnName(@"DebtAmount").HasColumnType("money").IsOptional().HasPrecision(19,4);
            Property(x => x.AsOfDate).HasColumnName(@"AsOfDate").HasColumnType("datetime").IsOptional();
            Property(x => x.RunId).HasColumnName(@"RunID").HasColumnType("int").IsOptional();
            InitializePartial();
        }
        partial void InitializePartial();
    }

    // PreciousMetalsHistory
    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.30.0.0")]
    public partial class PreciousMetalsHistoryConfiguration : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<PreciousMetalsHistory>
    {
        public PreciousMetalsHistoryConfiguration()
            : this("dbo")
        {
        }

        public PreciousMetalsHistoryConfiguration(string schema)
        {
            ToTable("PreciousMetalsHistory", schema);
            HasKey(x => x.ObjectId);

            Property(x => x.ObjectId).HasColumnName(@"ObjectID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            Property(x => x.AccountId).HasColumnName(@"AccountID").HasColumnType("int").IsOptional();
            Property(x => x.GoldSpotPrice).HasColumnName(@"GoldSpotPrice").HasColumnType("money").IsOptional().HasPrecision(19,4);
            Property(x => x.SilverSpotPrice).HasColumnName(@"SilverSpotPrice").HasColumnType("money").IsOptional().HasPrecision(19,4);
            Property(x => x.PlatinumSpotPrice).HasColumnName(@"PlatinumSpotPrice").HasColumnType("money").IsOptional().HasPrecision(19,4);
            Property(x => x.PalladiumSpotPrice).HasColumnName(@"PalladiumSpotPrice").HasColumnType("money").IsOptional().HasPrecision(19,4);
            Property(x => x.GoldOunces).HasColumnName(@"GoldOunces").HasColumnType("int").IsOptional();
            Property(x => x.SilverOunces).HasColumnName(@"SilverOunces").HasColumnType("int").IsOptional();
            Property(x => x.PlatinumOunces).HasColumnName(@"PlatinumOunces").HasColumnType("int").IsOptional();
            Property(x => x.PalladiumOunces).HasColumnName(@"PalladiumOunces").HasColumnType("int").IsOptional();
            Property(x => x.AsOfDate).HasColumnName(@"AsOfDate").HasColumnType("datetime").IsOptional();
            Property(x => x.RunId).HasColumnName(@"RunID").HasColumnType("int").IsOptional();

            // Foreign keys
            HasOptional(a => a.Account).WithMany(b => b.PreciousMetalsHistories).HasForeignKey(c => c.AccountId).WillCascadeOnDelete(false); // FK__PreciousM__Accou__49C3F6B7
            InitializePartial();
        }
        partial void InitializePartial();
    }

    // User
    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.30.0.0")]
    public partial class UserConfiguration : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<User>
    {
        public UserConfiguration()
            : this("dbo")
        {
        }

        public UserConfiguration(string schema)
        {
            ToTable("User", schema);
            HasKey(x => x.ObjectId);

            Property(x => x.ObjectId).HasColumnName(@"ObjectID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            Property(x => x.UserName).HasColumnName(@"UserName").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(255);
            Property(x => x.LastUsedDate).HasColumnName(@"LastUsedDate").HasColumnType("datetime").IsOptional();
            InitializePartial();
        }
        partial void InitializePartial();
    }

    #endregion

}
// </auto-generated>


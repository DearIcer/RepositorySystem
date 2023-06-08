using System;
using System.Data.Entity;
using System.Linq;
using System.Xml;

namespace Models
{
    public class RepositorySystemContext : DbContext
    {
        //您的上下文已配置为从您的应用程序的配置文件(App.config 或 Web.config)
        //使用“RepositorySystemContext”连接字符串。默认情况下，此连接字符串针对您的 LocalDb 实例上的
        //“Models.RepositorySystemContext”数据库。
        // 
        //如果您想要针对其他数据库和/或数据库提供程序，请在应用程序配置文件中修改“RepositorySystemContext”
        //连接字符串。
        public RepositorySystemContext()
            : base("name=RepositorySystemContext")
        {
        }

        //为您要在模型中包含的每种实体类型都添加 DbSet。有关配置和使用 Code First  模型
        //的详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=390109。
        public virtual DbSet<UserInfo> UserInfo { get; set; }
        public virtual DbSet<RoleInfo> RoleInfo { get; set; }
        public virtual DbSet<R_UserInfo_RoleInfo> R_UserInfo_RoleInfo { get; set; }
        public virtual DbSet<DepartmentInfo> DepartmentInfo { get; set; }
        public virtual DbSet<MenuInfo> MenuInfo { get; set; }
        public virtual DbSet<R_RoleInfo_MenuInfo> R_RoleInfo_MenuInfo { get; set; }
        public virtual DbSet<ConsumableInfo> ConsumableInfo { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<ConsumableRecord> ConsumableRecord { get; set; }
        public virtual DbSet<WorkFlow_Instance> WorkFlow_Instance { get; set; }
        public virtual DbSet<WorkFlow_InstanceStep> WorkFlow_InstanceStep { get; set; }
        public virtual DbSet<WorkFlow_Model> WorkFlow_Model { get; set; }
        public virtual DbSet<FileInfo> FileInfo { get; set; }

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}
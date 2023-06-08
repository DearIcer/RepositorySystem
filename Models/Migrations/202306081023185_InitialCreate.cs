namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 36),
                        CategoryName = c.String(maxLength: 16),
                        Description = c.String(maxLength: 32),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ConsumableInfoes",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 36),
                        Description = c.String(maxLength: 32),
                        CategoryId = c.String(maxLength: 36),
                        ConsumableName = c.String(maxLength: 16),
                        Specification = c.String(maxLength: 32),
                        Num = c.Int(nullable: false),
                        Unit = c.String(maxLength: 8),
                        Money = c.Decimal(nullable: false, precision: 18, scale: 2),
                        WarningNum = c.Int(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                        DeleteTime = c.DateTime(),
                        CreatedTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ConsumableRecords",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 36),
                        ConsumableId = c.String(maxLength: 36),
                        Num = c.Int(nullable: false),
                        Type = c.Int(nullable: false),
                        Creator = c.String(maxLength: 36),
                        CreatedTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DepartmentInfoes",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 36),
                        Description = c.String(maxLength: 32),
                        DepartmentName = c.String(maxLength: 16),
                        LeaderId = c.String(maxLength: 36),
                        ParentId = c.String(maxLength: 36),
                        IsDelete = c.Boolean(nullable: false),
                        DeleteTime = c.DateTime(),
                        CreatedTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.FileInfoes",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 36),
                        RelationId = c.String(maxLength: 36),
                        OldFileName = c.String(maxLength: 32),
                        NewFileName = c.String(maxLength: 32),
                        Extension = c.String(maxLength: 12),
                        Length = c.Long(nullable: false),
                        Creator = c.String(maxLength: 36),
                        Category = c.Int(nullable: false),
                        CreatedTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MenuInfoes",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 36),
                        Title = c.String(maxLength: 16),
                        Description = c.String(maxLength: 32),
                        Level = c.Int(nullable: false),
                        Sort = c.Int(nullable: false),
                        Href = c.String(maxLength: 128),
                        ParentId = c.String(maxLength: 36),
                        Icon = c.String(maxLength: 32),
                        Target = c.String(maxLength: 16),
                        IsDelete = c.Boolean(nullable: false),
                        DeleteTime = c.DateTime(),
                        CreatedTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.R_RoleInfo_MenuInfo",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 36),
                        RoleId = c.String(maxLength: 36),
                        MenuId = c.String(maxLength: 36),
                        CreatedTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.R_UserInfo_RoleInfo",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 36),
                        UserId = c.String(maxLength: 36),
                        RoleId = c.String(maxLength: 36),
                        CreatedTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RoleInfoes",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 36),
                        RoleName = c.String(maxLength: 16),
                        Description = c.String(maxLength: 32),
                        IsDelete = c.Boolean(nullable: false),
                        DeleteTime = c.DateTime(),
                        CreatedTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserInfoes",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 36),
                        Account = c.String(maxLength: 16),
                        UserName = c.String(maxLength: 16),
                        PhoneNum = c.String(maxLength: 16),
                        Email = c.String(maxLength: 32),
                        DepartmentId = c.String(maxLength: 36),
                        Sex = c.Int(nullable: false),
                        PassWord = c.String(maxLength: 32),
                        IsAdmin = c.Boolean(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                        DeleteTime = c.DateTime(),
                        CreatedTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.WorkFlow_Instance",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 36),
                        ModelId = c.String(maxLength: 36),
                        Status = c.Int(nullable: false),
                        Description = c.String(maxLength: 64),
                        Reason = c.String(maxLength: 64),
                        Creator = c.String(maxLength: 36),
                        OutNum = c.Int(nullable: false),
                        CreOutGoodsIdator = c.String(maxLength: 36),
                        CreatedTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.WorkFlow_InstanceStep",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 36),
                        InstanceId = c.String(maxLength: 36),
                        ReviewerId = c.String(maxLength: 36),
                        ReviewReason = c.String(maxLength: 64),
                        ReviewStatus = c.Int(nullable: false),
                        ReviewTime = c.DateTime(nullable: false),
                        BeforeStepId = c.String(maxLength: 36),
                        CreatedTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.WorkFlow_Model",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 36),
                        Title = c.String(maxLength: 32),
                        Description = c.String(maxLength: 64),
                        IsDelete = c.Boolean(nullable: false),
                        DeleteTime = c.DateTime(),
                        CreatedTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.WorkFlow_Model");
            DropTable("dbo.WorkFlow_InstanceStep");
            DropTable("dbo.WorkFlow_Instance");
            DropTable("dbo.UserInfoes");
            DropTable("dbo.RoleInfoes");
            DropTable("dbo.R_UserInfo_RoleInfo");
            DropTable("dbo.R_RoleInfo_MenuInfo");
            DropTable("dbo.MenuInfoes");
            DropTable("dbo.FileInfoes");
            DropTable("dbo.DepartmentInfoes");
            DropTable("dbo.ConsumableRecords");
            DropTable("dbo.ConsumableInfoes");
            DropTable("dbo.Categories");
        }
    }
}

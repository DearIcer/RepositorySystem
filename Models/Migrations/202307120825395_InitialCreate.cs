namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Category", "IsDelete", c => c.Boolean(nullable: false));
            AddColumn("dbo.Category", "DeleteTime", c => c.DateTime());
            AddColumn("dbo.Category", "CreatedTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Category", "CreatedTime");
            DropColumn("dbo.Category", "DeleteTime");
            DropColumn("dbo.Category", "IsDelete");
        }
    }
}

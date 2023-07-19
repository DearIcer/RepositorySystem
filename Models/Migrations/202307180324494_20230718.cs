namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20230718 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.WorkFlow_InstanceStep", "ReviewTime", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.WorkFlow_InstanceStep", "ReviewTime", c => c.DateTime(nullable: false));
        }
    }
}

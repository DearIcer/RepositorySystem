namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateOutGoodsId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.WorkFlow_Instance", "OutGoodsId", c => c.String(maxLength: 36));
            DropColumn("dbo.WorkFlow_Instance", "CreOutGoodsIdator");
        }
        
        public override void Down()
        {
            AddColumn("dbo.WorkFlow_Instance", "CreOutGoodsIdator", c => c.String(maxLength: 36));
            DropColumn("dbo.WorkFlow_Instance", "OutGoodsId");
        }
    }
}

namespace efTest.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dd : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Remark", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "Remark");
        }
    }
}

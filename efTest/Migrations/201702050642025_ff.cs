namespace efTest.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ff : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Colors",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        ColorDes = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Products", "ColorID", c => c.Long(nullable: false));
            CreateIndex("dbo.Products", "ColorID");
            AddForeignKey("dbo.Products", "ColorID", "dbo.Colors", "Id", cascadeDelete: true);
            DropColumn("dbo.Products", "Color");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "Color", c => c.String());
            DropForeignKey("dbo.Products", "ColorID", "dbo.Colors");
            DropIndex("dbo.Products", new[] { "ColorID" });
            DropColumn("dbo.Products", "ColorID");
            DropTable("dbo.Colors");
        }
    }
}

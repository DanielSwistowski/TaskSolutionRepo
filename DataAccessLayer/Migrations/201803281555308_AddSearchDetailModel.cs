namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSearchDetailModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SearchDetail",
                c => new
                    {
                        SearchDetailId = c.Int(nullable: false, identity: true),
                        Number = c.String(),
                        HeaderValues = c.String(),
                        NumberType = c.Int(nullable: false),
                        ComanyId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SearchDetailId)
                .ForeignKey("dbo.Company", t => t.ComanyId, cascadeDelete: true)
                .Index(t => t.ComanyId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SearchDetail", "ComanyId", "dbo.Company");
            DropIndex("dbo.SearchDetail", new[] { "ComanyId" });
            DropTable("dbo.SearchDetail");
        }
    }
}

namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Address",
                c => new
                    {
                        CompanyId = c.Int(nullable: false),
                        City = c.String(),
                        Street = c.String(),
                        HouseNumber = c.String(),
                        ZipCode = c.String(),
                    })
                .PrimaryKey(t => t.CompanyId)
                .ForeignKey("dbo.Company", t => t.CompanyId, cascadeDelete: true)
                .Index(t => t.CompanyId);
            
            CreateTable(
                "dbo.Company",
                c => new
                    {
                        ComanyId = c.Int(nullable: false, identity: true),
                        NIP = c.String(),
                        REGON = c.String(),
                        KRS = c.String(),
                        CompanyName = c.String(),
                    })
                .PrimaryKey(t => t.ComanyId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Address", "CompanyId", "dbo.Company");
            DropIndex("dbo.Address", new[] { "CompanyId" });
            DropTable("dbo.Company");
            DropTable("dbo.Address");
        }
    }
}

namespace kt.api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class zoolander : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        CountNOK = c.Int(nullable: false),
                        CountSOSO = c.Int(nullable: false),
                        CountOK = c.Int(nullable: false),
                        Deck_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Decks", t => t.Deck_Id)
                .Index(t => t.Deck_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tests", "Deck_Id", "dbo.Decks");
            DropIndex("dbo.Tests", new[] { "Deck_Id" });
            DropTable("dbo.Tests");
        }
    }
}

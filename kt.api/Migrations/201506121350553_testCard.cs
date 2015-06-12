namespace kt.api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class testCard : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TestCards",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Card_Id = c.String(maxLength: 128),
                        Test_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cards", t => t.Card_Id)
                .ForeignKey("dbo.Tests", t => t.Test_Id)
                .Index(t => t.Card_Id)
                .Index(t => t.Test_Id);
            
            DropColumn("dbo.Tests", "CountNOK");
            DropColumn("dbo.Tests", "CountSOSO");
            DropColumn("dbo.Tests", "CountOK");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tests", "CountOK", c => c.Int(nullable: false));
            AddColumn("dbo.Tests", "CountSOSO", c => c.Int(nullable: false));
            AddColumn("dbo.Tests", "CountNOK", c => c.Int(nullable: false));
            DropForeignKey("dbo.TestCards", "Test_Id", "dbo.Tests");
            DropForeignKey("dbo.TestCards", "Card_Id", "dbo.Cards");
            DropIndex("dbo.TestCards", new[] { "Test_Id" });
            DropIndex("dbo.TestCards", new[] { "Card_Id" });
            DropTable("dbo.TestCards");
        }
    }
}

namespace kt.api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class z : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TestCards", "Test_Id", "dbo.Tests");
            DropIndex("dbo.TestCards", new[] { "Test_Id" });
            RenameColumn(table: "dbo.TestCards", name: "Test_Id", newName: "TestId");
            AlterColumn("dbo.TestCards", "TestId", c => c.Int(nullable: false));
            CreateIndex("dbo.TestCards", "TestId");
            AddForeignKey("dbo.TestCards", "TestId", "dbo.Tests", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TestCards", "TestId", "dbo.Tests");
            DropIndex("dbo.TestCards", new[] { "TestId" });
            AlterColumn("dbo.TestCards", "TestId", c => c.Int());
            RenameColumn(table: "dbo.TestCards", name: "TestId", newName: "Test_Id");
            CreateIndex("dbo.TestCards", "Test_Id");
            AddForeignKey("dbo.TestCards", "Test_Id", "dbo.Tests", "Id");
        }
    }
}

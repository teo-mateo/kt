namespace kt.api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class card_lastshown : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cards", "LastShown", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Cards", "LastShown");
        }
    }
}

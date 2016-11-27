namespace CreativeSite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Chapters", "Tags", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Chapters", "Tags");
        }
    }
}

namespace MEPAC.Reportsitory.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateHiring : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Hiring", "IsActive", c => c.Boolean());
            AddColumn("dbo.Hiring", "IsShow", c => c.Boolean());
            AddColumn("dbo.Hiring", "LinkImage", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Hiring", "IsActive");
            DropColumn("dbo.Hiring", "IsShow");
            DropColumn("dbo.Hiring", "LinkImage");
        }
    }
}

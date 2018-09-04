namespace MEPAC.Reportsitory.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateCSDL01 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Hiring", "PostDate", c => c.DateTime());
            AddColumn("dbo.Hiring", "PostBy", c => c.String());
            AddColumn("dbo.News", "PostDate", c => c.DateTime());
            AddColumn("dbo.News", "PostBy", c => c.String());
            AddColumn("dbo.Projects", "PostDate", c => c.DateTime());
            AddColumn("dbo.Projects", "PostBy", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Projects", "PostBy");
            DropColumn("dbo.Projects", "PostDate");
            DropColumn("dbo.News", "PostBy");
            DropColumn("dbo.News", "PostDate");
            DropColumn("dbo.Hiring", "PostBy");
            DropColumn("dbo.Hiring", "PostDate");
        }
    }
}

namespace SMS.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMenu : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Menu",
                c => new
                    {
                        MenuID = c.Int(nullable: false, identity: true),
                        MenuName = c.String(nullable: false, maxLength: 255),
                        Url = c.String(maxLength: 255, unicode: false),
                        Icon = c.String(),
                        OrderNumber = c.Int(nullable: false),
                        IsDisplay = c.Boolean(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.MenuID);
            
            CreateTable(
                "dbo.MenuPermission",
                c => new
                    {
                        MenuPermissionID = c.Int(nullable: false, identity: true),
                        UserID = c.String(nullable: false),
                        MenuID = c.Int(nullable: false),
                        SubMenuID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MenuPermissionID);
            
            CreateTable(
                "dbo.MenuPermissionDetail",
                c => new
                    {
                        MenuPermissionDetailID = c.Int(nullable: false, identity: true),
                        MenuPermissionID = c.String(nullable: false),
                        IsView = c.Boolean(nullable: false),
                        IsAdd = c.Boolean(nullable: false),
                        IsEdit = c.Boolean(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.MenuPermissionDetailID);
            
            CreateTable(
                "dbo.SubMenu",
                c => new
                    {
                        SubMenuID = c.Int(nullable: false, identity: true),
                        SubMenuName = c.String(nullable: false, maxLength: 255),
                        Url = c.String(maxLength: 255, unicode: false),
                        MenuID = c.Int(nullable: false),
                        Icon = c.String(),
                        OrderNumber = c.Int(nullable: false),
                        IsDisplay = c.Boolean(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.SubMenuID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SubMenu");
            DropTable("dbo.MenuPermissionDetail");
            DropTable("dbo.MenuPermission");
            DropTable("dbo.Menu");
        }
    }
}

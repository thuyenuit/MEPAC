namespace MEPAC.Reportsitory.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreateDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Contact",
                c => new
                    {
                        ContactID = c.Int(nullable: false, identity: true),
                        FullName = c.String(maxLength: 100),
                        Email = c.String(maxLength: 100, unicode: false),
                        Content = c.String(maxLength: 1000),
                        SendDate = c.DateTime(nullable: false),
                        IsReading = c.Boolean(nullable: false),
                        ReaderBy = c.String(),
                        ReadDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ContactID);
            
            CreateTable(
                "dbo.Hiring",
                c => new
                    {
                        HiringID = c.Int(nullable: false, identity: true),
                        Position = c.String(maxLength: 200),
                        StartDate = c.DateTime(nullable: false),
                        EnddDate = c.DateTime(),
                        Content = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                        CreateBy = c.String(maxLength: 255),
                        UpdateDate = c.DateTime(),
                        UpdateBy = c.String(maxLength: 255),
                        MetaKeyword = c.String(maxLength: 500),
                        MetaDescription = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.HiringID);
            
            CreateTable(
                "dbo.MetaImage",
                c => new
                    {
                        MetaImageID = c.Int(nullable: false, identity: true),
                        ParentID = c.Int(nullable: false),
                        Link = c.String(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.MetaImageID);
            
            CreateTable(
                "dbo.Information",
                c => new
                    {
                        InformationID = c.Int(nullable: false, identity: true),
                        Display = c.String(maxLength: 500),
                        ShortDisplay = c.String(maxLength: 200),
                        Address1 = c.String(maxLength: 500),
                        Address2 = c.String(maxLength: 500),
                        Address3 = c.String(maxLength: 500),
                        Address4 = c.String(maxLength: 500),
                        Address5 = c.String(maxLength: 500),
                        Phone = c.String(maxLength: 100),
                        Website = c.String(maxLength: 100),
                        Email = c.String(maxLength: 100, unicode: false),
                        Content = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                        CreateBy = c.String(maxLength: 255),
                        UpdateDate = c.DateTime(),
                        UpdateBy = c.String(maxLength: 255),
                        MetaKeyword = c.String(maxLength: 500),
                        MetaDescription = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.InformationID);
            
            CreateTable(
                "dbo.Menu",
                c => new
                    {
                        MenuID = c.Int(nullable: false, identity: true),
                        Display = c.String(maxLength: 100),
                        IsActive = c.Boolean(nullable: false),
                        Order = c.Int(nullable: false),
                        Router = c.String(),
                        IsSubMenu = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MenuID);
            
            CreateTable(
                "dbo.SubMenu",
                c => new
                    {
                        SubMenuID = c.Int(nullable: false, identity: true),
                        MenuID = c.Int(nullable: false),
                        Display = c.String(maxLength: 100),
                        Order = c.Int(nullable: false),
                        Router = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        Image = c.String(),
                    })
                .PrimaryKey(t => t.SubMenuID)
                .ForeignKey("dbo.Menu", t => t.MenuID, cascadeDelete: true)
                .Index(t => t.MenuID);
            
            CreateTable(
                "dbo.News",
                c => new
                    {
                        NewsID = c.Int(nullable: false, identity: true),
                        DisplayName = c.String(maxLength: 500),
                        SubMenu = c.Int(nullable: false),
                        Content = c.String(),
                        ShortContent = c.String(maxLength: 500),
                        IsActive = c.Boolean(nullable: false),
                        Image = c.String(),
                        SubMenuID = c.Int(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        CreateBy = c.String(maxLength: 255),
                        UpdateDate = c.DateTime(),
                        UpdateBy = c.String(maxLength: 255),
                        MetaKeyword = c.String(maxLength: 500),
                        MetaDescription = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.NewsID)
                .ForeignKey("dbo.SubMenu", t => t.SubMenuID, cascadeDelete: true)
                .Index(t => t.SubMenuID);
            
            CreateTable(
                "dbo.Partner",
                c => new
                    {
                        PartID = c.Int(nullable: false, identity: true),
                        Display = c.String(maxLength: 200),
                        Logo = c.String(),
                        Order = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        Website = c.String(maxLength: 100),
                        Address = c.String(maxLength: 200),
                        Email = c.String(maxLength: 100, unicode: false),
                        Phone = c.String(maxLength: 100, unicode: false),
                        CreateDate = c.DateTime(nullable: false),
                        CreateBy = c.String(maxLength: 255),
                        UpdateDate = c.DateTime(),
                        UpdateBy = c.String(maxLength: 255),
                        MetaKeyword = c.String(maxLength: 500),
                        MetaDescription = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.PartID);
            
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        ProjectID = c.Int(nullable: false, identity: true),
                        Display = c.String(maxLength: 200),
                        FromDate = c.DateTime(nullable: false),
                        ToDate = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsShow = c.Boolean(nullable: false),
                        Description = c.String(),
                        Image = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                        CreateBy = c.String(maxLength: 255),
                        UpdateDate = c.DateTime(),
                        UpdateBy = c.String(maxLength: 255),
                        MetaKeyword = c.String(maxLength: 500),
                        MetaDescription = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.ProjectID);
            
            CreateTable(
                "dbo.Range",
                c => new
                    {
                        RangeID = c.Int(nullable: false, identity: true),
                        SubMenuID = c.Int(nullable: false),
                        Cotntent = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                        CreateBy = c.String(maxLength: 255),
                        UpdateDate = c.DateTime(),
                        UpdateBy = c.String(maxLength: 255),
                        MetaKeyword = c.String(maxLength: 500),
                        MetaDescription = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.RangeID)
                .ForeignKey("dbo.SubMenu", t => t.SubMenuID, cascadeDelete: true)
                .Index(t => t.SubMenuID);
            
            CreateTable(
                "dbo.Slide",
                c => new
                    {
                        SlideID = c.Int(nullable: false, identity: true),
                        Title = c.String(maxLength: 50),
                        Content = c.String(maxLength: 200),
                        IsActive = c.Boolean(nullable: false),
                        Image = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                        CreateBy = c.String(maxLength: 255),
                        UpdateDate = c.DateTime(),
                        UpdateBy = c.String(maxLength: 255),
                        MetaKeyword = c.String(maxLength: 500),
                        MetaDescription = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.SlideID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Range", "SubMenuID", "dbo.SubMenu");
            DropForeignKey("dbo.News", "SubMenuID", "dbo.SubMenu");
            DropForeignKey("dbo.SubMenu", "MenuID", "dbo.Menu");
            DropIndex("dbo.Range", new[] { "SubMenuID" });
            DropIndex("dbo.News", new[] { "SubMenuID" });
            DropIndex("dbo.SubMenu", new[] { "MenuID" });
            DropTable("dbo.Slide");
            DropTable("dbo.Range");
            DropTable("dbo.Projects");
            DropTable("dbo.Partner");
            DropTable("dbo.News");
            DropTable("dbo.SubMenu");
            DropTable("dbo.Menu");
            DropTable("dbo.Information");
            DropTable("dbo.MetaImage");
            DropTable("dbo.Hiring");
            DropTable("dbo.Contact");
        }
    }
}

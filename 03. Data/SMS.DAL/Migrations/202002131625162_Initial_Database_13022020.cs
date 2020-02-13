namespace SMS.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial_Database_13022020 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                        Alias = c.String(nullable: false, maxLength: 255, unicode: false),
                        Sequence = c.Int(nullable: false),
                        IsHomeFlag = c.Boolean(nullable: false),
                        MetaKeyword = c.String(maxLength: 500),
                        MetaDescription = c.String(maxLength: 500),
                        CreateDate = c.DateTime(nullable: false),
                        CreateBy = c.String(maxLength: 255),
                        ModifiedDate = c.DateTime(),
                        UpdateBy = c.String(maxLength: 255),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.CategoryID);
            
            CreateTable(
                "dbo.Colors",
                c => new
                    {
                        ColorID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        HexCode = c.String(maxLength: 50),
                        CreateDate = c.DateTime(nullable: false),
                        CreateBy = c.String(maxLength: 255),
                        ModifiedDate = c.DateTime(),
                        UpdateBy = c.String(maxLength: 255),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ColorID);
            
            CreateTable(
                "dbo.ColorProducts",
                c => new
                    {
                        ColorProductID = c.Int(nullable: false, identity: true),
                        ColorID = c.Int(nullable: false),
                        ProductID = c.Long(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        CreateBy = c.String(maxLength: 255),
                        ModifiedDate = c.DateTime(),
                        UpdateBy = c.String(maxLength: 255),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ColorProductID)
                .ForeignKey("dbo.Colors", t => t.ColorID, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductID, cascadeDelete: true)
                .Index(t => t.ColorID)
                .Index(t => t.ProductID);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductID = c.Long(nullable: false, identity: true),
                        ProductCategoryID = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 255),
                        Code = c.String(nullable: false, maxLength: 255),
                        Alias = c.String(nullable: false, maxLength: 255, unicode: false),
                        Image = c.Binary(),
                        MoreImage = c.String(storeType: "xml"),
                        PriceInput = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PriceOutput = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Content = c.String(unicode: false, storeType: "text"),
                        Sequence = c.Int(nullable: false),
                        IsHomeFlag = c.Boolean(nullable: false),
                        IsHotFlag = c.Boolean(nullable: false),
                        Status = c.Int(nullable: false),
                        Tags = c.String(maxLength: 500),
                        MetaKeyword = c.String(maxLength: 500),
                        MetaDescription = c.String(maxLength: 500),
                        CreateDate = c.DateTime(nullable: false),
                        CreateBy = c.String(maxLength: 255),
                        ModifiedDate = c.DateTime(),
                        UpdateBy = c.String(maxLength: 255),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ProductID)
                .ForeignKey("dbo.ProductCategories", t => t.ProductCategoryID, cascadeDelete: true)
                .Index(t => t.ProductCategoryID);
            
            CreateTable(
                "dbo.ProductCategories",
                c => new
                    {
                        ProductCategoryID = c.Int(nullable: false, identity: true),
                        CategoryID = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 255),
                        Alias = c.String(nullable: false, maxLength: 255, unicode: false),
                        Icon = c.Binary(),
                        Sequence = c.Int(nullable: false),
                        IsHomeFlag = c.Boolean(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        CreateBy = c.String(maxLength: 255),
                        ModifiedDate = c.DateTime(),
                        UpdateBy = c.String(maxLength: 255),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ProductCategoryID)
                .ForeignKey("dbo.Categories", t => t.CategoryID, cascadeDelete: true)
                .Index(t => t.CategoryID);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        CustomerID = c.Long(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 100),
                        CellPhone = c.String(nullable: false, maxLength: 15, unicode: false),
                        Address = c.String(maxLength: 255),
                        Email = c.String(maxLength: 100, unicode: false),
                        Identity = c.String(maxLength: 15, unicode: false),
                        CreateDate = c.DateTime(nullable: false),
                        CreateBy = c.String(maxLength: 255),
                        ModifiedDate = c.DateTime(),
                        UpdateBy = c.String(maxLength: 255),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.CustomerID);
            
            CreateTable(
                "dbo.ErrorLogs",
                c => new
                    {
                        LogID = c.Guid(nullable: false, identity: true),
                        Message = c.String(nullable: false),
                        DateLog = c.DateTime(nullable: false),
                        StackTrace = c.String(),
                        Method = c.String(maxLength: 255),
                        Table = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.LogID);
            
            CreateTable(
                "dbo.Fields",
                c => new
                    {
                        FieldID = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                        FieldGroupID = c.Int(nullable: false),
                        FieldType = c.Int(nullable: false),
                        Sequence = c.Int(nullable: false),
                        Setting = c.String(),
                    })
                .PrimaryKey(t => t.FieldID)
                .ForeignKey("dbo.FieldGroups", t => t.FieldGroupID, cascadeDelete: true)
                .Index(t => t.FieldGroupID);
            
            CreateTable(
                "dbo.FieldGroups",
                c => new
                    {
                        FieldGroupID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                        CategoryID = c.Int(nullable: false),
                        Sequence = c.Int(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.FieldGroupID)
                .ForeignKey("dbo.Categories", t => t.CategoryID, cascadeDelete: true)
                .Index(t => t.CategoryID);
            
            CreateTable(
                "dbo.FieldProduct",
                c => new
                    {
                        FieldProductID = c.Long(nullable: false, identity: true),
                        ProductID = c.Long(nullable: false),
                        FieldID = c.Long(nullable: false),
                        Value = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.FieldProductID)
                .ForeignKey("dbo.Fields", t => t.FieldID, cascadeDelete: true)
                .Index(t => t.FieldID);
            
            CreateTable(
                "dbo.InventoryDeliveryVouchers",
                c => new
                    {
                        InventoryDeliveryVoucherID = c.Long(nullable: false, identity: true),
                        OrderCode = c.String(nullable: false, maxLength: 255, unicode: false),
                        CustomerID = c.Long(nullable: false),
                        CustomerName = c.String(maxLength: 255),
                        Address = c.String(maxLength: 255),
                        Phone = c.String(maxLength: 15, unicode: false),
                        OrderType = c.Int(nullable: false),
                        TotalQuantity = c.Int(nullable: false),
                        TotalAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Payment = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        Description = c.String(maxLength: 255),
                        CreateDate = c.DateTime(nullable: false),
                        CreateBy = c.String(maxLength: 255),
                        ModifiedDate = c.DateTime(),
                        UpdateBy = c.String(maxLength: 255),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.InventoryDeliveryVoucherID);
            
            CreateTable(
                "dbo.InventoryDeliveryVoucherDetails",
                c => new
                    {
                        InventoryDeliveryVoucherDetailID = c.Long(nullable: false, identity: true),
                        InventoryDeliveryVoucherID = c.Long(nullable: false),
                        ProductID = c.Long(nullable: false),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.InventoryDeliveryVoucherDetailID)
                .ForeignKey("dbo.InventoryDeliveryVouchers", t => t.InventoryDeliveryVoucherID, cascadeDelete: true)
                .Index(t => t.InventoryDeliveryVoucherID);
            
            CreateTable(
                "dbo.InventoryReceivingVouchers",
                c => new
                    {
                        InventoryDeliveryVoucherID = c.Long(nullable: false, identity: true),
                        SupplierID = c.Int(),
                        TaxImport = c.Int(),
                        TaxVAT = c.Int(),
                        TotalQuantity = c.Int(nullable: false),
                        TotalAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AmountPaid = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AmountOwed = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Status = c.Int(nullable: false),
                        Description = c.String(maxLength: 1000),
                    })
                .PrimaryKey(t => t.InventoryDeliveryVoucherID);
            
            CreateTable(
                "dbo.InventoryReceivingVoucherDetails",
                c => new
                    {
                        InventoryReceivingVoucherDetailID = c.Long(nullable: false, identity: true),
                        InventoryReceivingVoucherID = c.Long(nullable: false),
                        ProductID = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.InventoryReceivingVoucherDetailID)
                .ForeignKey("dbo.InventoryReceivingVouchers", t => t.InventoryReceivingVoucherID, cascadeDelete: true)
                .Index(t => t.InventoryReceivingVoucherID);
            
            CreateTable(
                "dbo.Menus",
                c => new
                    {
                        MenuID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                        Url = c.String(maxLength: 255, unicode: false),
                        Sequence = c.Int(nullable: false),
                        ParentID = c.Int(nullable: false),
                        IsParent = c.Boolean(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        IsDisplay = c.Boolean(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.MenuID);
            
            CreateTable(
                "dbo.MenuPermissions",
                c => new
                    {
                        MenuPermissionID = c.Long(nullable: false, identity: true),
                        UserID = c.String(nullable: false),
                        MenuID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MenuPermissionID)
                .ForeignKey("dbo.Menus", t => t.MenuID, cascadeDelete: true)
                .Index(t => t.MenuID);
            
            CreateTable(
                "dbo.MenuPermissionDetails",
                c => new
                    {
                        MenuPermissionDetailID = c.Long(nullable: false, identity: true),
                        MenuPermissionID = c.Long(nullable: false),
                        IsView = c.Boolean(nullable: false),
                        IsAdd = c.Boolean(nullable: false),
                        IsEdit = c.Boolean(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.MenuPermissionDetailID)
                .ForeignKey("dbo.MenuPermissions", t => t.MenuPermissionID, cascadeDelete: true)
                .Index(t => t.MenuPermissionID);
            
            CreateTable(
                "dbo.IdentityRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IdentityUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                        IdentityRole_Id = c.String(maxLength: 128),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.IdentityRoles", t => t.IdentityRole_Id)
                .ForeignKey("dbo.ApplicationUsers", t => t.ApplicationUser_Id)
                .Index(t => t.IdentityRole_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.Stocks",
                c => new
                    {
                        StockID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                        Address = c.String(nullable: false, maxLength: 255),
                        CellPhone = c.String(nullable: false, maxLength: 15, unicode: false),
                        SeniorManagerID = c.String(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        CreateBy = c.String(maxLength: 255),
                        ModifiedDate = c.DateTime(),
                        UpdateBy = c.String(maxLength: 255),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.StockID);
            
            CreateTable(
                "dbo.StockManagers",
                c => new
                    {
                        StockManagerID = c.Long(nullable: false, identity: true),
                        StockID = c.Int(nullable: false),
                        UserID = c.String(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        CreateBy = c.String(maxLength: 255),
                        ModifiedDate = c.DateTime(),
                        UpdateBy = c.String(maxLength: 255),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.StockManagerID)
                .ForeignKey("dbo.Stocks", t => t.StockID, cascadeDelete: true)
                .Index(t => t.StockID);
            
            CreateTable(
                "dbo.StockProducts",
                c => new
                    {
                        StockProductID = c.Long(nullable: false, identity: true),
                        StockID = c.Int(nullable: false),
                        ProductID = c.Long(nullable: false),
                        ColorID = c.Int(nullable: false),
                        Size = c.String(),
                        Quantity = c.Int(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        CreateBy = c.String(maxLength: 255),
                        ModifiedDate = c.DateTime(),
                        UpdateBy = c.String(maxLength: 255),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.StockProductID)
                .ForeignKey("dbo.Products", t => t.ProductID, cascadeDelete: true)
                .ForeignKey("dbo.Stocks", t => t.StockID, cascadeDelete: true)
                .Index(t => t.StockID)
                .Index(t => t.ProductID);
            
            CreateTable(
                "dbo.Suppliers",
                c => new
                    {
                        SupplierID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                        Address = c.String(nullable: false, maxLength: 255),
                        Phone = c.String(maxLength: 15, unicode: false),
                        Email = c.String(maxLength: 200, unicode: false),
                        Description = c.String(maxLength: 500),
                        CreateDate = c.DateTime(nullable: false),
                        CreateBy = c.String(maxLength: 255),
                        ModifiedDate = c.DateTime(),
                        UpdateBy = c.String(maxLength: 255),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.SupplierID);
            
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        TagID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.TagID);
            
            CreateTable(
                "dbo.ApplicationUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(maxLength: 200),
                        LastName = c.String(maxLength: 50),
                        Address = c.String(maxLength: 255),
                        BirthDay = c.DateTime(nullable: false),
                        Email = c.String(),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IdentityUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ApplicationUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.IdentityUserLogins",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        LoginProvider = c.String(),
                        ProviderKey = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.ApplicationUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IdentityUserRoles", "ApplicationUser_Id", "dbo.ApplicationUsers");
            DropForeignKey("dbo.IdentityUserLogins", "ApplicationUser_Id", "dbo.ApplicationUsers");
            DropForeignKey("dbo.IdentityUserClaims", "ApplicationUser_Id", "dbo.ApplicationUsers");
            DropForeignKey("dbo.StockProducts", "StockID", "dbo.Stocks");
            DropForeignKey("dbo.StockProducts", "ProductID", "dbo.Products");
            DropForeignKey("dbo.StockManagers", "StockID", "dbo.Stocks");
            DropForeignKey("dbo.IdentityUserRoles", "IdentityRole_Id", "dbo.IdentityRoles");
            DropForeignKey("dbo.MenuPermissionDetails", "MenuPermissionID", "dbo.MenuPermissions");
            DropForeignKey("dbo.MenuPermissions", "MenuID", "dbo.Menus");
            DropForeignKey("dbo.InventoryReceivingVoucherDetails", "InventoryReceivingVoucherID", "dbo.InventoryReceivingVouchers");
            DropForeignKey("dbo.InventoryDeliveryVoucherDetails", "InventoryDeliveryVoucherID", "dbo.InventoryDeliveryVouchers");
            DropForeignKey("dbo.FieldProduct", "FieldID", "dbo.Fields");
            DropForeignKey("dbo.Fields", "FieldGroupID", "dbo.FieldGroups");
            DropForeignKey("dbo.FieldGroups", "CategoryID", "dbo.Categories");
            DropForeignKey("dbo.ColorProducts", "ProductID", "dbo.Products");
            DropForeignKey("dbo.Products", "ProductCategoryID", "dbo.ProductCategories");
            DropForeignKey("dbo.ProductCategories", "CategoryID", "dbo.Categories");
            DropForeignKey("dbo.ColorProducts", "ColorID", "dbo.Colors");
            DropIndex("dbo.IdentityUserLogins", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserClaims", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.StockProducts", new[] { "ProductID" });
            DropIndex("dbo.StockProducts", new[] { "StockID" });
            DropIndex("dbo.StockManagers", new[] { "StockID" });
            DropIndex("dbo.IdentityUserRoles", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserRoles", new[] { "IdentityRole_Id" });
            DropIndex("dbo.MenuPermissionDetails", new[] { "MenuPermissionID" });
            DropIndex("dbo.MenuPermissions", new[] { "MenuID" });
            DropIndex("dbo.InventoryReceivingVoucherDetails", new[] { "InventoryReceivingVoucherID" });
            DropIndex("dbo.InventoryDeliveryVoucherDetails", new[] { "InventoryDeliveryVoucherID" });
            DropIndex("dbo.FieldProduct", new[] { "FieldID" });
            DropIndex("dbo.FieldGroups", new[] { "CategoryID" });
            DropIndex("dbo.Fields", new[] { "FieldGroupID" });
            DropIndex("dbo.ProductCategories", new[] { "CategoryID" });
            DropIndex("dbo.Products", new[] { "ProductCategoryID" });
            DropIndex("dbo.ColorProducts", new[] { "ProductID" });
            DropIndex("dbo.ColorProducts", new[] { "ColorID" });
            DropTable("dbo.IdentityUserLogins");
            DropTable("dbo.IdentityUserClaims");
            DropTable("dbo.ApplicationUsers");
            DropTable("dbo.Tags");
            DropTable("dbo.Suppliers");
            DropTable("dbo.StockProducts");
            DropTable("dbo.StockManagers");
            DropTable("dbo.Stocks");
            DropTable("dbo.IdentityUserRoles");
            DropTable("dbo.IdentityRoles");
            DropTable("dbo.MenuPermissionDetails");
            DropTable("dbo.MenuPermissions");
            DropTable("dbo.Menus");
            DropTable("dbo.InventoryReceivingVoucherDetails");
            DropTable("dbo.InventoryReceivingVouchers");
            DropTable("dbo.InventoryDeliveryVoucherDetails");
            DropTable("dbo.InventoryDeliveryVouchers");
            DropTable("dbo.FieldProduct");
            DropTable("dbo.FieldGroups");
            DropTable("dbo.Fields");
            DropTable("dbo.ErrorLogs");
            DropTable("dbo.Customers");
            DropTable("dbo.ProductCategories");
            DropTable("dbo.Products");
            DropTable("dbo.ColorProducts");
            DropTable("dbo.Colors");
            DropTable("dbo.Categories");
        }
    }
}

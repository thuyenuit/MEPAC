namespace SMS.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Category",
                c => new
                    {
                        CategoryID = c.Long(nullable: false, identity: true),
                        CategoryName = c.String(nullable: false, maxLength: 255),
                        Alias = c.String(nullable: false, maxLength: 255, unicode: false),
                        Icon = c.Binary(),
                        OrderNumber = c.Int(nullable: false),
                        HomeFlag = c.Boolean(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        CreateBy = c.String(maxLength: 255),
                        ModifiedDate = c.DateTime(),
                        UpdateBy = c.String(maxLength: 255),
                        MetaKeyword = c.String(maxLength: 500),
                        MetaDescription = c.String(maxLength: 500),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.CategoryID);
            
            CreateTable(
                "dbo.CategoryType",
                c => new
                    {
                        CategoryTypeID = c.Long(nullable: false, identity: true),
                        CategoryTypeName = c.String(nullable: false, maxLength: 255),
                        Alias = c.String(nullable: false, maxLength: 255, unicode: false),
                        Icon = c.Binary(),
                        OrderNumber = c.Int(nullable: false),
                        HomeFlag = c.Boolean(nullable: false),
                        CategoryID = c.Long(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        CreateBy = c.String(maxLength: 255),
                        ModifiedDate = c.DateTime(),
                        UpdateBy = c.String(maxLength: 255),
                        MetaKeyword = c.String(maxLength: 500),
                        MetaDescription = c.String(maxLength: 500),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.CategoryTypeID);
            
            CreateTable(
                "dbo.Customer",
                c => new
                    {
                        CustomerID = c.Long(nullable: false, identity: true),
                        CustomerName = c.String(nullable: false, maxLength: 255),
                        Phone = c.String(nullable: false, maxLength: 15, unicode: false),
                        Address = c.String(maxLength: 255),
                        Identity = c.String(maxLength: 15, unicode: false),
                        CreateDate = c.DateTime(nullable: false),
                        CreateBy = c.String(maxLength: 255),
                        ModifiedDate = c.DateTime(),
                        UpdateBy = c.String(maxLength: 255),
                        MetaKeyword = c.String(maxLength: 500),
                        MetaDescription = c.String(maxLength: 500),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.CustomerID);
            
            CreateTable(
                "dbo.ErrorLog",
                c => new
                    {
                        ErrorLogID = c.Guid(nullable: false, identity: true),
                        Message = c.String(nullable: false),
                        DateLog = c.DateTime(nullable: false),
                        StackTrace = c.String(),
                        Method = c.String(maxLength: 255),
                        Table = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.ErrorLogID);
            
            CreateTable(
                "dbo.InventoryDeliveryVoucher",
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
                        MetaKeyword = c.String(maxLength: 500),
                        MetaDescription = c.String(maxLength: 500),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.InventoryDeliveryVoucherID);
            
            CreateTable(
                "dbo.InventoryDeliveryVoucherDetail",
                c => new
                    {
                        InventoryDeliveryVoucherDetailID = c.Long(nullable: false, identity: true),
                        InventoryDeliveryVoucherID = c.Long(nullable: false),
                        ProductID = c.Long(nullable: false),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.InventoryDeliveryVoucherDetailID)
                .ForeignKey("dbo.InventoryDeliveryVoucher", t => t.InventoryDeliveryVoucherID, cascadeDelete: true)
                .Index(t => t.InventoryDeliveryVoucherID);
            
            CreateTable(
                "dbo.InventoryReceivingVoucher",
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
                "dbo.InventoryReceivingVoucherDetail",
                c => new
                    {
                        InventoryReceivingVoucherDetailID = c.Long(nullable: false, identity: true),
                        InventoryReceivingVoucherID = c.Long(nullable: false),
                        ProductID = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.InventoryReceivingVoucherDetailID)
                .ForeignKey("dbo.InventoryReceivingVoucher", t => t.InventoryReceivingVoucherID, cascadeDelete: true)
                .Index(t => t.InventoryReceivingVoucherID);
            
            CreateTable(
                "dbo.ManagementStock",
                c => new
                    {
                        StockID = c.Long(nullable: false, identity: true),
                        StockName = c.String(nullable: false, maxLength: 500),
                        StockCode = c.String(nullable: false, maxLength: 255),
                        CreateDate = c.DateTime(nullable: false),
                        CreateBy = c.String(maxLength: 255),
                        ModifiedDate = c.DateTime(),
                        UpdateBy = c.String(maxLength: 255),
                        MetaKeyword = c.String(maxLength: 500),
                        MetaDescription = c.String(maxLength: 500),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.StockID);
            
            CreateTable(
                "dbo.Product",
                c => new
                    {
                        ProductID = c.Long(nullable: false, identity: true),
                        CategoryID = c.Long(nullable: false),
                        CategoryTypeID = c.Long(nullable: false),
                        ProductName = c.String(nullable: false, maxLength: 255),
                        ProductCode = c.String(nullable: false, maxLength: 255),
                        Alias = c.String(nullable: false, maxLength: 255, unicode: false),
                        Image = c.Binary(),
                        MoreImage = c.String(storeType: "xml"),
                        Content = c.String(unicode: false, storeType: "text"),
                        OrderNumber = c.Int(nullable: false),
                        HomeFlag = c.Boolean(nullable: false),
                        HotFlag = c.Boolean(nullable: false),
                        Status = c.Int(nullable: false),
                        Tags = c.String(maxLength: 500),
                        CreateDate = c.DateTime(nullable: false),
                        CreateBy = c.String(maxLength: 255),
                        ModifiedDate = c.DateTime(),
                        UpdateBy = c.String(maxLength: 255),
                        MetaKeyword = c.String(maxLength: 500),
                        MetaDescription = c.String(maxLength: 500),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ProductID);
            
            CreateTable(
                "dbo.ProductDetail",
                c => new
                    {
                        ProductDetailID = c.Guid(nullable: false, identity: true),
                        ProductID = c.Long(nullable: false),
                        Quantity = c.Int(nullable: false),
                        PriceInput = c.Decimal(precision: 18, scale: 2),
                        PriceSale = c.Decimal(precision: 18, scale: 2),
                        CreateDate = c.DateTime(nullable: false),
                        CreateBy = c.String(maxLength: 255),
                        ModifiedDate = c.DateTime(),
                        UpdateBy = c.String(maxLength: 255),
                        MetaKeyword = c.String(maxLength: 500),
                        MetaDescription = c.String(maxLength: 500),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ProductDetailID);
            
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
                "dbo.Supplier",
                c => new
                    {
                        SupplierID = c.Int(nullable: false, identity: true),
                        SupplierName = c.String(nullable: false, maxLength: 255),
                        Address = c.String(nullable: false, maxLength: 255),
                        Phone = c.String(maxLength: 15, unicode: false),
                        Email = c.String(maxLength: 200, unicode: false),
                        CreateDate = c.DateTime(nullable: false),
                        CreateBy = c.String(maxLength: 255),
                        ModifiedDate = c.DateTime(),
                        UpdateBy = c.String(maxLength: 255),
                        MetaKeyword = c.String(maxLength: 500),
                        MetaDescription = c.String(maxLength: 500),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.SupplierID);
            
            CreateTable(
                "dbo.Tag",
                c => new
                    {
                        TagID = c.Int(nullable: false, identity: true),
                        TagName = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.TagID);
            
            CreateTable(
                "dbo.ApplicationUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        UserCode = c.String(maxLength: 50, unicode: false),
                        FullName = c.String(maxLength: 200),
                        FirstName = c.String(maxLength: 50),
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
            DropForeignKey("dbo.IdentityUserRoles", "IdentityRole_Id", "dbo.IdentityRoles");
            DropForeignKey("dbo.InventoryReceivingVoucherDetail", "InventoryReceivingVoucherID", "dbo.InventoryReceivingVoucher");
            DropForeignKey("dbo.InventoryDeliveryVoucherDetail", "InventoryDeliveryVoucherID", "dbo.InventoryDeliveryVoucher");
            DropIndex("dbo.IdentityUserLogins", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserClaims", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserRoles", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserRoles", new[] { "IdentityRole_Id" });
            DropIndex("dbo.InventoryReceivingVoucherDetail", new[] { "InventoryReceivingVoucherID" });
            DropIndex("dbo.InventoryDeliveryVoucherDetail", new[] { "InventoryDeliveryVoucherID" });
            DropTable("dbo.IdentityUserLogins");
            DropTable("dbo.IdentityUserClaims");
            DropTable("dbo.ApplicationUsers");
            DropTable("dbo.Tag");
            DropTable("dbo.Supplier");
            DropTable("dbo.IdentityUserRoles");
            DropTable("dbo.IdentityRoles");
            DropTable("dbo.ProductDetail");
            DropTable("dbo.Product");
            DropTable("dbo.ManagementStock");
            DropTable("dbo.InventoryReceivingVoucherDetail");
            DropTable("dbo.InventoryReceivingVoucher");
            DropTable("dbo.InventoryDeliveryVoucherDetail");
            DropTable("dbo.InventoryDeliveryVoucher");
            DropTable("dbo.ErrorLog");
            DropTable("dbo.Customer");
            DropTable("dbo.CategoryType");
            DropTable("dbo.Category");
        }
    }
}

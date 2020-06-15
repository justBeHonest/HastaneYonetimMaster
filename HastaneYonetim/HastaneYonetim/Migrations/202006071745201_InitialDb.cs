namespace HastaneYonetim.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialDb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bakims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        KlinikBulgular = c.String(nullable: false),
                        Teshis = c.String(nullable: false, maxLength: 255),
                        Teshis2 = c.String(),
                        Teshis3 = c.String(),
                        Terapi = c.String(nullable: false),
                        Tarih = c.DateTime(nullable: false),
                        HastaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Hastas", t => t.HastaId, cascadeDelete: true)
                .Index(t => t.HastaId);
            
            CreateTable(
                "dbo.Hastas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        HastaNumarasi = c.String(nullable: false),
                        Ad = c.String(nullable: false, maxLength: 255),
                        Cinsiyet = c.Int(nullable: false),
                        DogumTarihi = c.DateTime(nullable: false),
                        Telefon = c.String(nullable: false, maxLength: 255),
                        Adres = c.String(nullable: false, maxLength: 255),
                        SehirId = c.Byte(nullable: false),
                        TarihSure = c.DateTime(nullable: false),
                        Boy = c.String(),
                        Kilo = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Sehirs", t => t.SehirId, cascadeDelete: true)
                .Index(t => t.SehirId);
            
            CreateTable(
                "dbo.Randevus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BaslangicTarihSure = c.DateTime(nullable: false),
                        Detay = c.String(nullable: false),
                        Durum = c.Boolean(nullable: false),
                        HastaId = c.Int(nullable: false),
                        DoktorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Doktors", t => t.DoktorId, cascadeDelete: true)
                .ForeignKey("dbo.Hastas", t => t.HastaId)
                .Index(t => t.HastaId)
                .Index(t => t.DoktorId);
            
            CreateTable(
                "dbo.Doktors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Ad = c.String(nullable: false, maxLength: 255),
                        Telefon = c.String(nullable: false),
                        musaitMi = c.Boolean(nullable: false),
                        Adres = c.String(),
                        UzmanlikId = c.Int(nullable: false),
                        HekimId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.HekimId, cascadeDelete: true)
                .ForeignKey("dbo.Uzmanliks", t => t.UzmanlikId, cascadeDelete: true)
                .Index(t => t.UzmanlikId)
                .Index(t => t.HekimId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Ad = c.String(),
                        aktifMi = c.Boolean(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Uzmanliks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Ad = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Sehirs",
                c => new
                    {
                        Id = c.Byte(nullable: false),
                        Ad = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Hastas", "SehirId", "dbo.Sehirs");
            DropForeignKey("dbo.Randevus", "HastaId", "dbo.Hastas");
            DropForeignKey("dbo.Doktors", "UzmanlikId", "dbo.Uzmanliks");
            DropForeignKey("dbo.Randevus", "DoktorId", "dbo.Doktors");
            DropForeignKey("dbo.Doktors", "HekimId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Bakims", "HastaId", "dbo.Hastas");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Doktors", new[] { "HekimId" });
            DropIndex("dbo.Doktors", new[] { "UzmanlikId" });
            DropIndex("dbo.Randevus", new[] { "DoktorId" });
            DropIndex("dbo.Randevus", new[] { "HastaId" });
            DropIndex("dbo.Hastas", new[] { "SehirId" });
            DropIndex("dbo.Bakims", new[] { "HastaId" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Sehirs");
            DropTable("dbo.Uzmanliks");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Doktors");
            DropTable("dbo.Randevus");
            DropTable("dbo.Hastas");
            DropTable("dbo.Bakims");
        }
    }
}

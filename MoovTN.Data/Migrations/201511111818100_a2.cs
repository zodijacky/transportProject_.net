namespace MoovTn.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class a2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.claim",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        claimDate = c.DateTime(precision: 0),
                        question = c.String(maxLength: 120, storeType: "nvarchar"),
                        questionRead = c.Boolean(),
                        response = c.String(maxLength: 120, storeType: "nvarchar"),
                        responseRead = c.Boolean(),
                        users_id = c.String(maxLength: 128, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.user", t => t.users_id)
                .Index(t => t.users_id);
            
            CreateTable(
                "dbo.user",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        type_user = c.String(unicode: false),
                        birthdate = c.DateTime(nullable: false, precision: 0),
                        Cin = c.Int(nullable: false),
                        FirstName = c.String(unicode: false),
                        job = c.String(unicode: false),
                        LastName = c.String(unicode: false),
                        DisplayItem = c.Boolean(nullable: false),
                        Image = c.Binary(),
                        Email = c.String(unicode: false),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(unicode: false),
                        SecurityStamp = c.String(unicode: false),
                        PhoneNumber = c.String(unicode: false),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(precision: 0),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128, storeType: "nvarchar"),
                        ClaimType = c.String(unicode: false),
                        ClaimValue = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.user", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.line",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(maxLength: 120, storeType: "nvarchar"),
                        path = c.String(maxLength: 120, storeType: "nvarchar"),
                        type = c.String(maxLength: 120, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.linestation",
                c => new
                    {
                        line_fk = c.Int(nullable: false),
                        station_fk = c.Int(nullable: false),
                        ordre = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.line_fk, t.station_fk })
                .ForeignKey("dbo.line", t => t.line_fk, cascadeDelete: true)
                .ForeignKey("dbo.station", t => t.station_fk, cascadeDelete: true)
                .Index(t => t.line_fk)
                .Index(t => t.station_fk);
            
            CreateTable(
                "dbo.station",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        latitude = c.Double(),
                        longitude = c.Double(),
                        name = c.String(maxLength: 120, storeType: "nvarchar"),
                        type = c.String(maxLength: 120, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.notification",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        broadcast = c.Boolean(nullable: false),
                        creationDate = c.DateTime(precision: 0),
                        description = c.String(maxLength: 120, storeType: "nvarchar"),
                        level = c.Int(nullable: false),
                        idLine = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.line", t => t.idLine)
                .Index(t => t.idLine);
            
            CreateTable(
                "dbo.subscriptioncard",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        expired = c.Boolean(nullable: false),
                        locked = c.Boolean(nullable: false),
                        validityEnd = c.DateTime(precision: 0),
                        validityStart = c.DateTime(precision: 0),
                        user_id = c.String(maxLength: 128, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.user", t => t.user_id)
                .Index(t => t.user_id);
            
            CreateTable(
                "dbo.trip",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        arrivalTime = c.DateTime(precision: 0),
                        canceled = c.Boolean(),
                        departureTime = c.DateTime(precision: 0),
                        line_id = c.Int(),
                        transportMean_serial = c.String(maxLength: 120, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.line", t => t.line_id)
                .ForeignKey("dbo.transportmean", t => t.transportMean_serial)
                .Index(t => t.line_id)
                .Index(t => t.transportMean_serial);
            
            CreateTable(
                "dbo.transportmean",
                c => new
                    {
                        serial = c.String(nullable: false, maxLength: 120, storeType: "nvarchar"),
                        state = c.String(maxLength: 120, storeType: "nvarchar"),
                        type = c.String(maxLength: 120, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.serial);
            
            CreateTable(
                "dbo.rent",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        members = c.Int(nullable: false),
                        rentEnd = c.DateTime(precision: 0),
                        rentStart = c.DateTime(precision: 0),
                        status = c.String(maxLength: 120, storeType: "nvarchar"),
                        idDepart = c.Int(),
                        idDestination = c.Int(),
                        transportMean_serial = c.String(maxLength: 120, storeType: "nvarchar"),
                        user_id = c.String(maxLength: 128, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.location", t => t.idDepart)
                .ForeignKey("dbo.location", t => t.idDestination)
                .ForeignKey("dbo.transportmean", t => t.transportMean_serial)
                .ForeignKey("dbo.user", t => t.user_id)
                .Index(t => t.idDepart)
                .Index(t => t.idDestination)
                .Index(t => t.transportMean_serial)
                .Index(t => t.user_id);
            
            CreateTable(
                "dbo.location",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        adress = c.String(maxLength: 120, storeType: "nvarchar"),
                        latitude = c.Double(nullable: false),
                        longitude = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        ProviderKey = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        UserId = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.user", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        RoleId = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.user", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        Name = c.String(nullable: false, maxLength: 120, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "transportds.subscriptioncard_line",
                c => new
                    {
                        SubscriptionCard_id = c.Int(nullable: false),
                        lines_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.SubscriptionCard_id, t.lines_id })
                .ForeignKey("dbo.subscriptioncard", t => t.SubscriptionCard_id, cascadeDelete: true)
                .ForeignKey("dbo.line", t => t.lines_id, cascadeDelete: true)
                .Index(t => t.SubscriptionCard_id)
                .Index(t => t.lines_id);
            
            CreateTable(
                "transportds.line_trip",
                c => new
                    {
                        trips_id = c.Int(nullable: false),
                        Line_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.trips_id, t.Line_id })
                .ForeignKey("dbo.trip", t => t.trips_id, cascadeDelete: true)
                .ForeignKey("dbo.line", t => t.Line_id, cascadeDelete: true)
                .Index(t => t.trips_id)
                .Index(t => t.Line_id);
            
            CreateTable(
                "transportds.transportmean_rent",
                c => new
                    {
                        rent_id = c.Int(nullable: false),
                        TransportMean_serial = c.String(nullable: false, maxLength: 120, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => new { t.rent_id, t.TransportMean_serial })
                .ForeignKey("dbo.rent", t => t.rent_id, cascadeDelete: true)
                .ForeignKey("dbo.transportmean", t => t.TransportMean_serial, cascadeDelete: true)
                .Index(t => t.rent_id)
                .Index(t => t.TransportMean_serial);
            
            CreateTable(
                "transportds.user_line",
                c => new
                    {
                        lines_id = c.Int(nullable: false),
                        USER_id = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => new { t.lines_id, t.USER_id })
                .ForeignKey("dbo.line", t => t.lines_id, cascadeDelete: true)
                .ForeignKey("dbo.user", t => t.USER_id, cascadeDelete: true)
                .Index(t => t.lines_id)
                .Index(t => t.USER_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.claim", "users_id", "dbo.user");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.user");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.user");
            DropForeignKey("transportds.user_line", "USER_id", "dbo.user");
            DropForeignKey("transportds.user_line", "lines_id", "dbo.line");
            DropForeignKey("dbo.trip", "transportMean_serial", "dbo.transportmean");
            DropForeignKey("dbo.rent", "user_id", "dbo.user");
            DropForeignKey("transportds.transportmean_rent", "TransportMean_serial", "dbo.transportmean");
            DropForeignKey("transportds.transportmean_rent", "rent_id", "dbo.rent");
            DropForeignKey("dbo.rent", "transportMean_serial", "dbo.transportmean");
            DropForeignKey("dbo.rent", "idDestination", "dbo.location");
            DropForeignKey("dbo.rent", "idDepart", "dbo.location");
            DropForeignKey("transportds.line_trip", "Line_id", "dbo.line");
            DropForeignKey("transportds.line_trip", "trips_id", "dbo.trip");
            DropForeignKey("dbo.trip", "line_id", "dbo.line");
            DropForeignKey("dbo.subscriptioncard", "user_id", "dbo.user");
            DropForeignKey("transportds.subscriptioncard_line", "lines_id", "dbo.line");
            DropForeignKey("transportds.subscriptioncard_line", "SubscriptionCard_id", "dbo.subscriptioncard");
            DropForeignKey("dbo.notification", "idLine", "dbo.line");
            DropForeignKey("dbo.linestation", "station_fk", "dbo.station");
            DropForeignKey("dbo.linestation", "line_fk", "dbo.line");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.user");
            DropIndex("transportds.user_line", new[] { "USER_id" });
            DropIndex("transportds.user_line", new[] { "lines_id" });
            DropIndex("transportds.transportmean_rent", new[] { "TransportMean_serial" });
            DropIndex("transportds.transportmean_rent", new[] { "rent_id" });
            DropIndex("transportds.line_trip", new[] { "Line_id" });
            DropIndex("transportds.line_trip", new[] { "trips_id" });
            DropIndex("transportds.subscriptioncard_line", new[] { "lines_id" });
            DropIndex("transportds.subscriptioncard_line", new[] { "SubscriptionCard_id" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.rent", new[] { "user_id" });
            DropIndex("dbo.rent", new[] { "transportMean_serial" });
            DropIndex("dbo.rent", new[] { "idDestination" });
            DropIndex("dbo.rent", new[] { "idDepart" });
            DropIndex("dbo.trip", new[] { "transportMean_serial" });
            DropIndex("dbo.trip", new[] { "line_id" });
            DropIndex("dbo.subscriptioncard", new[] { "user_id" });
            DropIndex("dbo.notification", new[] { "idLine" });
            DropIndex("dbo.linestation", new[] { "station_fk" });
            DropIndex("dbo.linestation", new[] { "line_fk" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.claim", new[] { "users_id" });
            DropTable("transportds.user_line");
            DropTable("transportds.transportmean_rent");
            DropTable("transportds.line_trip");
            DropTable("transportds.subscriptioncard_line");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.location");
            DropTable("dbo.rent");
            DropTable("dbo.transportmean");
            DropTable("dbo.trip");
            DropTable("dbo.subscriptioncard");
            DropTable("dbo.notification");
            DropTable("dbo.station");
            DropTable("dbo.linestation");
            DropTable("dbo.line");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.user");
            DropTable("dbo.claim");
        }
    }
}

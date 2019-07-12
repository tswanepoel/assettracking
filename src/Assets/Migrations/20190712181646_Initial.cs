using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Assets.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AssetType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetType", x => x.Id)
                        .Annotation("SqlServer:Clustered", true);
                    table.UniqueConstraint("AK_AssetType_Name", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "Blob",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Guid = table.Column<Guid>(nullable: false),
                    ContentType = table.Column<string>(maxLength: 128, nullable: false),
                    Content = table.Column<byte[]>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blob", x => x.Id)
                        .Annotation("SqlServer:Clustered", true);
                    table.UniqueConstraint("AK_Blob_Guid", x => x.Guid);
                });

            migrationBuilder.CreateTable(
                name: "ContactType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactType", x => x.Id)
                        .Annotation("SqlServer:Clustered", true);
                    table.UniqueConstraint("AK_ContactType_Name", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id)
                        .Annotation("SqlServer:Clustered", true);
                    table.UniqueConstraint("AK_Role_Name", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "Tenant",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Version = table.Column<byte[]>(rowVersion: true, nullable: false),
                    Area = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    CreatedDate = table.Column<DateTimeOffset>(nullable: false),
                    CreatedUser = table.Column<string>(maxLength: 128, nullable: false),
                    ModifiedDate = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedUser = table.Column<string>(maxLength: 128, nullable: false),
                    DeletedDate = table.Column<DateTimeOffset>(nullable: true),
                    DeletedUser = table.Column<string>(maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tenant", x => x.Id)
                        .Annotation("SqlServer:Clustered", true);
                    table.UniqueConstraint("AK_Tenant_Area", x => x.Area);
                    table.UniqueConstraint("AK_Tenant_Name", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Version = table.Column<byte[]>(rowVersion: true, nullable: false),
                    UserName = table.Column<string>(maxLength: 128, nullable: false),
                    PasswordSalt = table.Column<byte[]>(nullable: true),
                    PasswordHash = table.Column<byte[]>(nullable: true),
                    FullName = table.Column<string>(maxLength: 128, nullable: true),
                    Initials = table.Column<string>(maxLength: 128, nullable: true),
                    Phone = table.Column<string>(maxLength: 128, nullable: true),
                    Email = table.Column<string>(maxLength: 128, nullable: true),
                    LastAccessedDate = table.Column<DateTimeOffset>(nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(nullable: false),
                    CreatedUser = table.Column<string>(maxLength: 128, nullable: false),
                    ModifiedDate = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedUser = table.Column<string>(maxLength: 128, nullable: false),
                    DeletedDate = table.Column<DateTimeOffset>(nullable: true),
                    DeletedUser = table.Column<string>(maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id)
                        .Annotation("SqlServer:Clustered", true);
                    table.UniqueConstraint("AK_User_UserName", x => x.UserName);
                });

            migrationBuilder.CreateTable(
                name: "Contact",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TenantId = table.Column<int>(nullable: false),
                    ContactTypeId = table.Column<int>(nullable: false),
                    Guid = table.Column<Guid>(nullable: false),
                    Version = table.Column<byte[]>(rowVersion: true, nullable: false),
                    Name = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(nullable: false),
                    CreatedUser = table.Column<string>(maxLength: 128, nullable: false),
                    ModifiedDate = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedUser = table.Column<string>(maxLength: 128, nullable: false),
                    DeletedDate = table.Column<DateTimeOffset>(nullable: true),
                    DeletedUser = table.Column<string>(maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contact", x => x.Id)
                        .Annotation("SqlServer:Clustered", true);
                    table.UniqueConstraint("AK_Contact_Guid", x => x.Guid);
                    table.ForeignKey(
                        name: "FK_Contact_ContactType",
                        column: x => x.ContactTypeId,
                        principalTable: "ContactType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Contact_Tenant",
                        column: x => x.TenantId,
                        principalTable: "Tenant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRole",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    RoleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRole", x => new { x.UserId, x.RoleId })
                        .Annotation("SqlServer:Clustered", true);
                    table.ForeignKey(
                        name: "FK_UserRole_Role",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRole_User",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserTenantRole",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    TenantId = table.Column<int>(nullable: false),
                    RoleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTenantRole", x => new { x.UserId, x.TenantId, x.RoleId })
                        .Annotation("SqlServer:Clustered", true);
                    table.ForeignKey(
                        name: "FK_UserTenantRole_Role",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserTenantRole_Tenant",
                        column: x => x.UserId,
                        principalTable: "Tenant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserTenantRole_User",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Asset",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TenantId = table.Column<int>(nullable: false),
                    AssetTypeId = table.Column<int>(nullable: false),
                    Guid = table.Column<Guid>(nullable: false),
                    Version = table.Column<byte[]>(rowVersion: true, nullable: false),
                    Description = table.Column<string>(maxLength: 1024, nullable: true),
                    SerialNumber = table.Column<string>(maxLength: 128, nullable: true),
                    Make = table.Column<string>(maxLength: 128, nullable: true),
                    Model = table.Column<string>(maxLength: 128, nullable: true),
                    Tag = table.Column<string>(maxLength: 128, nullable: true),
                    AllocatedContactId = table.Column<int>(nullable: true),
                    AllocatedDate = table.Column<DateTimeOffset>(nullable: true),
                    AllocatedUser = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(nullable: false),
                    CreatedUser = table.Column<string>(maxLength: 128, nullable: false),
                    ModifiedDate = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedUser = table.Column<string>(maxLength: 128, nullable: false),
                    DeletedDate = table.Column<DateTimeOffset>(nullable: true),
                    DeletedUser = table.Column<string>(maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Asset", x => x.Id)
                        .Annotation("SqlServer:Clustered", true);
                    table.UniqueConstraint("AK_Asset_Guid", x => x.Guid);
                    table.ForeignKey(
                        name: "FK_Asset_AllocatedContact",
                        column: x => x.AllocatedContactId,
                        principalTable: "Contact",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Asset_AssetType",
                        column: x => x.AssetTypeId,
                        principalTable: "AssetType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Asset_Tenant",
                        column: x => x.TenantId,
                        principalTable: "Tenant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ContactComment",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Guid = table.Column<Guid>(nullable: false),
                    ContactId = table.Column<int>(nullable: false),
                    Text = table.Column<string>(maxLength: 128, nullable: false),
                    CreatedDate = table.Column<DateTimeOffset>(nullable: false),
                    CreatedUser = table.Column<string>(maxLength: 128, nullable: false),
                    ModifiedDate = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedUser = table.Column<string>(maxLength: 128, nullable: false),
                    DeletedDate = table.Column<DateTimeOffset>(nullable: true),
                    DeletedUser = table.Column<string>(maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactComment", x => x.Id)
                        .Annotation("SqlServer:Clustered", true);
                    table.UniqueConstraint("AK_ContactComment_Guid", x => x.Guid);
                    table.ForeignKey(
                        name: "FK_ContactComment_Contact",
                        column: x => x.ContactId,
                        principalTable: "Contact",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContactPicture",
                columns: table => new
                {
                    ContactId = table.Column<int>(nullable: false),
                    PictureId = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTimeOffset>(nullable: false),
                    CreatedUser = table.Column<string>(maxLength: 128, nullable: false),
                    ModifiedDate = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedUser = table.Column<string>(maxLength: 128, nullable: false),
                    DeletedDate = table.Column<DateTimeOffset>(nullable: true),
                    DeletedUser = table.Column<string>(maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactPicture", x => new { x.ContactId, x.PictureId })
                        .Annotation("SqlServer:Clustered", true);
                    table.ForeignKey(
                        name: "FK_ContactPicture_Blob",
                        column: x => x.ContactId,
                        principalTable: "Blob",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContactPicture_Contact",
                        column: x => x.ContactId,
                        principalTable: "Contact",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AssetAllocationChange",
                columns: table => new
                {
                    AssetId = table.Column<int>(nullable: false),
                    ContactId = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTimeOffset>(nullable: false),
                    CreatedUser = table.Column<string>(maxLength: 128, nullable: false),
                    ModifiedDate = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedUser = table.Column<string>(maxLength: 128, nullable: false),
                    DeletedDate = table.Column<DateTimeOffset>(nullable: true),
                    DeletedUser = table.Column<string>(maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetAllocationChange", x => new { x.AssetId, x.ContactId })
                        .Annotation("SqlServer:Clustered", true);
                    table.ForeignKey(
                        name: "FK_AssetAllocationChange_Asset",
                        column: x => x.AssetId,
                        principalTable: "Asset",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssetAllocationChange_Contact",
                        column: x => x.ContactId,
                        principalTable: "Contact",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AssetComment",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Guid = table.Column<Guid>(nullable: false),
                    AssetId = table.Column<int>(nullable: false),
                    Text = table.Column<string>(maxLength: 128, nullable: false),
                    CreatedDate = table.Column<DateTimeOffset>(nullable: false),
                    CreatedUser = table.Column<string>(maxLength: 128, nullable: false),
                    ModifiedDate = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedUser = table.Column<string>(maxLength: 128, nullable: false),
                    DeletedDate = table.Column<DateTimeOffset>(nullable: true),
                    DeletedUser = table.Column<string>(maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetComment", x => x.Id)
                        .Annotation("SqlServer:Clustered", true);
                    table.UniqueConstraint("AK_AssetComment_Guid", x => x.Guid);
                    table.ForeignKey(
                        name: "FK_AssetComment_Asset",
                        column: x => x.AssetId,
                        principalTable: "Asset",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AssetPicture",
                columns: table => new
                {
                    AssetId = table.Column<int>(nullable: false),
                    PictureId = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTimeOffset>(nullable: false),
                    CreatedUser = table.Column<string>(maxLength: 128, nullable: false),
                    ModifiedDate = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedUser = table.Column<string>(maxLength: 128, nullable: false),
                    DeletedDate = table.Column<DateTimeOffset>(nullable: true),
                    DeletedUser = table.Column<string>(maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetPicture", x => new { x.AssetId, x.PictureId })
                        .Annotation("SqlServer:Clustered", true);
                    table.ForeignKey(
                        name: "FK_AssetPicture_Asset",
                        column: x => x.AssetId,
                        principalTable: "Asset",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssetPicture_Blob",
                        column: x => x.AssetId,
                        principalTable: "Blob",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Computer",
                columns: table => new
                {
                    ComputerId = table.Column<int>(nullable: false),
                    Processor = table.Column<string>(maxLength: 128, nullable: true),
                    Memory = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Computer", x => x.ComputerId)
                        .Annotation("SqlServer:Clustered", true);
                    table.ForeignKey(
                        name: "FK_Computer_Asset",
                        column: x => x.ComputerId,
                        principalTable: "Asset",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Phone",
                columns: table => new
                {
                    PhoneId = table.Column<int>(nullable: false),
                    Imei = table.Column<string>(maxLength: 128, nullable: true),
                    Processor = table.Column<string>(maxLength: 128, nullable: true),
                    Memory = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Phone", x => x.PhoneId)
                        .Annotation("SqlServer:Clustered", true);
                    table.ForeignKey(
                        name: "FK_Phone_Asset",
                        column: x => x.PhoneId,
                        principalTable: "Asset",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Screen",
                columns: table => new
                {
                    ScreenId = table.Column<int>(nullable: false),
                    SizeInches = table.Column<decimal>(type: "DECIMAL(8, 1)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Screen", x => x.ScreenId)
                        .Annotation("SqlServer:Clustered", true);
                    table.ForeignKey(
                        name: "FK_Screen_Asset",
                        column: x => x.ScreenId,
                        principalTable: "Asset",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AssetType",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Computer" },
                    { 2, "Screen" }
                });

            migrationBuilder.InsertData(
                table: "ContactType",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Person" },
                    { 2, "Organisation" }
                });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Administrator" },
                    { 2, "Manager" },
                    { 3, "Reader" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Asset_AllocatedContactId",
                table: "Asset",
                column: "AllocatedContactId");

            migrationBuilder.CreateIndex(
                name: "IX_Asset_AssetTypeId",
                table: "Asset",
                column: "AssetTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Asset_Tenant_Guid",
                table: "Asset",
                columns: new[] { "TenantId", "Guid" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Asset_Tenant_Tag",
                table: "Asset",
                columns: new[] { "TenantId", "Tag" });

            migrationBuilder.CreateIndex(
                name: "IX_AssetAllocationChange_ContactId",
                table: "AssetAllocationChange",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_AssetComment_AssetId",
                table: "AssetComment",
                column: "AssetId");

            migrationBuilder.CreateIndex(
                name: "IX_Contact_ContactTypeId",
                table: "Contact",
                column: "ContactTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Contact_Tenant_Guid",
                table: "Contact",
                columns: new[] { "TenantId", "Guid" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ContactComment_ContactId",
                table: "ContactComment",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_RoleId",
                table: "UserRole",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTenantRole_RoleId",
                table: "UserTenantRole",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssetAllocationChange");

            migrationBuilder.DropTable(
                name: "AssetComment");

            migrationBuilder.DropTable(
                name: "AssetPicture");

            migrationBuilder.DropTable(
                name: "Computer");

            migrationBuilder.DropTable(
                name: "ContactComment");

            migrationBuilder.DropTable(
                name: "ContactPicture");

            migrationBuilder.DropTable(
                name: "Phone");

            migrationBuilder.DropTable(
                name: "Screen");

            migrationBuilder.DropTable(
                name: "UserRole");

            migrationBuilder.DropTable(
                name: "UserTenantRole");

            migrationBuilder.DropTable(
                name: "Blob");

            migrationBuilder.DropTable(
                name: "Asset");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Contact");

            migrationBuilder.DropTable(
                name: "AssetType");

            migrationBuilder.DropTable(
                name: "ContactType");

            migrationBuilder.DropTable(
                name: "Tenant");
        }
    }
}

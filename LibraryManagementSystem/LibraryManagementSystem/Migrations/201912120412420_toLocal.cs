namespace LibraryManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class toLocal : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Books",
                c => new
                    {
                        BookId = c.Int(nullable: false, identity: true),
                        BookTitle = c.String(nullable: false, maxLength: 50, unicode: false),
                        Author = c.String(nullable: false, maxLength: 50, unicode: false),
                        Publisher = c.String(nullable: false, maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.BookId);
            
            CreateTable(
                "dbo.Borrows",
                c => new
                    {
                        BorrowId = c.Int(nullable: false, identity: true),
                        MemberNo = c.String(nullable: false),
                        BookId = c.Int(nullable: false),
                        Author = c.String(),
                        Publisher = c.String(),
                        IsReturn = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.BorrowId)
                .ForeignKey("dbo.Books", t => t.BookId, cascadeDelete: true)
                .Index(t => t.BookId);
            
            CreateTable(
                "dbo.Members",
                c => new
                    {
                        MemberId = c.Int(nullable: false, identity: true),
                        MemberNo = c.String(nullable: false, maxLength: 50, unicode: false),
                        MemberName = c.String(nullable: false, maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.MemberId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Borrows", "BookId", "dbo.Books");
            DropIndex("dbo.Borrows", new[] { "BookId" });
            DropTable("dbo.Members");
            DropTable("dbo.Borrows");
            DropTable("dbo.Books");
        }
    }
}

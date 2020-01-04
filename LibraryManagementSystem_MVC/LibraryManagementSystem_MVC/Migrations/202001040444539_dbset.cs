namespace LibraryManagementSystem_MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dbset : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Books",
                c => new
                    {
                        BookId = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 50, unicode: false),
                        Author = c.String(nullable: false, maxLength: 50, unicode: false),
                        Publisher = c.String(nullable: false, maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.BookId);
            
            CreateTable(
                "dbo.BorrowBooks",
                c => new
                    {
                        BorrowId = c.Int(nullable: false, identity: true),
                        MemberNo = c.String(nullable: false, maxLength: 50, unicode: false),
                        BookId = c.Int(nullable: false),
                        Author = c.String(),
                        Publisher = c.String(),
                        IsBorrowed = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.BorrowId)
                .ForeignKey("dbo.Books", t => t.BookId)
                .Index(t => t.BookId);
            
            CreateTable(
                "dbo.Members",
                c => new
                    {
                        MemberId = c.Int(nullable: false, identity: true),
                        MemberNo = c.Int(nullable: false),
                        MemberName = c.String(nullable: false, maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.MemberId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BorrowBooks", "BookId", "dbo.Books");
            DropIndex("dbo.BorrowBooks", new[] { "BookId" });
            DropTable("dbo.Members");
            DropTable("dbo.BorrowBooks");
            DropTable("dbo.Books");
        }
    }
}

using System;
using System.Linq;
using System.Text;
using BookShop.Models;
using Microsoft.EntityFrameworkCore;

namespace BookShop
{
    using BookShop.Data;
    using BookShop.Initializer;

    public class StartUp
    {
        public static void Main()
        {
            using (var db = new BookShopContext())
            {
                //task 1
                //string command = Console.ReadLine();
                //string result = GetBooksByAgeRestriction(db, command);

                //task 2
                //string result = GetGoldenBooks(db);
                //Console.WriteLine(result);

                //task 3
                //string result = GetBooksByPrice(db);
                //Console.WriteLine(result);

                //task 4
                //int year = int.Parse(Console.ReadLine());
                //string result = GetBooksNotRealeasedIn(db, year);
                //Console.WriteLine(result);

                //task 5
                //string input = Console.ReadLine();
                //string result = GetBooksByCategory(db, input);
                //Console.WriteLine(result);

                //task 6
                //string dateString = Console.ReadLine();
                //string result = GetBooksReleasedBefore(db, dateString);
                //Console.WriteLine(result);

                //task 7
                //string intput = Console.ReadLine();
                //string result = GetAuthorNamesEndingIn(db, intput);
                //Console.WriteLine(result);

                //task 8
                //string input = Console.ReadLine();
                //string result = GetBookTitlesContaining(db, input);
                //Console.WriteLine(result);

                //task 9
                //string input = Console.ReadLine();
                //string result = GetBooksByAuthor(db, input);
                //Console.WriteLine(result);

                //task 10
                //int input = int.Parse(Console.ReadLine());
                //int result = CountBooks(db, input);
                //Console.WriteLine(result);

                //task 11
                //string result = CountCopiesByAuthor(db);
                //Console.WriteLine(result);

                //task 12
                //string result = GetTotalProfitByCategory(db);
                //Console.WriteLine(result);

                //task 13
                //string result = GetMostRecentBooks(db);
                //System.Console.WriteLine(result);

                //task 14
                //IncreasePrices(db);

                //task 15
                int result = RemoveBooks(db);
                System.Console.WriteLine(result);

            }
        }

        public static string GetBooksByAgeRestriction(BookShopContext context, string command)
        {
            var ageRestriction = (AgeRestriction)Enum.Parse(typeof(AgeRestriction), command, true);
            var books = context.Books.Where(x => x.AgeRestriction == ageRestriction).OrderBy(x => x.Title)
                .Select(t => t.Title).ToArray();
            return String.Join(Environment.NewLine, books);
        }

        public static string GetGoldenBooks(BookShopContext context)
        {
            var books = context.Books.Where(c => c.Copies < 5000 && c.EditionType == EditionType.Gold)
                .OrderBy(x => x.BookId).Select(x => x.Title).ToArray();
            return String.Join(Environment.NewLine, books);
        }

        public static string GetBooksByPrice(BookShopContext context)
        {
            var books = context.Books.Where(p => p.Price > 40).OrderByDescending(x => x.Price)
                .Select(x => $"{x.Title} - ${x.Price:F2}").ToArray();

            return String.Join(Environment.NewLine, books);
        }

        public static string GetBooksNotRealeasedIn(BookShopContext context, int year)
        {
            var books = context.Books.Where(x => x.ReleaseDate.Value.Year != year).OrderBy(x => x.BookId)
                .Select(x => x.Title).ToArray();


            return String.Join(Environment.NewLine, books);
        }

        public static string GetBooksByCategory(BookShopContext context, string input)
        {
            string[] categories = input.ToLower().Split(new char[] {' '}, StringSplitOptions.RemoveEmptyEntries);

            var books = context.Books.Where(x =>
                x.BookCategories.Any(c => categories.Contains(c.Category.Name.ToLower()))).Select(x=>x.Title).OrderBy(x=>x).ToArray();

            return String.Join(Environment.NewLine, books);
        }

        public static string GetBooksReleasedBefore(BookShopContext context, string date)
        {
            StringBuilder result = new StringBuilder();
            DateTime inputDate = DateTime.ParseExact(date, "dd-MM-yyyy", null);
            var books = context.Books.Where(x => x.ReleaseDate.Value < inputDate).OrderByDescending(x => x.ReleaseDate)
                .Select(x => new {x.Title, x.EditionType, x.Price}).ToArray();

            foreach (var book in books)
            {
                result.AppendLine($"{book.Title} - {book.EditionType} - ${book.Price:F2}");
            }

            return result.ToString().TrimEnd();
        }

        public static string GetAuthorNamesEndingIn(BookShopContext context, string input)
        {
            StringBuilder result = new StringBuilder();

            var authors = context.Authors.Where(f => EF.Functions.Like(f.FirstName, "%" + input))
                .Select(x => new {FullName = x.FirstName + " " + x.LastName}).OrderBy(x => x.FullName).ToArray();

            foreach (var author in authors)
            {
                result.AppendLine(author.FullName);
            }

            return result.ToString().TrimEnd();
        }

        public static string GetBookTitlesContaining(BookShopContext context, string input)
        {
            var books = context.Books.Where(x => EF.Functions.Like(x.Title, $"%{input}%")).Select(x => x.Title)
                .OrderBy(x => x);

            return String.Join(Environment.NewLine,books);
        }

        public static string GetBooksByAuthor(BookShopContext context, string input)
        {
            var books = context.Books.Where(l => EF.Functions.Like(l.Author.LastName, $"{input}%"))
                .OrderBy(x => x.BookId).Select(x => $"{x.Title} ({x.Author.FirstName} {x.Author.LastName})").ToArray();

            return String.Join(Environment.NewLine, books);
        }

        public static int CountBooks(BookShopContext context, int lengthCheck)
        {
            var bookCount = context.Books.Where(b => b.Title.Length > lengthCheck).Count();

            return bookCount;
        }

        public static string CountCopiesByAuthor(BookShopContext context)
        {
            var sb = new StringBuilder();

            var authorBooks = context
                .Authors
                .Include(a => a.Books)
                .Select(a => new
                {
                    AuthorName = $"{a.FirstName} {a.LastName}",
                    BookCopies = a.Books.Sum(b => b.Copies)

                })
                .OrderByDescending(a => a.BookCopies)
                .ToList();

            authorBooks.ForEach(a => sb.AppendLine($"{a.AuthorName} - {a.BookCopies}"));

            return sb.ToString().Trim();
        }

        public static string GetTotalProfitByCategory(BookShopContext context)
        {
            var sb = new StringBuilder();

            var categoryProfits = context
                .Categories
                .Include(c => c.CategoryBooks)
                .ThenInclude(cb => cb.Book)
                .Select(c => new
                {
                    Name = c.Name,
                    Profit = c.CategoryBooks.Sum(b => b.Book.Copies * b.Book.Price)
                })
                .OrderByDescending(c => c.Profit)
                .ThenBy(c => c.Name)
                .ToList();

            categoryProfits.ForEach(c => sb.AppendLine($"{c.Name} ${c.Profit}"));

            return sb.ToString().Trim();
        }

        public static string GetMostRecentBooks(BookShopContext context)
        {
            var sb = new StringBuilder();

            var categories = context
                .Categories
                .Include(c => c.CategoryBooks)
                .ThenInclude(cb => cb.Book)
                .OrderBy(c => c.CategoryBooks.Count)
                .ThenBy(c => c.Name)
                .Select(c => new
                {
                    CategoryName = c.Name,
                    Book = c.CategoryBooks
                        .OrderByDescending(b => b.Book.ReleaseDate)
                        .Take(3)
                        .Select(b => new
                        {
                            BookTitle = b.Book.Title,
                            ReleaseYear = b.Book.ReleaseDate.Value.Year
                        })
                        .ToList()
                })
                .ToList();

            foreach (var category in categories)
            {
                sb.AppendLine($"--{category.CategoryName}");
                category.Book.ForEach(b => sb.AppendLine($"{b.BookTitle} ({b.ReleaseYear})"));
            }

            return sb.ToString().Trim();
        }

        public static void IncreasePrices(BookShopContext context)
        {
            var currentBooks = context
                .Books
                .Where(b => b.ReleaseDate.Value.Year < 2010)
                .ToList();

            currentBooks.ForEach(b => b.Price = b.Price + 5);
            context.SaveChanges();
        }

        public static int RemoveBooks(BookShopContext context)
        {
            var currentBooks = context
                .Books
                .Where(b => b.Copies < 4200)
                .ToList();

            context.Books.RemoveRange(currentBooks);
            context.SaveChanges();
            return currentBooks.Count;
        }
    }
}

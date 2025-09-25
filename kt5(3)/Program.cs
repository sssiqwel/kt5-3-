#nullable disable
using System;
using System.Collections.Generic;

public interface IComparable<T>
{
    int CompareTo(T other);
}
public class Student : IComparable<Student>
{
    public string Name { get; set; }
    public int Age { get; set; }
    public double Grade { get; set; }

    public Student(string name, int age, double grade)
    {
        Name = name;
        Age = age;
        Grade = grade;
    }

    public int CompareTo(Student other)
    {
        if (other == null) return 1;
        int nameComparison = string.Compare(Name, other.Name, StringComparison.OrdinalIgnoreCase);
        if (nameComparison != 0)
            return nameComparison;

        int ageComparison = Age.CompareTo(other.Age);
        if (ageComparison != 0)
            return ageComparison;

        return other.Grade.CompareTo(Grade);
    }

    public override string ToString()
    {
        return $"Student: {Name}, Age: {Age}, Grade: {Grade:F1}";
    }

    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
            return false;

        Student other = (Student)obj;
        return Name == other.Name &&
               Age == other.Age &&
               Grade == other.Grade;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Name, Age, Grade);
    }
}
public class Book : IComparable<Book>
{
    public string Title { get; set; }
    public string Author { get; set; }
    public decimal Price { get; set; }

    public Book(string title, string author, decimal price)
    {
        Title = title;
        Author = author;
        Price = price;
    }

    public int CompareTo(Book other)
    {
        if (other == null) return 1;
        int authorComparison = string.Compare(Author, other.Author, StringComparison.OrdinalIgnoreCase);
        if (authorComparison != 0)
            return authorComparison;

        int titleComparison = string.Compare(Title, other.Title, StringComparison.OrdinalIgnoreCase);
        if (titleComparison != 0)
            return titleComparison;

        return Price.CompareTo(other.Price);
    }

    public override string ToString()
    {
        return $"Book: '{Title}' by {Author}, Price: {Price:C}";
    }

    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
            return false;

        Book other = (Book)obj;
        return Title == other.Title &&
               Author == other.Author &&
               Price == other.Price;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Title, Author, Price);
    }
}
public class StudentGradeComparer : IComparer<Student>
{
    public int Compare(Student x, Student y)
    {
        if (x == null && y == null) return 0;
        if (x == null) return -1;
        if (y == null) return 1;

        return y.Grade.CompareTo(x.Grade);
    }
}

public class StudentAgeComparer : IComparer<Student>
{
    public int Compare(Student x, Student y)
    {
        if (x == null && y == null) return 0;
        if (x == null) return -1;
        if (y == null) return 1;

        return x.Age.CompareTo(y.Age);
    }
}

public class BookPriceComparer : IComparer<Book>
{
    public int Compare(Book x, Book y)
    {
        if (x == null && y == null) return 0;
        if (x == null) return -1;
        if (y == null) return 1;

        return x.Price.CompareTo(y.Price);
    }
}

public class BookTitleComparer : IComparer<Book>
{
    public int Compare(Book x, Book y)
    {
        if (x == null && y == null) return 0;
        if (x == null) return -1;
        if (y == null) return 1;

        return string.Compare(x.Title, y.Title, StringComparison.OrdinalIgnoreCase);
    }
}
class Program
{
    static void Main()
    {
        Console.WriteLine("=== Демонстрация интерфейса IComparable<T> ===\n");

        DemonstrateStudents();
        Console.WriteLine();
        DemonstrateBooks();
        Console.WriteLine();
        DemonstrateCustomComparers();

        Console.WriteLine("\nНажмите любую клавишу для выхода...");
        Console.ReadKey();
    }

    static void DemonstrateStudents()
    {
        Console.WriteLine("=== РАБОТА СО СТУДЕНТАМИ ===");

        var students = new List<Student>
        {
            new Student("Иван", 20, 4.5),
            new Student("Анна", 19, 4.8),
            new Student("иван", 20, 4.2),
            new Student("Анна", 18, 4.7),
            new Student("Петр", 22, 4.9),
            new Student("Анна", 19, 4.6)
        };

        Console.WriteLine("Студенты до сортировки:");
        PrintCollection(students);

        students.Sort();

        Console.WriteLine("\nСтуденты после сортировки:");
        PrintCollection(students);

        Console.WriteLine("\nСравнение студентов:");
        var student1 = new Student("Петр", 21, 4.6);
        var student2 = new Student("Петр", 20, 4.6);

        int result = student1.CompareTo(student2);
        Console.WriteLine($"{student1} vs {student2}");
        Console.WriteLine($"Результат: {result} ({GetComparisonDescription(result)})");
    }

    static void DemonstrateBooks()
    {
        Console.WriteLine("\n=== РАБОТА С КНИГАМИ ===");

        var books = new List<Book>
        {
            new Book("Преступление и наказание", "Достоевский", 500),
            new Book("Война и мир", "Толстой", 800),
            new Book("Идиот", "Достоевский", 450),
            new Book("Анна Каренина", "Толстой", 700),
            new Book("Братья Карамазовы", "Достоевский", 600)
        };

        Console.WriteLine("Книги до сортировки:");
        PrintCollection(books);

        books.Sort();

        Console.WriteLine("\nКниги после сортировки:");
        PrintCollection(books);

        Console.WriteLine("\nСравнение книг:");
        var book1 = new Book("Идиот", "Достоевский", 500);
        var book2 = new Book("Идиот", "Достоевский", 450);

        int result = book1.CompareTo(book2);
        Console.WriteLine($"{book1} vs {book2}");
        Console.WriteLine($"Результат: {result} ({GetComparisonDescription(result)})");
    }

    static void DemonstrateCustomComparers()
    {
        Console.WriteLine("\n=== ДОПОЛНИТЕЛЬНЫЕ КОМПАРАТОРЫ ===");

        var students = new List<Student>
        {
            new Student("Иван", 20, 4.5),
            new Student("Анна", 19, 4.8),
            new Student("Петр", 22, 4.3)
        };

        var books = new List<Book>
        {
            new Book("Преступление и наказание", "Достоевский", 500),
            new Book("Война и мир", "Толстой", 800),
            new Book("Идиот", "Достоевский", 450)
        };

        Console.WriteLine("Студенты по оценке (убывание):");
        students.Sort(new StudentGradeComparer());
        PrintCollection(students);

        Console.WriteLine("\nСтуденты по возрасту (возрастание):");
        students.Sort(new StudentAgeComparer());
        PrintCollection(students);

        Console.WriteLine("\nКниги по цене (возрастание):");
        books.Sort(new BookPriceComparer());
        PrintCollection(books);

        Console.WriteLine("\nКниги по названию:");
        books.Sort(new BookTitleComparer());
        PrintCollection(books);
    }

    static void PrintCollection<T>(IEnumerable<T> collection)
    {
        int index = 1;
        foreach (var item in collection)
        {
            Console.WriteLine($"{index}. {item}");
            index++;
        }
    }

    static string GetComparisonDescription(int result)
    {
        if (result < 0)
            return "первый объект меньше второго";
        else if (result == 0)
            return "объекты равны";
        else
            return "первый объект больше второго";
    }
}
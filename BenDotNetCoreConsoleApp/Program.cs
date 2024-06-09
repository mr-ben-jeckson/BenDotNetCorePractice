//// See https://aka.ms/new-console-template for more information
//// Output Test
using System.Data;
using System.Data.SqlClient;

//Console.WriteLine("Hello, World!");
//// DB Connection
//SqlConnection connection = new SqlConnection("Data Source=.;Initial Catalog=BenDotNetCore;User ID=sa;Password=sasa@123");
//Console.WriteLine("Connection openning");
//connection.Open();
//Console.WriteLine("Connection Opend");
//Console.WriteLine("Connection Closing");
//connection.Close();
//Console.WriteLine("Connection Closed");
//// Variable Names and Day Types
//string product = "Apple Fruit";
//int price = 115;
//bool userStatus = true;
//char code = 'A'; // Ask
//long porpulation = 123333333333390877;
//Console.WriteLine(product + " price is " + price + " Category - " + code);

//// Conditions
//if(userStatus ==  true)
//{
//    Console.WriteLine("Myanmar Porpulation is " + porpulation);
//} else
//{
//    Console.WriteLine("You cannot access to view this");
//}

//// Shorthand
//var number = 13;
//bool checker = (number > 12 ) ? true : false;
//Console.WriteLine(checker);
//string text = "a"; // Ask

//switch (text)
//{
//    case "a": 
//        Console.WriteLine("You are lucky");
//        break;
//    case "b": 
//        Console.WriteLine("You are Unlucky");
//        break ;
//    default:
//        Console.WriteLine("Good bye");
//        break;
//}

//string[] brands = { "Apple", "Sansaumg", "Google", "LG" };
//foreach (string i in brands)
//{
//    Console.WriteLine(i);
//}
//Console.WriteLine("Enter your age:");
//int age = Convert.ToInt32(Console.ReadLine());

//if (age > 20)
//{
//    Console.WriteLine("You are adult guy");
//} else if (age < 20 && age > 10)
//{
//    Console.WriteLine("You are teenager");
//} else
//{
//    Console.WriteLine("Invalid Data");
//}


SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder();
sqlConnectionStringBuilder.DataSource = ".";
sqlConnectionStringBuilder.InitialCatalog = "BenDotNetCore";
sqlConnectionStringBuilder.UserID = "sa";
sqlConnectionStringBuilder.Password = "sasa@123";

string connectionString = sqlConnectionStringBuilder.ConnectionString;
SqlConnection connection = new SqlConnection(connectionString);
connection.Open();

//Create

string query = @"INSERT INTO [dbo].[TblBlogs] VALUES (
    'Author Name',
    'Title Name',
    'Blog Content Bla Bla Bla'
)";

SqlCommand cmdCreate = new SqlCommand(query, connection);
int result = cmdCreate.ExecuteNonQuery();
string message = result > 0 ? "Saving Success" : "Saving Failed";
Console.WriteLine(message);



// Read
SqlCommand cmdRead = new SqlCommand("SELECT * FROM TblBlogs", connection);
SqlDataAdapter adapter = new SqlDataAdapter(cmdRead);
DataTable dt = new DataTable();
adapter.Fill(dt);

connection.Close();
foreach (DataRow dr in dt.Rows)
{
    Console.WriteLine(dr["BlogId"]);
    Console.WriteLine(dr["BlogAuthor"]);
    Console.WriteLine(dr["BlogTitle"]);
    Console.WriteLine(dr["BlogContent"]);
    Console.WriteLine("------------------");
}
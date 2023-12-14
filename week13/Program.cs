using System.Data.SQLite;
using System.Xml.Serialization;

ReadData(CreateConnection());
//AddCustomer(CreateConnection());
//RemoveCustomer(CreateConnection());

static SQLiteConnection CreateConnection()
{
    SQLiteConnection connection = new SQLiteConnection("Data Source=db.db; Version=3; New=true; Compress=True;");
    try
    {
        connection.Open();
        Console.WriteLine("Connected");
    }
    catch
    {
        Console.WriteLine("Failed");
    }
    return connection;
}

static void ReadData(SQLiteConnection MyConnection)
{
    SQLiteDataReader read;
    SQLiteCommand command;
    command = MyConnection.CreateCommand();
    command.CommandText = "SELECT * FROM customer";

    read = command.ExecuteReader();
    while (read.Read())
    {
        string fName = read.GetString(0);
        string lName = read.GetString(1);
        string dob = read.GetString(2);

        Console.WriteLine($"Full name: {fName} {lName}; DoB: {dob}");
    }
    MyConnection.Close();
}

static void AddCustomer(SQLiteConnection MyConnection)
{
    SQLiteCommand command;

    string fName = "Harry";
    string lName = "Potter";
    string dob = "07-31-1980";

    command = MyConnection.CreateCommand();
    command.CommandText = $"INSERT INTO customer(firstName, lastName, dateOfBirth) VALUES('{fName}','{lName}','{dob}')";

    int rowInserted = command.ExecuteNonQuery();

    Console.Clear();
    Console.WriteLine($"Rows inserted: {rowInserted}");

    ReadData(MyConnection);

    MyConnection.Close();
}

static void RemoveCustomer(SQLiteConnection MyConnection)
{
    SQLiteCommand command;

    string idToDelete = "8";

    command = MyConnection.CreateCommand();
    command.CommandText = $"DELETE FROM customer Where rowid = {idToDelete}";

    int rowDeleted = command.ExecuteNonQuery();

    Console.Clear();
    Console.WriteLine($"Rows deleted: {rowDeleted}");
    ReadData(MyConnection);

    MyConnection.Close();
}


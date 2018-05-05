using System.Collections.Generic;
using MySql.Data.MySqlClient;
using HairSalonApp;
using System;

namespace HairSalonApp
{
  public class Client
  {
    private int _id;
    private string _name;
    private int _stylistId;
    private string _phoneNumber;
    private string _dateOfBirth;
    private string _notes;

    public Client(string name, string phone, string birth, string notes, int stylistId)
    {
        _name = name;
        _phoneNumber = phone;
        _dateOfBirth = birth;
        _notes = notes;
        _stylistId = stylistId;
    }

    public override bool Equals(System.Object otherClient)
    {
        if (!(otherClient is Client))
        {
            return false;
        }
        else
        {
            Client newClient = (Client) otherClient;
            bool idEquality = this.GetId() == newClient.GetId();
            bool nameEquality = this.GetName() == newClient.GetName();
            bool phoneEquality = this.GetPhone() == newClient.GetPhone();
            bool dateOfBirthEquality = this.GetDateOfBirth() == newClient.GetDateOfBirth();
            bool notesEquality = this.GetNotes() == newClient.GetNotes();
            bool stylistIdEquality = this.GetStylistId() == newClient.GetStylistId();
            return (idEquality && nameEquality && phoneEquality && dateOfBirthEquality &&  notesEquality && stylistIdEquality);
        }
    }

    public override int GetHashCode()
    {
         return this.GetName().GetHashCode();
    }

    public void SetId(int newId)
    {
        _id = newId;
    }

    public void SetName(string newName)
    {
        _name = newName;
    }

    public void SetDateOfBirth(string newDate)
    {
        _dateOfBirth = newDate;
    }

    public void SetNotes(string newNotes)
    {
        _notes = newNotes;
    }

    public void SetStylistId(int newId)
    {
        _stylistId = newId;
    }

    public int GetId()
    {
        return _id;
    }

    public string GetName()
    {
        return _name;
    }

    public string GetPhone()
    {
        return _phoneNumber;
    }

    public string GetDateOfBirth()
    {
        return _dateOfBirth;
    }

    public string GetNotes()
    {
        return _notes;
    }

    public int GetStylistId()
    {
        return _stylistId;
    }

    public void Save()
    {
        MySqlConnection conn = DB.Connection();
        conn.Open();

        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"INSERT INTO `clients` (`name`, `stylistID`, `phone_number`, `dateOfBirth`, `notes`) VALUES (@ClientName, @ClientStylistId, @ClientPhone, @ClientDateOfBirth, @ClientNotes);";

        MySqlParameter name = new MySqlParameter();
        name.ParameterName = "@ClientName";
        name.Value = this._name;
        cmd.Parameters.Add(name);

        MySqlParameter phone = new MySqlParameter();
        phone.ParameterName = "@ClientPhone";
        phone.Value = this._phoneNumber;
        cmd.Parameters.Add(phone);

        MySqlParameter stylistId = new MySqlParameter();
        stylistId.ParameterName = "@ClientStylistId";
        stylistId.Value = this._stylistId;
        cmd.Parameters.Add(stylistId);

        MySqlParameter dateOfBirth = new MySqlParameter();
        dateOfBirth.ParameterName = "@ClientDateOfBirth";
        dateOfBirth.Value = this._dateOfBirth;
        cmd.Parameters.Add(dateOfBirth);

        MySqlParameter notes = new MySqlParameter();
        notes.ParameterName = "@ClientNotes";
        notes.Value = this._notes;
        cmd.Parameters.Add(notes);

        cmd.ExecuteNonQuery();
        _id = (int) cmd.LastInsertedId;

        conn.Close();
        if (conn != null)
        {
            conn.Dispose();
        }
    }

    public static List<Client> GetAllClients()
    {
        List<Client> allClients = new List<Client> {};
        MySqlConnection conn = DB.Connection();
        conn.Open();
        MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"SELECT * FROM clients ORDER BY name ASC;";
        MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
        while(rdr.Read())
        {
            int id = rdr.GetInt32(0);
            string name = rdr.GetString(1);
            int stylistId = rdr.GetInt32(2);
            string phone = rdr.GetString(3);
            string dateOfBirth = rdr.GetString(4);
            string notes = rdr.GetString(5);

            Client newClient = new Client(name, phone, dateOfBirth, notes, stylistId);
            newClient.SetId(id);
            allClients.Add(newClient);
        }
        conn.Close();
        if (conn != null)
        {
            conn.Dispose();
        }

        return allClients;
    }

    public static List<Client> GetThisStylistClients(int stylistId)
    {
        List<Client> allClients = new List<Client> {};
        MySqlConnection conn = DB.Connection();
        conn.Open();
        MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"SELECT * FROM clients WHERE stylistID = @thisId ORDER BY name ASC;";

        MySqlParameter thisId = new MySqlParameter();
        thisId.ParameterName = "@thisId";
        thisId.Value = stylistId;
        cmd.Parameters.Add(thisId);

        MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
        while(rdr.Read())
        {
            int id = rdr.GetInt32(0);
            string name = rdr.GetString(1);
            string phone = rdr.GetString(3);
            string dateOfBirth = rdr.GetString(4);
            string notes = rdr.GetString(5);

            Client newClient = new Client(name, phone, dateOfBirth, notes, stylistId);
            newClient.SetId(id);
            allClients.Add(newClient);
        }
        conn.Close();
        if (conn != null)
        {
            conn.Dispose();
        }

        return allClients;
    }

    public static Client Find(int id)
    {
        MySqlConnection conn = DB.Connection();
        conn.Open();

        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"SELECT * FROM `clients` WHERE id = @thisId;";

        MySqlParameter thisId = new MySqlParameter();
        thisId.ParameterName = "@thisId";
        thisId.Value = id;
        cmd.Parameters.Add(thisId);

        var rdr = cmd.ExecuteReader() as MySqlDataReader;

        int clientId = 0;
        string name = "";
        int stylistId = 0;
        string phone = "";
        string dateOfBirth = "";
        string notes = "";

        while (rdr.Read())
        {
            clientId = rdr.GetInt32(0);
            name = rdr.GetString(1);
            stylistId = rdr.GetInt32(2);
            phone = rdr.GetString(3);
            dateOfBirth = rdr.GetString(4);
            notes = rdr.GetString(5);
        }

        Client foundClient = new Client(name, phone, dateOfBirth, notes, stylistId);
        foundClient.SetId(clientId);

        conn.Close();
        if (conn != null)
        {
            conn.Dispose();
        }

        return foundClient;
    }

    public static void DeleteClient(int id)
    {
        MySqlConnection conn = DB.Connection();
        conn.Open();
        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"DELETE FROM clients WHERE id = @thisId;";

        MySqlParameter thisId = new MySqlParameter();
        thisId.ParameterName = "@thisId";
        thisId.Value = id;
        cmd.Parameters.Add(thisId);

        cmd.ExecuteNonQuery();

        conn.Close();
        if (conn != null)
        {
            conn.Dispose();
        }
    }

    public static void DeleteThisStylistClients(int stylistId)
    {
        MySqlConnection conn = DB.Connection();
        conn.Open();
        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"DELETE FROM clients WHERE stylistID = @thisId;";

        MySqlParameter thisId = new MySqlParameter();
        thisId.ParameterName = "@thisId";
        thisId.Value = stylistId;
        cmd.Parameters.Add(thisId);

        cmd.ExecuteNonQuery();

        conn.Close();
        if (conn != null)
        {
            conn.Dispose();
        }
    }

    public static void DeleteAll()
    {
        MySqlConnection conn = DB.Connection();
        conn.Open();

        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"DELETE FROM clients;";

        cmd.ExecuteNonQuery();

        conn.Close();
        if (conn != null)
        {
            conn.Dispose();
        }
    }
  }
}

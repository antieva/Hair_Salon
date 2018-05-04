using System.Collections.Generic;
using MySql.Data.MySqlClient;
using HairSalonApp;
using System;

namespace HairSalonApp
{
  public class Stylist
  {
      private int _id;
      private string _name;
      private string _phoneNumber;
      private string _specialization;
      private string _workingDays;
      private string _time;

      public Stylist(string name, string phoneNumber, string workingDays, string time, string specialization = "universal")
      {
          _name = name;
          _phoneNumber = phoneNumber;
          _specialization = specialization;
          _workingDays = workingDays;
          _time = time;
      }

      public override bool Equals(System.Object otherStylist)
      {
          if (!(otherStylist is Stylist))
          {
              return false;
          }
          else
          {
              Stylist newStylist = (Stylist) otherStylist;
              bool idEquality = this.GetId() == newStylist.GetId();
              bool nameEquality = this.GetName() == newStylist.GetName();
              bool phoneEquality = this.GetPhone() == newStylist.GetPhone();
              bool specializationEquality = this.GetSpecialization() == newStylist.GetSpecialization();
              bool workingDaysEquality = this.GetWorkingDays() == newStylist.GetWorkingDays();
              bool timeEquality = this.GetTime() == newStylist.GetTime();
              return (idEquality && nameEquality && phoneEquality && specializationEquality &&  workingDaysEquality && timeEquality);
          }
      }

      public override int GetHashCode()
      {
           return this.GetName().GetHashCode();
      }

      public void SetId(int newID)
      {
          _id = newID;
      }

      public void SetName(string newName)
      {
          _name = newName;
      }

      public void SetPhone(string newPhoneNumber)
      {
          _phoneNumber = newPhoneNumber;
      }

      public void SetSpecializations(string newSpecialization)
      {
          _specialization = newSpecialization;
      }

      public void SetWorkingDays(string newDays)
      {
          _workingDays = newDays;
      }

      public void SetTime(string newTime)
      {
          _time = newTime;
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

      public string GetSpecialization()
      {
          return _specialization;
      }

      public string GetWorkingDays()
      {
          return _workingDays;
      }

      public string GetTime()
      {
          return _time;
      }

      public void Save()
      {
          MySqlConnection conn = DB.Connection();
          conn.Open();

          var cmd = conn.CreateCommand() as MySqlCommand;
          cmd.CommandText = @"INSERT INTO `stylists` (`name`, `phone_number`, `specialization`, `working_days`, `time`) VALUES (@StylistName, @StylistPhone, @StylistSpecialization, @StylistWorkingDays, @StylistTime);";

          MySqlParameter name = new MySqlParameter();
          name.ParameterName = "@StylistName";
          name.Value = this._name;
          cmd.Parameters.Add(name);

          MySqlParameter phone = new MySqlParameter();
          phone.ParameterName = "@StylistPhone";
          phone.Value = this._phoneNumber;
          cmd.Parameters.Add(phone);

          MySqlParameter specialization = new MySqlParameter();
          specialization.ParameterName = "@StylistSpecialization";
          specialization.Value = this._specialization;
          cmd.Parameters.Add(specialization);

          MySqlParameter workingDays = new MySqlParameter();
          workingDays.ParameterName = "@StylistWorkingDays";
          workingDays.Value = this._workingDays;
          cmd.Parameters.Add(workingDays);

          MySqlParameter time = new MySqlParameter();
          time.ParameterName = "@StylistTime";
          time.Value = this._time;
          cmd.Parameters.Add(time);

          cmd.ExecuteNonQuery();
          _id = (int) cmd.LastInsertedId;

          conn.Close();
          if (conn != null)
          {
              conn.Dispose();
          }
      }

      public static List<Stylist> GetAllStylists()
      {
          List<Stylist> allStylists = new List<Stylist> {};
          MySqlConnection conn = DB.Connection();
          conn.Open();
          MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
          cmd.CommandText = @"SELECT * FROM stylists ORDER BY name ASC;";
          MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
          while(rdr.Read())
          {
              int id = rdr.GetInt32(0);
              string name = rdr.GetString(1);
              string phoneNumber = rdr.GetString(2);
              string specialization = rdr.GetString(3);
              string workingDays = rdr.GetString(4);
              string time = rdr.GetString(5);
              Stylist newStylist = new Stylist(name, phoneNumber, workingDays, time, specialization);
              newStylist.SetId(id);
              allStylists.Add(newStylist);
          }
          conn.Close();
          if (conn != null)
          {
              conn.Dispose();
          }
          return allStylists;
      }

      public static Stylist Find(int id)
      {
          MySqlConnection conn = DB.Connection();
          conn.Open();

          var cmd = conn.CreateCommand() as MySqlCommand;
          cmd.CommandText = @"SELECT * FROM `stylists` WHERE id = @thisId;";

          MySqlParameter thisId = new MySqlParameter();
          thisId.ParameterName = "@thisId";
          thisId.Value = id;
          cmd.Parameters.Add(thisId);

          var rdr = cmd.ExecuteReader() as MySqlDataReader;

          int stylistId = 0;
          string name = "";
          string specialization = "";
          string phone = "";
          string workingDays = "";
          string time = "";

          while (rdr.Read())
          {
              stylistId = rdr.GetInt32(0);
              name = rdr.GetString(1);
              phone = rdr.GetString(2);
              specialization = rdr.GetString(3);
              workingDays = rdr.GetString(4);
              time = rdr.GetString(5);
          }

          Stylist foundStylist = new Stylist(name, phone, workingDays, time, specialization);
          foundStylist.SetId(stylistId);

          conn.Close();
          if (conn != null)
          {
              conn.Dispose();
          }

          return foundStylist;
      }

      public static void DeleteStylist(int id)
      {
          MySqlConnection conn = DB.Connection();
          conn.Open();
          var cmd = conn.CreateCommand() as MySqlCommand;
          cmd.CommandText = @"DELETE FROM stylists WHERE id = @thisId;";

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

      public static void DeleteAll()
      {
          MySqlConnection conn = DB.Connection();
          conn.Open();

          var cmd = conn.CreateCommand() as MySqlCommand;
          cmd.CommandText = @"DELETE FROM stylists;";

          cmd.ExecuteNonQuery();

          conn.Close();
          if (conn != null)
          {
              conn.Dispose();
          }
      }
  }
}

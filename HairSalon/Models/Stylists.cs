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

      public Stylist(int id, string name, string phoneNumber, string specialization, string workingDays, string time)
      {
          _id = id;
          _name = name;
          _phoneNumber = phoneNumber;
          _specialization = specialization;
          _workingDays = workingDays;
          _time = time;
      }

      public void SetID(int newID)
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

      public int GetID()
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
              Stylist newStylist = new Stylist(id, name, phoneNumber, specialization, workingDays, time);
              allStylists.Add(newStylist);
          }
          conn.Close();
          if (conn != null)
          {
              conn.Dispose();
          }
          return allStylists;
      }

      public static void DeleteAll()
      {
        
      }
  }
}

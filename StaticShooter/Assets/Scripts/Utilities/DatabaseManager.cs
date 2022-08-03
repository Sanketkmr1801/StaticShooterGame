using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;
using System.IO;

public class DatabaseManager : MonoBehaviour
{
    private Dictionary<string, float> _scores = new Dictionary<string, float>();
    private string _dbName = "URI=file:Inventory.db";
    void Start() {
        createDb("scores");
    }

    public void createDb(string dbName) {
        string query = "CREATE TABLE IF NOT EXISTS " + dbName + " (name VARCHAR(20), integerValue INT);";
        executeQuery(query);
    }

    public void addScore(string playerName, float score) {
        string query = "INSERT INTO scores (name, integerValue) VALUES " + "('" + playerName + "', '" + score + "')";
        executeQuery(query);
    }

    public void addWeapon(string weaponName, int weaponDamage) {
        string query = "INSERT INTO weapons (name, integerValue) VALUES " + "('" + weaponName + "', '" + weaponDamage + "')";
        executeQuery(query);
    }



    public void executeQuery(string query) {
        using (var connection = new SqliteConnection(_dbName)) {
            connection.Open();
            using(var command = connection.CreateCommand()) {
                command.CommandText = query;
                command.ExecuteNonQuery();
            }

            connection.Close();
        }
    }

    public void displayScores() {
        using (var connection = new SqliteConnection(_dbName)) {
            connection.Open();
            using(var command = connection.CreateCommand()) {
                command.CommandText = "SELECT * FROM scores;";
                using(IDataReader reader = command.ExecuteReader()) {

                    while(reader.Read()) {
                        Debug.Log("Name: " + reader["name"] + " score: " + reader["integerValue"]);
                    }

                    reader.Close();
                }
            }

            connection.Close();
        }
    }

    public void displayWeapons() {
        using (var connection = new SqliteConnection(_dbName)) {
            connection.Open();
            using(var command = connection.CreateCommand()) {
                command.CommandText = "SELECT * FROM WEAPONS;";
                using(IDataReader reader = command.ExecuteReader()) {

                    while(reader.Read()) {
                        Debug.Log("Name: " + reader["name"] + "Damage: " + reader["integerValue"]);
                    }

                    reader.Close();
                }
            }

            connection.Close();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using System.IO;
using System;

public class DatabaseDataAcces : MonoBehaviour
{
    
    public static List<Player> getAllPlayers()
    {
        List<Player> sol = new List<Player>();

        IDbCommand dbCommand = DatabaseConnection.connection.CreateCommand();

        string query = "select * from players";

        dbCommand.CommandText = query;
        IDataReader reader = dbCommand.ExecuteReader();

        while (reader.Read())
        {

            Player player = new Player();
            player.id = reader.GetInt32(0);
            player.nickname = reader.GetString(1);
            player.firstName = reader.GetString(2);
            player.lastName = reader.GetString(3);
            player.age = reader.GetInt32(4);

            sol.Add(player); 

        }

        reader.Close();

        return sol;
    }

    public static Player getPlayerWithNickname(string nickname)
    {
        Player player = null;

        SqliteCommand command = new SqliteCommand("select * from players where nickname=@nickname", DatabaseConnection.connection);

        command.Parameters.Add(new SqliteParameter("@nickname", nickname));

        IDataReader reader = command.ExecuteReader();

        if (reader.Read())
        {
            Debug.Log("USAO");
            player = new Player();
            player.id = reader.GetInt32(0);
            player.nickname = reader.GetString(1);
            player.firstName = reader.GetString(2);
            player.lastName = reader.GetString(3);
            player.age = reader.GetInt32(4);

        }

        reader.Close();

        return player;
    }

    public static void insertPlayer(Player p)
    {

        SqliteCommand command = new SqliteCommand("insert into players(nickname, first_name, last_name, age) VALUES (@nickname,@first,@last,@age)", DatabaseConnection.connection);

        command.Parameters.Add(new SqliteParameter("@nickname", p.nickname));
        command.Parameters.Add(new SqliteParameter("@first", p.firstName));
        command.Parameters.Add(new SqliteParameter("@last", p.lastName));
        command.Parameters.Add(new SqliteParameter("@age", p.age));

        command.ExecuteNonQuery();

    }

    public static List<Box> getPlayerBoxes(int id)
    {
        List<Box> boxes = new List<Box>();

        SqliteCommand command = new SqliteCommand("select * from boxes where player_id=@id AND opened = 0", DatabaseConnection.connection);

        command.Parameters.Add(new SqliteParameter("@id", id));

        IDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            Box box = new Box();
            box.id = reader.GetInt32(0);
            box.playerId = reader.GetInt32(1);
            box.acquired = reader.GetInt64(2);
            box.opened = reader.GetInt32(3);

            boxes.Add(box);

        }

        reader.Close();

        return boxes;
    }

    public static CarPart getCarPart(int id)
    {
        CarPart carPart = null;

        SqliteCommand command = new SqliteCommand("select * from car_parts where id = @id", DatabaseConnection.connection);

        command.Parameters.Add(new SqliteParameter("@id", id));

        IDataReader reader = command.ExecuteReader();

        if (reader.Read())
        {

            carPart = new CarPart();

            carPart.id = reader.GetInt32(0);
            carPart.type = reader.GetInt32(1);
            carPart.health = reader.GetInt32(2);
            carPart.power = reader.GetInt32(3);
            carPart.energy = reader.GetInt32(4);

        }

        reader.Close();

        return carPart;

    }

    public static int getHasCarPartId(int id)
    {
        int ret = -1;

        SqliteCommand command = new SqliteCommand("select * from has_car_part where id = @id", DatabaseConnection.connection);

        command.Parameters.Add(new SqliteParameter("@id", id));

        IDataReader reader = command.ExecuteReader();

        if (reader.Read())
        {

            ret = reader.GetInt32(2);

        }

        reader.Close();

        return ret;

    }

    public static Car getPlayersCar(int playerId)
    {

        Car car = null;

        SqliteCommand command = new SqliteCommand("select * from player_car where player_id = @id", DatabaseConnection.connection);

        command.Parameters.Add(new SqliteParameter("@id", playerId));

        IDataReader reader = command.ExecuteReader();

        if (reader.Read())
        {

            car = new Car();
            
            // chasiss
            if (!reader.IsDBNull(1))
            {
                int id = reader.GetInt32(1);
                int partId = getHasCarPartId(id);
                car.chassis = getCarPart(partId);

            }
            else
            {
                car.chassis = null;
            }

            // attack1
            if (!reader.IsDBNull(2))
            {
                int id = reader.GetInt32(2);
                int partId = getHasCarPartId(id);
                car.attack1 = getCarPart(partId);

            }
            else
            {
                car.attack1 = null;
            }

            if (!reader.IsDBNull(3))
            {
                int id = reader.GetInt32(3);
                int partId = getHasCarPartId(id);
                car.frontWheel = getCarPart(partId);

            }
            else
            {
                car.frontWheel = null;
            }

            if (!reader.IsDBNull(4))
            {
                int id = reader.GetInt32(4);
                int partId = getHasCarPartId(id);
                car.backWheel = getCarPart(partId);

            }
            else
            {
                car.backWheel = null;
            }

            if (!reader.IsDBNull(5))
            {
                int id = reader.GetInt32(5);
                int partId = getHasCarPartId(id);
                car.forklift = getCarPart(partId);

            }
            else
            {
                car.forklift = null;
            }

            if (!reader.IsDBNull(6))
            {
                int id = reader.GetInt32(6);
                int partId = getHasCarPartId(id);
                car.attack2 = getCarPart(partId);

            }
            else
            {
                car.attack2 = null;
            }
            
        }

        reader.Close();

        return car;
    }

}

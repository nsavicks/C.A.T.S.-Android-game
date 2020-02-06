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

    public static Player getPlayerWithId(int id)
    {
        Player player = null;

        SqliteCommand command = new SqliteCommand("select * from players where id=@id", DatabaseConnection.connection);

        command.Parameters.Add(new SqliteParameter("@id", id));

        IDataReader reader = command.ExecuteReader();

        if (reader.Read())
        {
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

    public static void OpenBox(int id)
    {
        SqliteCommand command = new SqliteCommand("UPDATE boxes SET opened=1 WHERE id=@id", DatabaseConnection.connection);

        command.Parameters.Add(new SqliteParameter("@id", id));

        command.ExecuteNonQuery();
    }

    public static int InsertHasCarPart(int playerId, int partId, int stars)
    {
        SqliteCommand command = new SqliteCommand("insert into has_car_part(playerId, partId, stars) VALUES(@playerId, @partId, @stars)", DatabaseConnection.connection);

        command.Parameters.Add(new SqliteParameter("@playerId", playerId));
        command.Parameters.Add(new SqliteParameter("@partId", partId));
        command.Parameters.Add(new SqliteParameter("@stars", stars));

        command.ExecuteNonQuery();

        command.CommandText = "select last_insert_rowid()";
        Int64 LastRowID64 = (Int64)command.ExecuteScalar();

        // Then grab the bottom 32-bits as the unique ID of the row.
        //
        return (int)LastRowID64;
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

    public static HasCarPart getHasCarPart(int id)
    {
        HasCarPart hasCarPart = null;

        SqliteCommand command = new SqliteCommand("select * from has_car_part where id = @id", DatabaseConnection.connection);

        command.Parameters.Add(new SqliteParameter("@id", id));

        IDataReader reader = command.ExecuteReader();

        if (reader.Read())
        {
            hasCarPart = new HasCarPart();
            hasCarPart.id = reader.GetInt32(0);
            hasCarPart.playerId = reader.GetInt32(1);
            hasCarPart.carPart = DatabaseDataAcces.getCarPart(reader.GetInt32(2));
            hasCarPart.stars = reader.GetInt32(3);

        }

        reader.Close();

        return hasCarPart;

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
                HasCarPart hasCarPart = getHasCarPart(id);
                car.chassis = hasCarPart.carPart;
                car.chassisStars = hasCarPart.stars;

            }
            else
            {
                car.chassis = null;
            }

            // attack1
            if (!reader.IsDBNull(2))
            {
                int id = reader.GetInt32(2);
                HasCarPart hasCarPart = getHasCarPart(id);
                car.attack1 = hasCarPart.carPart;
                car.attack1Stars = hasCarPart.stars;

            }
            else
            {
                car.attack1 = null;
            }

            if (!reader.IsDBNull(3))
            {
                int id = reader.GetInt32(3);
                HasCarPart hasCarPart = getHasCarPart(id);
                car.frontWheel = hasCarPart.carPart;
                car.frontWheelStars = hasCarPart.stars;

            }
            else
            {
                car.frontWheel = null;
            }

            if (!reader.IsDBNull(4))
            {
                int id = reader.GetInt32(4);
                HasCarPart hasCarPart = getHasCarPart(id);
                car.backWheel = hasCarPart.carPart;
                car.backWheelStars = hasCarPart.stars;

            }
            else
            {
                car.backWheel = null;
            }

            if (!reader.IsDBNull(5))
            {
                int id = reader.GetInt32(5);
                HasCarPart hasCarPart = getHasCarPart(id);
                car.forklift = hasCarPart.carPart;
                car.forkliftStars = hasCarPart.stars;

            }
            else
            {
                car.forklift = null;
            }

            if (!reader.IsDBNull(6))
            {
                int id = reader.GetInt32(6);
                HasCarPart hasCarPart = getHasCarPart(id);
                car.attack2 = hasCarPart.carPart;
                car.attack2Stars = hasCarPart.stars;

            }
            else
            {
                car.attack2 = null;
            }

        }

        reader.Close();

        return car;
    }

    public static void InsertPlayerCar(int playerId)
    {
        SqliteCommand command = new SqliteCommand("insert into player_car (player_id, chassis_id, attack1_id, frontwheel_id, backwheel_id, forklift_id, attack2_id) VALUES(@playerId, NULL, NULL, NULL, NULL, NULL, NULL) ", DatabaseConnection.connection);

        command.Parameters.Add(new SqliteParameter("@playerId", playerId));

        command.ExecuteNonQuery();
    }

    public static List<GamePlayed> getGamesPlayed(int playerId)
    {

        List<GamePlayed> games = new List<GamePlayed>();

        SqliteCommand command = new SqliteCommand("select * from game_results where first_player = @id", DatabaseConnection.connection);

        command.Parameters.Add(new SqliteParameter("@id", playerId));

        IDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            GamePlayed game = new GamePlayed();

            game.id = reader.GetInt32(0);
            game.firstPlayer = getPlayerWithId(reader.GetInt32(1));
            game.secondPlayer = getPlayerWithId(reader.GetInt32(2));
            game.winner = reader.GetInt32(3);
            game.datetime = new DateTime(reader.GetInt64(4));

            games.Add(game);

        }

        reader.Close();

        return games;

    }

    public static List<HasCarPart> getCarPartsForUser(int playerId)
    {
        List<HasCarPart> parts = new List<HasCarPart>();

        SqliteCommand command = new SqliteCommand(@"select *
        from has_car_part hc, player_car pc
        where pc.player_id = hc.playerId
        AND pc.player_id = @id
        AND(pc.chassis_id IS NULL OR hc.id <> pc.chassis_id)
        AND(pc.attack1_id IS NULL OR hc.id <> pc.attack1_id)
        AND(pc.attack2_id IS NULL OR hc.id <> pc.attack2_id)
        AND(pc.forklift_id IS NULL OR hc.id <> pc.forklift_id)
        AND(pc.frontwheel_id  IS NULL OR hc.id <> pc.frontwheel_id)
        AND(pc.backwheel_id IS NULL OR hc.id <> pc.backwheel_id)", DatabaseConnection.connection);

        command.Parameters.Add(new SqliteParameter("@id", playerId));

        IDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            HasCarPart hasCarPart = new HasCarPart();

            hasCarPart.id = reader.GetInt32(0);
            hasCarPart.playerId = playerId;
            hasCarPart.carPart = getCarPart(reader.GetInt32(2));
            hasCarPart.stars = reader.GetInt32(3);

            parts.Add(hasCarPart);

        }

        reader.Close();

        return parts;
    }

    public static void changePart(HasCarPart hasCarPart, int subtype)
    {
        SqliteCommand command = null;


        switch (hasCarPart.carPart.type)
        {
            case 1:
                command = new SqliteCommand("UPDATE player_car SET chassis_id = @partId WHERE player_id = @playerId", DatabaseConnection.connection);
                break;
            case 2:
                if (subtype == 1)
                {
                    command = new SqliteCommand("UPDATE player_car SET frontwheel_id = @partId WHERE player_id = @playerId", DatabaseConnection.connection);
                }
                else
                {
                    command = new SqliteCommand("UPDATE player_car SET backwheel_id = @partId WHERE player_id = @playerId", DatabaseConnection.connection);
                }
                break;
            case 3:
                command = new SqliteCommand("UPDATE player_car SET forklift_id = @partId WHERE player_id = @playerId", DatabaseConnection.connection);
                break;
            case 4:
                if (subtype == 1)
                {
                    command = new SqliteCommand("UPDATE player_car SET attack1_id = @partId WHERE player_id = @playerId", DatabaseConnection.connection);
                }
                else
                {
                    command = new SqliteCommand("UPDATE player_car SET attack2_id = @partId WHERE player_id = @playerId", DatabaseConnection.connection);
                }
                break;
        }

        command.Parameters.Add(new SqliteParameter("@playerId", hasCarPart.playerId));
        command.Parameters.Add(new SqliteParameter("@partId", hasCarPart.id));

        command.ExecuteNonQuery();
    }

    public static void removeItem(int playerId, int type)
    {
        SqliteCommand command = null;

        switch (type)
        {
            case 1:
                command = new SqliteCommand("UPDATE player_car SET attack1_id = NULL WHERE player_id = @playerId", DatabaseConnection.connection);
                break;
            case 2:
                command = new SqliteCommand("UPDATE player_car SET attack2_id = NULL WHERE player_id = @playerId", DatabaseConnection.connection);
                break;
            case 3:
                command = new SqliteCommand("UPDATE player_car SET forklift_id = NULL WHERE player_id = @playerId", DatabaseConnection.connection);
                break;
            case 4:
                command = new SqliteCommand("UPDATE player_car SET frontwheel_id = NULL WHERE player_id = @playerId", DatabaseConnection.connection);
                break;
            case 5:
                command = new SqliteCommand("UPDATE player_car SET backwheel_id = NULL WHERE player_id = @playerId", DatabaseConnection.connection);
                break;
        }

        command.Parameters.Add(new SqliteParameter("@playerId", playerId));

        command.ExecuteNonQuery();
    }

    public static void insertBox(int playerId, long acquired)
    {
        SqliteCommand command = new SqliteCommand("insert into boxes(player_id, acquired, opened) VALUES(@playerId, @acquired, 0)", DatabaseConnection.connection);

        command.Parameters.Add(new SqliteParameter("@playerId", playerId));
        command.Parameters.Add(new SqliteParameter("@acquired", acquired));

        command.ExecuteNonQuery();
    }

    public static int getNumberofWonInRow(int playerId)
    {
        List<GamePlayed> gamesPlayed = DatabaseDataAcces.getGamesPlayed(playerId);

        int lastWon = 0;

        for (int i = gamesPlayed.Count - 1; i >= 0; i--)
        {
            if (gamesPlayed[i].winner == 1) lastWon++; else break;
        }

        return lastWon;
    }

    public static int getNumberOfNotOpenedBoxes(int playerId)
    {
        List<Box> boxes = getPlayerBoxes(playerId);

        int cnt = 0;

        foreach (Box b in boxes)
        {
            if (b.opened == 0) cnt++;
        }

        return cnt;
    }

    public static void insertGamePlayed(int firstPlayer, int secondPlayer, int winner, int time)
    {
        SqliteCommand command = new SqliteCommand("insert into game_results(first_player,second_player,winner,time) VALUES(@fp, @sp, @winner, @time)", DatabaseConnection.connection);

        command.Parameters.Add(new SqliteParameter("@fp", firstPlayer));
        command.Parameters.Add(new SqliteParameter("@sp", secondPlayer));
        command.Parameters.Add(new SqliteParameter("@winner", winner));
        command.Parameters.Add(new SqliteParameter("@time", time));

        command.ExecuteNonQuery();
    }
}

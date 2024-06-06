"use strict";

import express from 'express'
import mysql from 'mysql2/promise'


const app = express()

app.use(express.json())

const port = 4444;

app.listen(port, (error)=>{
  if(!error)
  console.log(`App listening on: http://localhost:${port}`);
else
  console.log(`Error ocurred, server can't start: ${error}`);
})

async function connectToDB() {
    return await mysql.createConnection({
      host: "localhost",
      user: "manager",
      password: "cisco123",
      database: "fa_database",
    });
  }

app.post("/api/login", async (request, response) => {
    const { username, password } = request.body;
  
    let connection = null;
  
    try {
      connection = await connectToDB();

      const [results] = await connection.execute("CALL validate_login(?, ?, @registered_id, @login_successful, @status_message)", [username, password]);
      const [output] = await connection.query("SELECT @registered_id AS registered_id, @login_successful AS login_successful, @status_message AS status_message");
  
      const { registered_id, login_successful, status_message } = output[0];
  
      if (login_successful) {
        response.status(200).json({ message: 'Login successful', user_id: registered_id });
      } else {
        response.status(401).json({ message: 'Invalid credentials', user_id: null });
      }
    } finally {
      if (connection) {
        connection.end();
        console.log("Database connection closed");
      }
    }
  });
  
  app.post("/api/update_deck", async (request, response) => {
    const {player_id, card1, card2, card3, card4, card5} = request.body;
  
    let connection = null;
  
    try {
      connection = await connectToDB();
  
      await connection.execute("CALL update_deck(?, ?, ?, ?, ?, ?, @status_message)", [player_id, card1, card2, card3, card4, card5]);
      const [output] = await connection.query("SELECT @status_message AS status_message");
  
      const { status_message } = output[0];
  
      response.status(200).json({ message: status_message });
    } catch (error) {
      response.status(500).json({ error: error.message });
      console.log(error);
    } finally {
      if (connection) {
        connection.end();
        console.log("Database connection closed");
      }
    }
  });

app.post("/api/register", async (request, response) => {
  console.log('Request body:', request.body);

    const { registered_name, registered_password } = request.body;
  
    let connection = null;
  
    try {
      connection = await connectToDB();
  
      await connection.execute("CALL register_player(?, ?)", [registered_name, registered_password]);
      const [output] = await connection.query("SELECT @status_message AS status_message");
  
      const { status_message } = output[0];
  
      response.status(200).json({ message: status_message });
    } catch (error) {
      response.status(500).json({ error: error.message });
      console.log(error);
    } finally {
      if (connection) {
        connection.end();
        console.log("Database connection closed");
      }
    }
  });

app.get("/api/Character_card", async (request, response) => {
  let connection = null;

  try {


    connection = await connectToDB();

    const [results, fields] = await connection.execute("select * from character_ability;");

    console.log(`${results.length} rows returned`);
    const c={"cards":results};
    response.status(200).json(c);
  }
  catch (error) {
    response.status(500);
    response.json(error);
    console.log(error);
  }
  finally {
    
    if (connection !== null) {
      connection.end();
      console.log("Connection closed succesfully!");
    }
  }
});

app.get("/api/Pu_card", async (request, response) => {
  let connection = null;

  try {


    connection = await connectToDB();

    const [results, fields] = await connection.execute("Select * from powerup_ability" );

    console.log(`${results.length} rows returned`);
    const c={"powerUps":results};
    response.status(200).json(c);
  }
  catch (error) {
    response.status(500);
    response.json(error);
    console.log(error);
  }
  finally {
    
    if (connection !== null) {
      connection.end();
      console.log("Connection closed succesfully!");
    }
  }


})

app.get("/api/Deck_id/:ID", async (request, response) => {
  let connection = null;
  
  try {


    connection = await connectToDB();
    const [results, fields] = await connection.execute(
    `SELECT cc.*
    FROM Character_Ability cc
    JOIN Deck d ON cc.id IN (d.card1, d.card2, d.card3, d.card4, d.card5)
    WHERE d.id = ${request.params.ID}
    ORDER BY 
      CASE cc.id
        WHEN d.card1 THEN 1
        WHEN d.card2 THEN 2
        WHEN d.card3 THEN 3
        WHEN d.card4 THEN 4
        WHEN d.card5 THEN 5
      END;`);

    console.log(`${results.length} rows returned`);
    const c={"cards":results};
    response.status(200).json(c);
  }
  catch (error) {
    
    response.status(500);
    response.json(error);
    console.log(error);
  }
  finally {
    
    if (connection !== null) {
      connection.end();
      console.log("Connection closed succesfully!");
    }
  }
});

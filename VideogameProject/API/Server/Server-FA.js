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
});
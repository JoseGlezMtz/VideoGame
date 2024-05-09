"use strict";

import express from 'express'
import mysql from 'mysql2/promise'

const server = express()
const app = express()

app.use(express.json())

const port = 3000;

app.listen(port, ()=>{
  console.log(`running on port ${port}`)
})

async function connectToDB() {
    return await mysql.createConnection({
      host: "localhost",
      user: "manager",
      password: "cisco123",
      database: "cards_db",
    });
  }




app.get("/api/cards", async (request, response) => {
  let connection = null;

  try {


    connection = await connectToDB();

    const [results, fields] = await connection.execute("select * from card");

    console.log(`${results.length} rows returned`);
    response.status(200).json(results);
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
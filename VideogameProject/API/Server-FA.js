"use strict";

import express from 'express'

const server = express()

app.use(express.json())

const port = 3000;

async function connectToDB() {
    return await mysql.createConnection({
      host: "localhost",
      user: "Jose",
      password: "Jose1330",
      database: "cards_db",
    });
  }
"use strict"

import express, { response } from 'express';
import fs from "fs";
import mysql from 'mysql2/promise'

const port = 5000;
const app = express();

app.use(express.static("public"));
app.use(express.json());

async function connectToDB(){
    return await mysql.createConnection({host: 'localhost', user:'manager', password: 'cisco123', database:'fa_database'})
}

app.get(
    "/", (req, res)=>{
        const file = fs.readFileSync("public/html/play.html", "utf8", (err, html)=>{
            if (err) response.status(500).send('Error: ' + err)
            console.log("Loading page ...")
        response.send(html)
        });
        res.status(200).send(file);

        /*
        const file = fs.readFileSync("public/html/index.html", "utf8");
        res.status(200).send(file);
        */
    }
)

app.get('/stats/characters', async(request, response)=>{
    let connection = null
    try{
        connection = await connectToDB()

        let [results, fields] = await connection.query
        //Replace with information from the 
        ('select * from characters')

        console.log("Data sent correctly")
        response.status(200)
        response.json(results)
    }
    catch(error){
        response.status(500)
        response.json(error)
        console.log(error)
    }
    finally{
        if(connection!==null){
            connection.end()
            console.log("Connection closed succesfully!")
        }
    }
})

app.listen(port, ()=>{
    console.log(`Running on port ${port}`);
})
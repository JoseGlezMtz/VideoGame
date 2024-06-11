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
        const file = fs.readFileSync("public/html/Statistics.html", "utf8", (err, html)=>{
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

app.get('/stats/Game', async(request, response)=>{
    let connection = null
    try{
        connection = await connectToDB()

        let [results, fields] = await connection.query
        //Replace with information from the 
        ('select * FROM Game')

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
});
app.get('/stats/powerup_cards_played', async(request, response)=>{
    let connection = null
    try{
        connection = await connectToDB()

        let [results, fields] = await connection.query
        //Replace with information from the 
        ('select * FROM PowerUp_cards_played')

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
app.get('/stats/characters_played', async(request, response)=>{
    let connection = null
    try{
        connection = await connectToDB()

        let [results, fields] = await connection.query
        //Replace with information from the 
        ('select * FROM Characters_Cards_played')

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
    console.log(`Running on port http://localhost:${port}`);
})
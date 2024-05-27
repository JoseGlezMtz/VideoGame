"use strict"

import express from 'express';
import fs from "fs";

const port = 5000;

const app = express();
app.use(express.static("public"));

app.use(express.json());

app.get(
    "/", (req, res)=>{
        const file = fs.readFileSync("public/html/play.html", "utf8");
        res.status(200).send(file);

        /*
        const file = fs.readFileSync("public/html/index.html", "utf8");
        res.status(200).send(file);
        */
    }
)

app.listen(port, ()=>{
    console.log(`Running on port ${port}`);
})
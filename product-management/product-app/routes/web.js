const express = require("express");
const router = express.Router();
const { MongoClient, ObjectId } = require("mongodb");

const url = "mongodb://127.0.0.1:27017";
const client = new MongoClient(url);

// GET home page
router.get("/", async (req, res) => {
    await client.connect();
    const db = client.db("ProductDB");

    const products = await db.collection("ProductCollection")
        .find()
        .sort({ ProductStoreCode: -1 })  // SORT DESCENDING
        .toArray();

    res.render("index", { products });
});

// GET add page
router.get("/add", (req, res) => {
    res.render("addProduct");
});

// POST add product
router.post("/add", async (req, res) => {
    await client.connect();
    const db = client.db("ProductDB");
    
    await db.collection("ProductCollection").insertOne(req.body);
    res.redirect("/");
});

// DELETE product
router.get("/delete/:id", async (req, res) => {
    await client.connect();
    const db = client.db("ProductDB");

    await db.collection("ProductCollection")
        .deleteOne({ _id: new ObjectId(req.params.id) });

    res.redirect("/");
});

module.exports = router;

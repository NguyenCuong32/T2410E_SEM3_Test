const express = require("express");
const router = express.Router();
const { MongoClient, ObjectId } = require("mongodb");

const client = new MongoClient("mongodb://127.0.0.1:27017");

// Insert product
router.post("/products", async (req, res) => {
    try {
        await client.connect();
        const db = client.db("ProductDB");

        const result = await db.collection("ProductCollection")
                               .insertOne(req.body);

        res.json({ success: true, id: result.insertedId });

    } catch (err) {
        res.status(500).json({ error: err.message });
    }
});

// Delete product
router.delete("/products/:id", async (req, res) => {
    try {
        await client.connect();
        const db = client.db("ProductDB");

        await db.collection("ProductCollection")
                .deleteOne({ _id: new ObjectId(req.params.id) });

        res.json({ success: true });

    } catch (err) {
        res.status(500).json({ error: err.message });
    }
});

module.exports = router;

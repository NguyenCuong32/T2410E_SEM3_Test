const express = require('express');
const router = express.Router();
const Product = require('../models/product');


router.get('/', async (req, res) => {
    try {
        const products = await Product.find().sort({ ProductStoreCode: -1 });
        res.render('index', { products });
    } catch (err) {
        res.status(500).send(err.message);
    }
});


router.post('/add', async (req, res) => {
    try {
        const { ProductCode, ProductName, ProductDate, ProductOriginPrice, Quantity, ProductStoreCode } = req.body;

        await Product.create({
            ProductCode,
            ProductName,
            ProductDate,
            ProductOriginPrice,
            Quantity,
            ProductStoreCode
        });

        res.redirect('/');
    } catch (err) {
        console.error(err);
        res.status(500).send("Error adding product: " + err.message);
    }
});


router.post('/delete/:id', async (req, res) => {
    try {
        await Product.findByIdAndDelete(req.params.id);
        res.redirect('/');
    } catch (err) {
        console.error(err);
        res.status(500).send("Error deleting product");
    }
});

module.exports = router;

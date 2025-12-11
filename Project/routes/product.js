const express = require('express');
const router = express.Router();
const Product = require('../Models/Product');

// GET all products, sorted by ProductStoreCode and descending quantity
router.get('/', async (req, res) => {
    const products = await Product.find().sort({ ProductStoreCode: 1, Quantity: -1 });
    res.render('index', { products });
});

// POST add new product
router.post('/', async (req, res) => {
    try {
        const { ProductCode, ProductName, ProductDate, ProductOriginPrice, Quantity, ProductStoreCode } = req.body;
        const newProduct = new Product({
            ProductCode,
            ProductName,
            ProductDate,
            ProductOriginPrice,
            Quantity,
            ProductStoreCode
        });
        await newProduct.save();
        res.redirect('/products');
    } catch (err) {
        res.send('Error: ' + err);
    }
});

// DELETE product
router.delete('/:id', async (req, res) => {
    try {
        await Product.findByIdAndDelete(req.params.id);
        res.redirect('/products');
    } catch (err) {
        res.send('Error: ' + err);
    }
});

module.exports = router;

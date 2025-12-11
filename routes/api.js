const express = require('express');
const router = express.Router();
const Product = require('../models/product');


router.post('/products', async (req, res) => {
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
        res.status(201).json({ message: 'Product created successfully', product: newProduct });
    } catch (err) {
        res.status(500).json({ error: err.message });
    }
});


router.delete('/products/:id', async (req, res) => {
    try {
        const product = await Product.findOneAndDelete({ ProductCode: req.body.ProductCode } || { _id: req.params.id });
        
        const deleteResult = await Product.findByIdAndDelete(req.params.id);

        if (!deleteResult) {
           
            return res.status(404).json({ message: 'Product not found' });
        }

        res.json({ message: 'Product deleted successfully' });
    } catch (err) {
        res.status(500).json({ error: err.message });
    }
});

module.exports = router;

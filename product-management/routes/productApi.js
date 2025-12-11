const express = require('express');
const router = express.Router();
const Product = require('../models/Product');

// Insert product
router.post('/add', async (req, res) => {
    try {
        const product = new Product(req.body);
        await product.save();
        res.json({ message: "Product inserted", product });
    } catch (err) {
        res.status(500).json({ error: err.message });
    }
});

// Delete product
router.delete('/delete/:code', async (req, res) => {
    try {
        await Product.deleteOne({ ProductCode: req.params.code });
        res.json({ message: "Product deleted" });
    } catch (err) {
        res.status(500).json({ error: err.message });
    }
});

module.exports = router;

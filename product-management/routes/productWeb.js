const express = require('express');
const router = express.Router();
const Product = require('../models/Product');

// Show all products + sort descending ProductStoreCode
router.get('/', async (req, res) => {
    const products = await Product.find().sort({ ProductStoreCode: -1 });
    res.render('index', { products });
});

// Form thêm sản phẩm
router.get('/add', (req, res) => {
    res.render('add');
});

// POST thêm sản phẩm
router.post('/add', async (req, res) => {
    await Product.create(req.body);
    res.redirect('/');
});

// Delete
router.get('/delete/:code', async (req, res) => {
    await Product.deleteOne({ ProductCode: req.params.code });
    res.redirect('/');
});

module.exports = router;

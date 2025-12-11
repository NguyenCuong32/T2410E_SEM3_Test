const express = require('express');
const router = express.Router();
const Product = require('../models/Product');

// Trang chủ: hiển thị tất cả sản phẩm, có sắp xếp theo ProductStoreCode giảm dần
router.get('/', async (req, res) => {
  try {
    const products = await Product.find().sort({ ProductStoreCode: -1 });
    res.render('index', { products });
  } catch (err) {
    res.status(500).send('Server error');
  }
});

// Thêm sản phẩm từ form
router.post('/add', async (req, res) => {
  try {
    const product = new Product(req.body);
    await product.save();
    res.redirect('/');
  } catch (err) {
    res.status(400).send(`Add failed: ${err.message}`);
  }
});

// Xóa sản phẩm từ nút trên bảng
router.post('/delete/:code', async (req, res) => {
  try {
    await Product.deleteOne({ ProductCode: req.params.code });
    res.redirect('/');
  } catch (err) {
    res.status(400).send(`Delete failed: ${err.message}`);
  }
});

module.exports = router;
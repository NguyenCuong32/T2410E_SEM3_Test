const express = require('express');
const router = express.Router();
const Product = require('../models/Product');

// Thêm sản phẩm (POST /api/products)
router.post('/products', async (req, res) => {
  try {
    const product = new Product(req.body);
    await product.save();
    res.status(201).json(product);
  } catch (err) {
    res.status(400).json({ error: err.message });
  }
});

// Xóa sản phẩm theo ProductCode (DELETE /api/products/:code)
router.delete('/products/:code', async (req, res) => {
  try {
    const result = await Product.deleteOne({ ProductCode: req.params.code });
    if (result.deletedCount === 0) {
      return res.status(404).json({ message: 'Product not found' });
    }
    res.json({ message: 'Deleted successfully' });
  } catch (err) {
    res.status(400).json({ error: err.message });
  }
});

module.exports = router;
const express = require('express');
const router = express.Router();
const Product = require('../models/Product');

router.post('/products', async (req, res) => {
  try {
    let { ProductCode, ProductName, ProductDate, ProductOriginPrice, Quantity, ProductStoreCode } = req.body;

    let date = null;
    if (ProductDate) {
      if (ProductDate.includes('/')) {
        const [d,m,y] = ProductDate.split('/');
        date = new Date(`${y}-${m}-${d}`);
      } else {
        date = new Date(ProductDate);
      }
    }

    const product = new Product({
      ProductCode,
      ProductName,
      ProductDate: date,
      ProductOriginPrice: Number(String(ProductOriginPrice).replace(/[.,]/g, '')) || Number(ProductOriginPrice),
      Quantity: Number(Quantity) || 0,
      ProductStoreCode
    });

    await product.save();
    res.status(201).json({ success: true, data: product });
  } catch (err) {
    res.status(400).json({ success: false, error: err.message });
  }
});

router.delete('/products/:id', async (req, res) => {
  try {
    const idOrCode = req.params.id;
    let product = null;
    if (/^[0-9a-fA-F]{24}$/.test(idOrCode)) {
      product = await Product.findByIdAndDelete(idOrCode);
    }
    if (!product) {
      product = await Product.findOneAndDelete({ ProductCode: idOrCode });
    }
    if (!product) return res.status(404).json({ success: false, error: 'Product not found' });
    res.json({ success: true, data: product });
  } catch (err) {
    res.status(500).json({ success: false, error: err.message });
  }
});

module.exports = router;

const express = require('express');
const router = express.Router();
const Product = require('../models/Product');

router.get('/', async (req, res) => {
  try {
    const products = await Product.find().lean();

    const groups = {};
    products.forEach(p => {
      const key = p.ProductStoreCode || 'Unknown';
      if (!groups[key]) groups[key] = [];
      groups[key].push(p);
    });

    for (const k of Object.keys(groups)) {
      groups[k].sort((a,b) => (b.ProductOriginPrice || 0) - (a.ProductOriginPrice || 0));
    }

    res.render('index', { groups });
  } catch (err) {
    res.status(500).send(err.message);
  }
});

router.get('/products/new', (req, res) => {
  res.render('new');
});

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
    res.redirect('/');
  } catch (err) {
    res.status(400).send(err.message);
  }
});

router.delete('/products/:id', async (req, res) => {
  try {
    const id = req.params.id;
    await Product.findByIdAndDelete(id);
    res.redirect('/');
  } catch (err) {
    res.status(500).send(err.message);
  }
});

module.exports = router;

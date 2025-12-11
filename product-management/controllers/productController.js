const Product = require('../models/Product');

// 1. REST API
exports.apiGetAll = async (req, res) => {
  try {
    const products = await Product.find();
    res.json(products);
  } catch (err) {
    res.status(500).json({ message: err.message });
  }
};

exports.apiCreate = async (req, res) => {
  const product = new Product(req.body);
  try {
    const newProduct = await product.save();
    res.status(201).json(newProduct);
  } catch (err) {
    res.status(400).json({ message: err.message });
  }
};

exports.apiDelete = async (req, res) => {
  try {
    await Product.findByIdAndDelete(req.params.id);
    res.json({ message: 'Deleted successfully' });
  } catch (err) {
    res.status(500).json({ message: err.message });
  }
};

// 2 + 3. Web
exports.webGetAll = async (req, res) => {
  try {
    let products = await Product.find().sort({ ProductStoreCode: -1, ProductOriginPrice: -1 });
    // Nhóm theo ProductStoreCode để hiển thị đẹp hơn (tùy chọn)
    const grouped = products.reduce((acc, p) => {
      (acc[p.ProductStoreCode] = acc[p.ProductStoreCode] || []).push(p);
      return acc;
    }, {});
    res.render('index', { products, grouped });
  } catch (err) {
    res.status(500).send(err.message);
  }
};

exports.webAddForm = (req, res) => res.render('add');
exports.webCreate = async (req, res) => {
  const product = new Product(req.body);
  try {
    await product.save();
    res.redirect('/');
  } catch (err) {
    res.render('add', { error: err.message });
  }
};

exports.webDelete = async (req, res) => {
  try {
    await Product.findByIdAndDelete(req.params.id);
    res.redirect('/');
  } catch (err) {
    res.redirect('/');
  }
};
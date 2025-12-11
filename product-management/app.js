const express = require('express');
const mongoose = require('mongoose');
const methodOverride = require('method-override');
const path = require('path');

const app = express();

// MongoDB connection
mongoose.connect('mongodb://127.0.0.1:27017/productDB')
    .then(() => console.log('MongoDB connected'))
    .catch(err => console.log(err));

// Middleware
app.use(express.urlencoded({ extended: true }));
app.use(express.json());
app.use(methodOverride('_method'));
app.set('view engine', 'ejs');
app.set('views', path.join(__dirname, 'views'));
app.use(express.static(path.join(__dirname, 'public')));

// Model
const Product = require('./models/Product');

// =======================
// REST API
// =======================

// Add product via API
app.post('/api/products', async (req, res) => {
    try {
        const product = new Product(req.body);
        await product.save();
        res.status(201).json({ message: 'Product added', product });
    } catch (err) {
        res.status(400).json({ error: err.message });
    }
});

// Delete product via API
app.delete('/api/products/:id', async (req, res) => {
    try {
        await Product.findByIdAndDelete(req.params.id);
        res.json({ message: 'Product deleted' });
    } catch (err) {
        res.status(400).json({ error: err.message });
    }
});

// =======================
// Website routes
// =======================

// Get all products and display website
app.get('/products', async (req, res) => {
    try {
        // Sort by ProductStoreCode descending, then ProductOriginPrice descending
        const products = await Product.find().sort({ ProductStoreCode: -1, ProductOriginPrice: -1 });
        res.render('index', { products });
    } catch (err) {
        res.status(500).send(err.message);
    }
});

// Add product from website form
app.post('/products', async (req, res) => {
    try {
        const product = new Product(req.body);
        await product.save();
        res.redirect('/products');
    } catch (err) {
        res.status(400).send(err.message);
    }
});

// Delete product from website
app.delete('/products/:id/delete', async (req, res) => {
    try {
        await Product.findByIdAndDelete(req.params.id);
        res.redirect('/products');
    } catch (err) {
        res.status(400).send(err.message);
    }
});

// =======================
// Start server
// =======================
const PORT = 3000;
app.listen(PORT, () => console.log(`Server running on port ${PORT}`));

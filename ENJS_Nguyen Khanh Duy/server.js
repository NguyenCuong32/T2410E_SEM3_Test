const express = require('express');
const mongoose = require('mongoose');
const bodyParser = require('body-parser');
const app = express();
const Product = require('./models/Product');

// Connect to MongoDB
mongoose.connect('mongodb://localhost:27017/ProductDB', { useNewUrlParser: true, useUnifiedTopology: true })
    .then(() => console.log('MongoDB Connected'))
    .catch(err => console.log(err));

// Middleware
app.use(bodyParser.urlencoded({ extended: true }));
app.use(bodyParser.json());
app.set('view engine', 'ejs');

// Routes for Website
app.get('/', async (req, res) => {
    try {
        // Requirement 3: Sort products on same ProductStoreCode, sort in descending order
        // The requirement "Sort products on same ProductStoreCode" might mean group by store code?
        // "sort in descending order on website" likely implies sorting by ProductStoreCode DESC.
        const products = await Product.find().sort({ ProductStoreCode: -1 });
        res.render('index', { products });
    } catch (err) {
        res.status(500).send(err.message);
    }
});

app.post('/add', async (req, res) => {
    try {
        const { ProductCode, ProductName, ProductDate, ProductOriginPrice, Quantity, ProductStoreCode } = req.body;
        // Basic date handling needed? Input type="date" returns YYYY-MM-DD which is fine for new Date()
        // If "22/08/2023" comes in from a text input, we need parsing.
        // Assuming HTML5 date input for Web, and consistent format for API.
        // Let's support the DD/MM/YYYY text format if detected, otherwise standard.

        let dateToStore = ProductDate;
        if (typeof ProductDate === 'string' && ProductDate.includes('/')) {
            const parts = ProductDate.split('/');
            // DD/MM/YYYY -> YYYY-MM-DD
            if (parts.length === 3) {
                dateToStore = new Date(`${parts[2]}-${parts[1]}-${parts[0]}`);
            }
        }

        await Product.create({
            ProductCode,
            ProductName,
            ProductDate: dateToStore,
            ProductOriginPrice,
            Quantity,
            ProductStoreCode
        });
        res.redirect('/');
    } catch (err) {
        res.status(500).send(err.message);
    }
});

app.post('/delete/:id', async (req, res) => {
    try {
        await Product.findByIdAndDelete(req.params.id);
        res.redirect('/');
    } catch (err) {
        res.status(500).send(err.message);
    }
});

// Routes for REST API (Requirement 1)
app.post('/api/products', async (req, res) => {
    try {
        // Handle date parsing logic for API as well if needed, assuming JSON payload standard ISO or same logic
        const product = await Product.create(req.body);
        res.status(201).json(product);
    } catch (err) {
        res.status(400).json({ error: err.message });
    }
});

app.delete('/api/products/:id', async (req, res) => {
    try {
        await Product.findByIdAndDelete(req.params.id);
        res.status(200).json({ message: 'Product deleted' });
    } catch (err) {
        res.status(500).json({ error: err.message });
    }
});

const PORT = 3000;
app.listen(PORT, () => {
    console.log(`Server is running on port ${PORT}`);
});

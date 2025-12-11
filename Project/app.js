const express = require('express');
const mongoose = require('mongoose');
const bodyParser = require('body-parser');
const methodOverride = require('method-override');
const productRoutes = require('./routes/product');

const app = express();

// Connect MongoDB
mongoose.connect('mongodb://127.0.0.1:27017/ProductDB')
    .then(() => console.log('MongoDB connected'))
    .catch(err => console.log('MongoDB connection error:', err));

// Middleware
app.use(bodyParser.urlencoded({ extended: true }));
app.use(methodOverride('_method'));
app.use(express.static('public'));
app.set('view engine', 'ejs');

// Routes
app.use('/products', productRoutes);

// Start server
app.listen(3000, () => {
    console.log('Server running on http://localhost:3000');
});

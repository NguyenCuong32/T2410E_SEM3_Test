const express = require('express');
const mongoose = require('mongoose');
const path = require('path');
const bodyParser = require('body-parser');

const app = express();

// Kết nối MongoDB
mongoose.connect('mongodb://localhost:27017/productDB');

// Middleware
app.use(bodyParser.json());
app.use(express.urlencoded({ extended: true }));
app.use(express.static(path.join(__dirname, 'public')));

// View engine
app.set('view engine', 'ejs');
app.set('views', path.join(__dirname, 'views'));

// Model & Routes
const Product = require('./models/Product');
app.use('/api', require('./routes/api')); // REST API
app.use('/', require('./routes/web'));    // Website

// Start server
const PORT = 3000;
app.listen(PORT, () => {
  console.log(`Server running at http://localhost:${PORT}`);
});